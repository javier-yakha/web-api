namespace Models.Complaints.Requests
{
    public class CreateComplaint
    {
        public string PersonName { get; set; }
        public string PersonApartmentCode { get; set; }
        public Locations Location { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
    }
}
