namespace neighborhood_api.Models.Complaints
{
    public class Complaint
    {
        public string Id { get; set; }
        public string PersonName { get; set; }
        public string PersonApartmentCode { get; set; }
        public Locations Location { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        public ActiveStatus CurrentStatus { get; set; }
        public DateTime DateActivated { get; set; }
        public DateTime? DateDeActivated { get; set; }
    }
}
