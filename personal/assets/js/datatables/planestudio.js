$(document).ready(function() {
    // $('#divInfoDetails').hide();
var oTable = $('#tablaplanestudio').DataTable({
            "bPaginate": false,
            "bFilter": true,
            "bLengthChange": false,
            "bInfo": false,
            "aoColumnDefs": [{
            "bSortable": false,
            "aTargets": [ 1, 2, 3, 4, 5,6,7,8,9,10]
                
            }]
        });




    });
