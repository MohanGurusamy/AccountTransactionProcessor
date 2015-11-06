using System;
using System.Collections.Generic;
using AccountTransactionData;
using ATPViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mocks;
using Moq;

namespace ATPViewModelTests
{
    [TestClass]
    public class FileDetailViewModelTests
    {
        private Poller _poller = new Poller(TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(5000));
        AccTransaction[] _data = new []
            {
                new AccTransaction { Account = "a", CurrencyCode = "EUR", Description = "d", Value = 1m },
                new AccTransaction { Account = "a", CurrencyCode = "ABC", Description = "d", Value = 1m }
            };

        [TestMethod]
        public void FileDetail_ErrorMessagesPopulatedWhenFirstItemIsInError()
        {
            AssertErrorMessagesAreProperlyPopulatedFor(0, _data);
        }

        [TestMethod]
        public void FileDetail_ErrorMessagesPopulatedWhenLastItemIsInError()
        {
            AssertErrorMessagesAreProperlyPopulatedFor(_data.Length-1, _data);
        }

        [TestMethod]
        public void FileDetail_ErrorMessagesEmptyWhenThereAreNoError()
        {
            AssertErrorMessagesAreProperlyPopulatedFor(-1, _data);
        }

        [TestMethod]
        public void FileDetail_OnlyValidDataIsSentToTheDatabase()
        {
            var subMessage = "Something is wrong";
            var name = "a";
            var fileProcessorMock = AtpMockFactory.FileProcessorMock;
            fileProcessorMock.Setup(x => x.ReadTransactionData(name)).Returns(_data);
            var validatorMock = AtpMockFactory.TransactionValidatorMock;

            var validItem = _data[0];
            var invalidItem = _data[1];

            validatorMock.Setup(x => x.Validate(invalidItem)).Returns(subMessage);
            validatorMock.Setup(x => x.Validate(validItem)).Returns(null as string);

            var itemsSentToDataBase = new List<AccTransaction>();

            var dataServiceMock = AtpMockFactory.DataServiceMock;
            dataServiceMock.Setup(x => x.AddItem(It.IsAny<AccTransaction>()))
                .Callback<AccTransaction>(x => itemsSentToDataBase.Add(x));
            var fileDetail = new FileDetailViewModel(
                name,
                dataServiceMock.Object,
                AtpMockFactory.ViewThreadExecutorMock.Object,
                fileProcessorMock.Object,
                validatorMock.Object);

            fileDetail.UploadCommand.Execute();

            var isCompleted = _poller.Poll(() => fileDetail.Status == Status.Uploaded);

            Assert.IsTrue(isCompleted);
            Assert.AreEqual(1, itemsSentToDataBase.Count);
            Assert.AreEqual(validItem, itemsSentToDataBase[0]);
            
        }

        private void AssertErrorMessagesAreProperlyPopulatedFor(int errorIndex, params AccTransaction[] data)
        {
            var subMessage = "Something is wrong";
            var errorMessage = string.Format("Line:{0} {1}", errorIndex + 1, subMessage);
            var name = "a";
            var fileProcessorMock = AtpMockFactory.FileProcessorMock;

            
            fileProcessorMock.Setup(x => x.ReadTransactionData(name)).Returns(data);

            var validatorMock = AtpMockFactory.TransactionValidatorMock;
            for(int i = 0; i < data.Length; i++)
            {
                if(errorIndex == i)
                    validatorMock.Setup(x => x.Validate(data[errorIndex])).Returns(subMessage);
                else
                    validatorMock.Setup(x => x.Validate(data[i])).Returns(null as string);
            }
            var fileDetail = new FileDetailViewModel(
                name,
                AtpMockFactory.DataServiceMock.Object,
                AtpMockFactory.ViewThreadExecutorMock.Object,
                fileProcessorMock.Object,
                validatorMock.Object);

            fileDetail.UploadCommand.Execute();

            var isCompleted = _poller.Poll(() => fileDetail.Status == Status.Uploaded);

            Assert.IsTrue(isCompleted);

            var expectedErrorCount = errorIndex + 1;
            
            if(expectedErrorCount > 0)
            {
                Assert.AreEqual(1, fileDetail.ErrorList.Count);
                Assert.AreEqual(errorMessage, fileDetail.ErrorList[0]);
            }
            else
            {
                Assert.AreEqual(0, fileDetail.ErrorList.Count);

            }
        }
    }
}
