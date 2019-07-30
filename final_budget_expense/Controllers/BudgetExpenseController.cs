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
              VM.Records = GetFilteredRecords(monthVal, yearVal, VM.SelectedMonth);
            }
            else
            {
                VM.Records = GetFilteredRecords(Convert.ToInt32(month), year, VM.SelectedMonth);

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
                budgetRecord.Records = GetFilteredRecords(monthVal, yearVal, budgetRecord.SelectedMonth);
                budgetRecord.Years = GetYears();
                budgetRecord.Months = GetMonthsByYear(yearVal);
                //budgetRecord.SelectedMonth = monthVal;
                //budgetRecord.SelectedYear = yearVal;
            }
            else
            {
                monthVal = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                budgetRecord.Years = GetYears();
                budgetRecord.Months = GetMonthsByYear(year);
                budgetRecord.SelectedMonth = month;
                budgetRecord.SelectedYear = year;
                budgetRecord.Records = GetFilteredRecords(monthVal, year, budgetRecord.SelectedMonth);
                
            }
           
            
            
           
            return PartialView("_TransactionsPartial", budgetRecord);
        }

        public List<BudgetRecord> GetFilteredRecords(int month, int? year, string firstMonth)
        {
            List<BudgetRecord> budgetRecord = new List<BudgetRecord>();
            int firstMonth2 = 0;
            if (!String.IsNullOrEmpty(firstMonth))
            {
               firstMonth2 = DateTime.ParseExact(firstMonth, "MMMM", CultureInfo.InvariantCulture).Month;

            }
           
            if (DB.BudgetRecords.Any(x => x.DateOfTrans.Year == year))
            {

                if (DB.BudgetRecords.Where(y => y.DateOfTrans.Year == year).Any(x => x.DateOfTrans.Month == month))
                {
                    budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Month == month && x.DateOfTrans.Year == year).ToList();
                }
                else if(DB.BudgetRecords.Where(y => y.DateOfTrans.Year == year).Any(x => x.DateOfTrans.Month == firstMonth2))
                {
                    budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Month == firstMonth2 && x.DateOfTrans.Year == year).ToList();
                }
                else
                {
                    
                    budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year).ToList();
                   
                    //return the new month and year here
                }
            }
           
            
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

