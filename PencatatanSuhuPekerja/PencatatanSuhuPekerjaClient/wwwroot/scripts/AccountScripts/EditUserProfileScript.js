$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        url: "/UserProfiles/GetUserProfile",
        data: "",
        cache: false,
        type: "GET",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            $('#FirstName1').append(result.Item2.FirstName);
            $('#UserName1').html(result.Item2.UserName);
            $('#Division').html(result.Item2.Division);
            $('#Department').html(result.Item2.Department);

            $('#UserName').val(result.Item2.UserName);
            $('#FirstName').val(result.Item2.FirstName);
            $('#LastName').val(result.Item2.LastName);
            $('#Email').val(result.Item2.Email);
            $('#PhoneNumber').val(result.Item2.PhoneNumber);
        }
    });
}

function Edit() {
    var check = validate();
    if (check == false) {
        return false;
    }
    var editEmployeeVM = {
        UserName: $('#UserName').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
    };
    $.ajax({
        url: "/EditUserProfiles/Edit",
        data: editEmployeeVM,
        cache: false,
        type: "POST",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Successfully Edited !',
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
    return isValid;
}