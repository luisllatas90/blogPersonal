<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGeneraServicio.aspx.vb" Inherits="administrativo_controlpagos_frmGeneraServicio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>    
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
        <br />
        <asp:Label ID="lblCiclo" runat="server"></asp:Label>
    <asp:GridView ID="gvDetalle" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_dsc">
        <Columns>
            <asp:BoundField DataField="codigo_dsc" HeaderText="Codigo" Visible="False">
                
            </asp:BoundField>
            <asp:BoundField DataField="descripcion_Sco" HeaderText="Descripcion" />
            <asp:BoundField DataField="nropartes_dsc" HeaderText="Nro. Parte" />
            <asp:BoundField DataField="FechaInicio" HeaderText="F. Inicio" />
            <asp:BoundField DataField="FechaPago" HeaderText="F. Pago" />
            <asp:BoundField DataField="FechaVencimiento" HeaderText="F. Vence" />
            <asp:CommandField ShowEditButton="True" HeaderText="Editar" />
            <asp:CommandField ShowDeleteButton="True" HeaderText="Eliminar" />            
        </Columns>
        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
        <RowStyle Height="22px" />
    </asp:GridView>
    <br />    
        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Detalle" CssClass="agregar2" Width="120px" Height="22px" />
    <br />    
    </div>    
    <table width="100%">        
        <tr>
            <td></td>
            <td colspan="3">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="width:25%">
                <asp:Label ID="Label6" runat="server" Text="Nro. Parte:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPartes" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%">
                <asp:Label ID="Label7" runat="server" Text="Monto:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td style="width:25%">
                <asp:Label ID="Label2" runat="server" Text="F. Inicio:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtInicio" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%">
                <asp:Label ID="Label3" runat="server" Text="F. Vencimiento:"></asp:Label></td>
            <td>
                <asp:TextBox ID="txtVence" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:25%">
                <asp:Label ID="Label4" runat="server" Text="F. Pago:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPago" runat="server"></asp:TextBox>
            </td>
            <td style="width:25%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>                     
        <tr>
            <td style="width:25%">
                <asp:Label ID="Label1" runat="server" Text="Servicio Concepto:" Font-Bold="True"></asp:Label></td>
            <td colspan="3">
                <asp:Button ID="btnAgregarConcepto" runat="server" Text="Agregar Concepto" CssClass="agregar2" Width="120px" Height="22px"/>
                <asp:GridView ID="GridView1" runat="server" Width="100%">
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                </asp:GridView>
                <asp:TextBox ID="txtConcepto" runat="server" Width="80%"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />                                
                <asp:GridView ID="gvConceptos" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="codigo_Sco">
                    <Columns>
                        <asp:BoundField HeaderText="Concepto" DataField="descripcion_Sco" />
                        <asp:BoundField DataField="precio_Sco" HeaderText="Precio" />
                        <asp:BoundField DataField="fechaInicio_Sco" HeaderText="F. Inicio" />
                        <asp:BoundField DataField="fechaFin_Sco" HeaderText="F. Vence" />
                        <asp:CommandField HeaderText="Seleccionar" ShowSelectButton="True" />
                    </Columns>
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="4" align="center">

                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" />
&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
            </td>            
        </tr>                     
    </table>
    <asp:HiddenField ID="HdCodigo_cac" runat="server" />
    <asp:HiddenField ID="HdCodigo_sco" runat="server" />
    <asp:HiddenField ID="HdAccion" runat="server" />
    </form>
</body>
</html>
