using neighborhood_api.Models;

namespace neighborhood_api.Requests
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
