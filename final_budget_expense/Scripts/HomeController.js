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
document.getElementById("date_selection").onclick = callCurrentDate; 

function callCurrentDate () {
    console.log("current date : " + showCurrentDate());
    var input = document.getElementById("date_selection");
    //input.setAttribute("max", showCurrentDate());
    input.max = showCurrentDate();  
};
