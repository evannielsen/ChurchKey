using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
    public static class Endpoint
    {
        public static class WithInput<TInput> where TInput : IEndpointInput
        {
            public static class WithOutput<TOutput> where TOutput : IEndpointOutput
            {

            }
        }

        public static class WithoutInput
        {
            public static class WithOutput<TOutput> where TOutput : IEndpointOutput
            {

            }
        }
    }
}
