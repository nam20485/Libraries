using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Libraries.UtilityLib.Notification
{
    [DataContract]
    public class NotifyPropertyChangedEx : INotifyPropertyChangedEx
    {
        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChangedEx implementation
        public void NotifyPropertyChanged(string propertyName)
        {           
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }        
        }
        #endregion

        protected void NotifyPropertyChanged()
        {
            string propertyName = Reflection.Reflection.GetCallingMethodName();
            NotifyPropertyChanged(propertyName);
        }
    }
}

