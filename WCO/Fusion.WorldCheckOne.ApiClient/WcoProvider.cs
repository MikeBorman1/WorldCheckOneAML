using System;
using System.Collections.Generic;

namespace Fusion.WorldCheckOne.ApiClient
{
    public class WcoProvider
    {
        public string Identifier { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Master { get; set; }

        public List<WcoProviderSource> Sources { get; set; } = new List<WcoProviderSource>();
    }

    public class WcoProviderSource
    {
        public string Identifier { get; set; }
        public string ImportIdentifier { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Provider { get; set; }
        public WcoProviderSourceType Type { get; set; }
        public string ProviderSourceStatus { get; set; }
        public string RegionOfAuthority { get; set; }
        public DateTime CreationDate { get; set; }
        public string SubscriptionCategory { get; set; }

        public class WcoProviderSourceType
        {
            public string Identifier { get; set; }
            public string Name { get; set; }
            public WcoProviderSourceTypeCategory Category { get; set; }
            public List<WcoProviderSource> Sources { get; set; } = new List<WcoProviderSource>();
        }

        public class WcoProviderSourceTypeCategory
        {
            public string Identifier { get; set; }
            //public List<WcoProviderSourceType> ProviderSourceTypes { get; set; } = new 
            public string Name { get; set; }
            public string Description { get; set; }

        }
    }
}

