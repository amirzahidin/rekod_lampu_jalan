using RekodLampuJalan.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RekodLampuJalan.Utils
{
    public class UpdateData
    {
        public async Task<MapPin> UpdateMapPinAsync(MapPin model)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "updateMapPin.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(model.id.ToString()), "id");
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
                // Optional file uploads
                await AddFileIfExists(formData, model.attachmentFile);

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<MapPinInsertResponse>(resultJson);
                if (result.success)
                {
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
        public async Task<string> UpdateMapPinLocationAsync(int id, double lattitude, double longitude)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                { "lattitude", lattitude.ToString() },
                { "longitude", longitude.ToString() },
                    {"user_name",App.userName }
            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "updateMapPinLocation.php", content);

                    // If the request is successful, read the response content
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // This could be a success message or error message
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    return $"Exception: {ex.Message}";
                }
            }
        }
        public async Task<string> DeleteMapPinAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                 {"user_name",App.userName }
            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "deleteMapPin.php", content);

                    // If the request is successful, read the response content
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // Return the response data (success or error message)
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    return $"Exception: {ex.Message}";
                }
            }
        }
        public async Task<FeederPillar> UpdateFeederPillarAsync(FeederPillar model)
        {
            try
            {
                using var client = new HttpClient();
                var url = ConstantValue.ApiUrl + "updateFeederPillar.php"; // Replace with actual PHP API URL

                using var formData = new MultipartFormDataContent();

                // Add form fields
                formData.Add(new StringContent(model.id.ToString()), "id");
                formData.Add(new StringContent(model.no_meter.ToString()), "no_meter");
                formData.Add(new StringContent(model.nilai_amp.ToString() ?? ""), "nilai_amp");
                formData.Add(new StringContent(model.lattitude.ToString(CultureInfo.InvariantCulture)), "lattitude");
                formData.Add(new StringContent(model.longitude.ToString(CultureInfo.InvariantCulture)), "longitude");
                formData.Add(new StringContent(model.nilai_phase_r.ToString()), "nilai_phase_r");
                formData.Add(new StringContent(model.nilai_phase_y.ToString()), "nilai_phase_y");
                formData.Add(new StringContent(model.nilai_phase_b.ToString()), "nilai_phase_b");
                formData.Add(new StringContent(model.status_pintu.ToString() ?? ""), "status_pintu");
                formData.Add(new StringContent(model.footing.ToString() ?? ""), "footing");
                formData.Add(new StringContent(model.komen.ToString() ?? ""), "komen");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                // Optional file uploads
                await AddFileIfExists(formData, model.attachmentFile);

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();

                // Parse response to get new ID
                var result = JsonSerializer.Deserialize<FeederPillarInsertResponse>(resultJson);
                if (result.success)
                {
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
        public async Task<string> UpdateFeederPillarLocationAsync(int id, double lattitude, double longitude)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                { "lattitude", lattitude.ToString() },
                { "longitude", longitude.ToString() },
                 {"user_name",App.userName }
            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "updateFeederPillarLocation.php", content);

                    // If the request is successful, read the response content
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // This could be a success message or error message
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    return $"Exception: {ex.Message}";
                }
            }
        }
        public async Task<string> DeleteFeederPillarAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                 {"user_name",App.userName }
            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "deleteFeederPillar.php", content);

                    // If the request is successful, read the response content
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // Return the response data (success or error message)
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    return $"Exception: {ex.Message}";
                }
            }
        }
        public async Task<string> UpdateFlyCable(int id, double startLattitude, double startLongitude, double endLattitude, double endLongitude)
        {
            using (var client = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add form data
                formData.Add(new StringContent(id.ToString()), "id");
                formData.Add(new StringContent(startLattitude.ToString()), "start_lattitude");
                formData.Add(new StringContent(startLongitude.ToString()), "start_longitude");
                formData.Add(new StringContent(endLattitude.ToString()), "end_lattitude");
                formData.Add(new StringContent(endLongitude.ToString()), "end_longitude");

                try
                {
                    // Send the request
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "updateFlyCable.php", formData);

                    // Ensure we got a successful response
                    response.EnsureSuccessStatusCode();

                    // Read the response content
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent; // Return response content (JSON)
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
            }
        }
        public async Task<string> DeleteFlyCableAsync(int id)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "id", id.ToString() },
                 {"user_name",App.userName }
            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "deleteFlyCable.php", content);

                    // If the request is successful, read the response content
                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;  // Return the response data (success or error message)
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle error
                    return $"Exception: {ex.Message}";
                }
            }
        }
        private async Task AddFileIfExists(MultipartFormDataContent formData, List<AttachmentImage> files)
        {
            if (files == null) return;

            foreach (var kv in files)
            {
                if(kv.newItem)
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
}
