using System.Collections.Generic;
using System.Linq;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results.Aggregations
{
    public static class EducationGroupings
    {
        public static MyAggregation Create(List<PersonResultResponse> personResults) {
            var educationGroupings = personResults
                .GroupBy(x => x.Person.EducationLevel)
                .Select(x => new MyGrouping(x.Key, x))
                .ToList();

            return new MyAggregation(educationGroupings);
        }
    }
}