using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Libraries.UtilityLib.Notification
{
    public class ObservableCollectionEx<T> : ObservableCollection<T> 
        where T: INotifyPropertyChanged
    {
        public ObservableCollectionEx()
            : base()
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(HandleCollectionChanged);
        }

        public ObservableCollectionEx(List<T> items)
            : base(items)
        {
            CollectionChanged += new NotifyCollectionChangedEventHandler(HandleCollectionChanged);
        }

        protected void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    (item as INotifyPropertyChanged).PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                }
            }
        }

        protected void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}

