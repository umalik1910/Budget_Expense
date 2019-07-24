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
        BudgetExpenseEntities DB = new BudgetExpenseEntities();
        // GET: BudgetExpense
        /* public ActionResult Index()
         {

             List<BudgetRecord> budgetRecord = DB.BudgetRecords.ToList();
             return View(budgetRecord);
         }*/


        public ActionResult Index(string month)
        {
            if (String.IsNullOrEmpty(month))
            {
                month = DateTime.Now.Month.ToString();
            }

            List<BudgetRecord> budgetData = GetFilteredRecords(Convert.ToInt32(month));
            return View(budgetData);
        }


        public PartialViewResult GetTransactionPartial(string month)
        {

            var budgetRecord = GetFilteredRecords(Convert.ToInt32(month));
            return PartialView("_TransactionsPartial", budgetRecord);
        }

        public List<BudgetRecord> GetFilteredRecords(int month)
        {
            List<BudgetRecord> budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Month == month).ToList();

            return budgetRecord;
        }

    }
}