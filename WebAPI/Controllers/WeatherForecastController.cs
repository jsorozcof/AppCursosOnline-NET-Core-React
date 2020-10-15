using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Persistencia;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    //http://localhost:5000/WeatherForecast
    public class WeatherForecastController : ControllerBase
    {

        private readonly CurosOnlineContext context;
        public WeatherForecastController(CurosOnlineContext _context)
        {
            this.context = _context;
        }


        [HttpGet]
        public IEnumerable<Curso> Get()
        {
            return context.Curso.ToList();
        }
    }
}
