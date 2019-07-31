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

        public ActionResult Index(string month, int? year, int id)
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
            VM.User = DB.UserInfoes.Single(x => x.UserID == id); 
            return View(VM);
        }


        public PartialViewResult GetTransactionPartial(string month, int? year, int? id)
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
                //budgetRecord.User = DB.UserInfoes.Single(x => x.UserID == id);
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
                budgetRecord.Records = GetFilteredRecords(monthVal, year);
            }


            if(id != null)
            {
                budgetRecord.User = DB.UserInfoes.Single(x => x.UserID == id);
            }
            
            return PartialView("_TransactionsPartial", budgetRecord);
        }

        public List<BudgetRecord> GetFilteredRecords(int month, int? year)
        {
            List<BudgetRecord> budgetRecord = new List<BudgetRecord>();
            
            if (DB.BudgetRecords.Any(x => x.DateOfTrans.Year == year))
            {

                if (DB.BudgetRecords.Where(y => y.DateOfTrans.Year == year).Any(x => x.DateOfTrans.Month == month))
                {
                    budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Month == month && x.DateOfTrans.Year == year).ToList();
                }
               
                else
                {
                    var firstMonth = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year).First();
                    budgetRecord.Add(firstMonth);
                    //budgetRecord = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year).ToList();


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

