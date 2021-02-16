<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FiltrarInteresados.aspx.vb"
    Inherits="FiltrarInteresados" EnableViewStateMac="False" EnableEventValidation="False" %>

<!DOCTYPE html
    PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Interesados Potenciales</title>
    <link href="libs/bootstrap-4.1/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="libs/bootstrap-select-1.13.9/css/bootstrap-select.css?x=1" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="libs/bootstrap-datepicker-1.8.0/css/bootstrap-datepicker.min.css" rel="stylesheet" type="text/css">
    <link href="css/filtrarInteresados.css?x=23" rel="stylesheet" type="text/css" />

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="libs/popper-1.14.4/js/popper.js" type="text/javascript"></script>

    <script src="libs/bootstrap-4.1/js/bootstrap.js" type="text/javascript"></script>

    <script src="libs/bootstrap-select-1.13.9/js/bootstrap-select.js?x=1" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/fontawesome.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script src="libs/iframeresizer/iframeResizer.contentWindow.min.js" type="text/javascript"></script>

    <script src="libs/bootstrap-datepicker-1.8.0/js/bootstrap-datepicker.js"></script>
    
    <script src="libs/bootstrap-datepicker-1.8.0/locales/bootstrap-datepicker.es.min.js"></script>

    <script src="js/filtrarInteresados.js?x=36" type="text/javascript"></script>

    <script type="text/javascript">
        function mostrarMensaje(mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box = bootbox.alert({ message: mensaje, backdrop: true });
            box.find('.modal-body').css({ 'background-color': backcolor });
            box.find('.bootbox-body').css({ 'color': forecolor });
            box.find(".btn-primary").removeClass("btn-primary").addClass("btn-" + tipo);
        }
    </script>

</head>

