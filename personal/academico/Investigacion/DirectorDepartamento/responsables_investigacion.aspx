<%@ Page Language="VB" AutoEventWireup="false" CodeFile="responsables_investigacion.aspx.vb" Inherits="DirectorDepartamento_responsables_investigacion" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">

    <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">
    <script language="JavaScript" src="../../../../private/funciones.js"></script>
    <title>Página sin título</title>
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
    <div>
        <table cellpadding="0" cellspacing="1" style="width: 100%">
            <tr>
                <td align="right" colspan="3" valign="top">
                    </td>
            </tr>
            <tr>
                <td align="right" colspan="3" style="font-size: 4pt">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3" rowspan="2">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_Res"
            DataSourceID="Objresponsables" BorderColor="Transparent" GridLines="None" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="N&#176;">
                    <ItemStyle HorizontalAlign="Center" />
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_Res" HeaderText="codigo_Res" InsertVisible="False"
                    ReadOnly="True" SortExpression="codigo_Res" Visible="False" />
                <asp:BoundField DataField="descripcion_Tpi" HeaderText="Responsabilidad" SortExpression="descripcion_Tpi" >
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Situacion">
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
                <asp:BoundField DataField="Datos_Per" HeaderText="Datos_Per" ReadOnly="True" SortExpression="Datos_Per"
                    Visible="False" >
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </asp:BoundField>
                <asp:BoundField DataField="Datos2_Per" HeaderText="Responsable" ReadOnly="True" SortExpression="Datos2_Per" >
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                </asp:BoundField>
                <asp:BoundField DataField="Datos_Alu" HeaderText="Datos_Alu" ReadOnly="True" SortExpression="Datos_Alu"
                    Visible="False" />
                <asp:BoundField DataField="Datos_Ext" HeaderText="Datos_Ext" ReadOnly="True" SortExpression="Datos_Ext"
                    Visible="False" />
                <asp:BoundField DataField="nombre_Lin" HeaderText="nombre_Lin" SortExpression="nombre_Lin" Visible="False" />
                <asp:CommandField ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="../../../../images/menus/noconforme_small.gif" HeaderText="Eliminar" Visible="False" >
                    <HeaderStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:CommandField>
            </Columns>
            <RowStyle Font-Names="Verdana" Font-Size="8pt" Height="20px" />
            <HeaderStyle BackColor="#E1F1FB" Font-Names="Arial" Font-Size="X-Small" ForeColor="Navy"
                HorizontalAlign="Center" />
        </asp:GridView>
                    &nbsp;
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:ObjectDataSource ID="Objresponsables" runat="server" DeleteMethod="EliminarResponsableInv"
            SelectMethod="ConsultarInvestigaciones" TypeName="Investigacion">
            <DeleteParameters>
                <asp:Parameter Name="codigo_res" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="8" Name="tipo" Type="String" />
                <asp:QueryStringParameter DefaultValue="" Name="param1" QueryStringField="codigo_inv"
                    Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
                    <asp:Button ID="CmdAgregar" runat="server" Text="     Agregar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="attach_prp" Height="24px" Width="79px" ToolTip="Agregue un responsable a esta investigación" Visible="False" />
    </form>
</body>
</html>
