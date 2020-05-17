using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libraries.UnitLib
{
    public class Volume : Unit
    {
        public static Liter Liter = new Liter();
        public static FluidOunce FluidOunce = new FluidOunce();      
        public static Pint Pint = new Pint();
        public static Quart Quart = new Quart();
        public static Gallon Gallon = new Gallon();
        public static Cup Cup = new Cup();

        public static Volume BaseUnit = Liter;

        public Volume() { }

        protected Volume(decimal conversionFactor)
            : base(conversionFactor)
        {
        }
    }

    public class Liter : Volume
    {
        protected const decimal LITERS_IN_BASE_UNIT = 1.0m;
        public Liter() : base(LITERS_IN_BASE_UNIT) { }
    }

    public class FluidOunce : Volume
    {
        protected const decimal FLUIDOUNCES_IN_BASE_UNIT = 33.814m;
        public FluidOunce() : base(FLUIDOUNCES_IN_BASE_UNIT) { }
    }

    public class Pint : Volume
    {
        protected const decimal PINTS_IN_BASE_UNIT = 2.11338m;
        public Pint() : base(PINTS_IN_BASE_UNIT) { }
    }

    public class Quart : Volume
    {
        protected const decimal QUARTS_IN_BASE_UNIT = 1.05669m;
        public Quart() : base(QUARTS_IN_BASE_UNIT) { }
    }

    public class Gallon : Volume
    {
        protected const decimal GALLONS_IN_BASE_UNIT = 0.264172m;
        public Gallon() : base(GALLONS_IN_BASE_UNIT) { }
    }

    public class Cup : Volume
    {
        protected const decimal CUPS_IN_BASE_UNIT = 4.22675m;
        public Cup() : base(CUPS_IN_BASE_UNIT) { }
    }
}
