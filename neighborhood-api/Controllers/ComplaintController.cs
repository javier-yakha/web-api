using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using neighborhood_api.DataServices;
using neighborhood_api.Models.Complaints;
using neighborhood_api.Models;

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
        public async Task<JsonResult> CreateNewComplaintAsync([FromBody]CreateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            Dictionary<string, string> result = new Dictionary<string, string>(2);

            CreateComplaint complaint = new()
            {
                PersonName = requestBody.PersonName,
                PersonApartmentCode = requestBody.PersonApartmentCode,
                Location = requestBody.Location,
                Category = requestBody.Category,
                Description = requestBody.Description,
            };

            string resp = await complaintService.CreateNewComplaintAsync(complaint);

            if (string.IsNullOrEmpty(resp))
            {
                result.Add("status", "insertion failed");
                result.Add("complaint Id", "");
                return Json(result);
            }

            result.Add("status", "success");
            result.Add("complaint Id", resp);

            return Json(result);
        }

        [Route("complaint/update/data")]
        [HttpPost]
        public async Task<JsonResult> UpdateComplaintDataByComplaintIdAsync([FromBody]UpdateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            Dictionary<string, string> result = new(2);

            UpdateComplaint complaint = new()
            {
                Id = requestBody.Id,
                PersonName = requestBody.PersonName,
                PersonApartmentCode = requestBody.PersonApartmentCode,
                Location = requestBody.Location,
                Category = requestBody.Category,
                Description = requestBody.Description,
            };

            bool status = await complaintService.UpdateComplaintDataByComplaintIdAsync(complaint);

            result.Add("complaint Id", $"{complaint.Id}");
            result.Add("status", status ? "success" : "update failed");

            return Json(result);
        }

        [Route("complaint/update/status")]
        [HttpPost]
        public async Task<JsonResult> UpdateComplaintStatusByComplaintId([FromBody]UpdateComplaintStatus requestBody, [FromServices]ComplaintService complaintService)
        {
            Dictionary<string, string> result = new(2);

            UpdateComplaintStatus complaint = new()
            {
                Id = requestBody.Id,
                CurrentStatus = (Status)requestBody.CurrentStatus
            };

            var resp = await complaintService.UpdateComplaintStatusByComplaintIdAsync(complaint);

            
            if (!resp.Item1 || resp.Item2 is null)
            {
                result.Add("status", "update failed");

                return Json(result);
            }

            result.Add("status", "success");
            result.Add("date deactivated", resp.Item2.ToString());

            return Json(result);
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
    }
}
