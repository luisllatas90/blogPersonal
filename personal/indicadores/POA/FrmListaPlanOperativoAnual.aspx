<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaPlanOperativoAnual.aspx.vb" Inherits="indicadores_POA_FrmListaPlanOperativoAnual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField id="foco" runat="server"  />
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Plan Operativo Anual"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
        <tr>
        <td width="100px" >Plan Estratégico</td>
        <td><asp:DropDownList ID="ddlPlan" runat="server" Width="400"></asp:DropDownList></td>
        <td>&nbsp; Ejercicio Presupuestal&nbsp;</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="150px"></asp:DropDownList></td>
        <td>&nbsp; Vigencia &nbsp;</td>
        <td>
            <asp:DropDownList ID="ddlVigencia" runat="server" Width="150px">
            <asp:ListItem Value=1>Vigente</asp:ListItem>
            <asp:ListItem Value=0>No Vigente</asp:ListItem>
            <asp:ListItem Value=2>Todos</asp:ListItem>
            </asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr style="height:1px;">
        <td colspan="7">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="7"><asp:Button ID="btnNuevo" runat="server" Text="    Nuevo Plan" CssClass="btnNuevo" /></td>
        </tr>
            <tr>
                <td colspan="7">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvwPOA" runat="server" Width="100%" 
            AutoGenerateColumns="False" DataKeyNames="codigo_poa" CellPadding="3" >
            <Columns>
                <asp:BoundField HeaderText="PEI/PEF" DataField="pei" >
                <HeaderStyle Width="350px" />
                <ItemStyle CssClass="celda_combinada" />
                </asp:BoundField>
                <asp:BoundField DataField="nombre_poa" HeaderText="NOMBRE DE POA" >
                <HeaderStyle Width="450px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable" >
                <HeaderStyle Width="300px" />
                </asp:BoundField>
                <asp:BoundField HeaderText="EJERCICIO " DataField="ejercicio" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:BoundField>
                <asp:BoundField DataField="codigo_pla" HeaderText="codigo_pla" Visible="False" />
                <asp:BoundField DataField="codigo_ejp" HeaderText="codigo_ejp" Visible="False" />
                <asp:BoundField DataField="codigo_arp" HeaderText="codigo_arp" Visible="False" />      
                <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
                    HeaderText="EDITAR" SelectImageUrl="../../../images/editar_poa.png" 
                    UpdateImageUrl="../../images/editar.gif" SelectText="Editar" >
                <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:CommandField>
                <asp:TemplateField HeaderText="ELIMINAR" ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            CommandName="Delete" ImageUrl="../../Images/menus/noconforme_small.gif" alternateText="Eliminar" OnClientClick="return confirm('¿Desea Eliminar Registro?.')" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
           
                No se Encontraron Registros
            </EmptyDataTemplate>
            <EmptyDataRowStyle BackColor="Black"  />
            <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" /> 
        </asp:GridView>
        <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
        <table width="95%">
        <tr >
        <td runat="server" id="aviso_contador"><asp:Label ID="lblmensaje" runat="server" ></asp:Label></td>
        </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