<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel panel-body">
                    <asp:UpdatePanel ID="udpPanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="udpFiltrosSel" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <input type="hidden" id="hddFiltrosSel" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div class="container-filtros">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-3">
                                        <label for="cboFiltro" class="form-control-sm">
                                            Buscar por</label>
                                        <select id="cboFiltro" class="form-control form-control-sm" runat="server"
                                            onchange="seleccionarTipo(this);">
                                            <option value="I" selected="selected">INTERESADOS</option>
                                            <option value="A">ALUMNOS</option>
                                        </select>
                                    </div>
                                    <div class="col-xs-12 col-sm-4">
                                        <label for="ddlTipoEstudio" class="form-control-sm">
                                            Tipo Estudio</label>
                                        <asp:DropDownList runat="server" ID="ddlTipoEstudio"
                                            CssClass="form-control form-control-sm" AutoPostBack="true">
                                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-12 col-sm-5">
                                        <asp:UpdatePanel ID="udpConvocatoria" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <label for="ddlConvocatoria" class="form-control-sm">
                                                    Convocatoria</label>
                                                &nbsp;&nbsp;<asp:DropDownList runat="server" ID="ddlConvocatoria"
                                                    CssClass="form-control form-control-sm" AutoPostBack="true">
                                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-5">
                                        <label for="ddlEvento" class="form-control-sm">
                                            Evento</label>
                                        <asp:UpdatePanel ID="udpEvento" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="container-limpiar">
                                                    <asp:ListBox ID="ddlEvento" runat="server" SelectionMode="Multiple"
                                                        CssClass="form-control form-control-sm selectpicker"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                                    </asp:ListBox>
                                                    <button type="button" class="btn btn-sm btn-light btn-limpiar"
                                                        id="btnLimpiarEvento">X</button>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-xs-12 col-sm-7">
                                        <label for="ddlCarreraProfesional" class="form-control-sm">
                                            Carrera Profesional</label>
                                        <div class="container-limpiar">
                                            <asp:ListBox ID="ddlCarreraProfesional" runat="server" SelectionMode="Multiple"
                                                CssClass="form-control form-control-sm" AutoPostBack="true">
                                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                            </asp:ListBox>
                                            <button type="button" class="btn btn-sm btn-light btn-limpiar"
                                                id="btnLimpiarCarreraProfesional">X</button>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-5">
                                        <asp:UpdatePanel ID="udpInstitucionEducativa" runat="server"
                                            UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <label for="ddlInstitucionEducativa" class="form-control-sm">
                                                    Institución educativa</label>
                                                &nbsp;&nbsp;<div class="container-limpiar">
                                                    <asp:ListBox ID="ddlInstitucionEducativa" runat="server"
                                                        SelectionMode="Multiple"
                                                        CssClass="form-control form-control-sm selectpicker"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                                    </asp:ListBox>
                                                    <button type="button" class="btn btn-sm btn-light btn-limpiar"
                                                        id="btnLimpiarInstitucionEducativa">X</button>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-xs-8 col-sm-5">
                                        <asp:UpdatePanel ID="udpCentroCosto" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div id="divCentroCosto" runat="server" class="">
                                                    <label for="ddlCentroCosto" class="form-control-sm">
                                                        Centro Costo</label>
                                                    <div class="container-limpiar">
                                                        <asp:ListBox runat="server" ID="ddlCentroCosto"
                                                            SelectionMode="Multiple"
                                                            CssClass="form-control form-control-sm selectpicker"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                                        </asp:ListBox>
                                                        <button type="button" class="btn btn-sm btn-light btn-limpiar"
                                                            id="btnLimpiarCentroCosto">X</button>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="udpGrados" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div id="divGrados" runat="server" class="col-xs-12 col-sm-8">
                                                        <label for="ddlGrados" class="form-control-sm">
                                                            Grados</label>
                                                        <div class="container-limpiar">
                                                            <asp:ListBox ID="ddlGrados" runat="server"
                                                                SelectionMode="Multiple"
                                                                CssClass="form-control form-control-sm selectpicker"
                                                                AutoPostBack="true">
                                                                <asp:ListItem Value="T">TERCERO</asp:ListItem>
                                                                <asp:ListItem Value="C">CUARTO</asp:ListItem>
                                                                <asp:ListItem Value="Q">QUINTO</asp:ListItem>
                                                                <asp:ListItem Value="E">EGRESADO</asp:ListItem>
                                                                <asp:ListItem Value="U">UNIVERSITARIO</asp:ListItem>
                                                            </asp:ListBox>
                                                            <button type="button" class="btn btn-sm btn-light btn-limpiar"
                                                                id="btnLimpiarGrados">X</button>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4 col-sm-2">
                                        <label for="txtFechaDesde" class="form-control-sm">Desde</label>
                                        <asp:TextBox ID="txtFechaDesde" runat="server" CssClass="form-control form-control-sm" placeholder="Fecha Desde" />
                                    </div>
                                    <div class="col-xs-4 col-sm-2">
                                        <label for="txtFechaHasta" class="form-control-sm">Hasta</label>
                                        <asp:TextBox ID="txtFechaHasta" runat="server" CssClass="form-control form-control-sm" placeholder="Fecha Hasta" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-6 col-sm-6">
                                        <asp:UpdatePanel ID="udpPreferente" runat="server" UpdateMode="Always">
                                            <ContentTemplate>
                                                <label for="ddlInstitucionEducativa" class="form-control-sm">Ins. Edu. Preferente (Con convenio)</label>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="custom-control custom-radio custom-control-inline">
                                                            <asp:RadioButton id="rbtPreferenteTodos" GroupName="rbtPreferente" aria-checked="true"
                                                                runat="server" Text="Todos" AutoPostBack="true" />
                                                        </div>
                                                        <div class="custom-control custom-radio custom-control-inline">
                                                            <asp:RadioButton id="rbtSoloPreferente" GroupName="rbtPreferente" 
                                                                runat="server" Text="Preferente" AutoPostBack="true" />
                                                        </div>
                                                        <div class="custom-control custom-radio custom-control-inline">
                                                            <asp:RadioButton id="rbtSoloNoPreferente" GroupName="rbtPreferente"
                                                                runat="server" Text="No Preferente" AutoPostBack="true" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <hr>
                            <img src="img/loading.gif" class="loading-gif">
                            <asp:UpdatePanel ID="udpTotalInteresados" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <h5 id="totalInteresados" runat="server">Se han encontrado x interesados</h5>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            loading(true);

            switch (controlId) {
                case 'btnFiltrar':
                    Buscando();
                    break;
            }

            if (controlId.indexOf('btnPage') > -1) {
                Buscando();
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;

                switch (udpPanelId) {
                    case 'UpdatePanel':
                        InicializarControles();
                        break;

                    case 'udpEvento':
                        InitDdlEvento();
                        break;

                    case 'udpInstitucionEducativa':
                        InitDdlInstitucionEducativa();
                        break;

                    case 'udpPreferente':
                        FormatRbtPreferente();
                        break;
                }
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                //Manejar el error
                args.set_errorHandled(true);
                return false;
            }
        });

        Sys.Application.add_load(function () {
            switch (controlId) {
                case 'btnFiltrar':
                    BusquedaFinalizada();
                    break;
            }

            if (controlId.indexOf('btnPage') > -1) {
                BusquedaFinalizada();
            }

            loading(false);
        });

    </script>

</body>

</html>