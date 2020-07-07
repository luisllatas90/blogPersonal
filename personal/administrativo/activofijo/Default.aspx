<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="activofijo_Default" %>
<html id="Html1" lang="en" runat="server">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <title>Activo Fijo</title>
    <link rel="stylesheet" type="text/css" href="../scripts/css/bootstrap.min.css"/>
	<link rel="stylesheet" href="../academico/assets/css/material.css?x=1"/>		
	<link rel="stylesheet" type="text/css" href="../academico/assets/css/style.css?y=4"/>
	
	<!-- activo fijo -->
    <link href="assets/css/style_af.css" rel="stylesheet" type="text/css" />
    
	<!-- custom scrollbar stylesheet -->
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> <!--320-->
	<script type="text/javascript" src="../academico/assets/js/jquery.js"></script>
	<script type="text/javascript" src="../academico/assets/js/bootstrap.min.js"></script>	
	<script type="text/javascript" src='../academico/assets/js/noty/jquery.noty.js'></script>
    <script type="text/javascript" src='../academico/assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../academico/assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src='../academico/assets/js/noty/notifications-custom.js'></script>
    <script type="text/javascript" src='../academico/assets/js/jquery-ui-1.10.3.custom.min.js'></script>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/> 

</head>
    <script type="text/javascript">
        jQuery(document).ready(function() {

        });

</script>
<body>
<form id="frmListaCaracteristicas" name="frmListaCaracteristicas" action="#"  > 
    <input type="hidden" id="param0" name="param0" value="" />    

    <div class="panel panel-piluku">
        <div class="panel-heading">
                <h3 class="panel-title">
                    Datos Personales					
                </h3>
        </div>                            																						
	    <div class="panel-body">									
	        <div class="col-md-12">

		        <div class="table-responsive">	        
                    <div class="panel-body">

                    <!--Default Form-->
                    <div class="form-group">
                        <label class="control-label active">Realice una breve descripción de los datos más resaltantes en cuanto a su desempeño profesional y/o laboral: <div class="diverror" id="error" style="visibility:hidden"><p>(*)</p></div></label> 
                        <textarea name="txtperfil" id="txtperfil" class="contador form-control" cols="50" maxlength="1000" rows="5">aab</textarea>							                    
                    </div>							                   
                
                </div>              
       
                <center>                        
                <a href="#" id="btnGuardar" class="btn btn-primary btn-lg" style="width:30%"><i class="ion-android-done"></i>&nbsp;Guardar Perfil</a>                
                </center>    
            <br>  
            </div>

            </div>
        </div>	
    </div>	

</form>
</body>
</html>