var table = null;
var departmentSelect = $('#DepartOption');
var divisionSelect = $('#DivisionOption');

$(document).ready(function () {
    clearTextBox();
    getDepartmentDropdown();
    loadData();
});

function loadData() {
    table = $('#dataTable').DataTable({
        orderCellsTop: true,
        fixedHeader: true,
        ajax: {
            url: "/Employees/LoadEmployee",
            type: "GET",
            dataType: "Json",
            dataSrc: "",
        },
        columns: [
            {data: null },
            {data: 'FirstName' },
            {data: "LastName"},
            {data: "Email"},
            {data: "UserName"},
            {data: "PhoneNumber"},
            {data: "Salary"},
            {data: "DivisionName"},
            {data: "DepartmentName"},
            {
                data: "id",
                "render": function (data, type, row, meta) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + meta.row + ')" ><i class="fa fa-lg fa-times"></i></button>';
                },
                "sortable": false,
                "oderable":false
            }
        ],
        "columnDefs": [{
            "searchable": false,
            "orderable": false,
            "targets": 0
        }],
        "order": [[1, 'asc']]
    });

    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
}

function getDepartmentDropdown() {
    departmentSelect.empty();
    divisionSelect.val("");
    $.ajax({
        type: "GET",
        url: "/Departments/LoadDepartments",
        dataType: "Json",
        data: "",
        success: function (results) {
            if (results != null) {
                departmentSelect.append($('<option/>', {
                    value: "",
                    text: "Choose..."
                }));
                $.each(results, function (index, result) {
                    departmentSelect.append("<option value='" + result.id + "'>" + result.name + "</option>");
                });
            };
        },
        failure: function (response) {
            swal.fire('error', 'error getting Department Data !', 'error');
        }
    });
};

function getDivisionDropDown() {
    divisionSelect.empty();
    divisionSelect.val("");
    Id = $('#DepartOption').val();
    $.ajax({
        type: "GET",
        url: "/Divisions/GetDivisionByDepartment/" + Id,
        dataType: "Json",
        data: "",
        success: function (results) {
            if (results != null) {
                divisionSelect.append($('<option/>', {
                    value: "",
                    text: "Choose..."
                }));
                $.each(results, function (index, result) {
                    divisionSelect.append("<option value='" + result.id + "'>" + result.name + "</option>");
                });
            };
        },
        failure: function (response) {
            swal.fire('error', 'error getting Division Data !', 'error');
        }
    });
};


function Delete(index) {
    var Id = table.row(index).data().Id;
    Swal.fire({
        title: 'Confimation',
        text: "Are you sure want to delete this data",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
    }).then((result) => {
        if (result.value) {
            $.ajax({
                url: "/Employees/Delete/" + Id,
                type: "POST",
                dataType: "JSON"
            }).then((result) => {
                if (result.StatusCode == 200) {
                    $('#myModal').modal('hide');
                    Swal.fire({
                        position: 'center',
                        icon: 'Data Successfully Deleted !',
                        title: result.data,
                        showConfirmButton: false,
                        timer: 1500,
                    })
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    clearTextBox();
                }
            });
        }
    })
};

function GetById(index) {
    var Id = table.row(index).data().Id;
    $.ajax({
        url: "/Employees/GetById/",
        data: { Id: Id }
    }).then((result) => {
        $('#Id').val(Id);
        $('#FirstName').val(result.FirstName);
        $('#LastName').val(result.LastName);
        $('#UserName').val(result.UserName);
        $('#Email').val(result.Email);
        $('#PhoneNumber').val(result.PhoneNumber);
        $('#Salary').val(result.Salary);
        $('#DepartOption').val(result.DepartmentId);
        getDivisionDropDown()
        var divId = result.DivisionId;
        $('#myModal').modal('show');
        return divId
    }).then(divId => {
        $('#DivisionOption').val(divId);
    });
};

function Update() {
    var check = validate();
    if (check == false) {
        return false;
    }
    var editEmployeeVM = {
        Id: $('#Id').val(),
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        UserName: $('#UserName').val(),
        Email: $('#Email').val(),
        Salary: $('#Salary').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        DivisionId: $('#DivisionOption').val()
    };
    $.ajax({
        url: "/Employees/Edit",
        data: editEmployeeVM,
        type: "POST",
        dataType: "Json"
    }).then((result) => {
        if (result.Item1.StatusCode == 200) {
            $('#myModal').modal('hide');
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Succesfully Updated !',
                showConfirmButton: false,
                timer: 1500,
            })
            table.ajax.reload(null, false);

        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            clearTextBox();
        }
    });
};

//Function for clearing the textboxes
function clearTextBox() {
    divisionSelect.empty(); 
    $('#Id').val("");
    $('#FirstName').val("");
    $('#LastName').val("");
    $('#UserName').val("");
    $('#Email').val("");
    $('#PhoneNumber').val("");
};

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
    if ($('#Salary').val().trim() == "") {
        $('#Salary').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Salary').css('border-color', 'lightgrey');
    }
    if ($('#DepartOption').val().trim() == "") {
        $('#DepartOption').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DepartOption').css('border-color', 'lightgrey');
    }
    if ($('#DivisionOption').val().trim() == "") {
        $('#DivisionOption').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#DivisionOption').css('border-color', 'lightgrey');
    }
    return isValid;
};