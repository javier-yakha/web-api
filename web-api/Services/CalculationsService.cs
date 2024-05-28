using System.ComponentModel;
using web_api.Models;

namespace web_api.Services
{
    public class CalculationsService
    {
        public async Task<List<Calculations>> Calculate()
        {
            List<Calculations> calcs = new();
            Random r = new();

            for (int i = 0; i < 10; i++)
            {
                Calculations calc = new();
                int first = r.Next(1, 10);
                int second = r.Next(1, 10);
                calc.Sum = first + second;
                calc.Multiply = first * second;
                calcs.Add(calc);
            }

            return calcs;
        }
    }
}
