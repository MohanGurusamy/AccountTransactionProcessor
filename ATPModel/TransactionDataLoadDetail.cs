using AccountTransactionData;

namespace ATPModel
{
    public class TransactionDataLoadDetail
    {
        private readonly AccTransaction _data;
        private readonly string _error;

        public TransactionDataLoadDetail(AccTransaction data, string errorMsg=null)
        {
            _data = data;
            _error = errorMsg;
        }

        public AccTransaction Data
        {
            get { return _data; }
        }

        public string Error
        {
            get { return _error; }
        }

        public bool HasError
        {
            get
            {
                return !string.IsNullOrWhiteSpace(_error);
            }
        }
    }
}
