using LOND.API.Interfaces;
using LOND.API.Models;

namespace LOND.API.Services
{
    public class FoodAnalyticsService : IFoodAnalyticsService
    {
        // Base demand index (0–100) per food type representing average annual demand
        private static readonly Dictionary<FoodType, double> BaseDemand = new()
        {
            { FoodType.Vegetables, 75 },
            { FoodType.Fruits,     70 },
            { FoodType.Grains,     80 },
            { FoodType.Dairy,      65 },
            { FoodType.Meat,       60 },
            { FoodType.Seafood,    50 },
            { FoodType.Legumes,    55 },
            { FoodType.Nuts,       45 }
        };

        // Monthly seasonality multipliers [Jan..Dec] per SeasonalityFactor
        // Values represent relative demand strength; 1.0 = neutral
        private static readonly Dictionary<SeasonalityFactor, double[]> SeasonalityMultipliers = new()
        {
            {
                SeasonalityFactor.Spring,
                new[] { 0.70, 0.75, 0.90, 1.20, 1.35, 1.25, 1.10, 1.00, 0.95, 0.85, 0.75, 0.70 }
            },
            {
                SeasonalityFactor.Summer,
                new[] { 0.65, 0.65, 0.75, 0.90, 1.10, 1.35, 1.40, 1.30, 1.05, 0.85, 0.70, 0.65 }
            },
            {
                SeasonalityFactor.Autumn,
                new[] { 0.80, 0.75, 0.80, 0.85, 0.90, 0.95, 1.00, 1.10, 1.30, 1.35, 1.20, 1.00 }
            },
            {
                SeasonalityFactor.Winter,
                new[] { 1.35, 1.30, 1.10, 0.90, 0.75, 0.65, 0.65, 0.70, 0.80, 0.95, 1.15, 1.35 }
            },
            {
                SeasonalityFactor.YearRound,
                new[] { 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00, 1.00 }
            }
        };

        public IEnumerable<string> GetFoodTypes()
            => Enum.GetNames(typeof(FoodType));

        public IEnumerable<string> GetSeasonalityFactors()
            => Enum.GetNames(typeof(SeasonalityFactor));

        public FoodPredictionResponse Predict(FoodPredictionRequest request)
        {
            var baseIndex = BaseDemand[request.FoodType];
            var multipliers = SeasonalityMultipliers[request.SeasonalityFactor];

            var startDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
            var predictions = new List<FoodPredictionDataPoint>(request.MonthsAhead);

            for (int i = 0; i < request.MonthsAhead; i++)
            {
                var date = startDate.AddMonths(i);
                var monthIndex = date.Month - 1; // 0-based
                var multiplier = multipliers[monthIndex];

                // Apply a small linear trend (+0.5% per month) to simulate growth
                var trendFactor = 1 + (i * 0.005);
                var predictedValue = Math.Round(baseIndex * multiplier * trendFactor, 2);

                predictions.Add(new FoodPredictionDataPoint
                {
                    Month = date.ToString("MMM yyyy"),
                    PredictedDemandIndex = predictedValue
                });
            }

            return new FoodPredictionResponse
            {
                FoodType = request.FoodType.ToString(),
                SeasonalityFactor = request.SeasonalityFactor.ToString(),
                Predictions = predictions
            };
        }
    }
}
