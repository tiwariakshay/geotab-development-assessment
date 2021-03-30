using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JokesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JokesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokesController : ControllerBase
    {
        private readonly ILogger<JokesController> _logger;
        private readonly IJokesServices _service;


        public JokesController(ILogger<JokesController> logger, IJokesServices service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet("category")]
        public async Task<string> GetCategories()
        {
            try
            {
                return await _service.GetCategories();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> Get(string firstname, string lastname, string category, int number)
        {
            List<string> jokes = new List<string>();
            try
            {
                jokes = _service.GetJokes(firstname, lastname, category, number);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return jokes;
        }



    }
}
