<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDespAlmacen.aspx.vb"
    Inherits="administrativo_DespachoAlmacen_frmDespAlmacen" %>

    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <title>Despacho de Pedidos</title>
        <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta http-equiv="X-UA-Compatible" content="IE=7" />
        <meta http-equiv="X-UA-Compatible" content="IE=8" />
        <meta http-equiv="X-UA-Compatible" content="IE=10" />

        <!-- Estilos externos -->
        <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css"/>
        <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css"/>
        <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css"/>
        <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css"/>
        <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css"/>
        <link rel="stylesheet" href="css/bootstrap-select.css"/>

        <!-- Estilos propios -->
        <link rel="stylesheet" href="../../Alumni/css/estilos.css"/>

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

                } else if (tabActivo == 'despacho-tab') {
                    //HABILITAR
                    estadoTabDespacho('H');

                    //DESHABILITAR
                    estadoTabDetalle('D');

                } else if (tabActivo == 'despacho->detalle-tab') {                    
                    //HABILITAR
                    estadoTabDetalle('H');

                    //DESHABILITAR
                    estadoTabDespacho('D');

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

                function estadoTabDespacho(estado) {
                if (estado == 'H') {
                    $("#despacho-tab").removeClass("disabled");
                    $("#despacho-tab").addClass("active");
                    $("#despacho").addClass("show");
                    $("#despacho").addClass("active");
                } else {
                    $("#despacho-tab").removeClass("active");
                    $("#despacho-tab").addClass("disabled");
                    $("#despacho").removeClass("show");
                    $("#despacho").removeClass("active");
                }
            }

            /* Flujo de para activar y desactivar controles de: Detalle de Pedido */
            function flujoCont(controles)
            {
                if (controles == 'detalle-tab') {
                    //DESHABILITAR
                    estadoControles('D');
                } else {
                    //HABILITAR
                     estadoControles('H');
                }


            }

               function flujoContHab(controles)
            {
                if (controles == 'detalle-tab-hab') {
                    //HABILITAR
                    estadoControles('H');
                } 

            }

            function estadoControles(estado)
            {
                if (estado == 'D') {
                    $('#txtCantDespachar').attr("disabled", true);
                    $('#txtCantComprar').attr("disabled", true);
                    $('#lnkBtnOtroStock').attr("disabled", true);
                    $('#cbEstado').attr("disabled", true);
                } else {
                    $('#txtCantDespachar').attr("disabled", false);
                    $('#txtCantComprar').attr("disabled", false);
                    $('#lnkBtnOtroStock').attr("disabled", false);
                    $('#cbEstado').attr("disabled", false);
                }
                
            }

        </script>
    </head>

    <body>
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
                     <li class="nav-item">
                        <a href="#detalle" id="despacho-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="despacho" aria-selected="false">Despacho</a>

                    </li>
                </ul>
                <div class="tab-content" id="contentTabs">
                    <!--############################ -->
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

                                      
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="OPE.">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDetalle" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="DetallePedido" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Agregar Datos Contables">
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
                    <!--/PESTAÑA 01: TAB. DE LISTADO-->
                    <!--/########################## -->                  
                    <!--###################################### -->
                  <!--PESTAÑA 02: TAB. DE DETALLE DE PEDIDO-->
                  <div class="tab-pane" id="detalle" role="tabpanel" aria-labelledby="detalle-tab">
                       <!--Panel de Edición-->
                       <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpEdicion" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Editar Pedido</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtCantDespachar"
                                                    class="col-sm-2 col-form-label form-control-sm">Cantidad Despachar:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtCantDespachar" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCantComprar"
                                                    class="col-sm-2 col-form-label form-control-sm">Cantidad Comprar:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtCantComprar" runat ="server" ></asp:TextBox>
                                                </div>
                                             
                                            </div>
                                            <div class="row">
                                                 <label for="lnkBtnOtroStock"
                                                    class="col-sm-2 col-form-label form-control-sm">Otro Stock:</label>
                                                <div class="col-sm-2">
                                                    <asp:LinkButton ID="lnkBtnOtroStock" runat ="server" >
                                                       <span><i class="fa fa-plus"></i></span>
                                                    </asp:LinkButton>
                                                </div>
                                                <label for="cbEstado"
                                                    class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                                <div class="col-sm-2">
                                                     <asp:DropDownList ID="cbEstado" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="Atendido">ATENDIDO</asp:ListItem>
                                                        <asp:ListItem Value="PorDespachar">POR DESPACHAR</asp:ListItem>                            
                                                    </asp:DropDownList>
                                                </div>

                                            </div>
                                            <div class="row">
                                                 <div class="col-md-2">
                                                    <asp:LinkButton ID="lknBtnEditarPedido" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Registrar</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lknBtnDespachar" runat="server"
                                                        CssClass="btn btn-accion btn-azul">
                                                        <i class="fa fa-cart-arrow-down "></i>
                                                        <span class="text">Atender Pedido</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:LinkButton ID="lnkBtnSalirDetalle" runat="server"
                                                        CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                       <!--/Panel de Edición-->                                                
                      
                      <!--Contenedor de GridView (Pestaña 02)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpDetalle" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwDetalle" runat="server" Width="100%"
                                        AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codDetPedido"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="Selec.">
                                                <ItemTemplate>
                                                          <asp:CheckBox ID="chkSelecPedido" runat ="server" AutoPostBack="True" OnCheckedChanged="chkSelecPedido_Change" onchange="javascript:return flujoCont('detalle-tab-hab');"/>                                  
                                        
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="item" HeaderText="ITEM" />
                                            <asp:BoundField DataField="unidad" HeaderText="UNIDAD" />
                                            <asp:BoundField DataField="cantPed" HeaderText="CANT. PED." />
                                            <asp:BoundField DataField="cantAte" HeaderText="CANT. ATEND." />
                                            <asp:BoundField DataField="cantPorAte" HeaderText="CANT. POR ATEN." />
                                            <asp:BoundField DataField="stoActual" HeaderText="STOCK ACTUAL" />
                                            <asp:BoundField DataField="otroStock" HeaderText="OTRO STOCK" />
                                            <asp:BoundField DataField="cantFalt" HeaderText="CANT. FALTANTE" />                                      
                                            <asp:BoundField DataField="cantDesp" HeaderText="CANT. DESPACHAR" />
                                            <asp:BoundField DataField="cantComp" HeaderText="CANT. COMPRAR" />
                                            <asp:BoundField DataField="fecDesea" HeaderText="FEC. DESEADA" />
                                            <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                                            <asp:BoundField DataField="centCostos" HeaderText="CENTROS DE COSTOS" />
                                            <asp:BoundField DataField="precUnit" HeaderText="PRECIO UNITARIO" />
                                            <asp:BoundField DataField="observ" HeaderText="OBSERVACIÓN" />
                                            <asp:BoundField DataField="despA" HeaderText="DESPACHAR A" />
                                          
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
                    <!--/PESTAÑA 02: TAB. DE DETALLE DE PEDIDO-->
                    <!--/#################################### -->                  
                    <!-- PESTAÑA 03: TAB. DESPACHO DE PEDIDO-->
                    <!-- #################################### -->
                     <div class="tab-pane" id="despacho" role="tabpanel" aria-labelledby="despacho-tab">
                       <!--Panel de Despacho de Pedido-->
                       <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpDespacho" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Datos del Despacho</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtEntregadoA" class="col-sm-2 col-form-label form-control-sm">Entregado a:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtEntregadoA" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="cbMotivo"
                                                    class="col-sm-2 col-form-label form-control-sm">Motivo:</label>
                                                <div class="col-sm-2">
                                                     <asp:DropDownList ID="cbMotivo" runat="server"
                                                        AutoPostBack="false"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                        <asp:ListItem Value="salida">SALIDA</asp:ListItem>
                                                        <asp:ListItem Value="ingreso">INGRESO</asp:ListItem>    
                                                        <asp:ListItem Value="transfMismoAlmacen">TRANSFERENCIA MISMO ALMACÉN</asp:ListItem> 
                                                        <asp:ListItem Value="transfDistiAlmacen">TRANSFERENCIA DISTINTO ALMACÉN</asp:ListItem> 
                                                        <asp:ListItem Value="ingresoxDevolucion">INGRESO POR DEVOLUCIÓN</asp:ListItem> 
                                                        <asp:ListItem Value="salidaxDevolucion">SALIDA POR DEVOLUCIÓN</asp:ListItem> 
                                                        <asp:ListItem Value="conversUnidad">CONVERSIÓN UNIDAD</asp:ListItem> 
                                                        <asp:ListItem Value="ingresoRegulariz">INGRESO POR REGULARIZACIÓN</asp:ListItem> 
                                                        <asp:ListItem Value="salidaRegulariz">SALIDA POR REGULARIZACIÓN</asp:ListItem> 
                                                        <asp:ListItem Value="registroTransfere">REGISTRO DE TRANSFERENCIA</asp:ListItem>                                              
                                                      </asp:DropDownList>
                                                </div>
                                             
                                            </div>
                                            <div class="row">
                                                 <label class="col-sm-2 col-form-label form-control-sm">Glosario:</label>                                              
                                                 <label class="col-sm-2 col-form-label form-control-sm">Nro. Pedido: 133152</label>                                                   
                                              
                                                <label for="chkGuiaRemision"
                                                    class="col-sm-2 col-form-label form-control-sm">Guía de Remisión:</label>
                                                <div class="col-sm-2">
                                                     <asp:CheckBox ID="chkGuiaRemision" runat ="server"/>
                                                </div>

                                            </div>
                                            <div class="row">
                                                 <div class="col-md-2">
                                                    <asp:LinkButton ID="lnkBtnAtenderPedido" runat="server"
                                                        CssClass="btn btn-accion btn-azul">
                                                        <i class="fa fa-cart-arrow-down"></i>
                                                        <span class="text">Confirmar</span>
                                                    </asp:LinkButton>
                                                </div>    
                                                 <div class="col-md-2">
                                                    <asp:LinkButton ID="lnkBtnRechazarPedido" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-times-circle"></i>
                                                        <span class="text">Rechazar Pedido</span>
                                                    </asp:LinkButton>
                                                </div>    
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="lnkBtnAtenderTransferencia" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-exchange-alt"></i>
                                                        <span class="text">Atender por Transferencia</span>
                                                    </asp:LinkButton>
                                                </div>
                                                 <div class="col-md-2">
                                                    <asp:LinkButton ID="lnkBtnSalirDespacho" runat="server"
                                                        CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                       <!--/Panel de Despacho de Pedido-->
                      
                      <!--Contenedor de GridView (Pestaña 03)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpDespachoPedido" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="gvAtenderPedido" runat="server" Width="100%"
                                        AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codDespacho"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>
                                            
                                            <asp:BoundField DataField="item" HeaderText="ITEM" />
                                            <asp:BoundField DataField="unidad" HeaderText="UNIDAD" />                                            
                                            <asp:BoundField DataField="cantAte" HeaderText="CANT. ATEND." />                                                                                                                                           
                                            <asp:BoundField DataField="cantComp" HeaderText="CANT. COMPRAR" />                                                                                                                                                 
                                          
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
                        <!--/Contenedor de GridView  (Pestaña 03)-->
                    </div>
                     <!-- PESTAÑA 03: TAB. DESPACHO DE PEDIDO-->
                    <!-- #################################### -->

                </div>
            </div>

        </form>
    </body>

    </html>