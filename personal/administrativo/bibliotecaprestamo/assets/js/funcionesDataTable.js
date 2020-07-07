function fnCreateDataTableBasic(table,col,ord) {
    var dt = $('#' + table).DataTable({
                "sPaginationType": "full_numbers",
                "bLengthChange": false,
                "bAutoWidth": true,
                "aLengthMenu": [[10, 25, 50, 100, 200, -1], [10, 25, 50, 100, 200, "All"]],
                "iDisplayLength": 10,
                "aaSorting": [[col, ord]],
                "sDom": '<"toolbar">fltip'
    });
    return dt;
}

function fnResetDataTableBasic(table, col, ord) {
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
        "aaSorting": [[col, ord]],
        "sDom": '<"toolbar">frtip'
        });

        return dt;
    }
}

function fnResetDataTableTramite(table, col, ord) {
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
            "aaSorting": [[col, ord]],
            "sDom": '<"toolbar">frtip'          
        });
        
        
        
//         $('#' + table).DataTable().fnSettings().aoDrawCallback.push( {
//                "fn": function (oSettings) {
//                    if ( oSettings.aiDisplay.length == 0 ) {
//                    
//                        return;
//                    }
//                  
//                    var nTrs = $('#' + table + ' tbody tr');
//                    var iColspan = nTrs[0].getElementsByTagName('td').length;
//                    var sLastGroup = "";
//                    for ( var i=0 ; i<nTrs.length ; i++ ) {

//                        var iDisplayIndex = oSettings._iDisplayStart + i;
//                        data = oSettings.aoData[ oSettings.aiDisplay[iDisplayIndex] ]._aData;


//                        // Now you can access things like
//                        if (data.percentage > 80) {
//                            // And access the rows like this
//                            $(nTrs[i]).addClass("highlight-green");
//                        }

//                    }
//                },
//            } );   
        
        
        
        
        return dt;
    }
}




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

