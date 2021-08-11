using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Abstractions.Content
{
    public interface IResponseObjectFactory<T>
    {
        Task<T> BuildResponseObjectAsync(HttpResponseMessage responseMessage);
    }
}
