namespace neighborhood_api.Models
{
    public class Complaint
    {
        public enum ComplaintStatus
        {
            Active,
            Resolved,
            Cancelled
        }
        public enum ApartmentLocations
        {
            TowerA,
            TowerB,
            TowerC,
            Parking,
            Garden
        }
        public enum ComplaintCategories
        {
            Animals,
            Children,
            Maintenance,
            Parking,
            Other
        }
        public string Id { get; set; }
        public string IssuerName { get; set; }
        public int IssuerApartmentNumber { get; set; }
        public ApartmentLocations Location { get; set; }
        public string Description { get; set; }
        public ComplaintStatus Status { get; set; }
        public DateTime DateActivated { get; set; }
        public DateTime DateDeActivated { get; set; }

    }
}
