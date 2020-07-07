<%@ Page Language="VB" AutoEventWireup="false" CodeFile="calendario.aspx.vb" Inherits="librerianet_aulavirtual_calendario" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Calendario de Actividades</title>
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
        </style>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/PopCalendar.js"></script>
        
</head>
<body>
    <form id="form1" runat="server" >
    <%Response.Write(ClsFunciones.CargaCalendario)%>    
    <p class="usatTitulo">Calendario de actividades</p>
    
    <table style="border: 1px solid #808080; width:100%; background-color: #EEEEEE;">
        <tr>
            <td style="width:20%">
                &nbsp;</td>
            <td style="width:80%" align="right">
                <asp:Label ID="lbltotal" runat="server" Font-Bold="True" ForeColor="#003399"></asp:Label>
            </td>
            
        </tr>
    </table>
    <br />
                                    <table style="width:100%">
                                        <tr>
                                            <td style="width: 30%" valign="top">
                                    <asp:Calendar ID="Calendar1" runat="server" 
        BackColor="White" BorderColor="#6699FF" Font-Names="Times New Roman" 
        Font-Size="10pt" ForeColor="Black" Height="220px" 
        Width="100%" DayNameFormat="Shortest" NextPrevFormat="ShortMonth">
                                        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
                                        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" 
                                            Font-Size="8pt" ForeColor="#333333" Width="1%" />
                                        <TodayDayStyle BackColor="#CCCC99" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <DayStyle Width="14%" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#003399" />
                                        <DayHeaderStyle Font-Bold="True" Font-Size="7pt" BackColor="#CCCCCC" 
                                            ForeColor="#333333" Height="10pt" />
                                        <TitleStyle BackColor="#D2E1FF" 
                                            Font-Bold="True" Font-Size="13pt" ForeColor="#003399" Height="14pt" />
    </asp:Calendar>
    
                                                <br />
                                                <asp:HiddenField ID="hdIdcursoVirtual" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdIdUsuario" runat="server" />
                                                <asp:HiddenField ID="hdIdAgenda" runat="server" />
                                                <asp:HiddenField ID="hdcodigo_tfu" runat="server" />
                                                <br />
                                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
                                                <br />
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" />
                                            </td>
                                            <td style="width: 70%" valign="top">
                                                <asp:DataList ID="DataList1" runat="server" BorderColor="#6699FF" 
                                                    BorderStyle="Solid" BorderWidth="1px" GridLines="Horizontal" Width="100%" 
                                                    DataKeyField="idagenda">
                                                    <HeaderTemplate>
                                                        Lista de actividades
                                                    </HeaderTemplate>
                                                    <HeaderStyle BackColor="#D2E1FF" Font-Bold="True" ForeColor="Black" 
                                                        Height="25px" />
                                                    <ItemTemplate>
                                                        <table style="width:100%">
                                                            <tr>
                                                                <td rowspan="3" style="width: 3%" valign="top">
                                                                    <img alt="" src="../../images/menu3.gif" /></td>
                                                                <td style="width: 93%">
                                                                    <asp:Label ID="lblcategoria" runat="server" Font-Bold="True" 
                                                                        Text='<%# eval("nombrecategoria") %>'></asp:Label>
                                                                    :
                                                                    <asp:Label ID="lbltitulo" runat="server" Font-Bold="True" ForeColor="Maroon" 
                                                                        Text='<%# eval("tituloagenda") %>'></asp:Label>
                                                                    .
                                                                    <asp:Label ID="lbldescripcion" runat="server" Text='<%# eval("descripcion") %>'></asp:Label>
                                                                    . <b>Lugar</b>:
                                                                    <asp:Label ID="lbllugar" runat="server" Text='<%# eval("nombrearea") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 93%; font-weight: 500; font-style: italic;">
                                                                    Registrado por:
                                                                    <asp:Label ID="lblnombreusuario" runat="server" 
                                                                        Text='<%# eval("nombreusuario") %>'></asp:Label>
                                                                    &nbsp;| Desde:<asp:Label ID="lblfechainicio" runat="server" 
                                                                        Text='<%# eval("fechainicio") %>'></asp:Label>
                                                                    &nbsp; hasta&nbsp;
                                                                    <asp:Label ID="lblfechafin" runat="server" Text='<%# eval("fechafin") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" style="width: 93%">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
    </table>
    </form>
</body>
</html>
