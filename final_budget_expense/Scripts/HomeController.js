function showCategories() {
    var x = document.getElementsByClassName("expense_type")[0];
    var y = document.getElementById("budget_name").value;
    var z = document.getElementsByClassName("expense_type")[1];
    if (y == "Expense") {
        x.style.display = "block";
        z.style.display = "block";
    }
    else {
        x.style.display = "none";
        z.style.display = "none";
    }
}
function showCurrentDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }
    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;

    return today;

}
if (document.getElementById("date_selection") != null) {
    document.getElementById("date_selection").onclick = callCurrentDate();
}

function callCurrentDate() {
    var input = document.getElementById("date_selection");
    if (input != null) {
        input.setAttribute("max", showCurrentDate());
    }
    //input.max = showCurrentDate();  
};

function checkPassword(pass1, pass2) {
    if (pass1 === "" && pass2 === "" || pass1 !== pass2) {
        return false;
    }
    return true;
};
/*function submitDetails() {
    var password1 = document.getElementById("first_psw").value;
    var password2 = document.getElementById("second_psw").value;
    if (password1 && password2 != null) {
        if (checkPassword(password1, password2)) {
            //Save
            alert("Passwords match.");
            

        } else {
            //throw error
            alert("Passwords are not matching or the boxes are blank!");
        }
    }
}*/

$("#create_account_submit_button").click(function () {

    var password1 = document.getElementById("first_psw").value;
    var password2 = document.getElementById("second_psw").value;
    if (checkPassword(password1, password2)) {
        //Save
        alert("Passwords match.");
        {
            $.ajax({
                type: "POST",
                url: "/Home/CreateAccountAjaxPostCall",
                data: JSON.stringify({

                    'first_name_input': $('#first_name_input').val(),
                    'last_name_input': $('#last_name_input').val(),
                    'username_input': $('#username_input').val(),
                    'second_psw': $('#second_psw').val(),
                    'email_input': $('#email_input').val(),
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == null) {
                        alert("Something went wrong");
                    }
                    var url = 'Home';
                    window.location.href = url;
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }

    } else {
        //throw error
        alert("Passwords are not matching or the boxes are blank!");
    }
    
});

/*function directToHome() {
    var password1 = document.getElementById("first_psw").value;
    var password2 = document.getElementById("second_psw").value;
    if (password1 && password2 != null) {
        if (checkPassword(password1, password2)) {
            var url = 'Home';
            window.location.href = url;
            
        }
    }*/

   /* $("#create_account_submit_button").click(function () {
        {
            $.ajax({
                type: "POST",
                url: "/Home/CreateAccountAjaxPostCall",
                data: JSON.stringify({

                    'first_name_input': $('#first_name_input').val(),
                    'last_name_input': $('#last_name_input').val(),
                    'username_input': $('#username_input').val(),
                    'second_psw': $('#second_psw').val(),
                    'email_input': $('#email_input').val(),
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response == null) {
                        alert("Something went wrong");
                    }
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }
    });*/
    

    $("#submit_button").click(function () {

        var budgetRecord = new Object();
        budgetRecord.date_selection = $('#date_selection').val();
        console.log(budgetRecord.date_selection);
        budgetRecord.budget_name = $('#budget_name').val();
        console.log(budgetRecord.budget_name);
        budgetRecord.description = $('.description').val();
        console.log(budgetRecord.description); 
        budgetRecord.expense_type = $('.expense_type').val();
        console.log(budgetRecord.expense_type);
        budgetRecord.amount_input= $('#amount_input').val();
        console.log(budgetRecord.amount_input); 

        {
            
            $.ajax({
                type: "POST",
                url: "/Home/BudgetSubmitAjaxPostCall",
                data: JSON.stringify({

                    'budget_name': $('#budget_name').val().toString(),
                    'date_selection': $('#date_selection').val(),
                    'description': $('.description').val().toString(),
                    'expense_type': $('.expense_type').val().toString(),
                    'amount_input': parseFloat($('#amount_input').val()),
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                //data: budgetRecord,
                success: function (response) {
                    if (response == null) {
                        alert("Something went wrong");
                    }
                },
                failure: function (response) {
                    alert(response.responseText);
                },
                error: function (response) {
                    alert(response.responseText);
                }
            });
        }
        // console.log(budgetRecord.date_selection); 
    });

