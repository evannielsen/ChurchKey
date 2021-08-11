using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChurchKey.Working
{
	public interface IRequestConfiguration
	{
		string RelativeUrl { get; set; }

		HttpMethod HttpMethod { get; set; }
	}

	public interface IRequestConfiguration<T> : IRequestConfiguration
	{
		void HasHeaderWithValue(string headerName, Func<T, string> headerValueFunc);
		void HasUrlSegmentValue(string placeholder, Func<T, string> segmentValueFunc);
		void HasBody(Func<T, object> bodyValueFunc);
		void HasQuery(string queryKey, Func<T, string> queryValueFunc);
	}
}
