using LOND.API.Models;

namespace LOND.API.Interfaces
{
    public interface IFoodAnalyticsService
    {
        IEnumerable<string> GetFoodTypes();
        IEnumerable<string> GetSeasonalityFactors();
        FoodPredictionResponse Predict(FoodPredictionRequest request);
    }
}
