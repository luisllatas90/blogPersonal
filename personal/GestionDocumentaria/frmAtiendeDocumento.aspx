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
      
      /* Abrir modal Tipo documento*/
        function openModalObserva() {
            $('#myModalObserva').modal('show');
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
                }
            }             
            });          
        }
        
       /*  fin formato grilla */
          
             
    </script>

</head>
<body>
  
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <div class="container-fluid">
            <div class="panel-cabecera">
             <div class="card" style="height:1080px" >
               
                    <div class="card-header">
                       <div class="row">
                                <div class="col-sm-9">ATENCION DE DOCUMENTOS</div>
                                 <div class="col-sm-3">
                                    <asp:LinkButton ID="lbVerPendientes" runat="server" CssClass="btn btn-danger btn-sm" ToolTip="Solicitar PDF" Visible = "true">
                                        <span><i class="fa fa-eye"></i></span> &nbsp; &nbsp;Pendientes
						            </asp:LinkButton>
						            <asp:LinkButton ID="lbVerProcesadas" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Solicitar PDF" Visible = "true">
                                        <span><i class="fa fa-cog"></i></span> &nbsp; &nbsp;Generados
						            </asp:LinkButton>
						         <%--   
						            <asp:LinkButton ID="lbGenerarInforme" runat="server" CssClass="btn btn-info btn-sm" ToolTip="Informe" Visible = "true">
                                        <span><i class="fa fa-cog"></i></span> &nbsp; &nbsp;InformeAsesor
						            </asp:LinkButton>--%>
						            
                                </div>
                            </div>
                    </div>
                    <div class="card-body">
                        <br />
                        <div class="row">
                            <label class="col-sm-1 col-form-label form-control-sm" for="lblDocumento">Documento:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlCodigo_doc" runat="server" class="form-control" Enabled ="true" AutoPostBack = "true"  ></asp:DropDownList>
                            </div>
	                        <div class="col-sm-3">
	                                <asp:DropDownList ID="ddlCodigo_are" runat="server" class="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
	                        </div>
	                                  
                        </div>  
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                            <asp:GridView ID="gvListaSolicitudes" runat="server" CssClass="table table-striped table-bordered" 
                                            AutoGenerateColumns="False" DataKeyNames="codigo_cda, codigoDatos, nombreArchivo, codigo_sol, codigo_dot, estado_sol, referencia01, codigo_fac">
                                            <RowStyle Font-Size="12px" /> 
                                            <Columns>
                                                <asp:BoundField DataField="codigo_sol" HeaderText="SOLICITUD" />                                                
                                                <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" />                                  
                                                <asp:BoundField DataField="fechaReg" HeaderText="FECHA" />
                                                <asp:BoundField DataField="usuario" HeaderText="USUARIO QUE SOLICITA" />                                                
                                                <asp:BoundField DataField="descripcion_est" HeaderText="ESTADO" />
                                                <asp:BoundField DataField="serieCorrelativo_dot" HeaderText="SERIE/DOC" />  
                                                <%--<asp:BoundField DataField="codigo_cac" HeaderText="CODIGOCAC" />
                                                <asp:BoundField DataField="referencia01" HeaderText="REF" /> 
                                                <asp:BoundField DataField="codigoUniver_Alu" HeaderText="UNI" />--%>                                                                                              
                                                <asp:TemplateField HeaderText="OPCION">            
			                                        <ItemTemplate>
			                                            <asp:LinkButton ID="btnVer" ToolTip="Vista Previa" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="ver" CssClass="btn btn-success btn-sm">
                                                            <span><i class="fa fa-eye"></i></span> Ver
						                                </asp:LinkButton>
                                                        <asp:LinkButton ID="btnGenerar" ToolTip="Generar" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="generar" CssClass="btn btn-primary btn-sm">
                                                            <span><i class="fa fa-cog"></i></span> Generar
						                                </asp:LinkButton>
                                                        <asp:LinkButton ID="btnDescargar" ToolTip="Descargar" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="descargar" CssClass="btn btn-info btn-sm">
                                                            <span><i class="fa fa-file-download"></i></span> Descargar
						                                </asp:LinkButton>
						                                <asp:LinkButton ID="btnObservar" ToolTip="Observar" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="observar" CssClass="btn btn-danger btn-sm">
                                                            <span><i class="fa fa-exclamation-triangle"></i></span> Observar
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
				                <span class="modal-title">Ingresar la observación:</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                               <%-- <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">codigoSol:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtCodigoSol_mod" runat="server" CssClass="form-control" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                                </div>--%>
                                <br />
                                <div class="row">
                                    <label class="col-sm-12 col-form-label form-control-sm">Observación:</label>
                                    <div class="col-sm-12">
                                        <asp:TextBox ID="txtObservacion_mod" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />                             
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbGuardarObservacion" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="txtCodigoSol_mod" runat="server" Visible="false"></asp:TextBox>                                    
                                </div>
                            </div>          
	                  <%--  </ContentTemplate>
	                </asp:UpdatePanel>--%>
	            </div>
	        </div>
	    </div>
    </form>
    
    
</body>
</html>
