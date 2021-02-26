<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmInvestActivoFijoFaltante.aspx.vb" Inherits="administrativo_activofijo_L_Interfaces_frmRegistraActivoFijoFaltante" %>

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
                if (tabActivo == 'conResp-tab') {
                    //HABILITAR
                    estadoTabConResp("H");

                    //DESHABILITAR
                    estadoTabSinResp("D");

                } else if (tabActivo == 'sinResp-tab') {
                    //HABILITAR
                    estadoTabSinResp("H");

                    //DESHABILITAR
                    estadoTabConResp("D");
                }
            }

            function estadoTabConResp(estado) {
                if (estado == "H") {
                    $("#conResp-tab").removeClass("disabled");
                    $("#conResp-tab").addClass("active");
                    $("#conResp").addClass("show");
                    $("#ConResp").addClass("active");
                } else {
                    $("#conResp-tab").removeClass("active");
                    $("#conResp-tab").addClass("disabled");
                    $("#conResp").removeClass("show");
                    $("#conResp").removeClass("active");
                }
            }

            function estadoTabSinResp(estado) {
                if (estado == "H" {
                    $("#sinResp-tab").removeClass("disabled");
                    $("#sinResp-tab").addClass("active");
                    $("#sinResp").addClass("show");
                    $("#sinResp").addClass("active");
                } else {
                    $("#sinResp-tab").removeClass("active");
                    $("#sinResp-tab").addClass("disabled");
                    $("#sinResp").removeClass("show");
                    $("#sinResp").removeClass("active");
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
                    <div class="row title">INVESTIGACIÓN DE ACTIVO FIJO FALTANTE</div>
                </div>
                <!--/Cabecera de Panel-->
                <!--Tabs-->
                <ul class="nav nav-tabs" role="tablist">
                    <li class="nav-item">
                        <a href="#sinResp" id="sinResp-tab" class="nav-link active" data-toggle="tab" role="tab"
                            aria-controls="sinResp" aria-selected="true">Sin Responsable</a>

                    </li>
                    <li class="nav-item">
                        <a href="#conResp" id="conResp-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="conResp" aria-selected="false">Con Responsable</a>

                    </li>
                     <li class="nav-item">
                        <a href="#Todos" id="Todos-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="Todos" aria-selected="false">Todos</a>

                    </li>
                    <li class="nav-item">
                        <a href="#formBaja" id="formBaja-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                            aria-controls="formBaja" aria-selected="false">Formulario Baja</a>

                    </li>
                </ul>
                <div class="tab-content" id="contentTabs">
                    <!--Tab de Sin Responsable (Pestaña 01)-->
                    <div class="tab-pane show active" id="sinResp" role="tabpanel" aria-labelledby="sinResp-tab">
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
                                                        <asp:ListItem Value="CONRESP">CON RESPONBSALE</asp:ListItem>
                                                        <asp:ListItem Value="TODAS">TODAS
                                                        </asp:ListItem>                                                       
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
                                            <asp:BoundField DataField="comis_Inves" HeaderText="COMIS. DE INVESTIG." />
                                            <asp:BoundField DataField="fech_progInves" HeaderText="FECH. DE INVEST." />                                           
                                            <asp:BoundField DataField="respons" HeaderText="RESPONSABLE" />
                                                                          
                                           
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="FORM. BAJA">
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
                                                HeaderText="INFORME">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVerInforme" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="VerInforme" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Informe">
                                                        <span><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="ADJ. FORM. BAJA">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdjFormBaja" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="AdjFormBaja" CssClass="btn btn-primary btn-sm"
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
                    <div class="tab-pane" id="conResp" role="tabpanel" aria-labelledby="conResp-tab">
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
                                                        <asp:ListItem Value="SINRESP">SIN RESPONBSALE</asp:ListItem>
                                                        <asp:ListItem Value="TODAS">TODAS
                                                        </asp:ListItem>                                                       
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
                                            <asp:BoundField DataField="respons1" HeaderText="RESPONSABLE" />                                                                          
                                           
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="RESPONSABLE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnResponsable1" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="responsable" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Responsable">
                                                        <span><i class="fa fa-paperclip"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="ÁREA A TOMAR ACC.">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAreaTomaAccion1" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="Registrar área a tomar acción" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Ver Informe">
                                                        <span><i class="fa fa-paperclip"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                                HeaderText="REG. ACC. TOMAR">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRegAccTomar1" runat="server"
                                                        CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CommandName="accionTomar" CssClass="btn btn-primary btn-sm"
                                                        ToolTip="Registrar Acción a Tomar">
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
                    <!--/Tab de Con Responsable (Pestaña 02)-->   
                    

                    
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
