<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListMonitoreoRendiciones.aspx.vb" Inherits="personal_administrativo_Tesoreria_Rendiciones_frmListMonitoreoRendiciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <link rel="stylesheet" href="../shared/jqwidgets/styles/jqx.base.css" type="text/css" />
<link rel="stylesheet" href="../shared/jqwidgets/styles/jqx.metro.css" type="text/css" />
<link rel="stylesheet" href="../shared/jqwidgets/styles/jqx.bootstrap.css" type="text/css" />

	<script type="text/javascript" src="../shared/scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxcore.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxdata.js"></script> 
    <script type="text/javascript" src="../shared/jqwidgets/jqxbuttons.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxscrollbar.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxmenu.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.js"></script>
	<script type="text/javascript" src="../shared/jqwidgets/jqxgrid.sort.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.filter.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.selection.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.grouping.js"></script> 
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.pager.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxgrid.columnsresize.js"></script> 
    <script type="text/javascript" src="../shared/jqwidgets/jqxlistbox.js"></script>
    <script type="text/javascript" src="../shared/jqwidgets/jqxdropdownlist.js"></script>
    
    
      <script type="text/javascript">
          $(document).ready(function() {
              //var url_data='<?php echo $this->basePath().'/almacen/producto/listproductojson'; ?>';
            //  var url = location.href;
            //'  alert($(location).attr('hostname'));
              var url_data = "http://"+$(location).attr('hostname') + "/campusvirtual/personal/administrativo/tesoreria/rendiciones/json/JsonRendionesMonitor.aspx";
            //  alert(url_data);
              var source =
            {
                datatype: "json",
                datafields: [
                    { name: 'Nombres' },
                    { name: 'tipo' },
                    { name: 'identificador' },
                    { name: 'fechagen_egr', type: 'string' },
                    { name: 'descripcion_Tdo' },
                    { name: 'doc_egreso' },
                    { name: 'descripcion_tip' },
                    { name: 'importe_egr', type: 'number' },
                    { name: 'usuarioreg_egr' },
                    { name: 'descripcion_rub' },
                    { name: 'descripcion_Cco' },
                    { name: 'EstadoRendicion' },
                    { name: 'Observacion_deg' },
                    { name: 'importe_deg', type: 'number' },
                     { name: 'codigo_egr' },

                ],
                async: false,
                url: url_data,
                data: {
                    featureClass: "P",
                    style: "full",
                    maxRows: 50,
                    username: "jqwidgets"
                }
            };
              var dataAdapter = new $.jqx.dataAdapter(source,
                {
                    formatData: function(data) {
                        //data.name_startsWith = $("#searchField").val();
                        return data;
                    }
                }
            );
              $("#jqxgrid").jqxGrid(
            {
                width: 1300,
                source: dataAdapter,
                theme: 'metro',
                pageable: true,
                autoheight: true,
                sortable: true,
                altrows: true,
                enabletooltips: true,
                showtoolbar: true,
                filterable: true,
                pageSize: 400,
                pagesizeoptions: ['20', '40', '60', '100', '200', '300', '400', '500', '600', '700', '800', '900', '1000'],
                columns: [
                    { text: 'Nro.Egreso', datafield: 'codigo_egr', width: 100 },
                    { text: 'Datos personales', datafield: 'Nombres', width: 300 },
                    { text: 'Tipo', datafield: 'tipo', width: 120 },
                    { text: 'D.N.I/R.U.C', datafield: 'identificador', cellsformat: 'f', width: 120 },
                    { text: 'Fecha', datafield: 'fechagen_egr', width: 120 },
                    { text: 'T./Documento', datafield: 'descripcion_Tdo', width: 120 },
                    { text: 'Nro.Documento', datafield: 'doc_egreso', width: 120 },
                    { text: 'Total', datafield: 'importe_egr', width: 120 },
                    { text: 'Imp.Detalle ', datafield: 'importe_deg', width: 120 },
                    { text: 'Usuario', datafield: 'usuarioreg_egr', width: 120 },
                    { text: 'Rubro', datafield: 'descripcion_rub', width: 300 },
                    { text: 'Centro de costos', datafield: 'descripcion_Cco', width: 300 },
                    { text: 'Estado', datafield: 'EstadoRendicion', width: 120 },
                    { text: 'Observacion', datafield: 'Observacion_deg', width: 120 },
                ],
                groups: ['Nombres', 'codigo_egr'],
                showtoolbar: false,
                autoheight: true,
                groupable: true

            });
          });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
          
    <asp:Button ID="Button1" runat="server" style="font-weight: 700" 
        Text="Exportar Excel" Width="153px" />
    </div>
    </form>
    <div id='jqxWidget' style="font-size: 13px; font-family: Verdana; float: left;">
        <div id="jqxgrid">
        </div>
        
</body>
</html>
