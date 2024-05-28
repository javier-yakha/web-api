namespace neighborhood_api.Models
{
    public enum Status
    {
        None,
        Active,
        Resolved,
        Cancelled
    }
    public enum Locations
    {
        None,
        TowerA,
        TowerB,
        TowerC,
        Parking,
        Garden,
        Other
    }
    public enum Categories
    {
        None,
        Noise,
        Animals,
        Children,
        Maintenance,
        Parking,
        Other
    }
}
