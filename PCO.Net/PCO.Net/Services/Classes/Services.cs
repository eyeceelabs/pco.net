﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCO.Net
{
    public class Services
    {
        private ITransport transport;

        public Services()
        {
            transport = ClassFactory.Instance.CreateTransport();
        }

        public async Task<List<IPlan>> GetPlansAsync(string serviceTypeName, int numberOfPlans)
        {
            var queryString = ServiceTypeQueryBuilder.CreateListAllServiceTypesQuery();
            var jsonResult = await transport.GetJsonResult(queryString);
            List<IServiceType> serviceTypes = JsonToServiceTypeAdapter.GetServiceTypes(jsonResult);

            var serviceType = serviceTypes.Where(type => type.Name == serviceTypeName).FirstOrDefault();
            if (serviceType != null)
            {
                queryString = PlanQueryBuilder.CreateFuturePlansQuery(serviceType.Id, numberOfPlans);
                jsonResult = await transport.GetJsonResult(queryString);
                return JsonToPlanAdapter.GetPlans(jsonResult);
            }
            else
            {
                return new List<IPlan>();
            }
        }
    }
}
