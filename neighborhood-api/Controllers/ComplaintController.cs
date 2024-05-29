using Microsoft.AspNetCore.Mvc;
using neighborhood_api.DataServices;
using neighborhood_api.Models.Complaints;

namespace neighborhood_api.Controllers
{
    public class ComplaintController : Controller
    {
        [Route("test/json")]
        [HttpGet]
        public async Task<JsonResult> FirstJson()
        {
            return Json("test JSON");
        }
        [Route("test/string")]
        [HttpGet]
        public async Task<string> FirstString()
        {
            return "test string";
        }

        [Route("complaint/create")]
        [HttpPost]
        public async Task<Responses.CreateComplaintStatus> CreateNewComplaintAsync([FromBody]Requests.CreateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            Responses.Status status = new();

            string serviceResult = await complaintService.CreateNewComplaintAsync(requestBody);
            bool serviceStatus = string.IsNullOrEmpty(serviceResult);

            status.Code = serviceStatus ? 200 : 403;
            status.Message = serviceStatus ? "Successfully created new complaint" : "Insertion Failed";

            Responses.CreateComplaintStatus result = new(status);

            result.Id = serviceResult;

            return result;
        }

        [Route("complaint/update/data")]
        [HttpPost]
        public async Task<Responses.Status> UpdateComplaintDataByComplaintIdAsync([FromBody]Requests.UpdateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            bool serviceResponse = await complaintService.UpdateComplaintDataByComplaintIdAsync(requestBody);

            Responses.Status status = new()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Successfully updated" : "Update failed"
            };

            return status;
        }

        [Route("complaint/update/status")]
        [HttpPost]
        public async Task<Responses.UpdatedStatus> UpdateComplaintStatusByComplaintId([FromBody]Requests.UpdateComplaintStatus requestBody, [FromServices]ComplaintService complaintService)
        {
            DateTime dateTime = DateTime.UtcNow;

            bool serviceResponse = await complaintService.UpdateComplaintStatusByComplaintIdAsync(requestBody, dateTime);

            Responses.Status status = new()
            {
                Code = serviceResponse ? 200 : 403,
                Message = serviceResponse ? "Success" : "Update failed",
            };

            return new Responses.UpdatedStatus(status, dateTime);
        }

        [Route("complaint/read/all/bydate")]
        [HttpGet]
        public async Task<Responses.ComplaintListStatus> ReadAllComplaintsByDate([FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadAllComplaintsByDateAsync();

            Responses.Status status = new()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully retrieved complaints" : "Read Failed"
            };

            Responses.ComplaintListStatus results = new(status)
            {
                Total = serviceResponse?.Count,
                Complaints = serviceResponse ?? null
            };

            return results;
        }

        [Route("complaint/read/search")]
        [HttpGet]
        public async Task<Responses.ComplaintListStatus> ReadSearchComplaintByPersonName([FromQuery]string personName, [FromServices]ComplaintService complaintService)
        {
            List<Complaint>? serviceResponse = await complaintService.ReadSearchComplaintByPersonName(personName);

            Responses.Status status = new()
            {
                Code = serviceResponse is not null ? 200 : 403,
                Message = serviceResponse is not null ? "Successfully searched complaints" : "Search failed"
            };

            Responses.ComplaintListStatus results = new(status)
            {
                Total = serviceResponse?.Count,
                Complaints = serviceResponse ?? null
            };

            return results;
        }
    }
}
