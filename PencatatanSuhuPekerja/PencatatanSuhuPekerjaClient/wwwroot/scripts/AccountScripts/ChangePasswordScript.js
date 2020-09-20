$(document).ready(function () {
});

function ChangePassword() {
    var check = validate();
    if (check == false) {
        return false;
    }
    var changePassowordVM = {
        OldPassword: $('#OldPassword').val(),
        NewPassword: $('#NewPassword').val(),
        ConfirmNewPassword: $('#ConfirmNewPassword').val(),
    };
    $.ajax({
        url: "/ChangePasswords/ChangePassword",
        data: changePassowordVM,
        cache: false,
        type: "POST",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Password Successfully Changed !',
                showConfirmButton: false,
                timer: 1500
            });
            window.location.href = "/UserProfiles";
        } else {
            swal.fire('error', result.Item2, 'error');
        }
    });
}

function validate() {
    var isValid = true;
    if ($('#OldPassword').val().trim() == "") {
        $('#OldPassword').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#OldPassword').css('border-color', 'lightgrey');
    }
    if ($('#NewPassword').val().trim() == "") {
        $('#NewPassword').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#NewPassword').css('border-color', 'lightgrey');
    }
    if ($('#ConfirmNewPassword').val().trim() == "") {
        $('#ConfirmNewPassword').css('border-color', 'Red');
        isValid = false;
    } else if ($('#ConfirmNewPassword').val().trim() != $('#NewPassword').val().trim()) {
        $('#ConfirmNewPassword').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#ConfirmNewPassword').css('border-color', 'lightgrey');
    }
    return isValid;
}