using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Libraries.UtilityLib.Notification;


namespace Libraries.UnitLib
{
    public class Amount<U> : NotifyPropertyChangedEx 
        where U : Unit, new()
    {
        public decimal Magnitude { get; set; }
        public U Unit { get; set; }

        protected Amount()
        {
            Unit = new U();
        }

        public Amount(decimal magnitude, U unit)            
        {
            Magnitude = magnitude;
            Unit = unit;
        } 
       
        public Amount<D> Convert<D>() where D : Unit, new()
        {
            D destUnit = new D();            
            decimal magnitude = Unit.Convert<D>(Magnitude);
            return new Amount<D>(magnitude, destUnit);                     
        }        
    }       
}
