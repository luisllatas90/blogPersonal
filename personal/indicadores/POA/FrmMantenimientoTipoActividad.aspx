<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimientoTipoActividad.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />

    <style type="text/css">
       .lblbusqueda
       {
           padding-top:100px;
           }
           .boton1
           {
               font-size:11px;
               color:#333300;
               border: 1px solid #666666;
	           background: #FEFFE1 url('../../images/previo.gif') no-repeat 0% 80%;
	           width: 80;
	           font-family: Tahoma;
	           font-size: 8pt;
	           cursor: hand;
               }
           .boton2
           {
               font-size:11px;
               color:#333300;
               border: 1px solid #666666;
	           background: #FEFFE1 url('../../images/nuevo.gif') no-repeat 0% center;
	           width: 80;
	           font-family: Tahoma;
	           font-size: 8pt;
	           cursor: hand;
               }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" 
            Text="MANTENIMIENTO DE TIPO DE ACTIVIDAD"></asp:Label>
    
    </div>
    <div style="border: 1px solid #CCCCFF; padding-top: 5px; padding-right: inherit; padding-bottom: inherit; padding-left: inherit;">
        <table>
        <tr>
        <td width="150px" >Abreviatura</td>
        <td>
            <asp:TextBox ID="TextBox1" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>   
        <tr>
        <td width="150px" >Descripcion</td>
        <td>
            <asp:TextBox ID="TextBox2" runat="server" Width="324px"></asp:TextBox>
            </td>
        </tr>
        <tr>
        <td colspan="6">
    <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar2" 
            Text="   Guardar"/>
    <asp:Button ID="cmdCancelar" runat="server" CssClass="regresar2" 
            Text="  Cancelar"/>
            </td>
        </tr>
        <tr><td colspan="6"></td></tr>
    </table>
    </div>
    </form>
</body>
</html>
