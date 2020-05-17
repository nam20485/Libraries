using System;

namespace Libraries.UnitLib
{
    public class SpecificGravity : Unit    
    {
        public static DegreesPlato DegreesPlato = new DegreesPlato();
        //public static Gram Gram = new Gram();      

        public static SpecificGravity BaseUnit = DegreesPlato;

        public SpecificGravity() { }

        protected SpecificGravity(decimal conversionFactor)
            : base(conversionFactor)
        {
        }
    }

    public class DegreesPlato : SpecificGravity
    {
        protected const decimal DEGREESPLATO_IN_BASE_UNIT = 1.0m;
        public DegreesPlato() : base(DEGREESPLATO_IN_BASE_UNIT) { }
    }
}

