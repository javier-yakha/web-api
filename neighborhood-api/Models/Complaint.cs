namespace neighborhood_api.Models
{
    public class Complaint
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
        public string Id { get; set; }
        public string PersonName { get; set; }
        public string PersonApartmentCode { get; set; }
        public Locations Location { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public Status CurrentStatus { get; set; }
        public DateTime DateActivated { get; set; }
        public DateTime? DateDeActivated { get; set; }

    }
}
