using System;

namespace esdc_rules_api.AverageIncome
{
    public interface IGetIncomeForOneWeek
    {
        decimal Get(FullRoe roe, DateTime startOfWeek);
    }
}