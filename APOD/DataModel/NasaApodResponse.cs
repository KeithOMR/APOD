using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APOD.DataModel
{
    [JsonObject(MemberSerialization.OptIn)]
    public class NasaApodResponse
    {
        [JsonProperty(PropertyName = "url")]
        public string ImagePath { get; set; }

        [JsonProperty(PropertyName = "media_type")]
        public string MediaType { get; set; }

        [JsonProperty(PropertyName = "explanation")]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}
