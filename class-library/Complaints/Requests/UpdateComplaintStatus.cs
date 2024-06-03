using Models.Complaints;

namespace Models.Complaints.Requests
{
    public class UpdateComplaintStatus()
    {
        public string Id { get; set; }
        public ActiveStatus CurrentStatus { get; set; }
        public DateTime DateDeActivated { get; set; }
    }
}
