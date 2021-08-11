using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working.Requests
{
    public class HttpRequestMessageBuilder<T>
    {
        public HttpRequestMessage Build(RequestConfiguration<T> configuration, T obj)
        {
            string path = GetUrlPath(configuration, obj);
            string query = GetQueryString(configuration, obj);
            Uri url = new Uri($"{path}{query}", UriKind.Relative);
            HttpRequestMessage message = new HttpRequestMessage(configuration.HttpMethod, url);

            return message;
        }

        private string GetQueryString(RequestConfiguration<T> configuration, T obj)
        {
            string querystring = string.Empty;
            List<string> queries = new List<string>();

            foreach (var queryEvaluatorEntry in configuration.QueryEvaluators)
            {
                if (queryEvaluatorEntry.Value != null)
                {
                    string value = queryEvaluatorEntry.Value(obj);
                    queries.Add($"{queryEvaluatorEntry.Key}={value}");
                }
            }

            if (queries.Any())
            {
                querystring = $"?{string.Join("&", queries)}";
            }

            return querystring;
        }

        private string GetUrlPath(RequestConfiguration<T> configuration, T obj)
        {
            string path = configuration.RelativeUrl;

            foreach (var segmenEvaluatorEntry in configuration.UrlSegmetEvaluators)
            {
                if (segmenEvaluatorEntry.Value != null)
                {
                    string value = segmenEvaluatorEntry.Value(obj);
                    path = path.Replace(segmenEvaluatorEntry.Key, value);
                }
            }

            return path;
        }

        private HttpContent GetHttpContent(RequestConfiguration<T> configuration, T obj)
        {
            configuration.ContentFactory.BuildContent(obj);
        }
    }
}
