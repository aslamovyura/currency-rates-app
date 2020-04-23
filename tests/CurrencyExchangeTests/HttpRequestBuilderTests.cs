using Xunit;
using CurrencyExchange;
using System;

namespace CurrencyExchangeTests
{
    public class HttpRequestBuilderTests
    {
        public HttpRequestBuilder requestBuilder;

        public HttpRequestBuilderTests()
        {
            requestBuilder = new HttpRequestBuilder();
        }

        [Fact]
        public void RequestBuilder_WhenInitWithNullUrl_Return_ArgumentNullException()
        {
            // Arrange
            HttpRequestBuilder requestBuilder;
            string rootUrl = null;
            bool isException = false;

            // Act
            try
            {
                requestBuilder = new HttpRequestBuilder(rootUrl);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void RequestBuilder_WhenGetCurrencyInfoRequestWithNegativeIf_Return_ArgumentOutOfRangeException()
        {
            // Arrange
            int currencyId = -5;
            bool isException = false;

            // Act
            try
            {
                requestBuilder.GetCurrencyInfoRequest(currencyId);
            }
            catch (ArgumentOutOfRangeException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void RequestBuilder_WhenGetCurrencyRateRequestWithNegativeCode_Return_ArgumentOutOfRangeException()
        {
            // Arrange
            int currencyCode = -5;
            bool isException = false;

            // Act
            try
            {
                requestBuilder.GetCurrencyRateRequest(currencyCode);
            }
            catch (ArgumentOutOfRangeException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void RequestBuilder_WhenGetCurrencyRateRequestWithNullCurrencyAbbreviation_Return_ArgumentNullException()
        {
            // Arrange
            string curAbbreviation = null;
            bool isException = false;

            // Act
            try
            {
                requestBuilder.GetCurrencyRateRequest(curAbbreviation);
            }
            catch (ArgumentNullException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void RequestBuilder_WhenGetCurrencyRateRequestWithNegativeIdAndNormalDate_Return_ArgumentOutOfRangeException()
        {
            // Arrange
            int currencyId = -5;
            DateTime date = DateTime.Today;
            bool isException = false;

            // Act
            try
            {
                requestBuilder.GetCurrencyRateRequest(currencyId, date);
            }
            catch (ArgumentOutOfRangeException)
            {
                isException = true;
            }

            // Assert
            Assert.True(isException);
        }

        [Fact]
        public void RequestBuilder_WhenGetCurrencyRateRequestWithNormalIdAndDefaulDate_Return_ArgumentNullException()
        {
            // Arrange
            int currencyId = 1;
            DateTime date = default;
            bool isException = false;

            // Act
            try
            {
                requestBuilder.GetCurrencyRateRequest(currencyId, date);
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