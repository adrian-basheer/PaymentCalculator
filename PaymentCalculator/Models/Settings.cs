using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentCalculator.Models
{
    // structure of appsettings.json
    public class Settings
    {
        List<TaxBracket> _medicareLevy = [];
        List<TaxBracket> _budgetRepairLevy = [];
        List<TaxBracket> _incomeTax = [];

        public List<TaxBracket> MedicareLevy
        {
            get { return _medicareLevy; }
            set
            {
                if (value == null)
                {
                    _medicareLevy = [];
                }
                else
                {
                    _medicareLevy = [.. value.OrderByDescending(t => t.LowerBound)];
                }
            }
        }

        public List<TaxBracket> BudgetRepairLevy
        {
            get { return _budgetRepairLevy; }
            set
            {
                if (value == null)
                {
                    _budgetRepairLevy = [];
                }
                else
                {
                    _budgetRepairLevy = [.. value.OrderByDescending(t => t.LowerBound)];
                }
            }
        }
        public List<TaxBracket> IncomeTax
        {
            get { return _incomeTax; }
            set
            {
                if (value == null)
                {
                    _incomeTax = [];
                }
                else
                {
                    _incomeTax = [.. value.OrderByDescending(t => t.LowerBound)];
                }
            }
        }
    }
}
