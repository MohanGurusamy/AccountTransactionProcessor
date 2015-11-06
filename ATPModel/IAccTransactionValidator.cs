using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTransactionData;

namespace ATPModel
{
    public interface IAccTransactionValidator
    {
        string Validate(AccTransaction item);
    }
}
