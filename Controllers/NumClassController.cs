using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NumberClassificationAPI__HNG_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumClassController : Controller
    {

        private readonly HttpClient _httpClient;
        public NumClassController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetNumClass([FromQuery] string number)
        {
            if (!int.TryParse(number, out int num)) 
            {
                return BadRequest(new { number, error = true });
            }
            var response = new
            {
                number = num,
                is_prime = IsPrime(num),
                is_perfect = IsPerfect(num),
                properties = GetNumberProperties(num),
                digit_sum = GetDigitSum(num),
                fun_fact = await GetFunFact(num)
            };

            return Ok(response);
        }

        private bool IsPrime(int num)
        {
            if (num < 2) return false;
            for (int i = 2; i * i <= num; i++)
            {
                if (num % i == 0) return false;
            }
            return true;
        }

        private bool IsPerfect(int num)
        {
            int sum = 1;
            for (int i = 2; i * i <= num; i++)
            {
                if (num % i == 0)
                {
                    sum += i;
                    if (i != num / i) sum += num / i;
                }
            }
            return sum == num && num != 1;
        }

        private string[] GetNumberProperties(int num)
        {
            var properties = new List<string>();

            if (IsArmstrong(num))
                properties.Add("armstrong");

            properties.Add(num % 2 == 0 ? "even" : "odd");

            return properties.ToArray();
        }

        private bool IsArmstrong(int num)
        {
            int sum = 0, temp = num, digits = num.ToString().Length;
            while (temp > 0)
            {
                int digit = temp % 10;
                sum += (int)Math.Pow(digit, digits);
                temp /= 10;
            }
            return sum == num;
        }

        private int GetDigitSum(int num)
        {
            int sum = 0;
            while (num > 0)
            {
                sum += num % 10;
                num /= 10;
            }
            return sum;
        }

        private async Task<string> GetFunFact(int num)
        {
            var url = $"http://numbersapi.com/{num}";
            try
            {
                return await _httpClient.GetStringAsync(url);
            }
            catch
            {
                return "Fun fact not available.";
            }
        }
    }

}