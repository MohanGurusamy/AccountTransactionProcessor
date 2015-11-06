using AccountTransactionData;
using ATPModel;

namespace ATPModel
{
    public interface IDataService
    {
        void AddItems(AccTransaction[] item);
        void AddItem(AccTransaction item);
    }
}
