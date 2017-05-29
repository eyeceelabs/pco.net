using System;
namespace PCO.Net
{
    public class ClassFactory : IClassFactory
    {
        public static IClassFactory Instance { get; set; }

        public ITransport CreateTransport()
        {
            return Instance.CreateTransport();
        }
    }
}
