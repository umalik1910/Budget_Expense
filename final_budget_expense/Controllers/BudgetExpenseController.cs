using final_budget_expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final_budget_expense.Controllers
{
    public class BudgetExpenseController : Controller
    {
        // GET: BudgetExpense
        public ActionResult Index()
        {
            BudgetExpenseEntities DB = new BudgetExpenseEntities();
            List<BudgetRecord> budgetRecord = DB.BudgetRecords.ToList();
            return View(budgetRecord);
        }
    }
}