using System.ComponentModel.DataAnnotations;

namespace babel_web_app.Models
{
    public class SimulationFormViewModel
    {
        public SimulationCaseViewModel BaseCase { get; set; }
        public SimulationCaseViewModel VariantCase { get; set; }
        [Required]
        public string SimulationName { get; set; }

        public SimulationFormViewModel(SimulationCaseViewModel baseCase, SimulationCaseViewModel variantCase, string name) {
            BaseCase = baseCase;
            VariantCase = variantCase;
            SimulationName = name;
        }

        public SimulationFormViewModel() {
        }

    }
}