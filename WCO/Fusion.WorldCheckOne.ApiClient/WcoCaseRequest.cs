using System.Collections.Generic;
using Newtonsoft.Json;

namespace Fusion.WorldCheckOne.ApiClient
{
    public class WcoCaseRequest
    {
        /*
         
   "groupId":"{{group-id}}",
   "entityType":"INDIVIDUAL",
   "providerTypes":[  
      "WATCHLIST"
   ],
   "name":"Putin",
   "customFields":[
      {  
         "typeId":"{{custom-field-1}}",
         "value":"custom field 1 sample value"
      },
      {  
         "typeId":"{{custom-field-2}}",
         "value":"custom field 2 sample value"
      },
      {  
         "typeId":"{{custom-field-3}}",
         "value":"mandatory custom field sample value"
      }
   ],
   "secondaryFields":[  
      {  
         "typeId":"SFCT_1",
         "value":"MALE"
      },
      {  
         "typeId":"SFCT_2",
         "dateTimeValue": "2016-10-12"
      },
      {  
         "typeId":"SFCT_3",
         "value":"USA"
      },
      {  
         "typeId":"SFCT_4",
         "value":"AUS"
      },
         "typeId":"SFCT_5",
         "value":"ABW"
      {  
      }
   ]
}
         * */

        [JsonProperty("groupId")]
        public string GroupId { get; set; }

        [JsonProperty("entityType")]
        public string EntityType { get; set; }

        [JsonProperty("providerTypes")]
        public string[] ProviderTypes { get; set; } = { "WATCHLIST" };

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("primaryFields")]
        public List<WcoFieldDef> PrimaryFields { get; set; } = new List<WcoFieldDef>();

        [JsonProperty("secondaryFields")]
        public List<WcoFieldDef> SecondaryFields { get; set; } = new List<WcoFieldDef>();
        public WcoCaseRequest(string groupId, string entityType, string name, string gender = "", string dateOfBirth = "", string country = "")
        {
            GroupId = groupId;
            EntityType = entityType;
            Name = name;
            //
            if (!string.IsNullOrEmpty(gender))
            {
                SecondaryFields.Add(new WcoFieldDef("SFCT_1", gender));
            }
            //
            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                SecondaryFields.Add(new WcoFieldDef("SFCT_2", null, dateOfBirth));
            }
            //
            if (!string.IsNullOrEmpty(country))
            {
                SecondaryFields.Add(new WcoFieldDef("SFCT_3", country));
            }
        }
    }
}