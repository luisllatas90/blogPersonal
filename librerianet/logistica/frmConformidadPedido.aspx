<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConformidadPedido.aspx.vb" Inherits="logistica_frmConformidadPedido" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
	    function MarcarCursos(obj) {
	        //asignar todos los controles en array
	        var arrChk = document.getElementsByTagName('input');
	        for (var i = 0; i < arrChk.length; i++) {
	            var chk = arrChk[i];
	            //verificar si es Check
	            if (chk.type == "checkbox") {
	                if (chk.disabled == false) {
	                    chk.checked = obj.checked;
	                    if (chk.id != obj.id) {
	                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
	                    }
	                }
	            }
	        }
	    }

	    function PintarFilaMarcada(obj, estado) {
	        if (estado == true) {
	            obj.style.backgroundColor = "#FFE7B3"
	        } else {
	            obj.style.backgroundColor = "white"
	        }
	    }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="2" align="center">
                    <asp:Label ID="Label1" runat="server" Text="Conformidad de Pedido" 
                        Font-Bold="True" Font-Size="Small"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:10%">
                    <asp:Label ID="Label2" runat="server" Text="Pedido:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBuscar" runat="server"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buscar2" Width="100px" Height="22px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvCabecera" runat="server" Width="100%" 
                        AutoGenerateColumns="False" AllowPaging="True" 
                        DataKeyNames="CodigoPerCabecera">
                        <Columns>
                            <asp:BoundField DataField="codigo_Ped" HeaderText="Pedido" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FechaPed" HeaderText="Fecha" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CentroCostos" HeaderText="Centro Costos" />
                            <asp:CommandField ShowSelectButton="True" HeaderText="Seleccionar" >
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:BoundField DataField="CodigoPerCabecera" HeaderText="Personal Pedido" 
                                Visible="False" />
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" />
                        <EmptyDataTemplate>
                            <table width="100%>
                                <tr>
                                    <td colspan="4">No se encontraron pedidos</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <asp:HiddenField ID="HdPerCabecera" runat="server" />
                    <br />
                </td>                
            </tr>
            <tr>
                <td colspan="2">                
                    <br />
                    <asp:GridView ID="gvPedidos" runat="server" Width="100%" DataKeyNames="codigo_Dpe,codigo_Ped,codigo_Per" 
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField HeaderText="Detalle" DataField="codigo_Dpe" 
                                ItemStyle-HorizontalAlign="Center" Visible="False" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Articulo" DataField="descripcionArt" />
                            <asp:BoundField HeaderText="Unidad" DataField="descripcionuni" 
                                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cant. Pedida" DataField="cantidadPedida_Ecc" 
                                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Cant. Atendida" DataField="cantidadAtendida_Ecc" 
                                ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="descripcionEstado_Eped" HeaderText="Estado">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Despachar" DataField="Despachar" />
                            <asp:TemplateField HeaderText="" >
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>                
                                <asp:CheckBox ID="chkElegir" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                            <asp:BoundField DataField="codigo_Ped" HeaderText="Pedido" Visible="False" />
                            <asp:BoundField DataField="codigo_Per" HeaderText="Personal" Visible="False" />
                        </Columns>
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" />
                    </asp:GridView>                
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">                
                    <asp:Button ID="btnConfirma" runat="server" Text="Confirmar recepción" CssClass="guardar2" Height="22px" Width="140px" />
                </td>
            </tr>
            <tr>
                <td colspan="2">                
                <asp:Label ID="lblAviso" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
