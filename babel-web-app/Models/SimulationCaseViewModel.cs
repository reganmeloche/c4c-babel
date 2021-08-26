using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace babel_web_app.Models
{
    public class SimulationCaseViewModel
    {
        [DisplayName("Percentage")] 
        [Required(ErrorMessage = "PercentageRequired")]
        [Range(1, 100, ErrorMessage ="PercentageRange")]
        public double Percentage { get; set; }

        [DisplayName("Maximum Weekly Amount")] 
        [Required(ErrorMessage = "MaxWeeklyAmountRequired")]
        [Range(100, 2000, ErrorMessage = "MaxWeeklyAmountRange")]
        public decimal MaxWeeklyAmount { get; set; }

        [DisplayName("Number of Weeks")] 
        [Required(ErrorMessage = "NumWeeksRequired")]
        [Range(1, 52, ErrorMessage = "NumWeeksRange")]
        public int NumWeeks { get; set; }
    }
}