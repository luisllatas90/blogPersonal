<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OfertasLaborales.aspx.vb" Inherits="Egresado_campus_OfertasLaborales" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Consultar Ofertas Laborales</title>
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
       <script type="text/javascript" language="javascript">
               function showDiv(ctrl) {
                  if ($("#vermasO_" + ctrl).html() == "Ver más detalle") {
                       $("#vermasO_" + ctrl).html("Ocultar detalle");
                       $(".detalle_" + ctrl).fadeIn('fast');
                   } else {
                        $(".detalle_" + ctrl).fadeOut('fast');
                        $("#vermasO_" + ctrl).html("Ver más detalle");
                   }                  
               }
    </script>
    <style type="text/css">
    body {
            color: #2F4F4F;
            font-family: Verdana;
            font-size: 10px;
            padding-left:5px;
            padding-top:10px;                      
    }        
     .filaTitulo
     {
         color:White; background:#e33439; font-size:14px;padding:3px;
     }
      .filaborder
     {
        border-bottom:1px solid #e33439;
     }
     .filaTituloOferta
     {
        padding:7px;        
        background: #EFEDED;
        /*background: #FFFAE8; */
        font-weight:bold;        
        font-size:11px;   
        width:58%;
        color:#333232;
        border-right:3px solid #969696;
     }
     /*#DBD9D9*/
     
      .filaTituloOferta1
     {
        padding:7px;        
        background: #EFEDED;
        /*background: #FFFAE8; */
        font-weight:bold;        
        font-size:11px;   
        width:38%;
        /*color:#666464;*/
         color:#333232;
        /*border-left:1px solid blue;*/
     }
     
          
          .filaTituloOferta2
     {
        padding:7px;        
        background: #e33439;
        /*background: #FFFAE8; */
        font-weight:bold;        
        font-size:12px;   
        width:4%;
        color:#666464;
        
     }
     
     
     
     .CeldaEnlace 
     {
       
       width:0%;  
       text-align:right;    
        padding:5px;
      
     }

     .CeldaTituloOferta
     {
        font-weight:bold;        
        padding-left:5px; 
        width:10%;       
         /*font-size:10px;   */
     }
     .filaDescripcion
     {
         padding:7px;
         width:100%;
         text-align:justify;
     }
     
     .btn 
     {
        padding:4px;
        font-weight:bold;
        font-size :12px;   
        text-decoration:none;
        border:1px solid #C2C2C2;
        color:Black;
     }          
     
     
     .tablaOferta 
     {
         border: 1px solid #C2C2C2;
         width: 100%;                  
    }
    .style1
        {
            padding-top:10px;
            width: 20%;
        }
        
        .StyleVerMas
        {
           
            
            color:#e33439;
            text-align:center;
            font-size:11px;            
            cursor:hand;
            cursor:pointer;
            background:#F3F3F3;
            padding:3px;
            border: 1px solid #C2C2C2;
        }
        
        .StyleVerMas a:hover
        {
            text-decoration:underline;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" cellpadding=4>
	  <tr><td class="filaTitulo" colspan="3">&nbsp;Consulta de Ofertas Laborales</td></tr>	  
	  
	  <tr>
	
	  <td class="style1">Carrera Profesional<br /><asp:DropDownList ID="ddlCarrera" 
              runat="server" AutoPostBack="True" Font-Size="11px"></asp:DropDownList></td>
	  <td class="style1"><br />
	      <asp:DropDownList ID="ddlFecha" runat="server" AutoPostBack="True" 
              Visible="False">
          <asp:ListItem Selected="True" Value="0">Cualquiera</asp:ListItem>
          <asp:ListItem Value="1">Hoy y Ayer</asp:ListItem>
          <asp:ListItem Value="3">Últimos 3 días</asp:ListItem>
          <asp:ListItem Value="7">Últimos 7 días</asp:ListItem>
          <asp:ListItem Value="14">Últimos 14 días</asp:ListItem>
          <asp:ListItem Value="21">Últimos 21 días</asp:ListItem>
          <asp:ListItem Value="31">Últimos 31 días</asp:ListItem>
          </asp:DropDownList></td>
      <td class="style1"><br /></td>
	  </tr>
	  
	  <tr><td class="filaborder" colspan="3">
          <asp:Label ID="lblContador" runat="server" ForeColor="Maroon" Text="Label"></asp:Label>
          <br /></td></tr>
	</table>
	<br />
	
	<div id="dtablaOferta" runat="server"></div>

	
	
    </form>
 
</body>

</html>
