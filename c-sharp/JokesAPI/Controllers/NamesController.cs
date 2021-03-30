using System;
using System.Threading.Tasks;
using JokesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace JokesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NamesController : ControllerBase
    {
        private readonly ILogger<NamesController> _logger;
        private readonly INameServices _service;


        public NamesController(ILogger<NamesController> logger, INameServices service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                return await _service.GetNames();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
