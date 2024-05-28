using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using web_api.Models;

namespace web_api.Controllers
{
    //[Route("/{Controller}/{Route}")]
    public class ExampleController : Controller
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [Route("first")]
        [HttpGet]
        public async Task<JsonResult> First()
        {
            return Json("First value");
        }

        [Route("math/sum")]
        [HttpGet]
        public async Task<double> MathSum(double a, double b)
        {
            return a + b;
        }
        [Route("math/sum")]
        [HttpPost]
        public async Task<double> MathSum([FromBody] ExamplePostModel requestData)
        {
            return requestData.a + requestData.b;
        }

        [Route("math/calculate")]
        [HttpPost]
        public async Task<Calculations> MathCalculate([FromBody] ExamplePostModel requestData)
        {
            Calculations calc = new();
            calc.Sum = requestData.a + requestData.b;
            calc.Multiply = requestData.a * requestData.b;

            return calc;
        }

        [Route("shapes/circle/circumference")]
        [HttpGet]
        public async Task<double> ShapesCircleCircumference([FromQuery]double radius)
        {
            return 2 * Math.PI * radius;
        }
        [Route("shapes/circle/circumference")]
        [HttpPost]
        public async Task<Circle> ShapesCircleCircumference([FromBody] ShapesModel requestData)
        {
            Circle circle = new Circle();
            circle.Radius = requestData.Radius;
            circle.Circumference = 2 * Math.PI * requestData.Radius;

            return circle;
        }

        [Route("shapes/cone/volume")]
        [HttpGet]
        public async Task<double> ShapesConeVolume(double radius, double height)
        {
            double surfaceArea = Math.PI * radius * radius;

            return (surfaceArea * height) / 3;
        }
        [Route("shapes/cone/volume")]
        [HttpPost]
        public async Task<Cone> ShapesConeVolume([FromBody] ShapesModel requestData)
        {
            Cone cone = new Cone();
            cone.Radius = requestData.Radius;
            cone.Height = requestData.Height;
            cone.Area = Math.PI * cone.Radius * cone.Radius;
            cone.Volume = (cone.Area * cone.Height) / 3;

            return cone;
        }

        [Route("shapes/trapezoid/area")]
        [HttpGet]
        public async Task<double> ShapesTrapezoidArea(double baseA, double baseB, double height)
        {
            double bases = baseA + baseB;

            return (bases * height) / 2;
        }
        [Route("shapes/trapezoid/area")]
        [HttpPost]
        public async Task<Trapezoid> ShapesTrapezoidArea([FromBody] ShapesModel requestData)
        {
            Trapezoid trapezoid = new Trapezoid();

            trapezoid.BaseA = requestData.BaseA;
            trapezoid.BaseB = requestData.BaseB;
            trapezoid.Height = requestData.Height;

            double bases = trapezoid.BaseA + trapezoid.BaseB;
            trapezoid.Area = (bases * trapezoid.Height) / 2;

            return trapezoid;
        }

    }
}
