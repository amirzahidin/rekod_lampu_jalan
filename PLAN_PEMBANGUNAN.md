# Plan Pembangunan Semula — Street Node (Rekod Lampu Jalan)

**Tarikh:** 2 Jun 2026
**Disediakan untuk:** Amir (Vertbyte)
**Status:** Draf plan — untuk kelulusan sebelum kerja pembangunan bermula

---

## 1. Ringkasan Eksekutif

Aplikasi sedia ada (**.NET MAUI + PHP mentah + MySQL shared hosting**) berfungsi tetapi sudah sampai had dari segi prestasi, keselamatan dan kebolehskalaan. Project ni akan dibangun semula dari awal menggunakan:

| Lapisan | Stack baru | Versi terkini (Jun 2026) |
|---|---|---|
| Mobile | **Flutter / Dart** (Android **dan** iOS) | Flutter 3.44, Dart 3.12 |
| Backend API | **Laravel (PHP)** | Laravel 13.6, Sanctum 4.3 |
| Pangkalan data | **MySQL** (kekal cPanel) | MySQL 8.x |
| Pembayaran | **ToyyibPay (utama) + Billplz (pilihan)** | FPX + DuitNow QR |
| Storan gambar | Folder `uploads/` cPanel + auto-resize | — |

Matlamat: app yang laju walaupun data besar, **selamat**, ada langganan berbayar (payment gateway), dan saiz gambar dikecilkan automatik. App disasarkan untuk **Android dan iOS** (satu codebase Flutter).

---

## 2. Hasil Kajian Sistem Sedia Ada

### 2.1 Mobile App (`rekod_lampu_jalan`)
- **Framework:** .NET MAUI `net9.0` (Android, iOS, MacCatalyst, Windows). App ID `com.vertbyte.streetnode`, nama paparan "Street Node", versi 1.1 (build 10).
- **Seni bina:** MVVM separa (CommunityToolkit.Mvvm) tetapi banyak logik bercampur dalam code-behind & `Utils/`.
- **Peta:** `Onion.Maui.GoogleMaps` + clustering. Ada pin lampu jalan, feeder pillar, dan polyline fly cable.
- **Fungsi utama:** login, papar peta semua aset, tambah/edit/padam pin lampu, feeder pillar, fly cable, muat naik gambar setiap komponen, halaman ringkasan (Summary).
- **Komponen data:** `map_pin` (lampu jalan — banyak medan rujukan), `feeder_pillar`, `fly_cable`, `alamat`, jadual rujukan (jenis_item, jenis_lampu, dll).

### 2.2 API (`API Record lampu jalan`)
- PHP mentah (satu fail = satu endpoint), MySQL via `mysqli`.
- Endpoint: `login`, `selectAll`, `selectById`, `selectAllReference`, `selectAllAlamat`, `insert/update/delete` untuk MapPin, FeederPillar, FlyCable, Reference, `checkAlamat`, `refresh`.
- Gambar disimpan dalam `uploads/` di server (tidak dapat dimuat turun — terlalu besar; ini juga bukti masalah saiz gambar).

### 2.3 Isu Kritikal Yang Dijumpai

**Keselamatan (paling utama):**
1. **Kredential DB hardcoded** dalam `config.php` (username/password tertera dalam repo).
2. **Google Maps API key hardcoded** dalam `Variables.cs` & `ConstantValue.cs` — boleh disalahguna.
3. **Password disimpan plaintext** dalam jadual `user`, dibanding terus (`$user['password']==$password`). Tiada hashing.
4. **Tiada token/auth sebenar** — `login.php` cuma pulangkan `id` & `username`. Semua endpoint lain terdedah tanpa pengesahan (sesiapa boleh insert/delete).
5. **SQL injection berisiko** — `selectById.php` masukkan `$id` terus ke query (`WHERE user_id=$id`). `login` guna escape sahaja.
6. **CORS `*`** dibuka penuh pada semua endpoint.

**Prestasi / Big Data:**
7. **`selectAll.php` tarik SEMUA rekod** (map_pin + fly_cable + feeder_pillar) sekali gus tanpa pagination, tanpa had kawasan peta. Ini punca utama app jadi perlahan bila data membesar.
8. Tiada **pagination, bounding-box (viewport) query, atau caching**.
9. App muat semua pin ke memori — berat untuk peranti mudah alih.

**Kualiti gambar:**
10. **Tiada auto-resize** — gambar penuh resolusi kamera dimuat naik (sebab tu folder `uploads/` jadi terlalu besar).

