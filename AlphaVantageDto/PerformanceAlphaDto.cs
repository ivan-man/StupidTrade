using Newtonsoft.Json;

namespace AlphaVantageDto
{
    /// <summary>
    /// Perfomance of any Rank. 
    /// All values are percent.
    /// </summary>
    public class PerformanceAlphaDto
    {
        [JsonProperty(PropertyName = "Consumer Discretionary")]
        public float ConsumerDiscretionary { get; set; }

        [JsonProperty(PropertyName = "Industrials")]
        public float Industrials { get; set; }

        [JsonProperty(PropertyName = "Utilities")]
        public float Utilities { get; set; }

        [JsonProperty(PropertyName = "Real Estate")]
        public float RealEstate { get; set; }

        [JsonProperty(PropertyName = "Energy")]
        public float Energy { get; set; }

        [JsonProperty(PropertyName = "Consumer Staples")]
        public float ConsumerStaples { get; set; }

        [JsonProperty(PropertyName = "Health Care")]
        public float HealthCare { get; set; }

        [JsonProperty(PropertyName = "Materials")]
        public float Materials { get; set; }

        [JsonProperty(PropertyName = "Communication Services")]
        public float CommunicationServices { get; set; }

        [JsonProperty(PropertyName = "Financials")]
        public float Financials { get; set; }

        [JsonProperty(PropertyName = "Information Technology")]
        public float InformationTechnology { get; set; }
    }
}
