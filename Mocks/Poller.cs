using System;
using System.Threading;

namespace Mocks
{
    public class Poller
    {
        public Poller(TimeSpan interval, TimeSpan timeOut)
        {
            this.Interval = interval;
            this.TimeOut = timeOut;
        }

        public TimeSpan Interval { get; private set; }
        public TimeSpan TimeOut { get; private set; }

        public bool Poll(Func<bool> action)
        {
            var endTime = DateTime.Now + TimeOut;
            while(DateTime.Now <= endTime)
            {
                if(action())
                    return true;
                Thread.Sleep(Interval);
            }
            return false;
        }
    }
}
