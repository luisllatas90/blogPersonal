<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionComiteAreaLinea.aspx.vb" Inherits="DirectorInvestigacion_administrarAreasLineasTematicas" %>

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
                collapsed: true,
                unique: false,
                persist: "cookie",
                toggle: function() {
                    window.console && console.log("%o was toggled", this);
                }
            });

            $(".ifancybox").fancybox({
                'width': '60%',
                'height': '45%',
                'autoScale': true,
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
    a{ text-decoration:none; cursor:hand;}
    
A:link {text-decoration:none;color:#999999; font-weight:normal;}
A:visited {text-decoration:none;color:#999999; font-weight:normal;}
A:hover {text-decoration:underline;color:#5972a7; font-weight:normal;}

img {border-style:none;} 
    </style>
   
</head>
<body bgcolor="#0000cc">

    
       <div style="background:#d1ddef;color:#2f4f4f; font-size:12px; font-weight:bold; height:35px; text-align:left; padding:10px; border:1px solid #c2cff1;">Gestión de Áreas y Líneas de Investigación                     
       </div>
 
        
        <div id="DivContenedor" runat="server" style="border:0px solid; font-weight:lighter;border:1px solid #c2cff1;padding-top:10px; font-weight:bold;" >
        
        </div>
 
    
            

</body>
</html>

