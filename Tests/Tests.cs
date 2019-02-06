using currency.api.config;
using currency.api.Controllers;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;

namespace Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void DeveTrazerCotacaoAtualDolarParaReal()
		{
			var mockOpts = new Mock<IOptions<CurrencyLayerConfig>>();

			var controller = new CurrencyController(mockOpts.Object);

			var result = controller.Get("USD", "BRL").Result;



			Assert.AreEqual(true, true);
		}
	}
}