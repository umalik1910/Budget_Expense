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
                    if (response == "Existing") {
                        window.alert("Account already exists under this username and password, account cannot be made!");
                    }
                    else {
                        var url = 'SignIn/Home';
                        window.location.href = url;
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

    } else {
        //throw error
        alert("Passwords are not matching or the boxes are blank!");
}
    
});


    

$("#submit_button").click(function () {

    if (document.getElementById("amount_input").value < 0) {

        alert("Amount Value cannot be negative!");
    }
    else {

        $.ajax({
            type: "POST",
            url: "/Home/BudgetSubmitAjaxPostCall",
            data: JSON.stringify({
                'user_id': $('#user_id').text(),
                'budget_name': $('#budget_name').val().toString(),
                'date_selection': $('#date_selection').val(),
                'description': $('.description').val().toString(),
                'expense_type': $('.expense_type').val().toString(),
                'amount_input': parseFloat($('#amount_input').val()),
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response == null) {
                    alert("Something went wrong");
                }
                else {
                    window.alert("Transaction was processed"); 
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
});
       


$("#sign_in_submit_button").click(function () {
   {
        $.ajax({
            type: "POST",
            url: "/Home/LoginCheck",
            data: JSON.stringify({

                'username_input': $('#username_input').val(),
                'password_input': $('#password_input').val(),
                
            }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response == "Login Unsuccessful") {
                    window.alert("Account could not be found, please try again");
                }
                else {
                    window.alert("Login was successful");
                    if (response.redirectTo) {
                        window.location.href = response.redirectTo;
                    }
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
                window.alert("Login was unsuccessful");
            }
        });
    }  
});

$(document).ready("#monthOptions").change(function () {
    $.ajax({
            type: "GET",
            url: "/BudgetExpense/GetTransactionPartial",
            data: {
                'month': $("#monthOptions").val(),
                'year': $("#yearOptions").val(),
                'id': $('#user_id').text()
            },
            success: function (response) {
                if (response == null) {
                    alert("Something went wrong");
                }
                $('#recordList').empty();
                $('#recordList').html(response);
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    }); 
 /*$("#goBackButton").click(function () {
    var id = $("#user_id").text();
    var url = '@Html.Raw(Url.Action("Home/Home", new { id = @Model.User.UserID }))';
    var url2 = '@Html.Raw(Url.Action("Home/Home"))';
    if (id != null) {
      window.location.href = url;
 }
   else {
       window.location.href = url2;
   }
});*/