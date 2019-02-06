using currency.api.config;
using currency.api.domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace currency.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CurrencyController : ControllerBase
	{
		private const string jsonMediaType = "application/json";

		private readonly CurrencyLayerConfig _currencyLayerConfig;

		public CurrencyController(IOptions<CurrencyLayerConfig> options)
		{
			_currencyLayerConfig = options.Value;
		}

		// GET api/currency?source=USD&currencyFor=BRL
		[HttpGet]
		public ActionResult<dynamic> Get([FromQuery]string source, string currencyFor)
		{
			string uri = $"{_currencyLayerConfig.BaseAddress}live?access_key={_currencyLayerConfig.APIAccessKey}&source={source.ToUpper()}&currencies={currencyFor.ToUpper()}&format=1";
			HttpClient client = new HttpClient();
			client.DefaultRequestHeaders.Accept.Clear();
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(jsonMediaType));
			HttpResponseMessage message = client.GetAsync(uri).Result;
			if (!message.IsSuccessStatusCode)
				return $"{(int)message.StatusCode} {message.StatusCode} - {message.ReasonPhrase}";

			var result = message.Content.ReadAsStringAsync().Result;
			CurrencyLayer currencyReturn = JsonConvert.DeserializeObject<CurrencyLayer>(result);

			if (!currencyReturn.Success)
				return currencyReturn.Error;

			return currencyReturn.Quotes;
		}
	}
}