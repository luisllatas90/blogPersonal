<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroGrupo.aspx.vb" Inherits="PlanProyecto_frmRegistroGrupo" %>

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
                <td colspan="2" align="center"><b>REGISTRO DE NUEVO GRUPO</b></td>                
            </tr>
            <tr style="height: 22px">
                <td style="width:20%">
                    <asp:Label ID="Label1" runat="server" Text="Plan: "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblPlan" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label3" runat="server" Text="Nombre: "></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" Width="54.8%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label2" runat="server" Text="Descripción: "></asp:Label></td>
                <td>
                    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Width="54.8%"></asp:TextBox>
                </td>
            </tr>    
            <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Tipo de Proyecto:"></asp:Label></td>               
                <td>
                    <asp:DropDownList ID="cboTipo" runat="server" Width="55.9%">
                    </asp:DropDownList>
                </td>
            </tr>                 
            <tr>
                <td style="width:20%">
                    <asp:Label ID="Label7" runat="server" Text="Personal:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPersonal" runat="server" Width="54.8%"></asp:TextBox>
&nbsp;&nbsp;
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="buscar2" Width="90px" Height="22px" />
                &nbsp;<asp:Button ID="btnAddDetalle" runat="server" Text="Agregar" CssClass="agregar2" Width="90px" Height="22px" />
                </td>
            </tr>
            <tr>
                <td style="width:20%"></td>
                <td>
                    <asp:Label ID="lblFiltro" runat="server" Text="Nombre o DNI: "></asp:Label>
                    <asp:TextBox ID="txtFiltro" runat="server" Width="45%"></asp:TextBox>
                    <asp:Button ID="btnFiltro" runat="server" Text="Buscar" Width="100px" Height="22px" CssClass="buscar2" />
                </td>
            </tr>
            <tr>
                <td style="width:20%"></td>
                <td>
                    <asp:GridView ID="gvPersonal" runat="server" Width="100%" 
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
                                    <asp:Label ID="Label8" runat="server" Text="Personal" ForeColor="White" 
                                        Width="100%"></asp:Label>
                                </td>                            
                                <td align="center" style="width:20%">
                                    <asp:Label ID="Label9" runat="server" Text="Seleccionar" ForeColor="White" 
                                        Width="100%"></asp:Label>
                                </td>                                
                            </tr>                            
                        </table>
                        No se encontraron registros
                    </EmptyDataTemplate> 
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width:20%">
                    <asp:Label ID="lblIntegrantes" runat="server" Text="Integrantes: "></asp:Label>
                </td>
                <td></td>
            </tr>
            <tr>
                <td style="width:20%">&nbsp;</td>
                <td>                    
                    <asp:GridView ID="gvDetalle" runat="server" Width="100%" 
                        AutoGenerateColumns="False" DataKeyNames="codigo_gpd">
                        <Columns>
                            <asp:BoundField DataField="codigo_gpd" HeaderText="codigo_gpd" 
                                Visible="False" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Personal" />
                            <asp:CommandField ShowDeleteButton="True" ButtonType="Image"
                                DeleteImageUrl="../../images/eliminar.gif" HeaderText="Eliminar">                                
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
                                    <asp:Label ID="Label3" runat="server" Text="Eliminar" ForeColor="White" Width="100%"></asp:Label>
                                </td>                                
                            </tr>                            
                        </table>
                        El grupo no tiene integrantes
                    </EmptyDataTemplate> 
                        <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                        <RowStyle Height="22px" />
                    </asp:GridView>                    
                </td>
            </tr>
            <tr>
                <td style="width:20%">&nbsp;</td>
                <td>                    
                    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width:20%">&nbsp;</td>
                <td>                    
                    <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="guardar" Width="100px" Height="22px" />&nbsp;&nbsp;
                    <asp:Button ID="btnCancelar" runat="server" Text="Regresar" CssClass="salir" Width="100px" Height="22px" />
                </td>
            </tr>            
        </table>
    </div>
    <asp:HiddenField ID="HdCodigo_per" runat="server" />
    <asp:HiddenField ID="HdAccion" runat="server" />
    <asp:HiddenField ID="HdCodigo_gpr" runat="server" />
    <asp:HiddenField ID="HdCodigo_pro" runat="server" />
    </form>
</body>
</html>
