using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace BackendChallange.Controllers
{
    [Route("api/users/{userId}/coinValues")]
    [ApiController]
    public class CoinValuesController : ControllerBase
    {
        private readonly ILogger<CoinValuesController> logger;
        private readonly ICoinValueService coinValueService;

        public CoinValuesController(ILogger<CoinValuesController> logger, ICoinValueService coinValueService)
        {
            this.logger = logger;
            this.coinValueService = coinValueService;
        }

        [HttpGet("{coin}")]
        public async Task<IActionResult> GetCoinValues(int userId, string coin, DateTime? from, DateTime? to)
        {
            var coinValues = await this.coinValueService.GetCounValues(userId, coin, from, to);

            return StatusCode(200, coinValues);
        }
    }
}