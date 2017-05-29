using System;
using System.Threading.Tasks;
using PCLMock;
using PCO.Net;
namespace UnitTests
{
    public class TransportMock : MockBase<ITransport>, ITransport
    {
        public TransportMock(MockBehavior behavior = MockBehavior.Strict)
                : base(behavior)
        { }

        public Task<string> GetJsonResult(string url) => this.Apply(x => x.GetJsonResult(url));
    }
}
