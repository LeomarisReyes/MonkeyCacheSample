using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MonkeyCacheSample.Models;
using Refit;

namespace MonkeyCacheSample.Services
{
    public interface IFuelsApi
    {
        [Get("/api/fuels")]
        Task<HttpResponseMessage> GetFuels();
    }
}
