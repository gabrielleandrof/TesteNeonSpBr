using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace currency.api.domain
{
	public class CurrencyLayer
	{
		[JsonProperty]
		public bool Success { get; set; }
		[JsonProperty]
		public string Source { get; set; }
		[JsonProperty]
		public JToken Quotes { get; set; }
		[JsonProperty]
		public Error Error { get; set; }
	}

	public class Error
	{
		[JsonProperty]
		public int Code { get; set; }
		[JsonProperty]
		public string Type { get; set; }
		[JsonProperty]
		public string Info { get; set; }
	}
}