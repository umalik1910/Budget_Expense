using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_budget_expense.Models
{
    public class BudgetRecordModel
    {
        public int BudgetID { get; set; }
        public DateTime DateOfTrans { get; set; }
        public int UserID { get; set; }
        public string TypeOfTrans { get; set; }
        public string TransDescrption { get; set;}
        public float Amount { get; set; }
    }
}