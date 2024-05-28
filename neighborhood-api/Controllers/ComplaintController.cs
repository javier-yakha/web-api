using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using neighborhood_api.Models;
using neighborhood_api.Models.Enums;
using neighborhood_api.DataServices;

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
        public async Task<bool> CreateNewComplaint([FromBody]CreateComplaint requestBody, [FromServices]ComplaintService complaintService)
        {
            CreateComplaint complaint = new()
            {
                PersonName = requestBody.PersonName,
                PersonApartmentCode = requestBody.PersonApartmentCode,
                Location = requestBody.Location,
                Category = requestBody.Category,
                Description = requestBody.Description,
            };
            
            return await complaintService.CreateNewComplaintAsync(complaint);
        }

        [Route("complaint/get")]
        [HttpGet]
        public async Task<JsonResult> ComplaintGet()
        {
            Complaint complaint = new()
            {
                Id = Guid.NewGuid().ToString(),
                PersonName = "Angry Person",
                PersonApartmentCode = "32A",
                Location = Locations.Garden,
                Category = Categories.Noise,
                Description = "People having too much fun",
                CurrentStatus = Status.Active,
                DateActivated = DateTime.Now,
                DateDeActivated = null
            };

            return Json(complaint);
        }
    }
}
