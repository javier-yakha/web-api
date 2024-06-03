namespace Models.Complaints.Responses
{
    public class UpdatedStatus(Status status, DateTime dateTime)
    {
        public Status Status { get; set; } = status;
        public DateTime LastUpdated { get; set; } = dateTime;
    }
}
