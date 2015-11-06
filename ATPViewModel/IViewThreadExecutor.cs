using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATPViewModel
{
    public interface IViewThreadExecutor
    {
        void Execute(Action action);
    }
}
