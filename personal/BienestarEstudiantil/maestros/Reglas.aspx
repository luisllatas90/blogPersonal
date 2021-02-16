<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Reglas.aspx.vb"
    Inherits="BienestarEstudiantil_maestros_Reglas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Migraci&oacute;n Tr&acute;mite Virtual GyT</title>
    <link href="../../../private/estilo.css?z=x" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>    
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />  
    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <link href="private/timeline.css" rel="stylesheet" type="text/css" />
    <script src="private/jquery.loadmask.js" type="text/javascript"></script>
    <script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?y=1"></script>	
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    <script src="../../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>
    <script src="../../assets/js/promise.min.js" type="text/javascript"></script>
    
        <script type="text/javascript">
            var oTable;
            $(document).ready(function() {
                oTable = $('#gvDatos').DataTable({
                    //"sPaginationType": "full_numbers",
                    "bPaginate": false,
                    "bFilter": false,
                    "bLengthChange": false,
                    "bSort": true,
                    "bInfo": true,
                    "bAutoWidth": true
                });


            });

            function ShowMessage(message, messagetype) {

                var cssclass;
                switch (messagetype) {
                    case 'Success':
                        cssclass = 'alert-success'
                        break;
                    case 'Error':
                        cssclass = 'alert-danger'
                        break;
                    case '1':
                        cssclass = 'alert-danger'
                        break;

                    case 'Warning':
                        cssclass = 'alert-warning'
                        break;
                    default:
                        cssclass = 'alert-info'
                }

                if (cssclass != 'alert-danger') {
                    $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
                } else {
                    $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
                }
            }

            function fnConfirmacion(ctrl) {

                var defaultAction = $(ctrl).prop("href");
                Swal.fire({
                    title: '¿Está seguro que desea migrar el tramite al nuevo sistema de GyT?',
                    text: "Luego no podrá revertir",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si',
                    cancelButtonText: 'No'
                }).then(function(result) {
                    if (result.value == true) {
                        eval(defaultAction);
                    }
                })
                /*
                if (confirm('¿Está seguro que desea dar conformidad a tesis?')) {
                return true;
                } else {
                return false;
                }*/
            }


          
        </script>
    
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
     <div class="container-fluid">     
        <div class="messagealert" id="alert_container">
        </div>
        
           <div class="panel panel-info" id="pnlLista" runat="server">
            <div class="panel-heading" style="text-align:center; font-weight:bold;">
               REGLAS
            </div>
            <div class="panel-body">
                <div class="row">
                  
                    <div class="col-md-6">
                        <div class="form-group">
                            <label for="txtnroTramite" class="col-md-5 col-form-label">
                            Nombre:
                            </label>
                            <div class="col-md-7">
                                <asp:TextBox ID="txtbsqnombre" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
             
                      
                        <div class="col-md-6">
                        <div class="btn-group">
                      
                        <asp:LinkButton ID="btnConsultar" runat="server" Text='<i class="fa fa-search"></i> Buscar'
                        CssClass="btn btn-info btn-sm" ></asp:LinkButton>
                        <asp:LinkButton ID="btnCrear" runat="server" Text='<i class="fa fa-plus"></i> Crear'
                            CssClass="btn btn-success btn-sm"></asp:LinkButton>
                      
                        
                          
                       
                       </div>
                        </div>
                </div>
                         
                    
            </div>
             <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="table-responsive">
                            
                            <asp:GridView ID="gvDatos" runat="server" Width="100%" DataKeyNames=""
                                    CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" ShowHeader="true">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" ControlStyle-Width="5%" />
                                        <asp:BoundField DataField="CODIGO" HeaderText="CODIGO"  ControlStyle-Width="15%"/>
                                        <asp:BoundField DataField="NOMBRE" HeaderText="NOMBRE"  ControlStyle-Width="25%"/>
                                        <asp:BoundField DataField="DESCRIPCION" HeaderText="DESCRIPCION"  ControlStyle-Width="45%"/>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPCIONES"  ControlStyle-Width="20%">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEditar" runat="server" Text="Editar"  CssClass="btn btn-warning btn-xs" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />                                               
                                                 <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"  CssClass="btn btn-danger btn-xs" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />                                               
                                            </ItemTemplate>
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron registros!
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="11px" />
                                    <RowStyle Font-Size="11px" />
                                    <EditRowStyle BackColor="#ffffcc" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                         </div>
                    </div> 
                </div>
             </div>
           </div>
           <div class="panel panel-info" id="pnlRegistro" runat="server">
            <div class="panel-heading">
               Registro
            </div>
             <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <label class="col-md-2" for="txtnombre"> Nombre: </label>
				        <div class="col-md-10">
				            <div class="form-group" >
					           <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
				            </div>
				        </div>
				    </div>
  
                </div>
  <div class="row">
                    <div class="col-md-12">
                        <label class="col-md-2" for="txtdescripcion"> Descripcion: </label>
				        <div class="col-md-10">
				            <div class="form-group" >
					           <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control"></asp:TextBox>
				            </div>
				        </div>
				    </div>
  
                </div>
             </div>
             <div class="panel-footer">

             <div class="btn-group">
                    <asp:LinkButton ID="btnAceptar" runat="server" Text='<i class="ion-android-done"></i> Aceptar'
                    CssClass="btn btn-success btn-sm" ></asp:LinkButton>
                    <asp:LinkButton ID="btnCerrar" runat="server" Text='<i class="ion-close"></i> Cerrar'
                        CssClass="btn btn-danger btn-sm"></asp:LinkButton>
             </div>
             </div>
           </div>    
           <asp:HiddenField ID="hdtimelineactive" runat="server" />
                        <asp:HiddenField ID="hdtimelinechk" runat="server" />
                        <asp:HiddenField ID="hddtareq" runat="server" />
     </div>
    </form>
</body>
</html>
