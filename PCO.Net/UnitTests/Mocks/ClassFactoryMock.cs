using System;
using PCLMock;

using PCO.Net;

namespace UnitTests
{
    public class ClassFactoryMock : MockBase<IClassFactory>, IClassFactory
    {
        public ClassFactoryMock(MockBehavior behavior = MockBehavior.Strict)
                : base(behavior)
        { }

        public ITransport CreateTransport() => this.Apply(x => x.CreateTransport());
    }
}
