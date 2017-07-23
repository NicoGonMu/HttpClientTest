using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaHttpClient.Services
{
    public interface IServices
    {
        Task<List<string>[]> sendQueryAsync(string userName);
    }
}
