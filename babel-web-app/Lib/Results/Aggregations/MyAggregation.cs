using System;
using System.Collections.Generic;
using System.Linq;

namespace babel_web_app.Lib.Results.Aggregations
{
    public class MyAggregation
    {
        private readonly List<MyGrouping> Groupings;

        public MyAggregation(List<MyGrouping> groupings) {
            Groupings = groupings;
        }

        public List<string> GroupKeys => Groupings.Select(x => x.Descriptor).ToList();
        public int GrandTotal => Groupings.Sum(x => x.PersonResults.Count());

        public Dictionary<string, int> TotalCountsDict {
            get {
                return Groupings.ToDictionary(x => x.Descriptor, x => x.PersonResults.Count());
            }
        }

        public Dictionary<string, int> TotalPercentsDict {
            get {
                return Groupings.ToDictionary(x => x.Descriptor, x => 100 * x.PersonResults.Count() / GrandTotal);
            }
        }
        
        // Percents
        public Dictionary<string, double> PercentThatGainedDict {
            get {
                return Groupings.ToDictionary(
                    x => x.Descriptor,
                    x => {
                        var gainers = x.PersonResults.Where(y => y.VariantAmount > y.BaseAmount);
                        return ResultsSummary.GetPercentageOf(gainers.Count(), x.PersonResults.Count());
                    }
                );
            }
        }
        public Dictionary<string, double> PercentThatLostDict {
            get {
                return Groupings.ToDictionary(
                    x => x.Descriptor,
                    x => {
                        var losers = x.PersonResults.Where(y => y.VariantAmount < y.BaseAmount);
                        return ResultsSummary.GetPercentageOf(losers.Count(), x.PersonResults.Count());
                    }
                );
            }
        }
        public Dictionary<string, double> PercentUnchangedDict {
            get {
                return Groupings.ToDictionary(
                    x => x.Descriptor,
                    x => {
                        var neutral = x.PersonResults.Where(y => y.VariantAmount == y.BaseAmount);
                        return ResultsSummary.GetPercentageOf(neutral.Count(), x.PersonResults.Count());
                    }
                );
            }
        }


        // Averages
        public Dictionary<string, double> AverageAmountChangeDict {
            get {
                return Groupings.ToDictionary(
                    x => x.Descriptor,
                    x => ResultsSummary.GetAverageAmountChange(x.PersonResults)
                );
                
            }
        }
        public Dictionary<string, double> AveragePercentChangeDict { 
            get {
                return Groupings.ToDictionary(
                    x => x.Descriptor,
                    x => {
                        var initAmount = x.PersonResults.Sum(y => y.BaseAmount);
                        var newAmount = x.PersonResults.Sum(y => y.VariantAmount);
                        return GetPercentChange(initAmount, newAmount);
                    }
                );
            }
        }

        private double GetPercentChange(decimal a, decimal b) {
            if (a == 0) {
                return 0;
            };

            return Convert.ToDouble((b - a) * 100 / Math.Abs(a));
        }
    }
}