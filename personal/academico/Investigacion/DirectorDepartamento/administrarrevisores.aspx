<%@ Page Language="VB" AutoEventWireup="false" CodeFile="administrarrevisores.aspx.vb" Inherits="DirectorDepartamento_administrarrevisores" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrar Revisores</title>
      <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
      <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../../private/tooltip.js"></script>
      <STYLE type="text/css">
BODY {
scrollbar-face-color:#AED9F4;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
</head>
<body>
    <form id="form1" runat="server">
    <center>
                                            
    <table>
            <tr>
                <td align="center" colspan="3" 
                    style="height: 28px; font-weight: bold; font-size: 10.5pt; color: midnightblue;">
                    &nbsp;ASIGNACIÓN DE REVISORES E INVESTIGACIONES </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table class="contornotabla" style="width: 100%">
                        <tr>
                            <td style="height: 28px; font-weight: bold; color: black;">
                                &nbsp; &nbsp; Unidad de Investigacion</td>
                            <td style="height: 28px">
                                <asp:DropDownList ID="DDLUnidad" runat="server" AutoPostBack="True" Width="422px">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2"  align="center">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center" colspan="2">
                                            <asp:ListBox ID="LstPersonal" runat="server" Height="116px"
                                                Width="100%" ValidationGroup="agregar"></asp:ListBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="2">
                                            <asp:Button ID="CmdAsignar" runat="server" Text="Asignar como revisor" 
                                                CssClass="attach_prp" 
                                                onclientclick="return confirm ('¿Desea asignar como revisor?')" Width="194px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2"><asp:GridView ID="GridAsignados" runat="server" AutoGenerateColumns="False" 
                                                DataKeyNames="codigo_Rev" DataSourceID="signados" Width="100%" 
                                                GridLines="Horizontal">
                                                <RowStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                                                    Height="20px" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_Rev" HeaderText="codigo_Rev" 
                                                        InsertVisible="False" ReadOnly="True" SortExpression="codigo_Rev" 
                                                        Visible="False" />
                                                    <asp:BoundField DataField="Datos_Per" HeaderText="Apellidos y Nombres" 
                                                        ReadOnly="True" SortExpression="Datos_Per" />
                                                    <asp:CommandField HeaderText=" " SelectText=" " ShowSelectButton="True" />
                                                    <asp:CommandField ButtonType="Image" 
                                                        DeleteImageUrl="../../../../images/menus/Eliminar_s.gif" 
                                                        DeleteText="Quitar Personal como Revisor" ShowDeleteButton="True">
                                                        <ItemStyle HorizontalAlign="Center" Width="35px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="#FFFF80" />
                                                <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Verdana" 
                                                    Font-Size="9pt" ForeColor="White" Height="25px" />
                                            </asp:GridView>
                                            <asp:SqlDataSource ID="signados" runat="server" 
                                                ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                                SelectCommand="INV_ConsultarRevisoresAsignados" 
                                                SelectCommandType="StoredProcedure" DeleteCommand="SELECT 1">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="DDLUnidad" Name="codigo_cco" 
                                                        PropertyName="SelectedValue" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="font-weight: bold; color: maroon">
                                            Investigaciones NO asignadas<asp:RangeValidator ID="RangeValidator1" runat="server"
                                                ControlToValidate="LstFaltantes" ErrorMessage="Debe seleccionar una investigacion de la lista"
                                                MaximumValue="8000" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
                                                ValidationGroup="agregar">*</asp:RangeValidator></td>
                                        <td align="center" style="font-weight: bold; color: maroon">
                                            Investigaciones ASIGNADAS al                                             <br />
                                            personal seleccionado.<asp:RangeValidator ID="RangeValidator2" runat="server"
                                                ControlToValidate="LstAsignadas" ErrorMessage="Debe seleccionar una investigacion de la lista para retirar"
                                                MaximumValue="8000" MinimumValue="1" SetFocusOnError="True" Type="Integer" 
                                                ValidationGroup="retirar">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:ListBox ID="LstFaltantes" runat="server" Height="245px" SelectionMode="Multiple"
                                                Width="290px" ValidationGroup="agregar"></asp:ListBox></td>
                                        <td align="center">
                                            <asp:ListBox ID="LstAsignadas" runat="server" Height="245px" SelectionMode="Multiple"
                                                Width="290px" ValidationGroup="retirar"></asp:ListBox></td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="CmdAgregar" runat="server" Text="    > Agregar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="attach_prp" Height="25px" ValidationGroup="agregar" Width="87px" Enabled="False" />
                                            &nbsp;
                                        </td>
                                        <td align="left">
                                            &nbsp; &nbsp;<asp:Button ID="CmdRetirar" runat="server" Text="    < Retirar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="remove_prp" Height="25px" ValidationGroup="retirar" Width="87px" Enabled="False" /></td>
                                    </tr>
                                </table>
                                <asp:Label ID="Label1" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            </table>
    </center>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="agregar" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="retirar" />
    </form>
</body>
</html>
