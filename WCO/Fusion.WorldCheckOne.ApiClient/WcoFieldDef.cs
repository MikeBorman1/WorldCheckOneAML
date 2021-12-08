using Newtonsoft.Json;

namespace Fusion.WorldCheckOne.ApiClient
{
    public class WcoFieldDef
    {
        [JsonProperty("typeId")]
        public string TypeId { get; set; }

        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }

        [JsonProperty("dateTimeValue", NullValueHandling = NullValueHandling.Ignore)]
        public string DateTimeValue { get; set; }

        public WcoFieldDef(string typeId, string value, string dateTimeValue = null)
        {
            TypeId = typeId;
            Value = value;
            DateTimeValue = dateTimeValue;
        }
    }
}