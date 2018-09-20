using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using pmacore_api.Models.datatake;

namespace pmacore_api.services
{
    public class ApiService
    {
        public static async Task<ResponseApi> GetList<T>(string urlBase, string servicePrefix, string controller)
        {
            try
            {
                var client = new HttpClient();
             
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ResponseApi
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<T>(result);
                return new ResponseApi
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new ResponseApi
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    
    }
}