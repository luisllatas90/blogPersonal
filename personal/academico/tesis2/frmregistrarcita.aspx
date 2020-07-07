<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregistrarcita.aspx.vb" Inherits="frmregistrarcita" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Separar cita</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>

</head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <p class="usatTitulo">Registro de Solicitud de Cita<p/>
    
    <table width="100%" class="contornotabla">
        <tr>
            <td>
                Fecha de Registro</td>
            <td>
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Tipo</td>
            <td>
                <asp:DropDownList ID="dpTipoCita" runat="server">
                    <asp:ListItem>PRESENCIAL</asp:ListItem>
                    <asp:ListItem>VIRTUAL</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Asunto</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtAsuntoCita" runat="server" CssClass="cajas2"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                Descripción / Observaciones</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TextBox ID="txtObsCita" runat="server" CssClass="cajas2" Height="62px" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Archivos adjuntos</td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                1. Archivo 1.doc<br />
                2. Archivo 2.doc</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
                            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>
    </body>
</html>
