using NUnit.Framework;
using PCO.Net;
using PCLMock;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class Services_Tests
    {
        static string singlePlanJson = "{\"links\":{\"self\":\"https://api.planningcenteronline.com/services/v2/service_types/624905/plans?filter=future\\u0026per_page=1\",\"next\":\"https://api.planningcenteronline.com/services/v2/service_types/624905/plans?filter=future\\u0026offset=1\\u0026per_page=1\"},\"data\":[{\"type\":\"Plan\",\"id\":\"30299621\",\"attributes\":{\"created_at\":\"2017-03-01T13:01:26Z\",\"dates\":\"4 June 2017\",\"files_expire_at\":\"2017-06-19T12:00:00Z\",\"items_count\":15,\"last_time_at\":\"2017-06-04T10:30:00Z\",\"multi_day\":false,\"needed_positions_count\":12,\"other_time_count\":0,\"permissions\":\"Administrator\",\"plan_notes_count\":0,\"plan_people_count\":19,\"public\":false,\"rehearsal_time_count\":0,\"series_title\":\"Acts - Living as Resurrection People\",\"service_time_count\":1,\"short_dates\":\"4 Jun 2017\",\"sort_date\":\"2017-06-04T10:30:00Z\",\"title\":\"Pentecost\",\"total_length\":0,\"updated_at\":\"2017-05-28T11:16:06Z\"},\"links\":{\"self\":\"https://api.planningcenteronline.com/services/v2/service_types/624905/plans/30299621\"},\"relationships\":{\"service_type\":{\"data\":{\"type\":\"ServiceType\",\"id\":\"624905\"}},\"next_plan\":{\"data\":{\"type\":\"Plan\",\"id\":\"30299622\"}},\"previous_plan\":{\"data\":{\"type\":\"Plan\",\"id\":\"30299619\"}},\"attachment_types\":{\"data\":[]},\"series\":{\"data\":null},\"created_by\":{\"data\":{\"type\":\"Person\",\"id\":\"12700654\"}},\"updated_by\":{\"data\":{\"type\":\"Person\",\"id\":\"20000858\"}}}}],\"included\":[],\"meta\":{\"total_count\":30,\"count\":1,\"next\":{\"offset\":1},\"can_order_by\":[\"title\",\"created_at\",\"updated_at\",\"sort_date\"],\"can_query_by\":[\"title\"],\"can_include\":[\"series\"],\"can_filter\":[\"future\",\"past\",\"no_dates\"],\"parent\":{\"id\":\"624905\",\"type\":\"ServiceType\"}}}";
        static string singleServiceTypeJson = "{\"links\":{\"self\":\"https://api.planningcenteronline.com/services/v2/service_types\"},\"data\":[{\"type\":\"ServiceType\",\"id\":\"624905\",\"attributes\":{\"attachment_types_enabled\":false,\"created_at\":\"2016-08-01T13:48:11Z\",\"frequency\":\"Weekly\",\"name\":\"Central Gathering\",\"permissions\":\"Administrator\",\"sequence\":0,\"updated_at\":\"2017-04-20T09:57:27Z\"},\"links\":{\"self\":\"https://api.planningcenteronline.com/services/v2/service_types/624905\"},\"relationships\":{\"parent\":{\"data\":null}}}],\"included\":[],\"meta\":{\"total_count\":1,\"count\":1,\"can_include\":[\"time_preference_options\"],\"parent\":{\"id\":\"182972\",\"type\":\"Organization\"}}}";

        [Test]
        [Ignore]
        public async void GetPlansAync_Live_Test_Not_A_UT()
        {
            ClassFactory.Instance = new ClassFactoryImpl();

            var services = new Services();
            var plans = await services.GetPlansAsync("Central Gathering", 4);
            Assert.True(plans.Count == 4);
        }

        [Test]
        public async void GetPlansAsync_Returns_Empty_List_If_Json_Null()
        {
            var mockTransport = new TransportMock();
            mockTransport.When(x => x.GetJsonResult(It.IsAny<string>()))
                         .Return(() =>
                            {
                                var tcs = new TaskCompletionSource<string>();
                                tcs.SetResult(null);
                                return tcs.Task;
                            });

            var mockClassFactory = new ClassFactoryMock();
            mockClassFactory.When(x => x.CreateTransport())
                            .Return(mockTransport);

            ClassFactory.Instance = mockClassFactory;

            var services = new Services();
            var plans = await services.GetPlansAsync("Central Gathering", 4);
            Assert.True(plans.Count == 0);
        }

        [Test]
        public async void GetPlansAsync_Returns_Single_Plan()
        {
            int count = 0;
            var mockTransport = new TransportMock();
            mockTransport.When(x => x.GetJsonResult(It.IsAny<string>()))
                         .Return(() =>
                            {
                                var tcs = new TaskCompletionSource<string>();
                                tcs.SetResult(count == 0 ? singleServiceTypeJson : singlePlanJson);
                                count++;
                                return tcs.Task;
                            });

            var mockClassFactory = new ClassFactoryMock();
            mockClassFactory.When(x => x.CreateTransport())
                            .Return(mockTransport);

            ClassFactory.Instance = mockClassFactory;

            var services = new Services();
            var plans = await services.GetPlansAsync("Central Gathering", 4);
            Assert.True(plans.Count == 1);
        }

        [Test]
        public void Plans_Parse_JSON_For_Single_Plan()
        {
            var plans = JsonToPlanAdapter.GetPlans(singlePlanJson);
            Assert.True(plans[0].Id == 30299621);
            Assert.True(plans[0].Title == "Pentecost");
            Assert.True(plans.Count == 1);
        }

        [Test]
        public void Plans_Parse_JSON_For_Single_ServiceType()
        {
            var serviceTypes = JsonToServiceTypeAdapter.GetServiceTypes(singleServiceTypeJson);
            Assert.True(serviceTypes[0].Id == 624905);
            Assert.True(serviceTypes[0].Name == "Central Gathering");
            Assert.True(serviceTypes.Count == 1);
        }
    }
}
