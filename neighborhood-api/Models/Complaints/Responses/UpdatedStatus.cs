namespace neighborhood_api.Responses
{
    public class UpdatedStatus(Status status, DateTime dateTime)
    {
        public Status Status { get; set; } = status;
        public DateTime LastUpdatedDate { get; set; } = dateTime;
    }
}
