<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistrarIncidente.aspx.vb"
    Inherits="FrmRegistrarIncidente" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programar sustentación de Tesis</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <%-- ======================================================================================--%>

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

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

    <%-- ======================= Fecha y Hora =============================================--%>

    <script src="../../assets/js/moment-with-locales.js?x=1" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datetimepicker.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#ddlTipoAmbiente").change(function() {
                $("#DivFisico").hide();
                $("#DivVirtual").hide();
                if ($("#ddlTipoAmbiente").val() == "F") {
                    $("#DivFisico").show();
                } else if ($("#ddlTipoAmbiente").val() == "V") {
                    $("#DivVirtual").show();
                }
            })
            LoadingEstado();
            fnLoading(false);

        });
        function LoadingEstado() {
            $("#ddlEstado").change(function() {
                fnLoading(true);
            });
        }


        function fnDescargar(url) {
            var d = new Date();
            window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }

        function Calendario() {
            $('#datetimepicker2').datetimepicker({
                locale: 'es',
                format: 'L'
            });
        }

        function fnLoading(sw) {
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
        }
        function ValidarGuardar() {
            if ($("#ddlAsistente").val() == "") {
                fnMensaje('error', 'Seleccione asistente faltante')
                return false;
            }
            if ($("#txtdetalle").val() == "") {
                fnMensaje('error', 'Ingrese detalle de incidente')
                return false;
            }
            if (!confirm("Está seguro que desea guardar incidente?")) {
                return false
            }
            fnLoading(true);
            return true;
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
            min-width: 0px;
            max-width: 500px;
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
        #datetimepicker1 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
        }
        #datetimepicker2 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
            z-index: 99999;
        }
    </style>
</head>
<body class="">
    <form id="form1" runat="server" enctype="multipart/form-data">
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
            <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="container-fluid">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Sustentaciones Programadas</b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label7" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                                    <div class="col-sm-4 col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                            <%--<asp:ListItem Value="P">PENDIENTES</asp:ListItem>--%>
                                            <asp:ListItem Value="PS">POR SUSTENTAR</asp:ListItem>
                                            <asp:ListItem Value="CI">CON INCIDENTES</asp:ListItem>
                                            <%--<asp:ListItem Value="SO">SUSTENTADAS OBSERVADAS</asp:ListItem>--%>
                                            <%--<asp:ListItem Value="SC">SUSTENTADAS CALIFICADAS</asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>
                                    <%--<asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Fecha</asp:Label>
                                     <div class="col-sm-3 col-md-3">
                                        <div class="input-group date" id="datetimepicker2">
                                            <asp:TextBox runat="server" ID="txtFecha" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon"><span class="ion ion-calendar"></span></span>
                                        </div>
                                    </div>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="input-group date" id="Div3">
                                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-sm btn-primary btn-radius"
                                                Text="<span class='fa fa-pencil'></span>&nbsp;Buscar" OnClientClick="fnLoading(true);"></asp:LinkButton>
                                        </div>
                                    </div>--%>
                                    <%--      <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>--%>
                                </div>
                            </div>
                            <br />
                            <%--<asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                            <div class="form-group">
                                <div runat="server" id="Div2">
                                </div>
                                <asp:HiddenField runat="server" ID="hdjur" Value="0" />
                                <asp:HiddenField runat="server" ID="hdPst" Value="0" />
                                <asp:HiddenField runat="server" ID="hdtes" Value="0" />
                                <asp:HiddenField runat="server" ID="hddta" Value="0" />
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvTesis" CssClass="table table-condensed" DataKeyNames="codigo_Tes,titulo_tes,codigo_dta,codigo_pst,tieneincidentes"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="titulo_tes" HeaderText="TÍTULO" HeaderStyle-Width="45%" />
                                        <asp:BoundField DataField="Responsables" HeaderText="BACHILLER(ES)" HeaderStyle-Width="35%" />
                                        <asp:BoundField DataField="fecha_pst" HeaderText="FECHA Y HORA" HeaderStyle-Width="12%"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="10%" ShowHeader="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:LinkButton ID="btnReprogramar" runat="server" Text='<span class="fa fa-list"></span>'
                                                        CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Registrar Incidente" CommandName="RegistrarIncidente"
                                                        OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron Programaciones</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                            <br />
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                        <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                <ContentTemplate>
                    <div runat="server" id="DivIncidente" visible="false">
                        <div class="form-group">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>--%>
                                <h4 class="modal-title" id="H1">
                                    Registrar Incidente
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div id="Alumnos" runat="server">
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="col-sm-3 col-md-3 control-label">Carrera Profesional</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtCarrera" ReadOnly="true" CssClass="form-control"
                                                Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label17" runat="server" CssClass="col-sm-3 col-md-3 control-label">Título</asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:TextBox runat="server" ID="txtTitulo" ReadOnly="true" TextMode="MultiLine" Rows="3"
                                                CssClass="form-control" Text=""></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" CssClass="col-sm-3 col-md-3 control-label">Asistente Faltante</asp:Label>
                                        <div class="col-sm-8 col-md-8">
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="ddlAsistente">
                                                <asp:ListItem Value="">[-- Seleccione --]</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 col-md-3 control-label">Detalle de incidente</asp:Label>
                                        <div class="col-sm-8 col-md-8">
                                            <asp:TextBox runat="server" ID="txtdetalle" CssClass="form-control" Rows="4" MaxLength="1000"
                                                TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <center>
                                        <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-primary" Text="Guardar"
                                            OnClientClick="return ValidarGuardar();" />
                                        <asp:Button runat="server" ID="btnCerrar" CssClass="btn btn-danger" Text="Cerrar"
                                            OnClientClick="fnLoading(true);" />
                                    </center>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:GridView runat="server" ID="gvIncidentes" CssClass="table table-condensed" DataKeyNames="codigo_ist"
                                AutoGenerateColumns="false">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                    <asp:BoundField DataField="descripcion_tpi" HeaderText="TIPO" HeaderStyle-Width="10%" />
                                    <asp:BoundField DataField="asistente" HeaderText="ASISTENTE FALTANTE" HeaderStyle-Width="30%" />
                                    <asp:BoundField DataField="detalle_ist" HeaderText="DETALLE" HeaderStyle-Width="40%" />
                                    <asp:BoundField DataField="fecha_reg" HeaderText="FECHA REGISTRO" HeaderStyle-Width="20%"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <%--     <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="10%" ShowHeader="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:LinkButton ID="btnReprogramar" runat="server" Text='<span class="fa fa-list"></span>'
                                                        CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Registrar Incidente" CommandName="RegistrarIncidente"
                                                        OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                </Columns>
                                <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                <RowStyle Font-Size="12px" />
                                <EmptyDataTemplate>
                                    <b>No se encontraron Incidentes</b>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvTesis" EventName="RowCommand" />
                    <asp:AsyncPostBackTrigger ControlID="btnCerrar" />
                    <asp:AsyncPostBackTrigger ControlID="btnGuardar" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
