using System;
using System.Windows.Threading;
using ATPViewModel;

namespace AccountTransactionProcessor
{
    class ThreadExecutor : IViewThreadExecutor
    {
        private readonly Dispatcher _dispatcher;
        public ThreadExecutor(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public void Execute(Action action)
        {
            if(_dispatcher.CheckAccess())
            {
                action();
            }
            else
            {
                _dispatcher.Invoke(action);
            }
        }
    }
}
