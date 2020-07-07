function truncate(str, num) {
    if (num > str.length) {
        return str;
    } else {
        str = str.substring(0, num);
        return str + "...";
    }

}

function fnLoading(sw) {
  
    if (sw) {
        $('.piluku-preloader').removeClass('hidden');
    } else {
        $('.piluku-preloader').addClass('hidden');
    }
}
function fnLoadingDiv(div, sw) {

	if (sw){			
			$("#"+div).removeClass('hidden');
	}else{
			$("#"+div).addClass('hidden');
	}	
}
function fnDestroyDataTableDetalle(table) {
            var dt = $('#' + table).DataTable().fnDestroy();
            return dt;
        }
        function fnResetDataTable(table) 
        {
          
            
            if ($.fn.DataTable.fnIsDataTable('#' + table)) 
            {
                $('#' + table).DataTable({
                    "bDestroy": true
                });
            }
            else 
            {
                var oTable = $('#' + table).DataTable({
                    "sContentPadding": false
                });
                oTable = $('#' + table).DataTable().fnDestroy();
                oTable = $('#' + table).DataTable({
                    "bPaginate": false,
                    "bFilter": true,
                    "bLengthChange": false,
                    "bInfo": false

                    
                });
                
                return oTable;
            }
        }


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return ((key >= 48 && key <= 57) || (key == 8))
}

function fnDivLoad(div, time) {
    var $target = $('#' + div);
    $target.mask('<i class="fa fa-refresh fa-spin"></i> Cargando...');
    setTimeout(function() {
        $target.unmask(); 
    }, time);
}