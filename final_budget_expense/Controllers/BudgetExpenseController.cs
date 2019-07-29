using final_budget_expense.Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final_budget_expense.Controllers
{
    public class BudgetExpenseController : Controller
    {
       BudgetExpenseEntities DB = new BudgetExpenseEntities();
       BudgetRecordViewModel VM = new BudgetRecordViewModel();

        public ActionResult Index(string month, string year)
        {
            int monthVal = 0;
            int yearVal = 0;

            if (String.IsNullOrEmpty(month) && String.IsNullOrEmpty(year))
            {
              monthVal  = (int)DateTime.Now.Month;
              yearVal = (int)DateTime.Now.Year;
            } 
            
            VM.Years = GetYears();
            VM.Months = GetMonthsByYear(yearVal);
            VM.Records = GetFilteredRecords(Convert.ToInt32(month), Convert.ToInt32(year));
            return View(VM);
        }


        public PartialViewResult GetTransactionPartial(string month, string year)
        {

            List<BudgetRecord> budgetRecord = GetFilteredRecords(Convert.ToInt32(month), Convert.ToInt32(year));
            return PartialView("_TransactionsPartial", budgetRecord);
        }

        public List<BudgetRecord> GetFilteredRecords(int month, int year)
        {
            List<BudgetRecord> budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Month == month && x.DateOfTrans.Year == year).ToList();
            return budgetRecord;
        }
   
       
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
         
        public List<string>  GetMonthsByYear(int year)
        {
            List<string> availableMonths = new List<string>();
            var monthConvert = CultureInfo.CurrentCulture.DateTimeFormat;

            var monthList = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year).DistinctBy(x => x.DateOfTrans.Month).Select(y => y.DateOfTrans.Month).ToList();
            //var monthList = DB.BudgetRecords.DistinctBy(x => x.DateOfTrans.Month).Select(x => x.DateOfTrans.Month).ToList();
            foreach (var item in monthList)
            {
                availableMonths.Add(monthConvert.GetMonthName(item));
            }
            return availableMonths;
        }    
    }
}

