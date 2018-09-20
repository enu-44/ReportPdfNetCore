using System.Collections.Generic;
using Newtonsoft.Json;

namespace pmacore_api.Models.datatake
{
    public class ResponseElementoCables
    {
        [JsonProperty("@odata.count")]
        public int Count { get; set; }

        [JsonProperty("value")]
        public List<ViewElementoCables> List { get; set; }
    }
}