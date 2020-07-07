<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistrarEgresados.aspx.vb" Inherits="Alumni_frmRegistrarEgresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <link rel="stylesheet" href="css/sweetalert/sweetalert2.min.css">
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
               
    <%--<link rel="stylesheet" href="css/datatables/jquery.dataTables.min.css">    --%>
    
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
    
    <%--<script src="js/datatables/jquery.dataTables.min.js?1"></script>        --%>
    
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
    

    <script type="text/javascript">

        function validaReq(numero) {
            $("#mensaje").removeAttr("class");
            $("#mensaje").html("");
            
            var mensaje = "<p>EL ALUMNO NO CUMPLE CON "
            var mensaje2 = numero
            var mensaje3 = " REQUISITOS</p>"
            var mensaje4 = mensaje + mensaje2 + mensaje3
            $("#mensaje").attr("class", "alert alert-danger col-sm-12")            
            $("#mensaje").html(mensaje4)
        }
        function NoValidaReq() {
            $("#mensaje").removeAttr("class");
            $("#mensaje").html("");
            $("#mensaje").attr("class", "alert alert-success col-sm-12")
            $("#mensaje").html("<p> EL ALUMNO CUMPLE CON TODOS LOS REQUISITOS</>")
        }
        function EgresoExito() {
            $("#mensaje").removeAttr("class");
            $("#mensaje").html("");
            $("#mensaje").attr("class", "alert alert-success col-sm-12")
            $("#mensaje").html("<p> SE DIÓ EGRESO AL ALUMNO </>")
        }

        function openModal() {
            $('#myModal').modal('show');
        };
        
        function closeModal() {
            $('#myModal').modal('hide');
        };
        /* Dar formato a la grilla. */
//        function formatoGrilla() {
//            // Setup - add a text input to each footer cell
//            $('#gvAlumnosReq thead tr').clone(true).appendTo('#gvAlumnosReq thead');
//            $('#gvAlumnosReq thead tr:eq(1) th').each(function(i) {
//                var title = $(this).text();
//                $(this).html('<input type="text" placeholder="Buscar ' + title + '" />');

