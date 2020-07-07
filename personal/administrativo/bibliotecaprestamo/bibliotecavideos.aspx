<%@ Page Language="VB" AutoEventWireup="false" CodeFile="bibliotecavideos.aspx.vb" Inherits="bibliotecavideos" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html>
<html lang="es">
<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
 <meta name="google" value="notranslate">
    <title>Videos Biblioteca</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css'/>
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    

    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../assets/js/materialize.js'></script>    
<head runat="server">
    <title></title>
    <style type="text/css">
    .desc
    { text-align:justify;
        
        }
    </style>
    <script type="text/javascript">

    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    
 <div class="container-fluid">


	 <asp:Panel CssClass="panel panel-primary" id="pnlLista"  runat="server" style="padding:0px;">   
		 <div class="panel panel-heading" >
			 <h5>Video Biblioteca</h5>
		</div>
		<div class="panel panel-body"  style="padding:3px;">   
		<div class="row">
				<div class="col-md-12">
						 <iframe src="https://drive.google.com/file/d/16wmkj25dbiUqZtSe6876BfvZl_RftFDX/preview" width="100%" height="480"></iframe>
				</div>
		</div>
	   </div>
	</asp:Panel>

</div>
</form>
</body>
</html>
