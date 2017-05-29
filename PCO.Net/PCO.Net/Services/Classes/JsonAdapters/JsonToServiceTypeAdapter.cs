using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PCO.Net
{
    public static class JsonToServiceTypeAdapter
    {
        public static List<IServiceType> GetServiceTypes(string json)
        {
            var serviceTypes = new List<IServiceType>();

            try 
            {
                JObject data = JObject.Parse(json);

                JArray jsonServiceTypes = (JArray)data["data"];
                foreach (var jsonServiceType in jsonServiceTypes)
                {
                    var serviceType = new ServiceType
                    {
                        Id = (int)jsonServiceType["id"],
                        Name = (string)jsonServiceType["attributes"]["name"]
                    };

                    serviceTypes.Add(serviceType);
                }
            }
            catch (ArgumentNullException exception)
            {

            }
            catch (JsonReaderException exception)
            {
                
            }

            return serviceTypes;
        }
    }
}
