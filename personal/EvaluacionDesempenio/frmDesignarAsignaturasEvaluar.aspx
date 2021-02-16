<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDesignarAsignaturasEvaluar.aspx.vb" Inherits="EvaluacionDesempenio_frmDesignarAsignaturasEvaluar" %>
<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Designación de Asignaturas a Evaluar</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css"> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.css">

    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css?13">

    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    <script src="https://cdnjs.cloudflare.com/ajax/libs/summernote/0.8.12/summernote-bs4.js"></script>
    
    <!-- Scripts propios -->
    <script src="../../Alumni/js/funciones.js?1"></script>
    
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

        function udpListaInstrumentosUpdate(){
            formatoGrillaInstrumentos();
        }
        
        function udpRegistroUpdate() { 
            actualizarSummernote();

             /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });        
        }

        function actualizarSummernote(){
            /*Editor de Texto*/
            $('.summernote').summernote('destroy');            
            $('.summernote').summernote({
                placeholder: 'Redacta tu mensaje...',
                tabsize: 2,
                height: 500,
                focus: true
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

        function formatoGrillaInstrumentos(){
            // Setup - add a text input to each footer cell
            $('#grwListaInstrumentos thead tr').clone(true).appendTo( '#grwListaInstrumentos thead' );
            $('#grwListaInstrumentos thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaInstrumentos').DataTable( {
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
            if (ctl.id == 'btnGuardar') {
                makeSafe();    
            }

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

            }else if (tabActivo == 'registro-tab'){
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

        /*Permite codificar contenido html*/
        function makeSafe() {            
            //var contenido = window.escape($('#divMensaje').summernote('code'));
            var contenido = encodeURI($('#divPlantilla').summernote('code'));                           
            $("#<%=txtPlantilla.ClientID%>").val(contenido);
        };  

        function setCodigoPlantilla(contenido){
            $('#divPlantilla').summernote('code', contenido);
        }
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmSugerenciaAsignaturas" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">DESIGNAR ASIGNATURAS A EVALUAR</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Lista</a>
                </li>
                <li class="nav-item">
                    <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                        aria-controls="registro" aria-selected="false">Registro</a>
                </li>
            </ul>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="card">
                                    <div class="card-header">Filtros</div>
                                    <div class="card-body">

                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label for="cmbPeriodoFiltro" >PERIODO/FECHA:</label>
                                                
                                                <asp:DropDownList ID="cmbPeriodoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">2021 - I</asp:ListItem>
                                                    <asp:ListItem Value="Y">2021 - II</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                                
                                            </div>

                                            <div class="col-sm-6">
                                                <label for="cmbProgramaEstudioFiltro" >Programa de Estudio:</label>
                                                
                                                <asp:DropDownList ID="cmbProgramaEstudioFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">PROGRAMA 1</asp:ListItem>
                                                    <asp:ListItem Value="Y">PROGRAMA 2</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                                
                                            </div>

                                        </div>  

                                        <hr/>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <asp:LinkButton ID="btnListar" runat="server" CssClass="btn btn-accion btn-celeste">
                                                    <i class="fa fa-sync-alt"></i>
                                                    <span class="text">Listar</span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-azul">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">NUEVA ASIGNACIÓN</span>
                                                </asp:LinkButton>
                                                <!--
                                                    <asp:LinkButton ID="btnNuevo2" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar una nueva notificación?', 'warning');">
                                                        <i class="fa fa-plus-square"></i>
                                                        <span class="text">Nuevo</span>
                                                    </asp:LinkButton>
                                                -->                             
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
                                    <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_asg"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>

                                            <asp:BoundField DataField="codigo_asg" HeaderText="CÓDIGO" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="nombre_asg" HeaderText="ASIGNATURA" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="ciclo_asg" HeaderText="CICLO" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="docente_asg" HeaderText="DOCENTE" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="grupo_asg" HeaderText="GRUPO" ItemStyle-Width="5%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="comentario_asg" HeaderText="COMENTARIO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                            <asp:BoundField DataField="estado_asg" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
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
                                    <div class="card-header">Datos de la Sugerencia de Evaluaci&oacute;n de Asignaturas</div>
                                    <div class="card-body">
                                        <div class="row">

                                            <div class="col-sm-6">
                                                <label for="cmbProgramaEstudio">Programa de Estudio:</label>
                                                <asp:DropDownList ID="cmbProgramaEstudio" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="P1">PROGRAMA 1</asp:ListItem>
                                                    <asp:ListItem Value="P2">PROGRAMA 2</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-sm-6">
                                                <label for="cmbAsignatura" >Asignatura:</label>
                                                <asp:DropDownList ID="cmbAsignatura" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">ASIGNATURA 1</asp:ListItem>
                                                    <asp:ListItem Value="Y">ASIGNATURA 2</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>
                                            
                                        </div>

                                        <div class="row">                                            
                                            <div class="col-sm-6">
                                                <label for="cmbDocente" >Docente:</label>
                                                <asp:DropDownList ID="cmbDocente" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">DOCENTE 1</asp:ListItem>
                                                    <asp:ListItem Value="Y">DOCENTE 2</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                                
                                            </div>

                                            <div class="col-sm-3">
                                                <label for="cmbCiclo" >Ciclo:</label>
                                                <asp:DropDownList ID="cmbCiclo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">I</asp:ListItem>
                                                    <asp:ListItem Value="Y">II</asp:ListItem>
                                                    <asp:ListItem Value="Y">III</asp:ListItem>
                                                </asp:DropDownList>
                                                
                                            </div>

                                            <div class="col-sm-3">
                                                <label for="cmbGrupo" >Grupo:</label>
                                                <asp:DropDownList ID="cmbGrupo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="X">GRUPO A</asp:ListItem>
                                                    <asp:ListItem Value="Y">GRUPO B</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                                
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <label for="txtComentario">Comentario</label>
                                                <asp:Textbox id="txtComentario" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>

                                        <!--
                                        <div class="row">
                                            <label for="txtNombre" class="col-sm-1 col-form-label form-control-sm">Trabajador:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtNombre" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>      
                                            <label for="txtAbreviatura" class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                            <div class="col-sm-3">
                                                <asp:TextBox ID="txtAbreviatura" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                         
                                        </div> 
                                        <div class="row">
                                            <label for="txtVersion" class="col-sm-1 col-form-label form-control-sm">Versión:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtVersion" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>     
                                            <label for="cmbProfile" class="col-sm-1 col-form-label form-control-sm">Profile:</label>                                        
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbProfile" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>
                                            </div> 
                                        </div> 
                                        <div class="row">
                                            <label for="txtAsunto" class="col-sm-1 col-form-label form-control-sm">Asunto:</label>
                                            <div class="col-sm-10">
                                                <asp:TextBox ID="txtAsunto" runat="server" MaxLength="500" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                             
                                        </div>    
                                        <div class="row">
                                            <label for="divPlantilla" class="col-sm-1 col-form-label form-control-sm">Cuerpo:</label>
                                            <div class="col-sm-10">
                                                <asp:HiddenField ID="txtPlantilla" runat="server" />
                                                <div id="divPlantilla" class="summernote"></div>
                                            </div>                                            
                                        </div>                                                                                
                                        -->
                                    </div>
                                    <div class="card-footer" style="text-align: center;">
                                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-accion btn-verde"
                                            OnClientClick="return alertConfirm(this, event, '¿Desea registrar la notificación?', 'warning');">
                                            <i class="fa fa-save"></i>
                                            <span class="text">Guardar</span>
                                        </asp:LinkButton>    
                                        <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-accion btn-danger">
                                            <i class="fa fa-sign-out-alt"></i>
                                            <span class="text">Salir</span>
                                        </asp:LinkButton>                                                  
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>            
        </div>
    </form>

    <script type="text/javascript">
        var controlId = '';

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {   
            udpFiltrosUpdate();
            udpRegistroUpdate();            

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
    </script>
</body>
</html>