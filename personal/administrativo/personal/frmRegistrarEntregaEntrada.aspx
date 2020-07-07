<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistrarEntregaEntrada.aspx.vb" Inherits="administrativo_personal_frmRegistrarEntregaEntrada" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            font-family: Verdana;
            font-size: small;
        }
    </style>
    
    <script type="text/javascript">
        function confirmar() {            
            return confirm('¿Desea registrar entrega?');
        }
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div class="style1">
    
        Entrega de Entradas White Party<br />
        <br />
        Nombres / DNI
        <asp:TextBox ID="txtCadena" runat="server" Width="442px"></asp:TextBox>
&nbsp;<asp:Button ID="CmdBuscar" runat="server" Text="Buscar" />
        <br />
        <asp:GridView ID="dgvEntradas" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <Columns>
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                <asp:BoundField DataField="Persona" HeaderText="Persona">
                <ItemStyle Font-Bold="True" ForeColor="#3333FF" />
                </asp:BoundField>
                <asp:BoundField DataField="DNI" HeaderText="DNI" />
                <asp:BoundField DataField="Código" HeaderText="Código" />
                <asp:BoundField DataField="Escuela" HeaderText="Escuela/Área" />
                <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                <asp:BoundField DataField="Pago" HeaderText="Pago" />
                <asp:BoundField DataField="Saldo" HeaderText="Saldo" />
                <asp:BoundField DataField="Entrada" HeaderText="Entrada">
                <ItemStyle Font-Bold="True" ForeColor="Blue" />
                </asp:BoundField>
                <asp:BoundField DataField="EntradasCompradas" HeaderText="Entradas Compradas">
                <HeaderStyle BackColor="#66CCFF" ForeColor="Black" />
                <ItemStyle BackColor="#66CCFF" HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="EntradasEntregadas" HeaderText="Entradas Entregadas">
                <HeaderStyle BackColor="#CCCCFF" ForeColor="Black" />
                <ItemStyle BackColor="#CCCCFF" Font-Bold="True" HorizontalAlign="Center" />
                </asp:BoundField>
               
                <asp:CommandField HeaderText="ENTREGAR ENTRADA" SelectText="Entregar" 
                    ShowSelectButton="True" />
               
                <asp:BoundField DataField="codigo_deu" HeaderText="..." ReadOnly="True" 
                    ShowHeader="False" >
                <ControlStyle Width="0px" />
                <ItemStyle BackColor="White" ForeColor="White" Width="0px" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <EditRowStyle Font-Size="XX-Small" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
    
    </div>
    </form>
</body>
</html>
