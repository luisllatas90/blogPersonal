<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAmbienteConfig.aspx.vb" Inherits="academico_horarios_administrar_frmAmbienteConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style>
     body
        { font-family:Trebuchet MS;
          font-size:12px;
          cursor:hand;
          background-color:white;	
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
    TBODY {
	display: table-row-group;
}
   
        #txtDesde, #txtHasta
        {
            background-color: #C9DDF5;
        }
   
    </style>
     <link rel="stylesheet" href="jquery/jquery-ui.css" />
    <script src="jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 1
            });
            $("#txtHasta").datepicker({
                firstDay: 1
            });
        });
       
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <h4>Configuración de Solicitudes de Ambientes</h4>
    <div>
        <b>1. Fecha Límite:
        <asp:Label ID="lblFecha" runat="server" style="color: #336699" 
            Text="Label"></asp:Label>
        </b>
        <br />
&nbsp;<input type="text" runat="server" id="txtDesde"/>
      <asp:Button ID="btnGuardar" CssClass="btn"   runat="server" Text="Guardar" />
    </div>
    </form>
</body>
</html>
