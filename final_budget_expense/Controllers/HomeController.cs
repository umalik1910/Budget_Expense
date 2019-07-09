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
       
        public ActionResult Index()
        {
           
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
           
        
        
        public ActionResult BudgetSubmit([Bind(Include = "BudgetID, DateOfTrans, UserID, TypeOfTrans, TransDescription, Amount")] BudgetRecord budgetRecord)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    DB.BudgetRecords.Add(budgetRecord);
                    DB.SaveChanges();
                    return RedirectToAction("Home");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e); 
            }
            return View(budgetRecord);
        }
        public void AjaxPostCall(BudgetRecordModel budgetRecord)
        {
            BudgetRecordModel budget = new BudgetRecordModel
            {
                TypeOfTrans = budgetRecord.TypeOfTrans,
                DateOfTrans = budgetRecord.DateOfTrans,
                TransDescrption = budgetRecord.TransDescrption,
                Amount = budgetRecord.Amount

            };

        } 
    }

    
}



