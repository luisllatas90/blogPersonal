<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBandejaRecepcionObservaciones.aspx.vb" Inherits="administrativo_activofijo_Inventario_frmBandejaRecepcionObservaciones" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 
        <head runat="server">
        <title>Información Contable de Activos Fijos</title>

        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="X-UA-Compatible" content="IE=7" />
        <meta http-equiv="X-UA-Compatible" content="IE=8" />
        <meta http-equiv="X-UA-Compatible" content="IE=10" />

        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css/bootstrap.min.css" />
        <link rel="stylesheet" href="../../../assets/smart-wizard/css/smart_wizard.min.css" />
        <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css" />
        <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css" />
        <link rel="stylesheet" href="../../../Alumni/css/datatables/jquery.dataTables.min.css?12" />
        <link rel="stylesheet" href="../../../Alumni/css/sweetalert/sweetalert2.min.css" />

        <!-- Estilos propios -->
        <link rel="stylesheet" href="../../../Alumni/css/estilos.css?13" />

        <!-- Scripts externos -->
        <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
        <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
        <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
        <script src="../../../Alumni/js/popper.js"></script>
        <script src="../../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
        <script src="../../../Alumni/js/sweetalert/sweetalert2.js"></script>
        <script src="../../../Alumni/js/datatables/jquery.dataTables.min.js?20"></script>

        <!-- Scripts propios -->
        <script src="../../../Alumni/js/funciones.js?1"></script>

        <script type="text/javascript">
            function udpFiltrosUpdate() {
                /*Combos*/
                $(".combo_filtro").selectpicker({
                    size: 6,
                });
            }

            function udpListaUpdate() {
                formatoGrilla();
            }

            /* Dar formato a la grilla. */
            function formatoGrilla() {
                // Setup - add a text input to each footer cell
                $('#grwLista thead tr').clone(true).appendTo('#grwLista thead');
                $('#grwLista thead tr:eq(1) th').each(function (i) {
                    var title = $(this).text();
                    $(this).css("background-color", "white");
                    $(this).html(
                        '<input type="text" placeholder="Buscar ' + title + '" />'
                    );

                    $("input", this).on("keyup change", function () {
                        if (table.column(i).search() !== this.value) {
                            table
                                .column(i)
                                .search(this.value)
                                .draw();
                        }
                    });
                });


                var table = $('#grwLista').DataTable({
                    orderCellsTop: true,
                });


            }

            function alertConfirm(ctl, event, mensaje, icono) {
                // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
                var defaultAction = $(ctl).prop("href");
                // CANCEL DEFAULT LINK BEHAVIOUR
                event.preventDefault();

                swal({
                    title: mensaje,
                    type: icono,
                    showCancelButton: true,
                    confirmButtonText: "SI",
                    confirmButtonColor: "#45c1e6",
                    cancelButtonText: "NO"
                }).then(function (isConfirm) {
                    if (isConfirm) {
                        window.location.href = defaultAction;
                        return true;
                    } else {
                        return false;
                    }
                }).catch(swal.noop);
            }

            /* Flujo de tabs de la página principal. */
            function flujoTabs(tabActivo) {
                if (tabActivo == 'listado-tab') {
                    //HABILITAR
                    estadoTabListado('H');

                    //DESHABILITAR
                    estadoTabRegistro('D');

                } else if (tabActivo == 'registro-tab') {
                    //HABILITAR
                    estadoTabRegistro('H');

                    //DESHABILITAR
                    estadoTabListado('D');
                }
            }

            function estadoTabListado(estado) {
                if (estado == 'H') {
                    $("#listado-tab").removeClass("disabled");
                    $("#listado-tab").addClass("active");
                    $("#listado").addClass("show");
                    $("#listado").addClass("active");
                } else {
                    $("#listado-tab").removeClass("active");
                    $("#listado-tab").addClass("disabled");
                    $("#listado").removeClass("show");
                    $("#listado").removeClass("active");
                }
            }

            function estadoTabRegistro(estado) {
                if (estado == 'H') {
                    $("#registro-tab").removeClass("disabled");
                    $("#registro-tab").addClass("active");
                    $("#registro").addClass("show");
                    $("#registro").addClass("active");
                } else {
                    $("#registro-tab").removeClass("active");
                    $("#registro-tab").addClass("disabled");
                    $("#registro").removeClass("show");
                    $("#registro").removeClass("active");
                }
            }

        </script>

    </head>
    <body>
    <div class="loader"></div>
        <form id="frmInformacionContable" runat="server">
            <asp:ScriptManager ID="scmInformacionContable" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            </asp:UpdatePanel>

            <div class="container-fluid">
                <!--Cabecera de Panel-->
                <div class="card div-title">
                    <div class="row title">RECEPCIÓN DE OBSERVACIONES ENCONTRADAS</div>
                </div>
                <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="listado" aria-selected="true">Listado</a>

                    </li>
                    <li class="nav-item">
                        <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="registro" aria-selected="false">Registro</a>

                    </li>
                </ul>
                <div class="tab-content" id="contentTabs">
                    <!--Tab de Listado (Pestaña 01)-->
                    <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Observaciones Encontradas</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbTipoFiltro"
                                                    class="col-sm-1 col-form-label form-control-sm">Filtrar:</label>
                                                <div class="col-sm-3">
                                                   <asp:DropDownList ID="cmbTipoFiltro" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="Todos">TODAS
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="ENVIADAS">ENVIADAS</asp:ListItem>
                                                        <asp:ListItem Value="RECIBIDAS">RECIBIDAS
                                                        </asp:ListItem>                                                       
                                                    </asp:DropDownList>
                                                </div>                                               
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                  <asp:LinkButton ID="btnListarObservaciones" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Listar</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Panel de Filtro de Búsqueda-->

                        <!--Contenedor de GridView-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwLista" runat="server" Width="100%" AutoGenerateColumns="false"
                                        ShowHeader="true" DataKeyNames="cod_obsEnc" CssClass="display table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="equi_inv" HeaderText="EQUIPO DEL INVENTARIO" />
                                            <asp:BoundField DataField="resp_bien" HeaderText="RESPONSABLE DEL BIEN" />
                                            <asp:BoundField DataField="act_fijo" HeaderText="ACTIVO FIJO" />
                                            <asp:BoundField DataField="observ" HeaderText="OBSERVACION" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="VER EVIDENCIA">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVerEvidencia" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="Editar" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Evidencia">
                                                        <span><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="ADJUNTAR EVIDENCIA">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdjEvidencia" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="AdjuntarEvidencia" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Adjuntar Evidencia">
                                                        <span><i class="fa fa-paperclip"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos!
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle"
                                            HorizontalAlign="Center" Font-Size="12px" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Contenedor de GridView-->
                    </div>
                    <!--/Tab de Listado-->   
                    
                    <!--Tab de Adjuntar Evidencia (Pestaña 02)-->
                    <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">                                          
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Adjuntar Evidencias</div>
                                        <div class="card-body">
                                            <div class="row">                                               
                                                <label for="fuAdjuntarEvidencia"
                                                    class="col-sm-1 col-form-label form-control-sm">Subir Archivo:</label>
                                                <div class="col-sm-3">
                                                   <asp:FileUpload runat="server" ID ="fuAdjuntarEvidencia"/>
                                                </div>   
                                                <div class="col-sm-1"></div>
                                                 <div class="col-sm-2">
                                                    <asp:LinkButton ID="lnkBtnSalirAdjEvid" runat="server"
                                                        CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1"> 
                                                  <asp:LinkButton ID="btnAdjEvidec" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Registrar Evidencia</span>
                                                  </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>                     
                    </div>
                    <!--/Tab de Adjuntar Evidencia (Pestaña 02)-->   
                    
                </div>
            </div>

        </form>

        <script type="text/javascript">
            /* Ejecutar funciones una vez cargada en su totalidad la página web. */
            $(document).ready(function () {
                udpFiltrosUpdate();

                /*Ocultar cargando*/
                $(".loader").fadeOut("slow");
            });



            function udpFiltrosUpdate() {
                /*Combos*/
                $(".combo_filtro").selectpicker({
                    size: 3,
                });
            }

            /* Mostrar y ocultar gif al realizar un procesamiento. */
            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
                var elem = args.get_postBackElement();
                controlId = elem.id;

                switch (controlId) {
                    case 'btnListar':
                        AlternarLoading(false, 'Lista');
                        break;

                    case 'btnGuardar':
                        AlternarLoading(false, 'Registro');

                }
            });


            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
                var error = args.get_error();
                if (error) {
                    AlternarLoading(true, '');
                    return false;
                }

                switch (controlId) {
                    case 'btnListar':
                        AlternarLoading(true, 'Lista');
                        break;

                    case 'btnGuardar':
                        AlternarLoading(true, 'Registro');

                }
            });

            function AlternarLoading(retorno, elemento) {
                var $loadingGif;
                var $elemento;

                switch (elemento) {
                    case "Lista":
                        $loadingGif = $(".loader");
                        $elemento = $("#udpLista");
                        break;
                }

                if ($loadingGif != undefined) {
                    if (!retorno) {
                        $loadingGif.fadeIn("slow");
                        if ($elemento != undefined) {
                            $elemento.addClass("oculto");
                        }
                    } else {
                        $loadingGif.fadeOut("slow");
                        if ($elemento != undefined) {
                            $elemento.removeClass("oculto");
                        }
                    }
                }
            }
        </script>
    </body>

</html>
