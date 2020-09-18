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
            var FullName = result.Item2.FirstName + " " + result.Item2.LastName;
            // var RoleString = result.Item1.RoleName.Join();
            $('#UserName').html(result.Item2.UserName);
            $('#FirstName').append(result.Item2.FirstName);
            $('#Division').html(result.Item2.Division);
            $('#Department').html(result.Item2.Department);
            $('#Email').html(result.Item2.Email);
            $('#FullName').html(FullName);
            $('#PhoneNumber').html(result.Item2.PhoneNumber);
            $('#Roles').html(result.Item2.Roles.toString());
        }
    });
}