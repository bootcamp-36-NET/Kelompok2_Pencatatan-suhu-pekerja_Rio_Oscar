var table = null;
var arrDepart = [];

$(document).ready(function () {
    debugger;
    table = $("#dataTable").DataTable({
        "processing": true,
        "responsive": true,
        "pagination": true, 
        "stateSave": true,
        "ajax": {   
            url: "/Divisions/LoadDivisions",
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
            { "data": "name" },
            { "data": "department.name" },
            {
                "data": "createdAt",
                "render": function (jsonDate) {
                    var date = moment(jsonDate).format("DD MMMM YYYY, h:mm:ss a");
                    return date;
                }
            },
            {
                "data": "updatedAt",
                "render": function (jsonDate) {
                    if (!moment(jsonDate).isBefore('1000-01-01')) {
                        var date = moment(jsonDate).format("DD MMMM YYYY, h:mm:ss a");
                        return date;
                    }
                    return "Not Updated Yet";
                }
            },
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
        ],
        initComplete: function () {
            debugger;
            this.api().columns(1).every(function () {
                var column = this;
                var select = $('<select><option value="">Division All</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>');
                });
            });
            this.api().columns(2).every(function () {
                var column = this;
                var select = $('<select><option value="">Department All</option></select>')
                    .appendTo($(column.header()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );
                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });
                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>');
                });
            });
        }
    });
    table.on('order.dt search.dt', function () {
        table.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
            cell.innerHTML = i + 1;
        });
    }).draw();
});

function Delete(nummber) {
    var id = table.row(nummber).data().id;
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
                url: "/Divisions/DeleteDivision/",
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
    var id = table.row(nummber).data().id;
    debugger;
    $.ajax({
        url: "/Divisions/GetDivision/",
        data: { id: id }
    }).then((result) => {
        debugger;
        $('#Id').val(result.id);
        $('#Name').val(result.name);
        $('#DepartOption').val(result.departmentId);
        $('#Insert').hide();
        $('#Update').show();
        $('#exampleModal').modal('show');
    });
}
  

function LoadDepart(element) {
    //debugger;
    if (arrDepart.length === 0) {
        $.ajax({
            type: "Get",
            url: "/Departments/LoadDepartments",
            success: function (data) {
                arrDepart = data;
                renderDepart(element);
            }
        }); 
    }
    else {
        renderDepart(element);
    }
}

function renderDepart(element) {
    var $option = $(element);
    $option.empty();
    $option.append($('<option/>').val('0').text('Select Department').hide());
    $.each(arrDepart, function (i, val) {
        $option.append($('<option/>').val(val.id).text(val.name));
    });
}

LoadDepart($('#DepartOption'));

function ClearScreen() {
    LoadDepart($('#DepartOption'));     
    $('#Id').val('');
    $('#Name').val('');
    $('#Update').hide();
    $('#Insert').show();
}

function Save() {
    debugger;
    var Div = new Object();  
    Div.Id = null;
    Div.Name = $('#Name').val();
    Div.departmentId = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/divisions/InsertOrUpdateDivision/",
        cache: false,
        dataType: "JSON",
        data: Div
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
    var Div = new Object();
    Div.Id = $('#Id').val();
    Div.Name = $('#Name').val();
    Div.departmentId = $('#DepartOption').val();
    $.ajax({
        type: 'POST',
        url: "/divisions/InsertOrUpdateDivision/",
        cache: false,
        dataType: "JSON",
        data: Div
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