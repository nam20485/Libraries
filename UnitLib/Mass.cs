using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UnitLib
{
    public class Mass : Unit
    {
        public static Pound Pound = new Pound();
        public static Gram Gram = new Gram();      

        public static Mass BaseUnit = Pound;

        public Mass() { }

        protected Mass(decimal conversionFactor)
            : base(conversionFactor)
        {
        }
    }

    public class Pound : Mass
    {
        protected const decimal POUNDS_IN_BASE_UNIT = 1.0m;
        public Pound() : base(POUNDS_IN_BASE_UNIT) { }
    }

    public class Gram : Mass
    {
        protected const decimal GRAMS_IN_BASE_UNIT = 453.592m;
        public Gram() : base(GRAMS_IN_BASE_UNIT) { }
    }
}
