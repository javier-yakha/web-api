namespace neighborhood_api.Models.Complaints
{
    public class UpdateComplaintStatus
    {
        public string Id { get; set; }
        public Status CurrentStatus { get; set; }
        public DateTime DateDeActivated { get; set; }
    }
}
