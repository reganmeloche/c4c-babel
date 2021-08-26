using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace esdc_rules_classes.AverageIncome
{
    public class BaseRoe
    {
        /// <summary>
        /// How often the applicant was paid
        /// Possible values: weekly, bi-weekly, semi-monthly, monthly
        /// </summary>
        /// <value></value>
        [Required]
        public string PayPeriodType { get; set; }

        /// <summary>
        /// Last day that the applicant was actually paid
        /// </summary>
        /// <value></value>
        [Required]
        public DateTime LastDayForWhichPaid { get; set; }

        /// <summary>
        /// Final day of the last pay period of the RoE, regardless of whether they were paid or not
        /// </summary>
        /// <value></value>
        [Required]
        public DateTime FinalPayPeriodDay { get; set; }

        /// <summary>
        /// First day that the applicant was paid on for the RoE
        /// </summary>
        /// <value></value>
        [Required]
        public DateTime FirstDayForWhichPaid { get; set; }
        
    }

    public class SimpleRoe : BaseRoe {
        public List<PayPeriod> PayPeriods { get; set; }
    }
}