<%@ Page Language="VB" AutoEventWireup="false" CodeFile="fichaseguimientoparticipante.aspx.vb" Inherits="librerianet_aulavirtual_eva_fichaseguimientoparticipante" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ficha de Seguimiento</title>
     <link href="../../../private/estiloaulavirtual.css" rel="stylesheet" 
        type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" class="contornotabla">
        <tr>
            <td rowspan="4" valign="top" width="20%">
                <asp:Image ID="imgFoto" runat="server" Height="128px" Width="110px" />
            </td>
            <td width="80%">
                <asp:Label ID="Label1" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="80%">
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="80%">
                <asp:Label ID="Label3" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="80%">
                &nbsp;</td>
        </tr>
    </table>
    <p class="e4">
        Fechas de ingreso a la plataforma virtual del curso</p>
    <p>
        <asp:GridView ID="Visitas" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" BorderColor="#666666" 
            BorderStyle="Solid" BorderWidth="1px">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField HeaderText="#" />
                <asp:BoundField DataField="fechaentrada" HeaderText="Fecha de Entrada" />
                <asp:BoundField DataField="visitas" HeaderText="Veces" />
            </Columns>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <EmptyDataTemplate>
                <h5><b>No se han encontrado registros de acceso.</b></h5>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </p>
    <p class="e4">
        <asp:Label ID="lbldocumentos" runat="server" Text="Bitácora de acceso a recursos publicados" 
            Visible="False"></asp:Label>
    </p>
    <asp:GridView ID="Documentos" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" BorderStyle="Solid" BorderWidth="1px" 
        Width="100%">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField HeaderText="#" >
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="recurso" HeaderText="Tipo" >
                <ItemStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField DataField="nombrerecurso" HeaderText="Descripción del recurso" >
                <ItemStyle Width="70%" />
            </asp:BoundField>
            <asp:BoundField DataField="veces" HeaderText="Veces">
                <ItemStyle Width="5%" />
            </asp:BoundField>
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <EmptyDataTemplate>
            <h5>
                <b>No se han registrado documentos que haya descargado.</b></h5>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
