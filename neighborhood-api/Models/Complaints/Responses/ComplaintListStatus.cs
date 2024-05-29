using neighborhood_api.Models.Complaints;

namespace neighborhood_api.Responses
{
    public class ComplaintListStatus(Status status)
    {
        public Status Status { get; set; } = status;
        public int? Total {  get; set; }
        public List<Complaint>? Complaints { get; set; }
    }
}
