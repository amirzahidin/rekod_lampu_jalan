using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Devices.Sensors; // for Location
using Microsoft.Maui.ApplicationModel.Communication;
using System.Text.Json; // for Geocoding

namespace RekodLampuJalan.Utils
{
    public class LocationFunction
    {
        public async Task<AlamatInsert> GetAddress(Location location)
        {
            var alamat = new AlamatInsert();

            var placemarks = await Geocoding.GetPlacemarksAsync(location);

            if (placemarks != null)
            {
                var placemark = placemarks.FirstOrDefault();
                if (placemark != null)
                {
                    alamat.nama_jalan = placemark.Thoroughfare;     // Street name
                    alamat.bandar = placemark.SubLocality;
                    alamat.daerah = placemark.Locality;         // District / Daerah
                    alamat.poskod = placemark.PostalCode;           // Poskod
                    alamat.negeri = placemark.AdminArea;            // State / Negeri
                }
            }

            return alamat;
        }
        public async Task<List<AlamatInsert>> GetAddressList(Location location)
        {
            List<AlamatInsert> alamatList = new List<AlamatInsert>();

            var placemarks = await Geocoding.GetPlacemarksAsync(location);

            if (placemarks != null)
            {
                foreach (var placemark_ in placemarks)
                {
                    var alamat = new AlamatInsert();
                    if (placemark_ != null)
                    {
                        alamat.nama_jalan = placemark_.Thoroughfare;     // Street name
                        alamat.bandar = placemark_.SubLocality;
                        alamat.daerah = placemark_.Locality;         // District / Daerah
                        alamat.poskod = placemark_.PostalCode;           // Poskod
                        alamat.negeri = placemark_.AdminArea;            // State / Negeri
                        alamatList.Add(alamat);
                    }
                }
                //   var placemark = placemarks.FirstOrDefault();

            }

            return alamatList;
        }
        public async Task<AddressResponse> getAdressId(AlamatInsert alamat)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "checkAlamat.php"; // Replace with actual PHP API URL
                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(alamat.negeri), "negeri");
                formData.Add(new StringContent(alamat.poskod), "poskod");
                formData.Add(new StringContent(alamat.daerah), "daerah");
                formData.Add(new StringContent(alamat.bandar), "bandar");
                formData.Add(new StringContent(alamat.nama_jalan), "nama_jalan");

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<AddressResponse>(resultJson);
                return result;
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"Error inserting map pin: {ex.Message}");
                return null;
            }
        }
    }
    public class AlamatInsert
    {
        public string nama_jalan {  get; set; }
        public string bandar { get; set; }
        public string daerah { get; set; }
        public string poskod { get; set; }
        public string negeri { get; set; }
    }
    public class AddressResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public int negeri_id { get; set; }
        public int poskod_id { get; set; }
        public int daerah_id { get; set; }
        public int bandar_id { get; set; }
        public int jalan_id { get; set; }
    }
}
