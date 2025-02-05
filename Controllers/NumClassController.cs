using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace NumberClassificationAPI__HNG_.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NumClassController : Controller
    {

        //private readonly HttpClient _httpClient;
        //public NumClassController(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;

        public NumClassController(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
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

        //private bool IsPrime(int num)
        //{
        //    if (num < 2) return false;
        //    for (int i = 2; i * i <= num; i++)
        //    {
        //        if (num % i == 0) return false;
        //    }
        //    return true;
        //}

        private bool IsPrime(int num)
        {
            if (num < 2) return false;
            if (num == 2 || num == 3) return true;
            if (num % 2 == 0) return false;

            for (int i = 3; i * i <= num; i += 2)
            {
                if (num % i == 0) return false;
            }
            return true;
        }





        private bool IsPerfect(int num)
        {
            int sum = 1;
            int sqrt = (int)Math.Sqrt(num);
            for (int i = 2; i <= sqrt; i++)
            {
                if (num % i == 0)
                {
                    sum += i + (num / i);
                }
            }
            return sum == num && num != 1;
        }


        //private bool IsPerfect(int num)
        //{
        //    int sum = 1;
        //    for (int i = 2; i * i <= num; i++)
        //    {
        //        if (num % i == 0)
        //        {
        //            sum += i;
        //            if (i != num / i) sum += num / i;
        //        }
        //    }
        //    return sum == num && num != 1;
        //}







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
            int originalNum = num;
            num = Math.Abs(num);
            double sum = 0;
            var str_num = num.ToString();
            var len = str_num.Length;
            while (num > 0)
            {
                sum += Math.Pow((num % 10), len);
                num /= 10;
            }
            return sum == Math.Abs(originalNum);
        }

        private int GetDigitSum(int num)
        {
            num = Math.Abs(num); // Convert negative numbers to positive
            int sum = 0;

            while (num > 0)
            {
                sum += num % 10;
                num /= 10;

            }
            return sum;
        }

        //private async Task<string> GetFunFact(int num)
        //{
        //    var url = $"http://numbersapi.com/{num}";
        //    try
        //    {
        //        return await _httpClient.GetStringAsync(url);
        //    }
        //    catch
        //    {
        //        return "Fun fact not available.";
        //    }
        //}

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

        //private async Task<string> GetFunFact(int num)
        //{
        //    {
        //    }

        //    var url = $"http://numbersapi.com/{num}";
        //    try
        //    {
        //        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(300)); // Set timeout limit
        //        if (_cache.TryGetValue(num, out string funFact))
        //        {

        //        }
        //            string fact = await _httpClient.GetStringAsync(url, cts.Token);
        //        _cache.Set(num, fact, TimeSpan.FromMinutes(10)); // Cache for 10 minutes
        //        return fact;
        //    }

        //    catch
        //    {
        //        return "Fun fact not available.";
        //    }
        //}

    }

}




//try
//{
//    funFact = await _httpClient.GetStringAsync(url);
//    _cache.Set(num, funFact, TimeSpan.FromHours(1)); // Cache for 1 hour
//    return funFact;
//}