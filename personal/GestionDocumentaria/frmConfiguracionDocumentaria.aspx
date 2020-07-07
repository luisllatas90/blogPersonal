<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionDocumentaria.aspx.vb" Inherits="GestionDocumentaria_frmConfiguracionDocumentaria" %>

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
        
        /* Abrir modal Tipo documento*/
        function openModalTipoDoc() {
            $('#myModalTipoDocumento').modal('show');
        };
        
          /* Abrir modal documento*/
        function openModalDocumento() {
            $('#myModalDocumento').modal('show');
        };
         /* Abrir modal documento*/
        function openModalFirmas() {
            $('#myModalFirmas').modal('show');
        };
        
        /* formato dataTable*/      
        function formatoGrilla() {
            
            $('#<%=gvListaConfiguraDocumnetos.ClientID%>').DataTable({
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
            <br />
            <div class="panel-cabecera">
            <div class="card">             
                <div class="card-header">CONFIGURACIÓN DE DOCUMENTOS</div>      
                <div class="card-body">
                     <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>	                             
                    <div class="row">
                            <label class="col-sm-1 control-label" for="ddlCodigo_are">Area:</label>
                            <div class="col-sm-4">                              
                                <asp:DropDownList ID="ddlCodigo_are" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-1" >
                                <asp:LinkButton ID="lbNuevaArea" runat="server" CssClass="btn btn-primary" ToolTip="Nueva Area">
                                    <span><i class="fa fa-file"></i></span>
						        </asp:LinkButton>
                            </div>
                            <label class="col-sm-1 control-label" for="ddlCodigo_tfu">Función:</label>
                            <div class="col-sm-4">
                               <%-- <select runat="server" name="ddlCodigo_tfu" id="ddlCodigo_tfu" class="form-control">
                                </select>--%>
                                <asp:DropDownList ID="ddlCodigo_tfu" runat="server" class="form-control">
                                </asp:DropDownList>
                            </div>                       
                    </div>
                    <br />
                    <div class="row">
                        <label class="col-sm-1 control-label" for="ddlCodigo_are">Tipo:</label>
                        <div class="col-sm-4">                              
                            <asp:DropDownList ID="ddlCodigo_tid" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1" >
                            <asp:LinkButton ID="lbNuevoTipDoc" runat="server" CssClass="btn btn-primary" ToolTip="Nuevo Tipo de Documento">
                            <span><i class="fa fa-file"></i></span>
						    </asp:LinkButton>
                        </div>
                        <label class="col-sm-1 control-label" for="ddlCodigo_are">Documento:</label>
                        <div class="col-sm-4">                              
                            <asp:DropDownList ID="ddlDocumento" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-sm-1" >
                            <asp:LinkButton ID="lbNuevoDoc" runat="server" CssClass="btn btn-primary" ToolTip="Nuevo Documento">
                            <span><i class="fa fa-file"></i></span>
						    </asp:LinkButton>
                        </div>
                    </div>
                    <br />                   
                    <div class="row">
                        <label class="col-sm-1 control-label" for="ddlEstado_doc">Estado:</label>
                        <div class="col-sm-2">                           
                            <asp:DropDownList ID="ddlEstado_cda" runat="server" class="form-control">
                            </asp:DropDownList>
                        </div>  
                        <div class="col-sm-4" >                    
                             <div class="custom-control custom-checkbox">
                              <input type="checkbox" class="custom-control-input" id="chkIndFirma" runat="server" >
                              <label class="custom-control-label" for="chkIndFirma">Requiere Firma</label>
                            </div>
                        </div>                                             
                        <div class="col-sm-4" >
                           <asp:Image ID="imgIndicacion" runat="server"  style="float:inherit" />
                        </div>
                        
                         <asp:TextBox ID="txtCodigo_cda" runat="server" Visible="false"></asp:TextBox>  
                        <div class="col-sm-3"></div>
                        
                    </div>
                   
                    <div class="row">
                        <div class="col-sm-1">
	                        <asp:LinkButton ID="lbAgregar" runat="server" CssClass="btn btn-success" ToolTip="Agregar">
                            <span><i class="fa fa-plus-square"></i></span> &nbsp; &nbsp;Agregar
						    </asp:LinkButton>
	                    </div>
	                     <div class="col-sm-1">
	                        <asp:LinkButton ID="lbModificar" runat="server" CssClass="btn btn-info" ToolTip="Modificar">
                            <span><i class="fa fa-edit"></i></span> &nbsp; &nbsp;Modificar
						    </asp:LinkButton>
	                    </div>              
                    </div>
                    <hr />                    
                       </ContentTemplate>
	                 </asp:UpdatePanel>
	                 
                     <asp:UpdatePanel ID="udpListadoConf" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvListaConfiguraDocumnetos" runat="server" CssClass="table table-striped table-bordered" 
                            AutoGenerateColumns="False" DataKeyNames="codigo_cda, indFirma">
                            <RowStyle Font-Size="12px" /> 
                            <Columns>
                                <asp:BoundField DataField="descripcion_tid" HeaderText="TIPO DOCUMENTO" />                                                                                                                                 
                                <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" />                                  
                                <asp:BoundField DataField="descripcion_are" HeaderText="AREA" />
                                <asp:BoundField DataField="descripcion_Tfu" HeaderText="FUNCION" />
                                <asp:BoundField DataField="abreviatura_tid" HeaderText="ABREV. TIPO" />
                                <asp:BoundField DataField="abreviatura_doc" HeaderText="ABREV. DOCUMENTO" />
                                <asp:BoundField DataField="abreviatura_are" HeaderText="ABREV. AREA" />
                                <asp:BoundField DataField="correlativo" HeaderText="CORRELATIVO" />
                                <%--<asp:BoundField DataField="indFirma" HeaderText="FIRMA" Visible = "false"  />--%>
                                <asp:BoundField DataField="estado_cda" HeaderText="ESTADO" />   
                                <asp:TemplateField HeaderText="OPCION">            
			                        <ItemTemplate>
                                        <asp:LinkButton ID="btnSelEditar" ToolTip="Editar" runat="server" 
					                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                        CommandName="editConfiDoc" CssClass="btn btn-success btn-sm">
                                            <span><i class="fa fa-edit"></i></span>
						                </asp:LinkButton>
						                 <asp:LinkButton ID="btnFirmar" ToolTip="Firmas" runat="server" 
					                        CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                        CommandName="editFirmas" CssClass="btn btn-danger btn-sm">
                                            <span><i class="fa fa-barcode"></i></span>
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
    <!-- Modal Area -->
    <div id="myModalArea" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="modalArea">
	                <asp:UpdatePanel ID="udpModalArea" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Área</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Área:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtDescripcion_are" runat="server" CssClass="form-control" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                                </div>
                                <br />
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAbreviatura_are" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />                             
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbGuardarArea" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="txtCodigo_are" runat="server" Visible="false"></asp:TextBox>
                                    
                                </div>
                                
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvAreas" runat="server" CssClass="table table-striped table-bordered" 
                                        AutoGenerateColumns="False" DataKeyNames="codigo_are">
                                        <RowStyle Font-Size="12px" /> 
                                        <Columns>                                                                                                                             
                                            <asp:BoundField DataField="descripcion_are" HeaderText="AREA" />
                                            <asp:BoundField DataField="abreviatura_are" HeaderText="ABREV." />   
                                            <asp:TemplateField HeaderText="OPCION">            
			                                    <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelEditarArea" ToolTip="Editar Área" runat="server" 
					                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                    CommandName="editArea" CssClass="btn btn-success btn-sm">
                                                        <span><i class="fa fa-edit"></i></span>
						                            </asp:LinkButton>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                        </asp:GridView>     
                                    </div>                          
                                </div>
                                
                            </div>          
	                    </ContentTemplate>
	                </asp:UpdatePanel>
	            </div>
	        </div>
	    </div>
	<!-- Fin Modal-->
	<!-- Modal Tipo Documento-->
    <div id="myModalTipoDocumento" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="modalDocumento">
	                <asp:UpdatePanel ID="udpModalDoc" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Tipo de Documento</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Tipo Documento:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txt_Descripcion_tid" runat="server" CssClass="form-control" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                                </div>
                                <br />
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txt_Abreviatura_tid" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />                             
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbGuardarDoc" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="txt_codigo_tid" runat="server" Visible="false"></asp:TextBox>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvListaDocumentos" runat="server" CssClass="table table-striped table-bordered" 
                                        AutoGenerateColumns="False" DataKeyNames="codigo_tid">
                                        <RowStyle Font-Size="12px" /> 
                                        <Columns>                                                                                                                             
                                            <asp:BoundField DataField="descripcion_tid" HeaderText="AREA" />
                                            <asp:BoundField DataField="abreviatura_tid" HeaderText="ABREV." />   
                                            <asp:TemplateField HeaderText="OPCION">            
			                                    <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelEditarDoc" ToolTip="Editar Tipo Documento" runat="server" 
					                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                    CommandName="editDoc" CssClass="btn btn-success btn-sm">
                                                        <span><i class="fa fa-edit"></i></span>
						                            </asp:LinkButton>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                        </asp:GridView>     
                                    </div>                          
                                </div>
                            </div>          
	                    </ContentTemplate>
	                </asp:UpdatePanel>
	            </div>
	        </div>
	    </div>
	<!-- Fin Modal-->
	<!-- Modal Documento-->
    <div id="myModalDocumento" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="Div2">
	                <asp:UpdatePanel ID="udpModalDocumento" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Documento</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Documento:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtDescripcion_doc" runat="server" CssClass="form-control" Style="text-transform: uppercase" ></asp:TextBox>
                                    </div>                                    
                                </div>
                                <br />
                                <div class="row">
                                    <label class="col-sm-2 col-form-label form-control-sm">Abreviatura:</label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtAbreviatura_doc" runat="server" CssClass="form-control" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>    
                                </div>
                                <br />                             
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbGuardarDocumento" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="txtCodigo_doc" runat="server" Visible="false"></asp:TextBox>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvListaDoc" runat="server" CssClass="table table-striped table-bordered" 
                                        AutoGenerateColumns="False" DataKeyNames="codigo_doc">
                                        <RowStyle Font-Size="12px" /> 
                                        <Columns>                                                                                                                             
                                            <asp:BoundField DataField="descripcion_doc" HeaderText="DOCUMENTO" />
                                            <asp:BoundField DataField="abreviatura_doc" HeaderText="ABREV." />   
                                            <asp:TemplateField HeaderText="OPCION">            
			                                    <ItemTemplate>
                                                    <asp:LinkButton ID="btnSelEditarDocumento" ToolTip="Editar Documento" runat="server" 
					                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                    CommandName="editDocumento" CssClass="btn btn-success btn-sm">
                                                        <span><i class="fa fa-edit"></i></span>
						                            </asp:LinkButton>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                        </asp:GridView>     
                                    </div>                          
                                </div>
                            </div>          
	                    </ContentTemplate>
	                </asp:UpdatePanel>
	            </div>
	        </div>
	    </div>
	<!-- Fin Modal--> 
	<!-- Modal Firmas -->
	<div id="myModalFirmas" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	    <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="Div3">
	                <asp:UpdatePanel ID="udpFirmas" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Agregar Firmas</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="row" >
                                    <label class="col-sm-2 control-label" for="ddlCodigo_are">Documento:</label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDescripcion_doc_mod" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                                    </div>                                    
                                </div>
                                <br />
                                <%--<div class="row">
                                <label class="col-sm-2 control-label" for="ddlCodigo_are">Responsable:</label>
                                    <div class="col-sm-8">                              
                                        <asp:DropDownList ID="ddlUsuarioFirma" runat="server" class="form-control" Visible="false">                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />--%>
                                <div class="row">
                                    <label class="col-sm-2 control-label" for="ddlCodigo_are">Perfil Usuario:</label>
                                    <div class="col-sm-8">                              
                                        <asp:DropDownList ID="ddlCodigo_tfu_modFma" runat="server" class="form-control">                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <label class="col-sm-2 control-label" for="ddlCodigo_are">Alcance:</label>
                                    <div class="col-sm-8">                              
                                        <asp:DropDownList ID="ddlAlcance_modFma" runat="server" class="form-control">                                        
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                    <asp:TextBox ID="txtCodigo_fma" runat="server" Visible ="false"></asp:TextBox>  
                                    <asp:TextBox ID="txtCodigo_cda_modFma" runat="server" Visible ="false"></asp:TextBox>
                                    <asp:TextBox ID="txtCodigo_tfu_modFma" runat="server" Visible ="false"></asp:TextBox> 
                                    <asp:TextBox ID="txtOrden_fma" runat="server" Visible ="false"></asp:TextBox> 
                                                           
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbAddFirma" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                <asp:TextBox ID="TextBox1" runat="server" Visible="false"></asp:TextBox>
                                </div>
                                
                                <br />
                                
                                <div class="row">
                                     <div class="col-md-12">
                                        <asp:GridView ID="gvConfiguraFirma" runat="server" CssClass="table table-striped table-bordered" 
                                        AutoGenerateColumns="False" DataKeyNames="">
                                        <RowStyle Font-Size="12px" /> 
                                        <Columns>                                                                                                                             
                                            <asp:BoundField DataField="codigo_fma" HeaderText="CODIGO" />
                                            <asp:BoundField DataField="descripcion_tfu" HeaderText="TIPO" />
                                            <asp:BoundField DataField="alcance" HeaderText="ALCANCE" />
                                            <asp:BoundField DataField="orden_fma" HeaderText="ORDEN" />   
                                            <asp:TemplateField HeaderText="OPCION">            
			                                    <ItemTemplate>
                                                    <asp:LinkButton ID="editFirma" ToolTip="Editar Documento" runat="server" 
					                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                    CommandName="editFirma" CssClass="btn btn-success btn-sm">
                                                        <span><i class="fa fa-edit"></i></span>
						                            </asp:LinkButton>
                                                </ItemTemplate>                                            
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
                                        </asp:GridView>     
                                    </div>              
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
         </div>
         </div>
	<!--Fin modal-->
	 
	  
    </form>
</body>
</html>
