<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmFirmarDocumentacion.aspx.vb" Inherits="GestionDocumentaria_frmFirmarDocumentacion" %>

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
        
         /* Abrir modal area*/
        function openModal() {
            $('#myModalArea').modal('show');
        };
     
          
       /* Formato Datatable */
       
         function formatoGrilla() {
            
            $('#<%=gvListaDocByFirmar.ClientID%>').DataTable({
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
                 <div class="card">   
                    <div class="card-header">FIRMAR DOCUMENTOS</div>      
                    <div class="card-body">
                        <div class="row">
                            <label class="col-sm-1 col-form-label form-control-sm" for="lblDocumento">Documento:</label>
                            <div class="col-sm-3">
                                <asp:DropDownList ID="ddlCodigo_doc" runat="server" class="form-control" Enabled ="true" AutoPostBack = "true"></asp:DropDownList>
                            </div> 
                         </div>
                        <hr />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvListaDocByFirmar" runat="server" CssClass="table table-striped table-bordered" 
                                    AutoGenerateColumns="False" DataKeyNames="codigo_dot, codigo_dofm, descripcion_est, codigo_cda, codigo_sol, codigo_doa">
                                    <RowStyle Font-Size="12px" /> 
                                    <Columns>
                                        <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" /> 
                                        <asp:BoundField DataField="orden_fma" HeaderText="ORDEN" />                                 
                                        <asp:BoundField DataField="fecha" HeaderText="FECHA" />  
                                         <asp:BoundField DataField="descripcion_est" HeaderText="ESTADO" /> 
                                         <asp:BoundField DataField="serieCorrelativo_dot" HeaderText="SERIE/DOC" />
                                                                               
                                        <asp:TemplateField HeaderText="OPCION">            
			                                <ItemTemplate>
			                                    <asp:LinkButton ID="btnVer" ToolTip="Vista Previa" runat="server" 
					                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                CommandName="ver" CssClass="btn btn-success btn-sm">
                                                    <span><i class="fa fa-eye"></i></span> Ver
						                        </asp:LinkButton>						                       
						                        <asp:LinkButton ID="btnDescargar" ToolTip="Descargar" runat="server" 
					                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                CommandName="descargar" CssClass="btn btn-info btn-sm">
                                                    <span><i class="fa fa-file-download"></i></span> Descargar
						                        </asp:LinkButton> 
                                                <asp:LinkButton ID="btnFirmar" ToolTip="Generar" runat="server" 
					                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                CommandName="firmar" CssClass="btn btn-warning btn-sm">
                                                    <span><i class="fa fa-cog"></i></span> Firmar                                                      
						                        </asp:LinkButton>
						                        <asp:FileUpload ID="fuArchivoPdf" runat="server" EnableViewState="true" />&nbsp; &nbsp;
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                                                                
                                        <%--<asp:TemplateField HeaderText="OPCION">
                                            <ItemTemplate>
                                                <asp:FileUpload ID="fuArchivoPdf" runat="server" EnableViewState="true" />&nbsp;  &nbsp;
                                                <asp:LinkButton ID="btnFirmar" ToolTip="Generar" runat="server" 
					                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                CommandName="firmar" CssClass="btn btn-warning btn-sm">
                                                    <span><i class="fa fa-cog"></i></span> Firmar                                                      
						                        </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                </asp:GridView>     
                            </div>                          
                        </div>                     
                    </div>
                </div>
             </div>
        </div>
    </form>
</body>
</html>
