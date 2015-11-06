using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPModel.FileProcessors
{
    public interface IFileProcessorFactory
    {
        IReadOnlyList<string> ProcessorNames { get; }
        IFileProcessor GetFileProcessor(string code);
    }
}
