using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PodioAPI.Models;

namespace PodioAPI.Utils.ItemFields
{
    public class LocationItemField : ItemField
    {
        public IEnumerable<string> Locations
        {
            get
            {
                if (this.Values != null && this.Values.Any())
                    return new List<string>(this.Values.Select(s => (string) s["value"]));
                else
                    return new List<String>();
            }

            set
            {
                EnsureValuesInitialized();
                foreach (var location in value)
                {
                    var jobject = new JObject();
                    jobject["value"] = location;
                    this.Values.Add(jobject);
                }
            }
        }

        public string Location
        {
            set
            {
                EnsureValuesInitialized();

                var jobject = new JObject();
                jobject["value"] = value;
                this.Values.Add(jobject);
            }
        }

        public double? Latitude
        {
            get
            {
                if (this.Values.Any())
                {
                    return (double?)this.Values.First["lat"];
                }

                return null;
            }
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["lat"] = value;
            }        
        }

        public double? Longitude
        {
            get
            {
                if (this.Values.Any())
                {
                    return (double?)this.Values.First["lng"];
                }

                return null;
            }
            set
            {
                EnsureValuesInitialized(true);
                this.Values.First()["lng"] = value;
            }
        }
        public string StreetAddress
        {
            get
            {
                if (this.Values.Any())
                {
                    return (string)this.Values.First["street_address"];
                }

                return null;
            }
        }

        public string City
        {
            get
            {
                if (this.Values.Any())
                {
                    return (string)this.Values.First["city"];
                }

                return null;
            }
        }

        public string PostalCode
        {
            get
            {
                if (this.Values.Any())
                {
                    return (string)this.Values.First["postal_code"];
                }

                return null;
            }
        }

        public string Country
        {
            get
            {
                if (this.Values.Any())
                {
                    return (string)this.Values.First["country"];
                }

                return null;
            }
        }

        public string State
        {
            get
            {
                if (this.Values.Any())
                {
                    return (string)this.Values.First["state"];
                }

                return null;
            }
        }     
    }
}
