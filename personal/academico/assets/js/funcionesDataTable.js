function fnCreateDataTable(table) {
   var dt= $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
        "iDisplayLength": 10,
        "aaSorting": [[1, "desc"]],
        "defaultContent": "-",
        "bProcessing": true,
        "bDeferRender": true,
        "bSort": false,
        "sScrollY": '300px',
        "bScrollCollapse": true,
        "sPaging": false,
        "bInfo":false
    });
    return dt;
}

function fnResetDataTable(table) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt=$('#' + table).DataTable({
            "sContentPadding": false
        });
        dt=$('#' + table).DataTable().fnDestroy();
        dt=$('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
            "iDisplayLength": 10,
            "aaSorting": [[1, "desc"]],
            "defaultContent": "-",
            "bProcessing": true,
            "bDeferRender": true,
            "bSort": false,
            "sScrollY": '300px',
            "bScrollCollapse": true,
            "sPaging": false,
            "bInfo": false
        });

        return dt;
    }
}

function fnDestroyDataTable(table) {
  var dt=  $('#' + table).DataTable().fnDestroy();
  return dt;
}


function fnCreateDataTableDetalle(table) {
    var dt = $('#' + table).DataTable({
        "sPaginationType": "full_numbers",
        "bLengthChange": false,
        "bAutoWidth": true,
        "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
        "iDisplayLength": 10,
        "aaSorting": [[1, "desc"]],
        "defaultContent": "-",
        "bProcessing": true,
        "bDeferRender": true,
        "bSort": false,
        "sScrollY": '300px',
        "bScrollCollapse": true,
        "sPaging": false,
        "bInfo": false,
        "bPaginate": false,
        "bFilter": false
    });
    return dt;
}

function fnResetDataTableDetalle(table) {
    if ($.fn.DataTable.fnIsDataTable('#' + table)) {
        $('#' + table).DataTable({
            "bDestroy": true
        });
    }
    else {
        var dt = $('#' + table).DataTable({
            "sContentPadding": false
        });
        dt = $('#' + table).DataTable().fnDestroy();
        dt = $('#' + table).DataTable({
            "sPaginationType": "full_numbers",
            "bLengthChange": false,
            "bAutoWidth": true,
            "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
            "iDisplayLength": 10,
            "aaSorting": [[1, "desc"]],
            "defaultContent": "-",
            "bProcessing": true,
            "bDeferRender": true,
            "bSort": false,
            "sScrollY": '300px',
            "bScrollCollapse": true,
            "sPaging": false,
            "bInfo": false,
            "bPaginate": false,
            "bFilter": false
        });

        return dt;
    }
}

function fnDestroyDataTableDetalle(table) {
    var dt = $('#' + table).DataTable().fnDestroy();
    return dt;
}

