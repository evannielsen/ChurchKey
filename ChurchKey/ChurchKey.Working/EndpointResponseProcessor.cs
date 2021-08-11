using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChurchKey.Abstractions.Content;

namespace ChurchKey.Working
{
    public abstract class EndpointResponseProcessor<TOutput> where TOutput : IEndpointOutput, new()
    {
        private List<(HttpStatusCode statusCode, Func<HttpResponseMessage, Task> handler)> StatusCodeHandlers = new();
        private TOutput outputInternal = new();
        private Func<TOutput, bool> successResolver;

        /// <summary>
        /// Registers handlers to populate the output object
        /// </summary>
        /// <typeparam name="TFactory"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="statusCode"></param>
        /// <param name="memberAccessor">Expression used to determine the property to populate with the output of the factory.</param>
        /// <returns></returns>
        protected EndpointResponseProcessor<TOutput> HasResponseHandler<TFactory, T>(HttpStatusCode statusCode, Expression<Func<TOutput, T>> memberAccessor) where TFactory : IResponseObjectFactory<T>, IDisposable, new()
        {
            Func<HttpResponseMessage, Task> handler = async (responseMessage) =>
            {
                var factory = new TFactory();
                var factoryResult = await factory.BuildResponseObjectAsync(responseMessage);
                factory.Dispose();
                outputInternal.SetPropertyValue(memberAccessor, factoryResult);
            };

            StatusCodeHandlers.Add((statusCode, handler));

            return this;
        }

        /// <summary>
        /// Register a function to resolve whether the response was a success.
        /// </summary>
        /// <param name="successResolver"></param>
        protected void HasSuccessResolver(Func<TOutput, bool> successResolver)
        {
            this.successResolver = successResolver;
        }

        private async Task PopulateOutputFromHandlers(HttpResponseMessage httpResponseMessage)
        {
            IEnumerable<Func<HttpResponseMessage, Task>>  handlers =
                StatusCodeHandlers
                .Where(h => h.statusCode == httpResponseMessage.StatusCode)
                .Select(h => h.handler);

            foreach (var handler in handlers)
            {
                await handler(httpResponseMessage);
            }
        }

        /// <summary>
        /// Attempt to resolve if the response indicates a success
        /// </summary>
        private void ResolveSuccess()
        {
            if (successResolver != null)
            {
                outputInternal.IsSuccess = successResolver(outputInternal);
            }
        }

        /// <summary>
        /// Hook to allow inspection / updating of output.
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual async Task<TOutput> FinalOutputProcessAsync(TOutput output)
        {
            return await Task.FromResult(output);
        }

        /// <summary>
        /// Entry point for handling the response message.
        /// </summary>
        /// <param name="httpResponseMessage"></param>
        /// <returns></returns>
        protected async Task<TOutput> HandleResponseMessage(HttpResponseMessage httpResponseMessage)
        {
            await PopulateOutputFromHandlers(httpResponseMessage);
            ResolveSuccess();
            await FinalOutputProcessAsync(outputInternal);
            return outputInternal;
        }
    }
}
