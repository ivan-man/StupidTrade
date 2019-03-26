using AlphaVantageConnector.Enums;
using AlphaVantageConnector.Resources;
using AlphaVantageDto.Enums;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace AlphaVantageConnector.Helpers
{
    public static class PreDeserializationHelper
    {
        public static string ClearResponse(string response)
        {
            //remove numbers of fields
            var result = Regex.Replace(response, @"\""[0-9]{1,2}[\.:] ", @"""");

            if (response.Contains(AvResources.TimeSeries))
            {
                result = Regex.Replace(result, $@"({AvResources.TimeSeries} \([0-9a-zA-Z]+\))|([a-zA-Z]+ {AvResources.TimeSeries})", "TimeSeries");
                return result;
            }

            if (response.Contains(AvResources.GlobalQuote))
            {
                result = Regex.Replace(result, $@"([ %])", string.Empty);
                return result;
            }

            if (response.Contains(AvResources.TechnicalAnalysis))
            {
                var indicators = Enum.GetValues(typeof(TechnicalIndicators)) as TechnicalIndicators[];
                result = Regex.Replace(result, $@"({AvResources.TechnicalAnalysis}: ({string.Join("|", indicators)}))", "TechnicalAnalysis");
                return result;
            }

            //Sector
            if (response.Contains(AvResources.PerformanceRankA))
            {
                //We will adjust response and make from it good JSON
                var builder = new StringBuilder(result);

                builder.Replace($@"{AvResources.PerformanceRankA}"":", @"Performances"":{" + $@"""{PerfomanceRank.RankA_RealTime}"":");  //"Performances":{"RankA_RealTime":
                builder.Replace($@"""{AvResources.PerformanceRankB}"":", $@"""{PerfomanceRank.RankB_OneDay}"":");
                builder.Replace($@"""{AvResources.PerformanceRankC}"":", $@"""{PerfomanceRank.RankC_FiveDay}"":");
                builder.Replace($@"""{AvResources.PerformanceRankD}"":", $@"""{PerfomanceRank.RankD_OneMonth}"":");
                builder.Replace($@"""{AvResources.PerformanceRankE}"":", $@"""{PerfomanceRank.RankE_ThreeMonth}"":");
                builder.Replace($@"""{AvResources.PerformanceRankF}"":", $@"""{PerfomanceRank.RankF_YearToDate}"":");
                builder.Replace($@"""{AvResources.PerformanceRankG}"":", $@"""{PerfomanceRank.RankG_OneYear}"":");
                builder.Replace($@"""{AvResources.PerformanceRankH}"":", $@"""{PerfomanceRank.RankH_ThreeYear}"":");
                builder.Replace($@"""{AvResources.PerformanceRankI}"":", $@"""{PerfomanceRank.RankI_FiveYear}"":");
                builder.Replace($@"""{AvResources.PerformanceRankJ}"":", $@"""{PerfomanceRank.RankJ_TenYear}"":");
                builder.Replace("%", string.Empty);
                builder.Append("}");

                return builder.ToString();
            }


            return result;
        }
    }
}
