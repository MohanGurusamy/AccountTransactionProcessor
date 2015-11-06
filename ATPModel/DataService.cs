using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTransactionData;
using ATPModel;

namespace ATPModel
{
    public class DataService : IDataService
    {
        public void AddItem(AccTransaction item)
        {
            AddItemsImpl(item);
        }

        public void AddItems(AccTransaction[] items)
        {
            AddItemsImpl(items);
        }

        private void AddItemsImpl(params AccTransaction[] items)
        {
            using(var context = new AccountTransactionEntities1())
            {
                context.AccTransactions.AddRange(items);
                context.SaveChanges();
            }
        }
    }
}
