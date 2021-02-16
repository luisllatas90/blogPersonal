<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCargarMatrizIperc.aspx.vb" Inherits="DisenioPuestos_frmCargarMatrizIperc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
 <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Registro de Notificaciones</title>
    
    <!--<link href="../../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />-->
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
            $('#grwListaPuestos thead tr').clone(true).appendTo( '#grwListaPuestos thead' );
            $('#grwListaPuestos thead tr:eq(1) th').each( function (i) {
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
        
            var table = $('#grwListaPuestos').DataTable( {
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
        function OpenModalRiesgos() {
            $('#modalAddRiegos').modal('show');
        }
        
        
        
        
    </script>
<body>
    <div class="loader"></div>
    <form id="frmCompletarDatos" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">ADJUNTAR IPERC DEL PUESTO DE TRABAJO</div>                
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
                                            <label for="cmbTipoFiltro" class="col-sm-1 col-form-label form-control-sm">Dependencia/Area:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">FACULTAD DE MEDICINA</asp:ListItem>
                                                    <asp:ListItem Value="">DEPARTAMENTO DE CIENCIA DE LA SALUD</asp:ListItem>                                                                                                       
                                                </asp:DropDownList>
                                            </div>   
                                            <label for="cmbTipoFiltro" class="col-sm-1 col-form-label form-control-sm">Tipo Trabajo:</label>
                                            <div class="col-sm-2">
                                                <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">ADMINISTRATIVO</asp:ListItem>
                                                    <asp:ListItem Value="">DOCENTE</asp:ListItem>
                                                    <asp:ListItem Value="">JEFE DE PRACTICAS</asp:ListItem>
                                                    <asp:ListItem Value="">DE SERVICIO</asp:ListItem>                                                   
                                                </asp:DropDownList>
                                            </div>
                                            <label for="cmbPuesto" class="col-sm-1 col-form-label form-control-sm">Puesto:</label>
                                            <div class="col-sm-2">
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
                                <asp:GridView ID="grwListaPuestos" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames=""
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                       <asp:BoundField DataField="puesto" HeaderText="PUESTO DE TRABAJO" ItemStyle-Width="50%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="15%" ItemStyle-Wrap="false" />
                                      <%-- <asp:BoundField DataField="Fecha" HeaderText="FECHA" ItemStyle-Width="10%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="Escuela" HeaderText="ESCUELA" ItemStyle-Width="30%" ItemStyle-Wrap="false" />
                                       <asp:BoundField DataField="Egresado" HeaderText="EGRESADO" ItemStyle-Width="30%" ItemStyle-Wrap="false" />--%>
                                       <asp:TemplateField HeaderText="ADJUNTAR IPERC" ItemStyle-Width="20%" ItemStyle-Wrap="false">
                                            <ItemTemplate>                                            
                                                <asp:FileUpload ID="fuArchivoPdf" runat="server" EnableViewState="true" CssClass="fu" accept=".xlsx"/>
                                                <asp:LinkButton ID="btnSubir" runat="server" 
                                                    ToolTip="Adjuntar IPERC"   
					                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                CommandName="firmar" CssClass="btn btn-warning btn-sm">
                                                    <span><i class="fa fa-paperclip"></i></span> Adjuntar                                                      
						                        </asp:LinkButton>
						                    </ItemTemplate>
                                       </asp:TemplateField>             
						               <asp:TemplateField HeaderText="RIESGOS" ItemStyle-Width="15%" ItemStyle-Wrap="false">
                                            <ItemTemplate>      
                                                <asp:LinkButton ID="btnAddRiesgos" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="AddRiesgos" 
                                                    CssClass="btn btn-info btn-sm" 
                                                    ToolTip="Agregar riesgos del puesto de trabajo"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea agregar riesgos al puesto de trabajo?', 'warning');">
                                                    <span><i class="fa fa-plus"></i></span>
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
                                    <div class="card-header">Datos del Egresado / Tesis</div>
                                    <div class="card-body">
                                        <div class="row">
                                             <label for="txtDenominacion" class="col-sm-1 col-form-label form-control-sm">Egresado:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBox2" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                             <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Codigo:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="TextBox3" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                                 
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card-header">Datos de la Resolución de Sustentación</div>
                                    <div class="card-body">
                                         <div class="row">
                                             <label for="txtDenominacion" class="col-sm-1 col-form-label form-control-sm">Número:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="TextBox4" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                             <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Fecha:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="TextBox5" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                                 
                                        </div>
                                    </div>
                                    <br />
                                    <div class="card-header">Datos del Acta de Sustentación</div>
                                    <div class="card-body">  
                                        <div class="row">
                                             <label for="txtDenominacion" class="col-sm-1 col-form-label form-control-sm">Número:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="TextBox6" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                             <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Fecha:</label>
                                            <div class="col-sm-2">
                                                <asp:TextBox ID="TextBox7" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>                                                 
                                        </div>                                                                             
                                        <div class="row">
                                            <label for="cmbReporta" class="col-sm-1 col-form-label form-control-sm">Calificativo:</label>
                                            <div class="col-sm-5">
                                                <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                                    <asp:ListItem Value="">APROBADO</asp:ListItem>
                                                    <asp:ListItem Value="">NOTABLE</asp:ListItem>
                                                    <asp:ListItem Value="">DESAPROBADO</asp:ListItem>                                                                                                                                                         
                                                </asp:DropDownList>
                                            </div> 
                                                       
                                        </div> 
                                     </div>
                                     <br /> 
                                     <div class="card-header">Jurado de la Sustentación de Tesis</div>
                                     <div class="card-body"> 
                                         <div class="row">
                                            <label for="txtDenominacion" class="col-sm-1 col-form-label form-control-sm">Presidente:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBox8" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="lbModOlBuscaEmp" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>
                                            </div>
                                          </div>
                                          <div class="row">
                                            <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Secretario:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBox9" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>
                                            </div>
                                          </div>
                                          <div class="row">
                                          <label for="txtClasificacion" class="col-sm-1 col-form-label form-control-sm">Vocal:</label>
                                            <div class="col-sm-5">
                                                <asp:TextBox ID="TextBox10" runat="server"  CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                            </div>
                                            <div class="col-sm-1">
                                                <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>
                                            </div>                                                 
                                        </div>                                     
                                     </div>
                                     <br />
                                    
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
        <!-- modal Riesgos--> 
        <div id="modalAddRiegos" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="modalFinalizaBody">
                <asp:UpdatePanel ID="udpModalComp" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Agregar Riesgos</span> 
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
                                <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">Descripción Riesgo:</label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="txtRiesgo" ReadOnly="false" runat="server" MaxLength="300" CssClass="form-control form-control-sm uppercase" AutoComplete="off" TextMode="MultiLine" Rows="3"/>
                                </div>        
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbAddComp" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar el riesgo al puesto de trabajo?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Agregar</span>
                                    </asp:LinkButton>  
                                </div>
                            </div>
                            <br />
                            <div class="row">                                
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvRiesgo" runat="server" AutoGenerateColumns=false CssClass="table table-striped table-bordered table-hover" DataKeyNames="" > 
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="riesgo" HeaderText="RIESGO"/>
                                            <asp:TemplateField HeaderText="ACCION" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtDeleteRiesgo" runat="server" 
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

                //                case 'btnGuardar': 
                //                    AlternarLoading(false, 'Registro'); 

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

                //                case 'btnGuardar': 
                //                    AlternarLoading(true, 'Registro'); 

            }
        });    
    </script>
    
</body>
</html>
