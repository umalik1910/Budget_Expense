using final_budget_expense.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace budget_expense.Controllers
{
    public class HomeController : Controller
    {
        private BudgetExpenseEntities DB = new BudgetExpenseEntities();
        private BudgetRecord budgetRecordModel = new BudgetRecord();
       
        public ActionResult Index()
        {

            BudgetExpenseEntities DB = new BudgetExpenseEntities();
            List<BudgetRecord> budgetRecord = DB.BudgetRecords.ToList();
            return View(budgetRecord);

        }

        public ActionResult Home()
        {
            
            return View();

        }
        public ActionResult SignIn()
        {

            return View();
        }
        public ActionResult CreateAccount()
        {

            return View();

        }

        public ActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }



           [HttpPost]
           public ActionResult AjaxPostCall (string budget_name, DateTime date_selection, string description, string expense_type, float amount_input)
           {
            
            BudgetRecord budget = new BudgetRecord
            {
                UserID = 1, 
                BudgetID = 1,
                DateOfTrans = date_selection,
                TypeOfTrans = budget_name,
                TransDescription = description,
                Amount = amount_input,
                ExpenseType = expense_type,
                /* TypeOfTrans = budgetRecord.TypeOfTrans,
                DateOfTrans = budgetRecord.DateOfTrans,
                TransDescrption = budgetRecord.TransDescrption,
                Amount = budgetRecord.Amount */

            };

           

            try
            {
                if (ModelState.IsValid)
                {

                    DB.BudgetRecords.Add(budget);
                    DB.SaveChanges();
                    //RedirectToAction("Home");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("error" + e);
            }
            return Json(new { sucess = "true" });
           } 
    }

    
}



