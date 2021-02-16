<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InformacionContable.aspx.vb"
    Inherits="administrativo_activofijo_DatosContables_DatosContables" %>

    <!DOCTYPE html>
    <html xmlns="http://www.w3.org/1999/xhtml">

    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

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

        </script>


        <title>Información Contable de Activos Fijos</title>

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
                    <div class="row title">INFORMACIÓN CONTABLE DE ACTIVO FIJO</div>
                </div>
                <!--/Cabecera de Panel-->

                <!--Panel de Filtro de Búsqueda-->
                <div class="panel-cabecera">
                    <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="card">
                                <div class="card-header">Filtros de Búsqueda</div>
                                <div class="card-body">
                                    <div class="row">
                                        <label for="cbEstado"
                                            class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cbEstado" runat="server" AutoPostBack="true"
                                                CssClass="form-control form-control-sm combo_filtro"
                                                data-live-search="true" AutoComplete="off">
                                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                <asp:ListItem Value="PENDIENTECOMPLETAR">PENDIENTE DE COMPLETAR
                                                </asp:ListItem>
                                                <asp:ListItem Value="COMPLETO">COMPLETO</asp:ListItem>
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

                <!--Panel contenedor de GridView-->
                <div class="table-responsive">
                    <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwLista" runat="server" Width="100%" AutoGenerateColumns="false"
                                ShowHeader="true" DataKeyNames="codigo_af" CssClass="display table table-sm"
                                GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="desc_af" HeaderText="Activo Fijo" />
                                    <asp:BoundField DataField="resp_bien" HeaderText="Resp. del Bien" />
                                    <asp:BoundField DataField="ubicacion" HeaderText="Ubicación" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Ficha Control Pat.">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="BtnFichaControlPatrimonial" runat="server"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CommandName="Editar" CssClass="btn btn-primary btn-sm"
                                                ToolTip="Ver Ficha Control Patrimonial">
                                                <span><i class="fa fa-search"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Comprob. de Pago">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnVerComprobantePago" runat="server"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CommandName="Editar" CssClass="btn btn-primary btn-sm"
                                                ToolTip="Ver Comprobante de Pago">
                                                <span><i class="fa fa-search"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Datos Contables">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAgregarDatosContables" runat="server"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CommandName="Editar" CssClass="btn btn-primary btn-sm"
                                                ToolTip="Agregar Datos Contables">
                                                <span><i class="fa fa-plus"></i></span>
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
                <!--/Panel contenedor de GridView-->
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