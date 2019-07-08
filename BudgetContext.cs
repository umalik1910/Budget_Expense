using System;
using BudgetRecordModel.Models;
using System.Data.Entity;
using System.Data.Entity.DbContext;

namespace final_budget_expense.DAL
{
    public class BudgetContext : DbContext
    {
        public BudgetContext() : base("BudgetRecordModel")
        {
        }

        public DbSet<BudgetID> BudgetIDs { get; set; }
        public DbSet<DateOfTrans> DateOfTrans { get; set; }
        public DbSet<UserID> UserIDs { get; set; }
        public DbSet<TypeOfTrans> TypeOfTranss { get; set; }
        public DbSet<TransDescription> TransDescriptions { get; set; }
        public DbSet<Amount> Amounts { get; set; }
    }
}
