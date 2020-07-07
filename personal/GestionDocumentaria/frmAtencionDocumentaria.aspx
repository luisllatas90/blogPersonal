<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAtencionDocumentaria.aspx.vb" Inherits="GestionDocumentaria_frmAtencionDocumentaria" %>

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
        
    </script>
    

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager>
        <div>
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
                                        <span><i class="fa fa-cog"></i></span> &nbsp; &nbsp;Procesadas
						            </asp:LinkButton>
                                </div>
                            </div>
                            
                        </div>
                        <div class="card-body">
                            <%--<asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>--%>  
                                    <div class="row">
                                         
                                        
                                    </div>
                                   
                                    <div class="row">
                                        <label class="col-sm-1 col-form-label form-control-sm" for="lblDocumento">Documento:</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlCodigo_doc" runat="server" class="form-control" Enabled ="true" AutoPostBack = "true"  ></asp:DropDownList>
                                        </div>  
                                        <%--<label class="col-sm-1 col-form-label form-control-sm" for="lblArea" visible = "false" >Area:</label>--%>
	                                    <div class="col-sm-3">
	                                        <asp:DropDownList ID="ddlCodigo_are" runat="server" class="form-control" AutoPostBack="true" Visible="false"></asp:DropDownList>
	                                    </div>
	                                  
                                    </div>
                                    <hr /> 
                                 <%--   <br />
                                   <div class="row">  	                                   
	                                    <div class="col-sm-2">
	                                        <asp:TextBox ID="TxtCodAlu" runat="server" CssClass="form-control" Visible ="false" ></asp:TextBox>
	                                    </div>      
                                    </div> 
                                    <br />
                                    <div class="row">
                                         <div class="col-sm-1">
	                                        <asp:LinkButton ID="lbVerPdf" runat="server" CssClass="btn btn-success btn-sm" ToolTip="Visualizar" >
                                                <span><i class="fa fa-eye"></i></span> &nbsp; &nbsp;Visualizar
						                    </asp:LinkButton>
	                                    </div>  
	                                     <div class="col-sm-1">
	                                        <asp:LinkButton ID="lbGeneraSolicitud" runat="server" CssClass="btn btn-info" ToolTip="Solicitar PDF" Visible = "true">
                                                <span><i class="fa fa-file-pdf"></i></span> &nbsp; &nbsp;Solicitar
						                    </asp:LinkButton>
	                                    </div>
                                        <asp:TextBox ID="txtCodigo_cda" runat="server"></asp:TextBox>       
                                    </div>
                                    <hr />--%>
                                      
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvListaSolicitudes" runat="server" CssClass="table table-striped table-bordered" 
                                            AutoGenerateColumns="False" DataKeyNames="codigo_sol, codigo_dot, codigo_alu, codigo_cac, referencia01, codigoUniver_Alu, codigo_doc">
                                            <RowStyle Font-Size="12px" /> 
                                            <Columns>
                                                <asp:BoundField DataField="codigo_sol" HeaderText="SOLICITUD" />                                                
                                                <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" />                                  
                                                <asp:BoundField DataField="fechaReg" HeaderText="FECHA" />
                                                <asp:BoundField DataField="usuario" HeaderText="USUARIO QUE SOLICITA" />                                                
                                                <asp:BoundField DataField="estado_sol" HeaderText="ESTADO" />
                                                <%--<asp:BoundField DataField="codigo_alu" HeaderText="CODIGOALU" />  
                                                <asp:BoundField DataField="codigo_cac" HeaderText="CODIGOCAC" />
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
					                                        CommandName="generar" CssClass="btn btn-warning btn-sm">
                                                            <span><i class="fa fa-cog"></i></span> Generar
						                                </asp:LinkButton>
						                              
                                                        <asp:LinkButton ID="btnDescargar" ToolTip="Descargar" runat="server" 
					                                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                        CommandName="descargar" CssClass="btn btn-info btn-sm">
                                                            <span><i class="fa fa-file-download"></i></span> Descargar
						                                </asp:LinkButton>
                                                    </ItemTemplate>                                            
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                            </asp:GridView>     
                                        </div>                          
                                    </div>
                    
                                    <div class="row">                                        
                                        <iframe id="ifrmReporte" runat="server" src="" width="100%" height="600px" scrolling="no"
                                          frameborder="0"></iframe>                                        
                                    </div>                       
                              <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                          
                        </div> <!--/card body ->
                    </div> <!--/card ->
                </div>
            </div>       
        </div>

    </form>
</body>
</html>
