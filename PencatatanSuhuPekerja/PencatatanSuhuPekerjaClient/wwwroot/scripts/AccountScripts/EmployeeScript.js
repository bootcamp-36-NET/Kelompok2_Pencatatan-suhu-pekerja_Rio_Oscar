var table = null

$(document).ready(function () {
    loadData();
    //getDepartmentDropdown();
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
            { data: null },
            { data: 'FirstName' },
            {data: "LastName"},
            {data: "Email"},
            {data: "UserName"},
            {data: "PhoneNumber"},
            {data: "Salary"},
            {data: "Division.Name"},
            {data: "Division.Department.Name"},
            {
                data: null,
                render: function (data, type, row,meta) {
                    return '<Button class="btn btn-warning" onclick="return GetById(' + meta.row + ')">Update</button>'
                        + '&nbsp;'
                        + '<Button class="btn btn-danger" onclick="return Delete(' + meta.row + ')">Delete</button>'
                },
                orderable: false,
                searchable: false
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
    var departmentSelect = $('#Department');
    departmentSelect.empty();
    $.ajax({
        type: "GET",
        url: "/Departments/LoadDepartment",
        dataType: "Json",
        data: "",
        success: function (results) {
            if (results != null) {
                departmentSelect.append($('<option/>', {
                    value: "",
                    text: "Choose..."
                }));
                $.each(results, function (index, result) {
                    departmentSelect.append("<option value='" + result.Id + "'>" + result.Name + "</option>");
                });
            };
        },
        failure: function (response) {
            alert(response);
        }
    });
};

function getDepartmentFilter() {
    var departmentSelect = $('#DepartmentFilter');
    departmentSelect.empty();
    $.ajax({
        type: "GET",
        url: "/Departments/LoadDepartment",
        dataType: "Json",
        data: "",
        success: function (results) {
            if (results != null) {
                departmentSelect.append($('<option/>', {
                    value: "",
                    text: ""
                }));
                $.each(results, function (index, result) {
                    departmentSelect.append("<option value='" + result.Id + "'>" + result.Name + "</option>");
                });
            };
        },
        failure: function (response) {
            alert(response);
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
                url: "/Divisions/Delete/" + Id,
                type: "POST",
                dataType: "JSON"
            }).then((result) => {
                if (result.StatusCode == 200) {
                    $('#myModal').modal('hide');
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
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
}

function GetById(index) {
    var Id = table.row(index).data().Id;
    $.ajax({
        url: "/Divisions/GetById/",
        data: { Id: Id }
    }).then((result) => {
        $('#Id').val(result.Id);
        $('#Name').val(result.Name);
        $('#Department').val(result.DepartmentId);
        $('#btnAdd').hide();
        $('#btnUpdate').show();
        $('#myModal').modal('show');
    })
}

function Update() {
    var check = validate();
    if (check == false) {
        return false;
    }
    var division = {
        Id: $('#Id').val(),
        Name: $('#Name').val(),
        DepartmentId: $('#Department').val()
    };
    $.ajax({
        url: "/Divisions/AddOrUpdate",
        data: division,
        type: "POST",
        dataType: "Json"
    }).then((result) => {
        if (result.StatusCode == 200) {
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
}

function filterDepartment() {
    var selectedDepartment = $("DepartmentFilter").val();
    $.ajax({
        url: "/Divisions/FilteredData/" + selectedDepartment,
        data: "",
        type: "POST",
        dataType: "Json"
    }).then((result) => {
        if (result.StatusCode == 200) {
            table.loadData.
                table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Update', 'error');
            clearTextBox();
        }
    });
}

//Function for clearing the textboxes
function clearTextBox() {
    departmentSelect.empty();
    $('#Name').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
}

function validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Department').val().trim() == "") {
        $('#Department').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Department').css('border-color', 'lightgrey');
    }
    return isValid;
}



