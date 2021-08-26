using System;
using System.Collections.Generic;
using System.Linq;

using babel_web_app.Lib.Results.Aggregations;
using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results
{
    public class ResultsSummary
    {
        public Dictionary<string, MyAggregation> Aggregations { get; set; }
        public SimulationResponse Simulation { get; set; }
        public OverallSummary OverallSummary { get; set; }

        public ResultsSummary(SimulationResponse simulation, List<PersonResultResponse> personResults) {
            Simulation = simulation;
            
            OverallSummary = new OverallSummary(personResults);

            var ageAggregation = AgeGroupings.Create(personResults);
            var educationAggregation = EducationGroupings.Create(personResults);
            var incomeAggregation = IncomeGroupings.Create(personResults);
            var provinceAggregation = ProvinceGroupings.Create(personResults);

            Aggregations = new Dictionary<string, MyAggregation>() {
                { "Age", ageAggregation },
                { "Province", provinceAggregation },
                { "Education", educationAggregation },
                { "Income", incomeAggregation },
            };
        }

        public static double GetPercentChange(decimal a, decimal b) {
            if (a == 0) {
                return 0;
            };

            return Convert.ToDouble((b - a) * 100 / Math.Abs(a));
        }


        /*** Helper functions to handle divide by 0***/
        public static double GetPercentageOf(decimal n, decimal d) {
            if (d == 0) {
                return 0;
            }
            return Convert.ToDouble(n * 100 / Math.Abs(d));
        }

        public static double GetAverageAmountChange(IEnumerable<PersonResultResponse> persons) {
            try {
                return Convert.ToDouble(persons.Average(y => y.VariantAmount - y.BaseAmount));
            } catch (InvalidOperationException) {
                return 0;
            }
        }
    }
}