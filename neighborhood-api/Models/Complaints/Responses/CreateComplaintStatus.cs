﻿namespace neighborhood_api.Responses
{
    public class CreateComplaintStatus(Status status)
    {
        public Status Status { get; set; } = status;
        public string? Id { get; set; }
    }
}
