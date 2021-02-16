<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPuestoTrabajo.aspx.vb" Inherits="DisenioPuestos_frmPuestoTrabajo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Notificaciones</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../Alumni/css/datatables/jquery.dataTables.min.css">      
    <link rel="stylesheet" href="../Alumni/css/sweetalert/sweetalert2.min.css"> 

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
    
    <!-- Scripts propios -->
    <script src="../Alumni/js/funciones.js?1"></script>
    
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
        
        function udpRegistroUpdate() { 
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
        function OpenModalComp() {
            $('#modalAddComp').modal('show');
        }
        
        function OpenModalTF() {
            $('#modalAddTF').modal('show');
        }
        
         function OpenModalFuncionPt() {
            $('#modalAddFuncionPt').modal('show');
        }
        
        
    </script>
</head>
<body>
    <div class="loader"></div>
    <form id="frmPuestoTrabajo" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">MANTENIMIENTO DE PUESTO DE TRABAJO</div>                
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
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
                                    <div class="card-header">Filtros de Búsqueda</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="txtArea" class="col-sm-1 col-form-label form-control-sm">Dependencia / área:</label>
                                             <div class="col-sm-6">
                                                <asp:TextBox ID="txtArea" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" Text="FACULTAD DE MEDICINA"  Enabled="False" Font-Bold="True" />
                                            </div>  
                                        </div>
                                        <div class="row">
                                            <label for="cmbTipoFiltro" class="col-sm-1 col-form-label form-control-sm">Tipo Trabajo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">ADMINISTRATIVO</asp:ListItem>
                                                    <asp:ListItem Value="">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="">JEFE DE PRACTICAS</asp:ListItem>
                                                    <asp:ListItem Value="">DE SERVICIO</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbPuesto" class="col-sm-1 col-form-label form-control-sm">PUESTO TRABAJO:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbPuesto" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">COORDINADOR DE CALIDAD</asp:ListItem>
                                                    <asp:ListItem Value="">DIRECTOR DE ESCUELA</asp:ListItem>
                                                    <asp:ListItem Value="">DECANO DE FACULTAD</asp:ListItem>
                                                    <asp:ListItem Value="">ENFERMERA</asp:ListItem>                                                   
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
                                                <asp:LinkButton ID="btnNuevo" runat="server" CssClass="btn btn-accion btn-azul"                                                    
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea registrar un nuevo puesto de trabajo?', 'warning');">
                                                    <i class="fa fa-plus-square"></i>
                                                    <span class="text">Nuevo</span>
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
                                <asp:GridView ID="grwLista" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="Puesto" HeaderText="DENOMINACION DEL PUESTO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                        <%--<asp:BoundField DataField="tipo" HeaderText="TIPO" ItemStyle-Width="10%" ItemStyle-Wrap="false" />--%>
                                        <asp:TemplateField HeaderText="OPCIONES" ItemStyle-Width="10%" ItemStyle-Wrap="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" 
                                                    CssClass="btn btn-primary btn-sm" 
                                                    ToolTip="Editar puesto de trabajo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea editar el registro seleccionado?', 'warning');">
                                                    <span><i class="fa fa-pen"></i></span>
                                                </asp:LinkButton>                                              
                                                <asp:LinkButton ID="btnAddComp" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="AddComp" 
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Agregar competencia"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar competencias al puesto de trabajo?', 'warning');">
                                                    <span><i class="fa fa-list"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnAddTF" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="AddTF" 
                                                    CssClass="btn btn-warning btn-sm" 
                                                    ToolTip="Agregar rol de accesos"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar rol de accesos al puesto de trabajo?', 'warning');">
                                                    <span><i class="fa fa-user-plus"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnAddFuncPt" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="AddFuncioPt" 
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Agregar funciones"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar rol de accesos al puesto de trabajo?', 'warning');">
                                                    <span><i class="fa fa-bars"></i></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnDelete" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                    CommandName="Eliminar" 
                                                    CssClass="btn btn-danger btn-sm" 
                                                    ToolTip="Desactivar puesto de trabajo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea desactivar el puesto seleccionado?', 'warning');">
                                                    <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton>
                                                  <asp:LinkButton ID="btnEnviar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Enviar" 
                                                    CssClass="btn btn-success btn-sm" 
                                                    ToolTip="Enviar para aprobación"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar para aprobación?', 'warning');">
                                                    <span><i class="fa fa-paper-plane"></i></span>
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
                                    <div class="card-header">Datos del Puesto de Trabajo</div>
                                    <div class="card-body">
                                        <div class="row">
                                            <label for="cmbTipo" class="col-sm-1 col-form-label form-control-sm">Tipo de trabajo:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="cmbTipo" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">ADMINISTRATIVO</asp:ListItem>
                                                    <asp:ListItem Value="">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="">JEFE DE PRACTICAS</asp:ListItem>
                                                    <asp:ListItem Value="">DE SERVICIO</asp:ListItem>                                                  
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row">
                                             <label for="txtDenominacion" class="col-sm-1 col-form-label form-control-sm">Denominación:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="txtDenominacion" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                             <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Clasificación:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtClasificacion" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <label for="txtNroVac" class="col-sm-1 col-form-label form-control-sm">Nro. Vacantes:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtNroVac" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>     
                                        </div>
                                        <div class="row">
                                            <label for="cmbReporta" class="col-sm-1 col-form-label form-control-sm">Reporta a:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                      <asp:ListItem Value="">DECANO</asp:ListItem>
                                                    <asp:ListItem Value="">CAPELLAN</asp:ListItem>
                                                    <asp:ListItem Value="">DIRECTOR</asp:ListItem>
                                                    <asp:ListItem Value="">ANALISTA DE CALIDAD TI</asp:ListItem>                                                                                                     
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbSupervisa" class="col-sm-1 col-form-label form-control-sm">Supervisa a:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">DECANO</asp:ListItem>
                                                    <asp:ListItem Value="">CAPELLAN</asp:ListItem>
                                                    <asp:ListItem Value="">DIRECTOR</asp:ListItem>
                                                    <asp:ListItem Value="">ANALISTA DE CALIDAD TI</asp:ListItem>                                                                                                     
                                                </asp:DropDownList>
                                            </div>                             
                                        </div> 
                                        <div class="row">
                                           <%-- <label for="txtVersion" class="col-sm-1 col-form-label form-control-sm">Versión:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="txtVersion" runat="server" MaxLength="100" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>     
                                            <label for="cmbProfile" class="col-sm-1 col-form-label form-control-sm">Profile:</label>                                        
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="cmbProfile" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"/>
                                            </div> --%>
                                        </div> 
                                        <div class="row">
                                            <%--<label for="txtAsunto" class="col-sm-1 col-form-label form-control-sm">Asunto:</label>
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
                                        </div>                    --%>                                                            
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
        <!-- modal competencias--> 
        <div id="modalAddComp" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="modalFinalizaBody">
                <asp:UpdatePanel ID="udpModalComp" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Agregar Competencias</span> 
                                <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">				
                            <div class="row">
                                <div>                        
                                    <asp:HiddenField ID="hdCodigo_ofe" runat="server" />
                                    <asp:TextBox ID="txtCodOfMod" runat="server" Visible="False"></asp:TextBox>							
                                </div>                        
                            </div>
                            <div class="row">
                                <div id="mensajeModal" runat="server"></div>
                            </div>
                            <div class="row"> 
                                <label class="col-sm-3 col-form-label form-control-sm" for="lblCodigo">Competencias:</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlCarrrera" runat="server" class="form-control" AutoPostBack="true" >
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">LIDERAZGO</asp:ListItem>
                                                    <asp:ListItem Value="">CAPACIDAD DE NEGOCIACION</asp:ListItem>
                                                    <asp:ListItem Value="">PLANIFICACION</asp:ListItem>
                                                    <asp:ListItem Value="">COMUNICACION ASERTIVA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbAddComp" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar la competencia?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Agregar</span>
                                    </asp:LinkButton>  
                                </div>
                            </div>
                            <div class="row">                                
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvCompetencia" runat="server" AutoGenerateColumns=false CssClass="table table-striped table-bordered table-hover" DataKeyNames="" > 
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="COMPETENCIA"/>
                                            <asp:TemplateField HeaderText="ACCION" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDeleteComp" runat="server" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CommandName="DeleteComp" CssClass="btn btn-danger btn-sm">
                                                <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px"                                                     
                                        ForeColor="White" />
                                    </asp:GridView>
                                </div>                      
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnModCompRegresar" class="btn btn-accion btn-rojo" data-dismiss="modal"> 
                                <i class="fa fa-sign-out-alt"></i>Volver
                            </button>                    
                        </div>	
                    </ContentTemplate>
                </asp:UpdatePanel>  		
            </div> 
        </div>        
    </div>
         <!-- modal tipo funciones Rol de accesos--> 
        <div id="modalAddTF" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="Div2">
                <asp:UpdatePanel ID="udpModalTF" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Agregar Rol de Accesos</span> 
                                <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">				
                            <div class="row">
                                <div>                        
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:TextBox ID="TextBox1" runat="server" Visible="False"></asp:TextBox>							
                                </div>                        
                            </div>
                            <div class="row">
                                <div id="Div3" runat="server"></div>
                            </div>
                            <div class="row"> 
                                <label class="col-sm-3 col-form-label form-control-sm" for="lblCodigo">Rol de Accesos:</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlTf" runat="server" class="form-control" AutoPostBack="true" >
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">DIRECTOR DE ESCUELA</asp:ListItem>
                                                    <asp:ListItem Value="">JEFE DE PENSIONES</asp:ListItem>
                                                    <asp:ListItem Value="">ALTA DIRECCIÓN</asp:ListItem>
                                                    <asp:ListItem Value="">ASISTENTE DE DIRECTORES DE ESCUELA</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbAddTf" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar el rol de acceso?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Agregar</span>
                                    </asp:LinkButton>  
                                </div>
                            </div>
                            <div class="row">                                
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvTF" runat="server" AutoGenerateColumns=false CssClass="table table-striped table-bordered table-hover" DataKeyNames="" > 
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="ROL DE ACCESOS"/>
                                            <asp:TemplateField HeaderText="ACCION" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDeleteComp" runat="server" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CommandName="DeleteComp" CssClass="btn btn-danger btn-sm">
                                                <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px"                                                     
                                        ForeColor="White" />
                                    </asp:GridView>
                                </div>                      
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnVolverTf" class="btn btn-accion btn-rojo" data-dismiss="modal"> 
                                <i class="fa fa-sign-out-alt"></i>Volver
                            </button>                    
                        </div>	
                    </ContentTemplate>
                </asp:UpdatePanel>  		
            </div> 
        </div>        
    </div>
         <!-- modal funciones -->
        <div id="modalAddFuncionPt" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="Div1">
                <asp:UpdatePanel ID="udpFuncionesPt" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Agregar Fucnciones</span> 
                                <button type="button" class="close" data-dismiss="modal">
                                &times;</button>
                        </div>
                        <div class="modal-body">				
                            <div class="row">
                                <div>                        
                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                    <asp:TextBox ID="TextBox2" runat="server" Visible="False"></asp:TextBox>							
                                </div>                        
                            </div>
                            <div class="row">
                                <div id="Div4" runat="server"></div>
                            </div>
                            <div class="row"> 
                                <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">Descripción Función:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtFuncionPt" ReadOnly="false" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                </div>        
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar la función al puesto de trabajo?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Agregar</span>
                                    </asp:LinkButton>  
                                </div>
                            </div>
                            <br />
                            <div class="row">                                
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvFuncionPt" runat="server" AutoGenerateColumns=false CssClass="table table-striped table-bordered table-hover" DataKeyNames="" > 
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="riesgo" HeaderText="RIESGO"/>
                                            <asp:TemplateField HeaderText="ACCION" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDelFuncPt" runat="server" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CommandName="DeleteFuncionPt" CssClass="btn btn-danger btn-sm">
                                                <span><i class="fa fa-trash"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px"                                                     
                                        ForeColor="White" />
                                    </asp:GridView>
                                </div>                      
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="Button1" class="btn btn-accion btn-rojo" data-dismiss="modal"> 
                                <i class="fa fa-sign-out-alt"></i>Volver
                            </button>                    
                        </div>	
                    </ContentTemplate>
                </asp:UpdatePanel>  		
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
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
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

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
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
