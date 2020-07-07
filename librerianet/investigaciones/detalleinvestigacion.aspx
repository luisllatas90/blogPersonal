<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleinvestigacion.aspx.vb" Inherits="investigaciones_detalleinvestigacion" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <STYLE type="text/css">
BODY {
scrollbar-face-color:#FFFFFF;
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
        <asp:DataList ID="DataList1" runat="server" DataSourceID="Ciclos" Width="100%">
            <ItemTemplate>
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="font-weight: normal; font-size: 9pt; text-transform: uppercase; color: white;
                            font-family: Verdana; background-color: firebrick">
                            &nbsp;CICLO ACADEMICO : &nbsp;<asp:Label ID="descripcion_cacLabel" runat="server" Text='<%# Eval("descripcion_cac") %>'></asp:Label></td>
                        <td style="font-weight: normal; font-size: 9pt; text-transform: uppercase; color: white;
                            font-family: Verdana; background-color: firebrick">
                            <asp:Label ID="codigo_cacLabel" runat="server" Text='<%# Eval("codigo_cac") %>' Visible="False"></asp:Label></td>
                        <td align="right" style="font-weight: normal; font-size: 9pt; text-transform: uppercase;
                            color: white; font-family: Verdana; background-color: firebrick">
                            Total :
                            <asp:Label ID="TotalLabel" runat="server" Text='<%# Eval("Total") %>'></asp:Label>&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" rowspan="2" valign="top">
                            <asp:DataList ID="DataList2" runat="server" DataSourceID="Investigacion" Width="100%" CellPadding="0">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td style="font-size: 9pt; width: 10px; color: black; font-family: VERDANA;
                                                background-color: white; border-bottom: firebrick 1px solid;">
                                                <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="../../images/ext/pdf.gif"
                                                    NavigateUrl='<%# "../../filesInvestigacion/" & Eval("Ruta_Inv") %>' Target="_blank" Text='<%# Eval("Ruta_Inv") %>'
                                                    ToolTip="Descargar"></asp:HyperLink></td>
                                            <td align="left" style="font-weight: bold; font-size: 8pt; width: 550px; color: black;
                                                font-family: arial; background-color: white; border-bottom: firebrick 1px solid;">
                                                <asp:Label ID="Titulo_InvLabel" runat="server" Text='<%# Eval("Titulo_Inv") %>'></asp:Label></td>
                                            <td align="right" style="font-weight: normal; font-size: 8pt; color: black; font-family: arial;
                                                background-color: white; border-bottom: firebrick 1px solid;">
                                                Fecha de Registro :
                                                <asp:Label ID="FechaRegistro_InvLabel" runat="server" Text='<%# Eval("FechaRegistro_Inv", "{0:d}") %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-size: 8pt; color: black; font-family: verdana; text-align: justify; background-color: whitesmoke; padding-right: 2px; padding-left: 2px; padding-bottom: 2px; margin: 2px; padding-top: 2px;">
                                                <asp:Label ID="Descripcion_invLabel" runat="server" Text='<%# Eval("Descripcion_inv") %>'></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" style="font-weight: bold; font-size: 7pt; text-transform: capitalize;
                                                color: black; font-family: verdana; border-bottom: black 1px solid; background-color: whitesmoke;">
                                                &nbsp;Profesor Asesor:&nbsp;
                                                <asp:Label ID="ProfesorLabel" runat="server" Text='<%# Eval("Profesor") %>'></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList><asp:SqlDataSource ID="Investigacion" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                SelectCommand="INVALU_ConsultarInvestigacionesCursos" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="codigo_cpf" QueryStringField="cpf" Type="Int32" />
                                    <asp:QueryStringParameter Name="nombre_curso" QueryStringField="INV" Type="String" />
                                    <asp:ControlParameter ControlID="codigo_cacLabel" Name="codigo_cac" PropertyName="Text"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList><asp:SqlDataSource ID="Ciclos" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
            SelectCommand="INVALU_ConsultarCiclosInvestigaciones" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="codigo_cpf" QueryStringField="cpf" Type="Int32" />
                <asp:QueryStringParameter Name="nombre_curso" QueryStringField="Inv" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
