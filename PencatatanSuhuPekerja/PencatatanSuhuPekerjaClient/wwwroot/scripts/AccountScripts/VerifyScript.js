$(document).ready(function () {
});

function Verify() {
    Swal.showLoading()
    var check = validate();
    if (check == false) {
        return false;
    }
    var code = $('#Code').val();
    $.ajax({
        url: "/verifies/verify/" + code,
        data: { code: code },
        cache: false,
        type: "POST",
        dataType: "JSON"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            window.location.href = "/Dashboards";
        } else {
            alertify.error(result.Item2);
        }
    });
}


function validate() {
    var isValid = true;
    if ($('#Code').val().trim() == "") {
        $('#Code').css('border-color', 'Red');
        alertify.error('Code Cannot Empty');
        isValid = false;
    }
    else {
        $('#Code').css('border-color', 'lightgrey');
    }

    return isValid;
}