<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Administrarsolicitud.aspx.vb" Inherits="Equipo_Administrarsolicitud" EnableViewStateMac="false" %>

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
</head>
<body>
    <form id="frmadmsolicitud" runat="server">
   <table id="Tabla" border="0" cellpadding="0" cellspacing="0" style="height: 827px; width: 100%;">
            <tr>
                <td class="titulocel" colspan="3" rowspan="1" style="text-align: center; width: 1059px;">
                                Lista de solicitudes
                </td>
            </tr>
            <tr>
                <td class="titulocel" colspan="3" rowspan="1" style="text-align: center; width: 1059px;">
                    <table class="fondoblanco" width="95%">
                        <tr>
                            <td style="width: 871px; height: 15px;">
                                <strong>
                                Buscar por:</strong></td>
                            <td style="height: 15px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 871px; height: 32px">
                                <asp:DropDownList ID="CboTabla" runat="server" AutoPostBack="True">
                                    <asp:ListItem Value="0">Todos</asp:ListItem>
                                    <asp:ListItem Value="1">Estado</asp:ListItem>
                                    <asp:ListItem Value="2">Area</asp:ListItem>
                                </asp:DropDownList>&nbsp; 
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Visible="False">
                                    <asp:ListItem Value="-1">-- Seleccione --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Seleccione un campo de busqueda" ClientValidationFunction="validadatos" ValidationGroup="Buscar">*</asp:CustomValidator></td>
                            <td style="height: 32px; text-align: center">
                                <asp:Button ID="CmdBuscar" runat="server" Text="Buscar" ValidationGroup="Buscar" CssClass="Buscar" Height="30px" Width="90px" /></td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3" rowspan="3" style="text-align: center; width: 1059px; height: 443px;" valign="top" class="titulocel">
                    <table width="98%" border="0" cellpadding="0" cellspacing="0" style="height: 462px; background-color: #ffffff">
                        <tr>
                            <td colspan="4" style="height: 20px; text-align: right; width: 993px;">
                                <asp:Label ID="LblRegistros" runat="server" ForeColor="Red"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="4" rowspan="2" style="height: 344px; text-align: center;" valign="top">
                                <asp:GridView ID="gvSolicitud" runat="server" AutoGenerateColumns="False" DataKeyNames="id_sol" GridLines="Horizontal" Width="98%" AllowPaging="True" AllowSorting="True">
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="N&#186;" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol" >
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" ReadOnly="True" SortExpression="prioridad" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" HeaderText="Fecha" SortExpression="fecha_sol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_est" HeaderText="Estado" SortExpression="descripcion_est"
                                            Visible="False" />
                                        <asp:BoundField DataField="id_est" HeaderText="Estado" SortExpression="id_est" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco" />
                                        <asp:BoundField DataField="codigo_per" HeaderText="codigo_per" SortExpression="codigo_per"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="observacion_per" HeaderText="Observaci&#243;n" SortExpression="observacion_per" >
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemStyle Width="0px" Wrap="False" />
                                            <HeaderStyle Width="0px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle Height="30px" />
                                    <HeaderStyle Height="25px" CssClass="celdaencabezado" />
                                </asp:GridView>
                                </td>
                        </tr>
                        <tr>
                        </tr>
                        <tr>
                            <td colspan="4" rowspan="1" style="text-align: right; width: 993px; height: 35px;">
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" CssClass="Guardar" Width="82px" />&nbsp;
                </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
            </tr>
            <tr>
                <td class="titulocel" colspan="3" rowspan="1" style="width: 1059px; height: 26px;
                    text-align: center">
                    &nbsp;<iframe src="DatosSolicitud.aspx" id="ifDatos" name="ifDatos" frameborder="0" scrolling ="no" style="width: 95%; height: 243px;" class="fondoblanco" />
                    <br />
                </td>
            </tr>
        </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="Buscar" />
    </form>
</body>
</html>
