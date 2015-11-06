using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AccountTransactionData;

namespace ATPModel.FileProcessors
{
    public abstract class FileProcessorBase : IFileProcessor
    {
        private readonly string _fileExtension;
        private readonly string _dirPath;
        private readonly string _searchPattern;
        protected FileProcessorBase(string extension, string dirPath)
        {
            _fileExtension = extension;
            _dirPath = dirPath;
            _searchPattern = string.Format("*.{0}", extension);
        }

        public IEnumerable<string> GetAllFileNames()
        {
            return Directory.GetFiles(_dirPath, _searchPattern).Select(x => Path.GetFileNameWithoutExtension(x));
        }

        public IEnumerable<AccTransaction> ReadTransactionData(string fileName)
        {
            
            var pathWithoutExtension = Path.Combine(_dirPath, fileName);
            var path = Path.ChangeExtension(pathWithoutExtension, _fileExtension);
            return ReadTransactionDataFromFile(path);
        }

        protected abstract IEnumerable<AccTransaction> ReadTransactionDataFromFile(string path);
    }
}
