<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInvestActivoFijoFaltante.aspx.vb" Inherits="administrativo_activofijo_L_Interfaces_frmInvestActivoFijoFaltante" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
 
        <head runat="server">
        <title>Investigación Activo Faltante</title>

        <meta charset="UTF-8"/>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
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

         <style>
             background-modal 
             {
               background-color:Gray;
               filter:alpha(opacity=50);
               opacity:0.7;
             }

         </style>

        <script type="text/javascript">
            function udpFiltrosUpdate() {
                /*Combos*/
                $(".combo_filtro").selectpicker({
                    size: 6,
                });
            } 

            function udpListaSinRespUpdate() {
                formatoGrillaSinResp();
            }

             function udpListaConRespUpdate() {
                formatoGrillaConResp();
            }

            /* Dar formato a la grilla (Sin Responsable) */
            function formatoGrillaSinResp() {
                // Setup - add a text input to each footer cell
                $('#grwSinResp thead tr').clone(true).appendTo('#grwSinResp thead');
                $('#grwSinResp thead tr:eq(1) th').each(function (i) {
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


                var table = $('#grwSinResp').DataTable({
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

             /* Dar formato a la grilla (Con Responsable) */
            function formatoGrillaConResp() {
                // Setup - add a text input to each footer cell
                $('#grwConResp thead tr').clone(true).appendTo('#grwConResp thead');
                $('#grwConResp thead tr:eq(1) th').each(function (i) {
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


                var table = $('#grwConResp').DataTable({
                    orderCellsTop: true,
                });


            }


            /* Flujo de tabs de la página principal. */
            function flujoTabs(tabActivo) {
                if (tabActivo == 'sinresp-tab') {
                    //HABILITAR
                    estadoTabSinResp('H');

                    //DESHABILITAR
                    estadoTabConResp('D');

                } else if (tabActivo == 'conresp-tab') {
                    //HABILITAR
                    estadoTabConResp('H');

                    //DESHABILITAR
                    estadoTabSinResp('D');
                } else if (tabActivo == 'conresp->detallConResp') { 
                    //HABILITAR
                    estadoDetalleConResp('H');

                    //DESHABILITAR
                    estadoTabConResp('D');
                }  else if (tabActivo == 'detalle->conresponsable') { 
                    //HABILITAR
                    estadoTabConResp('H');

                    //DESHABILITAR
                    estadoDetalleConResp('D');
                } else if (tabActivo == 'conresponsable->sinresponsable') { 
                    //HABILITAR
                    estadoTabSinResp('H');

                    //DESHABILITAR
                    estadoTabConResp('D');
                } else if (tabActivo == 'SinRespo->AdjFichBaja') { 
                    //HABILITAR
                    estadoTabAdjFichBaja('H');

                    //DESHABILITAR
                    estadoTabSinResp('D');
                }

            }

            function estadoTabSinResp(estado) {
                if (estado == 'H') {
                    $("#sinresp-tab").removeClass("disabled");
                    $("#sinresp-tab").addClass("active");
                    $("#sinresp").addClass("show");
                    $("#sinresp").addClass("active");
                } else {
                    $("#sinresp-tab").removeClass("active");
                    $("#sinresp-tab").addClass("disabled");
                    $("#sinresp").removeClass("show");
                    $("#sinresp").removeClass("active");
                }
            }

            function estadoTabConResp(estado) {
                if (estado == 'H') {
                    $("#conresp-tab").removeClass("disabled");
                    $("#conresp-tab").addClass("active");
                    $("#conresp").addClass("show");
                    $("#conresp").addClass("active");
                } else {
                    $("#conresp-tab").removeClass("active");
                    $("#conresp-tab").addClass("disabled");
                    $("#conresp").removeClass("show");
                    $("#conresp").removeClass("active");
                }
            }

             function estadoDetalleConResp(estado) {
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

            function estadoTabAdjFichBaja(estado) {
                 if (estado == 'H') {
                    $("#adjFichBaja-tab").removeClass("disabled");
                    $("#adjFichBaja-tab").addClass("active");
                    $("#adjFichBaja").addClass("show");
                    $("#adjFichBaja").addClass("active");
                } else {
                    $("#adjFichBaja-tab").removeClass("active");
                    $("#adjFichBaja-tab").addClass("disabled");
                    $("#adjFichBaja").removeClass("show");
                    $("#adjFichBaja").removeClass("active");
                }

            }


        </script>

    </head>
    <body>
    <div class="loader"></div>
        <form id="frmActivoFijoFaltante" runat="server">
            <asp:ScriptManager ID="scmActivoFijoFaltante" runat="server"></asp:ScriptManager>

            <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            </asp:UpdatePanel>

            <div class="container-fluid">
                <!--Cabecera de Panel-->
                <div class="card div-title">
                    <div class="row title">INVESTIGACIÓN DE ACTIVO FIJO FALTANTE</div>
                </div>
                <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                     <li class="nav-item">
                        <a href="#sinresp" id="sinresp-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="sinresp" aria-selected="true">Sin responsable</a>

                    </li>
                    <li class="nav-item">
                        <a href="#conresp" id="conresp-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="conresp" aria-selected="false">Con Responsable</a>
                    </li> 
                    
                    <li class="nav-item"> 
                        <a href="#detalle" id="detalle-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="detalle" aria-selected="false">Detalle</a>
                    </li> 
                     <li class="nav-item"> 
                        <a href="#adjFichBaja" id="adjFichBaja-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="adjFichBaja" aria-selected="false">Ficha Baja</a>
                    </li> 
             
                </ul>
                <div class="tab-content" id="contentTabs">
                    <!--Tab de Sin Responsable (Pestaña 01)-->
                    <div class="tab-pane show active" id="sinresp" role="tabpanel" aria-labelledby="sinresp-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltroSinResp" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Investigación Sin Responsable</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbTipoFiltroSinResp"
                                                    class="col-sm-1 col-form-label form-control-sm">Filtrar:</label>
                                                <div class="col-sm-3">
                                                   <asp:DropDownList ID="cmbTipoFiltroSinResp" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="SINRESP">SIN RESPONSABLE</asp:ListItem>                                                       
                                                        <asp:ListItem Value="CONRESP">CON RESPONSABLE</asp:ListItem>                                                       
                                                                                                              
                                                    </asp:DropDownList>
                                                </div>                                               
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                  <asp:LinkButton ID="btnFiltrarSinResp" runat="server"
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
                            <asp:UpdatePanel ID="udpSinResp" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwSinResp" runat="server" Width="100%" AutoGenerateColumns="false"
                                        ShowHeader="true" DataKeyNames="cod_detInvest" CssClass="display table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="desc_actFijo" HeaderText="ARTÍCULO" />
                                            <asp:BoundField DataField="resp_bien" HeaderText="RESP. DEL BIEN" />
                                            <asp:BoundField DataField="comis_Inves" HeaderText="COMIS. INVEST." />
                                            <asp:BoundField DataField="fech_progInves" HeaderText="FECH. DE INVEST." />                                           
                                                                                                                    
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="FORM. BAJA">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVerInforme" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="VerInforme" CssClass="btn btn-warning btn-sm"
                                                        ToolTip="Ver Informe">
                                                        <span><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>                                                   
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="INFORME">
                                                <ItemTemplate>
                                                     <asp:LinkButton ID="btnVerFormBaja" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="VerFormBaja" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Formulario Baja">
                                                        <span><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="ADJ. FORM. BAJA">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdjFormBaja" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="AdjFormBaja" CssClass="btn btn-success btn-sm"
                                                        ToolTip="Ver Informe">
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
                    <!--/Tab de Sin Responsable-->   

                     <!--Tab de Con Responsable (Pestaña 02)-->
                    <div class="tab-pane" id="conresp" role="tabpanel" aria-labelledby="conresp-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltroConResp" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Investigación Con Responsable</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="cmbTipoFiltroConResp"
                                                    class="col-sm-1 col-form-label form-control-sm">Filtrar:</label>
                                                <div class="col-sm-3">
                                                   <asp:DropDownList ID="cmbTipoFiltroConResp" runat="server"
                                                        AutoPostBack="true"
                                                        CssClass="form-control form-control-sm combo_filtro"
                                                        data-live-search="true" AutoComplete="off">
                                                        <asp:ListItem Value="CONRESP">CON RESPONSABLE</asp:ListItem>                                                       
                                                        <asp:ListItem Value="SINRESP">SIN RESPONSABLE</asp:ListItem>                                                       
                                                                                                            
                                                    </asp:DropDownList>
                                                </div>                                               
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                  <asp:LinkButton ID="btnFiltrarConResp" runat="server"
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

                        <!--Contenedor de GridView (Pestaña 02)-->                      

                        <div class="table-responsive">
                            <asp:UpdatePanel ID="udpConResp" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwConResp" runat="server" Width="100%" AutoGenerateColumns="false"
                                        ShowHeader="true" DataKeyNames="cod_detInvest1" CssClass="display table table-sm"
                                        GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="desc_actFijo1" HeaderText="ARTÍCULO" />
                                            <asp:BoundField DataField="resp_bien1" HeaderText="RESP. DEL BIEN" />
                                            <asp:BoundField DataField="comis_Inves1" HeaderText="COMIS. DE INVESTIG." />
                                            <asp:BoundField DataField="fech_progInves1" HeaderText="FECH. DE INVEST." />                                          
                                           
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="OPE.">
                                                <ItemTemplate> 
                                                      <asp:LinkButton ID="btnAccionesConResp" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="detalleConResp" CssClass="btn btn-success btn-sm"
                                                        ToolTip="Registrar datos de Responsable">
                                                        <span><i class="fa fa-file-archive"></i></span>
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
                    <!--/Tab de Con Responsable (Pestaña 02)-->   
                    
                    <!--Tab de Detalle (Pestaña 03)-->
                    <div class="tab-pane" id="detalle" role="tabpanel" aria-labelledby="detalle-tab">
                        <!--Panel de Filtro de Búsqueda-->
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpDetalleConResp" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Datos de Responsable</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtResponsable"
                                                    class="col-sm-2 col-form-label form-control-sm">Responsable:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtResponsable" runat ="server" ></asp:TextBox>
                                                </div>  
                                                <label for="txtAreaResponsable"
                                                    class="col-sm-2 col-form-label form-control-sm">Área Responsable:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtAreaResponsable" runat ="server" ></asp:TextBox>
                                                </div>  
                                                                                                                                     
                                           </div>
                                            <div class="row">
                                                <label for="txtAcciones"
                                                    class="col-sm-2 col-form-label form-control-sm">Acciones a tomar:</label>
                                                <div class="col-sm-4">
                                                    <textarea id="txtArea" name="Acciones" rows="4" cols="50" runat="server"></textarea>
                                                 </div> 
                                             </div>
                                           <div class="row">                                          
                                                <div class="col-sm-2">
                                                  <asp:LinkButton ID="btnSalirDetalle" runat="server"
                                                        CssClass="btn btn-accion btn-danger">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--/Panel de Filtro de Búsqueda-->

                    </div>
                    <!--/Tab de Detalle (Pestaña 03)-->   

                    <!--Tab de Adjuntar Ficha Baja (Pestaña 04)-->
                    <div class="tab-pane" id="adjFichBaja" role="tabpanel" aria-labelledby="adjFichBaja-tab">                                       
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpAdjFichBaja" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Adjuntar Ficha de Baja</div>
                                        <div class="card-body">
                                            <div class="row">                                               
                                                <label for="fuAdjFichBaja"
                                                    class="col-sm-1 col-form-label form-control-sm">Subir Archivo:</label>
                                                <div class="col-sm-3">
                                                   <asp:FileUpload runat="server" ID ="fuAdjuntarEvidencia"/>
                                                </div>   
                                                <div class="col-sm-1"></div>
                                                 <div class="col-sm-2">
                                                    <asp:LinkButton ID="btnSalirAdjFichBaja" runat="server"
                                                        CssClass="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-1"> 
                                                  <asp:LinkButton ID="btnAdjFichBaja" runat="server"
                                                        CssClass="btn btn-accion btn-celeste">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Registrar Ficha Baja</span>
                                                  </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>                       
                    </div>
                    <!--/Tab de Adjuntar Ficha Baja (Pestaña 04)-->   
         
         
                    
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