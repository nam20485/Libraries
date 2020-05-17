using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Libraries.UtilityLib.Notification
{
    public interface INotifyPropertyChangedEx : INotifyPropertyChanged
    {      
        void NotifyPropertyChanged(string propertyName);
    }
}

