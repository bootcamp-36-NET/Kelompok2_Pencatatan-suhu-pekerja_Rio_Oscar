$(document).ready(function () {
});

function Login() {
    var check = validate();
    if (check == false) {
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
            if (result.Item3 == "false") {
                window.location.href = "/verifies";
            } else {
                window.location.href = "/Dashboards";
            }
        } else {
            alertify.error(result.Item2);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        alertify.error('Email Cannot Empty');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        alertify.error('Password Cannot Empty');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    return isValid;
}