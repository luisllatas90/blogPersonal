<%@ Page Language="VB" AutoEventWireup="false" CodeFile="_jsfrmGestionComiteAreaLinea.aspx.vb" Inherits="DirectorInvestigacion_administrarAreasLineasTematicas" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Investigación :: Unidades de Investigación</title>    
    <link href="private/jquery.treeview.css" rel="stylesheet" type="text/css" />
    <script src="private/jquery.js" type="text/javascript"></script>
    <script src="private/jquery.cookie.js" type="text/javascript"></script>
    <script src="private/jquery.treeview.js" type="text/javascript"></script>

    <script src="private/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="private/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    
    <script type="text/javascript">
            
        $(document).ready(function() {
            $("#red").treeview({
                animated: "fast",
                collapsed: false,
                unique: false,
                persist: "cookie",
                toggle: function() {
                    window.console && console.log("%o was toggled", this);
                }
            });

            $(".ifancybox").fancybox({
                'width': '60%',
                'height': '30%',
                'autoScale': false,
                'transitionIn': 'elastic',
                'transitionOut': 'elastic',
                'type': 'iframe'
            });
           
            
        });
        
        function showdiv(dx, ctrl) {
            if (dx==1) {
                $("#" + ctrl).fadeIn('slow');
                
            } else {
                $("#" + ctrl).hide();
            }
          
        }   
    </script>
    
    <style type="text/css">    
    html>body {
	    font-size: 16px; 
	    font-size: 68.75%;	   	    
    }
    body {
	    font-family: Verdana, helvetica, arial, sans-serif;
	    font-size: 68.75%;
	    background: #fff;
	    color: #203360;
    }
    A{ text-decoration:none; cursor:hand;}
    A:link {text-decoration:none;color:#0000cc;}
    A:visited {text-decoration:none;color:#ffcc33;}
    A:active {text-decoration:none;color:#ff0000;}
    A:hover {text-decoration:underline;color:#999999;}
    img {border-style:none;} 
    
    .divcontent
    {
        border:0px solid; font-weight:lighter;
        }
    </style>
   
</head>
<body bgcolor="#0000cc">

    
       <div style="border-bottom:1px solid #888888; font-size:12px; font-weight:bold;">Gestión de Áreas y Líneas de Investigación</div>
       <br /><br /><br />
       <div id="DivContenedor" runat="server" class="divcontent" >     
       </div>
       
       <div id="OpMenu" style="display:none;padding-left:10px;">
          <a id="enlace" class="ifancybox"  style="color: #999999; text-decoration:none;" href="#">
            <img src="private/images/add.png"/> Agregar Área</a>
       </div>

</body>
</html>

