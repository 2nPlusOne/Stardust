using System;

namespace Spotnose.Stardust
{
    [Serializable]
    public struct SizeOrder
    {
        public CelestialBodyType celestialBodyType;
        public SizeCategory sizeCategory;

        private int Integer() => (int) celestialBodyType + (int) sizeCategory;
        
        public static implicit operator int(SizeOrder sizeOrder) => sizeOrder.Integer();
        
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            
            return Integer() == (int) obj;
        }
        
        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Integer();
        }
        
        public static bool operator ==(SizeOrder a, SizeOrder b) => a.Integer() == b.Integer();
        public static bool operator !=(SizeOrder a, SizeOrder b) => a.Integer() != b.Integer();
        public static bool operator >(SizeOrder a, SizeOrder b) => a.Integer() > b.Integer();
        public static bool operator <(SizeOrder a, SizeOrder b) => a.Integer() < b.Integer();
        public static bool operator <=(SizeOrder a, SizeOrder b) => a.Integer() <= b.Integer();
        public static bool operator >=(SizeOrder a, SizeOrder b) => a.Integer() >= b.Integer();
    }
}
