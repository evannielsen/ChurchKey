using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
    public class EndpointExecutor<TOutput> : EndpointResponseProcessor<TOutput> where TOutput : IEndpointOutput, new()
    {
        protected string relativeUrl = string.Empty;

        public EndpointExecutor()
        {
            
        }

        public async Task<TOutput> ExecuteAsync()
        {

        }
    }
}
