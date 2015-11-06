using System;
using ATPModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpModelTests
{
    [TestClass]
    public class CurrencyValidatorTests
    {
        string _validCurrency = "EUR";
        string _invalidCurrency = "INVALID";

        [TestMethod]
        public void IsCurrencyValid_InvalidCurrency_ReturnsFalse()
        {
            var currencyValidator = new CurrencyValidator();
            var isValid = currencyValidator.IsCurrencyValid(_invalidCurrency);
            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsCurrencyValid_ValidCurrency_ReturnsTrue()
        {
            var currencyValidator = new CurrencyValidator();
            var isValid = currencyValidator.IsCurrencyValid(_validCurrency);
            Assert.IsTrue(isValid);
        }
    }
}
