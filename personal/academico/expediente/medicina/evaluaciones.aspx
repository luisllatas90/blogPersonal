<%@ Page Language="VB" AutoEventWireup="false" CodeFile="evaluaciones.aspx.vb" Inherits="medicina_evaluaciones" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <link href="../../../../private/estilo.css" rel="stylesheet" type="text/css" />
     
    <script src="../../../../private/funciones.js" type="text/javascript"></script>
</head>
<body style="margin:0,0,0,0">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td colspan="2" style="border-top: black 1px solid; font-weight: bold; font-size: 11pt;
                    color: white; border-bottom: black 1px solid; font-family: verdana; height: 21px;
                    background-color: firebrick; text-align: center">
                    &nbsp;Registro de Evaluaciones de Profesor</td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;
                    <asp:HyperLink ID="LinkRegresar" runat="server" NavigateUrl="../../notas/profesor/miscursos.asp"
                        Style="font-size: 8pt; color: saddlebrown; font-family: verdana">«« Regresar</asp:HyperLink></td>
                <td align="right">
                    &nbsp;<asp:Button ID="CmdNuevo" runat="server" CssClass="usatnuevo" Enabled="False"
                        Text="Nuevo" Width="68px" />
                    <asp:Button ID="CmdModificar" runat="server" CssClass="modificar" Enabled="False"
                        Text="     Modificar" Width="81px" />
                    <asp:Button ID="CmdElminar" runat="server" CssClass="eliminar" Enabled="False" Text="     Elminar"
                        Width="71px" /></td>
            </tr>
            <tr>
                <td colspan="2" style="border-top: #660000 1px solid; height: 16px">
                    &nbsp;&nbsp; Profesor :
                    <asp:Label ID="LblProfesor" runat="server" Style="font-size: 7pt; text-transform: uppercase;
                        color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="border-bottom: #660000 1px solid; height: 16px">
                    &nbsp;&nbsp; Asignatura :
                    <asp:Label ID="LblAsignatura" runat="server" Style="font-size: 7pt; text-transform: uppercase;
                        color: dimgray; font-family: verdana"></asp:Label></td>
            </tr>
            <tr>
                <td rowspan="2" style="width: 50%" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="500px" ScrollBars="Auto" Width="100%">
                        <asp:TreeView ID="TreeActividad" runat="server" ImageSet="Arrows" ShowLines="True">
                            <ParentNodeStyle Font-Bold="False" />
                            <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                            <SelectedNodeStyle BackColor="#FFE0C0" Font-Underline="True" ForeColor="Maroon" HorizontalPadding="0px"
                                VerticalPadding="0px" />
                            <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px"
                                NodeSpacing="0px" VerticalPadding="0px" />
                            <Nodes>
                                <asp:TreeNode SelectAction="SelectExpand" Text="Actividades" Value="0"></asp:TreeNode>
                            </Nodes>
                        </asp:TreeView>
                    </asp:Panel>
                    &nbsp;&nbsp;</td>
                <td rowspan="2" valign="top">

                    <script src="../../../../private/calendario.js" type="text/javascript"></script>

                    <table border="0" cellpadding="0" cellspacing="0" style="border-right: black 2px solid;
                        padding-right: 5px; border-top: black 2px solid; padding-left: 5px; padding-bottom: 5px;
                        margin: 3px; border-left: black 2px solid; padding-top: 5px; border-bottom: black 2px solid;
                        background-color: whitesmoke" width="95%">
                        <tr>
                            <td colspan="3" rowspan="3">
                                <asp:DataList ID="DataList1" runat="server" DataKeyField="codigo_Act" DataSourceID="Actividades"
                                    Width="100%">
                                    <ItemTemplate>
                                        <table cellspacing="6" width="100%">
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Nombre</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="nombreLabel" runat="server" Text='<%# Eval("nombre") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Semana</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="semana_actLabel" runat="server" Text='<%# Eval("semana_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Fecha Inicio</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="fechaini_actLabel" runat="server" Text='<%# Eval("fechaini_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold; width: 80px">
                                                    Fecha Fin</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="fechafin_actLabel" runat="server" Text='<%# Eval("fechafin_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Estado</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="estado_actLabel" runat="server" Text='<%# Eval("estado_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Tipo Actividad</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="descripcion_actLabel" runat="server" Text='<%# Eval("descripcion_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Modalidad Evaluación&nbsp;</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("descripcion_mta") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Ponderación</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("peso_act") %>'></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: bold">
                                                    Estado Actividad</td>
                                                <td style="font-size: 10pt">
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("realizado") %>'></asp:Label></td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table style="font-weight: bold; font-size: 11pt; text-transform: uppercase; color: black;
                                            font-family: arial; text-decoration: underline" width="100%">
                                            <tr>
                                                <td align="center" colspan="3" rowspan="3">
                                                    Detalle de EVALUACIÓN Seleccionada</td>
                                            </tr>
                                            <tr>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                </asp:DataList><asp:SqlDataSource ID="Actividades" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                    SelectCommand="MED_ConsultarEvaluacionDescripcion" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="TreeActividad" Name="codigo_act" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
        <asp:HiddenField ID="HidenTree" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
