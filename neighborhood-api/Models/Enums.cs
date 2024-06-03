namespace neighborhood_api.Models
{
    public enum ActiveStatus
    {
        None,
        Active,
        Resolved,
        Cancelled
    }
    public enum Locations
    {
        Other,
        TowerA,
        TowerB,
        TowerC,
        Parking,
        Garden
    }
    public enum Categories
    {
        Other,
        Noise,
        Animals,
        Children,
        Maintenance,
        Parking
    }
}
