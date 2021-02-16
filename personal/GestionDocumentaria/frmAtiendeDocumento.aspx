<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAtiendeDocumento.aspx.vb" Inherits="GestionDocumentaria_frmAtiendeDocumento" %>

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
    <link href="../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    
    <!-- Iconos -->    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css"/> 
    
    <!--Alertas css-->    
    <link href="../Alumni/css/sweetalert/animate.css" rel="stylesheet" type="text/css" />
    <link href="../Alumni/css/sweetalert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    
    <!--Jquery-->
    <script src="../assets/jquery/jquery-3.3.1.js" type="text/javascript"></script>
    <%-- <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    <!--Modal -->
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js" type="text/javascript"></script>
        
    <!-- Datatable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
    
    
    <%--<script src="../Alumni/js/datatables/jquery.dataTables.min.js" type="text/javascript"></script> 
    <link href="../Alumni/css/datatables/jquery.dataTables.min.css" rel="stylesheet" type="text/css" />--%>
     
      
        
    <!--Alertas js-->    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        
    /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } /* fin de la funcion */
      
      /* Abrir modal observa tramite*/
        function openModalObserva() {
            $('#myModalObserva').modal('show');
        };
        
          function cerrarModalObserva() {
            $('#myModalObserva').modal('hide');
        };
          /* Abrir modal completar datos*/
        function openModalDatos() {
            $('#myModalDatos').modal('show');
        };
         function cerrarModalDatos() {
            $('#myModalDatos').modal('hide');
        };
        
     
        
       /* Formato Datatable */
       
         function formatoGrilla() {
            
            $('#<%=gvListaSolicitudes.ClientID%>').DataTable({
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
                },
            paging: false,   
            searching: false
            }    
                  
            });          
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
             
    </script>

