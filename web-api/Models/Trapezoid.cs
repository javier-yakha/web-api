using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Trapezoid
    {
        public double BaseA { get; set; }
        public double BaseB { get; set; }
        public double Height { get; set; }
        public double Area { get; set; }
    }
}
