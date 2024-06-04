namespace Models.Complaints.Responses
{
    public class ComplaintListStatus()
    {
        public int? Total { get; set; }
        public List<Complaint>? Complaints { get; set; }
    }
}
