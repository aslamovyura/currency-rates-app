using Xunit;
using CurrencyExchange.Core.Controllers;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchange.Core.Models;
using System.Collections.Generic;

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
                currencyController.Print(rate);
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
                currencyController.Print(rates);
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
                currencyController.SaveToFileAsync(rates).GetAwaiter().GetResult();
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