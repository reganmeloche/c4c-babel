using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class AverageIncomeRequest : IRequest
    {
        /// <summary>
        /// Record of Employment information
        /// </summary>
        /// <value></value>
        [Required]
        public SimpleRoe Roe { get; set; }

        /// <summary>
        /// Application date for Maternity Benefits
        /// </summary>
        /// <value></value>
        [Required]
        public DateTime ApplicationDate { get; set;}

        /// <summary>
        /// Number of best weeks to be used when calculating average income
        /// </summary>
        /// <value></value>
        [Required]
        [Range(1,52)]
        public int NumBestWeeks { get; set; }
    }
}