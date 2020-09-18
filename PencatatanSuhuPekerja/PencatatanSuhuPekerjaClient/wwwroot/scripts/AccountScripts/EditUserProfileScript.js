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
            window.location.href = "/UserProfiles";
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
    return isValid;
}