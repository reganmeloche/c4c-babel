using System.Collections.Generic;
using System.Linq;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results.Aggregations
{
    public static class AgeGroupings
    {
        public static MyAggregation Create(List<PersonResultResponse> personResults) {
            // Groups
            var lessThan20 = new MyGrouping("0 - 20", personResults.Where(x => x.Person.Age < 20));
            var _20to25 = new MyGrouping("20 - 25", personResults.Where(x => x.Person.Age >= 20 && x.Person.Age < 25));
            var _25to30 = new MyGrouping("25 - 30", personResults.Where(x => x.Person.Age >= 25 && x.Person.Age < 30));
            var _30to35 = new MyGrouping("30 - 35", personResults.Where(x => x.Person.Age >= 30 && x.Person.Age < 35));
            var _35to40 = new MyGrouping("35 - 40", personResults.Where(x => x.Person.Age >= 35 && x.Person.Age < 40));
            var over40 = new MyGrouping("40+", personResults.Where(x => x.Person.Age >= 40));

            var ageGroupings = new List<MyGrouping>() {
                lessThan20,
                _20to25,
                _25to30,
                _30to35,
                _35to40,
                over40
            };

            return new MyAggregation(ageGroupings);
        }



    }
}