**Bug sebenar dalam error_log:**
11. `insertFeederPillar.php` — `Undefined array key "user_name"`, `"user_id"`, dan `Column 'alamat' cannot be null` (fatal). Insert feeder pillar gagal.
12. `updateMapPin.php` — banyak `Undefined array key` (panjang_lengan_lampu, jenama_lampu, dll) — mismatch nama medan antara app & API.

---

## 3. Seni Bina Target

```
┌─────────────────────────┐        HTTPS/JSON + Bearer token (Sanctum)
│   Flutter Mobile App     │  ───────────────────────────────────────►  ┌──────────────────────────┐
│  (Android / iOS)         │                                            │   Laravel 13 REST API     │
│                          │  ◄───────────────────────────────────────  │  (atas cPanel/public_html)│
│  - Riverpod state mgmt   │        JSON ter-pagination + cache          │                           │
│  - google_maps_flutter   │                                            │  - Sanctum auth (token)   │
│  - dio + retry           │                                            │  - Eloquent ORM           │
│  - drift (SQLite cache)  │                                            │  - Form Request validate │
│  - flutter_image_compress│                                            │  - Intervention/Image     │
└─────────────────────────┘                                            │  - Spatial bounding query │
                                                                        └────────────┬─────────────┘
                                                                                     │
                                                              ┌──────────────────────┼───────────────────┐
                                                              ▼                       ▼                   ▼
                                                       MySQL 8 (cPanel)      uploads/ (auto-resize)   ToyyibPay/Billplz
                                                       + index + spatial                              (FPX/DuitNow)
```

### 3.1 Mobile (Flutter 3.44)
- **State management:** Riverpod (mudah uji, sesuai data besar).
- **Peta:** `google_maps_flutter` + clustering (`google_maps_cluster_manager`).
- **HTTP:** `dio` (interceptor untuk token, retry, logging).
- **Cache tempatan:** `drift` (SQLite) — simpan pin secara offline-first, sync bila online.
- **Gambar:** `flutter_image_compress` — kecilkan sebelum upload (lihat Seksyen 6).
- **Seni bina:** feature-first + clean (data/domain/presentation layer).

### 3.2 Backend (Laravel 13.6)
- **Auth:** Sanctum (token Bearer per peranti, boleh revoke).
- **ORM:** Eloquent + migration (skema versioned, tak payah edit DB manual).
- **Validation:** Form Request (elak bug "undefined key" & null column).
- **Resize gambar:** Intervention Image (server-side, lihat Seksyen 6).
- **Config:** `.env` (kredential keluar dari kod).
- **Deploy ke cPanel:** letak Laravel di luar `public_html`, symlink folder `public` — masih boleh atas shared hosting.

---

## 4. Cadangan Penambahbaikan Features

### 4.1 Keutamaan Tinggi (teras)
1. **Autentikasi sebenar + peranan (role):** Admin, Supervisor, Field worker. Token Sanctum, password di-hash (bcrypt).
2. **Mode offline-first:** rekod & gambar disimpan tempatan, auto-sync bila ada internet (sangat berguna untuk kerja lapangan kawasan liputan lemah).
3. **Carian & penapis pin:** ikut jenis aset, status (rosak/elok), kawasan/alamat, julat tarikh.
4. **Status penyelenggaraan:** tanda lampu "rosak / dalam baik pulih / selesai", dengan sejarah (audit trail) — kembangkan `user_log` sedia ada.
5. **Dashboard ringkasan:** jumlah aset, peratus rosak, aset ikut kawasan, carta — naik taraf `SummaryPage`.

### 4.2 Keutamaan Sederhana
6. **Eksport laporan:** PDF / Excel (per kawasan / per tarikh) untuk client/majlis.
7. **Foto bergeotag + timestamp** automatik pada gambar lapangan.
8. **Navigasi ke pin:** buka Google Maps/Waze terus ke lokasi aset.
9. **Push notification** (Firebase) — tugasan baru, peringatan penyelenggaraan.
10. **Multi-tenant / company:** pisahkan data ikut syarikat (jadual `user` dah ada `company_name`).

### 4.3 Nice-to-have
11. **Imbas QR / NFC** pada tiang untuk akses rekod terus.
12. **Heatmap** kepadatan kerosakan.
13. **Web admin panel** (Laravel Filament/Nova) untuk urus data tanpa app.

