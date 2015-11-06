using System;
using System.Collections.Generic;
using AccountTransactionData;

namespace ATPModel.FileProcessors
{
    public interface IFileProcessor
    {
        IEnumerable<string> GetAllFileNames();
        IEnumerable<AccTransaction> ReadTransactionData(string fileName);
    }
}
