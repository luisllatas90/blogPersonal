<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultaSaeta.aspx.vb" Inherits="GestionDocumentaria_frmConsultaSaeta" %>

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
     <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" /> <!--hcano-->
    <!-- Iconos -->    
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css"/> 
    
    <!--Alertas css-->    
    <link href="../Alumni/css/sweetalert/animate.css" rel="stylesheet" type="text/css" />
    <link href="../Alumni/css/sweetalert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    
      <!--Jquery-->
    <script type="text/javascript" src='../../assets/js/jquery.js'></script>
    
    <!--Modal -->
    <%-- <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js" type="text/javascript"></script>--%>
        <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
    
    <!--Alertas js-->    
    <script src="../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script> 
      
      <!--Datetimepicker--> 
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    <!-- Datatable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
     
    <script type="text/javascript" language="javascript">
        
        /* Mostrar mensajes al usuario. */
        function showMessage(message, messagetype) {              
            swal({ 
                title: message,
                type: messagetype ,
                confirmButtonText: "OK" ,
                confirmButtonColor: "#45c1e6"
            }).catch(swal.noop);
        } /* fin de la funcion */
        
        /* formato dataTable*/      
        function formatoGrilla() {
            
            $('#<%=gvListaDiplomas.ClientID%>').DataTable({
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
            
        }/*  fin formato grilla */
        
    </script>
    
    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        /*
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: Black;
            border-color: Black;
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle;
            font-weight: bold;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <b>CONSULTAR SAETA</b>                    
                </div>                
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12">
                                <asp:Button runat="server" ID="btnConsultar" CssClass="btn btn-success" Text="Consultar" />               
                            </div>                                    
                        </div>
                        <br />                        
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView runat="server" ID="gvListaDiplomas" CssClass="table table-striped table-bordered" DataKeyNames=""
                                AutoGenerateColumns="false">
                                    <Columns>                                    
                                            <%-- <asp:BoundField DataField="codigo_scu" HeaderText="ID" HeaderStyle-Width="5" />--%>
                                        <asp:BoundField DataField="codigoOperacionGrupo" HeaderText="COD. ENVIO" HeaderStyle-Width="18%" />
                                        <asp:BoundField DataField="egresado" HeaderText="EGRESADO" HeaderStyle-Width="18%" />
                                        <asp:BoundField DataField="mensajeOperacionFirma" HeaderText="RESPUESTA - ESTADO PROVEEDOR / " HeaderStyle-Width="18%" />  
                                        <%--<asp:BoundField DataField="tipo_sesion" HeaderText="TIPO" HeaderStyle-Width="18%" /> 
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
                                        --%>   
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
            </div>
        </div>
    </form>    
</body>
</html>
