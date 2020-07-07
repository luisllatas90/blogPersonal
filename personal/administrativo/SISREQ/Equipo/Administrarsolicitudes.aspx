<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Administrarsolicitudes.aspx.vb" Inherits="Equipo_Administrarsolicitudes" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administrar Solicitud</title>
    <link href ="../private/estilo.css" rel ="Stylesheet" type="text/css" />
    <link href ="../private/estiloweb.css" rel ="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" src="../private/funcion.js"></script>
    <script language ="javascript" type="text/javascript" >
    function validadatos(source, arguments)
    {
        if (frmadmsolicitud.CboTabla.value > 0)
           if (frmadmsolicitud.CboCampo.value > -1)
                arguments.IsValid=true
            else
                arguments.IsValid=false
        
    }
    </script>

    <style type="text/css">
        .style1
        {
            width: 871px;
            height: 41px;
        }
        .style2
        {
            width: 93px;
            height: 41px;
        }
    </style>

</head>
<body>
    <form id="frmadmsolicitud" runat="server">
    <div>
        <table 0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td align="center">
                    <table width="100%">
                        <tr>
                            <td>
                                <strong>Buscar por: </strong>
                                <asp:DropDownList ID="CboTabla" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Estado</asp:ListItem>
                                    <asp:ListItem Value="2">Area</asp:ListItem>
                                </asp:DropDownList>&nbsp;<asp:DropDownList ID="CboCampo" runat="server" Visible="False">
                                    <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validadatos"
                                    ErrorMessage="Seleccione un campo de busqueda" ValidationGroup="Buscar">*</asp:CustomValidator></td>
                            <td align="right">
                                <asp:Button ID="CmdBuscar" runat="server" CssClass="Buscar" Height="30px" Text="Buscar"
                                    ValidationGroup="Buscar" Width="90px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2" style="height: 1px; background-color: #004182;">
                            </td>
                        </tr>
                        <tr>
                            <td align="justify">
                                &nbsp;&nbsp;
                                <img alt="" src="../images/CUADRO_VERDE.jpg" /> : requerimientos en grupo
                                <img alt="" src="../images/cuadro_azul.jpg" /> : solicitudes individuales</td>
                            <td align="right">
                                <asp:Label ID="LblRegistros" runat="server" ForeColor="#000099"></asp:Label></td>
                        </tr>
                        </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table border="0" cellpadding="0" cellspacing="0" style="height: 462px; background-color: #ffffff"
                        width="98%">
                        <tr>
                            <td colspan="4" align="center" height="350" valign="top">
                                <asp:GridView ID="gvSolicitud" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" DataKeyNames="id_sol" DataSourceID="ObjGrid"
                                    Width="98%" PageSize="8" GridLines="Horizontal">
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="N&#186;" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha" SortExpression="fecha_sol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est"
                                            Visible="False" />
                                        <asp:BoundField DataField="id_est" HeaderText="Estado" SortExpression="id_est">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="tipo" HeaderText=" " SortExpression="tipo" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco" />
                                        <asp:BoundField DataField="id_act" HeaderText=" " SortExpression="id_act" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="observacion_per" HeaderText="Observaci&#243;n" SortExpression="observacion_per">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="0px" Wrap="False" />
                                            <HeaderStyle Width="0px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle ForeColor="#666666" />
                                    <RowStyle Height="35px" />
                                    <PagerStyle Height="15px" />
                                    <HeaderStyle Height="20px" ForeColor="White" CssClass="TituloReq" />
                                </asp:GridView>
                                </td>
                        </tr>
                        <tr>
                            <td colspan="4" height="3" align="left">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="4" align="right" height="15">
                                <asp:Button ID="CmdGuardar" runat="server" CssClass="Guardar" Text="Guardar" />&nbsp;
                                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="CelDatos" style="visibility:visible">
                <td style="height: 8px; background-color: #004182;">
                    &nbsp;</td>
            </tr>
            <tr id="CelDatos" style="visibility:visible">
                <td>
                    <iframe id="ifDatos" class="fondoblanco" frameborder="0" name="ifDatos" scrolling="no"
                        src="DatosSolicitud.aspx" style="width: 100%; height: 243px"></iframe>
                </td>
            </tr>
            <tr>
                <td style="height: 1px; background-color: #004182;">
                </td>
            </tr>
        </table>
    
    </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="Buscar" />
                                <asp:ObjectDataSource ID="ObjGrid" runat="server" SelectMethod="obtieneSolicitudesPorResponsableDeCronograma"
                                    TypeName="clsRequerimientos">
                                    <SelectParameters>
                                        <asp:QueryStringParameter Name="cod_per" QueryStringField="id" Type="Int32" />
                                        <asp:ControlParameter ControlID="CboCampo" Name="campo" PropertyName="SelectedValue"
                                            Type="Int16" DefaultValue="0" />
                                        <asp:ControlParameter ControlID="CboTabla" DefaultValue="0" Name="valor" PropertyName="SelectedValue"
                                            Type="Int16" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
    </form>
</body>
</html>
