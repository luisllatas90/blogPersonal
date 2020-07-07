<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProgramarCurso.aspx.vb" Inherits="academico_ProgramacionAcademica_frmProgramarCurso" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Programar Curso</title>

    <!-- Estilos externos -->         
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">   
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css">    

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../Alumni/css/estilos.css?1">

    <!-- Scripts externos -->
    <script src="../../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>        
    <script src="../../Alumni/js/popper.js"></script>    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="../../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>

    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script> 

    <script type="text/javascript">
        /* Scripts a ejecutar al actualizar los paneles.*/
        function udpFiltrosUpdate() {
            $(".combo_filtro").selectpicker({
                size: 6,
            });
        }

        function udpRegistroCursoUpdate(){
            /*Campos de Fecha*/
            var date_input_1=$('input[id="txtFechaInicio"]');
            var date_input_2=$('input[id="txtFechaFin"]');
            var date_input_3=$('input[id="txtFechaRetiro"]'); 

            var container=$('.bootstrap-iso form').length>0 ? $('.bootstrap-iso form').parent() : "body";
            var options={
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };

            date_input_1.datepicker(options);
            date_input_2.datepicker(options);
            date_input_3.datepicker(options);

            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });
        }

        /* Dar formato a la grilla. */
        function formatoGrilla(){
            // Setup - add a text input to each footer cell
            $('#grwLista thead tr').clone(true).appendTo( '#grwLista thead' );
            $('#grwLista thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwLista').DataTable( {
                orderCellsTop: true
            } );
        }

        function formatoGrillaCurso(){
            // Setup - add a text input to each footer cell
            $('#grwListaCurso thead tr').clone(true).appendTo( '#grwListaCurso thead' );
            $('#grwListaCurso thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwListaCurso').DataTable( {
                orderCellsTop: true
            } );
        }        

        function formatoGrillaCursoEquivalente(){
            // Setup - add a text input to each footer cell
            $('#grwListaCursoEquivalente thead tr').clone(true).appendTo( '#grwListaCursoEquivalente thead' );
            $('#grwListaCursoEquivalente thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( tableCursoEquivalente.column(i).search() !== this.value ) {
                        tableCursoEquivalente
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            tableCursoEquivalente = $('#grwListaCursoEquivalente').DataTable( {
                orderCellsTop: true
            } );            
        }

        function formatoGrillaProfesorSugerido(){
            // Setup - add a text input to each footer cell
            $('#grwListaProfesorSugerido thead tr').clone(true).appendTo( '#grwListaProfesorSugerido thead' );
            $('#grwListaProfesorSugerido thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( tableProfesorSugerido.column(i).search() !== this.value ) {
                        tableProfesorSugerido
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            tableProfesorSugerido = $('#grwListaProfesorSugerido').DataTable( {
                orderCellsTop: true
            } );            
        }

        function formatoGrillaBloque(){
            // Setup - add a text input to each footer cell
            $('#grwListaBloque thead tr').clone(true).appendTo( '#grwListaBloque thead' );
            $('#grwListaBloque thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( table.column(i).search() !== this.value ) {
                        table
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            var table = $('#grwListaBloque').DataTable( {
                orderCellsTop: true
            } );
        } 

        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } 
        
        function alertConfirm(ctl, event, titulo, icono) {
            // STORE HREF ATTRIBUTE OF LINK CTL (THIS) BUTTON
            var defaultAction = $(ctl).prop("href");                      
            // CANCEL DEFAULT LINK BEHAVIOUR
            event.preventDefault();            
            
            swal({
                title: titulo,                
                type: icono,
                showCancelButton: true ,
                confirmButtonText: "SI" ,
                confirmButtonColor: "#45c1e6" ,
                cancelButtonText: "NO"
            }).then(function (isConfirm) {
                if (isConfirm) {
                    if (ctl.id == "btnGuardarCurso"){                        
                        tableCursoEquivalente.page.len(-1).draw();
                        tableProfesorSugerido.page.len(-1).draw();
                    }

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
                estadoTabListadoCurso('H');

                //DESHABILITAR
                estadoTabRegistro('D');
                estadoTabRegistroCurso('D');

            }else if (tabActivo == 'registro-tab'){
                //HABILITAR
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
            }else if (tabActivo == 'listado_curso-tab') {
                //HABILITAR
                estadoTabListadoCurso('H');

                //DESHABILITAR
                estadoTabRegistroCurso('D');

            }else if (tabActivo == 'registro_curso-tab'){
                //HABILITAR
                estadoTabRegistroCurso('H');

                //DESHABILITAR
                estadoTabListadoCurso('D');
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

        function estadoTabListadoCurso(estado) {
            if (estado == 'H') {
                $("#listado_curso-tab").removeClass("disabled");
                $("#listado_curso-tab").addClass("active");
                $("#listado_curso").addClass("show");
                $("#listado_curso").addClass("active");
            } else {
                $("#listado_curso-tab").removeClass("active");
                $("#listado_curso-tab").addClass("disabled");
                $("#listado_curso").removeClass("show");
                $("#listado_curso").removeClass("active");
            }
        }

        function estadoTabRegistroCurso(estado) {
            if (estado == 'H') {
                $("#registro_curso-tab").removeClass("disabled");
                $("#registro_curso-tab").addClass("active");
                $("#registro_curso").addClass("show");
                $("#registro_curso").addClass("active");
            } else {
                $("#registro_curso-tab").removeClass("active");
                $("#registro_curso-tab").addClass("disabled");
                $("#registro_curso").removeClass("show");
                $("#registro_curso").removeClass("active");
            }
        }

        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) {
                case 'Lista':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista');               
                    break;

                case 'Registro':                      
                    $loadingGif = $('.loader');                  
                    break;   
                
                case 'ListaCurso':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpListaCurso');               
                    break;   

                case 'GuardarBloque':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpListaBloque');               
                    break;                      

            }

            if ($loadingGif != undefined) {
                if (!retorno) {
                    $loadingGif.fadeIn('slow');  
                    if ($elemento != undefined) {
                        $elemento.addClass("oculto");
                    }                     
                } else {
                    $loadingGif.fadeOut('slow');
                    if ($elemento != undefined) {
                        $elemento.removeClass("oculto");
                    }                    
                }
            } 
        }

        /* Abrir y cerrar el modales. */
        function openModal(elemento) {
            switch (elemento) {
                case 'RegistrarBloque':
                    $('#mdlRegistrarBloque').modal('show');                   
                    break;                 
            }
        }

        function closeModal(elemento) {
            switch (elemento) { 
                case 'RegistrarBloque':
                    $('#mdlRegistrarBloque').modal('hide');                   
                    break;                   
            }            
        }  

        /*Reestablecer el numero de filas*/        
        function reestablecerFilas(){
            tableCursoEquivalente.page.len(10).draw();
            tableProfesorSugerido.page.len(10).draw();
        }
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmProgramarCurso" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">PROGRAMACIÓN DE CURSOS</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
                </li>
                <li class="nav-item">
                    <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="registro" aria-selected="false">Programación</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Filtros de Búsqueda</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbProgramacionAcademica" class="col-sm-2 form-control-sm">Semestre Académico:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbProgramacionAcademica" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbCarreraProfesional" class="col-sm-2 form-control-sm">Carrera Profesional:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbCarreraProfesional" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="cmbPlanEstudios" class="col-sm-2 form-control-sm">Plan de Estudios:</label>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="cmbPlanEstudios" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off"/>                                    
                                            </div>
                                            <label for="cmbCiclo" class="col-sm-2 form-control-sm">Ciclo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbCiclo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off"/>                                    
                                            </div>
                                        </div>
                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
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
                    <div class="table-responsive">
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_cur, codigo_pes, codigo_Cpf, grupos"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="ciclo_cur" HeaderText="CICLO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="nombre_cur" HeaderText="CURSO" ItemStyle-Width="70%" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="grupos" HeaderText="GRUPOS" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnProgramar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Programar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Programar curso"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea realizar la programación del curso seleccionado?', 'warning');">
                                                    <span><i class="fa fa-calendar-alt"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br/>
                    </div>
                    <br/>
                </div>
                <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Datos de la Asignatura</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtCodigo" class="col-sm-2 col-form-label form-control-sm">Código:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtCodigo" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>  
                                            <label for="txtNombre" class="col-sm-1 col-form-label form-control-sm">Nombre:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>                                                                                                                           
                                            <asp:Label ID="lblElectivo" runat="server" CssClass="col-sm-2 col-form-label form-control-sm requerido" Text="Curso Electivo(*)" Visible="false"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label for="txtCreditos" class="col-sm-2 col-form-label form-control-sm">Créditos:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtCreditos" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <label for="txtDepAcademico" class="col-sm-1 col-form-label form-control-sm">Dpto Acad.:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtDepAcademico" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <label for="txtTipo" class="col-sm-2 col-form-label form-control-sm">Tipo:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtTipo" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>                                                 
                                            <label for="txtHorasTeoria" class="col-sm-1 col-form-label form-control-sm">H.Teoría:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtHorasTeoria" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <label for="txtHorasPractica" class="col-sm-1 col-form-label form-control-sm">H.Práctica:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtHorasPractica" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>
                                            <label for="txtHorasTotal" class="col-sm-1 col-form-label form-control-sm">H.Total:</label>
                                            <div class="col-sm-1">
                                                <asp:TextBox ID="txtHorasTotal" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                            </div>                                       
                                        </div>
                                    </div>
                                </div>                                                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>                                  
                    <div class="container-fluid">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a href="#listado_curso" id="listado_curso-tab" class="nav-link active" data-toggle="tab" role="tab"
                                    aria-controls="listado_curso" aria-selected="true">Grupos</a>
                            </li>
                            <li class="nav-item">
                                <a href="#registro_curso" id="registro_curso-tab" class="nav-link active" data-toggle="tab" role="tab"
                                    aria-controls="registro_curso" aria-selected="true">Registro</a>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane show active" id="listado_curso" role="tabpanel" aria-labelledby="listado_curso-tab">
                                <div class="panel-cabecera">
                                    <asp:UpdatePanel ID="udpFiltrosCurso" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header">Grupos Horario Programados</div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <asp:LinkButton ID="btnListarGrupo" runat="server" CssClass="btn btn-accion btn-celeste">
                                                                <i class="fa fa-sync-alt"></i>
                                                                <span class="text">Listar</span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="btnNuevoGrupo" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                                OnClientClick="return alertConfirm(this, event, '¿Desea agregar un nuevo grupo horario?', 'warning');">
                                                                <i class="fa fa-plus-square"></i>
                                                                <span class="text">Nuevo</span>
                                                            </asp:LinkButton> 
                                                            <asp:LinkButton ID="btnSalirGrupo" runat="server" CssClass="btn btn-accion btn-danger">
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
                                <br/>
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="udpListaCurso" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:GridView ID="grwListaCurso" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_cup"
                                                CssClass="display table table-sm" GridLines="None">
                                                <Columns>
                                                    <asp:BoundField DataField="grupohor_cup" HeaderText="GRUPO" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="vacantes_cup" HeaderText="VACANTES" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="fechainicio_cup" HeaderText="INICIO" DataFormatString="{0:d}" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="fechafin_cup" HeaderText="FIN" DataFormatString="{0:d}" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="abreviaturaA_cpf" HeaderText="AGRUPADO" ItemStyle-Width="40%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="total_mat" HeaderText="INSCRITOS" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                    <asp:BoundField DataField="estado_descripcion" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                    <asp:CheckBoxField DataField="SoloPrimerCiclo_cup" HeaderText="PRIMER CICLO" ItemStyle-Width="10%" />
                                                    <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEditarCurso" runat="server" 
                                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                CommandName="Editar" 
                                                                CssClass="btn btn-primary btn-sm" 
                                                                ToolTip="Editar curso programado"
                                                                OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                                <span><i class="fa fa-pen"></i></span>
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="btnEliminarCurso" runat="server" 
                                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                CommandName="Eliminar" 
                                                                CssClass="btn btn-danger btn-sm" 
                                                                ToolTip="Eliminar curso programado"
                                                                OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el registro seleccionado?', 'error');">
                                                                <span><i class="fa fa-trash"></i></span>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                </div>
                                <br/>
                            </div>
                            <div class="tab-pane" id="registro_curso" role="tabpanel" aria-labelledby="registro_curso-tab">
                                <div class="panel-cabecera">
                                    <asp:UpdatePanel ID="udpRegistroCurso" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header">Programar Grupo Horario</div>
                                                <div class="card-body">
                                                    <div class="row">
                                                        <label for="txtGrupo" class="col-sm-2 col-form-label form-control-sm">Grupo:</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtGrupo" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                        </div>
                                                        <label for="cmbEstado" class="col-sm-1 col-form-label form-control-sm">Estado:</label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="cmbEstado" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off">
                                                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                                <asp:ListItem Value="1">ABIERTO</asp:ListItem>
                                                                <asp:ListItem Value="0">CERRADO</asp:ListItem>
                                                            </asp:DropDownList>                                    
                                                        </div>       
                                                        <label for="txtNroGrupos" class="col-sm-1 col-form-label form-control-sm">N° Grupos:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtNroGrupos" runat="server" MaxLength="100" CssClass="form-control form-control-sm" ReadOnly="true" 
                                                                onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                        </div>
                                                        <label for="txtVacantes" class="col-sm-1 col-form-label form-control-sm">Vacantes:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtVacantes" runat="server" MaxLength="100" CssClass="form-control form-control-sm" 
                                                                onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                        </div>                                                 
                                                    </div>
                                                    <div class="row">
                                                        <label for="txtFecha" class="col-sm-2 col-form-label form-control-sm">Fecha:</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtFecha" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                                        </div>   
                                                        <label for="cmbTurno" class="col-sm-1 col-form-label form-control-sm">Turno:</label>
                                                        <div class="col-sm-2">
                                                            <asp:DropDownList ID="cmbTurno" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" ReadOnly="true" AutoComplete="off">
                                                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                                <asp:ListItem Value="M">MAÑANA</asp:ListItem>
                                                                <asp:ListItem Value="T">TARDE</asp:ListItem>
                                                                <asp:ListItem Value="N">NOCHE</asp:ListItem>
                                                            </asp:DropDownList>                                    
                                                        </div>      
                                                        <label for="chkPrimerCiclo" class="col-sm-1 col-form-label form-control-sm">Sólo 1er Ciclo:</label>                                   
                                                        <div class="col-sm-1">                                                
                                                            <asp:CheckBox ID="chkPrimerCiclo" AutoPostBack="True" runat="server"/>
                                                        </div>        
                                                        <label for="chkMultiple" class="col-sm-1 col-form-label form-control-sm">Múltiples Escuelas:</label>                                   
                                                        <div class="col-sm-1">                                                
                                                            <asp:CheckBox ID="chkMultiple" AutoPostBack="True" runat="server"/>
                                                        </div>                                             
                                                    </div>
                                                    <div class="row">                                                                                                                
                                                        <label for="txtFechaInicio" class="col-sm-2 col-form-label form-control-sm">F. Inicio:</label>
                                                        <div class="input-group col-sm-2">
                                                            <asp:TextBox ID="txtFechaInicio" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                                onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="fa fa-calendar-alt"></i>
                                                                </span>
                                                            </div>                                                
                                                        </div>
                                                        <label for="txtFechaFin" class="col-sm-1 col-form-label form-control-sm">F. Fin:</label>
                                                        <div class="input-group col-sm-2">
                                                            <asp:TextBox ID="txtFechaFin" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                                onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="fa fa-calendar-alt"></i>
                                                                </span>
                                                            </div>                                                
                                                        </div>
                                                        <label for="txtFechaRetiro" class="col-sm-1 col-form-label form-control-sm">F. Retiro:</label>
                                                        <div class="input-group col-sm-2">
                                                            <asp:TextBox ID="txtFechaRetiro" runat="server" MaxLength="10" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY" 
                                                                onkeypress="javascript:return soloNumerosFecha(event)" AutoComplete="off"/>
                                                            <div class="input-group-prepend">
                                                                <span class="input-group-text">
                                                                    <i class="fa fa-calendar-alt"></i>
                                                                </span>
                                                            </div>                                                
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <label for="txtRegistrado" class="col-sm-2 col-form-label form-control-sm">Registrado:</label>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtRegistrado" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                                        </div>  
                                                        <label ID="lblBloques" for="txtBloques" runat="server" class="col-sm-1 col-form-label form-control-sm">N° Bloques:</label> 
                                                        <div class="col-sm-2">
                                                            <asp:LinkButton ID="btnEditarBloque" runat="server" CssClass="btn btn-accion btn-naranja"                                                    
                                                                OnClientClick="return alertConfirm(this, event, '¿Desea editar el número de bloques?', 'warning');">
                                                                <i class="fa fa-th"></i>
                                                                <span class="text">Editar</span>
                                                            </asp:LinkButton>  
                                                        </div>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtBloques" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase" ReadOnly="true" Visible="false" AutoComplete="off"/>
                                                        </div>                                                                                                            
                                                    </div>
                                                </div>
                                                <div class="card-footer">
                                                    <asp:LinkButton ID="btnGuardarCurso" runat="server" CssClass="btn btn-accion btn-verde"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar la programación del grupo horario?', 'warning');">
                                                        <i class="fa fa-save"></i>
                                                        <span class="text">Guardar</span>
                                                    </asp:LinkButton>    
                                                    <asp:LinkButton ID="btnSalirCurso" runat="server" CssClass="btn btn-accion btn-danger">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton>                                                  
                                                </div>
                                            </div>                                            
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <br/>
                                    <asp:UpdatePanel ID="udpCursoEquivalente" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header">Asignaturas Equivalentes de Planes Anteriores que se Agruparán con esta Asignatura</div>
                                                <div class="card-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grwListaCursoEquivalente" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_ceq, codigo_PesE, codigo_CurE"
                                                            CssClass="display table table-sm" GridLines="None">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SEL.">
                                                                    <ItemTemplate>                
                                                                        <asp:CheckBox ID="chkElegirCurso" Checked='<%# Bind("estado") %>' ItemStyle-Width="5%" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>                                                                
                                                                <asp:BoundField DataField="nombre_CurE" HeaderText="CURSO" ItemStyle-Width="65%" ItemStyle-Wrap="false"/>
                                                                <asp:BoundField DataField="abreviatura_cpfE" HeaderText="ESCUELA" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                                <asp:BoundField DataField="abreviatura_PesE" HeaderText="PLAN" ItemStyle-Width="10%" ItemStyle-Wrap="false"/>
                                                                <asp:BoundField DataField="total" HeaderText="GRUPOS" ItemStyle-Width="5%" ItemStyle-Wrap="false"/>                                                            
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br/>
                                                    </div>                                                    
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>  
                                    <br/>                                  
                                    <asp:UpdatePanel ID="udpProfesorSugerido" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="card">
                                                <div class="card-header">Listado de Profesores Sugeridos para el Curso</div>
                                                <div class="card-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="grwListaProfesorSugerido" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_Per"
                                                            CssClass="display table table-sm" GridLines="None">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="SEL.">
                                                                    <ItemTemplate>                
                                                                        <asp:CheckBox ID="chkElegirProfesor" Checked='<%# Bind("marcado") %>' ItemStyle-Width="5%" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:BoundField DataField="profesor" HeaderText="PROFESOR" ItemStyle-Width="50%" ItemStyle-Wrap="false"/>                                                           
                                                                <asp:BoundField DataField="anterior" HeaderText="ANTERIOR" ItemStyle-Width="15%" ItemStyle-Wrap="false"/>
                                                                <asp:BoundField DataField="ciclo_asignado" HeaderText="CICLO ASIGNADO" ItemStyle-Width="30%" ItemStyle-Wrap="false"/>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <br/>
                                                    </div>                                                    
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel> 
                                    <br/>                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal Bloques -->
        <div id="mdlRegistrarBloque" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">           
                        <span class="modal-title">REGISTRAR BLOQUES</span>             
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>                                                
                    </div> 
                    <div class="modal-body">
                        <div class="container-fluid">
                            <div class="tab-content" id="contentTabsBloque">
                                <div class="tab-pane show active" id="registroBloque" role="tabpanel" aria-labelledby="registroBloque-tab">
                                    <div class="panel-cabecera">
                                        <asp:UpdatePanel ID="udpRegistroBloque" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="card">
                                                    <div class="card-header">Datos</div>
                                                    <div class="card-body">
                                                        <div class="row">
                                                            <label for="txtNombreAsignatura" class="col-sm-2 form-control-sm">Asignatura:</label>
                                                            <div class="col-sm-9">                                                                                
                                                                <asp:TextBox ID="txtNombreAsignatura" runat="server" CssClass="form-control form-control-sm uppercase" ReadOnly="true" AutoComplete="off"/>
                                                            </div>                                                              
                                                        </div>
                                                        <div class="row">
                                                            <label for="txtHorasDisponible" class="col-sm-2 col-form-label form-control-sm">N° Horas Disp.:</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtHorasDisponible" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase"
                                                                    onkeypress="javascript:return soloNumeros(event)" ReadOnly="true" AutoComplete="off"/>
                                                            </div>
                                                            <label for="txtHorasBloque" class="col-sm-2 col-form-label form-control-sm">N° Horas Bloque:</label>
                                                            <div class="col-sm-2">
                                                                <asp:TextBox ID="txtHorasBloque" runat="server" MaxLength="10" CssClass="form-control form-control-sm uppercase"
                                                                    onkeypress="javascript:return soloNumeros(event)" AutoComplete="off"/>
                                                            </div>                                                            
                                                        </div>
                                                        <hr/>
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <asp:LinkButton ID="btnGuardarBloque" runat="server" CssClass="btn btn-accion btn-verde"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar el bloque?', 'warning');">
                                                                    <i class="fa fa-save"></i>
                                                                    <span class="text">Guardar</span>
                                                                </asp:LinkButton>  
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <br/>
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="udpListaBloque" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="grwListaBloque" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_bcup, codigo_cup"
                                                    CssClass="display table table-sm" GridLines="None">
                                                    <Columns>
                                                        <asp:BoundField DataField="numerohoras" HeaderText="N° HORAS" ItemStyle-Width="50%" ItemStyle-Wrap="false" />
                                                        <asp:TemplateField HeaderText="OPE." ItemStyle-Width="50%" ItemStyle-Wrap="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEliminarBloque" runat="server" 
                                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                                    CommandName="Eliminar" 
                                                                    CssClass="btn btn-danger btn-sm" 
                                                                    ToolTip="Eliminar bloque"
                                                                    OnClientClick="return alertConfirm(this, event, '¿Desea eliminar el bloque seleccionado?', 'error');">
                                                                    <span><i class="fa fa-trash"></i></span>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>  
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br/>
                                    </div>
                                    <br/>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- Modal Bloques -->
    </form>

    <script type="text/javascript">
        var controlId = '';
        var tableCursoEquivalente;
        var tableProfesorSugerido;

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {   
            udpFiltrosUpdate();
            flujoTabs("listado_curso-tab");

            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });

        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnListar':
                    AlternarLoading(false, 'Lista');                      
                    break; 
                
                case 'btnGuardarCurso':
                    AlternarLoading(false, 'Registro');

                case 'btnListarGrupo':
                    AlternarLoading(false, 'ListaCurso');                      
                    break;    

                case 'btnGuardarBloque':
                    AlternarLoading(false, 'GuardarBloque');                      
                    break;                                        
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

                case 'btnGuardarCurso':
                    AlternarLoading(true, 'Registro');

                case 'btnListarGrupo':
                    AlternarLoading(true, 'ListaCurso');                      
                    break;   

                case 'btnGuardarBloque':
                    AlternarLoading(true, 'GuardarBloque');                      
                    break;                                
            }
        });
    </script>
</body>
</html>