</head>
<body>
  
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
         <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
        <div class="container-fluid">
            <div class="panel-cabecera">
             <div class="card" style="height:1080px" >
             
               
                    <div class="card-header">
                       <div class="row">
                                <div class="col-sm-9">ATENCIÓN DE DOCUMENTOS</div>
                                 <div class="col-sm-3">
                                    <asp:LinkButton ID="lbVerPendientes" runat="server" CssClass="btn btn-danger btn-sm" ToolTip="Solicitar PDF" Visible = "false">
                                        <span><i class="fa fa-eye"></i></span> &nbsp; &nbsp;Pendientes
						            </asp:LinkButton>
						            <asp:LinkButton ID="lbVerProcesadas" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Solicitar PDF" Visible = "false">
                                        <span><i class="fa fa-cog"></i></span> &nbsp; &nbsp;Generados
						            </asp:LinkButton>
						         
						            
                                </div>
                            </div>
                    </div>                
                                                          
                    <div class="card-body">
                        <br />                     
	                    
                        <div class="row">
                            <label class="col-sm-1 col-form-label form-control-sm" for="lblEstado">Estado:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlCodigo_est" runat="server" class="form-control" Enabled ="true" AutoPostBack = "true"></asp:DropDownList>
                            </div> 
                            <label class="col-sm-1 col-form-label form-control-sm" for="lblDocumento">Documento:</label>
                            <div class="col-sm-5">
                                <asp:DropDownList ID="ddlCodigo_doc" runat="server" class="form-control" Enabled ="true" AutoPostBack = "true"  ></asp:DropDownList>
                            </div>
	                        <div class="col-sm-3">
	                                <asp:DropDownList ID="ddlCodigo_are" runat="server" class="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
	                        </div>  	                                  
                        </div> 
                       <%-- <asp:UpdatePanel ID="udpListado" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>--%>
                         
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                            <asp:GridView ID="gvListaSolicitudes" runat="server" CssClass="table table-striped table-bordered" 
                                            AutoGenerateColumns="False" DataKeyNames="codigo_cda, codigoDatos, nombreArchivo, codigo_sol, codigo_dot, estado_sol, referencia01, codigo_fac, finEtapaTramiteGen, enviaEmailGen, adjuntaFileGen , observaDocGen, completarDatosGen, indFirma, observaTramite">
                                            <RowStyle Font-Size="12px" /> 
                                            <Columns>
                                                <asp:BoundField DataField="codigo_sol" HeaderText="SOLICITUD" />                                                
                                                <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" />                                  
                                                <asp:BoundField DataField="fechaReg" HeaderText="FECHA" />
                                                <asp:BoundField DataField="usuario" HeaderText="USUARIO / ALUMNO QUE SOLICITA" />                                                
                                                <asp:BoundField DataField="descripcion_est" HeaderText="ESTADO" />
                                                <asp:BoundField DataField="serieCorrelativo_dot" HeaderText="SERIE/DOC" />  
                                                <%--<asp:BoundField DataField="codigo_cac" HeaderText="CODIGOCAC" />
                                                <asp:BoundField DataField="referencia01" HeaderText="REF" /> 
                                                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="UNI" />--%>                                                                                              
                                                <asp:TemplateField HeaderText="OPCION">            
			                                        <ItemTemplate>
			                                            <asp:LinkButton ID="btnDatos" ToolTip="Completar Datos" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="datos" CssClass="btn btn-info btn-sm">
                                                            <span><i class="fa fa-retweet"></i></span>
						                                </asp:LinkButton>
			                                            <asp:LinkButton ID="btnVer" ToolTip="Vista Previa" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="ver" CssClass="btn btn-success btn-sm">
                                                            <span><i class="fa fa-eye"></i></span>
						                                </asp:LinkButton>
                                                        <asp:LinkButton ID="btnGenerar" ToolTip="Generar Documento" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="generar" CssClass="btn btn-primary btn-sm"
					                                        OnClientClick="return alertConfirm(this, event, '¿Desea generar el documento?', 'warning');">
                                                            <span><i class="fa fa-cog"></i></span> 
						                                </asp:LinkButton>
                                                        <asp:LinkButton ID="btnDescargar" ToolTip="Descargar Documento" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="descargar" CssClass="btn btn-info btn-sm">
                                                            <span><i class="fa fa-file-download"></i></span> 
						                                </asp:LinkButton>
						                                 <asp:LinkButton ID="btnObservar" ToolTip="Observar" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="observar" CssClass="btn btn-danger btn-sm">
                                                            <span><i class="fa fa-exclamation-triangle"></i></span> 
						                                </asp:LinkButton>
						                                <asp:LinkButton ID="btnObservado" ToolTip="Observado" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="observado" CssClass="btn btn-warning btn-sm">
                                                            <span><i class="fa fa-exclamation-triangle"></i></span> Observado
						                                </asp:LinkButton>
						                               
                                                    </ItemTemplate>                                            
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                            </asp:GridView>     
                                        </div>                          
                        </div>
                        
                          <%-- </ContentTemplate>
	                </asp:UpdatePanel>--%>
                    </div>
                  
                    
               
            </div>
        </div>
        </div>
        <!--Modal Oberva-->
        <div id="myModalObserva" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="modalObserva">
	                <%--<asp:UpdatePanel ID="udpModalArea" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>--%>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">OBSERVACIONES - HISTORIAL:</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Trámite:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtTramite_obs" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                               
                                    <label class="col-sm-2 col-form-label form-control-sm">Fecha:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFecha_obs" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                 </div>                                
                                <br /> 
                                 <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Alumno:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtAlumno_obs" runat="server"  readonly="true" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>                               
                                <br />                                
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Concepto:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtConcepto_obs" runat="server" readonly="true" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />   
                                  <!--<div class="row" visible = "false">
                                    <label class="col-sm-2 col-form-label form-control-sm">Descripcion:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtDescripcion_obs" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>    
                                </div>
                               <br />-->                                
                                <div class="row" id="divObservacion_mod" runat="server">
                                    <label class="col-sm-2 col-form-label form-control-sm">Observación:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtObservacion_obs" runat="server" CssClass="form-control" Style="text-transform: uppercase" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>    
                                </div>
                                <br /> 
                                <div class="col-md-12">
                                    <asp:GridView ID="gvListaObservaciones" runat="server" CssClass="table table-striped table-bordered" 
                                    AutoGenerateColumns="False" >
                                    <RowStyle Font-Size="12px" /> 
                                    <Columns>
                                        <asp:BoundField DataField="fechaReg" HeaderText="FECH. OBSERVA" />                                                
                                        <asp:BoundField DataField="observacion" HeaderText="OBSERVACION" />                                  
                                        <asp:BoundField DataField="fechaMod" HeaderText="FECH. RESPUESTA" />
                                        <asp:BoundField DataField="respuesta" HeaderText="RESPUESTA" />                                                
                                       
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                    </asp:GridView>     
                                </div>
                                <br />                            
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbGuardarObservacion" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                
	                                <asp:TextBox ID="txtCodigoDta_obs" runat="server" Visible="false"></asp:TextBox> 
	                               <asp:TextBox ID="txtObservaTramite_obs" runat="server" Visible="false"></asp:TextBox> 
	                               <asp:TextBox ID="txtCodigoSol_mod" runat="server" Visible="false"></asp:TextBox> 
	                               <asp:TextBox ID="txtCodigoEstado_obs" runat="server" Visible="false"></asp:TextBox>                                    
                                </div>
                            </div>          
	                  <%--  </ContentTemplate>
	                </asp:UpdatePanel>--%>
	            </div>
	        </div>
	    </div>
	     <!--Modal Datos-->
        <div id="myModalDatos" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="Div2">
	                <%--<asp:UpdatePanel ID="udpModalDatos" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
	                    <ContentTemplate>--%>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold; data-backdrop=static; ">
	                        
	                        
	                        
				                <span class="modal-title">COMPLETAR DATOS DEL TRAMITE </span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Trámite:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtModDatTramite" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                               
                                    <label class="col-sm-2 col-form-label form-control-sm">Fecha:</label>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtModDatFecha" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                 </div>                                
                                <br /> 
                                 <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Alumno:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtModDatAlumnos" runat="server"  readonly="true" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                    <label class="col-sm-1 col-form-label form-control-sm">Cod.Univ.:</label> 
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtModDatCodUniv" runat="server"  readonly="true" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>  
                                </div>                               
                                <br />                                
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Concepto:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtModDatConcepto" runat="server" readonly="true" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />   
                                <div class="row" id = "divDescripcionDat" runat="server">
                                    <label class="col-sm-2 col-form-label form-control-sm">Descripcion:</label>
                                    <div class="col-sm-10">
                                        <asp:TextBox ID="txtModDatDescripcion" runat="server" CssClass="form-control" readonly="true" Style="text-transform: uppercase" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />   
                               
                                <div class="row" id="divDdlCodigoCac" runat = "server">
                                    <label class="col-sm-2 col-form-label form-control-sm" for="lblEstado">Ciclo:</label>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlModDatosCodigoCac" runat="server" class="form-control" Enabled ="true" AutoPostBack = "false"></asp:DropDownList>
                                    </div> 
                                </div> 
                                <br /> 
                                 <div class="col-md-12">
                                    <asp:GridView ID="gvListaObservacionesDat" runat="server" CssClass="table table-striped table-bordered" 
                                    AutoGenerateColumns="False" >
                                    <RowStyle Font-Size="12px" /> 
                                    <Columns>
                                        <asp:BoundField DataField="fechaReg" HeaderText="FECH. OBSERVA" />                                                
                                        <asp:BoundField DataField="observacion" HeaderText="OBSERVACION" />                                  
                                        <asp:BoundField DataField="fechaMod" HeaderText="FECH. RESPUESTA" />
                                        <asp:BoundField DataField="respuesta" HeaderText="RESPUESTA" />                                                
                                       
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                    </asp:GridView>     
                                </div>
                                <br />                    
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbDatosGuardar" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="txtCodigoSol_mod_dat" runat="server" Visible="false"></asp:TextBox>
	                                <asp:TextBox ID="txtCodigoCda_mod_dat" runat="server" Visible="false"></asp:TextBox> 
	                                <asp:TextBox ID="TextBox4" runat="server" Visible="false"></asp:TextBox> 
	                                <asp:TextBox ID="TextBox5" runat="server" Visible="false"></asp:TextBox>                                     
                                </div>
                            </div>          
	                   <%-- </ContentTemplate>
	                </asp:UpdatePanel>--%>
	            </div>
	        </div>
	     
	    </div>
	    
    </form>
    
    
</body>
</html>
