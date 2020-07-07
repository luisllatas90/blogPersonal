<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAdscripcionPersonal.aspx.vb"
    Inherits="FrmAdscripcionPersonal" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <!-- Cargamos css -->
    <link rel='stylesheet' href='assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='assets/css/material.css' />
    <link rel='stylesheet' href='assets/css/style.css?x=1' />
    <!-- Cargamos JS -->

    <script type="text/javascript" src='assets/js/jquery.js'></script>

    <script src="assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='assets/js/wow.min.js'></script>

    <script type="text/javascript" src="assets/js/jquery.nicescroll.min.js"></script>

    <script type="text/javascript" src='assets/js/jquery.loadmask.min.js'></script>

    <%--<script type="text/javascript" src='assets/js/jquery.accordion.js'></script>--%>

    <script type="text/javascript" src='assets/js/materialize.js'></script>

    <%--<script type="text/javascript" src='assets/js/form-elements.js'></script>--%>
    <%--<script type="text/javascript" src='assets/js/select2.js'></script>--%>

    <script src='assets/js/bootstrap-datepicker.js' type="text/javascript"></script>

    <script type="text/javascript" src='assets/js/jquery.multi-select.js'></script>

    <script type="text/javascript" src='assets/js/bootstrap-colorpicker.js'></script>

    <script type="text/javascript" src='assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='assets/css/jquery.dataTables.min.css' />
    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Fin Notificaciones =============================================--%>
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        .content .main-content
        {
            padding-right: 18px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
        }
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
            text-align: left;
        }
        .modal
        {
            overflow: auto !important;
        }
        .input-group .form-control
        {
            z-index: 0;
        }
        #tbFiltros, #gvAdscriptos, #tbNuevo
        {
            width: 100%;
            padding: 4px;
        }
        #tbFiltros tbody, #gvAdscriptos tbody, #tbNuevo tbody
        {
            padding: 4px;
        }
        #tbFiltros tbody tr th, #gvAdscriptos tbody tr th
        {
            text-align: center;
            height: 30px;
        }
        #tbFiltros tr td, #tbNuevo tr td
        {
            padding: 4px;
        }
        #tbFiltros tr td
        {
            padding: 4px;
            text-align: center;
        }
        #gvAdscriptos tbody tr td
        {
            padding: 4px;
        }
    </style>
    <title>Lista de Adscripcion Docente</title>
</head>
<body class="">
    <div class="row card">
        <div class="row">
            <div class="col-md-12">
                <div class="page-header">
                    <h3>
                        Adscripción Docente</h3>
                </div>
            </div>
        </div>
        <form id="form1" runat="server">
        <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
            Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
        <table id="tbFiltros" runat="server">
            <tr>
                <td style="width: 10%">
                    <asp:Label runat="server" CssClass="control-label">Periodo Laborable</asp:Label>
                </td>
                <td style="width: 15%">
                    <asp:DropDownList runat="server" ID="cboPeriodo" CssClass="form-control">
                    </asp:DropDownList>
                </td>
                <td style="width: 15%">
                    <asp:Label ID="Label2" runat="server" CssClass="control-label"> Departamento Acádemico
                    </asp:Label>
                </td>
                <td style="width: 40%">
                    <asp:DropDownList runat="server" ID="cboDepartamento" CssClass="form-control">
                        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="width: 10%">
                    <asp:Button runat="server" ID="btnConsultar" Text="Consultar" CssClass="btn btn-primary" />
                </td>
                <td style="width: 10%">
                    <asp:Button runat="server" ID="btnNuevo" Text="Nuevo" CssClass="btn btn-success" />
                </td>
            </tr>
        </table>
        <div class="row card" id="divGrilla" runat="server">
            <asp:GridView ID="gvAdscriptos" runat="server" AutoGenerateColumns="False" CaptionAlign="Top"
                BorderStyle="None" CellPadding="4" AlternatingRowStyle-BackColor="#F7F6F4" DataKeyNames="codigo_aper,codigo_apl,codigo_pel,codigo_Dac,codigo_per">
                <RowStyle BorderColor="#C2CFF1" />
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="departamento" HeaderText="DEPARTAMENTO ACÁDEMICO" HeaderStyle-Width="38%" />
                    <asp:BoundField DataField="Personal" HeaderText="PERSONAL" HeaderStyle-Width="40%" />
                    <%--<asp:BoundField DataField="tienecontrato" HeaderText="CONTRATO VIGENTE" HeaderStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>--%>
                    <asp:BoundField DataField="tienecarga" HeaderText="CARGA ACADÉMICA" HeaderStyle-Width="5%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="estado" HeaderText="ESTADO" HeaderStyle-Width="7%">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-Width="5%">
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            Quitar
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Button runat="server" Text="Quitar" CommandName="Quitar" CommandArgument="<%#Container.DataItemIndex%>"
                                CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('¿Desea Eliminar Registro?.')" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <div style="color: #3266DB; background-color: #E8EEF7; padding: 5px; font-style: italic;">
                        No se encontraron registros.
                    </div>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="#3366CC" HorizontalAlign="Justify" />
                <AlternatingRowStyle BackColor="#F7F6F4" />
            </asp:GridView>
        </div>
        <div class="row card" runat="server" id="DivNuevo" style="width: 80%;">
            <table id="tbNuevo">
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label4" runat="server" CssClass="control-label"> Periodo Laborable
                        </asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList runat="server" ID="ddlPeriodo" CssClass="form-control">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label1" runat="server" CssClass="control-label"> Departamento Acádemico
                        </asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList runat="server" ID="ddlDepartamento" CssClass="form-control">
                            <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label3" runat="server" CssClass="control-label"> Docente
                        </asp:Label>
                    </td>
                    <td style="width: 50%">
                        <asp:DropDownList runat="server" ID="ddlPersonal" CssClass="form-control">
                            <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%">
                    </td>
                    <td style="width: 50%">
                        <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-success" Text="Guardar" />
                        <asp:Button runat="server" ID="btnCancelar" CssClass="btn btn-danger" Text="Cancelar" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </div>
</body>
</html>
