<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="avisosusat_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
		   body{ background: #000;}
        .img 
        {
            /*
                :: Css para centrar la imagen ::
            */
                width: 800px;       /*width: 580px;*/
                height: 600px;      /*height: 390px;*/
                
                margin-top: -275px;     /*margin-top: -195px;*/
                margin-left: -390px;    /*margin-left: -290px;*/
                
                left: 50%;
                top: 50%;
                position: absolute;
            }
    </style>
    
</head>
<body>
    <div>
         <!-- <img  src="images/paraprueba.jpg"/>-->
        <asp:Image ID="Image1" CssClass="img" runat="server"/>
        
    </div>
</body>
</html>
