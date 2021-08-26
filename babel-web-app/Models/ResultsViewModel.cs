using babel_web_app.Lib.Results;

namespace babel_web_app.Models
{
    public class ResultsViewModel
    {
        public ResultsSummary Results { get; set; }

        public ResultsViewModel(ResultsSummary results) {
            Results = results;
        }
    }
}