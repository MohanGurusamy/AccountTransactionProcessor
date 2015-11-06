using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountTransactionData;
using ATPModel.FileProcessors;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AtpModelTests
{
    [TestClass]
    public class CsvFileProcessorTests
    {
        string _csvFileName;
        string _cvsFilePath;
        AccTransaction item1 = new AccTransaction { Account = "a", CurrencyCode = "EUR", Description = "d", Value = 1m };
        AccTransaction item2 = new AccTransaction { Account = "e", CurrencyCode = "USD", Description = "f", Value = 1m };

        [TestInitialize]
        public void Setup()
        {
            _csvFileName = Guid.NewGuid().ToString().Replace("-", string.Empty);
            var path = Path.GetTempPath();
            path = Path.Combine(path, _csvFileName);
            _cvsFilePath = Path.ChangeExtension(path, "csv");

            CreateFile(_cvsFilePath, item1, item2);
        }

        private void CreateFile(string filename, params AccTransaction[] items)
        {
            using(var writer = new StreamWriter(filename))
            {
                foreach(var item in items)
                {
                    writer.WriteLine("{0},{1},{2},{3}", item.Account, item.Description, item.CurrencyCode, item.Value);
                }
            }
        }

        [TestCleanup]
        public void TearDown()
        {
            File.Delete(_cvsFilePath);
        }

        [TestMethod]
        public void CsvFileProcessor_ItemsAreReadCorrectly()
        {
            var processor = new CsvFileProcessor(Path.GetTempPath());
            var items = processor.ReadTransactionData(_csvFileName).ToList();

            Assert.AreEqual(2, items.Count);
            Assert.AreEqual(item1, items[0]);
            Assert.AreEqual(item2, items[1]);
        }
    }
}