//                $('input', this).on('keyup change', function() {
//                    if (table.column(i).search() !== this.value) {
//                        table
//                            .column(i)
//                            .search(this.value)
//                            .draw();
//                    }
//                });
//            });
//            var table = $('#gvAlumnosReq').DataTable({
//                orderCellsTop: true
//                //fixedHeader: true
//            });
//        }

        function formatoGrilla() {
            
            $('#<%= gvAlumnosReq.ClientID %>').DataTable({
            pageLength : 100,
            language: {
                "decimal": "",
                "emptyTable": "No hay información",
                "info": "Mostrando _START_ a _END_ de _TOTAL_ Registros",
                "infoEmpty": "Mostrando 0 to 0 of 0 Registros",
                "infoFiltered": "(Filtrado de _MAX_ total Registro)",
                "infoPostFix": "",
                "thousands": ",",
                "lengthMenu": "Mostrar _MENU_ Registros",
                "loadingRecords": "Cargando...",
                "processing": "Procesando...",
                "search": "Buscar:",
                "zeroRecords": "Sin resultados encontrados",
                "paginate": {
                    "first": "Primero",
                    "last": "Ultimo",
                    "next": "Siguiente",
                    "previous": "Anterior"
                }
            }             
            });          
        } 

        
        function MarcarCursos(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName("input");
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        } /* fin de la funcion */

        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        } /* fin de la funcion */
        
         /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } /* fin de la funcion */
        
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
        // funcion para la espera de la carga de la página
        function AlternarLoading(retorno, elemento) {
            var $loadingGif ;
            var $elemento ;

            switch (elemento) { 
                case 'Lista':
                    $loadingGif = $('#loading-lista')
//                    $elemento = $('#udpListado');
                    break;
//                case 'Registro':
//                    $loadingGif = $('#loading-registro')                    
//                    break;     
//                case 'BuscarRUC':
//                    $loadingGif = $('#loading-buscarRUC')                    
//                    break; 
                case 'ListaRefresh':
                    $loadingGif = $('#loading-lista')
                    //$elemento = $('#udpListado');
                    break;                 
            }
            
            if ($loadingGif != undefined) {
                if (!retorno) {
                    $loadingGif.fadeIn(150);  
                    if ($elemento != undefined) {
                        $elemento.addClass("oculto");
                    }                     
                } else {
                    $loadingGif.fadeOut(150);
                    if ($elemento != undefined) {
                        $elemento.removeClass("oculto");
                    }                    
                }
            }          
        } /*fin de la funcion*/      
          
                        
    </script>
    
    <style type="text/css">        
         .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
            
        }
        .panel-heading
        {  
        	 padding:3px;
        }
        .panel-body
        {
        	margin-top: 0px;
        	padding-top: 0px;         	
        	}
        .form-control
        {
            border-color: #d9d9d9;
            height: 30px; /*font-weight: 300;  line-height: 40px; */
            color: black;
            font-size:0.8rem; 
        }
        
        label.form-control-sm {
            font-size: 0.75rem;
            font-weight: 500;
            text-align: left;
            line-height: 1.3; 
        }
        
        .loading {
            text-align:center;    
        }
        .loading img {
            vertical-align : middle;
            width: 50px; 
            height: 50px;
        }

        .oculto {
            display: none;
            width: 100%;
        }
                
        /*INICIO TABLA
        .dataTable {
            width: 100% !important;
        }

        thead input {
            width: 100%;
        }

        .table.table-sm {
            font-size: 0.7em;
            font-family: Verdana, Geneva, Tahoma, sans-serif;      
        }

        .table td {
            vertical-align: middle; 
            text-align: left;  
        }

        .table th {
            vertical-align: middle; 
            text-align: center;  
        }

        .table .sorting_asc, .table .sorting, .table .sorting_desc{
            background-color: #E33439;
            text-align: center; 
            color: white;
        }

        .col-operacion {
            width: 45px !important; 
        }

        .dataTables_length, .dataTables_filter, .dataTables_info, .dataTables_paginate {  
            font-size: 0.7rem;    
            font-family: Verdana, Geneva, Tahoma, sans-serif;   
        }
       FIN TABLA*/ 
        
        
    </style>  
    
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">   
		<div class="card div-title">                     
            <div class="row title">REGISTRAR EGRESADOS</div>
        </div> 	
		<ul class="nav nav-tabs" role="tablist">
			<li class="nav-item">
				<a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
					aria-controls="listado" aria-selected="true">Listado</a>
			</li>         
	    </ul>  
        <div class="card">
            <div class="card-header">Filtros de Búsqueda</div>
            <div class="card-body">
                <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
                <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                <ContentTemplate>
					
				<div class="row">
					<label class="col-sm-2 col-form-label form-control-sm" for="txtCarrProf">Carrera Profesional:</label>
					<div class="col-sm-4">
						<asp:DropDownList ID="ddlCarrrera" runat="server" class="form-control" AutoPostBack="true" ></asp:DropDownList>
					</div>                
					<label class="col-sm-2 col-form-label form-control-sm" for="txtPlanEst">Plan de Estudio:</label>					
					<div class="col-sm-4">
						<asp:DropDownList ID="ddlPlanEst" runat="server" class="form-control"></asp:DropDownList>
					</div>
				</div>
				<div class="row">
					<label class="col-sm-2 col-form-label form-control-sm" for="txtAlumCod">&nbsp;&nbsp;Alumno / Código:</label>
					<div class="col-sm-4">
						<asp:TextBox runat="server" name="txtAlumno" ID="txtAlumno" class="form-control"></asp:TextBox>
					</div>
					<label class="col-sm-5 col-form-label form-control-sm" for="txtSemestre" style="text-align:left"></label>                                                      
				</div>
				<hr/>
				<div class="row">
					<div class="col-sm-2">
						<asp:LinkButton ID="lbBusca" runat="server" class="btn btn-accion btn-celeste">   
							<i class="fa fa-sync-alt"></i>
							<span>Listar</span>
						</asp:LinkButton>
					</div> 
				 </div>                                                                     
				 <hr />      
				 <div class="row">
					<label class="col-sm-3 col-form-label form-control-sm" for="txtSemestre" style="color:Red; text-align:left">
					&nbsp;&nbsp;Asignar Semestre de Egreso:</label>
					<label class="col-sm-9 col-form-label form-control-sm" for="txtSemestre" style="color:Red; text-align:left">
					&nbsp;&nbsp;Cronograma:</label>  
					<div class="col-sm-3">
						<asp:DropDownList ID="ddlSemestre1" runat="server" class="form-control" AutoPostBack="true" >                                    
						</asp:DropDownList>
					</div>
					 <asp:Label ID="lblAsigna" runat="server" Text="" CssClass="col-sm-3" style="font-size:small"></asp:Label>
					 <div class="col-sm-2">
						<asp:LinkButton ID="cmdGuardar" runat="server" Text="GUARDAR" class="btn btn-accion btn-verde" Visible="false">   
							<i class="fa fa-save"></i>
							<span class="text">Guardar</span>
						</asp:LinkButton>   
						
					</div> 
					
				 </div>                                   
				 <hr />     
				 <div id="loading-lista" class="loading oculto">
					<img src="img/loading.gif">
				</div>
			</ContentTemplate>
            </asp:UpdatePanel>    
                <asp:UpdatePanel ID="udpListado" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-12">
                                <asp:GridView ID="gvAlumnosReq" runat="server" OnRowDataBound="gvAlumnosReq_RowDataBound" CssClass="table table-striped table-bordered" 
                                    AutoGenerateColumns="False" DataKeyNames="codigo_alu"  >
                                    <RowStyle Font-Size="12px" /> 
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <%--<asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
                                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick="MarcarCursos(this)"/>--%>
                                                    Sel.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkElegir" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>  
                                            <asp:BoundField DataField="codigo_alu" HeaderText="COD" Visible="false" />
                                            <asp:BoundField DataField="codigoUniver_alu" HeaderText="CODIGO" />
                                            <asp:BoundField DataField="alumno" HeaderText="APELLIDOS NOMBRES" />
                                            <asp:BoundField DataField="cicloIng_alu" HeaderText="SEM. INGRESO" />
                                            <asp:BoundField DataField="estadoActual_alu" HeaderText="ESTADO" />
                                            <asp:BoundField DataField="CCreditosAprobados_alu" HeaderText="CRD. APROB" />
                                            <asp:BoundField DataField="DebeTesis" HeaderText="TESIS" />                                                                                                                                   
                                            <asp:BoundField DataField="totalCredElecObl_Pes" HeaderText="CRED. OBLIG. ELECTIVO" />
                                            <asp:BoundField DataField="totalCreObl_Pes" HeaderText="CRED. PLAN ESTUDIO" />
                                            <asp:BoundField DataField="descripcion_Cac" HeaderText="ULT. MAT." />
                                            <asp:TemplateField HeaderText="REQUISITOS" ItemStyle-CssClass="col-operacion">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" 
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                    CommandName="Editar" CssClass="btn btn-primary btn-sm">
                                                    <span><i class="fa fa-search"></i></span>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                                <ItemStyle CssClass="col-operacion" />
                                            </asp:TemplateField>
                                        </Columns>                                            
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                            
                                </asp:GridView> 
                            </div>                                                   
                        </div>  
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
     </div>
     <!-- Modal Registro de Tipo Requisito de Egreso -->
     <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	<div class="modal-dialog modal-lg">
		<div class="modal-content" id="modalFinalizaBody">
		<asp:UpdatePanel ID="udpModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
			<div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				<span class="modal-title">Registrar Egreso</span> 
                    <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
            </div>
            <div class="modal-body">
					<div class="row">
						<div>                        
						    <asp:HiddenField ID="HdCondicion" runat="server" />
							<asp:HiddenField ID="hdCodigo_alu" runat="server" />
                            <asp:HiddenField ID="hdCodigo_req" runat="server" />
                            <asp:HiddenField ID="hdCodigo_pcur" runat="server" />
                            <asp:HiddenField ID="hdCodigo_tip" runat="server" />
                            <asp:HiddenField ID="hdCodigo_are" runat="server" />
                         </div>                        
                    </div>
                    
                    <div class="row" >
                        <div id="mensaje" runat="server">
                         </div>
                    </div>
                    <br />
                    
                    <div class="row">                         
                        <%--<div class="col-sm-12">
							<asp:TextBox runat="server" name="txtCondicion" ID="txtCondicion" class="form-control"></asp:TextBox>
                        </div>--%>                        
						<label class="col-sm-1 col-form-label form-control-sm" for="lblCodigo">
                        CODIGO:</label>
                        <div class="col-sm-3">
							<asp:TextBox runat="server" name="txtCodUniAlum" ID="txtCodUniAlum" class="form-control"></asp:TextBox>
                        </div>
                        <label class="col-sm-1 col-form-label form-control-sm" for="lblNombres">
                        NOMBRES:</label>
                        <div class="col-sm-7">
							<asp:TextBox runat="server" name="txtNombres" ID="txtNombres" class="form-control"></asp:TextBox>
                        </div>
                        
                    </div>
                    <div class="row">                                
						<div class="col-sm-12">
								<asp:GridView ID="gvRequisitos" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_req,codigo_pcur,codigo_tip, codigo_are, cumplio" CssClass="table table-striped table-bordered table-hover"> 
									<RowStyle Font-Size="12px" />
									<Columns>									    
										<asp:BoundField DataField="cantidad" HeaderText="CANT."/>
                                        <asp:BoundField DataField="nombre" HeaderText="REQUISITOS DE EGRESO"/>                                        
                                        <asp:TemplateField>
                                            <HeaderTemplate>                                                
                                                CUMPLIÓ
                                            </HeaderTemplate>
                                            <ItemTemplate>  
                                                <asp:CheckBox ID="chkCumplir" runat="server" Checked='<%# Eval("cumplio") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="observacion" HeaderText="OBSERVACION" Visible="False" />                                        
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px"                                                     
                                    ForeColor="White" />
                                </asp:GridView>
                        </div>                      
                    </div>
				
            </div>
			<div class="modal-footer">
			       <%--<asp:LinkButton ID="lbSalir" runat="server" Text="Cancelar/Regresar" class="btn btn-danger" />--%>
                    <button type="button" id="btnsalir" class="btn btn-accion btn-rojo" data-dismiss="modal">
                    <i class="fa fa-sign-out-alt"></i>
                    Salir
                    </button>
                    <%--<asp:LinkButton ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" />--%>
            </div>	
            </ContentTemplate>
		</asp:UpdatePanel>  		
        </div> 
    </div>        
</div>
</form>
    <script type="text/javascript">
        var controlId = '';

        /* Mostrar y ocultar gif al realizar un procesamiento. */
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id;

            switch (controlId) {
                case 'lbBusca':
                    AlternarLoading(false, 'Lista');
                    break;
                case 'cmdGuardar':
                    AlternarLoading(false, 'ListaRefresh');
                    break;
            }
        });

       
    </script>

    
    
    
</body>
</html>
