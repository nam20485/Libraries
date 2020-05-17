using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UtilityLib.Settings
{
    public class SettingChangedEventArgs<TValue> : EventArgs where TValue : struct
    {
        public readonly TValue PreviousValue;
        public readonly TValue NewValue;

        public SettingChangedEventArgs(TValue prevValue, TValue newValue)
        {
            PreviousValue = prevValue;
            NewValue = newValue;
        }
    }

    public delegate void SettingChangedHandler<TValue>(Setting<TValue> sender, SettingChangedEventArgs<TValue> e) where TValue : struct, IComparable;

    public class Setting<TValue> where TValue : struct, IComparable
    {
        public event SettingChangedHandler<TValue> SettingChanged;

        public readonly string Name;

        protected TValue value;
        public TValue Value
        {
            get { return this.value; }
            protected set
            {
                if (this.value.CompareTo(value) == 0)
                {
                    TValue prevValue = this.value;
                    this.value = value;
                    if (SettingChanged != null)
                    {
                        SettingChanged(this, new SettingChangedEventArgs<TValue>(prevValue, this.value));
                    }
                }
            }
        }

        public Setting(string name, TValue val)
        {
            Name = name;
            Value = val;
        }
    }
}
