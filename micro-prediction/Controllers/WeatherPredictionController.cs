using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;

namespace micro_prediction.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherPredictionController : ControllerBase
    {
        
        

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherPredictionController> _logger;

        public WeatherPredictionController(ILogger<WeatherPredictionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {

            HttpClient client = new HttpClient();
            var response = await client.GetAsync("http://localhost:5000/WeatherForecast");

            var objs = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(await response.Content.ReadAsStringAsync());
            
            var result = objs.ToList();

            foreach (WeatherForecast item in result)
            {
                item.TemperatureC *= 100;
            }

            return objs;

        }



        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

            public string Summary { get; set; }
        }
    }
}