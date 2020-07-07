<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetallePedido.aspx.vb" Inherits="Odontologia_frmDetallePedido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css"rel="stylesheet" type="text/css" />     
    
</head>
<body>
    <form id="form1" runat="server">
    <div>        
        <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
        <br /><br />
        <asp:GridView ID="gvDetalle" runat="server" Width="100%" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="codigo_dpo" HeaderText="Detalle" />
                <asp:BoundField DataField="descripcionArt" HeaderText="Articulo" />
                <asp:BoundField DataField="cantidad_dpo" HeaderText="Cantidad" />
                <asp:BoundField DataField="cantMaxima_pod" HeaderText="Cant. Máxima" />
                <asp:TemplateField HeaderText="Cant. Permitida">
                <ItemStyle HorizontalAlign="Right" />
                <ItemTemplate>
                    <asp:TextBox ID="txtPermitido" EnableViewState="true" Width="70px" Height="20px" runat="server" Text='<%#  DataBinder.Eval(Container, "DataItem.cantMaxima_pod") %>'>
                    </asp:TextBox>
                </ItemTemplate>            
            </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#e33439" ForeColor="White" Height="25px" />                
        </asp:GridView>
    </div>
    <br />
    <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="guardar" Width="100px" Height="22px" />
    </form>
</body>
</html>
