namespace LOND.API.Models
{
    public class FoodPredictionDataPoint
    {
        public string Month { get; set; } = string.Empty;
        public double PredictedDemandIndex { get; set; }
    }

    public class FoodPredictionResponse
    {
        public string FoodType { get; set; } = string.Empty;
        public string SeasonalityFactor { get; set; } = string.Empty;
        public List<FoodPredictionDataPoint> Predictions { get; set; } = new();
    }
}
