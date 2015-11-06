using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTransactionData;

namespace ATPModel
{
    public class AccTransactionValidator : IAccTransactionValidator
    {
        private readonly ICurrencyValidator _currencyValidator;

        public AccTransactionValidator(ICurrencyValidator currencyValidator)
        {
            _currencyValidator = currencyValidator;
        }
        public string Validate(AccTransaction item)
        {
            if(string.IsNullOrWhiteSpace(item.Account) || string.IsNullOrWhiteSpace(item.CurrencyCode) || string.IsNullOrWhiteSpace(item.Description) || item.Value <= 0)
                return "Fields are not properly formed";

            if(!_currencyValidator.IsCurrencyValid(item.CurrencyCode))
                return "Currency code is not valid";

            return null;
        }

        
    }
}
