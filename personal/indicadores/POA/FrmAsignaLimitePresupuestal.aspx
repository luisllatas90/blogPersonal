<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAsignaLimitePresupuestal.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label2" runat="server" Text="Asignar Limite de Presupuesto POA"></asp:Label>
    </div>
     <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>Plan Estratégico</td>
                <td><asp:DropDownList ID="ddlPlan" runat="server" Width="400"></asp:DropDownList>&nbsp;</td>
                <td>Ejercicio </td>
                <td><asp:DropDownList ID="ddlEjercicio" runat="server" Width="100"></asp:DropDownList></td>
                <td>Estado</td>
                <td><asp:DropDownList ID="ddlestado" runat="server" Width="250">
                    <asp:ListItem Value="1">Sin Limite y/o Sin Centro de Costo</asp:ListItem>
                    <asp:ListItem Value="2">sin Limite</asp:ListItem>
                    <asp:ListItem Value="3">Sin Centro de Costo</asp:ListItem>
                    <asp:ListItem Value="4">Asignados</asp:ListItem>
                    <asp:ListItem Value="5">Todos</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
            <tr>
                <td colspan="7">
                    <div runat="server" id="aviso">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr> 
            <tr>
                <td colspan="7">

                    &nbsp;</td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
            </tr>       
                
<tr>
    <td colspan="7">
        
        &nbsp;</td>
</tr>          
        </table>
         <asp:Label ID="lblMensajeFormulario" runat="server" Text=""></asp:Label>
         <br />
    </div>

        <asp:HiddenField ID="HdCodigo_poa" runat="server" />
        
        <asp:HiddenField ID="HdCodigo_cco" runat="server" />
        <asp:HiddenField ID="HdNombre_cco" runat="server" />

        
<asp:GridView ID="dgvAsignar" runat="server" Width="100%" CellPadding="3"
    AutoGenerateColumns="False" DataKeyNames="codigo_poa,codigo_cco,nombre_cco">
    <Columns>
        <asp:BoundField HeaderText="PLAN OPERATIVO ANUAL" DataField="nombre_poa" />
        <asp:BoundField HeaderText="RESPONSABLE" DataField="responsable" />
        <asp:BoundField HeaderText="EJERCICIO" DataField="descripcion_ejp" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField HeaderText="LIMITE INGRESO" DataField="limite_ingreso" 
            DataFormatString="{0:N}"  >
        <ItemStyle HorizontalAlign="Right" Width="80px" />
        </asp:BoundField>
        <asp:BoundField DataField="limite_egreso" HeaderText="LIMITE EGRESO" 
            DataFormatString="{0:N}">
        <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="utilidad" HeaderText="UTILIDAD" 
            DataFormatString="{0:N}" Visible="False">
        <ItemStyle Width="80px" HorizontalAlign="Right" />
        </asp:BoundField>
         
        <asp:CommandField ShowSelectButton="true" ButtonType="Image"  
            HeaderText="DETALLES" SelectImageUrl="../../../images/editar_poa.png" 
            UpdateImageUrl="../../images/editar.gif" SelectText="Editar" >
        <ItemStyle HorizontalAlign="Center" />
        </asp:CommandField>
        
                        
        <asp:BoundField DataField="codigo_poa" HeaderText="codigo_poa" 
            Visible="False" />
        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" 
            Visible="False" />
        <asp:BoundField DataField="nombre_cco" HeaderText="nombre_cco" 
            Visible="False" />
    </Columns>

<HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" /> 
</asp:GridView>

        
    </form>
</body>
</html>
