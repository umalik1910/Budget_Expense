using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using final_budget_expense.Models;

namespace final_budget_expense.DAL
{
    public class UserContext : DbContext
    {
            public UserContext() : base("UserModel")
            {
            }
            public DbSet<UserModel> UserInfos { get; set; }
   }
}