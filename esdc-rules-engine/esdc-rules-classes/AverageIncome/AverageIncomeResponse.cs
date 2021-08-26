namespace esdc_rules_classes.AverageIncome
{
    public class AverageIncomeResponse : IResponse
    {
        /// <summary>
        /// Calculate value for the applicant's average weekly income. Used in the Maternity Benefits calculation
        /// </summary>
        /// <value></value>
        public decimal AverageIncome { get; set; }
    }
}