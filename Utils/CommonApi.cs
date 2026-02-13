using Newtonsoft.Json;
using RekodLampuJalan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace RekodLampuJalan.Utils
{
    public class CommonApi
    {
        private readonly HttpClient _httpClient = new HttpClient();
        public async Task<Location?> GetLocationFromGoogle(string query)
        {
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={query}&key={ConstantValue.GoogleApiKey}";
            var response = await _httpClient.GetStringAsync(url);

            using JsonDocument doc = JsonDocument.Parse(response);
            var results = doc.RootElement.GetProperty("results");

            if (results.GetArrayLength() > 0)
            {
                var location = results[0].GetProperty("geometry").GetProperty("location");
                double lat = location.GetProperty("lat").GetDouble();
                double lng = location.GetProperty("lng").GetDouble();
                return new Location(lat, lng);
            }
            return null;
        }
        public async Task<DataResponse> GetDataAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    // Make the GET request to the PHP API
                    var response = await client.GetStringAsync(ConstantValue.ApiUrl + "selectAll.php");

                    // Deserialize the JSON response into the DataResponse object
                    var data = JsonConvert.DeserializeObject<DataResponse>(response);
                    return data;
                }
                catch (Exception ex)
                {
                    // Handle error
                    Console.WriteLine($"Error: {ex.Message}");
                    return null;
                }
            }
        }
        //public async Task<DataResponse> GetRefeshDataAsync()
        //{
        //    using (var client = new HttpClient())
        //    {
        //        try
        //        {
        //            // Make the GET request to the PHP API
        //            var response = await client.GetStringAsync(ConstantValue.ApiUrl + "refresh.php");

        //            // Deserialize the JSON response into the DataResponse object
        //            var data = JsonConvert.DeserializeObject<DataResponse>(response);
        //            return data;
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle error
        //            Console.WriteLine($"Error: {ex.Message}");
        //            return null;
        //        }
        //    }
        //}
        public async Task<string> InsertMapPin(string jenis, string jenis_lampu, string tinggi_tiang, string material_tiang, string pintu_penyelenggaraan, string panjang_lengan_lampu,string jenis_tiang, double lattitude, double longitude, string? imagePath)
        {
            using (var client = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add the form fields
                formData.Add(new StringContent(jenis), "jenis");
                formData.Add(new StringContent(jenis_lampu), "jenis_lampu");
                formData.Add(new StringContent(tinggi_tiang), "tinggi_tiang");
                formData.Add(new StringContent(material_tiang), "material_tiang");
                formData.Add(new StringContent(pintu_penyelenggaraan), "pintu_penyelenggaraan");
                formData.Add(new StringContent(panjang_lengan_lampu), "panjang_lengan_lampu");
                formData.Add(new StringContent(jenis_tiang), "jenis_tiang");
                formData.Add(new StringContent(lattitude.ToString()), "lattitude");
                formData.Add(new StringContent(longitude.ToString()), "longitude");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                // Optional image attachment
                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    var byteArray = await File.ReadAllBytesAsync(imagePath);
                    string filename = Path.GetFileName(imagePath);
                    string ext = Path.GetExtension(filename)?.TrimStart('.') ?? "jpeg";

                    var fileContent = new ByteArrayContent(byteArray);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + ext);
                    formData.Add(fileContent, "attachment", DateTime.Now.ToString("yyyyMMddHHmmss") + filename);
                }

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "insertMapPin.php", formData);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        return responseData;
                    }
                    else
                    {
                        return $"Error: {response.StatusCode}";
                    }
                }
                catch (Exception ex)
                {
                    return $"Exception: {ex.Message}";
                }
            }
        }

        public async Task<string> UpdateMapPin(int id, string jenis, string jenisLampu, string tinggi_tiang, string jenis_tiang,
                string material_tiang, string pintu_penyelenggaraan,
                string panjang_lengan_lampu, string? imagePath)
        {
            using (var client = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add text fields
                formData.Add(new StringContent(id.ToString()), "id");
                formData.Add(new StringContent(jenis), "jenis");
                formData.Add(new StringContent(jenisLampu), "jenis_lampu");
                formData.Add(new StringContent(tinggi_tiang), "tinggi_tiang");
                formData.Add(new StringContent(jenis_tiang), "jenis_tiang");
                formData.Add(new StringContent(material_tiang), "material_tiang");
                formData.Add(new StringContent(pintu_penyelenggaraan), "pintu_penyelenggaraan");
                formData.Add(new StringContent(panjang_lengan_lampu), "panjang_lengan_lampu");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                // Conditionally add image file if provided
                if (!string.IsNullOrWhiteSpace(imagePath) && File.Exists(imagePath))
                {
                    var byteArray = await File.ReadAllBytesAsync(imagePath);
                    string filename = Path.GetFileName(imagePath);
                    string ext = Path.GetExtension(filename).TrimStart('.');

                    var fileContent = new ByteArrayContent(byteArray);
                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/" + ext);
                    formData.Add(fileContent, "attachment", DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + filename);
                }

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "updateMapPin.php", formData);

                    // Ensure we got a successful response
                    response.EnsureSuccessStatusCode();

                    // Get the response content
                    var responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                catch (Exception ex)
                {
                    return $"Error: {ex.Message}";
                }
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
        public async Task<string> InsertFlyCable(double startLattitude, double startLongitude, double endLattitude, double endLongitude)
        {
            using (var client = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add form data
                formData.Add(new StringContent(startLattitude.ToString()), "start_lattitude");
                formData.Add(new StringContent(startLongitude.ToString()), "start_longitude");
                formData.Add(new StringContent(endLattitude.ToString()), "end_lattitude");
                formData.Add(new StringContent(endLongitude.ToString()), "end_longitude");
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
                try
                {
                    // Send the request
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "insertFlyCable.php", formData);

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
                formData.Add(new StringContent(App.userName ?? ""), "user_name");
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
        public async Task<string> LoginAsync(string username, string password)
        {
            using (var client = new HttpClient())
            {
                var parameters = new Dictionary<string, string>
            {
                { "username", username },
                { "password", password },

            };

                var content = new FormUrlEncodedContent(parameters);

                try
                {
                    // Send the POST request to the PHP API
                    var response = await client.PostAsync(ConstantValue.ApiUrl + "login.php", content);

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

        
    }
    public class DataResponse
    {
        public List<MapPin> map_pin { get; set; }
        public List<FlyCable> fly_cable { get; set; }
    }
}

