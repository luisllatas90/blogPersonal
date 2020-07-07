<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfigRequisitosFuncion.aspx.vb" Inherits="frmConfigRequisitosFuncion" %>

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
    
    
     <script type="text/javascript">

         function openModal() {
             $('#myModal').modal('show');
         }
         function closeModal() {
             $('#myModal').modal('hide');
         }

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
        } /* fin de la funcion */   

     </script>
     
    
    <%--<style type="text/css">  
              
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
    --%>    
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="udpScripts" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"></asp:UpdatePanel>
    <div class="container-fluid">
        <div class="card div-title">                        
            <div class="row title">CONFIGURACIÓN DE REQUISITOS</div>
        </div> 
		<ul class="nav nav-tabs" role="tablist">
			<li class="nav-item">
				<a href="#listado" id="listado-tab" class="nav-link active" data-toggle="tab" role="tab"
					aria-controls="listado" aria-selected="true">Listado</a>
			</li>         
	    </ul> 
        <div class="panel-cabecera">
            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <div class="card">
                        <div class="card-header">Filtros de Búsqueda</div>
                        <div class="card-body">
                            <div class="row">
                                <label class="col-sm-2 col-form-label form-control-sm" for="fCarProf">Carrera Profesional:</label>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlCarrrera" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                </div> 
                                <label class="col-sm-2 col-form-label form-control-sm" for="fPlanEst">Plan Curricular:</label>                                                                                 
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlPlanEst" runat="server" class="form-control"></asp:DropDownList>
                                </div>
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
                        </div>
                    </div>                                                     
                </ContentTemplate>                    
            </asp:UpdatePanel>
        </div>   
        <br/>
        <div class="table-responsive">
            <asp:UpdatePanel ID="udpListado" runat="server" UpdateMode="Conditional">
                <ContentTemplate>              
                    <asp:GridView ID="gvRequisitoEgreso" runat="server" AutoGenerateColumns="false"
                    ShowHeader="true" DataKeyNames="codigo_req, codigo_tip, codigo_pcur, cantidad, nombre, codigo_cat"
                    CssClass="table table-bordered table-hover">
                        <Columns>                                
                            <asp:BoundField DataField="codigo_tip" HeaderText="Código" Visible="false" />
                            <%--<asp:BoundField DataField="cantidad" HeaderText="Cantidad" />--%>                                
                            <asp:BoundField DataField="nombre" HeaderText="REQUISITO DE EGRESO" />
                            <asp:BoundField DataField="nombre_cat" HeaderText="TIPO"/>
                            <asp:TemplateField>
                                <HeaderTemplate>                                                
                                    PLAN DE ESTUDIO
                                </HeaderTemplate>
                                <ItemTemplate>  
                                    <asp:CheckBox ID="chkPlanEst" runat="server" Checked='<%# Eval("indica_pe") %>' Enabled="false" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-primary btn-sm">
                                        <span><i class="fa fa-pen"></i></span>
                                    </asp:LinkButton>                                        
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se encontraron Requisitos de Egreso
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="12px" />
                        <RowStyle Font-Size="11px" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>  
    </div>

    <!-- Modal Registro de Tipo Requisito de Egreso -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">    
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalContent">
             <asp:UpdatePanel ID="udpModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <span class="modal-title">Configurar Requisito de Egreso</span> 
                    <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                </div>
                    <div class="panel-cabecera">
                          <div class="modal-body">
                            <asp:HiddenField ID="hf_Codigo_req" runat="server" />
                            <div class="row">
                            <div id="mensajeModal" runat="server">
                                </div>
                            </div>
                            <div class="row">
                             <label class="col-sm-12 col-form-label form-control-sm" for="lblReq" style="text-align:left" >
                                &nbsp&nbsp&nbsp REQUISITO:</label>
                                <div class="col-sm-12">
                                    <asp:TextBox ID="txtRequisito" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                </div>               
                            </div>
                            <br />
                             <div class="row"> 
                                <label class="col-sm-12 col-form-label form-control-sm" for="lblTipReq" style="text-align:left">
                                &nbsp&nbsp&nbsp TIPO REQUISITO:</label>
                                <div class="col-sm-7">
                                    <asp:DropDownList ID="ddlTipoRequisito" runat="server" class="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <label class="col-sm-4 col-form-label form-control-sm" for="lblIndicador">
                                PERTENECE A PLAN DE ESTUDIO:</label>                        
                                <asp:CheckBox ID="chkPlanEstudio" runat="server" />
                            </div>
                        </div>  
                    </div>
                      
                <div class="modal-footer">
                    <%--<button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Retornar</button>--%>
                    <asp:LinkButton ID="btnRetornar" runat="server" class="btn btn-accion btn-rojo">
                     <i class="fa fa-sign-out-alt"></i>
                    <span class="text">Salir</span>
                    </asp:LinkButton>  
                    <asp:LinkButton ID="btnGuardar" runat="server" class="btn btn-accion btn-verde">
                        <i class="fa fa-save"></i>
                        <span class="text">Guardar</span>
                    </asp:LinkButton>  
                </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
