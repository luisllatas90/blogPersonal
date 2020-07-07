<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCambioAmbiente.aspx.vb" Inherits="academico_horarios_frmCambioAmbiente" %>

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

    <!--Boopstrap-->    
    <link href="../../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Iconos -->    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css"/> 
    
    <!--Alertas css--> 
    <link href="../../Alumni/css/sweetalert/animate.css" rel="stylesheet" type="text/css" />
    <link href="../../Alumni/css/sweetalert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    
    
    
    
    <!--Jquery-->
    <script src="../../assets/jquery/jquery-3.3.1.js" type="text/javascript"></script>
    <%-- <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <!--Modal -->
    <script src="../../assets/bootstrap-4.1/js/bootstrap.bundle.js" type="text/javascript"></script>
    
    
    <!-- Datatable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
       
    <!--Alertas js-->    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script>
    
    
    
    <script type="text/javascript">
        
        /* Abrir modal*/
        function openModal() {
            $('#myModalAmbiente').modal('show');
        };
        
        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } /* fin de la funcion */
        
      /* Datatable */      
        function formatoGrilla() {
            
            $('#<%= gvListaCursosProg.ClientID %>').DataTable({
            pageLength : 10,
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
       /*---------------------------------------------------*/
       
       
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

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
         <div class="container-fluid">
             <div class="panel-cabecera">
                 <div class="card">
                    <div class="card-header">CAMBIO DE AMBIENTE</div>
                    <div class="card-body">
                        <ul class="nav nav-tabs" role="tablist">
                            <li class="nav-item">
                                <a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
                                aria-controls="listado" aria-selected="true">Cursos Programados</a>
                            </li>
                            <li class="nav-item">
                                <a href="#registro" id="registro-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                                aria-controls="registro" aria-selected="false">Horarios y Ambientes</a>
                             </li>
                             <li class="nav-item">
                                <a href="#envio" id="envio-tab" class="nav-link disabled" data-toggle="tab" role="tab"
                                aria-controls="envio" aria-selected="false">Cambiar Ambiente</a>
                             </li>                
                         </ul>                         
                         <div class="tab-content" id="contentTabs">
                            <div class="tab-pane show active" id="listado" role="tabpanel" aria-labelledby="listado-tab">                                                                
                                <div class="panel-cabecera">
                                    <div class="card">  
                                        <div class="card-body">
                                              <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>
                                                         <div class="row">
	                                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblSemAca">Semestre. Académido:</label>
	                                                        <div class="col-sm-2">
	                                                            <asp:DropDownList ID="ddlSemAca" runat="server" class="form-control" AutoPostBack="true" ></asp:DropDownList>
	                                                        </div> 
	                                                         <label class="col-sm-2 col-form-label form-control-sm" for="lblCarProf">Carrera Profesional:</label>
	                                                        <div class="col-sm-5">
	                                                            <asp:DropDownList ID="ddlCarProf" runat="server" class="form-control" AutoPostBack="true" ></asp:DropDownList>
	                                                        </div>
	                                                    </div>
	                                                    <br />
                                                        <div class="row">   
	                                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblSemAca">Curso</label>
	                                                        <div class="col-sm-4">
	                                                            <asp:TextBox ID="txtCursoList" runat="server" CssClass="form-control"></asp:TextBox>
	                                                        </div>   
	                                                         <div class="col-sm-2">
	                                                            <asp:LinkButton ID="lbBusca" runat="server" CssClass="btn btn-info">
                                                                    <span><i class="fa fa-search"></i></span> BUSCAR
						                                         </asp:LinkButton>
	                                                        </div>       
                                                        </div>
                                                        <hr />                                                      
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>                                                
                                                <asp:UpdatePanel ID="udpListado" runat="server" UpdateMode="Conditional" >
                                                    <ContentTemplate>                                                       
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                  <asp:GridView ID="gvListaCursosProg" runat="server" CssClass="table table-striped table-bordered" 
                                                                  
                                                                     AutoGenerateColumns="False" DataKeyNames="codigo_cup,codigo_cac, nombre_Cur,grupoHor_cup">
                                                                    <RowStyle Font-Size="12px" /> 
                                                                    <Columns>                                                                                                                             
                                                                        <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" />   
                                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="PLAN ESTUDIO" />
                                                                        <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" />
                                                                        <asp:BoundField DataField="grupoHor_cup" HeaderText="GRUPO" /> 
                                                                        <asp:BoundField DataField="nroMatriculados_cup" HeaderText="MATRIC." />   
                                                                        
                                                                        <asp:TemplateField HeaderText="VER">            
			                                                                <ItemTemplate>
                                                                                <asp:LinkButton ID="btnSelCambHor" ToolTip="Ver Ambientes" runat="server" 
					                                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                                            CommandName="selCambHor" CssClass="btn btn-primary btn-sm">
                                                                                <span><i class="fa fa-binoculars"></i></span>
						                                                        </asp:LinkButton>
                                                                            </ItemTemplate>                                            
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
                            </div>                            
                            <div class="tab-pane" id="registro" role="tabpanel" aria-labelledby="registro-tab">
                                <asp:UpdatePanel ID="udpRegistro" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="panel-cabecera">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <asp:HiddenField ID="hfCodCupTabReg" runat="server" />
                                                         <label class="col-sm-1 col-form-label form-control-sm">&nbsp;&nbsp; Curso:</label>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtCursoTabReg" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Grupo:</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtGrupTabReg" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Ciclo Académico:</label>
                                                        <div class="col-sm-2">
                                                            <asp:TextBox ID="txtCacTabReg" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>     
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <asp:GridView ID="gvListaAmbientesCursos" runat="server" CssClass="table table-striped table-bordered" DataKeyNames="codigo_lho,codigo_Cup, descripcionReal_amb,capacidad_amb, dia_lho,nombre_Hor,horaFin_Lho,nroMatriculados_cup"
                                                                    AutoGenerateColumns="False">
                                                                    <RowStyle Font-Size="12px" /> 
                                                                    <Columns>
                                                                        <asp:BoundField DataField="codigo_lho" HeaderText="CODIGO"/>
                                                                        <asp:BoundField DataField="codigo_Amb" HeaderText="CODAMB"/> 
                                                                        <asp:BoundField DataField="descripcion_amb" HeaderText="AMBIENTE ACTUAL"/>
                                                                        <asp:BoundField DataField="descripcionReal_amb" HeaderText="UBIC." />
                                                                        <asp:BoundField DataField="capacidad_amb" HeaderText="CAP." />                                                                     
                                                                        <asp:BoundField DataField="dia_lho" HeaderText="DIA" />
                                                                        <asp:BoundField DataField="nombre_Hor" HeaderText="H.INI" />
                                                                        <asp:BoundField DataField="horaFin_Lho" HeaderText="H.FIN" />
                                                                        <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" />   
                                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="PLAN ESTUDIO" />
                                                                        <asp:BoundField DataField="nroMatriculados_cup" HeaderText="MATRIC." />
                                                                        <asp:BoundField DataField="" HeaderText="CLHON" />
                                                                        <asp:BoundField DataField="" HeaderText="CAMBN" />                                                                                                                                                
                                                                        <asp:BoundField DataField="" HeaderText="NUEVO AMBIENTE" />
                                                                        <asp:BoundField DataField="" HeaderText="CAP." />
                                                                        <asp:TemplateField HeaderText="CAMBIAR">            
			                                                                <ItemTemplate>
                                                                                <asp:LinkButton ID="lbSelCambHor" ToolTip="Cambiar Ambiente" runat="server" 
					                                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                                            CommandName="selCambHor" CssClass="btn btn-primary btn-sm">
                                                                                <span><i class="fa fa-retweet"></i></span>
						                                                        </asp:LinkButton>
                                                                            </ItemTemplate>                                            
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                                                </asp:GridView>     
                                                        </div>   
                                                    </div>
                                                    <hr />
                                                   <div class="row">
                                                        <div class="col-sm-1">
                                                             <asp:LinkButton ID="lbRetornoListaAmb" runat="server" CssClass="btn btn-danger">
                                                                <i class="fa fa-chevron-left"></i>
                                                                <span class="text">Regresar</span>
                                                            </asp:LinkButton>   
                                                        </div>
                                                       
                                                        <div class="col-sm-2">
                                                             <asp:LinkButton ID="lbGuardar" runat="server" CssClass="btn btn-success">
                                                                <i class="fa fa-save"></i>
                                                                <span class="text">Guardar</span>
                                                            </asp:LinkButton>   
                                                        </div>                                                   
                                                   </div>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="tab-pane" id="envio" role="tabpanel" aria-labelledby="envio-tab">
                                  <asp:UpdatePanel ID="udpEnvio" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <div class="panel-cabecera">
                                            <div class="card">
                                                <div class="card-body">
                                                    <div class="row">
                                                        <asp:TextBox ID="txtCod_lhoTabEnv" runat="server" CssClass="form-control" ReadOnly="true" Visible="false" ></asp:TextBox>
                                                        <asp:HiddenField ID="hfCod_lhoTabEnv" runat="server" />
                                                    </div>
                                                     <div class="row">
                                                         <label class="col-sm-1 col-form-label form-control-sm">Curso:</label>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtCurso_tabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Ambiente:</label>
                                                        <div class="col-sm-5">
                                                            <asp:TextBox ID="txtAmbiente_tabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>     
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <label class="col-sm-1 col-form-label form-control-sm">Capacidad:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtCapacTabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Dia:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtDiaTabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Hora Ini:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtHorIniTabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>
                                                        <label class="col-sm-1 col-form-label form-control-sm">Hora Fin:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtHorFinTabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>                                                      
                                                        <label class="col-sm-1 col-form-label form-control-sm">Matriculados:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtMatTabEnv" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                                        </div>                                                                                                                                                                                          
                                                    </div>
                                                    <br />
                                                    <div class="row">
                                                        <label class="col-sm-2 col-form-label form-control-sm">Capacidad Requerida:</label>
                                                        <div class="col-sm-1">
                                                            <asp:TextBox ID="txtCapReqTabEnv" onkeypress="javascript:return solonumeros(event)" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2">
	                                                        <asp:LinkButton ID="lbBuscaAmb" runat="server" CssClass="btn btn-info">
                                                                <span><i class="fa fa-search"></i></span> BUSCAR
						                                    </asp:LinkButton>
	                                                    </div>     
                                                    </div>                                                    
                                                    <hr />
                                                    <asp:UpdatePanel ID="udpListaAmbientes" runat="server" UpdateMode="Conditional" >
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <asp:GridView ID="gvAmbParaCambiar" runat="server" CssClass="table table-striped table-bordered" 
                                                                    AutoGenerateColumns="False" DataKeyNames="codigo_lho,codigo_Amb,descripcion_Amb,descripcionReal_Amb,capacidad_Amb"  >
		                                                                <RowStyle Font-Size="12px" /> 
                                                                        <Columns>
			                                                                <%--<asp:TemplateField>
				                                                                <HeaderTemplate>--%>
					                                                                <%--<asp:CheckBox ID="chkHeader" runat="server" onclick="MarcarCursos(this)" />
					                                                                <asp:CheckBox ID="CheckBox1" runat="server" onclick="MarcarCursos(this)"/>--%>
					                                                            <%--    Sel.
				                                                                </HeaderTemplate>
				                                                                <ItemTemplate>
					                                                                <asp:CheckBox ID="chkElegir" runat="server" />
				                                                                </ItemTemplate>
				                                                                <ItemStyle HorizontalAlign="Center" />
			                                                                </asp:TemplateField>--%>  
			                                                                <asp:BoundField DataField="codigo_lho" HeaderText="CODIGO"  ItemStyle-Width="0px" ItemStyle-Wrap="false"/>
			                                                                <asp:BoundField DataField="codigo_Amb" HeaderText="CODAMB"/>
			                                                                <asp:BoundField DataField="descripcion_Tam" HeaderText="TIPO"/>
			                                                                <asp:BoundField DataField="descripcion_Amb" HeaderText="AMBIENTE"/>
                                                                            <asp:BoundField DataField="descripcionReal_Amb" HeaderText="UBICACION"/>
                                                                            <asp:BoundField DataField="dia_Lho" HeaderText="DIA"/>
                                                                            <asp:BoundField DataField="nombre_Hor" HeaderText="H. INI" />
                                                                            <asp:BoundField DataField="horaFin_Lho" HeaderText="H.FIN" />
                                                                            <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" />
                                                                            <asp:BoundField DataField="capacidad_Amb" HeaderText="CAPACIDAD" />
                                                                            <asp:BoundField DataField="nroMatriculados_Cup" HeaderText="MAT." />
                                                                            <%--<asp:BoundField DataField="DebeTesis" HeaderText="TESIS" />                                                                                                                                   
                                                                            <asp:BoundField DataField="totalCredElecObl_Pes" HeaderText="CRED. OBLIG. ELECTIVO" />
                                                                            <asp:BoundField DataField="totalCreObl_Pes" HeaderText="CRED. PLAN ESTUDIO" />
                                                                            <asp:BoundField DataField="descripcion_Cac" HeaderText="ULT. MAT." />--%>
                                                                            <asp:TemplateField HeaderText="CAMBIAR" ItemStyle-CssClass="col-operacion">            
			                                                                <ItemTemplate>
                                                                                <asp:LinkButton ID="lbCambiarAmb" runat="server" 
					                                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                                                CommandName="CambiarAmb" CssClass="btn btn-primary btn-sm">
                                                                                    <span><i class="fa fa-retweet"></i></span>
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
                                                    
                                                    <hr />
                                                   <div class="row">
                                                        <div class="col-sm-2">
                                                             <asp:LinkButton ID="lbRetTabEnv" runat="server" CssClass="btn btn-danger">
                                                                <i class="fa fa-chevron-left"></i>
                                                                <span class="text">Regresar</span>
                                                            </asp:LinkButton>   
                                                        </div>                                                   
                                                   </div>
                                                    
                                                </div>
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
        <!-- Modal Horario -->
        <div id="myModalAmbiente" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	        <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="modalAmbiente">
	                <asp:UpdatePanel ID="udpModalAmbiente" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Ambientes Disponibles</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Curso:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCursoModal" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Ambiente Actual:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAmbienteModal" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                    </div>    
                                </div>
                            </div>          
	                    </ContentTemplate>
	                </asp:UpdatePanel>
	            </div>
	        </div>
	    </div>
	    <!-- Fin Modal-->
    </form>
</body>
</html>
