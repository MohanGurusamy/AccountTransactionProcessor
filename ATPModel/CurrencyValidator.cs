using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ATPModel
{
    public class CurrencyValidator : ICurrencyValidator
    {
        private readonly HashSet<string> _currencies = new HashSet<string>();
        private const string _currencyFile = "ATPModel.Data.currencies.csv";
        public CurrencyValidator()
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(_currencyFile);
            using(var reader = new StreamReader(stream))
            {
                var line = reader.ReadLine();
                while(line!=null)
                {
                    var trimmed = line.Trim();
                    if(!string.IsNullOrWhiteSpace(trimmed))
                    {
                        _currencies.Add(trimmed);
                    }
                    line = reader.ReadLine();
                }
            }
        }
        public bool IsCurrencyValid(string code)
        {
            return _currencies.Contains(code);
        }
    }
}
