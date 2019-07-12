using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using final_budget_expense.Models;

namespace final_budget_expense.DAL
{
  
        public class BudgetRecordModelContext : DbContext
        {
            public BudgetRecordModelContext() : base("BudgetRecordModel")
            {
            }

            public DbSet<Models.BudgetRecordModel> BudgetRecords { get; set; }
            
        }
}
