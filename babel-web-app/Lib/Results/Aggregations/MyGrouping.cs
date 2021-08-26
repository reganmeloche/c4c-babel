using System.Collections.Generic;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib.Results.Aggregations
{
    public class MyGrouping {
        public string Descriptor { get; set; }
        public IEnumerable<PersonResultResponse> PersonResults { get; set; }

        public MyGrouping(string descriptor, IEnumerable<PersonResultResponse> personResults) {
            Descriptor = descriptor;
            PersonResults = personResults;
        }
    }
}