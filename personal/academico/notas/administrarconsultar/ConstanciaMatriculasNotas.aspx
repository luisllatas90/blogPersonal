<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConstanciaMatriculasNotas.aspx.vb" Inherits="academico_notas_administrarconsultar_ConstanciaMatriculasNotas" %>

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
    <link href="../../../assets/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Iconos -->    
    <link rel="stylesheet" href="../../../assets/fontawesome-5.2/css/all.min.css"/> 
    
    <!--Alertas css--> 
    <link href="../../../Alumni/css/sweetalert/animate.css" rel="stylesheet" type="text/css" />
    <link href="../../../Alumni/css/sweetalert/sweetalert2.min.css" rel="stylesheet" type="text/css" />
    
    
    
    
    <!--Jquery-->
    <script src="../../../assets/jquery/jquery-3.3.1.js" type="text/javascript"></script>
    <%-- <script src="assets/js/bootstrap.min.js" type="text/javascript"></script>--%>
    
    <!--Alertas js-->    
    <script src="../../../Alumni/js/sweetalert/es6-promise.auto.min.js" type="text/javascript"></script>
    <script src="../../../Alumni/js/sweetalert/sweetalert2.js" type="text/javascript"></script>
    
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
                        <div class="card-header">FICHA DE MATRÍCULAS Y NOTAS</div>
                        <div class="card-body">
                            <asp:UpdatePanel ID="udpFiltros" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                
                                    <div class="row">
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblTipReporte">Tipo de Reporte:</label>
                                        <div class="col-sm-3">
                                            <asp:DropDownList ID="ddlTipoReporte" runat="server" class="form-control" Enabled ="false" ></asp:DropDownList>
                                        </div>  
                                        <label class="col-sm-2 col-form-label form-control-sm" for="lblSemAca">Semestre. Académido:</label>
	                                    <div class="col-sm-3">
	                                        <asp:DropDownList ID="ddlSemAca" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
	                                    </div>
                                    </div> 
                                    <br />
                                   <div class="row">   
	                                    <label class="col-sm-2 col-form-label form-control-sm" for="lblSemAca">Código Alumno</label>
	                                    <div class="col-sm-3">
	                                        <asp:TextBox ID="txtCodigoAlu" runat="server" CssClass="form-control"></asp:TextBox>
	                                    </div>
	                                    <div class="col-sm-5">
	                                        <asp:TextBox ID="txtDescripcionAlu" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
	                                    </div>   
	                                    <div class="col-sm-1">
	                                        <asp:LinkButton ID="lbBuscaAlu" runat="server" CssClass="btn btn-info">
                                                <span><i class="fa fa-search"></i></span>
						                    </asp:LinkButton>
	                                    </div> 
	                                    <div class="col-sm-2">
	                                        <asp:TextBox ID="TxtCodAlu" runat="server" CssClass="form-control" Visible ="false" ></asp:TextBox>
	                                    </div>      
                                    </div> 
                                    <br />
                                    <div class="row">
                                         <div class="col-sm-2">
	                                        <asp:LinkButton ID="lbImprimir" runat="server" CssClass="btn btn-success">
                                                <span><i class="fa fa-print"></i></span> &nbsp; &nbsp;Imprimir
						                    </asp:LinkButton>
	                                    </div>       
                                    </div>
                                    <hr />
                                    <div class="row">                                        
                                        <iframe id="ifrmReporte" runat="server" src="" width="100%" height="600px" scrolling="no"
                                          frameborder="0"></iframe>                                        
                                    </div>                       
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<div class="card">
                                <div class="card-body">
                                     <iframe id="ifrmReporte" runat="server" src="" width="100%" scrolling="no"
                                     frameborder="0"></iframe>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>       
        </div>
    </form>
</body>
</html>
