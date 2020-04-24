using Xunit;
using CurrencyExchange;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchange.API;
using System.Collections.Generic;

namespace CurrencyExchangeTests
{
    public class CurrencyHandlerTests
    {
        public CurrencyHandler currencyHandler;

        public CurrencyHandlerTests()
        {
            currencyHandler = new CurrencyHandler();
        }

        [Fact]
        public void CurrencyHandler_WhenSendRequestAsyncWithNullRequest_Return_ArgumentNullException()
        {
            // Arrange
            string request = null;
            bool isException = false;

            Type type = typeof(CurrencyHandler);
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
                currencyHandler.ShowCurrencyRate(curAbbreviation);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void CurrencyHandler_WhenPrintNullCurrencyRate_Return_ArgumentNullException()
        {
            // Arrange
            Rate rate = new Rate();
            rate = null;
            bool isException = false;

            // Act
            try
            {
                currencyHandler.Print(rate);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void CurrencyHandler_WhenPrintNullCurrencyRateList_Return_ArgumentNullException()
        {
            // Arrange
            List<Rate> rates = new List<Rate>();
            bool isException = false;

            // Act
            try
            {
                currencyHandler.Print(rates);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void CurrencyHandler_WhenSaveToFileNullCurrencyRateList_Return_ArgumentNullExceptionAsync()
        {
            // Arrange
            List<Rate> rates = new List<Rate>();
            bool isException = false;

            // Act
            try
            {
                currencyHandler.SaveToFileAsync(rates).GetAwaiter().GetResult();
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