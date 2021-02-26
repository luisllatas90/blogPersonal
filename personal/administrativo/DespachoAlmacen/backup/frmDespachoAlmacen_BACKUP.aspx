<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDespachoAlmacen.aspx.vb"
    Inherits="administrativo_DespachoAlmacen_frmDespachoAlmacen" %>

    <!DOCTYPE html
        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Despacho de Pedidos</title>
        <meta charset="UTF-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="X-UA-Compatible" content="IE=7" />
        <meta http-equiv="X-UA-Compatible" content="IE=8" />
        <meta http-equiv="X-UA-Compatible" content="IE=10" />

        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
        <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
        <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
        <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css">
        <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css">
        <link rel="stylesheet" href="css/bootstrap-select.css">

        <!-- Estilos propios -->
        <link rel="stylesheet" href="../../Alumni/css/estilos.css?13">

        <!-- Scripts externos -->
        <script src="../../assets/jquery/jquery-3.3.1.js"></script>
        <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
        <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js?12"></script>
        <script src="../../Alumni/js/popper.js"></script>
        <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
        <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>
        <script src="../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script>

        <!-- Scripts propios -->
        <script src="../../Alumni/js/funciones.js?1"></script>

        <script type="text/javascript">

            function udpFiltrosUpdate() {
                /*Combos*/
                $('.combo_filtro').selectpicker({
                    size: 6,
                });
            }

            function udpListaUpdate() {
                formatoGrillaListado();
            }

            function udpDetalleUpdate() {
                formatoGrillaDetalle();
            }

            /* Dar formato a la grilla (LISTADO). */
            function formatoGrillaListado() {
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

            /* Dar formato a la grilla (DETALLE). */
            function formatoGrillaDetalle() {
                // Setup - add a text input to each footer cell
                $('#grwDetalle thead tr').clone(true).appendTo('#grwDetalle thead');
                $('#grwDetalle thead tr:eq(1) th').each(function (i) {
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


                var table = $('#grwDetalle').DataTable({
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
                    estadoTabDetalle('D');

                } else if (tabActivo == 'detalle-tab') {
                    //HABILITAR
                    estadoTabDetalle('H');

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

            function estadoTabDetalle(estado) {
                if (estado == 'H') {
                    $("#detalle-tab").removeClass("disabled");
                    $("#detalle-tab").addClass("active");
                    $("#detalle").addClass("show");
                    $("#detalle").addClass("active");
                } else {
                    $("#detalle-tab").removeClass("active");
                    $("#detalle-tab").addClass("disabled");
                    $("#detalle").removeClass("show");
                    $("#detalle").removeClass("active");
                }
            }

        </script>
    </head>

    <body>
        <div class="loader"></div>
        <form id="frmDespachoAlmacen" runat="server">
            <asp:ScriptManager ID="scmDespachoAlmacen" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            </asp:UpdatePanel>

            <div class="container-fluid">
                <!--Cabecera de Panel-->
                <div class="card div-title">
                    <div class="row title">DESPACHO DE PEDIDOS</div>
                </div>
                <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="listado" aria-selected="true">Listado</a>

                    </li>
                    <li class="nav-item">
                        <a href="#detalle" id="detalle-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="detalle" aria-selected="false">Detalle</a>

                    </li>
                </ul>
                <div class="tab-content" id="contentTabs">
                    <!--#################### -->
                    <!--PESTAÑA 01: TAB. DE LISTADO-->
                    <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Filtros de Búsqueda</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbTipoFiltro"
                                                    class="col-sm-1 col-form-label form-control-sm">Buscar por:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="cmbTipoFiltro" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="PEDIDOS_TODOS">MOSTRAR TODOS LOS PEDIDOS
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="NUM_PEDIDO">NÚMERO DE PEDIDO</asp:ListItem>
                                                        <asp:ListItem Value="SOL_PEDIDO">SOLICITANTE DE PEDIDO
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="ANIO_PEDIDO">AÑO DE PEDIDO
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="PRODUCTO">PRODUCTO
                                                        </asp:ListItem>
                                                        <asp:ListItem Value="FECHAS">FECHAS (DESDE-HASTA)
                                                        </asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-7">
                                                    <asp:LinkButton ID="btnListar" runat="server"
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

                        <!--Contenedor de GridView (Pestaña 01)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwLista" runat="server" Width="100%" AutoGenerateColumns="false"
                                        ShowHeader="true" DataKeyNames="codPedido" CssClass="display table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nroPedido" HeaderText="N° DE PEDIDO" />
                                            <asp:BoundField DataField="fechPedido" HeaderText="FECHA DE PEDIDO" />
                                            <asp:BoundField DataField="nombSolic" HeaderText="SOLICITANTE" />
                                            <asp:BoundField DataField="centCosto" HeaderText="CENT. DE COSTOS" />
                                            <asp:BoundField DataField="codCentCosto" HeaderText="CÓD. CC." />

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPE.">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDetalle" runat="server"
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CommandName="Detalle" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Detalle de Pedido">
                                                        <span><i class="fa fa-search"></i></span>
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
                    <!--/TAB. DE LISTADO-->
                    <!--/#################### -->
                    <br>
                    <!--#################### -->
                    <!--PESTAÑA 02: TAB. DE DETALLE DE PEDIDO-->
                    <div class="tab-pane" id="detalle" role="tabpanel" aria-labelledby="detalle-tab">
                        <!--Contenedor de GridView (Pestaña 02)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpDetalle" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwDetalle" runat="server" Width="100%"
                                        AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codPedido"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nroPedido" HeaderText="N° DE PEDIDO" />
                                            <asp:BoundField DataField="fechPedido" HeaderText="FECHA DE PEDIDO" />
                                            <asp:BoundField DataField="nombSolic" HeaderText="SOLICITANTE" />
                                            <asp:BoundField DataField="centCosto" HeaderText="CENT. DE COSTOS" />
                                            <asp:BoundField DataField="codCentCosto" HeaderText="CÓD. CC." />
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
                    <!--/TAB. DE DETALLE DE PEDIDO-->
                    <!--/#################### -->
                </div>
            </div>

        </form>

        <script type="text/javascript">
            var controlId = '';

            /* Ejecutar funciones una vez cargada en su totalidad la página web. */
            $(document).ready(function () {
                /*Ocultar cargando*/
                udpFiltrosUpdate();
                $(".loader").fadeOut("slow");
            });


            function udpFiltrosUpdate() {
                /*Combos*/
                $('.combo_filtro').selectpicker({
                    size: 6,
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
                        AlternarLoading(false, 'Detalle');

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
                        AlternarLoading(true, 'Detalle');

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