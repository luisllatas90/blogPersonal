<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdministrarActividadTipoFuncion.aspx.vb" Inherits="AdministrarActividadTipoFuncion" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html>
<html lang="es">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
 <meta name="google" value="notranslate">
    <title>Acceso Actividad por Función</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../../assets/css/material.css'/>
    <link href="../../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    

    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../../assets/js/materialize.js'></script>    
    
    <script type="text/javascript" src="../../../assets/js/jquery.dataTables.min.js?x=1"></script>	
<link rel='stylesheet' href='../../../assets/css/jquery.dataTables.min.css?z=1'/>
<head runat="server">
    <title></title>
    <style type="text/css">
    .desc
    { text-align:justify;
        
        }
    </style>
    <script type="text/javascript">
       var event = jQuery.Event("DefaultPrevented");
     $(document).trigger(event);
     $(document).ready(function() {
     var oTable = $('#grwTipoFuncion').DataTable({
         //"sPaginationType": "full_numbers",
         "bPaginate": false,
         "bFilter": true,
         "bLengthChange": false,
         "bSort": false,
         "bInfo": true,
         "bAutoWidth": true
     });


     });
    
        function ShowMessage(message, messagetype) {
            var cssclss;
            switch (messagetype) {
                case 'Success':
                    cssclss = 'alert-success'
                    break;
                case 'Error':
                    cssclss = 'alert-danger'
                    break;
                case 'Warning':
                    cssclss = 'alert-warning'
                    break;
                default:
                    cssclss = 'alert-info'
            }
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }

    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    
 <div class="container-fluid">

<div class="messagealert" id="alert_container"></div>
	 <asp:Panel CssClass="panel panel-primary" id="pnlLista"  runat="server" style="padding:0px;">   
		 <div class="panel panel-heading" >
			 <h5>ACCESO ACTIVIDAD POR FUNCIÓN</h5>
		</div>
		<div class="panel panel-body"  style="padding:3px;">   
		<div class="row">
				        <div class="col-md-7">
                           <div class="form-group">
                            <label class="col-md-3" for="ddlActividadBsq">
                                Actividad</label>
                            <div class="col-md-9">
                                <asp:DropDownList name="ddlActividadBsq" ID="ddlActividadBsq" runat="server" CssClass="form-control" AutoPostBack=true>
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            </div>
                         </div> 
                          <div class="col-md-2">   
                           <div class="form-group">
                            <div class="col-md-12">
                            <label class="col-md-8" for="spTotal">
                                 Total Asignados </label>					           
					        <div class="col-md-4">
					            <span class="badge" id="spTotal" runat="server">0</span>				           
				            </div>
                            </div>
                           </div>
                         </div>                             
                         <div class="col-md-3">           
                            <asp:LinkButton ID="btnBuscarTipoFuncion" runat="server" Text='<i class="fa fa-search"></i> Buscar'
                            CssClass="btn btn-primary btn-sm" ></asp:LinkButton>
                            <asp:LinkButton ID="btnGuardarTipoFuncion" runat="server" Text='<i class="fa fa-save"></i> Guardar'
                            CssClass="btn btn-success btn-sm" ></asp:LinkButton>
                             
                         </div>
                         
                         
		</div>
		  <div class="row">
                    <div class="col-md-12">  
                         <asp:GridView ID="grwTipoFuncion" runat="server" AutoGenerateColumns="false" 
                                 CssClass="display cell-border" 
                                 DataKeyNames="codigo_Tfu, descripcion_Tfu,sel,codigo_ActTfu" GridLines="None" 
                                 RowStyle-Font-Size="X-Small">
                                 <Columns>
                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="">
                                         <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                         <HeaderTemplate>
                                             <center>
                                                 <asp:CheckBox ID="chkall" runat="server" AutoPostBack="true" 
                                                     OnCheckedChanged="chckchanged" Text="" />
                                             </center>
                                         </HeaderTemplate>
                                         <ItemStyle HorizontalAlign="Center" Width="5%" />
                                         <ItemTemplate>
                                             <asp:CheckBox ID="chkElegir" runat="server" AutoPostBack="false"  />
                                         </ItemTemplate>
                                         <ItemStyle HorizontalAlign="Center" />
                                     </asp:TemplateField>
                                     <asp:BoundField DataField="descripcion_Tfu" HeaderStyle-Width="95%" 
                                         HeaderText="TIPO FUNCIÓN" />
                                 </Columns>
                                 <EmptyDataTemplate>
                                     No se ha tipos de funcion
                                 </EmptyDataTemplate>
                                 <HeaderStyle BackColor="#E33439" Font-Size="10px" ForeColor="White" 
                                     HorizontalAlign="Center" VerticalAlign="Middle" />
                                 <EditRowStyle BackColor="#FFFFCC" />
                                 <EmptyDataRowStyle CssClass="table table-bordered" ForeColor="Red" />
                             </asp:GridView>                 
                    </div>  
                
                </div> 
	   </div>
	</asp:Panel>

</div>
</form>
</body>
</html>
