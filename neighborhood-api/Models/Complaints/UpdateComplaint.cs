namespace neighborhood_api.Models.Complaints
{
    public class UpdateComplaint
    {
        public string Id { get; set; }
        public string PersonName { get; set; }
        public string PersonApartmentCode { get; set; }
        public Locations Location { get; set; }
        public Categories Category { get; set; }
        public string Description { get; set; }
        
    }
}
