<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmResumenActividad.aspx.vb" Inherits="PlanProyecto_frmResumen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Resumen de Actividad</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />   
    <script type="text/javascript" src="../../private/calendario.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">        
        <tr>
            <td colspan="3" align="left" style="width:50%">
                <asp:Label ID="Label9" runat="server" Text="Registro de Actividades" 
                    Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
            <td align="right">
                <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="modificar2" Width="100px" Height="22px" />
                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="eliminar2" Width="100px" Height="22px" />                
            </td>
        </tr>
        <tr>
            <td style="width: 20%; height: 22px">
                <asp:Label ID="Label1" runat="server" Text="Proyecto:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:Label ID="lblProyecto" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label2" runat="server" Text="Título:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtTitulo" runat="server" Width="80%" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label3" runat="server" Text="Descripción:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" 
                    Width="80%" Enabled="False" Height="45px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label4" runat="server" Text="F. Inicio:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server" Enabled="False"></asp:TextBox>
                &nbsp;</td>
              <td style="width: 20%">
                <asp:Label ID="Label5" runat="server" Text="F. Fin:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server" Enabled="False"></asp:TextBox>
                &nbsp;</td>   
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label6" runat="server" Text="Prioridad:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dpPrioridad" runat="server" Width="40%" Enabled="False">
                    <asp:ListItem Value="A" Text="ALTA"></asp:ListItem>
                    <asp:ListItem Value="M" Text="MEDIA"></asp:ListItem>
                    <asp:ListItem Value="B" Text="BAJA"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td style="width: 20%">
                <asp:Label ID="Label10" runat="server" Text="Nro. Orden:" Font-Bold="true"></asp:Label>
            </td>
            <td>                
                <asp:TextBox ID="txtOrden" runat="server" Enabled="False"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label7" runat="server" Text="Avance:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAvance" runat="server" Enabled="False"></asp:TextBox>
            </td>            
            <td style="width: 20%">
                <asp:Label ID="Label11" runat="server" Text="Proceso:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkProceso" runat="server" Enabled="False" />          
            </td>  
        </tr> 
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label12" runat="server" Text="Mostrar Dias:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkDias" runat="server" Enabled="False" />
            </td>
            <td style="width: 20%">
                <asp:Label ID="Label13" runat="server" Text="Visible:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkVisible" runat="server" Enabled="False" />
            </td>
        </tr>       
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label8" runat="server" Text="Depende:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDepende" runat="server" Width="80%" Enabled="False"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td style="width: 20%">&nbsp;</td>            
            <td colspan="3">
                <br />
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">&nbsp;
                <asp:GridView ID="gvDetalle" runat="server" Width="95%" AllowPaging="True" 
                    AutoGenerateColumns="False" PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="codigo_res" HeaderText="Codigo" Visible="False" />
                        <asp:BoundField DataField="Nombre" HeaderText="Responsable" />
                    </Columns>
                <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                <RowStyle Height="22px" />
                </asp:GridView>
            </td>
        </tr>        
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True"></asp:Label>&nbsp;</td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                &nbsp;
                </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCodigo_apr" runat="server" />
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    </form>
</body>
</html>