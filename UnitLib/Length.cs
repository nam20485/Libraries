using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UnitLib
{
    public class Length : Unit
    {
        public static Foot Foot = new Foot();
        public static Yard Yard = new Yard();
        public static Meter Meter = new Meter();

        public static Length BaseUnit = Foot;

        public Length() { }

        protected Length(decimal conversionFactor)
            : base(conversionFactor)
        {
        }       
    }

    public class Foot : Length
    {
        protected const decimal BASE_UNITS_PER_FOOT = 1.0m;                    
        public Foot() : base(BASE_UNITS_PER_FOOT) { }
    }

    public class Meter : Length
    {        
        protected const decimal BASE_UNITS_PER_METER = 3.28084m;   
        public Meter() : base(BASE_UNITS_PER_METER) { }      
    }

    public class Yard : Length
    {
        protected const decimal BASE_UNITS_PER_YARD = 3.0m;
        public Yard() : base(BASE_UNITS_PER_YARD) { }                  
    }
}
