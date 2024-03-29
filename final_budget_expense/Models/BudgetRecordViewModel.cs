﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_budget_expense.Models
{
    public class BudgetRecordViewModel
    {

        public IEnumerable<BudgetRecord> Records { get; set; }
        public List<int> Years { get; set; }
        public List<string> Months { get; set; }
        public string SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }

        public UserInfo User { get; set; }

    }
}