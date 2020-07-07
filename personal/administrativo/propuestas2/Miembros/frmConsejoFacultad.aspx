<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsejoFacultad.aspx.vb" Inherits="administrativo_propuestas2_Miembros_frmConsejoFacultad" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            MIEMBROS DEL CONSEJO DE FACULTAD
        </div>
        
         <asp:Panel ID="PanelConsulta" runat="server">
         
             <table class="style1">
                 <tr>
                     <td colspan="2">
                         Consultar</td>
                 </tr>
                 <tr>
                     <td>
                         Facultad</td>
                     <td>
                         <asp:DropDownList ID="ddlFacultad" runat="server">
                         </asp:DropDownList>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Button ID="Button2" runat="server" Text="Consultar" />
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="Button3" runat="server" Text="Nuevo" />
                     </td>
                 </tr>
             </table>

        </asp:Panel>
        <asp:Panel ID="PanelRegistro" runat="server" Visible="false">
            <table class="style1">
                <tr>
                    <td>
                        Registrar</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        Personal</td>
                    <td>
                        <asp:DropDownList ID="ddlPersonal" runat="server" Height="19px" Width="103px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cargo</td>
                    <td>
                        <asp:DropDownList ID="ddlCargo" runat="server" Height="19px" Width="248px">
                            <asp:ListItem Selected="True" Value="F">Miembro del Consejo de Facultad</asp:ListItem>
                            <asp:ListItem Value="T">Secretario de Facultad</asp:ListItem>
                            <asp:ListItem Enabled="False" Value="G">Consejo de Postgrado</asp:ListItem>
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="Button4" runat="server" Text="Registrar" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button5" runat="server" Text="Cancelar" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <br />
        <asp:Panel ID="PanelLista" runat="server">
            <asp:GridView ID="dgvLista" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="codigo_pcc" HeaderText="codigo_pcc" 
                            Visible="False" />
                    <asp:BoundField DataField="responsable_Cco" HeaderText="Responsable" />
                    <asp:BoundField DataField="Cargo_Cjf" HeaderText="Cargo" />
                    <asp:CommandField DeleteText="Inactivar" ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <br />
    </form>
</body>
</html>
