namespace esdc_rules_classes.BestWeeks
{
    public class BestWeeksRequest : IRequest
    {
        /// <summary>
        /// Postal code of applicant
        /// </summary>
        /// <value></value>
        public string PostalCode { get; set; }
    }
}