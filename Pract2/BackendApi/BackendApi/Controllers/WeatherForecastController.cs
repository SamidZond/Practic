using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BackendApi.Controllers
{
    public class WeatherData
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public int Degree { get; set; }
        public string Location { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static List<WeatherData> weatherDatas = new()
        {
         new WeatherData() { Id = 1, Date = "21.01.2022", Degree=10, Location="Мурманск"} ,
            new WeatherData() { Id = 23, Date = "10.08.2019", Degree=20, Location="Пермь" },
            new WeatherData() { Id = 24, Date = "05.11.2020", Degree=15, Location="Омск" },
            new WeatherData() { Id = 25, Date = "07.02.2021", Degree=0, Location="Томск" },
            new WeatherData() { Id = 2, Date = "30.05.2022", Degree=3, Location="Калиниград" }
        };


        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<WeatherData> Get()
        {
            return weatherDatas;
        }

        [HttpPost]
        public IActionResult Add(WeatherData data)
        {
            if (data == null)
            {
                return BadRequest("Null name");
            }

            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    return BadRequest("Идентификатор уже существует");
                }
            }
            weatherDatas.Add(data);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(WeatherData data)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == data.Id)
                {
                    weatherDatas[i] = data;
                    return Ok();
                }
            }

            return BadRequest("ID не найден");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    weatherDatas.RemoveAt(i);
                    return Ok();
                }
            }

            return BadRequest("ID не найден");
        }

        [HttpGet("{id}")]
        public IActionResult GetName(int id)
        {
            if (id < 0 || id >= Summaries.Count)
            {
                return BadRequest("Меняй индекс");
            }

            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Id == id)
                {
                    return Ok(weatherDatas[i]);
                }
            }

            return BadRequest("Не нашел");
        }

        [HttpGet("find-by-city")]
        public IActionResult GetByCity(string location)
        {
            for (int i = 0; i < weatherDatas.Count; i++)
            {
                if (weatherDatas[i].Location == location)
                {
                    return Ok("Нашел!");
                }
            }

            return BadRequest("Не нашел!");
        }
    }
}