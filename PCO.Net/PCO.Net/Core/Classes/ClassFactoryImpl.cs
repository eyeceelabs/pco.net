using System;
namespace PCO.Net
{
    public class ClassFactoryImpl : IClassFactory
    {
        public ITransport CreateTransport()
        {
            return new HttpTransport();
        }
    }
}
