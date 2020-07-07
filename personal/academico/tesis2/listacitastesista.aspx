<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listacitastesista.aspx.vb" Inherits="SysTesisInv_listacitastesista" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de citas</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
        <p class="usatTitulo">
        Citas / Asesorías registradas</p>
    <table width="100%">
        <tr>
            <td style=" width:15%">
                Estado de citas</td>
            <td style=" width:85%">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>Pendientes</asp:ListItem>
                    <asp:ListItem>Atendidas</asp:ListItem>
                    <asp:ListItem>Suspendidas</asp:ListItem>
                    <asp:ListItem>Todas</asp:ListItem>
                </asp:DropDownList>
                    </td>
        </tr>
    </table>
    <p class="azul">Detalle de cita registrada<p/>
<table border="1" cellpadding="3" cellspacing="0" 
        style="width:100%; border-collapse:collapse">
        <tr style="background-color:#5D7B9D;color:White">
            <th>
                Tipo</th>
            <th>
                Fecha Reg</th>
            <th>
                Asunto</th>
            <th>
                Estado</th>
            <th>
                Inicio - Fin</th>
            <th>
                Acción</th>
        </tr>
        <tr class="Selected">
            <td>
                <%=Session("tipocita")%></td>
            <td>
               <%=Session("fechacita")%></td>
            <td>
                <%=Session("asuntocita")%></td>
            <td>
                 <%=Session("estadocita")%></td>
            <td>
                &nbsp;</td>
            <td align="center">
                <asp:Button ID="cmdAnular" runat="server" 
                    onclientclick="AbrirPopUp('frmanularcita.aspx','400','500');return(false)" 
                    Text="Postergar" CausesValidation="False" UseSubmitBehavior="False" />
                        </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                 &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                 &nbsp;</td>
            <td>
                &nbsp;</td>
            <td align="center">
                &nbsp;</td>
        </tr>
    </table>

            <p class="azul">Detalle de cita seleccionada<p/>
    <table border="1" cellpadding="3" cellspacing="0" 
        style="width:100%; border-collapse:collapse">
        <tr style="background-color:#5D7B9D;color:White">
            <th>
                Fecha Reg</th>
            <th>
                Observación</th>
            <th>
                Archivos publicados</th>
        </tr>
        <tr class="Selected">
            <td>
               <%=Session("fechaasesoria")%></td>
            <td>
                <%=Session("asuntoasesoria")%></td>
            <td>
                 &nbsp;</td>
        </tr>
        <tr >
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                 &nbsp;</td>
        </tr>
        <tr >
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                 &nbsp;</td>
        </tr>
    </table>
    
            <p>
                <asp:Button ID="cmdAtender" runat="server" 
                    onclientclick="AbrirPopUp('frmasesorarcita.aspx','500','600');return(false)" 
                    Text="Responder a Asesoría" CausesValidation="False" 
                    UseSubmitBehavior="False" />
                    &nbsp;</p>
    </form>
</body>
</html>
