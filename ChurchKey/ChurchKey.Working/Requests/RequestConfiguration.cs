using ChurchKey.Abstractions.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
    public class RequestConfiguration<T> : IRequestConfiguration<T>
    {
        public string RelativeUrl { get; set; }

        public HttpMethod HttpMethod { get; set; }

        public Dictionary<string, Func<T, string>> HeaderEvaluators { get; } = new Dictionary<string, Func<T, string>>();
        
        public Dictionary<string, Func<T, string>> UrlSegmetEvaluators { get; } = new Dictionary<string, Func<T, string>>();

        public Dictionary<string, Func<T, string>> QueryEvaluators { get; } = new Dictionary<string, Func<T, string>>();

        //public Func<T, object> BodyEvaluator { get; private set; }

        public IRequestContentFactory<T> CustomContentFactory { get; private set; }

        //public void HasBody(Func<T, object> bodyValueFunc)
        //{
        //    this.BodyEvaluator = bodyValueFunc;
        //}

        public void HasCustomContent(IRequestContentFactory<T> requestContentFactory)
        {
            CustomContentFactory = requestContentFactory;
        }

        public void HasHeaderWithValue(string headerName, Func<T, string> headerValueFunc)
        {
            HeaderEvaluators.Add(headerName, headerValueFunc);
        }

        public void HasQuery(string queryKey, Func<T, string> queryValueFunc)
        {
            QueryEvaluators.Add(queryKey, queryValueFunc);
        }

        public void HasUrlSegmentValue(string placeholder, Func<T, string> segmentValueFunc)
        {
            UrlSegmetEvaluators.Add(placeholder, segmentValueFunc);
        }
    }
}
