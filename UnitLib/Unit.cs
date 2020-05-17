using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Libraries.UnitLib
{
    [DataContract]
    [KnownType(typeof(Pound))]
    public abstract class Unit
    {
        protected readonly decimal conversionFactor = 0.0m;
        public decimal ConversionFactor { get { return conversionFactor; } }  // multiply by this to convert to BaseUnit 

        public virtual string Name
        {
            get
            {
                return this.GetType().Name;
            }
        }

        protected Unit() { }

        protected Unit(decimal conversionFactor)
        {
            this.conversionFactor = conversionFactor;
        }

        public virtual decimal Convert<D>(decimal magnitude) where D : Unit, new()
        {
            return Convert(magnitude, new D());
        }

        public virtual decimal Convert(decimal magnitude, Unit destUnit)
        {         
            decimal convertedMagnitude = magnitude * ConversionFactor / destUnit.ConversionFactor;
            return convertedMagnitude;
        }
    }
}
