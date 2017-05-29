using Flurl;

namespace PCO.Net
{
    public static class ServiceTypeQueryBuilder
    {
        public static string CreateListAllServiceTypesQuery()
        {
            var url = HttpTransport.BaseUri
                                   .AppendPathSegment("service_types");

            return url.ToString();
        }
    }
}
