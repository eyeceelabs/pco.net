﻿using System;
using System.Threading.Tasks;

namespace PCO.Net
{
    public interface ITransport
    {
        Task<string> GetJsonResult(string url);
    }
}
