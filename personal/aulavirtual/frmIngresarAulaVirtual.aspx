<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmIngresarAulaVirtual.aspx.vb" Inherits="aulavirtual_frmIngresarAulaVirtual" %>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Asistencia Docente</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css"> 
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../Alumni/css/estilos.css">
    
    <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>            
    <script src="../Alumni/js/popper.js"></script>    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js"></script>
    <script src="../Alumni/js/datatables/jquery.dataTables.min.js?1"></script> 
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js?1"></script>   
    
    <script type="text/javascript">
        function udpHorarioUpdate() {
            formatoGrillaHorario();
        }

        function udpHistorialUpdate() {
            formatoGrillaHistorial();
        }

        /* Dar formato a la grilla. */
        function formatoGrillaHorario(){
            // Setup - add a text input to each footer cell
            /*
            $('#grwHorario thead tr').clone(true).appendTo( '#grwHorario thead' );
            $('#grwHorario thead tr:eq(1) th').each( function (i) {
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
            */
            var table = $('#grwHorario').DataTable( {
                orderCellsTop: true ,
                "order": [[ 2, "asc" ]]
            } );
        }

        function formatoGrillaHistorial(){
            // Setup - add a text input to each footer cell
            $('#grwHistorial thead tr').clone(true).appendTo( '#grwHistorial thead' );
            $('#grwHistorial thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwHistorial').DataTable( {
                orderCellsTop: true ,
                "order": [[ 0, "desc" ]]
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
                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            }).catch(swal.noop);
        }

        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) {
                case 'Actualizar':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista'); 
                    break;

                case 'IniciarClase':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista');               
                    break;

                case 'FinalizarClase':                      
                    $loadingGif = $('.loader');
                    $elemento = $('#udpLista');                  
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

        function loginMoodle(p01, p02, p03, p04, p05, p06, p07, p08, p09, p10){             
            document.getElementById("avm1").value = p01
            document.getElementById("avm2").value = p02
            document.getElementById("avm3").value = p03
            document.getElementById("avm4").value = p04
            document.getElementById("avm5").value = p05
            document.getElementById("avm6").value = p06
            document.getElementById("avm7").value = p07
            document.getElementById("avm8").value = p08
            document.getElementById("curso").value = p09
            
            if(p10 == "S"){
                setTimeout(() => { document.getElementById("frmMoodle").submit(); }, 3000);
            }else{
                document.getElementById("frmMoodle").submit();
            }
        }          

        function mensajePassword(tipo){
            $("#divMensajePassword").addClass("alert-" + tipo);
        }   

        function focusFunction(){
            if(perdioFoco == 1){
                document.getElementById("btnActualizar").click();
                perdioFoco = 0;
            }
        }

        function blurFunction(){
            perdioFoco = 1;
        }

        //onfocus="focusFunction()" onblur="blurFunction()"
    </script>
</head>
<body runat="server" ID="divBody">
    <div class="loader"></div>

    <form id="frmIngresarAulaVirtual" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">INGRESAR AL AULA VIRTUAL</div>
            </div>
            <div class="tab-content" id="contentTabs">
                <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                    <div class="panel-cabecera">
                        <asp:UpdatePanel ID="udpMoodle" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div class="col-sm-12">
                                    <div class="row">
                                        <div class="col-sm-8 alert alert-info" role="alert">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="btnGestionarCursos" runat="server" CssClass="btn btn-accion btn-naranja">
                                                            <i class="fa fa-sync-alt"></i>
                                                            <span class="text">Gestionar mis Cursos</span>
                                                        </asp:LinkButton>                                                 
                                                    </div> 
                                                    <br/>
                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="btnCapacitacion" runat="server" CssClass="btn btn-accion btn-azul">
                                                            <i class="fa fa-plus-square"></i>
                                                            <span class="text">Capacitación y Formación</span>
                                                        </asp:LinkButton>                                                  
                                                    </div> 
                                                </div>                                                
                                                <div class="col-sm-8">                                            
                                                    <h5 class="col-sm-12 alert-heading" style="text-align: center;">¿CONSULTAS SOBRE AULA VIRTUAL?</h5>
                                                    <label class="col-sm-12 col-form-label" style="text-align: center;">
                                                        <i class="fa fa-envelope"></i> serviciosti@usat.edu.pe &nbsp;&nbsp;
                                                        <i class="fa fa-phone"></i> Anexo N° 4050
                                                    </label>                                                                                            
                                                </div>                                        
                                            </div>                                  
                                        </div>                                        
                                        <div class="col-sm-4">
                                            <div class="row">
                                                <div class="col-sm-2"></div>
                                                <div id="divMensajePassword" class="col-sm-10 alert" role="alert">
                                                    <asp:Label ID="lblMensajePassword" Text="" runat="server"/>
                                                    <br/>
                                                    <a href="../servicios/GuiaCambioPassword.pdf" target="_blank" class="alert-link" title="¿Cómo lo hago?">
                                                        <span style="color: #d21d22; text-decoration: underline; margin-top: 3px; display: inline-block;
                                                            cursor: pointer" onmouseover="this.style.color='#900206';" onmouseout="this.style.color='#d21d22';">
                                                            ¿Cómo cambiar mi contraseña?
                                                        </span>
                                                    </a>
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
                                </div>                               
                                <br/>
                                <div class="card oculto">
                                    <div class="card-header" style="text-align: center;"><h5>REGISTRO DE ASISTENCIA DOCENTE</h5></div>
                                    <div class="card-body">
                                        <asp:UpdatePanel ID="udpActualizacion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:LinkButton ID="btnActualizar" runat="server" CssClass="btn btn-accion btn-verde">
                                                            <i class="fa fa-sync-alt"></i>
                                                            <span class="text">Actualizar</span>
                                                        </asp:LinkButton>                                                                                     
                                                        <label id="lblUltimaActualizacion" class="col-sm-5 form-control-sm" style="text-align: left !important;" runat="server">ÚLTIMA ACTUALIZACIÓN:</label>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <hr/>                                        
                                        <div class="table-responsive">
                                            <asp:UpdatePanel ID="udpHorario" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                                <ContentTemplate>
                                                    <asp:GridView ID="grwHorario" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_hdo, codigo_cup, codigo_lho, codigo_hop, codigo_per, descripcion_horario, tipo"
                                                        CssClass="display table table-sm" GridLines="None" EmptyDataText="En este momento no presenta cursos pendientes por iniciar o finalizar.">
                                                        <Columns>
                                                            <asp:BoundField DataField="descripcion_curso" HeaderText="CURSO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="nombre_cpf" HeaderText="ESCUELA" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                            <asp:BoundField DataField="descripcion_horario" HeaderText="HORARIO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                            <asp:TemplateField HeaderText="OPE." ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnIniciar" runat="server" 
                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                        CommandName="Iniciar" 
                                                                        CssClass="btn btn-success btn-sm" 
                                                                        ToolTip="Iniciar Clase"
                                                                        OnClientClick="return alertConfirm(this, event, '¿Desea iniciar la clase del curso seleccionado?', 'warning');">
                                                                        <span><i class="fa fa-toggle-on"></i></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnIniciarInactivo" runat="server" 
                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                        CommandName="IniciarInactivo" 
                                                                        CssClass="btn btn-secondary btn-sm" 
                                                                        ToolTip="Iniciar Clase">
                                                                        <span><i class="fa fa-toggle-on"></i></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnFinalizar" runat="server" 
                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                        CommandName="Finalizar" 
                                                                        CssClass="btn btn-danger btn-sm" 
                                                                        ToolTip="Finalizar Clase"
                                                                        OnClientClick="return alertConfirm(this, event, '¿Desea finalizar la clase del curso seleccionado?', 'warning');">
                                                                        <span><i class="fa fa-power-off"></i></span>
                                                                    </asp:LinkButton>
                                                                    <asp:LinkButton ID="btnFinalizarInactivo" runat="server" 
                                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                                        CommandName="FinalizarInactivo" 
                                                                        CssClass="btn btn-secondary btn-sm" 
                                                                        ToolTip="Finalizar Clase">
                                                                        <span><i class="fa fa-power-off"></i></span>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                                             
                                                        </Columns>
                                                        <EmptyDataRowStyle BackColor="#E33439" ForeColor="White" Font-Size="14px"/> 
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br/>
                                        </div>                                        
                                    </div>                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <br/>
                    <div class="card oculto">
                        <div class="card-header" style="text-align: center;"><h5>HISTORIAL DE ASISTENCIA DOCENTE EN EL SEMESTRE VIGENTE</h5></div>
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:UpdatePanel ID="udpHistorial" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <asp:GridView ID="grwHistorial" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                            CssClass="display table table-sm" GridLines="None" EmptyDataText="En este momento no presenta historial de asistencia en el semestre vigente.">
                                            <Columns>
                                                <asp:BoundField DataField="fecha_clase" HeaderText="F. CLASE" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="curso_programado" HeaderText="CURSO PROGRAMADO" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="nombre_cpf" HeaderText="ESCUELA" ItemStyle-Width="20%" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="horario_curso" HeaderText="HORARIO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="hora_inicio" HeaderText="INICIO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                                <asp:BoundField DataField="hora_fin" HeaderText="FIN" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                            </Columns>
                                            <EmptyDataRowStyle BackColor="#E33439" ForeColor="White" Font-Size="14px"/> 
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
    </form>

    <form id="frmMoodle" target="_blank" method="POST" action="//intranet.usat.edu.pe/aulavirtual/login/index.php">
        <input type="hidden" id="avm1" name="avm1" value="">
        <input type="hidden" id="avm2" name="avm2" value="">
        <input type="hidden" id="avm3" name="avm3" value="">
        <input type="hidden" id="avm4" name="avm4" value="">
        <input type="hidden" id="avm5" name="avm5" value="">
        <input type="hidden" id="avm6" name="avm6" value="">
        <input type="hidden" id="avm7" name="avm7" value="">
        <input type="hidden" id="avm8" name="avm8" value="">
        <input type="hidden" id="curso" name="curso" value="">
    </form>

    <script type="text/javascript">
        var controlId = '';
        var perdioFoco = 0;

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {             
            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        }); 

        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {     
                case 'btnActualizar':
                    AlternarLoading(false, 'Actualizar');                                    
                    break; 

                case 'btnIniciarClase':
                    AlternarLoading(false, 'IniciarClase');                      
                    break; 
                
                case 'btnFinalizarClase':
                    AlternarLoading(false, 'FinalizarClase');
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
                case 'btnActualizar':
                    AlternarLoading(true, 'Actualizar');                                    
                    break; 

                case 'btnIniciarClase':
                    AlternarLoading(true, 'IniciarClase');                                    
                    break;   

                case 'btnFinalizarClase':
                    AlternarLoading(true, 'FinalizarClase');
                    break;

            }
        });               
    </script>
</body>
</html>
