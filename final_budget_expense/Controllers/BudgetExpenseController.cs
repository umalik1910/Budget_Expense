using final_budget_expense.Models;
using MoreLinq;
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
        BudgetRecordViewModel VM = new BudgetRecordViewModel();

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
   
        /*
        public List<int> GetYears()
        {
            var records = DB.BudgetRecords.DistinctBy(x => x.DateOfTrans.Year).ToList();
            List<int> availableYears = new List<int>();

            foreach(var item in records)
            {
                availableYears.Add(item.DateOfTrans.Year);
            }
            return availableYears;
        }
        public List<int>  GetMonthsByYear(List<int> years)
        {
            List<int> availableMonths = new List<int>();
            foreach (var item in years)
            {
                var month = DB.BudgetRecords.DistinctBy(x => x.DateOfTrans.Year == item).First().DateOfTrans.Month;
                availableMonths.Add(month); 
            }
            return availableMonths;
        }
        */
    }
}

