using RekodLampuJalan.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RekodLampuJalan.Utils
{
    public class LoadData
    {
        private readonly HttpClient _httpClient = new();

        public async Task<DropDownItem?> GetDropDownAsync()
        {
            try
            {
                string url = ConstantValue.ApiUrl+"selectAllReference.php";
                var response = await _httpClient.GetStringAsync(url);

                var result = JsonSerializer.Deserialize<DropDownItem>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching lookup data: {ex.Message}");
                return null;
            }
        }

        public async Task<MainData?> GetMainDataAsync()
        {
            try
            {
                string url = ConstantValue.ApiUrl + "selectAll.php";
                var response = await _httpClient.GetStringAsync(url);

                var result = JsonSerializer.Deserialize<MainData>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching lookup data: {ex.Message}");
                return null;
            }
        }
        public async Task<MainData?> GetMainByIDDataAsync(int id)
        {
            try
            {
                using var client = new HttpClient();
                string url = ConstantValue.ApiUrl + "selectById.php";
                using var formData = new MultipartFormDataContent();


                // Add form fields
                formData.Add(new StringContent(id.ToString()), "user_id");

                var response = await client.PostAsync(url, formData);
                var resultJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<MainData>(resultJson, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching lookup data: {ex.Message}");
                return null;
            }
        }

        public async Task<Alamat> GetAlamatAsync()
        {
            try
            {
                string url = ConstantValue.ApiUrl + "selectAllAlamat.php";
                var response = await _httpClient.GetStringAsync(url);

                var result = JsonSerializer.Deserialize<AlamatRoot>(response, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result.data;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error fetching lookup data: {ex.Message}");
                return null;
            }
        }

    }

}
