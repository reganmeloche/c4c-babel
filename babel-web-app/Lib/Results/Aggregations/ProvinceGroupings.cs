using System.Collections.Generic;
using System.Linq;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results.Aggregations
{
    public static class ProvinceGroupings
    {
        public static MyAggregation Create(List<PersonResultResponse> personResults) {
            var provinceGroupings = personResults
                .GroupBy(x => x.Person.Province)
                .Select(x => new MyGrouping(x.Key, x))
                .ToList();

            return new MyAggregation(provinceGroupings);
        }
    }
}