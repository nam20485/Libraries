using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UnitLib
{
    public class Temperature : Unit
    {
        public static Fahrenheit Fahrenheit = new Fahrenheit();
        public static Celsius Celsius = new Celsius();
        public static Kelvin Kelvin = new Kelvin();
        public static Centigrade Centigrade = new Centigrade();

        public static Temperature BaseUnit = Fahrenheit;
       
        public decimal ZeroShift { get; protected set; }

        public Temperature() { }

        protected Temperature(decimal conversionFactor, decimal zeroShift)
            : base(conversionFactor)
        {
            ZeroShift = zeroShift;
        }

        public new decimal Convert<D>(decimal magnitude) where D : Temperature, new()
        {
            return Convert(magnitude, new D());
        }
        
        public override decimal Convert(decimal magnitude, Unit destUnit)
        {
            decimal baseMagnitude = magnitude * ConversionFactor + ZeroShift;
            decimal convertedMagnitude = (baseMagnitude - (destUnit as Temperature).ZeroShift) / destUnit.ConversionFactor;
            return convertedMagnitude;
        }
    }

    public class Fahrenheit : Temperature
    {
        public Fahrenheit() : base(1.0m, 0.0m)
        { 
        }
    }

    public class Celsius : Temperature
    {
        public Celsius() : base(9.0m/5.0m, 32.0m)
        {
        }
    }

    public class Kelvin : Temperature
    {
        public Kelvin() : base(9.0m/5.0m, -459.67m)
        {
        }
    }

    public class Centigrade : Celsius
    {        
    }
}
