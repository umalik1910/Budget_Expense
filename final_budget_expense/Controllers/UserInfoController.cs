using final_budget_expense.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace final_budget_expense.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: UserInfo
        public ActionResult Index()
        {
            BudgetExpenseEntities DB = new BudgetExpenseEntities();
            List<UserInfo> userInfo = DB.UserInformation.ToList();
            return View();
        }
    }
}