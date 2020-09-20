$(document).ready(function () {
});

function Login() {
    debugger;
    Swal.showLoading()
    var check = validate();
    if (check == false) {
        Swal.fire('Error', 'Invalid Data', 'error');
        return false;
    }
    var loginVM = {
        Email: $('#Email').val(),
        Password: $('#Password').val()
    };
    $.ajax({
        url: "/Logins/Login",
        data: loginVM,
        cache: false,
        type: "POST",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            if (result.Item3 === "false") {
                window.location.href = "/verifies";
            } else {
                window.location.href = "/Dashboards";
            }
        } else {
            Swal.fire('Error', result.Item2, 'error');
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    return isValid;
}