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
        //public static int one = 0;
        public ActionResult Index(string month, int? year, int id)
        {
            int yearVal = 0;
            int monthVal = 0;
            int userID = DB.UserInfoes.Single(x => x.UserID == id).UserID;
            VM.User = DB.UserInfoes.Single(x => x.UserID == id);
            if (String.IsNullOrEmpty(month) && year == null)
            {
                yearVal = (int)DateTime.Now.Year;
                VM.Records = GetFilteredRecords(monthVal, yearVal, userID);
            }
            else
            {
                VM.Records = GetFilteredRecords(Convert.ToInt32(month), year, userID);

            }

            VM.Years = GetYears(userID);
            VM.Months = GetMonthsByYear(yearVal, userID);
            return View(VM);
        }


        public PartialViewResult GetTransactionPartial(string month, int? year, int? id)
        {

            BudgetRecordViewModel budgetRecord = new BudgetRecordViewModel();
            int monthVal = 0;
            int yearVal = 0;
            if (String.IsNullOrEmpty(month) && year == null || id == null)
            {
                yearVal = (int)DateTime.Now.Year;
                budgetRecord.Records = GetFilteredRecords(monthVal, yearVal, 0);
                budgetRecord.Years = GetYears(0);
                budgetRecord.Months = GetMonthsByYear(yearVal, 0);
                //budgetRecord.User = DB.UserInfoes.Single(x => x.UserID == id);
                //budgetRecord.SelectedMonth = monthVal;
                //budgetRecord.SelectedYear = yearVal;
            }
            else
            {
                monthVal = DateTime.ParseExact(month, "MMMM", CultureInfo.InvariantCulture).Month;
                budgetRecord.Years = GetYears(id);
                budgetRecord.Months = GetMonthsByYear(year, id);
                budgetRecord.SelectedMonth = month;
                budgetRecord.SelectedYear = year;
                budgetRecord.Records = GetFilteredRecords(monthVal, year, id);
            }


            if (id != null)
            {
                budgetRecord.User = DB.UserInfoes.Single(x => x.UserID == id);
            }
             
            return PartialView("_TransactionsPartial", budgetRecord);
        }
        public List<BudgetRecord> GetFilteredRecords(int? month, int? year, int? userId)
        {
            List<BudgetRecord> budgetRecord = new List<BudgetRecord>();
            var userRecords = DB.BudgetRecords.Where(y => y.DateOfTrans.Year == year && y.UserID == userId);
            var firstMonth = userRecords.FirstOrDefault()?.DateOfTrans.Month ?? 1;

            if(month == 0)
            {
                month = firstMonth;
            }

            if (userRecords.Any(x => x.DateOfTrans.Year == year && x.DateOfTrans.Month == month) && userId != 0)
            {
                budgetRecord = userRecords.Where(x => x.DateOfTrans.Month == month).ToList();
            }
            else
            {
                budgetRecord = userRecords.Where(x => x.DateOfTrans.Month == firstMonth).ToList();
            }

            return budgetRecord;
        }
        public List<int> GetYears(int? userID)
        {
            var records = DB.BudgetRecords.Where(x => x.UserID == userID).DistinctBy(y => y.DateOfTrans.Year).ToList();
            List<int> availableYears = new List<int>();

            foreach(var item in records)
            {
                availableYears.Add(item.DateOfTrans.Year);
            }
            return availableYears;
        }

        public List<string>  GetMonthsByYear(int? year, int? userID)
        {
            List<string> availableMonths = new List<string>();
            var monthConvert = CultureInfo.CurrentCulture.DateTimeFormat;

            var monthList = DB.BudgetRecords.Where(x => x.DateOfTrans.Year == year && x.UserID == userID).DistinctBy(x => x.DateOfTrans.Month).Select(y => y.DateOfTrans.Month).ToList();
            foreach (var item in monthList)
            {
                availableMonths.Add(monthConvert.GetMonthName(item));
            }
            return availableMonths;
        }    
       
    }
}

