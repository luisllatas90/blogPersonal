<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmanularcita.aspx.vb" Inherits="frmanularcita" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Sustentación</title>
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />

</head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%>
    <p class="usatTitulo">Registro de Suspensión de cita<p/>
<table cellpadding="3" cellspacing="0" width="100%" class="contornotabla">
        <tr>
            <td width:20%>
                Fecha de registro</td>
            <td width:80%>
                <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
        </tr>
        <tr>
            <td style="width:100%" colspan="2">
                Motivo</td>
        </tr>
        <tr>
            <td colspan="2" style="width:100%">
                <asp:TextBox ID="txtobs" runat="server" CssClass="cajas2" 
                    TextMode="MultiLine" Height="168px"></asp:TextBox>
            </td>
        </tr>
        </table>
       
        <p style="text-align:center">
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Font-Bold="True" />&nbsp;
            <asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
        </p>
    </form>
</body>
</html>
