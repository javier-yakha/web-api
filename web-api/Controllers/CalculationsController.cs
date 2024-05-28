using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Services;

namespace web_api.Controllers
{
    public class CalculationsController : Controller
    {
        [Inject]
        CalculationsService CalculationService { get; set; }

        public CalculationsController(CalculationsService calculationService) 
        {
            CalculationService = calculationService;
        }

        [Microsoft.AspNetCore.Mvc.Route("math/service")]
        [HttpGet]
        public async Task<List<Calculations>> Calculate([FromServices]CalculationsService CalculationService)
        {
            
            return await CalculationService.Calculate();
        }
    }
}
