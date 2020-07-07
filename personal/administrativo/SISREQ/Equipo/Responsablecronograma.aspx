<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Responsablecronograma.aspx.vb" Inherits="Equipo_AgregarResponsableASolicitud" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Asignar Responsable</title>
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
                <td rowspan="1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td rowspan="1">
                    <table width="100%" align="center">
                        <tr>
                            <td width="70">
                                <strong>&nbsp; Consultar</strong></td>
                            <td style="width: 706px; height: 7px">
                                <asp:RadioButtonList ID="RblConsultar" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                    <asp:ListItem Value="1">Nuevos M&#243;dulos</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="2">M&#243;dulos Existentes</asp:ListItem>
                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 87px">
                                <table width="100%" id="TblBuscar">
                                    <tr>
                                        <td align="left" colspan="1">
                                            &nbsp;Ver</td>
                                        <td align="left" colspan="3">
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
                            <td align="left" colspan="1">
                                &nbsp;Buscar por</td>
                            <td align="left" colspan="3">
                                :
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Width="87px">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">M&#243;dulo</asp:ListItem>
                                    <asp:ListItem Value="2">Prioridad</asp:ListItem>
                                    <asp:ListItem Value="3">&#193;rea</asp:ListItem>
                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboCampo"
                                    ErrorMessage="Seleccione campo de busqueda" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator>&nbsp;
                            </td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; height: 16px; text-align: left; border-bottom-style: none">
                                &nbsp;</td>
                                    </tr>
                                    <tr>
                            <td align="left" colspan="1">
                            &nbsp;Valor</td>
                            <td align="left" colspan="3">
                                :&nbsp;<asp:DropDownList ID="CboValor" runat="server" Width="431px" 
                                    Enabled="False">
          </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CboValor"
              ErrorMessage="Seleccione Valor de Busqueda" Operator="GreaterThanEqual" ValueToCompare="0">*</asp:CompareValidator></td>
                            <td align="left" colspan="1" style="width: 6px; border-top-style: none; border-right-style: none;
                                border-left-style: none; text-align: left; border-bottom-style: none">
                                <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar" Height="26px" Text="Buscar" />
                            </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height:1px; background-color:#003366">
                    </td>
            </tr>
            <tr>
                <td style="text-align: center;" valign="top">
                    <table style="width: 98%;" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" style="height: 27px; text-align: right" valign="top">
                                <asp:Label ID="lblTotal" runat="server" ForeColor="#0000CC"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" bgcolor="#FFFFCC" height="15">
                                Para asignar el responsable dar clic en la imagen
                                <img alt="" src="../images/agregar.gif" /> </td>
                        </tr>
                        <tr>
                            <td style="height: 11px" valign="top">
                                <asp:GridView ID="GvSinAsignar" runat="server" Width="100%" 
                                    DataSourceID="ObjObtieneResponsables" AllowPaging="True" AllowSorting="True" 
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" PageSize="15">
                                    <FooterStyle Font-Bold="True" ForeColor="White" />
                                    <RowStyle Height="40px" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle CssClass="TituloReq" Height="20px" 
                                        Font-Bold="True" ForeColor="White" />
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
                                            <ItemStyle Width="250px" />
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
                                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha" 
                                            SortExpression="fecha_sol" DataFormatString="{0:dd/MM/yyyy}" 
                                            HtmlEncode="False" >
                                            <ItemStyle HorizontalAlign="Center" Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl" Visible="False" />
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/agregar.gif" SelectText=""
                                            ShowSelectButton="True">
                                            <ItemStyle Width="20px" />
                                        </asp:CommandField>
                                    </Columns>
                                    <PagerStyle BackColor="#0055AA" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <span style="color: #ff0000">No se encontraron registros</span>
                                    </EmptyDataTemplate>
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjObtieneResponsables" runat="server" SelectMethod="ObtieneConsultaSolicitudes"
                                    TypeName="clsRequerimientos">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="RblConsultar" DefaultValue="" Name="Existencia"
                                            PropertyName="SelectedValue" Type="Int16" />
                                        <asp:ControlParameter ControlID="CboCampo" DefaultValue="-1" Name="Campo" PropertyName="SelectedValue"
                                            Type="Int16" />
                                        <asp:ControlParameter ControlID="CboValor" DefaultValue="-1" Name="valor" PropertyName="SelectedValue"
                                            Type="Int32" />
                                        <asp:ControlParameter ControlID="CboSolicitudPor" DefaultValue="0" Name="asignado"
                                            PropertyName="SelectedValue" Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" />
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
