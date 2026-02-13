using RekodLampuJalan.Models;
using RekodLampuJalan.PopUp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RekodLampuJalan.Utils
{
    public class InsertNewData
    {
        public async Task<MapPin> InsertMapPinAsync(MapPin model)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "insertMapPin.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(model.jenis_item.ToString()), "jenis_item");
                formData.Add(new StringContent(model.jenis_lampu.ToString() ?? ""), "jenis_lampu");
                formData.Add(new StringContent(model.lattitude.ToString(CultureInfo.InvariantCulture)), "lattitude");
                formData.Add(new StringContent(model.longitude.ToString(CultureInfo.InvariantCulture)), "longitude");
                formData.Add(new StringContent(model.tinggi_tiang.ToString()), "tinggi_tiang");
                formData.Add(new StringContent(model.material_tiang.ToString()), "material_tiang");
                formData.Add(new StringContent(model.jenis_pintu_penyelenggaraan.ToString()), "jenis_pintu_penyelenggaraan");
                formData.Add(new StringContent(model.saiz_pintu_penyelenggaraan.ToString() ?? ""), "saiz_pintu_penyelenggaraan");
                formData.Add(new StringContent(model.panjang_lengan_lampu.ToString()), "panjang_lengan_lampu");
                formData.Add(new StringContent(model.jenis_tiang.ToString() ?? ""), "jenis_tiang");
                formData.Add(new StringContent(model.jenama_lampu.ToString()), "jenama_lampu");
                formData.Add(new StringContent(model.jenama_tiang.ToString()), "jenama_tiang");
                formData.Add(new StringContent(model.status.ToString()), "status");
                formData.Add(new StringContent(model.alamat.ToString()), "alamat");
                formData.Add(new StringContent(model.komen.ToString() ?? ""), "komen");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                formData.Add(new StringContent(App.userId.ToString() ?? ""), "user_id");
                // Optional file uploads
                await AddFileIfExists(formData, model.attachmentFile);

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<MapPinInsertResponse>(resultJson);
                if(result.success)
                {
                    result.data.alamatFull= model.alamatFull;
                    return result.data;
                }
                return new MapPin();
                 
            }
            catch (Exception ex)
            {
               // Debug.WriteLine($"Error inserting map pin: {ex.Message}");
                return new MapPin();
            }
        }
        public async Task<FeederPillar> InsertFeederPillarAsync(FeederPillar model)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "insertFeederPillar.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(model.no_meter.ToString()), "no_meter");
                formData.Add(new StringContent(model.nilai_amp.ToString() ?? ""), "nilai_amp");
                formData.Add(new StringContent(model.lattitude.ToString(CultureInfo.InvariantCulture)), "lattitude");
                formData.Add(new StringContent(model.longitude.ToString(CultureInfo.InvariantCulture)), "longitude");
                formData.Add(new StringContent(model.nilai_phase_r.ToString()), "nilai_phase_r");
                formData.Add(new StringContent(model.nilai_phase_y.ToString()), "nilai_phase_y");
                formData.Add(new StringContent(model.nilai_phase_b.ToString()), "nilai_phase_b");
                formData.Add(new StringContent(model.status_pintu.ToString()??""), "status_pintu");
                formData.Add(new StringContent(model.footing.ToString() ?? ""), "footing");
                formData.Add(new StringContent(model.komen.ToString() ?? ""), "komen");
                formData.Add(new StringContent(model.alamat.ToString() ?? ""), "alamat");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                formData.Add(new StringContent(App.userId.ToString() ?? ""), "user_id");
                // Optional file uploads
                await AddFileIfExists(formData, model.attachmentFile);

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<FeederPillarInsertResponse>(resultJson);
                if (result.success)
                {
                    result.data.alamatFull = model.alamatFull;
                    return result.data;
                }
                return new FeederPillar();
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"Error inserting map pin: {ex.Message}");
                return new FeederPillar();
            }
        }
        public async Task<int> InsertFlyCableAsync(FlyCable model)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "insertFlyCable.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(model.start_lattitude.ToString(CultureInfo.InvariantCulture)), "start_lattitude");
                formData.Add(new StringContent(model.start_longitude.ToString(CultureInfo.InvariantCulture)), "start_longitude");
                formData.Add(new StringContent(model.end_lattitude.ToString(CultureInfo.InvariantCulture)), "end_lattitude");
                formData.Add(new StringContent(model.end_longitude.ToString(CultureInfo.InvariantCulture)), "end_longitude");
                formData.Add(new StringContent(model.jarak.ToString()), "jarak");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                formData.Add(new StringContent(App.userId.ToString() ?? ""), "user_id");
                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<FlyCableInsertResponse>(resultJson);
                return result?.success == true ? result.id : -1;
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"Error inserting map pin: {ex.Message}");
                return -1;
            }
        }
        public async Task<ReferenceInsertResponse> InsertReferenceAsync(string table,string item)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "insertReference.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(table), "table");
                formData.Add(new StringContent(item), "item");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                formData.Add(new StringContent(App.userId.ToString() ?? ""), "user_id");
                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<ReferenceInsertResponse>(resultJson);
                return result;
            }
            catch (Exception ex)
            {
                // Debug.WriteLine($"Error inserting map pin: {ex.Message}");
                return new ReferenceInsertResponse();
            }
        }

        private async Task AddFileIfExists(MultipartFormDataContent formData, List<AttachmentImage> files)
        {
            if (files == null) return;

            foreach (var kv in files)
            {
                var fieldName = kv.fileName;
                var path = kv.filePath;
          
               if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    var fileBytes = await File.ReadAllBytesAsync(path);
                    var content = new ByteArrayContent(fileBytes);

                    var contentType = GetMimeType(path);
                    content.Headers.ContentType = new MediaTypeHeaderValue(contentType);

                    formData.Add(content, fieldName, Path.GetFileName(path));
                }
            }
        }
        private string GetMimeType(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                _ => "application/octet-stream",
            };
        }


    }
    public class MapPinInsertResponse
    {
        public bool success { get; set; }
        public MapPin data { get; set; }
    }
    public class FeederPillarInsertResponse
    {
        public bool success { get; set; }
        public FeederPillar data { get; set; }
    }
    public class FlyCableInsertResponse
    {
        public bool success { get; set; }
        public int id { get; set; }
    }
    public class ReferenceInsertResponse
    {
        public string message { get; set; }
        public int id { get; set; }
    }
}
