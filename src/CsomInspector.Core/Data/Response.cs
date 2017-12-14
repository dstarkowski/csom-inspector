using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace CsomInspector.Core
{
	public class Response
	{
		public ErrorInfo ErrorInfo { get; set; }

		public String LibraryVersion { get; set; }

		public String TraceCorrelationId { get; set; }

		public static Response FromJson(JToken responseElement)
		{
			return JsonConvert.DeserializeObject<Response>(responseElement.ToString());
		}
	}
}