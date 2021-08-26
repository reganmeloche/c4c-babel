using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results
{
    public class OverallSummary
    {
        public List<PersonResultResponse> PersonResults { get; set; }

        public OverallSummary(List<PersonResultResponse> personResults) {
            PersonResults = personResults;
        }

        public int SampleSize { 
            get {
                return PersonResults.Count;
            } 
        }
        public double PercentChangeInCost {
            get {
                var baseCost = PersonResults.Sum(x => x.BaseAmount);
                var variantCost = PersonResults.Sum(x => x.VariantAmount);

                return GetPercentChange(baseCost, variantCost);
            }
        }
        
        // Percents
        public double PercentThatGained => ResultsSummary.GetPercentageOf(Gainers.Count(), SampleSize);
        public double PercentThatLost =>  ResultsSummary.GetPercentageOf(Losers.Count(), SampleSize);
        public double PercentUnchanged => ResultsSummary.GetPercentageOf(Neutral.Count(), SampleSize);

        // Averages
        public double AverageAmountGained => ResultsSummary.GetAverageAmountChange(Gainers);
        public double AveragePercentGained {
            get {
                var initAmount = Gainers.Sum(x => x.BaseAmount);
                var newAmount = Gainers.Sum(x => x.VariantAmount);
                return GetPercentChange(initAmount, newAmount);
            }
        }
        public double AverageAmountLost => ResultsSummary.GetAverageAmountChange(Losers);
        public double AveragePercentLost {
            get {
                var initAmount = Losers.Sum(x => x.BaseAmount);
                var newAmount = Losers.Sum(x => x.VariantAmount);
                return GetPercentChange(initAmount, newAmount);
            }
        }

        public double AverageAmountChange => ResultsSummary.GetAverageAmountChange(PersonResults);
        public double AveragePercentChange { 
            get {
                var initAmount = PersonResults.Sum(x => x.BaseAmount);
                var newAmount = PersonResults.Sum(x => x.VariantAmount);
                return GetPercentChange(initAmount, newAmount);
            }
        }

        
        // Private 
        private IEnumerable<PersonResultResponse> Gainers => PersonResults.Where(x => x.VariantAmount > x.BaseAmount);
        private IEnumerable<PersonResultResponse> Losers => PersonResults.Where(x => x.VariantAmount < x.BaseAmount);
        private IEnumerable<PersonResultResponse> Neutral => PersonResults.Where(x => x.VariantAmount == x.BaseAmount);
    
        private double GetPercentChange(decimal a, decimal b) {
            if (a == 0) {
                return 0;
            };

            return Convert.ToDouble((b - a) * 100 / Math.Abs(a));
        }
    }
}