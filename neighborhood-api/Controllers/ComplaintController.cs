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
        public async Task<JsonResult> ReadAllComplaintsByDate([FromServices]ComplaintService complaintService)
        {
            Dictionary<string, JsonResult> result = new(3);

            (bool, List<Complaint>) resp = await complaintService.ReadAllComplaintsByDateAsync();

            if (!resp.Item1)
            {
                result.Add("status", Json("read failed"));

                return Json(result);
            }

            result.Add("status", Json("success"));
            result.Add("total complaints", Json(resp.Item2.Count));
            result.Add("complaints", Json(resp.Item2));

            return Json(result);
        }

        [Route("complaint/read/search")]
        [HttpGet]
        public async Task<JsonResult> ReadSearchComplaintByPersonName([FromQuery]string personName, [FromServices]ComplaintService complaintService)
        {
            Dictionary<string, JsonResult> result = new(3);

            (bool, List<Complaint>) resp = await complaintService.ReadSearchComplaintByPersonName(personName);

            if (!resp.Item1)
            {
                result.Add("status", Json("read failed"));

                return Json(result);
            }

            result.Add("status", Json("Success"));
            result.Add("total found", Json(resp.Item2.Count));
            result.Add("complaints", Json(resp.Item2));

            return Json(result);
        }
    }
}
