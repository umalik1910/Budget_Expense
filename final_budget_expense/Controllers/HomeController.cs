﻿using final_budget_expense.Models;
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
       // private BudgetRecordViewModel budgetRecordModel = new BudgetRecordViewModel();
        private UserInfo user = new UserInfo();
      public ActionResult Home(int id)
        {
            
            var user = DB.UserInfoes.Single(x => x.UserID == id);
            return View(user);
                
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
        public ActionResult BudgetSubmitAjaxPostCall(string budget_name, DateTime date_selection, string description, string expense_type, float amount_input, int user_id)
        {

            BudgetRecord budget = new BudgetRecord
            {
                UserID = user_id,
                DateOfTrans = date_selection,
                TypeOfTrans = budget_name,
                TransDescription = description,
                Amount = amount_input,
                ExpenseType = expense_type,
            };



            try
            {
                if (ModelState.IsValid)
                {

                    DB.BudgetRecords.Add(budget);
                    DB.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error" + e);
            }
            return Json(new { sucess = "true" });
        }

        public ActionResult CreateAccountAjaxPostCall(string first_name_input, string last_name_input, string username_input, string second_psw, string email_input)
        {
            UserInfo userInfo = new UserInfo

            {

                FirstName = first_name_input,
                LastName = last_name_input,
                UserName = username_input,
                EmailAddress = email_input,
                Password = second_psw,


            };
            if (DB.UserInfoes.Any(x => x.UserName == username_input && x.Password == second_psw))
            {
                return Json("Existing");
            }
            else
            {


                try
                {
                    if (ModelState.IsValid)
                    {

                        DB.UserInfoes.Add(userInfo);
                        DB.SaveChanges();

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("error" + e);
                }
                return Json("Success");

            }
        }

        public ActionResult LoginCheck(string username_input, string password_input)
        {
            //var user = DB.UserInfoes?.FirstOrDefault(x => x.UserName == username_input) ?? new UserInfo();
            var user = DB.UserInfoes?.SingleOrDefault(x => x.UserName == username_input && x.Password == password_input);
            if (user == null)
            {
                return Json("Login Unsuccessful");
            }
            else
            {
                if (user != null)
                {
                    if (user.UserName == username_input && user.Password == password_input)
                    {
                        return Json(new { redirectTo = Url.Action("Home", "Home", new { id = user.UserID }) });
                    }
                    else
                    {
                        return Json("Error");
                    }
                }
                else
                {
                    return Json("Error");
                }
            }

        }
    }
}





