using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using esdc_rules_api.Lib;
using esdc_rules_api.MaternityBenefits;
using esdc_rules_classes.MaternityBenefits;

namespace esdc_rules_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MaternityBenefitsController : ControllerBase
    {
        private readonly IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> _requestHandler;
        private readonly IHandleBulkRequests _bulkRequestHandler;
        private readonly ILogger<MaternityBenefitsController> _logger;

        public MaternityBenefitsController(
            IHandleRequests<MaternityBenefitsRequest, MaternityBenefitsResponse> requestHandler,
            IHandleBulkRequests bulkRequestHandler,
            ILogger<MaternityBenefitsController> logger)
        {
            _requestHandler = requestHandler;
            _bulkRequestHandler = bulkRequestHandler;
            _logger = logger;
        }

        /// <summary>
        /// Calculate the weekly Maternity Benefit entitlement amount given an encoded rule and an individual
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<MaternityBenefitsResponse> Calculate(MaternityBenefitsRequest request)
        {
            try {
                var result = _requestHandler.Handle(request);
                return Ok(result);
            } catch (ValidationException ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { error = ex.Message});
            }
        }
        
        /// <summary>
        /// Calculate the weekly Maternity Benefit entitlement amount given an encoded rule and a set of individuals
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Bulk")]
        public ActionResult<MaternityBenefitsBulkResponse> CalculateBulk(MaternityBenefitsBulkRequest request) {
            try {
                _logger.LogInformation("Bulk Request with Size {0}", request.Persons.Count);
                var result = _bulkRequestHandler.Handle(request);
                return Ok(result);
            } catch (ValidationException ex) {
                _logger.LogError(ex, ex.Message);
                return BadRequest(new { error = ex.Message});
            }
        }
    }
}
