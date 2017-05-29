using Flurl;

namespace PCO.Net
{
    public static class PlanQueryBuilder
    {
        public static string CreateFuturePlansQuery(int serviceTypeId, int number)
        {
            var url = HttpTransport.BaseUri
                                    .AppendPathSegments(new[] { "service_types", serviceTypeId.ToString(), "plans" })
                                    .SetQueryParams(new
                                    {
                                        filter = "future",
                                        per_page = number
                                    });

            return url.ToString();
        }
    }
}
