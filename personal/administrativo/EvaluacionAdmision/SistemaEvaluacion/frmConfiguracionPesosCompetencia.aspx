<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionPesosCompetencia.aspx.vb" Inherits="frmConfiguracionPesosCompetencia" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>.:: Configuración de Pesos por Competencia y Escuela ::.</title>
    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../../assets/bootstrap-4.1/css-custom/bootstrap.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css">
    <link rel="stylesheet" href="../../../assets/bootstrap-datepicker/css/bootstrap-datepicker.css">
    <link rel="stylesheet" href="../../../assets/toastr-2.1.4-7/toastr.min.css">
    <!-- Estilos propios -->
    <link rel="stylesheet" href="../css/style.css">
    <link rel="stylesheet" href="../css/plantilla.css">
</head>

<body>
    <form id="form" runat="server">
        <div class="container-fluid">
            <div class="messagealert" id="divMensaje" runat="server">
            </div>
            <div class="row">
                <div class="col-sm-48">
                    <h1 class="main-title">Configuración de Pesos por Competencia y Escuela<img src="img/loading.gif" id="loadingGif"></h1>
                    <hr>
                </div>
            </div>
            <nav>
                <div class="nav nav-tabs" id="nav-tab" role="tablist">
                    <a class="nav-item nav-link active" id="navlistatab" data-toggle="tab" href="#nav-lista" role="tab" aria-controls="nav-lista" 
                        aria-selected="true" runat="server">Lista</a>
                    <a class="nav-item nav-link" id="navmantenimientotab" data-toggle="tab" href="#nav-mantenimiento" role="tab" aria-controls="nav-mantenimiento"
                        aria-selected="false" runat="server">Mantenimiento</a>
                </div>
            </nav>
            <div class="tab-content" id="nav-tabContent">
                <div class="tab-pane fade show active" id="navlista" role="tabpanel" aria-labelledby="nav-lista-tab" runat="server">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Filtros</h5>
                        </div>
                        <div class="card-body">
                            <div class="row form-group">
                                <div class="col-sm-18 d-flex">
                                    <label for="cmbCicloAcademico" class="form-control-sm">Semestre Académico:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbCicloAcademico" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-sm-18 d-flex" id="divProgramaEstudio" runat="server">
                                    <label for="cmbCarreraProfesional" class="form-control-sm">Programa de Estudios:</label>
                                    <div class="fill">
                                        <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                            data-live-search="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-sm-18 text-right">
                                    <button type="button" id="btnListar" runat="server" class="btn btn-accion btn-light" onserverclick="btnListar_Click">
                                        <i class="fa fa-search"></i>
                                        <span class="text">Listar</span>
                                    </button>
                                    <button type="button" id="btnImportar" runat="server" class="btn btn-accion btn-success">
                                        <i class="fas fa-file-import"></i>
                                        <span class="text">Importar</span>
                                    </button>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-48">
                                    <hr class="separador">
                                    <asp:GridView ID="grvPesos" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_fac,codigo_cpf,nombre_cpf" CssClass="table table-sm" GridLines="None"
                                        OnRowCreated="grvPesos_OnRowCreated" OnRowDataBound="grvPesos_OnRowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_fac" HeaderText="FACULTAD" />
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="NOMBRE" />
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="navmantenimiento" role="tabpanel" aria-labelledby="nav-lista-tab" runat="server">
                    <div class="card">
                        <div class="card-header">
                            <h5 class="title">Registro / Edición</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-48">
                                    <h1 class="title">Configuración de pesos por competencia</h1>
                                    <hr>
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="txtCicloAcademico" class="form-control-sm col-md-8">Semestre Académico:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtCicloAcademico" runat="server" CssClass="form-control form-control-sm" placeholder="Campo 2" Enabled="false"
                                        Text="2020-II" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="txtCarreraProfesional" class="form-control-sm col-md-8">Programa de Estudios:</label>
                                <div class="col-md-15">
                                    <asp:TextBox ID="txtCarreraProfesional" runat="server" CssClass="form-control form-control-sm" placeholder="Campo 2" Enabled="false"
                                        Text="ADMINISTRACIÓN" />
                                </div>
                            </div>
                            <div class="row form-group">
                                <label for="chkFacultad" class="form-control-sm col-md-8">Aplicar:</label>
                                <div class="col-md-20">
                                    <div class="custom-control custom-checkbox custom-control-inline">
                                        <input type="checkbox" id="chkFacultad" name="chkFacultad" class="custom-control-input" runat="server" value="1">
                                        <label class="custom-control-label form-control-sm" for="chkFacultad">Todos los Programas de Estudios de la Facultad</label>
                                    </div>
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group">
                                <label for="grvManPesos" class="form-control-sm col-md-8">Configuración:</label>
                                <div class="col-sm-24">
                                    <asp:GridView ID="grvManPesos" runat="server" AutoGenerateColumns="false" 
                                        DataKeyNames="codigo_pcom, codigo_cpf, codigo_com, peso_pcom" CssClass="table table-sm" GridLines="None">
                                        <Columns>
                                            <asp:BoundField DataField="nombre_com" HeaderText="COMPETENCIA" />
                                            <asp:TemplateField HeaderText="PESO" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtPeso" runat="server" CssClass="form-control form-control-sm" placeholder="Peso" Text='<%# Eval("peso_pcom") %>'/>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="thead-dark" />
                                    </asp:GridView>
                                </div>
                            </div>
                            <hr class="separador">
                            <div class="row form-group">
                                <div class="col-md-20 offset-md-10">
                                    <button type="button" id="btnCancelar" runat="server" class="btn btn-accion btn-light" onserverclick="btnCancelar_Click">
                                        <i class="fa fa-ban"></i>
                                        <span class="text">Cancelar</span>
                                    </button>
                                    <button type="button" id="btnGuardar" runat="server" class="btn btn-accion btn-primary" onserverclick="btnGuardar_Click">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Guardar</span>
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="mdlImportar" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <span runat="server" class="modal-title">Importar Configuración</span>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                    </div>
                    <div class="modal-body">
                        <div class="row form-group">
                            <label for="cmbDesdeCicloAcademico" class="form-control-sm col-md-14">Semestre a Importar:</label>
                            <div class="col-md-33">
                                <asp:DropDownList ID="cmbDesdeCicloAcademico" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm selectpicker"
                                    data-live-search="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row form-group">
                            <label for="txtHaciaCicloAcademico" class="form-control-sm col-md-14">Semestre Actual:</label>
                            <div class="col-md-33">
                                <asp:TextBox ID="txtHaciaCicloAcademico" runat="server" CssClass="form-control form-control-sm" Enabled="false" Text="2020-II" />
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                        <%--<button type="button" class="btn btn-primary" data-dismiss="modal">Importar</button>--%>
                        <button type="button" id="btnAltImportar" runat="server" onserverclick="btnAltImportar_Click"
                            class="btn btn-primary" data-dismiss="modal">
                            Importar
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!-- Scripts externos -->
    <script src="../../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../../assets/bootstrap-4.1/js/bootstrap.bundle.min.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../../assets/bootstrap-select-1.13.1/js/i18n/defaults-es_ES.min.js"></script>
    <script src="../../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../../../assets/toastr-2.1.4-7/toastr.min.js"></script>
    <!-- Scripts propios -->
    <script src="../js/funciones.js"></script>
    <script src="../js/configuracionPesosCompetencia.js"></script>
</body>

</html>