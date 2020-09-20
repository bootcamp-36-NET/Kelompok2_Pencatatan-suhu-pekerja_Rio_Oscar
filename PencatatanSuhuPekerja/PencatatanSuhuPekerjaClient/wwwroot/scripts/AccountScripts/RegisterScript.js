$(document).ready(function () {
});

function Register() {
    var check = validate();
    if (check == false) {
        swal.fire('error', 'invalid Input', 'error');
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
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Successfully Registered !',
                showConfirmButton: false,
                timer: 1500
            });
            window.location.href = "/logins";
        } else {
            swal.fire('error', result.Item2, 'error');
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#FirstName').val().trim() == "") {
        $('#FirstName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#FirstName').css('border-color', 'lightgrey');
    }
    if ($('#LastName').val().trim() == "") {
        $('#LastName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#LastName').css('border-color', 'lightgrey');
    }
    if ($('#UserName').val().trim() == "") {
        $('#UserName').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#UserName').css('border-color', 'lightgrey');
    }
    if ($('#Email').val().trim() == "") {
        $('#Email').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    if ($('#PhoneNumber').val().trim() == "") {
        $('#PhoneNumber').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#PhoneNumber').css('border-color', 'lightgrey');
    }
    if ($('#Password').val().trim() == "") {
        $('#Password').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    if ($('#ConfirmPassword').val().trim() == "") {
        $('#ConfirmPassword').css('border-color', 'Red');
        isValid = false;
    }
    else if ($('#ConfirmPassword').val().trim() != $('#Password').val().trim()) {
        $('#ConfirmPassword').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ConfirmPassword').css('border-color', 'lightgrey');
    }

    return isValid;
}