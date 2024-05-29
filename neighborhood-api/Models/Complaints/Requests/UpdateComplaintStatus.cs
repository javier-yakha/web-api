using neighborhood_api.Models;

namespace neighborhood_api.Requests
{
    public class UpdateComplaintStatus()
    {
        public string Id { get; set; }
        public ActiveStatus CurrentStatus { get; set; }
        public DateTime DateDeActivated { get; set; }
    }
}
