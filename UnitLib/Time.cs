using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UnitLib
{
    public class Time : Unit
    {
        public static Second Second = new Second();
        public static Minute Minute = new Minute();
        public static Hour Hour = new Hour();

        public static Time BaseUnit = Second;

        public Time() { }

        protected Time(decimal conversionFactor)
            : base(conversionFactor)
        {
        }
    }

    public class Second : Time
    {
        protected const decimal BASE_UNITS_PER_SECOND = 1.0m;                    
        public Second() : base(BASE_UNITS_PER_SECOND) { }
    }

    public class Minute : Time
    {
        protected const decimal BASE_UNITS_PER_MINUTE = 60;                    
        public Minute() : base(BASE_UNITS_PER_MINUTE) { }
    }

    public class Hour : Time
    {
        protected const decimal BASE_UNITS_PER_HOUR = 60*60;
        public Hour() : base(BASE_UNITS_PER_HOUR) { }
    }
}
