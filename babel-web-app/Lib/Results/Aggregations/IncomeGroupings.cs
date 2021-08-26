using System.Collections.Generic;
using System.Linq;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results.Aggregations
{
    public static class IncomeGroupings
    {
        public static MyAggregation Create(List<PersonResultResponse> personResults) {
            // Groups
            var a = new MyGrouping("1. 0 - 500", personResults.Where(x => x.Person.AverageIncome < 500));
            var b = new MyGrouping("2. 500 - 1000", personResults.Where(x => x.Person.AverageIncome >= 500 && x.Person.AverageIncome < 1000));
            var c = new MyGrouping("3. 1000 - 1500", personResults.Where(x => x.Person.AverageIncome >= 100 && x.Person.AverageIncome < 1500));
            var d = new MyGrouping("4. 1500 - 2000", personResults.Where(x => x.Person.AverageIncome >= 1500 && x.Person.AverageIncome < 2000));
            var e = new MyGrouping("5. 2000+", personResults.Where(x => x.Person.AverageIncome >= 2000));

            var incomeGroupings = new List<MyGrouping>() {
                a,b,c,d,e
            };

            return new MyAggregation(incomeGroupings);
        }
    }
}