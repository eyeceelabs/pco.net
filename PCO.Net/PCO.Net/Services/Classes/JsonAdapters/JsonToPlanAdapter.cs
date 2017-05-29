﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace PCO.Net
{
    public static class JsonToPlanAdapter
    {
        public static List<IPlan> GetPlans(string json)
        {
            var plans = new List<IPlan>();

            try 
            {
                JObject data = JObject.Parse(json);

                JArray jsonPlans = (JArray)data["data"];
                foreach (var jsonPlan in jsonPlans)
                {
                    var plan = new Plan
                    {
                        Id = Convert.ToInt32(jsonPlan["id"]),
                        Title = (string)jsonPlan["attributes"]["title"],
                        SeriesTitle = (string)jsonPlan["attributes"]["series_title"],
                        Time = DateTime.Parse((string)jsonPlan["attributes"]["sort_date"], CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal)
                    };

                    plans.Add(plan);
                }
            }
            catch (ArgumentNullException exception)
            {

            }
            catch (JsonReaderException exception)
            {
                
            }

            return plans;
        }
    }
}
