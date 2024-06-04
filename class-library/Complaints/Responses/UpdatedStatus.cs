namespace Models.Complaints.Responses
{
    public class UpdatedStatus(DateTime dateTime)
    {
        public DateTime LastUpdated { get; set; } = dateTime;
    }
}
