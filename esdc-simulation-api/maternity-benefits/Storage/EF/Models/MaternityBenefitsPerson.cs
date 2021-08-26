using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace maternity_benefits.Storage.EF.Models
{
    [Table("MaternityBenefitsPerson")]
    public class MaternityBenefitsPerson
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string SpokenLanguage { get; set; }
        public string EducationLevel { get; set; }
        public string Province { get; set; }

        [Column(TypeName = "decimal(7, 2)")]
        public decimal AverageIncome { get; set; }
    }
}