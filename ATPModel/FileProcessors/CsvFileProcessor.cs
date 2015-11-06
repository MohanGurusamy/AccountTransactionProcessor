using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AccountTransactionData;

namespace ATPModel.FileProcessors
{
    public class CsvFileProcessor : FileProcessorBase
    {
        public CsvFileProcessor(string dirPath) : base("csv", dirPath ) { }
        private static readonly char[] _seperator = new char[]{','};


        protected override IEnumerable<AccTransaction> ReadTransactionDataFromFile(string path)
        {
            var transactionList = new List<AccTransaction>();
            string content = string.Empty;
            using(var reader = new StreamReader(path))
            {
                content = reader.ReadToEnd();
            }
            var allLines = content.Split(new [] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < allLines.Length; i++)
            {
                var line = allLines[i];
                int lineNumber = i + 1;
                var fields = line.Split(_seperator, StringSplitOptions.RemoveEmptyEntries);

                if(fields.Length != 4)
                {
                    yield return new AccTransaction();
                }
                else
                {
                    var account = fields[0].Trim();
                    var description = fields[1].Trim();
                    var currencyCode = fields[2].Trim();
                    decimal value = 0m;
                    decimal.TryParse(fields[3].Trim(), out value);
                    yield return new AccTransaction { Account = account, CurrencyCode = currencyCode, Description = description, Value = value };
                }
            }
        }
    }
}
