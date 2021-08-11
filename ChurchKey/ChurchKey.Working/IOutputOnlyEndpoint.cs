using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
    public interface IOutputOnlyEndpoint<TOutput> where TOutput : IEndpointOutput
    {
        Task<TOutput> ExecuteAsync();
    }
}
