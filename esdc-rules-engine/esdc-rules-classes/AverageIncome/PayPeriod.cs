using System;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class PayPeriod {
        /// <summary>
        /// Pay period number
        /// </summary>
        /// <value></value>
        [Required]
        [Range(1,100)]
        public int PayPeriodNumber { get; set; }

        /// <summary>
        /// Amount earned during the pay period
        /// </summary>
        /// <value></value>
        [Required]
        [Range(0,Int32.MaxValue)]
        public decimal Amount { get; set; }
        
        public PayPeriod(int ppNumber, decimal amount) {
            PayPeriodNumber = ppNumber;
            Amount = amount;
        }
    }
}