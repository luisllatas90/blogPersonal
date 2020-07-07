<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmOfertaLaboral.aspx.vb" Inherits="Alumni_frmOfertaLaboral" %>

<!DOCTYPE html>
<html lang="es">

<head>
       <title></title>
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%-- Compatibilidas --%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />    
    
     <!-- Estilos externos -->         
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">            
    <link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css"> 
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">
       
    <%--<link href="css/estilos.css" rel="stylesheet" type="text/css" />--%>
    
    <!-- Estilos propios -->        
    <link rel="stylesheet" href="css/estilos.css?3">
    
     <!-- Scripts externos -->    
    <script src="../assets/jquery/jquery-3.3.1.js"></script>    
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../assets/fileDownload/jquery.fileDownload.js"></script>    
    <script src="js/popper.js"></script>    
    <script src="js/sweetalert/es6-promise.auto.min.js"></script>
    <script src="js/sweetalert/sweetalert2.js"></script>
    <script src="js/datatables/jquery.dataTables.min.js?1"></script>  
    
    <script type="text/javascript">

        //var getCanvas; // global variable
        var selTodos = true;
        var tableEmailEgr;

        $(document).ready(function() {
//            var element = $("#html-content-holder"); // global variable
//            var getCanvas; // global variable

//            html2canvas(element, {
//                onrendered: function(canvas) {
//                    $("#previewImage").append(canvas);
//                    getCanvas = canvas;
//                }
//            });
            $("#btn-Convert-Html2Image").on('click', function() {
                var imgageData = getCanvas.toDataURL("image/png");
                // Now browser starts downloading it instead of just showing it
                var newData = imgageData.replace(/^data:image\/png/, "data:application/octet-stream");
                $("#btn-Convert-Html2Image").attr("download", "your_pic_name.png").attr("href", newData);
            });
        });

