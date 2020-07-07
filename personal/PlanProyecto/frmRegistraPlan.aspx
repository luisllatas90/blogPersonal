<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistraPlan.aspx.vb" Inherits="PlanProyecto_frmRegistraPlan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />   
    <script src="../../private/calendario.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr style="height: 22px">            
            <td colspan="2" align="center">
                <asp:Label ID="Label1" runat="server" Text="Registro de Plan" Font-Bold="True" 
                    Font-Size="Medium"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                <asp:Label ID="Label8" runat="server" Text="Título:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtTitulo" runat="server" Width="54.8%"></asp:TextBox>
            </td>
        </tr> 
        <tr>
            <td style="width:20%">
                <asp:Label ID="Label9" runat="server" Text="Descripción:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDescripcion" runat="server" Width="54.8%" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                <asp:Label ID="Label10" runat="server" Text="F. Inicio:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtInicio" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtInicio')" type="button" value="..." />
            </td>
        </tr> 
        <tr>
            <td style="width:20%">
                <asp:Label ID="Label11" runat="server" Text="F. Final:"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFinal" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFinal')" type="button" value="..." />
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                <asp:Label ID="Label12" runat="server" Text="Centro de Costo:"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dpCentroCosto" runat="server">
                </asp:DropDownList>
            </td>
        </tr> 
        <tr>
                <td style="width:20%">
                    <asp:Label ID="Label7" runat="server" Text="Responsable:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPersonal" runat="server" Width="54.8%"></asp:TextBox>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buscar2" Width="100px" Height="22px" />
                </td>
            </tr>
            <tr>
                <td style="width:20%"></td>
                <td>
                    <asp:Label ID="lblFiltro" runat="server" Text="Nombre o DNI: "></asp:Label>
                    <asp:TextBox ID="txtFiltro" runat="server" Width="44.1%"></asp:TextBox>
                    <asp:Button ID="btnFiltro" runat="server" Text="Buscar" Width="100px" Height="22px" CssClass="buscar2" />
                </td>
            </tr>
            <tr>
                <td style="width:20%"></td>
                <td><asp:GridView ID="gvPersonal" runat="server" Width="100%" 
                        AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="codigo_per" 
                        PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="codigo_per" HeaderText="Codigo" Visible="False" />
                        <asp:BoundField DataField="NombrePersonal" HeaderText="Personal" />
                        <asp:CommandField ButtonType="Image" HeaderText="Seleccionar" 
                            SelectImageUrl="../../images/ok.gif" ShowSelectButton="True">
                               <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:CommandField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="background:#0B3861" width="100%">
                            <tr style="height:25px">
                                <td align="center">
                                    <asp:Label ID="Label2" runat="server" Text="Personal" ForeColor="White" Width="100%"></asp:Label>
                                </td>                            
                                <td align="center" style="width:20%">
                                    <asp:Label ID="Label3" runat="server" Text="Seleccionar" ForeColor="White" Width="100%"></asp:Label>
                                </td>                                
                            </tr>                            
                        </table>
                        No se encontraron registros
                    </EmptyDataTemplate> 
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                    </asp:GridView></td>
            </tr>        
        <tr>
            <td style="width:20%"></td>
            <td>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:20%">&nbsp;</td>
            <td>
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" Width="100px" Height="22px" CssClass="guardar" />&nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Width="100px" Height="22px" CssClass="salir" />
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdCodigo_per" runat="server" />
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    </form>
</body>
</html>
