using AlphaVantageDto.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaVantageDto
{
    public class PerformanceDto : PerformanceAlphaDto
    {
        public PerfomanceRank PerfomanceRank { get; set; }

        public static explicit operator PerformanceDto(KeyValuePair<PerfomanceRank, PerformanceAlphaDto> pair)
        {
            return new PerformanceDto
            {
                PerfomanceRank = pair.Key,
                ConsumerDiscretionary = pair.Value.ConsumerDiscretionary,
                Industrials = pair.Value.Industrials,
                Utilities = pair.Value.Utilities,
                RealEstate = pair.Value.RealEstate,
                Energy = pair.Value.Energy,
                ConsumerStaples = pair.Value.ConsumerStaples,
                HealthCare = pair.Value.HealthCare,
                Materials = pair.Value.Materials,
                CommunicationServices = pair.Value.CommunicationServices,
                Financials = pair.Value.Financials,
                InformationTechnology = pair.Value.InformationTechnology,
            };
        }
    }
}