//        /// Titulos
//        $(function() {
//            $('[data-toggle="tooltip"]').tooltip()
//        })
//        
        function previoBanner() {
            var element = $("#html-content-holder"); // global variable
            

            html2canvas(element, {
                onrendered: function(canvas) {
                    $("#previewImage").append(canvas);
                    getCanvas = canvas;
                }
            });

        }

        /* Funtion */
       
        /* Dar formato a la grilla. */
        function formatoGrilla() {
        
            // Setup - add a text input to each footer cell
            $('#gvListarOfertas thead tr').clone(true).appendTo('#gvListarOfertas thead');
            $('#gvListarOfertas thead tr:eq(1) th').each(function(i) {
                var title = $(this).text();
                $(this).html('<input type="text" placeholder="Buscar ' + title + '" />');

                $('input', this).on('keyup change', function() {
                    if (table.column(i).search() !== this.value) {
                        table
                            .column(i)
                            .search(this.value)
                            .draw();
                    }
                });
            });
            var table = $('#gvListarOfertas').DataTable({
                orderCellsTop: true,
                "order": [[3, "desc"]]                
                //fixedHeader: true
            });
        }
        function formatoGrillaEgre() {

            // Setup - add a text input to each footer cell
            $('#gvEmailEgr thead tr').clone(true).appendTo('#gvEmailEgr thead');
            $('#gvEmailEgr thead tr:eq(1) th').each(function(i) {
                var title = $(this).text();
                $(this).html('<input type="text" placeholder="Buscar ' + title + '" />');

                $('input', this).on('keyup change', function() {
                    if (tableEmailEgr.column(i).search() !== this.value) {
                        tableEmailEgr
                            .column(i)
                            .search(this.value)
                            .draw();
                    }
                });
            });
            tableEmailEgr = $('#gvEmailEgr').DataTable({
                orderCellsTop: true,                
                //fixedHeader: true
            });
        }
        
        function udpFechaIni() {
            var date_input = $('input[id="txtFechIni"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }
        function udpFechaFin() {
            var date_input = $('input[id="txtFechFin"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }

        function udpFechaIniMod() {
            var date_input = $('input[id="txtFechIniPub"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }
        function udpFechaFinModal() {
            var date_input = $('input[id="txtFechFinPub"]'); //our date input has the name "date"
            var container = $('.bootstrap-iso form').length > 0 ? $('.bootstrap-iso form').parent() : "body";
            var options = {
                format: 'dd/mm/yyyy',
                container: container,
                todayHighlight: true,
                autoclose: true,
                language: 'es'
            };
            date_input.datepicker(options);
        }

        /* Flujo de tabs de la página principal. */
        function flujoTabs(tabActivo) {
            if (tabActivo == 'listado-tab') {
                //HABILITAR  
                estadoTabListado('H');

                //DESHABILITAR
                estadoTabRegistro('D');
                estadoTabEnvio('D'); 

            } else if (tabActivo == 'registro-tab') {
                //HABILITAR             
                estadoTabRegistro('H');

                //DESHABILITAR
                estadoTabListado('D');
                estadoTabEnvio('D');
                
            }else if (tabActivo == 'envio-tab'){
                //HABILITAR             
                estadoTabEnvio('H');

                //DESHABILITAR 
                estadoTabListado('D');
                estadoTabRegistro('D');                
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

        function estadoTabEnvio(estado) {
            if (estado == 'H') {
                $("#envio-tab").removeClass("disabled");
                $("#envio-tab").addClass("active");
                $("#envio").addClass("show");
                $("#envio").addClass("active");
            } else {
                $("#envio-tab").removeClass("active");
                $("#envio-tab").addClass("disabled");
                $("#envio").removeClass("show");
                $("#envio").removeClass("active");
            }
        }
        
        function OpenModal() {
            $('#modalAddCar').modal('show');
        }

        function OpenModalAlta() {
            $('#modalDarAlta').modal('show');
        }
        function closeModalAlta() {
            $('#modalDarAlta').modal('hide');
        }

        function OpenModalBuscaEmp() {
            $('#mdlBuscarEmpresa').modal('show');
        }

        function closeModalBuscaEmp() {
            $('#mdlBuscarEmpresa').modal('hide');
        }
               

        function validaModal(mensaje) {
            var alerta = "<p>"
            var alerta2 = mensaje
            var alerta3 = "</p>"
            var alerta4 = alerta + alerta2 + alerta3
            $("#mensajeModal").attr("class", "alert alert-danger col-sm-12")            
            $("#mensajeModal").html(alerta4)
        }
        function borraValidaModal(mensaje) {
            $("#mensaje").removeAttr("class");
            $("#mensaje").html("");
        }

         function valida(mensaje) {
            var alerta = "<p>"
            var alerta2 = mensaje
            var alerta3 = "</p>"
            var alerta4 = alerta + alerta2 + alerta3
            $("#mensaje").attr("class", "alert alert-danger col-sm-12")
            $("#mensaje").attr("style", "padding:0px;")
            $("#mensaje").html(alerta4)
        }
        function noValida(mensaje) {
            var alerta = "<p>"
            var alerta2 = mensaje
            var alerta3 = "</p>"
            var alerta4 = alerta + alerta2 + alerta3
            $("#mensaje").attr("class", "alert alert-success col-sm-12")
            $("#mensaje").attr("style", "padding:0px;")
            $("#mensaje").html(alerta4)
        }
        function noValidaModAlta(mensaje) {
            var alerta = "<p>"
            var alerta2 = mensaje
            var alerta3 = "</p>"
            var alerta4 = alerta + alerta2 + alerta3
            $("#dimMsjAlta").attr("class", "alert alert-success col-sm-12")
            $("#dimMsjAlta").attr("style", "padding:0px;")
            $("#dimMsjAlta").html(alerta4)
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
                    if (ctl.id == "btnEnviarEmail"){
                        tableEmailEgr.page.len(-1).draw();                                                                        
                    }

                    window.location.href = defaultAction;
                    return true;
                } else {
                    return false;
                }
            }).catch(swal.noop);
        }

        function alerta() {
            alert('Hola');
        }


        /*Seleccionar Egresados para Enviar Mensaje*/
        function marcarTodosEgresados() {
            //asignar todos los controles en array
            /*
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = selTodos;
                }
            }
            */
            var rows = tableEmailEgr.rows({ 'search': 'applied' }).nodes();            
            $('input[type="checkbox"]', rows).prop('checked', selTodos); 

            if (selTodos) {
                selTodos = false;
            } else {
                selTodos = true;
            }
        }
        // fin de marcar todosegresados
        
        
        

        function solonumeros(e) {

            var key;

            if (window.event) // IE
            {
                key = e.keyCode;
            }
            else if (e.which) // Netscape/Firefox/Opera
            {
                key = e.which;
            }

            if (key < 48 || key > 57) {
                return false;
            }

            return true;
        }

        /*Mostrar el boton Seleccionar Todos*/
        function mostrarBotonTodos() {
            $('#sel-todos').removeClass("oculto");
        }
    
    </script>
    
   <style type="text/css">        
       
        .col-operacion {
            width: 210px !important; 
        }
       
    </style> 
    
    
</head>
<body>

<form id="frmListaOferta" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>    
    <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        
    <div class="container-fluid">                                           
        <div class="card div-title">                        
            <div class="row title">OFERTAS LABORALES</div>
        </div> 
        <div id="mensaje" runat="server"></div>                                              
        <ul class="nav nav-tabs" role="tablist">
            <li class="nav-item">
                <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                    aria-controls="listado" aria-selected="true">Listado</a>
            </li>
            <li class="nav-item">
                <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                aria-controls="registro" aria-selected="false">Registro</a>
            </li>
            <li class="nav-item">
                <a href="#envio" id="envio-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                aria-controls="envio" aria-selected="false">Enviar Oferta</a>
            </li>                
        </ul>                        
        <div class="tab-content" id="contentTabs">
            <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">
                <asp:UpdatePanel ID="updListOfertas" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="panel-cabecera">
                            <div class="card">   
                                <div class="card-header">Filtros de Búsqueda</div>                       
                                <div class="card-body">    
                                    <div class="row">                                                    
                                        <label for="txtFechaIni" class="col-sm-1 col-form-label form-control-sm">Inicio:</label>
                                        <div class="input-group col-sm-3">
                                            <asp:TextBox ID="txtFechIni" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"/>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-calendar-alt"></i>
                                                </span>
                                            </div>                                                
                                        </div>
                                        <label for="txtFechaFin" class="col-sm-1 col-form-label form-control-sm">Fin:</label>
                                        <div class="input-group col-sm-3">
                                            <asp:TextBox ID="txtFechFin" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"/>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-calendar-alt"></i>
                                                </span>
                                            </div>                                                
                                        </div>      
                                    </div>
                                    <hr/>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <button type="button" id="btnListarOfertas" runat="server" class="btn btn-accion btn-celeste">
                                                <i class="fa fa-sync-alt"></i>
                                                <span>Listar</span>
                                            </button>
                                            <button type="button" id="btnNuevaOferta" runat="server" class="btn btn-accion btn-azul">
                                                <i class="fa fa-plus-square"></i>
                                                <span>Nuevo</span>
                                            </button>
                                             <div class="col-sm-1">
	                                        <asp:LinkButton ID="lbResolPrueba" runat="server" CssClass="btn btn-info" ToolTip="Solicitar PDF" Visible = "true">
                                                <span><i class="fa fa-file-pdf"></i></span> &nbsp; Resolucion 
						                    </asp:LinkButton>
	                                        </div>
                                                                                                                                            
                                        </div>        
                                    </div>   
                                </div>
                            </div> 
                        </div>   
                        <br/>
                        <div class="table-responsive">
                            <asp:GridView ID="gvListarOfertas" runat="server" ShowHeader="true" CssClass="display table table-sm" AutoGenerateColumns="False" DataKeyNames="codigo_ofe" GridLines="None" >
                                <RowStyle Font-Overline="False" Font-Size="12px" />
                                <Columns>
                                    <asp:BoundField HeaderText="EMPRESA" DataField="nombrePro" />
                                    <asp:BoundField HeaderText="OFERTA" DataField="titulo_ofe" />                                                            
                                    <asp:BoundField HeaderText="TELEFONO" DataField="telefonocontacto_ofe" />
                                    <asp:BoundField HeaderText="FECHA REG" DataField="fechaReg_ofe" />
                                    <asp:BoundField HeaderText="ESTADO" DataField="estado_ofe" />
                                    <asp:TemplateField HeaderText="ACCIONES" ItemStyle-CssClass="col-operacion">                                                                
                                        <ItemTemplate>     
                                        <asp:LinkButton ID="btnEditOfe" runat="server" title="Modificar" data-placement="bottom"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                            CommandName="EditOfe" CssClass="btn btn-primary btn-sm">
                                            <span><i class="fa fa-pen"></i></span>
                                            
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnAddCarrera" runat="server"  title="Agregar Carrera" data-placement="bottom" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                            CommandName="AddCarr" CssClass="btn btn-success btn-sm">
                                            <span><i class="fa fa-plus"></i></span>
                                        </asp:LinkButton>
                                    <asp:LinkButton ID="btnGenBanner" runat="server" title="Descargar Anuncio" data-placement="bottom" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                            CommandName="GenBanner" CssClass="btn btn-info btn-sm">
                                            <span><i class="fa fa-download"></i></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEnvio" runat="server"  title="Enviar Oferta" data-placement="bottom"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                            CommandName="enviaEmail" CssClass="btn btn-primary btn-sm">
                                            <span><i class="fa  fa-envelope"></i></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkAlta" runat="server" title="Estado Oferta" data-placement="bottom" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                            CommandName="darAlta" CssClass="btn btn-success btn-sm">
                                            <span><i class="fa fa-check-square"></i></span>
                                    </asp:LinkButton>                                                                      
                                    </ItemTemplate>                                                                 
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#E33439" Font-Size="12px" ForeColor="White" />
                            </asp:GridView>
                        </div>                                                                                   
                    </ContentTemplate>                                                  
                </asp:UpdatePanel>                                        
            </div>
            <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">
                <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="panel-cabecera">
                            <div class="card">                                        
                                <div class="card-header">
                                    <asp:TextBox ID="txtCodigo_ofe" runat="server" Visible="False"></asp:TextBox>
                                    <div class="row">
                                        <div class="col-sm-10" >
                                            <div>Informacion de la oferta</div>
                                        </div>                                                                                                       
                                    </div>                                        
                                </div>
                                <div class="card-body">
                                    <div class="row">                                                
                                        <label class="col-sm-1 col-form-label form-control-sm">Título: <span class="requerido">(*)</span> </label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtTitulo" runat="server" 
                                                CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>                                                                                    
                                        <label class="col-sm-1 col-form-label form-control-sm">Empresa:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-4">
                                            <asp:HiddenField ID="hf_codigo_emp" runat="server" />
                                            <asp:HiddenField ID="hf_idPro" runat="server" />
                                            <asp:HiddenField ID="hf_estadoOfe" runat="server" />
                                            <asp:TextBox ID="txtEmpresa" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                                        </div>                                                         
                                        <div class="col-sm-1">
                                            <asp:LinkButton ID="lbModOlBuscaEmp" runat="server" CssClass="btn btn-sm btn-success" Text='<i class="fa fa-search"></i>'></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">                                               
                                        <label class="col-sm-1 col-form-label form-control-sm">Descripción:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control form-control-sm" Rows="3" 
                                                            TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                        <label class="col-sm-1 col-form-label form-control-sm">Requisitos:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtRequisitos" runat="server" CssClass="form-control form-control-sm" Rows="3" 
                                            TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>                        
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Dpto:<span class="requerido">(*)</span></label>
                                        <div class="col-md-5">
                                            <asp:DropDownList ID="ddlDepartamento" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-1 col-form-label form-control-sm">Lugar:</label>
                                        <div class="col-md-5">
                                            <asp:TextBox ID="txtLugar" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>                                           
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Tipo:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlTipoOferta" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>                                                                                             
                                        <label class="col-sm-1 col-form-label form-control-sm">Sector:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>            
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Contacto:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtContacto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>                                
                                        <label class="col-sm-1 col-form-label form-control-sm">Teléfono:</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtTelefono" onkeypress="javascript:return solonumeros(event)" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>                   
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Tipo Trabajo:<span class="requerido">(*)</span></label>
                                        <div class="col-sm-5">
                                            <asp:DropDownList ID="ddlTipoTrabajo" runat="server" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-sm-1 col-form-label form-control-sm">Desc Banner:</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtDescBanner" Height="45px" runat="server" CssClass="form-control form-control-sm" rows="2" TextMode="MultiLine"></asp:TextBox>                                                                                                                                         
                                        </div>
                                    </div>
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Postular vía:</label>
                                        <div class="col-sm-5">    
                                            <label class="col-sm-2 col-form-label form-control-sm">Web</label> <asp:RadioButton ID="rbPostWeb" runat="server" GroupName="Elegir" /> 
                                            <label class="col-sm-2 col-form-label form-control-sm">Correo</label> <asp:RadioButton ID="rbPostCorreo" runat="server" GroupName="Elegir"/>                             
                                        </div>                        
                                    </div>                                            
                                    <div class="row">                                                
                                        <label class="col-sm-1 col-form-label form-control-sm">Correo:</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>                                    
                                        </div>
                                        <div class="col-sm-5">                                    
                                            <asp:CheckBox ID="chkMostrarCorreo" runat="server" Text="&nbsp;&nbsp;Mostrar Correo" 
                                                CssClass="form-control-sm"  Font-Size="12px" Font-Names="Arial" /> 
                                        </div>
                                    </div>                                              
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm">Web:</label>
                                        <div class="col-sm-5">
                                            <asp:TextBox ID="txtWebOfe" runat="server" CssClass="form-control form-control-sm" Placeholder="http://www.miempresa.com"></asp:TextBox>                                    
                                        </div>                                                                         
                                    </div>                                           
                                    <div class="row">                                                
                                        <label class="col-sm-1 col-form-label form-control-sm">Publicación: Inicio<span class="requerido">(*)</span></label>                                                                                         
                                        <div class="input-group col-sm-2">
                                            <asp:TextBox ID="txtFechIniPub" runat="server" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY"/>
                                                <div class="input-group-prepend">
                                                    <span class="input-group-text">
                                                        <i class="fa fa-calendar-alt"></i>
                                                    </span>
                                                </div>      
                                        </div>                                                                                                
                                        <label class="col-sm-1 col-form-label form-control-sm">Fin<span class="requerido">(*)</span></label>                                                                                                                                                  
                                        <div class="input-group col-sm-2">
                                            <asp:TextBox ID="txtFechFinPub" runat="server" CssClass="form-control form-control-sm" placeholder="DD/MM/YYYY"/>
                                            <div class="input-group-prepend">
                                                <span class="input-group-text">
                                                    <i class="fa fa-calendar-alt"></i>
                                                </span>
                                            </div>    
                                        </div>
                                    </div>                   
                                </div>                                            
                                <div class="card-footer">                                                                                           
                                    <asp:LinkButton ID="btnGuardarOferta" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información de la empresa?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Guardar</span>
                                    </asp:LinkButton>                                              
                                        <asp:LinkButton ID="lbRetorno" runat="server" CssClass="btn btn-accion btn-rojo">
                                        <i class="fa fa-sign-out-alt"></i>
                                        <span class="text">Salir</span>
                                        </asp:LinkButton>                                                                                          
                                </div>
                            </div>
                        </div>                                       
                    </ContentTemplate>                                                  
                </asp:UpdatePanel>                                       
            </div>
            <div class="tab-pane" id="envio" role="tabpanel" aria-labelledby="registro-tab">
                <asp:UpdatePanel ID="udpEnviarMail" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="card">
                            <div class="card-header">ENVIO DE OFERTA</div>
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate> 
                                    <div class="panel-cabecera">
                                        <div class="card-body">
                                            <asp:TextBox ID="txtCodigo_ofeMod" runat="server" Visible="False"></asp:TextBox>
                                            <div class="row">                                                
                                                <label class="col-sm-2 col-form-label form-control-sm">TITULO DE LA OFERTA:</label>
                                                <div class="col-sm-10">
                                                    <asp:TextBox ID="txtTitOfeEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div> 
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-2 col-form-label form-control-sm">PUBLICACION:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtFechIniPubEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div> 
                                                <label class="col-sm-2 col-form-label form-control-sm">FIN DE LA PUBLICACION:</label>
                                                <div class="col-sm-2">
                                                    <asp:TextBox ID="txtFechFinPubEmail" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <label class="col-sm-2 col-form-label form-control-sm">CARRRERA PROFESIONAL:</label>
                                                <div class="col-sm-4">
                                                    <asp:DropDownList ID="ddlCarrProfEmail" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="btnListarEgre" runat="server" class="btn btn-accion btn-celeste">
                                                        <i class="fa fa-sync-alt"></i>
                                                        <span class="text">Listar</span>
                                                    </asp:LinkButton>                                              
                                                    <asp:LinkButton ID="btnEnviarEmail" runat="server" class="btn btn-accion btn-azul"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea enviar un mensaje a los egresados seleccionados?', 'warning');">
                                                        <i class="fa fa-envelope-square"></i>
                                                        <span class="text">Enviar Mensaje</span>
                                                    </asp:LinkButton>         
                                                </div>
                                                <div class="col-sm-2" >
                                                    <asp:LinkButton ID="btnSalirListEgre" runat="server" class="btn btn-accion btn-rojo">
                                                        <i class="fa fa-sign-out-alt"></i>
                                                        <span class="text">Salir</span>
                                                    </asp:LinkButton> 
                                                </div>
                                            </div>                                                
                                            <div class="row-sm-12">
                                                <div id="sel-todos" class="oculto">
                                                    <asp:LinkButton ID="btnElegir" runat="server" CssClass="btn btn-accion btn-naranja"
                                                        OnClientClick="marcarTodosEgresados();">
                                                        <i class="fa fa-check-square"></i>
                                                        <span class="text">Todos</span>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row col-sm-12">
                                                <table class="table-responsive" >
                                                    <asp:GridView ID="gvEmailEgr" runat="server" AutoGenerateColumns="false" GridLines="None" ShowHeader="true"
                                                    CssClass="display table table-sm" DataKeyNames="codigo_ega, codigo_Pso, correo_personal, correo_profesional">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SEL.">
                                                                <ItemTemplate>                
                                                                    <asp:CheckBox ID="chkElegir" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>                                 
                                                            <asp:BoundField DataField="apellidos" HeaderText="APELLIDOS" />
                                                            <asp:BoundField DataField="nombres" HeaderText="NOMBRES" />
                                                            <asp:BoundField DataField="CicloEgre" HeaderText="CICLO EGRESO" />
                                                            <asp:BoundField HeaderText="Email Personal" DataField="correo_personal" />
                                                            <asp:BoundField HeaderText="Email Profesional" DataField="correo_profesional" />
                                                        </Columns>                                                
                                                    </asp:GridView>
                                                </table>                                                    
                                            </div> 
                                        </div>                                        
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>       
                        </div>
                    </ContentTemplate>                                                  
                </asp:UpdatePanel>   
            </div>
        </div>          
    </div>
        
    <%--modal--%>
    <div id="modalAddCar" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="modalFinalizaBody">
                <asp:UpdatePanel ID="udpModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Agregar Carrera Profesional</span> 
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
                                <label class="col-sm-3 col-form-label form-control-sm" for="lblCodigo">CARRERA PROFESIONAL:</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlCarrrera" runat="server" class="form-control" AutoPostBack="true" >
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:LinkButton ID="lbAddCarr" runat="server" CssClass="btn btn-accion btn-verde"
                                        OnClientClick="return alertConfirm(this, event, '¿Desea registrar la información de la empresa?', 'warning');">
                                        <i class="fa fa-save"></i>
                                        <span class="text">Agregar</span>
                                    </asp:LinkButton>  
                                </div>
                            </div>
                            <div class="row">                                
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvCarreras" runat="server" AutoGenerateColumns=false CssClass="table table-striped table-bordered table-hover" DataKeyNames="codigo_cpf" > 
                                        <RowStyle Font-Size="12px" />
                                        <Columns>
                                            <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL"/>
                                            <asp:TemplateField HeaderText="ACCION" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CommandName="DeleteCarr" CssClass="btn btn-danger btn-sm">
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

    <%--modal Alta--%>
    <div id="modalDarAlta" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
		    <div class="modal-content" id="divModalAlta">
		        <asp:UpdatePanel ID="udpModalAlta" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                            <span class="modal-title">Estado de Oferta Laboral</span>                     
                        </div>
                        <div class="modal-body">
                            <div class="panel-cabecera">
                                <div class="card-body">
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:TextBox ID="txtCodOfeAlta" runat="server" Visible="false"></asp:TextBox>	
                                    <div class="row">
                                        <div id="dimMsjAlta" runat="server"></div>
                                    </div>
                                    <div class="row"> 
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">OFERTA:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtOfeLabAlta" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>                        
                                    </div>
                                    <div class="row"> 
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">DESCRIPCIÓN:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtDescOfeAlta" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>                        
                                    </div>
                                    <div class="row"> 
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">REQUISITOS:</label>
                                        <div class="col-sm-10">
                                            <asp:TextBox ID="txtReqOfeAlta" runat="server" CssClass="form-control" ReadOnly="true" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                        </div>                        
                                    </div>
                                    <div class="row"> 
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblCodigo">ESTADO:</label>
                                        <div class="col-sm-10">
                                            <asp:DropDownList ID="ddListEstado" runat="server" class="form-control" AutoPostBack="true" ></asp:DropDownList>
                                        </div>                        
                                    </div>
                                </div>	
                            </div>
                        </div>            
                        <div class="modal-footer">
                            <asp:LinkButton ID="btnXModalAlta" runat="server" class="btn btn-accion btn-rojo"><i class="fa fa-sign-out-alt"></i><span class="text">Salir</span></asp:LinkButton>    
                            <asp:LinkButton ID="btnGuardarAltaOfe" runat="server" class="btn btn-accion btn-verde"> <i class="fa fa-save"></i><span class="text">Guardar</span></asp:LinkButton>  
                        </div>	        	
                    </ContentTemplate>
		        </asp:UpdatePanel>  		
            </div> 
        </div>        
    </div>

    <%--modal busca empresa--%>
    <div id="mdlBuscarEmpresa" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">           
                    <span class="modal-title">EMPRESAS</span>             
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>                                                
                </div>   
                <div class="modal-body">
                    <div class="container-fluid">   
                        <div class="panel-cabecera">
                            <asp:UpdatePanel ID="udpFiltrosEmpresa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <div class="card">
                                        <div class="card-header">Filtros de Búsqueda</div>
                                        <div class="card-body">
                                            <div class="row">
                                                <label for="txtNombreComercialFiltro" class="col-sm-2 form-control-sm">Nombre / RUC:</label>
                                                <div class="col-sm-5">                                                                                
                                                    <asp:TextBox ID="txtNombreComercialFiltro" runat="server" CssClass="form-control form-control-sm uppercase" AutoComplete="off"/>
                                                </div>  
                                            </div>
                                            <hr/>
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:LinkButton ID="btnListarEmpresa" runat="server" CssClass="btn btn-accion btn-celeste">
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
                        <br/>
                        <div class="table-responsive">
                            <div id="loading-listaEmpresa" class="loading oculto">
                                <img src="img/loading.gif">
                            </div>                 
                            <asp:UpdatePanel ID="udpListaEmpresa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="grwListaEmpresa" runat="server" AutoGenerateColumns="false" ShowHeader="true" DataKeyNames="codigo_emp, nombreComercial_emp"
                                        CssClass="display table table-sm" GridLines="None">
                                        <Columns>                                    
                                            <asp:BoundField DataField="nombreComercial_emp" HeaderText="EMPRESA" ItemStyle-Width="80%" ItemStyle-Wrap="false"/>
                                            <asp:TemplateField HeaderText="SEL." ItemStyle-Width="20%" ItemStyle-Wrap="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnSeleccionar" runat="server" 
                                                        CommandName="Seleccionar" 
                                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                        CssClass="btn btn-primary btn-sm" 
                                                        ToolTip="Seleccionar empresa"
                                                        OnClientClick="return alertConfirm(this, event, '¿Desea enviar la empresa seleccionada?', 'warning');">
                                                        <span><i class="fa fa-check-circle"></i></span>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField> 
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel> 
                        </div>                                                   
                        <br/>                                                                             
                    </div>
                </div>                 
            </div>                
        </div>
    </div>                                       
</form>        
</body>
</html>
