<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActividades.aspx.vb" Inherits="administrativo_pec2_frmActividades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Actividad</title>
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
        </style>
        <script language="JavaScript">
         function validarnumeros(e) {
             var unicode = e.charCode ? e.charCode : e.keyCode
             if (unicode != 8) {
                 if (unicode < 48 || unicode > 57) //if not a number
                 { return false } //disable key press    
             }
         }
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td style="width:100px">Titulo</td>
                <td>
                    <asp:TextBox ID="txtTitulo" runat="server" MaxLength="100" Width="300px" 
                        Font-Names="Arial"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px">Contenido</td>
                <td>
                    <asp:TextBox ID="txtContenido" runat="server" MaxLength="1000" 
                        TextMode="MultiLine" Width="300px" Font-Names="Arial"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px">Ruta</td>
                <td>
                    <asp:TextBox ID="txtRuta" runat="server" MaxLength="50" Width="301px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width:100px">Tipo Actividad</td>
                <td>
                    <asp:DropDownList ID="cboActividad" runat="server" Height="22px" Width="307px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:100px">Máximo de Inscripciones por Alumno</td>
                <td>
                    <asp:TextBox ID="txtMaxInscripciones" runat="server" MaxLength="50" Width="40px" Text="0"  onkeypress="return validarnumeros(event);"></asp:TextBox>
                    <asp:Label runat="server">Ingresar 0 si no hay límite de inscripciones</asp:Label></td>
            </tr>
            <tr>
                <td style="width:100px"></td>
                <td>
                    <asp:Button ID="cmdAceptar" runat="server" Text="Guardar" Height="22px" Width="100px" CssClass="usatGuardar" />&nbsp;&nbsp;
                    <asp:Button ID="cmdCancelar" runat="server" Text="Regresar" Height="22px" Width="100px" CssClass="usatSalir" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