> Cadangan: Fasa 1 fokus features teras (#1–#5) + payment. Selebihnya fasa kemudian.

---

## 5. Payment Gateway (Malaysia — FPX)

**Pilihan utama: ToyyibPay** — fee rendah, sokong FPX + DuitNow QR, Shariah-compliant, API ringkas (pulang Bill Code JSON). Sesuai model langganan tempatan.
**Sandaran: Billplz** — payout lebih laju (FPX next-day, DuitNow real-time), lebih banyak e-wallet (GrabPay, TNG, ShopeePay, Boost) + BNPL.

### Model langganan dicadangkan
- **Free:** had bilangan pin / 1 pengguna.
- **Pro (bulanan/tahunan):** pin tanpa had, eksport laporan, multi-user.
- **Enterprise:** multi-company, web panel, sokongan.

### Aliran teknikal
1. App → Laravel: minta create bill (`amount`, `plan`, `user`).
2. Laravel → ToyyibPay API: create bill, dapat `BillCode` + URL.
3. App buka URL pembayaran (in-app webview / browser).
4. ToyyibPay → **callback/webhook** ke Laravel (`return_url` + `callback_url`) → sahkan status → kemaskini `subscriptions`.
5. App poll/refresh status langganan.

> **Penting:** sentiasa sahkan pembayaran melalui webhook server-side, jangan percaya status dari app sahaja. Simpan jadual `subscriptions` & `payment_transactions`.

---

## 6. Auto-Resize Gambar (2 lapisan)

**Masalah sekarang:** gambar resolusi penuh dimuat naik → `uploads/` membengkak (tak dapat download), app jadi lambat tarik gambar.

**Lapisan 1 — Client (Flutter):** sebelum upload, `flutter_image_compress` kecilkan:
- Resize sisi terpanjang ke ~1600px, kualiti JPEG ~80%.
- Tujuan: jimat data mobile + laju upload (selalunya 3–5MB → ~300–500KB).

**Lapisan 2 — Server (Laravel + Intervention Image):** semasa terima, hasilkan 2 saiz:
- `full/` — maksimum 1920px (untuk paparan penuh).
- `thumb/` — 400px (untuk senarai & marker peta — muat sangat laju).
- Tukar ke format `.webp` (lebih kecil ~30% dari JPEG).

**Folder `uploads/` sedia ada (yang besar):**
Sebab fail tak dapat dimuat turun, resize mesti dijalankan **di server**. Plan: skrip Artisan command (`php artisan images:optimize`) yang:
- Imbas `uploads/`, jana versi `thumb/` & `full/` yang dimampatkan, kemas kini rujukan DB.
- Jalankan sekali (one-time migration) — boleh batch supaya tak timeout shared hosting.
- App baru hanya tarik `thumb/` untuk senarai/peta → jimat banyak.

---

## 7. Pengoptimuman Data (Big Data)

1. **Ganti `selectAll`** dengan endpoint **bounding-box**: hanya pulang pin dalam viewport peta semasa (`?minLat=&maxLat=&minLng=&maxLng=`). Inilah penyelesaian terbesar untuk kelajuan.
2. **Pagination** untuk senarai (`?page=&per_page=`) + cursor pagination untuk dataset besar.
3. **Index DB:** tambah index pada `lattitude`, `longitude`, `user_id`, `status`, `alamat`. Pertimbang `SPATIAL INDEX` (`POINT`) untuk query geo.
4. **Clustering server-side** (optional) bila zoom jauh — pulang gugusan, bukan setiap pin.
5. **Cache:** respons rujukan (jenis_item dll jarang berubah) di-cache di app (drift) & server (Laravel cache).
6. **Pilih medan** (`SELECT id, lat, lng, status` untuk marker) — jangan tarik semua lajur + attachment untuk paparan peta.
7. **Sync delta:** hanya tarik rekod berubah sejak `updated_at` terakhir (offline-first).
8. **Thumbnail** untuk gambar (Seksyen 6) — kurangkan bandwidth dramatik.

---

## 8. Migrasi Data

- Skema MySQL sedia ada **dikekalkan** (data tak hilang). Laravel migration dipetakan ke jadual sedia ada.
- Langkah selamat: hash semula password pengguna (paksa reset atau hash on next login), tambah lajur baru (`role`, `subscription_id`, timestamps) tanpa drop data.
- Folder `uploads/` kekal di server; jalankan command optimize (Seksyen 6).
- Backup penuh DB + uploads sebelum sebarang migrasi.

---

## 9. Fasa Pembangunan

| Fasa | Skop | Hasil |
|---|---|---|
| **0. Persediaan** | Repo baru, backup DB+uploads, audit skema, setup `.env`, keluarkan rahsia dari kod | Persekitaran selamat |
| **1. Backend Laravel** | Migrate skema, Sanctum auth, hash password, Form Request, betulkan bug insert/update, endpoint bounding-box + pagination, Intervention resize | API selamat & laju |
| **2. Optimize gambar** | Client compress + server resize/webp + command batch `uploads/` lama | Saiz gambar kecil |
| **3. Mobile Flutter (teras)** | Auth, peta + clustering, CRUD pin/feeder/flycable, upload gambar termampat, offline cache | App boleh guna |
| **4. Features teras** | Role, carian/penapis, status penyelenggaraan + audit, dashboard | Nilai tambah |
| **5. Payment** | Integrasi ToyyibPay, jadual subscription, webhook, gating Pro | Monetisasi |
| **6. Penambahbaikan** | Eksport laporan, push notif, geotag, navigasi, (web panel) | Lengkap |
| **7. QA & Lancar** | Uji beban data besar, keselamatan, build store, migrasi data live | Go-live |

---

## 10. Risiko & Perhatian

- **Shared cPanel ada had:** tiada queue worker tetap, masa eksekusi terhad, tiada Redis. Penyelesaian: guna `cron` untuk schedule, cache fail/DB, batch command kecil. Jika trafik membesar, naik taraf VPS akan jadi perlu (boleh dirancang kemudian).
- **Tukar API key Google Maps** & hadkan ikut package name/SHA + restrict HTTP referrer. Key lama dalam repo **mesti dibatalkan**.
- **Tukar password DB** yang terdedah dalam repo segera.
- **Rewrite penuh** (MAUI→Flutter) ambil masa — sebab tu fasa-fasa di atas supaya boleh hantar berperingkat.
- Uji **callback payment** dalam mode sandbox dahulu.

---

## 11. Naik Taraf Keselamatan (Android + iOS)

Keselamatan jadi tiang utama project ni, merentas backend, mobile, dan kedua-dua platform.

### 11.1 Backend / API
- **Hapus semua rahsia dari kod** → pindah ke `.env` (DB, API key, secret payment). Batalkan & jana semula key DB + Google Maps yang dah terdedah dalam repo.
- **Password hashing** bcrypt/argon2 (paksa reset atau re-hash on login).
- **Auth token Sanctum** per peranti, boleh revoke; token tamat tempoh + refresh.
- **Prepared statements / Eloquent** sepenuhnya — hapus risiko SQL injection (cth `selectById`).
- **Validation ketat** (Form Request) pada setiap input — elak null/undefined & data jahat.
- **Authorization (policy/role)** — setiap endpoint semak peranan & pemilikan data (multi-tenant).
- **Rate limiting** (throttle) untuk login & API — halang brute force.
- **CORS ketat** — hadkan kepada origin app sahaja, bukan `*`.
- **HTTPS wajib** + header keselamatan (HSTS). Webhook payment sahkan signature.
- **Audit log** semua tindakan tulis (kembangkan `user_log`).

### 11.2 Mobile (Flutter — Android & iOS)
- **Storan selamat token:** `flutter_secure_storage` → Android **Keystore** + iOS **Keychain** (bukan SharedPreferences/plaintext).
- **Certificate pinning** (dio + `http_certificate_pinning`) — halang serangan MITM.
- **Sembunyikan API key:** guna `--dart-define` / config runtime, jangan hardcode dalam source.
- **Biometrik (optional):** `local_auth` — Face ID / fingerprint untuk buka app.
- **Obfuscation:** build dengan `--obfuscate --split-debug-info` (Android & iOS) supaya kod susah di-reverse.
- **Root/jailbreak detection** (optional, untuk data sensitif).

### 11.3 Khusus Platform
- **Android:** `network_security_config` (block cleartext), minify+R8, Play App Signing, target API terkini ikut keperluan Play Store.
- **iOS:** App Transport Security (ATS) aktif, simpan rahsia dalam Keychain, ikut garis panduan privasi App Store (Privacy Manifest).

### 11.4 Pematuhan & Data
- **Backup berkala** DB + uploads, simpan berasingan.
- **Enkripsi data rehat** untuk maklumat sensitif jika perlu.
- **Dasar privasi** (PDPA Malaysia) — penting bila ada akaun & pembayaran.

---

## 12. Tindakan Seterusnya

Sahkan plan ni (atau beritahu bahagian nak diubah), kemudian aku boleh mula **Fasa 0 + Fasa 1**: scaffold projek Laravel baru, petakan skema sedia ada ke migration, dan bina endpoint auth + bounding-box yang selamat. Aku akan tanya beberapa butiran skema DB (nama lajur penuh setiap jadual) sebelum tulis migration.
