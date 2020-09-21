var table = null;
var ArrayEmployee = [];

$(document).ready(function () {
    debugger;
    table = $("#dataTable").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true,
        "stateSave": true,
        "ajax": {
            url: "/Temperatures/LoadTemperatures",
            type: "GET",
            dataType: "json",
            dataSrc: ""
        },

        "columnDefs": [{
            sortable: false,
            "class": "index",
            targets: 0
        }],
        order: [[1, 'asc']],
        fixedColumns: true,
        "columns": [
            { "data": null },
            { "data": "employee.firstName" },
            { "data": "employee.lastName" },
            { "data": "employeeTemperature" },
            //{ "data": "createTime" },
            //{ "data": "updateTime" },
            {
                "sortable": false,
                "data": "id",
                "render": function (data, type, row, meta) {
                    //console.log(row);
                    $('[data-toggle="tooltip"]').tooltip();
                    return '<button class="btn btn-outline-warning btn-circle" data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + meta.row + ')" ><i class="fa fa-lg fa-edit"></i></button>'
                        + '&nbsp;'
                        + '<button class="btn btn-outline-danger btn-circle" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + meta.row + ')" ><i class="fa fa-lg fa-times"></i></button>';
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'csvHtml5',
                text: '<i class="fas fa-file-csv"></i> CSV Export',
                className: 'btn btn-info',
                title: 'Division List',
                filename: 'cek ' + moment()
            },
            {
                extend: 'excelHtml5',
                text: '<i class="fas fa-file-excel"></i> Excel Export',
                className: 'btn btn-info',
                title: 'Division List',
                filename: 'cek ' + moment()
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fas fa-file-pdf"></i> PDF Export',
                className: 'btn btn-info',
                title: 'Division List',
                filename: 'cek ' + moment(),
                exportOptions: {
                    columns: [0, 1, 2, 3, 4],
                    search: 'applied',
                    order: 'applied',
                    modifier: {
                        page: 'current',
                    },
                },
                customize: function (doc) {
                    debugger;
                    var rowCount = doc.content[1].table.body.length;
                    for (i = 1; i < rowCount; i++) {
                        doc.content[1].table.body[i][2].alignment = 'center';
                    };
                    doc.content[1].table.body[0][0].text = 'No.';
                    doc.content[1].table.body[0][2].text = 'Divisions';
                    doc.content[1].table.body[0][2].text = 'Departments';
                    doc['footer'] = (function (page, pages) {
                        return {
                            columns: [
                                'This is your left footer column',
                                {
                                    // This is the right column
                                    alignment: 'right',
                                    text: ['page ', { text: page.toString() }, ' of ', { text: pages.toString() }]
                                }
                            ],
                            margin: [10, 0]
                        }
                    });
                }
            }
        ]
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function Delete(nummber) {
    var id = table.row(nummber).data().temperatureId;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        debugger;
        if (result.value) {
            $.ajax({
                url: "/Temperatures/DeleteTemperature/",
                data: { Id: id }
            }).then((result) => {
                debugger;
                if (result === 200) {
                    Swal.fire({
                        position: 'center',
                        icon: 'success',
                        title: 'Delete Successfully',
                        showConfirmButton: false,
                        timer: 1500
                    });
                    table.ajax.reload(null, false);
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            });
        }
    });
}

function GetById(nummber) {
    var id = table.row(nummber).data().temperatureId;
    debugger;
    $.ajax({
        url: "/Temperatures/GetTemperatures/",
        data: { id: id }
    }).then((result) => {
        debugger;
        $('#Id').val(result.temperatureId);
        $('#Name').val(result.employeeTemperature);
        $('#DepartOption').val(result.employeeId);
        $('#Insert').hide();
        $('#Update').show();
        $('#exampleModal').modal('show');
    });
}


function LoadEmployee(element) {
    //debugger;
    if (ArrayEmployee.length === 0) {
        $.ajax({
            type: "Get",
            url: "/employees/loademployee",
            success: function (data) {
                arrDepart = data;
                RenderEmployee(element);
            }
        });
    }
    else {
        renderDepart(element);
    }
}

function RenderEmployee(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Employee').hide());
    $.each(arrDepart, function (i, val) {
        $option.append($('<option/>').val(val.Id).text(val.UserName + " (" + val.FirstName + " " + val.LastName + ")"));
    });
}

LoadEmployee($('#DepartOption'));

function ClearScreen() {
    LoadEmployee($('#DepartOption'));
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Insert').show();
}

function Save() {
    debugger;
    var Tmp = new Object();
    Tmp.temperatureId = null;
    Tmp.employeeTemperature = $('#Name').val();
    Tmp.employeeId = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/Temperatures/InsertOrUpdateTemperature/",
        cache: false,
        dataType: "JSON",
        data: Tmp
    }).then((result) => {
        debugger;
        if (result === 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data inserted Successfully',
                showConfirmButton: false,
                timer: 1500
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    });
}

function Update() {
    debugger;
    var Tmp = new Object();
    Tmp.temperatureId = $('#Id').val();
    Tmp.employeeTemperature = $('#Name').val();
    Tmp.employeeId = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/Temperatures/InsertOrUpdateTemperature/",
        cache: false,
        dataType: "JSON",
        data: Tmp
    }).then((result) => {
        debugger;
        if (result === 200) {
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Data Updated Successfully',
                showConfirmButton: false,
                timer: 1500
            });
            table.ajax.reload(null, false);
        } else {
            Swal.fire('Error', 'Failed to Input', 'error');
            ClearScreen();
        }
    });
}