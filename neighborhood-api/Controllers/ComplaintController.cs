using Microsoft.AspNetCore.Mvc;
using neighborhood_api.DataServices;
using Models;
using Models.Complaints;
using Responses = Models.Complaints.Responses;
using Requests = Models.Complaints.Requests;

namespace neighborhood_api.Controllers
{
    public class ComplaintController : Controller
    {
        [Route("test/json")]
        [HttpGet]
        public async Task<ResponseStatus<object?>> TestHttpConnection()
        {
            await Task.Delay(0);

            return new ResponseStatus<object?>()
            {
                Code = 200,
                Success = true,
                Message = "Connection Established"
            };
        }

        [Route("complaint/create")]
        [HttpPost]
        public async Task<ResponseStatus<Responses.CreateComplaint>> CreateNewComplaintAsync([FromBody]Requests.CreateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            string serviceResult = await complaintService.CreateNewComplaintAsync(requestBody);
            bool serviceSuccess = !string.IsNullOrEmpty(serviceResult);
            return new ResponseStatus<Responses.CreateComplaint>()
            {
                Code = serviceSuccess ? 200 : 403,
                Message = serviceSuccess ? "Successfully created new complaint" : "Insertion Failed",
                Success = serviceSuccess,
                Data = new Responses.CreateComplaint { Id = serviceResult }
            };
        }

        [Route("complaint/update/data")]
        [HttpPost]
        public async Task<ResponseStatus<object?>> UpdateComplaintDataByComplaintIdAsync([FromBody]Requests.UpdateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            bool serviceResponse = await complaintService.UpdateComplaintDataByComplaintIdAsync(requestBody);

            return new ResponseStatus<object?>()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Successfully updated" : "Update failed",
                Success = serviceResponse
            };
        }

        [Route("complaint/update/status")]
        [HttpPost]
        public async Task<ResponseStatus<Responses.UpdatedStatus>> UpdateComplaintStatusByComplaintId([FromBody]Requests.UpdateComplaintStatus requestBody, [FromServices]ComplaintService complaintService)
        {
            DateTime dateTime = DateTime.UtcNow;

            bool serviceResponse = await complaintService.UpdateComplaintStatusByComplaintIdAsync(requestBody, dateTime);

            return new ResponseStatus<Responses.UpdatedStatus>()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Success" : "Update failed",
                Success = serviceResponse,
                Data = new Responses.UpdatedStatus(dateTime)
            };
        }

        [Route("complaint/read/all/bydate")]
        [HttpGet]
        public async Task<ResponseStatus<Responses.ComplaintList>> ReadAllComplaintsByDate([FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadAllComplaintsByDateAsync();

            return new ResponseStatus<Responses.ComplaintList>()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully retrieved complaints" : "Read Failed",
                Success = serviceResponse is not null,
                Data = new Responses.ComplaintList()
                {
                    Total = serviceResponse?.Count,
                    Complaints = serviceResponse ?? null
                },
            };
        }

        [Route("complaint/read/search")]
        [HttpGet]
        public async Task<ResponseStatus<Responses.ComplaintList>> ReadSearchComplaintByPersonName([FromQuery]string personName, [FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadSearchComplaintByPersonName(personName);

            return new()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully searched complaints" : "Search failed",
                Success = serviceResponse is not null,
                Data = new Responses.ComplaintList()
                {
                    Total = serviceResponse?.Count,
                    Complaints = serviceResponse ?? null
                },
            };
        }
    }
}
