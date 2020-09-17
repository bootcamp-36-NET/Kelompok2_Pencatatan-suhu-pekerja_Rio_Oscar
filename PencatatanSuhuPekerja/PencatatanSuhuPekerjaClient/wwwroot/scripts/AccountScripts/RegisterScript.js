$(document).ready(function () {
});

function Register() {
    var check = validate();
    if (check == false) {
        return false;
    }
    var registerVM = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        UserName: $('#UserName').val(),
        Password: $('#Password').val(),
        ConfirmPassword: $('#ConfirmPassword').val()
    }
    $.ajax({
        url: "/Registers/Register",
        data: registerVM,
        type: "POST",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            window.location.href = "/logins";
        } else {
            alertify.error(result.Item2);
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        alertify.error('First Name Cannot Empty');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }
    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        alertify.error('Last Name Cannot Empty');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }
    if ($('#UserName').val().trim() == "") {
        $('#UserName').css('border-color', 'Red');
        alertify.error('User Name Number Cannot Empty');
        isValid = false;
    }
    else {
        $('#UserName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        alertify.error('Email Cannot Empty');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#PhoneNumber').val().trim() == "") {
        $('#PhoneNumber').css('border-color', 'Red');
        alertify.error('Phone Number Cannot Empty');
        isValid = false;
    }
    else {
        $('#PhoneNumber').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        alertify.error('Password Cannot Empty');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    if ($('#ConfirmPassword').val().trim() == "") {
        $('#ConfirmPassword').css('border-color', 'Red');
        alertify.error('Confirm Password must be same !');
        isValid = false;
    }
    else if ($('#ConfirmPassword').val().trim() != $('#Password').val().trim()) {
        $('#ConfirmPassword').css('border-color', 'Red');
        alertify.error('Password and Confirm Password must be same !');
        isValid = false;
    }
    else {
        $('#ConfirmPassword').css('border-color', 'lightgrey');
    }

    return isValid;
}