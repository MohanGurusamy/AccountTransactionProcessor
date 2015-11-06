using System.Collections.Generic;
using System.Linq;

namespace ATPModel.FileProcessors
{
    public class FileProcessorFactory : IFileProcessorFactory
    {
        private readonly IDictionary<string, IFileProcessor> _processors = new Dictionary<string, IFileProcessor>();
        private readonly IReadOnlyList<string> _processorNames;

        public FileProcessorFactory(IDictionary<string, IFileProcessor> processors)
        {
            _processors = processors;
            _processorNames = _processors.Keys.ToArray();
        }

        public IReadOnlyList<string> ProcessorNames
        {
            get { return _processorNames; }
        }

        public IFileProcessor GetFileProcessor(string code)
        {
            return _processors[code];
        }
    }
}
