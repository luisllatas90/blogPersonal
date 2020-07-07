<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMantenimientoRequisitosAdmision.aspx.vb" Inherits="administrativo_gestion_educativa_frmMantenimientoRequisitosAdmision" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
    <title>Requisitos de Admisión</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css">
    <link rel="stylesheet" href="css/mantenimientoRequisitosAdmision.css?x=1">
</head>

<body>
    <div id="loading-layer">
        <img src="img/loading.gif" class="loading-gif">
    </div>
    <form id="frmMantenimiento" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpHidden" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <ContentTemplate>
                <asp:HiddenField ID="hddRpta" Value="" runat="server" />
                <asp:HiddenField ID="hddMsg" Value="" runat="server" />
                <asp:HiddenField ID="hddCod" Value="" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="container-fluid">
            <h5 class="main-title">Requisitos de Admisión</h5>
            <ul class="nav nav-tabs" role="tablist" id="divMainTabs">
                <li class="nav-item">
                    <a href="#lista" id="lista-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="lista" aria-selected="true">Lista</a>
                </li>
                <li class="nav-item">
                    <a href="#mantenimiento" id="mantenimiento-tab" class="nav-link disabled" data-toggle="tab"
                        role="tab" aria-controls="mantenimiento" aria-selected="false">Mantenimiento</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="lista" role="tabpanel" aria-labelledby="lista-tab">
                    <div class="card" id="filters-card">
                        <div class="card-body">
                            <h5 class="title">Filtros</h5>
                            <hr>
                            <div class="row form-group">
                                <label for="cmbFiltroTipoEstudio"
                                    class="col-md-7 col-sm-9 col-12 col-form-label form-control-sm">Tipo
                                    de Estudio:</label>
                                <div class="col-md-10 col-sm-15 col-34">
                                    <asp:UpdatePanel ID="udpFiltroTipoEstudio" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbFiltroTipoEstudio" runat="server"
                                                AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label for="cmbFiltroModalidad"
                                    class="col-md-5 col-sm-9 col-12 col-form-label form-control-sm">Modalidad:</label>
                                <div class="col-md-10 col-sm-15 col-34">
                                    <asp:UpdatePanel ID="udpFiltroModalidad" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cmbFiltroModalidad" runat="server" AutoPostBack="true"
                                                CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <label for="txtFiltroNombre"
                                    class="col-md-5 col-sm-9 col-12 col-form-label form-control-sm">Nombre:</label>
                                <div class="col-md-9 col-sm-15 col-34">
                                    <input type="text" id="txtFiltroNombre" class="form-control form-control-sm" placeholder="Buscar">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="card-header">
                            <div id="divOpciones" class="row">
                                <div class="col-sm-22">Lista de Requisitos</div>
                                <div class="col-sm-4 text-center rel-absolute">
                                    <img src="img/loading.gif" class="loading-gif" id="loading-gif">
                                </div>
                                <div class="col-sm-22 text-right">
                                    <asp:UpdatePanel ID="udpBotonesLista" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <button type="button" id="btnNuevo" runat="server"
                                                class="btn btn-accion btn-verde">
                                                <i class='fa fa-plus-square'></i>
                                                <span class="text">Nuevo</span>
                                            </button>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpGrwRequisitosAdmision" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwRequisitosAdmision" runat="server" AutoGenerateColumns="false"
                                        DataKeyNames="codigo_req" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField HeaderText="Nro" HeaderStyle-Width="40px" />
                                            <asp:BoundField DataField="descripcion_req" HeaderText="Nombre" />
                                            <asp:TemplateField ItemStyle-CssClass="operacion" HeaderText="Editar"
                                                HeaderStyle-Width="100px" />
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="tab-pane show" id="mantenimiento" role="tabpanel" aria-labelledby="m-tab">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="title">Formulario de Mantenimiento</h5>
                            <img src="img/loading.gif" class="loading-gif" id="loading-gif-mantenimiento">
                            <hr>
                            <asp:UpdatePanel ID="udpMantenimiento" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row form-group">
                                        <label for="txtDescripcion"
                                            class="col-md-7 col-sm-9 col-12 col-form-label form-control-sm">Nombre:</label>
                                        <div class="col-md-24 col-sm-48">
                                            <input type="text" id="txtDescripcion" class="form-control form-control-sm"
                                                runat="server">
                                        </div>
                                    </div>
                                    <hr>
                                    <div class="row form-group">
                                        <div class="col-sm-41 offset-sm-7">
                                            <div class="badge badge-info">Seleccione las modalidades a las que pertenece
                                                el requisito</div>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbTipoEstudio"
                                            class="col-md-7 col-sm-9 col-12 col-form-label form-control-sm">Tipo
                                            de Estudio:</label>
                                        <div class="col-md-10 col-sm-15 col-34">
                                            <asp:UpdatePanel ID="udpTipoEstudio" runat="server" UpdateMode="Conditional"
                                                ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="cmbTipoEstudio" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm selectpicker"
                                                        data-live-search="true">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <label for="cmbModalidad"
                                            class="col-md-7 col-sm-9 col-12 col-form-label form-control-sm">Modalidad(es):</label>
                                        <div class="col-md-24 col-sm-48">
                                            <asp:UpdatePanel ID="udpModalidad" runat="server" UpdateMode="Conditional"
                                                ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:ListBox ID="cmbModalidad" runat="server" AutoPostBack="true"
                                                        SelectionMode="Multiple"
                                                        CssClass="form-control form-control-sm selectpicker"
                                                        data-live-search="true">
                                                    </asp:ListBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="row form-group">
                                        <div class="col-md-41 offset-md-7">
                                            <asp:UpdatePanel ID="udpBotonesMantenimiento" runat="server"
                                                UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnGuardar" runat="server"
                                                        class="btn btn-accion btn-azul">
                                                        <i class='fa fa-save'></i>
                                                        <span class="text">Guardar</span>
                                                    </button>
                                                    <button type="button" id="btnCancelar"
                                                        class="btn btn-accion btn-cancel">
                                                        <i class='fa fa-ban'></i>
                                                        <span class="text">Cancelar</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlConfirm" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Confirmar operación</h5>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="alert" id="messageConfirm"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="btnConfirm">Continuar</button>
                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/mantenimientoRequisitosAdmision.js?x=1"></script>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id

            switch (controlId) {
                case 'btnNuevo':
                    stateButton(controlId);
                    break;
                case 'btnGuardar':
                    stateLoadingGif('mantenimiento', false);
                    stateButton(controlId);
                    stateButton('btnCancelar');
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                stateButton('btnNuevo');
                stateButton(controlId);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                // Manejar el error
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                switch (udpPanelId) {
                    case 'udpTipoEstudio':
                        initCombo('cmbTipoEstudio');
                        break;
                    case 'udpFiltroModalidad':
                        initCombo('cmbFiltroModalidad');
                        break;
                    case 'udpModalidad':
                        initCombo('cmbModalidad');
                        break;
                    case 'udpMantenimiento':
                        initCombo('cmbTipoEstudio');
                        initCombo('cmbModalidad');
                        break;
                    case 'udpGrwRequisitosAdmision':
                        reformatOnClick();
                        clearFilter();
                        break;
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);

            switch (controlId) {
                case 'btnNuevo':
                    toggleTab(true);
                    stateButton(controlId);
                    break;
                case 'btnGuardar':
                    stateLoadingGif('mantenimiento', true);
                    stateButton(controlId);
                    stateButton('btnCancelar');
                    checkResponseForm();
                    break;
            }

            if (controlId.indexOf('btnEditar') > -1) {
                toggleTab(true);
                stateButton('btnNuevo');
                stateButton(controlId);
            }

            checkResponsePostback();
        });    
    </script>
</body>

</html>