using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATPViewModel;
using ATPViewModel.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ATPViewModelTests
{
    [TestClass]
    public class StatusValueConverterTests
    {
        [TestMethod]
        public void Convert_ValuesAreConvertedCorrectly()
        {
            var converter = new StatusValueConverter();
            //"Uploaded" : "Not uploaded"
            Assert.AreEqual("Uploaded", converter.Convert(Status.Uploaded, null, null, null));
            Assert.AreEqual("Not uploaded", converter.Convert(Status.NotUploaded, null, null, null));
        }
    }
}
