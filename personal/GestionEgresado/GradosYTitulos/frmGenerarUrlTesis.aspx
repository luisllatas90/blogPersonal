<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarUrlTesis.aspx.vb" Inherits="GestionEgresado_GradosYTitulos_frmGenerarUrlTesis" %>

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
     <link href="../../assets/css/bootstrap.min.css?x=2" rel="stylesheet" type="text/css" /> <!--hcano-->
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
    <!-- Datatable -->
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.9/js/jquery.dataTables.min.js"></script>
    <link type="text/css" rel="stylesheet" href="https://cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" />
       
    <!--Alertas js-->    
    <script src="../../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script> 
    
    
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
        function openModalUrl() {
            $('#myModalUrl').modal('show');
        };
        function closeModalUrl() {
            $('#myModalUrl').modal('hide');
        };
        
        function validarUrl() {
           var url = document.getElementById("txtUrl").value;
            
           //var pattern = /(ftp|http|https):\/\/(\w+:{0,1}\w*@)?(\S+)(:[0-9]+)?(\/|\/([\w#!:.?+=&%@!\-\/]))?/;
           var pattern = /(https?:\/\/)/;
           if (pattern.test(url)) {
                
                 document.getElementById('<%=hfValidaUrl.ClientID%>').value = 'true';
                                                  //document.getElementById("titulo").innerHTML;
           } else {
            
             document.getElementById('<%=hfValidaUrl.ClientID%>').value = 'false';}
             
            }
            
           
        
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
        <asp:ScriptManager ID="ScriptManager" runat="server">
        </asp:ScriptManager> 
        <div class="container-fluid">
            <div class="panel panel-default">
                 <div class="panel-heading"><b>GENERAR URL TESIS</b></div>      
                 <div class="panel-body">
                    <asp:UpdatePanel ID="udpListadoConf" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false" >
	                    <ContentTemplate>
	                        <div id="divLista" runat="server" visible="true">
	                            <div class="form-horizontal">
	                                <div class="form-group">
	                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 control-label" for="">Estado:</asp:Label>                                    
                                        <div class="col-sm-4">                              
                                            <asp:DropDownList ID="ddlEstado" runat="server" class="form-control" AutoPostBack = "true">                                                
                                            </asp:DropDownList>
                                        </div>
	                                </div>
	                                <div class="row">
	                                    <div class="col-md-12">
                                            <asp:GridView ID="gvListaTramTitulosUrl" runat="server" CssClass="table table-striped table-bordered" 
                                                AutoGenerateColumns="False" DataKeyNames="codigo_tes, titulo_tes,url_Tes, codigo_dta, codigo_tfu, estadoAprobacion">
                                                <RowStyle Font-Size="12px" /> 
                                                    <Columns>
                                                        <asp:BoundField DataField="fechaReg_trl" HeaderText="FECHA" HeaderStyle-Width="8%" />                                                                                                                                 
                                                        <asp:BoundField DataField="descripcion_ctr" HeaderText="TRAMITE" HeaderStyle-Width="20%" />                                  
                                                        <asp:BoundField DataField="alumno" HeaderText="ALUMNO" HeaderStyle-Width="20%" />
                                                        <asp:BoundField DataField="Titulo_Tes" HeaderText="TESIS" HeaderStyle-Width="32%" />
                                                        <asp:BoundField DataField="url_Tes" HeaderText="URL TESIS" HeaderStyle-Width="12%" />
                                                        <asp:TemplateField HeaderText="URL" HeaderStyle-Width="8%">            
			                                                <ItemTemplate>
                                                                <asp:LinkButton ID="btnSelEditarUrl" ToolTip="Generar URL Tesis" runat="server" 
					                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
					                                                CommandName="editUrl" CssClass="btn btn-success btn-sm">
                                                                    <span><i class="fa fa-edit"></i></span>
						                                        </asp:LinkButton>						                                         
                                                            </ItemTemplate>                                            
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#E33439" Font-Bold="True" Font-Size="12px" ForeColor="White" />                                 
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
        <!-- Modal Documento-->
        <div id="myModalUrl" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
	        <div class="modal-dialog modal-lg">
	            <div class="modal-content" id="Div2">
	                <asp:UpdatePanel ID="udpModalUrl" runat="server" UpdateMode="Conditional" >
	                    <ContentTemplate>
	                        <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
				                <span class="modal-title">Generar URL de tesis</span> 
                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                            </div>
                            <div class="modal-body">
                                <div class="form-horizontal">
                                    <div class="form-group">
	                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 control-label" for="">Tesis:</asp:Label>                                    
                                       <div class="col-sm-10">
                                            <asp:TextBox ID="txtDescTesis" runat="server" CssClass="form-control" Style="text-transform: uppercase" TextMode="MultiLine" TabIndex="3"></asp:TextBox>
                                        </div>     
	                                </div>
	                                 <div class="form-group">
	                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 control-label" for="">URL:</asp:Label>                                    
                                       <div class="col-sm-10">
                                            <asp:TextBox ID="txtUrl" runat="server" placeholder="http://hdl.handle.net/00.000.0000/0000" CssClass="form-control"></asp:TextBox>
                                            <asp:HiddenField ID="hfValidaUrl" runat="server" />
                                        </div>     
	                                </div>
                                </div>                               
                                <div class="row">
                                    <div class="col-sm-2">
	                                    <asp:LinkButton ID="lbGuardarURL" runat="server" CssClass="btn btn-success" ToolTip="Guardar" OnClientClick="validarUrl()">
                                        <span><i class="fa fa-save"></i></span> &nbsp; &nbsp;Guardar
						                </asp:LinkButton>
	                                </div>
	                                 <div class="col-sm-2">
	                                    <asp:LinkButton ID="lbCerrarMod" runat="server" CssClass="btn btn-danger" ToolTip="Cerrar" >
                                        <span><i class="fa fa-times"></i></span> &nbsp; &nbsp;cerrar
						                </asp:LinkButton>
	                                </div>
	                                
	                                <div class="col-sm-1">
	                                    <asp:TextBox ID="txtCodigoTesis" runat="server" Visible="false"></asp:TextBox>
	                                </div>
	                                 <div class="col-sm-1">
	                                   <asp:TextBox ID="txtCodigoDta" runat="server" Visible="false"></asp:TextBox>
	                                </div>
	                                 <div class="col-sm-1">
	                                  <asp:TextBox ID="txtUrlTesis" runat="server" Visible="false"></asp:TextBox>
	                                </div>
	                                 <div class="col-sm-1">
	                                  <asp:TextBox ID="txtCodigoTfuFinal" runat="server" Visible="false"></asp:TextBox>
	                                </div>
	                                 <div class="col-sm-1">
	                                  <asp:TextBox ID="txtEstadoAprobacion" runat="server" Visible="false"></asp:TextBox>
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
