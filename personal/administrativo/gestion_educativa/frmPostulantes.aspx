<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPostulantes.aspx.vb" Inherits="administrativo_pec_test_frmPostulantes" EnableViewStateMac="False" EnableEventValidation="False" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Inscritos / Ingresantes</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="css/style.css?30">
    <link rel="stylesheet" href="css/postulantes.css?10">
</head>

<body>
    <div id="loading-layer">
        <img src="img/loading.gif" class="loading-gif">
    </div>
    <form id="frmPostulantes" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="hidden" id="urlMod" name="urlMod" runat="server">
        <input type="hidden" id="urlId" name="urlId" runat="server">
        <input type="hidden" id="urlCtf" name="urlCtf" runat="server">
        <div class="container-fluid">
            <asp:UpdatePanel ID="udpCabecera" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <img src="img/loading.gif" id="loading-gif">
                    <div class="card">
                        <div class="card-body">
                            <h5 class="title">Inscripción a Escuela PreUniversitaria</h5>
                            <hr>
                            <div class="row">
                                <label for="centroCosto" class="col-sm-2 col-form-label form-control-sm">
                                    Centro de Costo:</label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="cmbCentroCosto" runat="server" AutoPostBack="true"
                                        CssClass="form-control form-control-sm" data-live-search="true">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#postulantes" id="postulantes-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="postulantes" aria-selected="true">Inscritos</a>
                </li>
                <li class="nav-item">
                    <a href="#ingresantes" id="ingresantes-tab" class="nav-link" data-toggle="tab" role="tab"
                        aria-controls="ingresantes" aria-selected="false">Ingresantes</a>
                </li>
                <li class="nav-item">
                    <a href="#datosEvento" id="datosEvento-tab" class="nav-link" data-toggle="tab" role="tab"
                        aria-controls="datosEvento" aria-selected="true">Evento</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="postulantes" role="tabpanel" aria-labelledby="postulantes-tab">
                    <img src="img/loading.gif" class="loading-gif">
                    <div class="card">
                        <div class="card-header">Lista de interesados</div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltrosPostulantes" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <asp:UpdatePanel ID="udpBotonesPostulante" runat="server"
                                                UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnListarPostulante" runat="server"
                                                        class="btn btn-accion btn-celeste">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Listar</span>
                                                    </button>
                                                    <button type="button" id="btnRegistrarPostulante" runat="server"
                                                        class="btn btn-accion btn-azul">
                                                        <i class="fa fa-plus-square"></i>
                                                        <span class="text">Inscripción</span>
                                                    </button>
                                                    <button type="button" id="btnRegistrarPostulanteCompleto"
                                                        runat="server" class="btn btn-accion btn-verde">
                                                        <i class="fa fa-plus-square"></i>
                                                        <span class="text">Inscr. Completa</span>
                                                    </button>
                                                    <button type="button" id="btnExportarInteresados" runat="server"
                                                        class="btn btn-accion btn-naranja">
                                                        <i class="fa fa-file-export"></i>
                                                        <span class="text">Exportar</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="row justify-content-end">
                                                <label for="txtFiltroDNI"
                                                    class="col-sm-1 col-form-label form-control-sm">DNI:</label>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtFiltroDNI" runat="server"
                                                        CssClass="form-control form-control-sm" placeholder="DNI" />
                                                </div>
                                                <div class="custom-control custom-checkbox col-sm-3">
                                                    <input type="checkbox" class="custom-control-input"
                                                        id="chkMostrarSinDeuda" runat="server">
                                                    <label class="custom-control-label" for="chkMostrarSinDeuda">Mostrar
                                                        sin deuda</label>
                                                </div>
                                                <label for="cmbEstadoAlumno"
                                                    class="col-sm-2 col-form-label form-control-sm">Estado:</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="cmbEstadoAlumno" runat="server"
                                                        AutoPostBack="true" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Value="T">TODOS</asp:ListItem>
                                                        <asp:ListItem Value="A" Selected="True">ACTIVOS</asp:ListItem>
                                                        <asp:ListItem Value="I">INACTIVOS</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <hr>
                                    <div class="row">
                                        <div class="col-sm-2">
                                            <input type="text" id="txtFiltroPostulantes" runat="server"
                                                class="form-control form-control-sm" placeholder="Filtrar en tabla" />
                                        </div>
                                        <label for="cmbFilasPorPaginaPostu"
                                            class="offset-sm-8 col-sm-1 col-form-label form-control-sm">Filas:</label>
                                        <div class="col-sm-1">
                                            <asp:DropDownList ID="cmbFilasPorPaginaPostu" runat="server"
                                                AutoPostBack="true" CssClass="form-control form-control-sm">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                                                <asp:ListItem Value="0">- ∞ -</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="udpPostulantes" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwPostulantes" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="codigo_pso, cli" CssClass="table table-sm" GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Nro" />
                                    <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." />
                                    <asp:BoundField DataField="Nrodoc" HeaderText="Nro Doc." />
                                    <asp:BoundField DataField="Participante" HeaderText="Participante" />
                                    <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad" />
                                    <asp:BoundField DataField="cicloIng_Alu" HeaderText="Ciclo Ingreso" />
                                    <asp:BoundField DataField="estadoAdmision" HeaderText="Condición" />
                                    <asp:BoundField DataField="CargoTotal" HeaderText="Cargo Total" />
                                    <asp:BoundField DataField="AbonoTotal" HeaderText="Abono Total" />
                                    <asp:BoundField DataField="SaldoTotal" HeaderText="Saldo Total" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Mov." />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Act." />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Ficha" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Requ." />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Conv." />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Cargo" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Anul." />
                                </Columns>
                                <HeaderStyle CssClass="thead-dark" />
                            </asp:GridView>
                            <asp:UpdatePanel ID="udpPgrPostulantes" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div id="rowPagination" runat="server" class="row">
                                        <div class="col-sm-3">
                                            <h6><span id="lblPaginacion" runat="server"
                                                    class="badge badge-light"></span></h6>
                                        </div>
                                        <div class="col-sm-9">
                                            <nav>
                                                <ul id="pgrPostulantes" runat="server"
                                                    class="pagination pagination-sm justify-content-end"></ul>
                                            </nav>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div id="errorMensaje" runat="server"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="ingresantes" role="tabpanel" aria-labelledby="ingresantes-tab">
                    <img src="img/loading.gif" class="loading-gif">
                    <div class="card">
                        <div class="card-header">Lista de ingresantes</div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltrosIngresantes" runat="server" UpdateMode="Conditional"
                                ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="form-group row">
                                        <label for="cmbCicloAcademico"
                                            class="col-form-label form-control-sm col-sm-1">Semestre:</label>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="true"
                                                CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true" />
                                        </div>
                                        <label for="cmbModalidadIngreso"
                                            class="col-form-label form-control-sm col-sm-1">Modalidad:</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="cmbModalidadIngreso" runat="server"
                                                AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true" />
                                        </div>
                                        <label for="cmbCarreraProfesional"
                                            class="col-form-label form-control-sm col-sm-1">Carrera:</label>
                                        <div class="col-sm-4">
                                            <asp:DropDownList ID="cmbCarreraProfesional" runat="server"
                                                AutoPostBack="true" CssClass="form-control form-control-sm selectpicker"
                                                data-live-search="true" />
                                        </div>
                                    </div>
                                    <hr>
                                    <div class="form-group row">
                                        <div class="col-sm-6">
                                            <asp:UpdatePanel ID="udpBotonesIngresante" runat="server"
                                                UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <button type="button" id="btnListarIngresante" runat="server"
                                                        class="btn btn-accion btn-celeste">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Listar</span>
                                                    </button>
                                                    <button type="button" id="btnExportarIngresantes" runat="server"
                                                        class="btn btn-accion btn-verde">
                                                        <i class="fa fa-file-export"></i>
                                                        <span class="text">Exportar</span>
                                                    </button>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="offset-sm-3 col-sm-3">
                                            <asp:TextBox ID="txtFiltroIngresantes" runat="server"
                                                CssClass="form-control form-control-sm"
                                                placeholder="Filtrar en tabla" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <asp:UpdatePanel ID="udpIngresantes" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwIngresantes" runat="server" AutoGenerateColumns="false"
                                DataKeyNames="codigo_pso, codigo_Alu, codUniversitario" CssClass="table table-sm"
                                GridLines="None">
                                <Columns>
                                    <asp:BoundField HeaderText="Nro" />
                                    <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc." />
                                    <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc." />
                                    <asp:BoundField HeaderText="Participante" DataField="participante" />
                                    <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." />
                                    <asp:BoundField DataField="carrera" HeaderText="Escuela" />
                                    <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad Ingreso" />
                                    <asp:BoundField DataField="CicloIngreso" HeaderText="Ciclo Ingreso" />
                                    <asp:BoundField DataField="notaIngreso_Dal" HeaderText="Nota" />
                                    <asp:BoundField DataField="notaIngresoReal_Dal" HeaderText="Nota Real" />
                                    <asp:BoundField DataField="estadoAdmision" HeaderText="Condición" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Edit." />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Enviar" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Ficha" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="operacion"
                                        HeaderText="Requ." />
                                </Columns>
                                <HeaderStyle CssClass="thead-dark" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="tab-pane" id="datosEvento" role="tabpanel" aria-labelledby="datosEvento-tab">
                    <img src="img/loading.gif" class="loading-gif">
                    <div class="card">
                        <div class="card-header">Datos del evento</div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-lg-8 col-md-12">
                                    <asp:UpdatePanel ID="udpDatosEvento" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <table class="table table-sm">
                                                <tr class="thead-dark">
                                                    <th colspan="4">Datos informativos</th>
                                                </tr>
                                                <tr>
                                                    <td>Nombre Corto:</td>
                                                    <td colspan="3" id="tdNombreCorto" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Nro. Resolución:</td>
                                                    <td colspan="3" id="tdNroResolucion" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Coordinador General:</td>
                                                    <td colspan="3" id="tdCoordinadorGeneral" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Coordinador Apoyo:</td>
                                                    <td colspan="3" id="tdCoordinadorApoyo" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Fecha inicio propuesta:</td>
                                                    <td id="tdFechaInicioPropuesta" runat="SERVER"></td>
                                                    <td>Fecha fin propuesta:</td>
                                                    <td id="tdFechaFinPropuesta" runat="SERVER"></td>
                                                </tr>
                                                <tr class="thead-dark">
                                                    <th colspan="4">Precios / Descuentos por participante</th>
                                                </tr>
                                                <tr>
                                                    <td>Meta de participantes:</td>
                                                    <td colspan="3" id="tdMetaParticipantes" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Precios:</td>
                                                    <td colspan="3" id="tdPrecios" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>% Descuentos:</td>
                                                    <td colspan="3" id="tdDescuentos" runat="SERVER"></td>
                                                </tr>
                                                <tr class="thead-dark">
                                                    <th colspan="4">Otros datos</th>
                                                </tr>
                                                <tr>
                                                    <td>Gestiona Notas:</td>
                                                    <td colspan="3" id="tdGestionaNotas" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Horarios:</td>
                                                    <td colspan="3" id="tdHorarios" runat="SERVER"></td>
                                                </tr>
                                                <tr>
                                                    <td>Observaciones:</td>
                                                    <td colspan="3" id="tdObservaciones" runat="SERVER"></td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-lg-4 col-md-12">
                                    <asp:UpdatePanel ID="udpOpcionesEvento" runat="server" UpdateMode="Conditional"
                                        ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header">Opciones</div>
                                                <div class="card-body">
                                                    <button type="button" id="btnAnularCargosEvento" runat="server"
                                                        class="btn btn-accion btn-rojo">
                                                        <i class="fa fa-minus-circle"></i>
                                                        <span class="text">Anular Cargos</span>
                                                    </button>
                                                    <button type="button" id="btnInactivarInscritosEvento"
                                                        runat="server" class="btn btn-accion btn-naranja">
                                                        <i class="fa fa-ban"></i>
                                                        <span class="text">Inactivar Inscritos</span>
                                                    </button>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMovimientosPostulante" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpMovimientosPostulante" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Movimientos</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <asp:UpdatePanel ID="udpSinMovimientos" runat="server" UpdateMode="Conditional"
                                    ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="alert alert-info" id="divSinMovimientos" runat="server">No se han
                                            encontrado movimientos</div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:GridView ID="grwMovimientosPostulante" runat="server" AutoGenerateColumns="false"
                                    CssClass="table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="Servicio" HeaderText="Servicio" ReadOnly="True" />
                                        <asp:BoundField DataField="codigo_Deu" HeaderText="Codigo Deuda"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="Documento" HeaderText="Documento" ReadOnly="True" />
                                        <asp:BoundField DataField="Fecha_Operacion" HeaderText="Fecha Operacion"
                                            ReadOnly="True" />
                                        <asp:BoundField DataField="Cargos" HeaderText="Cargos" ReadOnly="True" />
                                        <asp:BoundField DataField="Abonos" HeaderText="Abonos" ReadOnly="True" />
                                        <asp:BoundField DataField="Saldo" HeaderText="Saldo" ReadOnly="True" />
                                        <asp:BoundField DataField="Trans" HeaderText="Transf." ReadOnly="True" />
                                        <asp:BoundField DataField="observacion_Deu" HeaderText="Observacion"
                                            ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Genera Mora" />
                                        <asp:BoundField DataField="FechaVenc" HeaderText="Fecha Venc."
                                            ReadOnly="True" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlInscripcionPostulante" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpInscripcionPostulante" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Formulario de registro</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <iframe src="" id="ifrmInscripcionPostulante" runat="server" frameborder="0"
                                    width="100%" scrolling="no"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary submit"
                                    id="btnRegistrarPostulanteModal">Registrar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlMantenimientoPostulante" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpMantenimientoPostulante" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Actualizar Datos</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <iframe src="" id="ifrmMantenimientoPostulante" runat="server" frameborder="0"
                                    width="100%" scrolling="no"></iframe>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlGenerarCargo" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpGenerarCargo" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Generar Cargo</span>
                            </div>
                            <div class="modal-body">
                                <iframe src="" id="ifrmGenerarCargo" runat="server" frameborder="0" width="100%"
                                    scrolling="no"></iframe>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlAnularCargo" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpAnularCargo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Anular Cargo</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <iframe src="" id="ifrmAnularCargo" runat="server" frameborder="0" width="100%"
                                    scrolling="no"></iframe>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlRequisitosAdmision" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <asp:UpdatePanel ID="udpRequisitosAdmision" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <span class="modal-title">Requisitos de Admisión</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <iframe src="" id="ifrmRequisitosAdmision" runat="server" frameborder="0" width="100%"
                                    scrolling="no"></iframe>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-primary submit"
                                    id="btnGuardarRequisitosAdmision">Guardar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="mdlMensajes" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Mensaje</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div id="mensajePostBack" class="alert alert-light"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlMensajeServidor" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="udpMensajeServidorParametros" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div id="divMdlMenServParametros" runat="server" data-mostrar="false"></div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="modal-header">
                        <asp:UpdatePanel ID="udpMensajeServidorHeader" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <span id="spnMensajeServidorTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="udpMensajeServidorBody" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="mensajeServer" class="alert alert-warning" runat="server"></div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>
        <!-- Validación desde el servidor -->
        <div id="mdlValServ" class="modal fade" tabindex="-1" role="dialog">
            <asp:UpdatePanel ID="udpValServ" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
                    <div class="modal-dialog modal-md" role="document">
                        <div class="modal-content">
                            <div id="divValServParametros" runat="server" data-mostrar="false"></div>
                            <div class="modal-header">
                                <span id="spnValServTitulo" runat="server" class="modal-title">Respuesta del
                                    Servidor</span>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div id="divValServMensaje" class="alert alert-warning" runat="server"></div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" id="btnValServProcesar" runat="server"
                                    class="btn btn-accion btn-danger">
                                    <i class="fa fa-angle-double-right"></i>
                                    <span class="text">Continuar</span>
                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="mdlConfirmarOperacion" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span class="modal-title">Advertencia</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="alert alert-warning">¿Está seguro de realizar esta acción?</div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="btnAceptarOperacion">Aceptar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../../assets/fileDownload/jquery.fileDownload.js"></script>
    <!-- Scripts propios -->
    <script src="js/funciones.js"></script>
    <script src="js/postulantes.js?70"></script>
    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();

            controlId = elem.id
            switch (controlId) {
                case 'cmbCentroCosto':
                    CargandoPanelPostulantes();
                case 'cmbEstadoAlumno':
                    CargandoPanelPostulantes();
                    break;
                case 'btnListarPostulante':
                    AtenuarBoton(controlId, false);
                    CargandoPanelPostulantes();
                    break;
                case 'btnRegistrarPostulante':
                    AtenuarBoton(controlId, false);
                    AlternarLoadingGif('interno', false);
                    break;
                case 'btnRegistrarPostulanteCompleto':
                    AtenuarBoton(controlId, false);
                    AlternarLoadingGif('interno', false);
                    break;
                case 'btnListarIngresante':
                    AtenuarBoton(controlId, false);
                    CargandoPanelIngresantes();
                    break;
                case 'cmbFilasPorPaginaPostu':
                    CargandoPostulantesPorPagina(controlId);
                    break;
                case 'btnAnularCargosEvento':
                case 'btnInactivarInscritosEvento':
                    CargandoPanelValServ(controlId);
                    break;
                case 'btnValServProcesar':
                    AtenuarBoton(controlId, false);
                    break;
            }

            if (controlId.indexOf('btnInfoPostulante') > -1
                || controlId.indexOf('btnEditarPostulante') > -1
                || controlId.indexOf('btnEnviarCorreoAdmision') > -1
                || controlId.indexOf('btnRequisitosAdmision') > -1
                || controlId.indexOf('btnImprimirFichaIngresante') > -1
                || controlId.indexOf('btnAnularCargo') > -1) {
                AtenuarBoton(controlId, false);
                AlternarLoadingGif('interno', false);
            }

            if (controlId.indexOf('btnPagePostu') > -1) {
                CargandoPostulantesPorPagina(controlId);
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function (sender, args) {
            var error = args.get_error();
            if (error) {
                console.log(error);
                MostrarMensajeClient(-1, 'Ha ocurrido un error en el servidor.');
                AtenuarBoton(controlId, true);
                AlternarLoadingGif('global', true);
                AlternarLoadingGif('interno', true);
                AlternarDataGridView('grwPostulantes', true);
                args.set_errorHandled(true);
                return false;
            }

            if (controlId == 'udpGenerarCargo') {
                CargarModalGenerarCargo(controlId, 'btnGuardar');
            }
        });

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            var udpFormUpdated = false
            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanel = updatedPanels[i];
                switch (udpPanel.id) {
                }
            }
        });

        Sys.Application.add_load(function () {
            var elem = document.getElementById(controlId);
            generarCargo = false;

            switch (controlId) {
                case 'cmbCentroCosto':
                    PanelPostulantesCargado();
                    InicializarControles();
                    break;
                case 'chkMostrarSinDeuda':
                    PanelPostulantesCargado();
                case 'cmbEstadoAlumno':
                    PanelPostulantesCargado();
                    break;
                case 'btnListarPostulante':
                    AtenuarBoton(controlId, true);
                    PanelPostulantesCargado(controlId);
                    break;
                case 'btnRegistrarPostulante':
                    generarCargo = true;
                    FormularioRegistroInscripcionCargado();
                    break;
                case 'btnRegistrarPostulanteCompleto':
                    generarCargo = true;
                    CargarModalMantenimientoPostulante(controlId, true);
                    break;
                case 'btnListarIngresante':
                    AtenuarBoton(controlId, true);
                    PanelIngresantesCargado();
                    break;
                case 'cmbFilasPorPaginaPostu':
                    PostulantesCargadosPorPagina(controlId);
                    break;
                case 'btnAnularCargosEvento':
                case 'btnInactivarInscritosEvento':
                    PanelValServCargado();
                    break;
                case 'btnValServProcesar':
                    AtenuarBoton(controlId, true);
                    break;
            }

            if (controlId.indexOf('btnInfoPostulante') > -1) {
                CargarModalMovimientos(controlId);
            }

            if (controlId.indexOf('btnEditarPostulante') > -1) {
                CargarModalMantenimientoPostulante(controlId, false);
                FiltrarGrillaPostulantes();
            }

            if (controlId.indexOf('btnAnularCargo') > -1) {
                CargarModalAnularCargo(controlId);
                FiltrarGrillaPostulantes();
            }

            if (controlId.indexOf('btnEnviarCorreoAdmision') > -1) {
                AtenuarBoton(controlId, true);
                AlternarLoadingGif('interno', true);
                // MostrarMensajeServer();
            }

            if (controlId.indexOf('btnGenerarCargo') > -1) {
                CargoInscripcionProcesado();
            }

            if (controlId.indexOf('btnRequisitosAdmision') > -1) {
                CargarModalRequisitosAdmision(controlId, 'btnGuardar');
            }

            if (controlId.indexOf('btnImprimirFichaIngresante') > -1) {
                VerificarRespuestaFichaInscripcion();
                FichaInscripcionCargada(controlId);
            }

            if (controlId.indexOf('btnPagePostu') > -1) {
                PostulantesCargadosPorPagina(controlId);
            }

            VerificarMensajeServer();
            VerificarMdlValidacionServidor();
        });
    </script>
</body>

</html>