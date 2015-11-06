using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;
using ATPModel;
using ATPModel.FileProcessors;

namespace ATPViewModel
{
    public class AccountTransactionViewModel : NotifyPropertyChangedBase
    {
        public AccountTransactionViewModel(IFileProcessorFactory processorFactory, IViewThreadExecutor viewThreadExecutor, IDataService dataService, IAccTransactionValidator validator)
        {
            var processorTable = processorFactory.ProcessorNames.ToDictionary(x => x, x => processorFactory.GetFileProcessor(x));
            var details = processorFactory
                .ProcessorNames
                .Select(x =>
                {
                    var fileProcessor = processorFactory.GetFileProcessor(x);
                    var allFileDetails = fileProcessor.GetAllFileNames()
                    .Select(fileName => new FileDetailViewModel(fileName, dataService, viewThreadExecutor, fileProcessor, validator))
                    .ToArray();
                    return new ProcessorDetails(x, allFileDetails);
                }).ToList();
            ProcessorDetails = new ListCollectionView(details);
            if(details.Count > 0)
                ProcessorDetails.MoveCurrentToPosition(0);
            
        }

        public ListCollectionView ProcessorDetails { get; private set; }
    }
}
