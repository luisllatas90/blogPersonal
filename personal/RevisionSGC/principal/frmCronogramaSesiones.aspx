<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCronogramaSesiones.aspx.vb" Inherits="GestionDocumentaria_frmCronogramaSesiones" %>

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
    <!--<link href="../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />-->
     <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" /> <!--hcano-->
    <!-- Iconos -->    
    <link rel="stylesheet" href="../../assets/fontawesome-5.2/css/all.min.css"/> 
    
    <!--Alertas css-->    
    <link href="../../Alumni/css/sweetalert/animate.css" rel="stylesheet" type="text/css" />
    <link href="../../Alumni/css/sweetalert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    
      <!--Jquery-->
    <script type="text/javascript" src='../../assets/js/jquery.js'></script>
    <!--<script src="../assets/jquery/jquery-3.3.1.js" type="text/javascript"></script>-->
    
    <!--Modal -->
    <%-- <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js" type="text/javascript"></script>--%>
        <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>
    
    <!--Alertas js-->    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script> 
      
      <!--Datetimepicker--> 
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    <!-- Datatable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
       
    
    <script type="text/javascript">
     $(document).ready(function() {
     
       $('#txtFecha').datepicker({
            autoclose: true, orientation: "top"
        });
     
        $('#btn_calendIni').click(function() {
            $("#txtFecha").datepicker('show');
        });
     
     }); /*fin ready*/ 
     
        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } /* fin de la funcion */
        
           /* Abrir modal sesion*/
        function openModalSesion() {
            $('#myModalSesion').modal('show');
        };
        /* Cerrar modal sesion*/
        function closeModalSesion() {
            $('#myModalSesion').modal('hide');
        };
     
        
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
        
         /* formato dataTable*/      
        function formatoGrilla() {
            
            $('#<%=gvListaSesiones.ClientID%>').DataTable({
            pageLength : 10,
            "ordering": false,
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
       
    
    <style type="text/css">
        body {padding-right: 0 !important;}  
        
        .modal-open
        {
            overflow: inherit;
        }
        .form table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
            
        }
        /*.control-label{ font-size:small; }*/
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            min-width: 0px;
            max-width: 500px;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
        #datetimepicker1 a
        {
            color: #337ab7;
            font-weight: bold;
            vertical-align: middle;
        }                    
    </style>      
        
</head>
<body>   
    <form id="form1" runat="server">
        <div class="container-fluid"> 
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>SESIONES DE CONSEJO</b>                    
                </div>  
                <div class="panel-body">                    
                    <asp:UpdatePanel runat="server" ID="updFiltros" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">                    
                                <div class="form-group">
                                     <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Tipo Consejo: </asp:Label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="col-sm-12 col-md-12">
                                            <asp:DropDownList runat="server" ID="ddlConsejo" CssClass="form-control" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:Label ID="lblComboFacultad" runat="server" CssClass="col-sm-1 col-md-1 control-label" Visible ="false">Facultad: </asp:Label>
                                    <div class="col-sm-3 col-md-3">
                                        <div class="col-sm-12 col-md-12">
                                            <asp:DropDownList runat="server" ID="ddlFacultad" CssClass="form-control" AutoPostBack="true" Visible="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="col-md-12">
                                        <asp:Button runat="server" ID="btnAgregar" CssClass="btn btn-success" Text="Agregar" />               
                                    </div>                                    
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView runat="server" ID="gvListaSesiones" CssClass="table table-striped table-bordered" DataKeyNames="codigo_scu, cod_tipoSesion,descripcion_scu,fecha_scu"
                                            AutoGenerateColumns="false">
                                        <Columns>                                    
                                        <%-- <asp:BoundField DataField="codigo_scu" HeaderText="ID" HeaderStyle-Width="5" />--%>
                                            <asp:BoundField DataField="descripcion_scu" HeaderText="SESION" HeaderStyle-Width="18%" />
                                            <asp:BoundField DataField="fecha_scu" HeaderText="FECHA" HeaderStyle-Width="18%" />
                                            <asp:BoundField DataField="nombre_con" HeaderText="CONSEJO" HeaderStyle-Width="18%" />  
                                            <asp:BoundField DataField="tipo_sesion" HeaderText="TIPO" HeaderStyle-Width="18%" /> 
                                             <asp:BoundField DataField="nombre_fac" HeaderText="FACULTAD" HeaderStyle-Width="18%" />                                        
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-edit"></span>'
                                                        CssClass="btn btn-info btn-sm btn-radius" ToolTip="Editar Sesion" CommandName="Editar"
                                                        CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa fa-trash"></span>'
                                                        CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Desactivar Sesion" CommandName="Eliminar"
                                                        OnClientClick="return alertConfirm(this, event, '¿Está seguro de deshabilitar esta sesión de consejo?', 'warning');"
                                                        CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#E33439" ForeColor="white" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataTemplate>
                                            <b>No se encontró información</b>
                                        </EmptyDataTemplate>
                                </asp:GridView>
                    
                                
                            </div>
                                </div>                                                                
                            </div>
                        </div>
                    </ContentTemplate>                  
                  
                    </asp:UpdatePanel>                    
                </div>
            </div>
        </div> 
         <!-- Modal Sesiones-->
        <div id="myModalSesion" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	        <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="Div2">
	                <asp:UpdatePanel ID="udpModalSesion" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Sesiones de Consejo</span> 
                                <button type="button" class="close" data-dismiss="modal">&times;</button>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div runat="server" id="divSesionActual" visible ="false">
                                        <div class="form-group">
                                           <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 control-label" for="">Sesion Actual:</asp:Label>                                    
                                           <div class="col-sm-9">
                                                <asp:TextBox ID="txtSesionActual" runat="server" CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                           </div> 
                                        </div>
                                        <div id="Div1" class="form-group" runat="server">
                                           <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 control-label" for="">Fecha Actual:</asp:Label>                                    
                                           <div class="col-sm-9">
                                                <asp:TextBox ID="txtFechaActual" runat="server" CssClass="form-control" Style="text-transform: uppercase" ReadOnly="true"></asp:TextBox>
                                            </div> 
                                        </div>
                                        <hr />
                                    </div>                                    
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label">Fecha: </asp:Label>
                                        <%--<label class="col-md-5">Publicación:</label>--%>
                                        <div class= "col-md-3">
                                            <div class="input-group date col-sm-12 col-md-12">
                                                <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control input-sm"
                                                    data-provide="datepicker"></asp:TextBox>
                                                <span class="input-group-addon bg" id="btn_calendIni"><i class="fa fa-calendar"></i></span>
                                            </div>
                                        </div>
                                        
                                     </div>        
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-3 control-label" Visible ="true">Tipo Sesión: </asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:DropDownList runat="server" ID="ddlTipoSesion" CssClass="form-control" AutoPostBack="true" Visible="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                  
                                    <%--<div class="form-group">
                                        <asp:Label ID="lblCargoPers" runat="server" CssClass="col-sm-3 col-md-3 control-label" Visible="true">Cargo de Personal: </asp:Label>
                                        <div class="col-sm-9 col-md-9">
                                            <asp:DropDownList runat="server" ID="ddlCargoPers" CssClass="form-control" AutoPostBack="false" Visible ="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>--%>
                                </div>                               
                                <div class="row">
                                    <div class="col-sm-1">
	                                    <asp:LinkButton ID="lbActualizarSesion" runat="server" CssClass="btn btn-success" ToolTip="Guardar">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                 <div class="col-sm-1">
	                                    <asp:TextBox ID="txtCodigoScu" runat="server" Visible="false"></asp:TextBox>
	                                </div>
                                </div>
                            </div>          
	                    </ContentTemplate>
	                </asp:UpdatePanel>
	            </div>
	        </div>
	    </div>    
    </form>
    
</body>
</html>
