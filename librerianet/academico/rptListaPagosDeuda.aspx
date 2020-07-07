<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptListaPagosDeuda.aspx.vb" Inherits="academico_rptListaPagosDeuda" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnExportar" runat="server" Text="Exportar" Height="22px" Width="100px" CssClass="excel2" />
    <br /><br />
    <asp:GridView ID="gvPagoVsDeuda" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField HeaderText="Fec. Venc." DataField="fechaVencimiento_Deu">
                        <ItemStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Concepto" DataField="Concepto" />
                    <asp:BoundField HeaderText="Estado" DataField="estado_Deu">
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cargo" DataField="montoTotal_Deu">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Pago" DataField="Pago_Deu" >
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Saldo" DataField="saldo_Deu">
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Documento" DataField="descripcion_Tdo" >
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Fec. Doc." DataField="fecha_Cin" >                    
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Importe" HeaderText="Importe" >
                        <ItemStyle Width="60px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>  
                <RowStyle Height="20px"  />              
                <FooterStyle BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" 
                    HorizontalAlign="Center" />
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                    BorderWidth="1px" ForeColor="#3366CC" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
