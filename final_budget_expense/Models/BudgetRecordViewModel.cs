using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_budget_expense.Models
{
    public class BudgetRecordViewModel
    {
        public BudgetRecord Records { get; set; }
       public List<int> Years { get; set; }
        public List<int> Months { get; set; }


    }
}