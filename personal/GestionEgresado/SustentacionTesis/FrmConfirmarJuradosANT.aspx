<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfirmarJuradosANT.aspx.vb"
    Inherits="FrmConfirmarJurados" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Confirmación de Jurados de Tesis </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            fnLoading(false);

            $("#ddlEstado").change(function() {
                fnLoading(true);
            })

        });
        /*
        function initCombo(id) {
        var options = {
        noneSelectedText: '-- Seleccione --',
        size: 6,
        dropdownAlignRight: true
        };
        $('#' + id).selectpicker(options);
        }*/
        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
        }

        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 5000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }

        function ValidarActualizar() {
            if (confirm('¿Está seguro que desea actualizar los jurados?')) {
                fnLoading(true);
                return true;
            } else {
                return false;
            }
        }
        function ValidarConformidad() {
            if (confirm('¿Está seguro que desea dar conformidad a jurados de tesis?')) {
                fnLoading(true);
                return true;
            } else {
                return false;
            }
        }
    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .modal-open
        {
            overflow: inherit;
        }
        .form table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
        }
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            max-height: 125px;
            max-width: 350px;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
        .panel-default
        {
            padding-bottom: 100px;
        }
    </style>
</head>
<body class="">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="piluku-preloader text-center">
                <div class="loader">
                    Loading...</div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Confirmar jurados para sustentación de Tesis</b>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                        <div class="col-sm-3 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                <asp:ListItem Value="A">APROBADOS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%--      <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>--%>
                        <div class="col-sm-6 col-md-6">
                            <table>
                                <tr>
                                    <td style="width: 10%; padding: 4px;">
                                        <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                            CssClass="btn btn-info btn-sm btn-radius" Font-Size="11px" ToolTip="Actualizar"
                                            OnClientClick="return false;">
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="Label9" runat="server" CssClass="control-label">Actualizar Jurados</asp:Label>
                                    </td>
                                    <td style="width: 10%; padding: 4px;">
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text='<span class="fa fa-check"></span>'
                                            CssClass="btn btn-success btn-sm btn-radius" Font-Size="11px" ToolTip="Conformidad"
                                            OnClientClick="return false;"> 
                                        </asp:LinkButton>
                                    </td>
                                    <td style="width: 40%;">
                                        <asp:Label ID="Label14" runat="server" CssClass="control-label">Dar conformidad a Jurados de tesis</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <br />
                <div class="form-group">
                    <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div runat="server" id="lblmensaje">
                            </div>
                            <asp:GridView runat="server" ID="gvTesis" CssClass="table table-condensed" DataKeyNames="codigo_Tes,codigo_dta,codigo_trl,jpresidente,jsecretario,jvocal,njpresidente,njsecretario,njvocal,codigo_pst"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="titulo_Tes" HeaderText="TÍTULO" HeaderStyle-Width="35%" />
                                    <asp:BoundField DataField="alumno" HeaderText="BACHILLER(ES)" HeaderStyle-Width="15%" />
                                    <asp:TemplateField HeaderText="JURADOS" HeaderStyle-Width="35%" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <div class="row">
                                                <label class="col-xs-3 col-sm-3 col-md-3 control-label">
                                                    Presidente:
                                                </label>
                                                <div class="col-xs-9 col-sm-9 col-md-9">
                                                    <asp:DropDownList runat="server" ID="ddlPresidente" CssClass="form-control">
                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-xs-3 col-sm-3 col-md-3 control-label">
                                                    Secretario:
                                                </label>
                                                <div class="col-xs-9 col-sm-9 col-md-9">
                                                    <asp:DropDownList runat="server" ID="ddlSecretario" CssClass="form-control ">
                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-xs-3 col-sm-3 col-md-3 control-label">
                                                    Vocal:
                                                </label>
                                                <div class="col-xs-9 col-sm-9 col-md-9">
                                                    <asp:DropDownList runat="server" ID="ddlVocal" CssClass="form-control ">
                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle Width="35%" />
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnActualizar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                CssClass="btn btn-info btn-sm btn-radius" ToolTip="Actualizar" CommandName="Actualizar"
                                                OnClientClick="return ValidarActualizar();" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnConforme" runat="server" Text='<span class="fa fa-check"></span>'
                                                CssClass="btn btn-success btn-sm btn-radius" ToolTip="Conformidad" CommandName="Conformidad"
                                                OnClientClick="return ValidarConformidad();" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataTemplate>
                                    <b>No se encontraron alumnos</b>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                            <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                <%--            </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" EventName="click" />
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </form>
</body>
</html>
