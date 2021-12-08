using System;
using System.Collections.Generic;
using System.Linq;


namespace Fusion.WorldCheckOne.ApiClient
{
    // This creates a cast for which the Json files are mapped to
    public class WcoProfile
    {
        public bool isSanctioned;
        public bool isIndividual;
        public bool isOrganisation;
        private List<string> countryInfluence = new List<string>();

        public List<string> CountryInfluence
        {
            get
            {
                countryInfluence.Add(CountryLinks.FirstOrDefault()?.CountryText);
                return countryInfluence;
            }
        } 
        private int RiskAss(float AsNumT, float LinkNumT)
        {

            if (AsNum == 0){AsNumT = 1;}
            if (LinkNumT == 0){LinkNumT = 1;}
            var risk = (AsNumT * LinkNumT)/5;
            if (risk > 20) { risk = 20; }
            risk *= 5;
            return (int)Math.Round(risk, 2);
        }
        
        public string riskComp => "Associate%:" + Math.Round((AsNum / (AsNum + LinkNum) ) * 100.0, 2)  + "  Link%:" +
                                  Math.Round((LinkNum / (AsNum + LinkNum) ) * 100, 2);
        public int Risk => RiskAss(AsNum, LinkNum);
        public string EntityType { get; set; }
        public DateTime CreationDate { get; set; }

        public bool Active { get; set; }
        public float AsNum => Associates.Count;
        public float LinkNum => WebLinks.Count;
        public List<WcoAddress> Addresses { get; set; }

        public List<WcoName> Names { get; set; }

        public string Name => Names.FirstOrDefault()?.FullName;
        public string Gender { get; set; }
        public string entityId { get; set; }
        public int age
        {
            get
            {
                var dateString = Events?.FirstOrDefault()?.fullDate;
                if (dateString == null)
                {
                    return 0;
                }
                //
                if (dateString.EndsWith("-00-00"))
                {
                    dateString = dateString.Replace("-00-00", "-01-01");
                }
                DateTime date;
                if (DateTime.TryParse(dateString, out date))
                {
                    return DateTime.Now.Year - date.Year; ;
                }
                else
                {
                    return 0;
                }
            }
        }

        public List<WcoCountryLinks> CountryLinks { get; set; }

        public List<WcoReports> Details { get; set; }

        public string Reports
        {
            get { return Details.FirstOrDefault(r => r.DetailType == "REPORTS")?.Text; }
        }

        public List<WcoWebLinks> WebLinks { get; set; }
        


        public List<WcoProviderSource> Sources { get; set; }

        
        public List<WcoAssociates> Associates { get; set; }
        public List<WcoEvents> Events { get; set; }
        public List<WcoRoles> Roles { get; set; }
        private static int AgeCalc(DateTime date)
        {
            int age = 0;
            if (date > DateTime.MinValue)
            {
                age = DateTime.Now.Year - date.Year;
            }
            return age;

        }
    }

    public class WcoRoles
    {
        public string Location { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
    }
    public class WcoAssociates
    {
        public string Reversed { get; set; }
        public string TargetEntityId { get; set; }
        public string TargetExternalImportId { get; set; }
        public string Type { get; set; }

        public WcoProfile Profile { get; set; }
        

        public void GetExtra(WorldCheckOneApiClient client)
        {
            Profile = client.GetProfileFromCache(TargetEntityId);
        }
    }

   
    public class WcoEvents
    {
        public WcoAddress Address { get; set; }
        public string fullDate { get; set; }
        public string Type { get; set; }
        public int Year { get; set; }
    }
    public class WcoWebLinks
    {
        public string Caption { get; set; }
        public string Uri { get; set; }
    }
   
    }
    public class WcoReports
    {
        public string DetailType { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }
    }
    public class WcoCountryLinks
    {
        public string Type { get; set; }
        public string CountryText { get; set; }

    }
    public class WcoAddress
    {
        public string City { get; set; }
        public WcoCountry Country { get; set; }
        public string PostCode { get; set; }
        public string Region { get; set; }
        public string Street { get; set; }
    }

    public class WcoName
    {
        public string FullName { get; set; }
    }

    public class WcoCountry
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    