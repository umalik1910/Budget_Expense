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

        public ActionResult Index(string month, int? year)
        {
            int monthVal = 0;
            int yearVal = 0;

            if (String.IsNullOrEmpty(month) && year == null)
            {
              monthVal  = (int)DateTime.Now.Month;
              yearVal = (int)DateTime.Now.Year;
              VM.Records = GetFilteredRecords(monthVal, yearVal);
            }
            else
            {
                VM.Records = GetFilteredRecords(Convert.ToInt32(month), year);

            }
            
            VM.Years = GetYears();
            VM.Months = GetMonthsByYear(yearVal);
            
            return View(VM);
        }


        public PartialViewResult GetTransactionPartial(string month, int? year)
        {

            BudgetRecordViewModel budgetRecord = new BudgetRecordViewModel();
            int monthVal = 0;
            int yearVal = 0;
            if (String.IsNullOrEmpty(month) && year == null)
            {
                monthVal = (int)DateTime.Now.Month;
                yearVal = (int)DateTime.Now.Year;
                budgetRecord.Records = GetFilteredRecords(monthVal, yearVal);
                budgetRecord.Years = GetYears();
                budgetRecord.Months = GetMonthsByYear(yearVal);
            }
            else
            {
                monthVal = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                budgetRecord.Years = GetYears();
                budgetRecord.Months = GetMonthsByYear(year);
                budgetRecord.Records = GetFilteredRecords(monthVal, year);
            }
            
           
            return PartialView("_TransactionsPartial", budgetRecord);
        }

        public List<BudgetRecord> GetFilteredRecords(int month, int? year)
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

        public List<string>  GetMonthsByYear(int? year)
        {
            List<string> availableMonths = new List<string>();
            var monthConvert = CultureInfo.CurrentCulture.DateTimeFormat;

            var monthList = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year).DistinctBy(x => x.DateOfTrans.Month).Select(y => y.DateOfTrans.Month).ToList();
            foreach (var item in monthList)
            {
                availableMonths.Add(monthConvert.GetMonthName(item));
            }
            return availableMonths;
        }    
       
    }
}

