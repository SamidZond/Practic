using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BackendApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<string> Get()
        {
            return Summaries;
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (name.Length < 1)
            {
                return BadRequest("Write new name");
            }
            Summaries.Add(name);
            return Ok();

        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Неверный индекс");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpGet("{index}")]
        public string Get(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return "Bad index";
            }
            return Summaries[index];
        }


        [HttpGet("find-by-name")]
        public int GetString(string name)
        {
            return Summaries.Count(a => a == name);
        }


        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("Такой индекс неверный!!!!");
            }
            if (name == null)
            {
                return BadRequest("Null name");
            }
            Summaries[index] = name;
            return Ok();
        }

        [HttpGet("sort")]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy == null) return Ok(Summaries);
            if (sortStrategy == 1)
            {
                Summaries.Sort();
                return Ok(Summaries);
            }
            if (sortStrategy == -1)
            {
                Summaries.Sort();
                Summaries.Reverse();
                return Ok(Summaries);
            }
            else
            {
                return BadRequest("Неверное значение параметра sortStrategy");
            }



        }

    }
}