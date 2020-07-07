<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteActivoFijo.aspx.vb" Inherits="administrativo_activofijo_reporteActivoFijo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<form id="frmReporteActivoFijo" name="frmReporteActivoFijo" action="#"  > 
    <input type="hidden" id="param0" name="param0" value="" />  
    
    <div class="col-md-7" >
        <div class="panel panel-piluku">
            <div class="panel-heading">
                    <h3 class="panel-title">
                        Reporte de Activo Fijo
                    </h3>
            </div>                            																						
	        <div class="panel-body">								
	            <div class="col-md-12">
	            
                    <div class="row">			            
                    <div class="form-group">
                        
                        <table class='display dataTable cell-border' id='tbRepActivoFijo' style="width:100%;font-size:smaller;">
                            <thead>
                            <tr>                             
                                 <th style="width:10%;">ITEM</th>                             
                                 <th style="width:10%;">CANT</th>
                                 <th style="width:50%;">ARTICULO</th>
                                 <th style="width:10%;">ESTADO</th>                             
                             </tr>
                             </thead>     
                             <tbody id ="pRepActivoFijo" runat ="server">
                             </tbody>                             
                               <tfoot>
                                <tr>
                                <th colspan="6"></th>
                                </tr>
                                </tfoot>
                        </table>
                    </div>
                    </div>           
                           
                                                         
                </div>
            </div>
            
        </div>	
    </div>	



</form>
</body>
</html>
