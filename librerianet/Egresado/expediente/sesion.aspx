<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sesion.aspx.vb" Inherits="Egresado_expediente_sesion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
 
    <style>
    .content
       {
                padding:2px;
                border:1px solid #e33439;
                background-color:#FEFFE1;
                font-family:Verdana;
                font-size:10px;        
            }
        .head_titulo
        { 
            font-weight:bold; background-color:#e33439; color:White;
            padding:4px;        
        }
        .btn
        {
        padding:4px;
        font-weight:bold;
        font-size :12px;   
        text-decoration:none;
        border:1px solid #C2C2C2;
        color:Black;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="">
    <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">Mensaje de Campus - alumniUSAT</span>
            <br />
           <br />
            Se ha terminado su sesión en el campus virtual, por favor vuelva a ingresar.</div>
            
    </div>
</div>               
    </form>
</body>
</html>
