using Microsoft.AspNetCore.Mvc;
using neighborhood_api.DataServices;
using Models;
using Models.Complaints;
using Responses = Models.Complaints.Responses;
using Requests = Models.Complaints.Requests;
using Models.Complaints.Responses;

namespace neighborhood_api.Controllers
{
    public class ComplaintController : Controller
    {
        [Route("test/json")]
        [HttpGet]
        public async Task<ResponseStatus<short?>> TestHttpConnection()
        {
            await Task.Delay(0);

            return new ResponseStatus<short?>()
            {
                Code = 200,
                Message = "Connection Established"
            };
        }

        [Route("complaint/create")]
        [HttpPost]
        public async Task<ResponseStatus<CreateComplaintStatus>> CreateNewComplaintAsync([FromBody]Requests.CreateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            string serviceResult = await complaintService.CreateNewComplaintAsync(requestBody);

            return new ResponseStatus<CreateComplaintStatus>()
            {
                Code = !string.IsNullOrEmpty(serviceResult) ? 200 : 403,
                Message = !string.IsNullOrEmpty(serviceResult) ? "Successfully created new complaint" : "Insertion Failed",
                Data = new CreateComplaintStatus { Id = serviceResult }
            };
        }

        [Route("complaint/update/data")]
        [HttpPost]
        public async Task<ResponseStatus<short?>> UpdateComplaintDataByComplaintIdAsync([FromBody]Requests.UpdateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            bool serviceResponse = await complaintService.UpdateComplaintDataByComplaintIdAsync(requestBody);

            return new ResponseStatus<short?>()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Successfully updated" : "Update failed"
            };
        }

        [Route("complaint/update/status")]
        [HttpPost]
        public async Task<ResponseStatus<UpdatedStatus>> UpdateComplaintStatusByComplaintId([FromBody]Requests.UpdateComplaintStatus requestBody, [FromServices]ComplaintService complaintService)
        {
            DateTime dateTime = DateTime.UtcNow;

            bool serviceResponse = await complaintService.UpdateComplaintStatusByComplaintIdAsync(requestBody, dateTime);

            return new ResponseStatus<UpdatedStatus>()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Success" : "Update failed",
                Data = new UpdatedStatus(dateTime)
            };
        }

        [Route("complaint/read/all/bydate")]
        [HttpGet]
        public async Task<ResponseStatus<ComplaintListStatus>> ReadAllComplaintsByDate([FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadAllComplaintsByDateAsync();

            return new ResponseStatus<ComplaintListStatus>()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully retrieved complaints" : "Read Failed",
                Data = new ComplaintListStatus()
                {
                    Total = serviceResponse?.Count,
                    Complaints = serviceResponse ?? null
                },
            };
        }

        [Route("complaint/read/search")]
        [HttpGet]
        public async Task<ResponseStatus<ComplaintListStatus>> ReadSearchComplaintByPersonName([FromQuery]string personName, [FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadSearchComplaintByPersonName(personName);

            return new()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully searched complaints" : "Search failed",
                Data = new ComplaintListStatus()
                {
                    Total = serviceResponse?.Count,
                    Complaints = serviceResponse ?? null
                },
            };
        }
    }
}
