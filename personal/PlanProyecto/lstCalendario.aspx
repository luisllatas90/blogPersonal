<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstCalendario.aspx.vb" Inherits="PlanProyecto_lstCalendario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />            
    <link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script> 
    <script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>	     
     <style type="text/css">
        .tabla{
            position:absolute;
            top:0px;            
            left:0px;            
            bottom: 130px;            
        }
        .tablaBusqueda{
            position:absolute;
            top:0;
            left:0;            
            /*height:100%;*/
        }       
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id='Cal1' class='tabla' runat="server">                
    </div>
    </div>    
    </form>    
</body>
</html>
