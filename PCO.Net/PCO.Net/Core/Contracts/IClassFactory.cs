using System;
namespace PCO.Net
{
    public interface IClassFactory
    {
        ITransport CreateTransport();
    }
}
