<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsistenciaEvento.aspx.vb" Inherits="administrativo_pec2_frmAsistenciaEvento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Asistencia</title>
    <script src="js/MaskHora.js" type="text/javascript"></script>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
        <style type="text/css">
            table {
	            font-family: Trebuchet MS;
	            font-size: 8pt;
            }
            TBODY {
	            display: table-row-group;
            }
            tr {
	            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	            font-size: 8pt;
	            color: #2F4F4F;
            }
            select {
	            font-family: Verdana;
	            font-size: 8.5pt;
            }
            .agregar2
            {}
            .style1
            {
                width: 669px;
            }
        </style>
        <script type="text/javascript">
            function seleccionaFoco() {
                if (event.keyCode == 13) {
                    event.keyCode = 9;
                    //document.miFormulario.campo1.focus() 
                    //document.miFormulario.campo1.select()
                }

            } 
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="40%">
        <tr>
            <td colspan="2" 
                style="background:#E6E6FA; width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" height="40px">
                <asp:Label ID="lblTitulo" runat="server" Text="Asistencia de Evento" 
                    Font-Bold="True" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>Evento:</td>            
            <td colspan="2" class="style1">
                <asp:DropDownList ID="cboEvento" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Calendario:</td>
            <td colspan="2" class="style1">
                <asp:Calendar ID="calFecha" runat="server"></asp:Calendar>
            </td>
        </tr>
        <tr>
            <td>Actividad:</td>
            <td colspan="2" class="style1">
                <asp:DropDownList ID="cboActividad" runat="server" Width="300px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Hora:</td>
            <td><asp:TextBox ID="txtHora" runat="server" Width="127px" Font-Names="Arial" 
                        Font-Size="X-Small"></asp:TextBox></td>
        </tr>        
        <tr>
            <td>Documento:</td>
            <td colspan="2" class="style1">
                <asp:DropDownList ID="cboDocumento" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Nro. Doc.:</td>
            <td colspan="2" class="style1">
                <asp:TextBox ID="txtDocumento" runat="server"></asp:TextBox>&nbsp;&nbsp;
                <asp:Button ID="cmdAgregar" runat="server" Text="Agregar" Width="80px" 
                    CssClass="agregar2" /></td>
        </tr>
        <tr>            
            <td colspan="3">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="#FF3300" 
                    Font-Size="20pt"></asp:Label>
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdPermiteAsistencia" runat="server" />
    <asp:HiddenField ID="HdCicloActual" runat="server" />
    </form>    
</body>
</html>
