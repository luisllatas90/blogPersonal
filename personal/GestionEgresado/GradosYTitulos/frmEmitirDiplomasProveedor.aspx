<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEmitirDiplomasProveedor.aspx.vb" Inherits="GraduacionTitulacion_Tramite_frmEmitirDiplomasProveedor" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <meta http-equiv="X-UA-Compatible" content="IE=10" />    
    <title>Envío de Diplomas a Proveedor</title>

    <!-- Estilos externos -->
    <link rel="stylesheet" href="../../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="../../Alumni/css/datatables/jquery.dataTables.min.css">   
    <link rel="stylesheet" href="../../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">   
    <link rel="stylesheet" href="../../Alumni/css/sweetalert/sweetalert2.min.css"> 
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="../../Alumni/css/estilos.css"> 

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
    <script src="../../Alumni/js/funciones.js"></script>
    
    <script type="text/javascript">
        var selTodos = true;

        function udpFiltrosUpdate() {
            /*Combos*/
            $(".combo_filtro").selectpicker({
                size: 6,
            });                     
        }

        function udpListaUpdate() {
            formatoGrillaFisico();
            formatoGrillaElectronico();
        }

        /* Dar formato a la grilla. */
        function formatoGrillaFisico(){
            // Setup - add a text input to each footer cell
            $('#grwListaFisico thead tr').clone(true).appendTo( '#grwListaFisico thead' );
            $('#grwListaFisico thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( tableFisico.column(i).search() !== this.value ) {
                        tableFisico
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            tableFisico = $('#grwListaFisico').DataTable( {
                orderCellsTop: true
            } );
        }  

        function formatoGrillaElectronico(){
            // Setup - add a text input to each footer cell
            $('#grwListaElectronico thead tr').clone(true).appendTo( '#grwListaElectronico thead' );
            $('#grwListaElectronico thead tr:eq(1) th').each( function (i) {
                var title = $(this).text();
                $(this).html( '<input type="text" placeholder="Buscar '+ title+'" />' );
        
                $( 'input', this ).on( 'keyup change', function () {
                    if ( tableElectronico.column(i).search() !== this.value ) {
                        tableElectronico
                            .column(i)
                            .search( this.value )
                            .draw();
                    }
                } );
            } );
        
            tableElectronico = $('#grwListaElectronico').DataTable( {
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
                    if (ctl.id == "btnEnviar"){
                        tableElectronico.page.len(-1).draw();
                    }
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
                case 'Lista':
                    $loadingGif = $('.loader');   
                    $elemento = $('#udpLista');               
                    break;

                case 'Enviar':                      
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
        
        function marcarTodos() {             
            //var rowsFisico = tableFisico.rows({ 'search': 'applied' }).nodes();            
            //$('input[type="checkbox"]', rowsFisico).prop('checked', selTodos);   

            var rowsElectronico = tableElectronico.rows({ 'search': 'applied' }).nodes();            
            $('input[type="checkbox"]', rowsElectronico).prop('checked', selTodos);

            if (selTodos) {
                selTodos = false;
            } else {
                selTodos = true;
            }         
        }  

        /*Mostrar el boton Seleccionar Todos*/ 
        function mostrarBotonTodos(ind_mostrar) {
            if(ind_mostrar == "O"){
                $('#sel-todos').addClass("oculto");
            }else{
                $('#sel-todos').removeClass("oculto");
            }            
        }             

        function presionarBotonListar(){
            setTimeout(() => { document.getElementById('btnListar').click(); }, 3000);
        }                       
    </script>
</head>
<body>
    <div class="loader"></div>

    <form id="frmEmitirDiplomasProveedor" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>

        <div class="container-fluid">
            <div class="card div-title">                        
                <div class="row title">ENVÍO DE DIPLOMAS A PROVEEDOR</div>
            </div>
            <ul class="nav nav-tabs" role="tablist">
                <li class="nav-item">
                    <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                        aria-controls="listado" aria-selected="true">Listado</a>
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
                                            <label for="cmbTipoDiplomaFiltro" class="col-sm-2 col-form-label form-control-sm">Tipo Diploma:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoDiplomaFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[--SELECCIONE--]</asp:ListItem>
                                                    <asp:ListItem Value="F">FÍSICO</asp:ListItem>
                                                    <asp:ListItem Value="E">ELECTRÓNICO</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>        
                                            <label for="cmbSesionConsejoFiltro" class="col-sm-2 col-form-label form-control-sm">Sesión de Consejo:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbSesionConsejoFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>                                                                                                                                                                                                                 
                                        </div> 
                                        <div class="row">  
                                            <label for="cmbTipoDenominacionFiltro" class="col-sm-2 col-form-label form-control-sm">Denominación:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoDenominacionFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off"></asp:DropDownList>
                                            </div>  
                                            <label ID="lblTipoEmisionFiltro" for="cmbTipoEmisionFiltro" runat="server" class="col-sm-2 col-form-label form-control-sm">Tipo de Emisión:</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="cmbTipoEmisionFiltro" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm combo_filtro" data-live-search="true" AutoComplete="off">
                                                    <asp:ListItem Value="">[--SELECCIONE--]</asp:ListItem>
                                                    <asp:ListItem Value="O">ORIGINAL</asp:ListItem>
                                                    <asp:ListItem Value="D">DUPLICADO</asp:ListItem>
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
                                                <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-accion btn-verde">
                                                    <i class="fa fa-file-excel"></i>
                                                    <span class="text">Exportar</span>
                                                </asp:LinkButton>   
                                                <asp:LinkButton ID="btnEnviar" runat="server" CssClass="btn btn-accion btn-rojo" Visible="false"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea enviar los diplomas electrónicos al proveedor?', 'warning');">
                                                    <i class="fa fa-envelope-square"></i>
                                                    <span class="text">Enviar a Proveedor</span>
                                                </asp:LinkButton>  
                                                <asp:LinkButton ID="btnExportarRegistrar" runat="server" CssClass="btn btn-accion btn-rojo" Visible="false"
                                                    OnClientClick="return alertConfirm(this, event, '¿Desea exportar y registrar las tramas de diplomas físicos?', 'warning');">
                                                    <i class="fa fa-file-excel"></i>
                                                    <span class="text">Exportar y Registrar</span>
                                                </asp:LinkButton>                                                                                                                                                                                                                                                             
                                            </div>  
                                        </div>                                     
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="table-responsive" runat="server">
                        <asp:UpdatePanel ID="udpLista" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <div id="sel-todos" class="oculto">
                                    <asp:LinkButton ID="btnTodos" runat="server" CssClass="btn btn-accion btn-naranja"
                                        OnClientClick="marcarTodos();">
                                        <i class="fa fa-check-square"></i>
                                        <span class="text">Todos</span>
                                    </asp:LinkButton>
                                </div>
                                <br/>
                                <asp:GridView ID="grwListaFisico" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_egr, codigo_trl"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:BoundField DataField="grado_académico" HeaderText="GRADO_ACADÉMICO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Alumno" HeaderText="ALUMNO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Opto_Dia" HeaderText="FEC_OPTO_DIA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Opto_Mes" HeaderText="FEC_OPTO_MES" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Opto_Año" HeaderText="FEC_OPTO_AÑO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Facultad" HeaderText="FACULTAD" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Escuela" HeaderText="ESCUELA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Emi_Dia" HeaderText="FEC_EMI_DIA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Emi_Mes" HeaderText="FEC_EMI_MES" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Emi_Año" HeaderText="FEC_EMI_AÑO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Rector" HeaderText="RECTOR" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Cargo_Rec" HeaderText="CARGO_REC" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Secretario_General" HeaderText="SECRETARIO_GENERAL" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Cargo_Sec" HeaderText="CARGO_SEC" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Decano" HeaderText="DECANO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Cargo_dec" HeaderText="CARGO_DEC" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="cod_Uni" HeaderText="COD_UNI" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="T_Documento" HeaderText="T_DOCUMENTO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="N_Documento" HeaderText="N_DOCUMENTO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Abrev_G" HeaderText="ABREV_G" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Obtenido_Por" HeaderText="OBTENIDO_POR" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Modalidad" HeaderText="MODALIDAD" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Num_Reso" HeaderText="NUM_RESO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Fec_Reso" HeaderText="FEC_RESO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Nro_Diploma" HeaderText="NRO_DIPLOMA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Tipo_Emisión" HeaderText="TIPO_EMISIÓN" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Libro" HeaderText="LIBRO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Folio" HeaderText="FOLIO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Registro" HeaderText="REGISTRO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Jefe_Ofic" HeaderText="JEFE_OFIC" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Cargo_Jefe_Ofic" HeaderText="CARGO_JEFE_OFIC" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="Imagen_Descripcion" HeaderText="IMAGEN_DESCRIPCION" ItemStyle-Wrap="false" />                                        
                                    </Columns>                               
                                </asp:GridView>
                                <asp:GridView ID="grwListaElectronico" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_egr, codigo_trl"
                                    CssClass="display table table-sm" GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SEL.">
                                            <ItemTemplate>                
                                                <asp:CheckBox ID="chkElegir" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="NUMERO_DOCUMENTO" HeaderText="NUMERO_DOCUMENTO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="TIPO_DOCUMENTO" HeaderText="TIPO_DOCUMENTO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_Y_APELLIDOS" HeaderText="NOMBRES_Y_APELLIDOS" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO" HeaderText="CORREO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FOTO" HeaderText="FOTO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="DNI_A1" HeaderText="DNI_A1" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_A1" HeaderText="NOMBRES_A1" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CARGO_A1" HeaderText="CARGO_A1" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO_A1" HeaderText="CORREO_A1" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="DNI_A2" HeaderText="DNI_A2" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_A2" HeaderText="NOMBRES_A2" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CARGO_A2" HeaderText="CARGO_A2" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO_A2" HeaderText="CORREO_A2" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="DNI_A3" HeaderText="DNI_A3" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_A3" HeaderText="NOMBRES_A3" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CARGO_A3" HeaderText="CARGO_A3" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO_A3" HeaderText="CORREO_A3" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="DNI_A4" HeaderText="DNI_A4" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_A4" HeaderText="NOMBRES_A4" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CARGO_A4" HeaderText="CARGO_A4" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO_A4" HeaderText="CORREO_A4" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="DNI_A5" HeaderText="DNI_A5" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NOMBRES_A5" HeaderText="NOMBRES_A5" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CARGO_A5" HeaderText="CARGO_A5" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CORREO_A5" HeaderText="CORREO_A5" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="GRADO_ACADEMICO" HeaderText="GRADO_ACADEMICO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_OPTO_DIA" HeaderText="FEC_OPTO_DIA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_OPTO_MES" HeaderText="FEC_OPTO_MES" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_OPTO_ANIO" HeaderText="FEC_OPTO_ANIO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="CODIGO_TEST" HeaderText="CODIGO_TEST" Visible="false" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FACULTAD" HeaderText="FACULTAD" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="ESCUELA" HeaderText="ESCUELA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_EMI_DIA" HeaderText="FEC_EMI_DIA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_EMI_MES" HeaderText="FEC_EMI_MES" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_EMI_ANIO" HeaderText="FEC_EMI_ANIO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="COD_UNI" HeaderText="COD_UNI" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="ABREV_G" HeaderText="ABREV_G" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="OBTENIDO_POR" HeaderText="OBTENIDO_POR" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="MODALIDAD" HeaderText="MODALIDAD" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NUM_RESO" HeaderText="NUM_RESO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FEC_RESO" HeaderText="FEC_RESO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="NRO_DIPLOMA" HeaderText="NRO_DIPLOMA" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="TIPO_EMISION" HeaderText="TIPO_EMISION" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="LIBRO" HeaderText="LIBRO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FOLIO" HeaderText="FOLIO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="REGISTRO" HeaderText="REGISTRO" ItemStyle-Wrap="false" />
                                        <asp:BoundField DataField="FECHA_DUPLICADO" HeaderText="FECHA_DUPLICADO" ItemStyle-Wrap="false" />
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
    </form>

    <script type="text/javascript">
        var controlId = '';
        var tableFisico;
        var tableElectronico;

        /* Ejecutar funciones una vez cargada en su totalidad la página web. */
        $(document).ready(function() {   
            udpFiltrosUpdate();            

            /*Ocultar cargando*/   
            $('.loader').fadeOut("slow");
        });

        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function (sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {                
                case 'btnListar':
                    selTodos = true;
                    AlternarLoading(false, 'Lista');                                         
                    break; 
                
                case 'btnEnviar':
                    AlternarLoading(false, 'Enviar');
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

                case 'btnEnviar':
                    tableElectronico.page.len(10).draw();
                    AlternarLoading(true, 'Enviar');
                    break;
            }
        });    
    </script>    
</body>
</html>
