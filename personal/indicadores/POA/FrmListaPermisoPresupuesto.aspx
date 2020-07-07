<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaPermisoPresupuesto.aspx.vb" Inherits="indicadores_POA_FrmListaEvaluarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Activar Edición de Presupuesto de Programa/Proyecto"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
        <tr style="height:30px;">
        <td width="140px" >Plan Estratégico</td>
        <td width="510px"><asp:DropDownList ID="ddlplan" runat="server" Width="500" AutoPostBack="true"></asp:DropDownList></td>
        <td width="50px"></td>
        <td width="140px">Ejercicio Presupuestal</td>
        <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true"></asp:DropDownList></td>
        <td><asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" /></td>
        </tr>
        <tr>
        <td>Plan Operativo Anual</td>
        <td>
        <asp:DropDownList ID="ddlPoa" runat="server" Width="500">
        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
        </asp:DropDownList> 
        </td>
        <td width="50px"></td>
        <td></td>
         <td>
        </td>
        <td></td>
        </tr>
        
        <tr style="height:15px">
        <td></td>
        <td colspan="5" align="right">
         <asp:Button ID="cmdGuardar" runat="server" Text="  Guardar" CssClass="btnGuardar" Visible="false" />
        </td>
        </tr>
         <tr>
            <td colspan="6">
                <div runat="server" id="aviso">
                    <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                </div>
            </td> 
        </tr>
        </table>
       <asp:GridView ID="dgvActividades" runat="server" Width="100%"
            AutoGenerateColumns="False"
            datakeynames="codigo_acp" HeaderStyle-Height="20px"
            CellPadding="4">
            <Columns>
                <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" DataField="nombre_poa" >
                <HeaderStyle Width="40%" />
                </asp:BoundField>
                <asp:BoundField HeaderText="PROGRAMA/PROYECTO" DataField="resumen_acp" >
                <HeaderStyle Width="50%" />
                </asp:BoundField>
                 <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkHeader" runat="server"  ToolTip="Seleccionar" AutoPostBack="true" OnCheckedChanged="chbSeleccionTodo_CheckedChanged" />
                    </HeaderTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>
                         <asp:CheckBox ID="checkbody" runat="server" Checked='<%# Bind("AutorizaDirFin") %>' />
                     </ItemTemplate>
                 </asp:TemplateField>
            </Columns>
        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" /> 
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" /> 
        <EmptyDataTemplate>Registros Visibles Solo Para Dirección/Director de Finanzas </EmptyDataTemplate>
        </asp:GridView>
    </form>
</body>
</html>
