using currency.api.config;
using currency.api.Controllers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Tests
{
	public class Tests
	{
		private CurrencyLayerConfig _config = new CurrencyLayerConfig();
		private Mock<IOptions<CurrencyLayerConfig>> _mockOpts = new Mock<IOptions<CurrencyLayerConfig>>();

		[SetUp]
		public void Setup()
		{
			_config.APIAccessKey = ("58866c4084e7b975298b7a04a6b70d7b");
			_config.BaseAddress = ("http://apilayer.net/api/");
			_mockOpts.Setup(x => x.Value).Returns(_config);
		}

		[Test]
		public void DeveInstanciarCurrencyController()
		{
			var controller = new CurrencyController(_mockOpts.Object);
			Assert.IsNotNull(controller);
		}

		[Test]
		public void DeveTrazerCotacaoAtualDolarParaReal()
		{
			string source = "USD";
			string currencyFor = "BRL";

			var controller = new CurrencyController(_mockOpts.Object);
			var retorno = controller.Get(source, currencyFor).Value;

			string currency = retorno[source + currencyFor].Path;
			decimal value = (decimal)retorno[source + currencyFor].Value;

			Assert.AreEqual(source + currencyFor, currency);
			Assert.NotZero(value);
			Assert.IsNotNull(retorno);
		}

		[Test]
		public void DeveTrazerCotacaoAtualDolarParaVariasMoedas()
		{
			string source = "USD";
			string currencyForBRL = "BRL";
			string currencyForEUR = "EUR";
			string currencyForCAD = "CAD";

			var controller = new CurrencyController(_mockOpts.Object);
			var retorno = controller.Get(source, $"{currencyForBRL},{currencyForEUR},{currencyForCAD}").Value;

			string currencyBRL = retorno[source + currencyForBRL].Path;
			decimal valueBRL = (decimal)retorno[source + currencyForBRL].Value;
			string currencyEUR = retorno[source + currencyForEUR].Path;
			decimal valueEUR = (decimal)retorno[source + currencyForEUR].Value;
			string currencyCAD = retorno[source + currencyForCAD].Path;
			decimal valueCAD = (decimal)retorno[source + currencyForCAD].Value;

			Assert.AreEqual(source + currencyForBRL, currencyBRL);
			Assert.AreEqual(source + currencyForEUR, currencyEUR);
			Assert.AreEqual(source + currencyForCAD, currencyCAD);
			Assert.NotZero(valueBRL);
			Assert.NotZero(valueEUR);
			Assert.NotZero(valueCAD);
			Assert.IsNotNull(retorno);
		}
	}
}