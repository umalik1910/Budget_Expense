using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace final_budget_expense.Models
{
    public class UserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public int UserID { get; set; }
    }
}