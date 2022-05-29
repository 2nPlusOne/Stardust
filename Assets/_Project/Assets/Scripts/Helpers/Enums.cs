namespace Spotnose.Stardust
{
    public enum ParticulateType { DustParticulate, MetalParticulate, WaterParticulate }
    
    public enum CelestialBodyType
    {
        Asteroid = 0,
        Moon = 10,
        Planet = 20
    }

    public enum SizeCategory
    {
        Minuscule = 0,
        Tiny = 1,
        Small = 2,
        Medium = 3,
        Large = 4,
        Huge = 5,
        Gigantic = 6,
        Enormous = 7,
        Colossal = 8
    }
    
    public enum InventoryItemType { Metal }
    
    public enum PoolComponentType
    {
        Particulate,
        Debris
    }
}