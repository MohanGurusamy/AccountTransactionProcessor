using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ATPModel;
using ATPModel.FileProcessors;
using Microsoft.Practices.Prism.Commands;
using System.Linq;

namespace ATPViewModel
{
    public class FileDetailViewModel : NotifyPropertyChangedBase
    {
        private readonly IDataService _dataService;
        private readonly IViewThreadExecutor _viewThreadExecutor;
        private readonly IFileProcessor _fileProcessor;
        private readonly IAccTransactionValidator _validator;

        public FileDetailViewModel(string name, IDataService dataService, IViewThreadExecutor viewThreadExecutor, IFileProcessor fileProcessor, IAccTransactionValidator validator)
        {
            Name = name;
            _dataService = dataService;
            _viewThreadExecutor = viewThreadExecutor;
            _fileProcessor = fileProcessor;
            _validator = validator;
            UploadCommand = new DelegateCommand(() => OnUploadData(), () => (Status==Status.NotUploaded) && (!IsUploading) );
            ErrorList = new ObservableCollection<string>();
        }

        private bool _isUploading;
        public bool IsUploading
        {
            get
            {
                return _isUploading;
            }
            set
            {
                if(_isUploading == value)
                    return;
                _isUploading = value;
                RaiseNotification();
                UploadCommand.RaiseCanExecuteChanged();
            }
        }

        private Status _status = Status.NotUploaded;
        public Status Status
        {
            get
            {
                return _status;
            }
            set
            {
                if(_status == value)
                    return;
                _status = value;
                RaiseNotification();
            }
        }

        async private void OnUploadData()
        {
            IsUploading = true;
            await UploadData(Name);
            IsUploading = false;
            Status = Status.Uploaded;
        }

        private int _total=0; 
        public int TotalCount
        {
            get
            {
                return _total;
            }
            set
            {
                if(_total == value)
                    return;
                _total = value;
                RaiseNotification();
                RaiseNotification("ImportedCount");
            }
        }

        private int _uploadedCount;
        public int UploadedCount
        {
            get
            {
                return _uploadedCount;
            }
            set
            {
                if(_uploadedCount == value)
                    return;
                _uploadedCount = value;
                RaiseNotification();
                RaiseNotification("ImportedCount");
            }
        }

        public int ImportedCount
        {
            get
            {
                return UploadedCount - ErrorList.Count;
            }
        }

        private Task UploadData(string fileName)
        {
            return Task.Factory.StartNew(() =>
            {

                UploadedCount = 0;
                var allTransactions = _fileProcessor.ReadTransactionData(fileName).ToList();
                TotalCount = allTransactions.Count;
                foreach(var item in allTransactions)
                {
                    UploadedCount = UploadedCount+1;
                    string errorMsg = _validator.Validate(item);
                    if(errorMsg!=null)
                    {
                        errorMsg = string.Format("Line:{0} {1}", UploadedCount, errorMsg);
                        _viewThreadExecutor.Execute(() => ErrorList.Add(errorMsg));
                        RaiseNotification("ImportedCount");
                    }
                    else
                    {
                        _dataService.AddItem(item);
                    }
                }
            });
        }

        public ObservableCollection<string> ErrorList { get; private set; }

        public DelegateCommand UploadCommand { get; private set; }
        public string Name { get; private set; }
       
    }
}
