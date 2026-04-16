using System.ComponentModel.DataAnnotations;

namespace LOND.API.Models
{
    public class FoodPredictionRequest
    {
        [Required]
        public FoodType FoodType { get; set; }

        [Required]
        public SeasonalityFactor SeasonalityFactor { get; set; }

        /// <summary>
        /// Number of months ahead to predict (1–24). Defaults to 12.
        /// </summary>
        [Range(1, 24)]
        public int MonthsAhead { get; set; } = 12;
    }
}
