using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ATPViewModel
{
    public class NotifyPropertyChangedBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaiseNotification([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if(handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
