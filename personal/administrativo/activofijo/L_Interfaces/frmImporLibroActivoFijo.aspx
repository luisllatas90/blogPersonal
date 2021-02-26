

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmImporLibroActivoFijo.aspx.vb" Inherits="administrativo_activofijo_L_Interfaces_frmImporLibroActivoFijo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 
        <head runat="server">
        <title>Libro de Activo Fijo</title>

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
        <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css" />
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
        <script src="../../../assets/bootstrap-datepicker/js/bootstrap-datepicker.min.js"></script>
        <script src="../../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>

        <!-- Scripts propios -->
        <script src="../../../Alumni/js/funciones.js?1"></script>

        <script type="text/javascript">
            function udpFiltrosUpdate() {
                /*Combos*/
                $(".combo_filtro").selectpicker({
                    size: 6,
                });
            }

            $(function () {               
                $('#txtFechaDesde').datepicker(
                    {
                       language: 'es'
                    }
                 ).datepicker("setDate", new Date());

            });

              $(function () {
                  $('#txtFechaHasta').datepicker(
                      {
                           language: 'es'
                      }).datepicker("setDate", new Date());
            });          


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
                    estadoTabAdjuntar('D');

                } else if (tabActivo == 'registrar-tab') {
                    //HABILITAR
                    estadoTabARegistrar('H');

                    //DESHABILITAR
                    estadoTabListado('D');
                } else if (tabActivo == 'editar-tab') {
                    //HABILITAR
                    estadoTabEditar('H');

                    //DESHABILITAR
                    estadoTabListado('D');
                } else if (tabActivo == 'registrar->listar') {
                    //HABILITAR
                     estadoTabListado('H');

                    //DESHABILITAR
                     estadoTabARegistrar('D');
                   
                } else if (tabActivo == 'editar->listar') {
                    //HABILITAR
                     estadoTabListado('H');

                    //DESHABILITAR
                     estadoTabEditar('D');
                   
                } else if (tabActivo == 'listar->importar') {
                    //HABILITAR
                     estadoTabImportar('H');

                    //DESHABILITAR
                     estadoTabListado('D');
                   
                } else if (tabActivo == 'importar->listar') {
                    //HABILITAR
                     estadoTabListado('H');

                    //DESHABILITAR
                     estadoTabImportar('D');
                   
                } else if (tabActivo == 'listar->resumenfinal') {
                    //HABILITAR
                     estadoTabResumenFinal('H');

                    //DESHABILITAR
                     estadoTabListado('D');
                   
                } else if (tabActivo == 'resumenfinal->listar') {
                    //HABILITAR
                     estadoTabListado('H');

                    //DESHABILITAR
                     estadoTabResumenFinal('D');
                   
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

            function estadoTabARegistrar(estado) {
                if (estado == 'H') {
                    $("#registrar-tab").removeClass("disabled");
                    $("#registrar-tab").addClass("active");
                    $("#registrar").addClass("show");
                    $("#registrar").addClass("active");
                } else {
                    $("#registrar-tab").removeClass("active");
                    $("#registrar-tab").addClass("disabled");
                    $("#registrar").removeClass("show");
                    $("#registrar").removeClass("active");
                }
            }

             function estadoTabEditar(estado) {
                if (estado == 'H') {
                    $("#editar-tab").removeClass("disabled");
                    $("#editar-tab").addClass("active");
                    $("#editar").addClass("show");
                    $("#editar").addClass("active");
                } else {
                    $("#editar-tab").removeClass("active");
                    $("#editar-tab").addClass("disabled");
                    $("#editar").removeClass("show");
                    $("#editar").removeClass("active");
                }
            }

            
             function estadoTabImportar(estado) {
                if (estado == 'H') {
                    $("#importar-tab").removeClass("disabled");
                    $("#importar-tab").addClass("active");
                    $("#importar").addClass("show");
                    $("#importar").addClass("active");
                } else {
                    $("#importar-tab").removeClass("active");
                    $("#importar-tab").addClass("disabled");
                    $("#importar").removeClass("show");
                    $("#importar").removeClass("active");
                }
            }

             function estadoTabResumenFinal(estado) {
                if (estado == 'H') {
                    $("#resumen-tab").removeClass("disabled");
                    $("#resumen-tab").addClass("active");
                    $("#resumen").addClass("show");
                    $("#resumen").addClass("active");
                } else {
                    $("#resumen-tab").removeClass("active");
                    $("#resumen-tab").addClass("disabled");
                    $("#resumen").removeClass("show");
                    $("#resumen").removeClass("active");
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
                    <div class="row title">LIBRO DE ACTIVO FIJO</div>
                </div>
                <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="listado" aria-selected="true">Listado</a>

                    </li>
                    <li class="nav-item">
                        <a href="#registrar" id="registrar-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="registro" aria-selected="false">Registrar</a>

                    </li>
                    <li class="nav-item">
                        <a href="#editar" id="editar-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="editar" aria-selected="false">Editar</a>

                    </li>
                    <li class="nav-item">
                        <a href="#importar" id="importar-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="importar" aria-selected="false">Importar</a>

                    </li>
                    <li class="nav-item">
                        <a href="#resumen" id="resumen-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="resumen" aria-selected="false">Resumen Final</a>

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
                                        <div class="card-header">Filtro</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtFechaDesde"
                                                    class="col-3 col-sm-1 col-form-label form-control-sm">Fecha Desde:</label>
                                                <div class="col-sm-3">
                                                  
                                                      <asp:TextBox id="txtFechaDesde" runat ="server"></asp:TextBox>
                                                </div>   
                                                 <label for="txtFechaHasta"
                                                    class="col-3 col-sm-1 col-form-label form-control-sm">Fecha Hasta:</label>
                                                <div class="col-sm-3">
                                                   <asp:TextBox id="txtFechaHasta" runat ="server"></asp:TextBox>
                                                </div>    
                                            </div>
                                            <div class="row">
                                                <div class="col col-sm-1">
                                                  <asp:LinkButton ID="btnListarLibroContable" runat="server"
                                                        CssClass="btn btn-primary">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Listar</span>
                                                    </asp:LinkButton>
                                                </div>
                                                <div class="col col-sm-1">
                                                  <asp:LinkButton ID="btnNuevoRegistro" runat="server"
                                                        CssClass="btn btn-success">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Nuevo</span>
                                                    </asp:LinkButton>
                                                </div>

                                                <div class="col col-sm-2">
                                                  <asp:LinkButton ID="btnResumenFinal" runat="server"
                                                        CssClass="btn btn-secondary">
                                                        <i class="fa fa-chart-line"></i>
                                                        <span class="text">Resumen Final</span>
                                                    </asp:LinkButton>
                                                </div>
                                                
                                                
                                                <div class="col col-sm-2">                                                   
                                                    <asp:LinkButton ID="btnImportar" runat="server"
                                                        CssClass="btn btn-warning">
                                                        <i class="fa fa-upload"></i>
                                                        <span class="text">Importar</span>
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
                                        ShowHeader="true" DataKeyNames="cod_LibroActFijo" CssClass="display table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="cod_cuent_contable" HeaderText="CÓD. CUENT. CONT." />
                                            <asp:BoundField DataField="cuenta_contable" HeaderText="CUENT. CONTABLE" /> 
                                            <asp:BoundField DataField="fecha_adquis" HeaderText="FECH. ADQUISIÓN" />
                                            <asp:BoundField DataField="num_comprobPago" HeaderText="N° COMPR. PAGO" />
                                            <asp:BoundField DataField="ruc_proveedor" HeaderText="RUC PrOVEEDOR" />
                                            <asp:BoundField DataField="desc_proveedor" HeaderText="PROVEEDOR" />
                                            <asp:BoundField DataField="modelo" HeaderText="MODELO" />
                                            <asp:BoundField DataField="marca" HeaderText="MARCA" />
                                            <asp:BoundField DataField="serie" HeaderText="SERIE" />
                                            <asp:BoundField DataField="cantidad" HeaderText="CANTIDAD" />
                                            <asp:BoundField DataField="costoUnitario" HeaderText="COSTO UNITARIO" />
                                            <asp:BoundField DataField="costoAdquisicion" HeaderText="COSTO ADQUISI." />
                                            <asp:BoundField DataField="porcDepreciacion" HeaderText="PORC. DEPRECI." />
                                            <asp:BoundField DataField="valorNeto" HeaderText="VALOR NETO" />
                                 
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="OPE.">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="Editar" CssClass="btn btn-success"
                                                        ToolTip="Editar Datos de Libro de Activo Fijo">
                                                        <span><i class="fa fa-edit"></i></span>
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
                    <!--/Tab de Listado (Pestaña 01)-->  
                    
                    <!--Tab de Registro de Activo Fijo (Pestaña 02)-->
                    <div class="tab-pane" id="registrar" role="tabpanel" aria-labelledby="registrar-tab">                      
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                   
                                        <div class="card-header">Cuenta Contable </div>
                                        <div class="card-body">
                                            <div class="row">                                               
                                                <label for="txtCodCuentaContable"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Código Cuenta Contable:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox1" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCuentaContable"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Cuenta Contable:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox2" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                          </div>

                                       <div class="card-header">Adquisición </div>
                                           <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtFechaAdquision"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Fecha de Adquisión:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox3" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtNumComprobPago"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Número Comprobante Pago:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox4" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                           </div>
                                     
                                    <div class="card-header">Proveedor </div>
                                          <div class="card-body">
                                            <div class="row">                                               
                                                <label for="txtRUCProveedor"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">RUC de Proveedor:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox5" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtDescProveedor"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Descripción Proveedor:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox6" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card-header">Características del Bien </div>
                                           <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtModelo"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Modelo:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox7" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtMarca"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Marca:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox8" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>

                                             <div class="row">                                               
                                                <label for="txtSerie"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Serie:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox9" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCantidad"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Cantidad:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox10" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                          </div>

                                         <div class="card-header">Costos </div>
                                         <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtCostoUnitario"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Costo Unitario:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox11" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCostoAdquisicion"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Costo Adquisición:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="TextBox12" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                         </div>

                                          <div class="card-header">Depreciación </div>
                                            <div class="card-body">
                                               <div class="row">                                               
                                                    <label for="txtPorcDepreciacion"
                                                        class="col-12 col-sm-2 col-form-label form-control-sm">Porcentaje Depreciación:</label>
                                                    <div class="col-6 col-sm-2">
                                                        <asp:TextBox ID="TextBox13" runat ="server" ></asp:TextBox>
                                                    </div>
                                               </div>
                                            </div>

                                            <div class="card-header">Valor Neto </div>
                                            <div class="card-body">
                                                 <div class="row">   
                                                    <label for="txtValorNeto"
                                                        class="col-12 col-sm-2 col-form-label form-control-sm">Valor Neto:</label>
                                                    <div class="col-6 col-sm-2">
                                                        <asp:TextBox ID="TextBox14" runat ="server" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row justify-content-center" >
                                                <div class="col-sm-2"> 
                                                  <asp:LinkButton ID="btnRegistrarDatosContables" runat="server"
                                                        CssClass="btn btn-accion btn-success">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Registrar</span>
                                                  </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2"> 
                                                  <asp:LinkButton ID="btnSalirRegDatosContables" runat="server"
                                                        CssClass="btn btn-accion btn-danger">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                  </asp:LinkButton>
                                                </div>
                                            </div>
                                        
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Panel de Filtro de Búsqueda-->
                    </div>
                    <!--/Tab de Registro de Activo Fijo (Pestaña 02)-->  
                    
                    <!--Tab de Editar Activo Fijo (Pestaña 03)-->
                    <div class="tab-pane" id="editar" role="tabpanel" aria-labelledby="editar-tab">                      
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpEditarActivoFijo" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                   
                                        <div class="card-header">Cuenta Contable </div>
                                        <div class="card-body">
                                            <div class="row">                                               
                                                <label for="txtCodCuentaContable"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Código Cuenta Contable:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtCodCuentaContable" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCuentaContable"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Cuenta Contable:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtCuentaContable" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                          </div>

                                       <div class="card-header">Adquisición </div>
                                           <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtFechaAdquision"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Fecha de Adquisión:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtFechaAdquision" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtNumComprobPago"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Número Comprobante Pago:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtNumComprobPago" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                           </div>
                                     
                                    <div class="card-header">Proveedor </div>
                                          <div class="card-body">
                                            <div class="row">                                               
                                                <label for="txtRUCProveedor"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">RUC de Proveedor:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtRUCProveedor" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtDescProveedor"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Descripción Proveedor:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtDescProveedor" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card-header">Características del Bien </div>
                                           <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtModelo"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Modelo:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtModelo" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtMarca"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Marca:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtMarca" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>

                                             <div class="row">                                               
                                                <label for="txtSerie"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Serie:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtSerie" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCantidad"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Cantidad:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtCantidad" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                          </div>

                                         <div class="card-header">Costos </div>
                                         <div class="card-body">
                                             <div class="row">                                               
                                                <label for="txtCostoUnitario"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Costo Unitario:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtCostoUnitario" runat ="server" ></asp:TextBox>
                                                </div>
                                                <label for="txtCostoAdquisicion"
                                                    class="col-12 col-sm-2 col-form-label form-control-sm">Costo Adquisición:</label>
                                                <div class="col-6 col-sm-2">
                                                    <asp:TextBox ID="txtCostoAdquisicion" runat ="server" ></asp:TextBox>
                                                </div>
                                            </div>
                                         </div>

                                          <div class="card-header">Depreciación </div>
                                            <div class="card-body">
                                               <div class="row">                                               
                                                    <label for="txtPorcDepreciacion"
                                                        class="col-12 col-sm-2 col-form-label form-control-sm">Porcentaje Depreciación:</label>
                                                    <div class="col-6 col-sm-2">
                                                        <asp:TextBox ID="txtPorcDepreciacion" runat ="server" ></asp:TextBox>
                                                    </div>
                                               </div>
                                            </div>

                                            <div class="card-header">Valor Neto </div>
                                            <div class="card-body">
                                                 <div class="row">   
                                                    <label for="txtValorNeto"
                                                        class="col-12 col-sm-2 col-form-label form-control-sm">Valor Neto:</label>
                                                    <div class="col-6 col-sm-2">
                                                        <asp:TextBox ID="txtValorNeto" runat ="server" ></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row justify-content-center" >
                                                <div class="col-sm-2"> 
                                                  <asp:LinkButton ID="btnDetalleDatosContables" runat="server"
                                                        CssClass="btn btn-accion btn-success">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Registrar</span>
                                                  </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-2"> 
                                                  <asp:LinkButton ID="btnSalirDetDatosContables" runat="server"
                                                        CssClass="btn btn-accion btn-danger">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                  </asp:LinkButton>
                                                </div>
                                            </div>
                                        
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Panel de Filtro de Búsqueda-->
                    </div>
                    <!--/Tab de Editar Activo Fijo (Pestaña 03)-->
                    
                    <!--Tab de Importar Excel (Pestaña 04)-->
                    <div class="tab-pane" id="importar" role="tabpanel" aria-labelledby="importar-tab">                      
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpImportar" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Importar Excel</div>
                                        <div class="card-body">
                                            <div class="row">                                               
                                                <label for="fuImportExcel"
                                                    class="col-sm-1 col-form-label form-control-sm">Subir Archivo:</label>
                                                <div class="col-sm-3">
                                                   <asp:FileUpload runat="server" ID ="fuImportExcel"/>
                                                </div>   
                                                <div class="col-sm-1"></div>
                                                 <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnSalirImportar" runat="server"
                                                        CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1"> 
                                                  <asp:LinkButton ID="btnImportarExcel" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Registar</span>
                                                  </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Panel de Filtro de Búsqueda-->
                    </div>
                    <!--/Tab de Importar Excel (Pestaña 04)-->  
                    
                    <!--Tab de Resumen Final (Pestaña 05)-->
                    <div class="tab-pane" id="resumen" role="tabpanel" aria-labelledby="resumen-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltroResFinal" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Filtro</div>
                                        <div class="card-body">
                                            <div class="row">

                                                <label for="cbListColumnas"
                                                    class="col-sm-2 col-form-label form-control-sm">Columnas:</label>
                                                <div class="col-sm-2">
                                                     <asp:CheckBoxList ID="cbListColumnas" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">                                                      
                                                        <asp:ListItem Value="chkCostoAdquis">COSTO ADQUISICIÓN</asp:ListItem>
                                                        <asp:ListItem Value="chkDeprecAcum2019">DEPREC. ACUMULADA 2019</asp:ListItem>    
                                                        <asp:ListItem Value="chkDeprecEjer2020">DEPREC. EJERC. 2020</asp:ListItem>   
                                                        <asp:ListItem Value="chkValorNeto">VALOR NETO</asp:ListItem>    
                                                    </asp:CheckBoxList>
                                                </div>

                                                 
                                            </div>
                                            <div class="row">
                                                <div class="col col-sm-1">
                                                  <asp:LinkButton ID="btnCalcularResFinal" runat="server"
                                                        CssClass="btn btn-primary">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Calcular</span>
                                                    </asp:LinkButton>
                                                </div>
                                             
                                                <div class="col col-sm-1">                                                   
                                                    <asp:LinkButton ID="btnSalirResFinal" runat="server"
                                                        CssClass="btn btn-danger">
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
                        <!--/Panel de Filtro de Búsqueda-->

                        <!--Contenedor de GridView (Calculos Resumen Final)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpResumenFinal" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card-header">SUMAS</div>
                                    <div class="card-body">
                                        <asp:GridView ID="grwResumenFinal" runat="server" Width="100%" AutoGenerateColumns="false"
                                            ShowHeader="true" DataKeyNames="cod_ResFinal" CssClass="display table table-sm"
                                            GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="num_CuenContable" HeaderText="CUENTA CONTABLE" />                                               
                                                <asp:BoundField DataField="sum_CostoAdquis" HeaderText="COSTO ADQUISICIÓN" />
                                                <asp:BoundField DataField="dep_acumulada_2019" HeaderText="DEPREC. ACUMULADA 2019" /> 
                                                <asp:BoundField DataField="dep_ejerc_2020" HeaderText="DEPREC. EJERC. 2020" />
                                                <asp:BoundField DataField="sum_valor_neto" HeaderText="VALOR NETO" />            
                                          
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontraron Datos!
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle"
                                                HorizontalAlign="Center" Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                  </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Contenedor de GridView (Calculos Resumen Final)-->

                        
                        <!--Contenedor de GridView (Calculos Resumen Final Totales)-->
                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpResFinalTotalGeneral" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card-header">TOTAL GENERAL</div>
                                    <div class="card-body">
                                        <asp:GridView ID="grwResFinalTotales" runat="server" Width="100%" AutoGenerateColumns="false"
                                            ShowHeader="true" DataKeyNames="cod_ResFinalTotalGeneral" CssClass="display table table-sm"
                                            GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="sum_CostoAdquisTot" HeaderText="COSTO ADQUISICIÓN" />
                                                <asp:BoundField DataField="dep_acumulada_2019Tot" HeaderText="DEPREC. ACUMULADA 2019" /> 
                                                <asp:BoundField DataField="dep_ejerc_2020Tot" HeaderText="DEPREC. EJERC. 2020" />
                                                <asp:BoundField DataField="sum_valor_netoTot" HeaderText="VALOR NETO" />            
                                          
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se encontraron Datos!
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle"
                                                HorizontalAlign="Center" Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                  </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Contenedor de GridView (Calculos Resumen Final Totales)-->
                    </div>
                    <!--/Tab de Resumen Final (Pestaña 05)-->  
                    
                    
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

