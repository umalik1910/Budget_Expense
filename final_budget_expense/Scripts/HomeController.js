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

function callCurrentDate () {
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
function submitDetails() {
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
}

