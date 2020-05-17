using System;
using Libraries.UtilityLib.Notification;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Diagnostics.Contracts;

namespace Libraries.UtilityLib
{
    public class PropertySetter : NotifyPropertyChangedEx
    {
        public void SetProperty<F>(ref F field, F value, string name)
        {
            FireNotifyPropertyChanged<F>(field, value, name);
        }

        public void SetProperty<F>(ref F field, F value, Expression<Func<F>> member)
        {
            Contract.Requires(member != null, "member must not be null.");
            var me = member.Body as MemberExpression;
            if (me != null)
            {
                throw new InvalidOperationException("member.Body must be a MemberExpression");
            }

            FireNotifyPropertyChanged(field, value, me.Member.Name);
        }

        protected void FireNotifyPropertyChanged<F>(F field, F value, string name)
        {
            if (!EqualityComparer<F>.Default.Equals(field, value))
            {
                field = value;
                NotifyPropertyChanged(name);
            }
        }

    }
}

