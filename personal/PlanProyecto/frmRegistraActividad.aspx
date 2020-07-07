<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistraActividad.aspx.vb" Inherits="PlanProyecto_frmRegistraActividad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />   
    <script src="../../private/calendario.js"></script>
    <style type="text/css">
        .style1
        {
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td colspan="5" align="center" class="style1">
                <asp:Label ID="lblTitulo" runat="server" Text="Registro de Actividades" 
                    Font-Bold="True" Font-Size="Medium"></asp:Label>
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
                <asp:TextBox ID="txtTitulo" runat="server" Width="80%" MaxLength='100'></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label3" runat="server" Text="Descripción:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Width="80%" MaxLength='250'></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label4" runat="server" Text="F. Inicio:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaInicio')" type="button" value="..." />
            </td>
            <td style="width: 20%">
                <asp:Label ID="Label7" runat="server" Text="Avance:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtAvance" runat="server"></asp:TextBox>
            </td>
        </tr>        
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label5" runat="server" Text="F. Fin:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                <input onclick="MostrarCalendario('txtFechaFin')" type="button" value="..." />
            </td>             
            <td style="width: 20%">
                <asp:Label ID="Label9" runat="server" Text="Nro. Orden:" Font-Bold="true"></asp:Label>
            </td>
            <td>                
                <asp:TextBox ID="txtOrden" runat="server"></asp:TextBox>                
            </td>
        </tr>        
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label6" runat="server" Text="Prioridad:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="dpPrioridad" runat="server" Width="40%">
                    <asp:ListItem Value="A" Text="ALTA"></asp:ListItem>
                    <asp:ListItem Value="M" Text="MEDIA"></asp:ListItem>
                    <asp:ListItem Value="B" Text="BAJA"></asp:ListItem>
                </asp:DropDownList>
            </td>        
            <td style="width: 20%">
                <asp:Label ID="Label11" runat="server" Text="Proceso:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkProceso" runat="server" />          
            </td>            
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label12" runat="server" Text="Mostrar Dias:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkDias" runat="server" />
            </td>
            <td style="width: 20%">
                <asp:Label ID="Label13" runat="server" Text="Visible:" Font-Bold="true"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkVisible" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label14" runat="server" Text="Feriado:" Font-Bold="True"></asp:Label>
            </td>
            <td>
                <asp:CheckBox ID="chkFeriado" runat="server" />
            </td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label8" runat="server" Text="Depende:" Font-Bold="True"></asp:Label>
            </td>
            <td colspan="3">
                <asp:DropDownList ID="dpDependiente" runat="server" Width="81.5%">
                </asp:DropDownList>
            </td>
        </tr>        
        <tr>
            <td style="width: 20%">
                <asp:Label ID="Label10" runat="server" Font-Bold="True" Text="Responsable:"></asp:Label>
            </td>            
            <td colspan="3">
                <asp:TextBox ID="txtResponsable" runat="server" Width="70%"></asp:TextBox>
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" Height="22px" CssClass="buscar2" />
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="100px" Height="22px" CssClass="agregar2" />
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>            
            <td colspan="3">
                <asp:Label ID="lblFiltro" runat="server" Text="Apellidos y/o Nombre:"></asp:Label>
                <asp:TextBox ID="txtFiltro" runat="server" Width="40%"></asp:TextBox>
                <asp:Button ID="btnFiltro" runat="server" Text="Buscar" CssClass="buscar2" Width="100px" Height="22px" /><br />
                <asp:GridView ID="gvResponsables" runat="server" Width="100%" 
                    AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="Codigo,Tipo" 
                    PageSize="5">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="False" />
                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" Visible="False" />
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                        <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                            HeaderText="Seleccionar" SelectImageUrl="../../images/ok.gif">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:CommandField>
                    </Columns>
                <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                <RowStyle Height="22px" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="width: 20%">&nbsp;</td>
            <td colspan="3">
                <asp:GridView ID="gvDetalle" runat="server" Width="100%" AllowPaging="True" 
                    AutoGenerateColumns="False" PageSize="5" DataKeyNames="codigo_res">
                    <Columns>
                        <asp:BoundField DataField="codigo_res" HeaderText="Codigo" Visible="False" />
                        <asp:BoundField DataField="Nombre" HeaderText="Responsable" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            DeleteImageUrl="../../images/eliminar.gif" HeaderText="Eliminar">
                            <ItemStyle HorizontalAlign="Center" Width="15%" />
                        </asp:CommandField>
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
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="guardar" Width="100px" Height="22px" />&nbsp;
                <asp:Button ID="btnCancelar" runat="server" Text="Cerrar" CssClass="salir" 
                    Width="100px" Height="22px" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdCodigo_Res" runat="server" />
    <asp:HiddenField ID="HdTipo_Res" runat="server" />
    <asp:HiddenField ID="HdCodigo_apr" runat="server" />
    <asp:HiddenField ID="HdAccion" runat="server" />
    </form>
</body>
</html>
