using System.Collections.Generic;

namespace Fusion.WorldCheckOne.ApiClient
{
    public class WcoGroup
    {
        /*  {
              "id": "0a3687cf-5d37-1448-9754-212f00000366",
              "name": "Fusion Systems Limited (Trial)",
              "parentId": null,
              "hasChildren": true,
              "status": "ACTIVE",
              "children": [
              {
                  "id": "0a3687d0-5d37-1694-9754-215600000395",
                  "name": "Fusion Systems Limited (Trial) - Screening",
                  "parentId": "0a3687cf-5d37-1448-9754-212f00000366",
                  "hasChildren": false,
                  "status": "ACTIVE",
                  "children": []
              }
              ]
          }*/

        public string Id { get; set; }

        public string Name { get; set; }

        public string ParentId { get; set; }

        public bool HasChildren { get; set; }

        public string Status { get; set; }

        public List<WcoGroup> Children { get; set; } = new List<WcoGroup>();
    }
}