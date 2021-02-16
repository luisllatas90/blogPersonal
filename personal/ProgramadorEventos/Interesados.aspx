<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Interesados.aspx.vb" Inherits="Interesados"
    EnableViewStateMac="False" EnableEventValidation="False" %>

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
    <link href="libs/bootstrap-select-1.13.2/css/bootstrap-select.css" rel="stylesheet" type="text/css" />
    <link href="css/interesados.css?3" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="libs/popper-1.14.4/js/popper.js" type="text/javascript"></script>

    <script src="libs/bootstrap-4.1/js/bootstrap.js" type="text/javascript"></script>

    <script src="libs/bootstrap-select-1.13.2/js/bootstrap-select.js" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/fontawesome.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script src="js/interesados.js?31" type="text/javascript"></script>

    <script src="libs/iframeresizer/iframeResizer.min.js" type="text/javascript"></script>
    <script src="libs/iframeresizer/iframeResizer.contentWindow.min.js" type="text/javascript"></script>

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
                    <input type="hidden" id="hddFiltros" runat="server" />
                    <h2 class="page-header" id="pageHeader" runat="server">
                        <span style="color: rgb(205,0,0);">Interesados Potenciales</span></h2>
                    <iframe src="FiltrarInteresados.aspx" id="ifrmFiltrarInteresados" runat="server" frameborder="0"
                        width="100%" scrolling="no"></iframe>
                    <hr>
                    <asp:UpdatePanel ID="UpdatePanelDest" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="card">
                                <div class="card-header">Seleccionar evento de interés</div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-3">
                                            <label for="ddlTipoEstudioDest" class="form-control-sm">
                                                Tipo Estudio</label>
                                            <asp:DropDownList runat="server" ID="ddlTipoEstudioDest"
                                                CssClass="form-control form-control-sm" AutoPostBack="true">
                                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <label for="ddlConvocatoriaDest" class="form-control-sm">
                                                Convocatoria</label>
                                            <asp:DropDownList runat="server" ID="ddlConvocatoriaDest"
                                                CssClass="form-control form-control-sm" AutoPostBack="true">
                                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <label for="ddlEventoDest" class="form-control-sm">
                                                Evento destino</label>
                                            <asp:DropDownList runat="server" ID="ddlEventoDest"
                                                CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-12 col-sm-3">
                                            <label for="btnRegistrar" class="form-control-sm" visible="false" style="color: White;
                                                visibility: hidden;">
                                                .</label>
                                            <button type="button" id="fakeBtnRegistrar" name="fakeBtnRegistrar"
                                                class="form-control btn btn-success">
                                                Enviar
                                            </button>
                                            <button type="submit" id="btnRegistrar" name="btnRegistrar"
                                                class="form-control btn btn-success d-none" runat="server">
                                                Registrar
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

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

            $('#ddlCentroCosto').selectpicker({
                size: 10
            });

            $('#ddlGrados').selectpicker({
                size: 10
            });
        });

        Sys.Application.add_load(function () {
            switch (controlId) {
                case 'btnFiltrar':
                    BusquedaFinalizada();
                    break;

                case 'btnRegistrar':
                    EnvioFinalizado();
                    break;
            }

            if (controlId.indexOf('btnPage') > -1) {
                BusquedaFinalizada();
            }
        });

    </script>

</body>

</html>