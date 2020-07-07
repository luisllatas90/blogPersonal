<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmregsustentacion.aspx.vb" Inherits="frmregsustentacion" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Sustentación</title>
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#eeeeee">
    <form id="form2" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%>
    <p class="usatTitulo">Registro de Sustentación<p/>
<table cellpadding="3" cellspacing="0" width="100%" class="contornotabla">
        <tr>
            <td>
                Nro Expediente</td>
            <td>
                <asp:TextBox ID="txtcodigo" runat="server"></asp:TextBox>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Fecha</td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                </asp:TextBox>
                <asp:Button ID="Button2" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
            </td>
            <td>
                Hora</td>
            <td colspan="2">
                <asp:TextBox ID="txtHora" runat="server" Width="37px"></asp:TextBox>
                &nbsp;Min
                <asp:TextBox ID="txtMinuto" runat="server" Width="37px"></asp:TextBox>
                    </td>
        </tr>
        <tr>
            <td>
                Ambiente</td>
            <td>
                <asp:DropDownList ID="dpFase" runat="server">
                    <asp:ListItem>AUDIOVISUAL 314</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Observaciones</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:TextBox ID="TextBox1" runat="server" CssClass="cajas2" 
                    TextMode="MultiLine" Height="68px"></asp:TextBox>
            </td>
        </tr>
        </table>
       
        <p style="text-align:center">
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Font-Bold="True" />&nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cerrar" 
                                    CausesValidation="False" onclientclick="window.close();return(false)" />
                            &nbsp;
        </p>
    </form>
</body>
</html>
