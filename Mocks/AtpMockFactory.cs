using System;
using ATPModel;
using ATPModel.FileProcessors;
using ATPViewModel;
using Moq;

namespace Mocks
{
    public static class AtpMockFactory
    {
        //IDataService dataService, IViewThreadExecutor viewThreadExecutor, IFileProcessor fileProcessor, IAccTransactionValidator validator

        public static Mock<IFileProcessor> FileProcessorMock
        {
            get
            {
                return new Mock<IFileProcessor>();
            }
        }

        public static Mock<IAccTransactionValidator> TransactionValidatorMock
        {
            get
            {
                return new Mock<IAccTransactionValidator>();
            }
        }

        public static Mock<IDataService> DataServiceMock
        {
            get
            {
                return new Mock<IDataService>();
            }
        }

        public static Mock<IViewThreadExecutor> ViewThreadExecutorMock
        {
            get
            {
                var mk  = new Mock<IViewThreadExecutor>();
                mk.Setup(x => x.Execute(It.IsAny<Action>())).Callback<Action>(x =>  x());
                return mk;
            }
        }
    }
}
