<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LIBROSPEDIDOS.aspx.vb" Inherits="Biblioteca_LIBROSPEDIDOS" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script src="../../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
    <link href="../private/estilo.css" type="text/css" rel="Stylesheet" />
</head>
<body style="margin:0" >
    <form id="form1" runat="server">

      <%  response.write(clsfunciones.cargacalendario) %>
    <table style="width:100%;" border="0" cellpadding="5" cellspacing="0">
        <tr>
            <td bgcolor="#F0F0F0" align="center" class="usatTitulousat">

    
                <asp:Label ID="LblTitulo" runat="server" Text="REPORTE DE LIBROS"></asp:Label>
    

                    </td>
        </tr>
        <tr>
            <td bgcolor="#F0F0F0">

    
    <table align="center" width="98%" bgcolor="#F0F0F0" border="0" cellpadding="0" 
                    cellspacing="0">
        <tr>
            <td width="90px">
                Consultar por</td>
            <td >
                :</td>
            <td colspan="2" >
                <asp:DropDownList ID="CboConsultar" runat="server" AutoPostBack="True" 
                    Width="135px">
                    <asp:ListItem Value="0">Todos</asp:ListItem>
                    <asp:ListItem Value="1">Por Titulo</asp:ListItem>
                </asp:DropDownList>
            &nbsp;<asp:TextBox ID="txtTextoBus" runat="server" Width="232px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="90px" >
                Ver</td>
            <td>
                :</td>
            <td>
                <asp:DropDownList ID="CboVer" runat="server" AutoPostBack="True" Width="135px">
                    <asp:ListItem Value="TO">Todos</asp:ListItem>
                    <asp:ListItem Value="PR">Los mas prestados</asp:ListItem>
                    <asp:ListItem Value="NP">Los no prestados</asp:ListItem>
                </asp:DropDownList>
                            &nbsp;<asp:Label ID="LblSigno" runat="server" Font-Bold="True"></asp:Label>
&nbsp;
                <asp:TextBox ID="TxtCantidad" runat="server" Width="52px"></asp:TextBox>
                            </td>
            <td align="right">
                <asp:Button ID="CmdBuscar" runat="server"  Text="Buscar" CssClass="buscar" 
                    Width="80px" Height="20px" EnableViewState="False" />
                            </td>
        </tr>
        <tr>
            <td width="90px" >
                                <asp:CheckBox ID="ChkFechas" runat="server" Text="Por fechas" 
                                    EnableViewState="False" />
                            </td>
            <td>
                &nbsp;</td>
            <td >
                &nbsp;Fecha de Inicio:                 <asp:TextBox ID="txtFinicio" runat="server" Width="88px"></asp:TextBox>
                <input id="Button3" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFinicio,'dd/mm/yyyy')" 
                    style="height: 22px" />&nbsp; Fecha fin:
                <asp:TextBox ID="txtFfin" runat="server" Width="87px"></asp:TextBox>
                <input id="Button2" type="button"  class="cunia" 
                    onclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFfin,'dd/mm/yyyy')" 
                    style="height: 22px" /></td>
            <td align="right">
            &nbsp;<asp:Button ID="CmdExportar" runat="server" Text="Exportar" 
                    CssClass="Exportar" Height="20px" Width="80px" EnableViewState="False" />
                </td>
        </tr>
        </table>
    

                    </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="LblTotal" runat="server" ForeColor="#0000CC"></asp:Label>
&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
        <asp:GridView ID="GvLibros" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None" Width="100%" EnableViewState="False" 
            AutoGenerateColumns="False" style="margin-bottom: 0px" AllowPaging="True" 
              PageSize="100">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Cod. Barras" HeaderText="Cod. Barras" />
                <asp:BoundField HeaderText="Codigo Dewey" />
                <asp:BoundField DataField="Titulo" HeaderText="Título" />
                <asp:BoundField DataField="Autor" HeaderText="Autor" />
                <asp:BoundField DataField="Nro prestamos" HeaderText="Nro prestamos" />
                <asp:BoundField DataField="F. Reg Compra" HeaderText="F. Reg Compra" />
                <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                <asp:BoundField DataField="DescripcionFormato" HeaderText="Formato" />
                <asp:BoundField DataField="Tipo Adq" HeaderText="Tipo Adq" />
                <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                No se encontraron registros
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    
          
    
        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" Text="Se está procesando su información" 
        Title="Por favor espere" />
    </form>
</body>
</html>
