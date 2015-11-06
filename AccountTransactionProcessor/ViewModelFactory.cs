using System.Collections.Generic;
using System.Configuration;
using System.Windows.Threading;
using ATPModel;
using ATPModel.FileProcessors;
using ATPViewModel;

namespace AccountTransactionProcessor
{
    public static class ViewModelFactory
    {

        public static AccountTransactionViewModel CreateATPViewModel(Dispatcher dispatcher)
        {
            var dirPath = ConfigurationManager.AppSettings["csvpath"];
            var processors = new Dictionary<string, IFileProcessor>
                  {
                      {"CSV", new CsvFileProcessor(dirPath) }
                  };
            var fileProcessorFactory = new FileProcessorFactory(processors);
            var accValidator = new AccTransactionValidator(new CurrencyValidator());
            var viewModel = new AccountTransactionViewModel(fileProcessorFactory, new ThreadExecutor(dispatcher), new DataService(), accValidator);
            return viewModel;
        }
    }
}
