namespace BackendChallange.Controllers
{
    using global::Services.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    [Route("api/users/{userId}/portfolioEntries")]
    [ApiController]
    public class PortfolioEntriesController : ControllerBase
    {
        private readonly ILogger<PortfolioEntriesController> logger;
        private readonly IPortfolioService portfolioService;

        public PortfolioEntriesController(
            ILogger<PortfolioEntriesController> logger, 
            IPortfolioService portfolioService)
        {
            this.logger = logger;
            this.portfolioService = portfolioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPortfolioEntries(int userId)
        {

            var portfolioEntities = await this.portfolioService.GetPortfolioEntities(userId);

            return StatusCode(200, portfolioEntities);
        }
    }
}