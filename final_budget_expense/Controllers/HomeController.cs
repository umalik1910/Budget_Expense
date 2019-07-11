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
           
            List<BudgetRecordModel> budgetRecord = DB.BudgetRecords.ToList();
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
           public ActionResult AjaxPostCall (BudgetRecordModel budgetRecord)
           {
               BudgetRecordModel budget = new BudgetRecordModel
               {
                   TypeOfTrans = budgetRecord.TypeOfTrans,
                   DateOfTrans = budgetRecord.DateOfTrans,
                   TransDescrption = budgetRecord.TransDescrption,
                   Amount = budgetRecord.Amount

               };

            try
            {
                if (ModelState.IsValid)
                {

                    DB.BudgetRecords.Add(budget);
                    DB.SaveChanges();
                    RedirectToAction("Home");
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



