using System;
using AccountTransactionData;
using ATPModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpModelTests
{
    [TestClass]
    public class AccTransactionValidatorTests
    {
        string _validCurrency = "EUR";
        string _invalidCurrency = "INVALID";
        AccTransaction _validFieldsValidCurrency;
        AccTransaction _validWithInvalidCurrency;
        AccTransaction _invalidFields;


        [TestInitializeAttribute]
        public void OnSetUp()
        {
            _validFieldsValidCurrency = new AccTransaction { Account = "a", CurrencyCode = _validCurrency, Description = "d", Value = 1m };
            _validWithInvalidCurrency= new AccTransaction { Account = "a", CurrencyCode = _invalidCurrency, Description = "d", Value = 1m };
            _invalidFields= new AccTransaction { Account = "", CurrencyCode = _validCurrency, Description = "d", Value = 1m };
        }

        [TestMethod]
        public void Validate_ValidFieldsAndValidCurrency_ReturnsNull()
        {
            var validator = new AccTransactionValidator(new CurrencyValidator());
            Assert.IsNull(validator.Validate(_validFieldsValidCurrency)); 
        }

        [TestMethod]
        public void Validate_ValidFieldsAndInvalidCurrency_ReturnsErrorMsg()
        {
            var validator = new AccTransactionValidator(new CurrencyValidator());

            Assert.IsNotNull(validator.Validate(_validWithInvalidCurrency));
        }

        [TestMethod]
        public void Validate_InvalidFields_ReturnsErrorMsg()
        {
            var validator = new AccTransactionValidator(new CurrencyValidator());

            Assert.IsNotNull(validator.Validate(_invalidFields));
        }
    }
}
