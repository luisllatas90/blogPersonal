<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Responsablecronograma.aspx.vb" Inherits="Equipo_AgregarResponsableASolicitud" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel ="stylesheet" type ="text/css" />
    <link href="../private/estiloweb.css" rel ="stylesheet" type ="text/css" />
    <script language="javascript" type="text/javascript" src="../../../../private/tooltip.js"></script>
    <script language="javascript" type="text/javascript" src="../private/funcion.js"></script>
    <script language ="javascript" type="text/javascript" src ="../private/funciones.js"></script>
</head>
<body style="text-align: center; margin-top: 0px; margin-left: 0px; clip: rect(auto auto auto auto); margin-right: 0px;">
    <form id="frmResponsableCronograma" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td class="titulocel" colspan="3" rowspan="1" style="height: 20px; text-align: center">
                    Asignar Responsable</td>
            </tr>
            <tr>
                <td class="titulocel" colspan="3" rowspan="1" style="height: 20px; text-align: center">
                    <table width="95%">
                        <tr>
                            <td>
                                <strong>Consultar</strong></td>
                            <td colspan="2">
                                <asp:RadioButtonList ID="RblConsultar" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                    <asp:ListItem Value="1">Nuevos M&#243;dulos</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">M&#243;dulos Existentes</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 9px">
                                <hr style="width: 100%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="height: 13px">
                                <table width="100%" id="TblBuscar">
                                    <tr>
                                        <td align="left" colspan="1" style="width: 78px; border-top-style: none; border-right-style: none;
                                            border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                            &nbsp;Ver</td>
                                        <td align="left" colspan="3" style="border-top-style: none; border-right-style: none;
                                            border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                            :
                                            <asp:DropDownList ID="CboSolicitudPor" runat="server" AutoPostBack="True">
                                                <asp:ListItem Value="0">Solicitudes Sin Asignar</asp:ListItem>
                                                <asp:ListItem Value="1">Solicitudes Asignadas</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                            border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                        </td>
                                    </tr>
                                    <tr>
                            <td align="left" colspan="1" style="width: 78px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                &nbsp;Buscar por</td>
                            <td align="left" colspan="3" style="border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                :
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Width="231px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">M&#243;dulo</asp:ListItem>
                                    <asp:ListItem Value="2">Prioridad</asp:ListItem>
                                    <asp:ListItem Value="3">&#193;rea</asp:ListItem>
                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboCampo"
                                    ErrorMessage="Seleccione campo de busqueda" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator>&nbsp;
                            </td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                <asp:Button ID="Button1" runat="server" CssClass="buscar" Height="26px" Text="Buscar" /></td>
                                    </tr>
                                    <tr>
                            <td align="left" colspan="1" style="width: 78px; border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none">
                            </td>
                            <td align="left" colspan="3" style="border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none">
                                &nbsp;&nbsp;<asp:DropDownList ID="CboValor" runat="server" Visible="False" Width="431px">
          </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CboValor"
              ErrorMessage="Seleccione Valor de Busqueda" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator></td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none">
                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="3" style="text-align: center">
                    <table width="95%">
                        <tr>
                            <td align="left" colspan="5" style="border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 15px; text-align: left; border-bottom-style: none">
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="5" style="text-align: left; height: 30px;">
                                <strong>
                                Solicitudes sin asignación de responsable para cronograma</strong></td>
                        </tr>
                        <tr>
                            <td colspan="5" style="text-align: center; height: 241px;" valign="top">
                                <asp:GridView ID="GvSinAsignar" runat="server" Width="100%" DataSourceID="ObjObtieneResponsables" AllowPaging="True" AllowSorting="True" PageSize="8" AutoGenerateColumns="False">
                                    <RowStyle Height="40px" />
                                    <HeaderStyle CssClass="celdaencabezado" Height="25px" />
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="id_sol" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol" />
                                        <asp:BoundField DataField="id_Est" HeaderText="id_Est" SortExpression="id_Est" Visible="False" />
                                        <asp:BoundField DataField="vigente_solest" HeaderText="vigente_solest" SortExpression="vigente_solest"
                                            Visible="False" />
                                        <asp:BoundField DataField="cant" HeaderText="cant" SortExpression="cant" Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea que solicit&#243;" SortExpression="descripcion_cco" >
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad" >
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="responsable" HeaderText="responsable" SortExpression="responsable"
                                            Visible="False" />
                                        <asp:BoundField DataField="activa_Sol" HeaderText="activa_Sol" SortExpression="activa_Sol"
                                            Visible="False" />
                                        <asp:BoundField DataField="prioridad_sol" HeaderText="prioridad_sol" SortExpression="prioridad_sol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est" Visible="False" />
                                        <asp:BoundField DataField="id_Est1" HeaderText="id_Est1" SortExpression="id_Est1"
                                            Visible="False" />
                                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha Solicitud" SortExpression="fecha_sol" DataFormatString="{0:dd-mm-yyyy}" HtmlEncode="False" >
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl" Visible="False" />
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/agregar.gif" SelectText=""
                                            ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No se encontraron registros</span>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjObtieneResponsables" runat="server" SelectMethod="ObtieneConsultaSolicitudes"
                                    TypeName="clsRequerimientos">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="RblConsultar" DefaultValue="" Name="Existencia"
                                            PropertyName="SelectedValue" Type="Int16" />
                                        <asp:ControlParameter ControlID="CboCampo" DefaultValue="0" Name="Campo" PropertyName="SelectedValue"
                                            Type="Int16" />
                                        <asp:ControlParameter ControlID="CboValor" DefaultValue="" Name="valor" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="CboSolicitudPor" DefaultValue="0" Name="asignado"
                                            PropertyName="SelectedValue" Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 15px">
                                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical" Width="100%">
                                </asp:Panel>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 15px">
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
