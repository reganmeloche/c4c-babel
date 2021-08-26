using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_rules_api.Lib;
using esdc_rules_classes.AverageIncome;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AverageIncomeController : ControllerBase
    {
        private readonly IHandleRequests<AverageIncomeRequest, AverageIncomeResponse> _requestHandler;
        private readonly ILogger<AverageIncomeController> _logger;

        public AverageIncomeController(
            IHandleRequests<AverageIncomeRequest, AverageIncomeResponse> requestHandler,
            ILogger<AverageIncomeController> logger)
        {
            _requestHandler = requestHandler;
            _logger = logger;
        }

        /// <summary>
        /// Calculate the Average Weekly income for an individual based on their records of employment and EI Application
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<AverageIncomeResponse> Calculate(AverageIncomeRequest request)
        {
            try {
                var result = _requestHandler.Handle(request);
                return Ok(result);
            } catch (ValidationException ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}
