using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace pmacore_api.Controllers.Pdfreport
{
    public static class Traductor
    {
        public static async Task<string> TranslateThisAsync(string valor)
        {
            string translatedText = "";
            var url = "https://translate.googleapis.com/translate_a/single?client=gtx&sl="
            + "en" + "&tl=" + "es" + "&dt=t&q=" + valor;

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(url))
            using (HttpContent content = response.Content)
            {
                // ... Read the response as a string.
                var tr = content.ReadAsStringAsync().Result;
                // ... turn to an Jarray to be easier to select
                JArray ja = JsonConvert.DeserializeObject<JArray>(tr);
                // ... read the data we want
                translatedText = ja[0][0][0].ToString();
            }
            return translatedText;
        }
    }
}