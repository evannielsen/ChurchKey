using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
    public interface IInputOutputEndpoint<TInput, TOutput> 
        where TInput : IEndpointInput 
        where TOutput : IEndpointOutput
    {
        Task<TOutput> ExecuteAsync(TInput input);
    }
}
