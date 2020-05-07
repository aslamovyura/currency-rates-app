using Xunit;
using CurrencyExchange.Core.Controllers;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchangeTests
{
    public class CurrencyHandlerTests
    {
        public CurrencyController currencyController;

        public CurrencyHandlerTests()
        {
            currencyController = new CurrencyController();
        }

        [Fact]
        public void CurrencyHandler_WhenSendRequestAsyncWithNullRequest_Return_ArgumentNullException()
        {
            // Arrange
            string request = null;
            bool isException = false;

            Type type = typeof(CurrencyController);
            var handler = Activator.CreateInstance(type);
            MethodInfo method = type.GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.Name == "SendRequestAsync" && x.IsPrivate)
                .First();

            // Act
            try
            {
                var sendRequestTask = (Task<string>)method.Invoke(handler, new object[] { request });
                var response = sendRequestTask.GetAwaiter().GetResult();
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void CurrencyHandler_WhenShowCurrencyRateWithNullCurrencyAbbreviation_Return_ArgumentNullException()
        {
            // Arrange
            string curAbbreviation = null;
            bool isException = false;

            // Act
            try
            {
                currencyController.ShowCurrencyRate(curAbbreviation);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }
    }
}