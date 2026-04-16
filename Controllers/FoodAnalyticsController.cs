using LOND.API.Auth;
using LOND.API.Interfaces;
using LOND.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace LOND.API.Controllers
{
    [ApiController]
    [ApiKeyAuthorize]
    [Route("api/[controller]")]
    public class FoodAnalyticsController : ControllerBase
    {
        private readonly IFoodAnalyticsService _foodAnalyticsService;

        public FoodAnalyticsController(IFoodAnalyticsService foodAnalyticsService)
        {
            _foodAnalyticsService = foodAnalyticsService;
        }

        /// <summary>
        /// Returns the list of available food types that can be selected for analysis.
        /// </summary>
        [HttpGet("FoodTypes")]
        public IActionResult GetFoodTypes()
        {
            var types = _foodAnalyticsService.GetFoodTypes();
            return Ok(types);
        }

        /// <summary>
        /// Returns the list of available seasonality factors.
        /// </summary>
        [HttpGet("SeasonalityFactors")]
        public IActionResult GetSeasonalityFactors()
        {
            var factors = _foodAnalyticsService.GetSeasonalityFactors();
            return Ok(factors);
        }

        /// <summary>
        /// Returns a demand index prediction over the requested time period
        /// for the given food type and seasonality factor.
        /// </summary>
        [HttpPost("Predict")]
        public IActionResult Predict([FromBody] FoodPredictionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!Enum.IsDefined(typeof(FoodType), request.FoodType))
            {
                return BadRequest("Invalid FoodType value.");
            }

            if (!Enum.IsDefined(typeof(SeasonalityFactor), request.SeasonalityFactor))
            {
                return BadRequest("Invalid SeasonalityFactor value.");
            }

            var result = _foodAnalyticsService.Predict(request);
            return Ok(result);
        }
    }
}
