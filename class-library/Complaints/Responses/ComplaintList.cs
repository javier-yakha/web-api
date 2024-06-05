namespace Models.Complaints.Responses
{
    public class ComplaintList()
    {
        public int? Total { get; set; }
        public List<Complaint>? Complaints { get; set; }
    }
}
