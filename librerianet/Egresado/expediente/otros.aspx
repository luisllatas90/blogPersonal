<%@ Page Language="VB" AutoEventWireup="false" CodeFile="otros.aspx.vb" Inherits="frmpersona" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <title>Datos Adicionales</title>
	<link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    
      <style type="text/css">
        .style1
          {
              width: 97px;
          }
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
      </style>
      
      <script type="text/javascript">
        $(document).ready(function() {                    
            $(".contador").each(function() {
                var longitud = $(this).val().length;
                if (longitud == 0) {
                    longitud = 500;
                }
                var nueva_longitud = $(this).val().length;
                $(this).parent().find('#longitud_textarea').html('Max 500 caracteres. <i>Quedan: ' + (500 - nueva_longitud) + ' caracteres</i>');                
                
                $(this).keyup(function() {
                    var nueva_longitud = $(this).val().length;
                    $(this).parent().find('#longitud_textarea').html('<i>Max 500 caracteres. Quedan: ' + (500 - nueva_longitud) + ' caracteres</i>');
                    if (nueva_longitud >= "500") {
                        $('#longitud_textarea').css('color', '#ff0000');
                        var text = $(".contador").val();
                        var new_text = text.substr(0, 500);
                        $(".contador").val(new_text);
                        var nueva_longitud = $(this).val().length;
                        $(this).parent().find('#longitud_textarea').html('Max 500 caracteres. <i>Quedan: ' + (500 - nueva_longitud) + ' caracteres</i>');
                    }
                });
            });

        });
        $(".CallFancyBox_DeclaracionJurada").live("click", function() {
            $.fancybox({
                'width': '80%',
                'height': '300px',
                'autoScale': true,
                'type': 'inline',
                'href': '#divdj'
            });
            return false;
        });

        function acepta() {
            form1.submit();
        }
        function txtOtros_onclick() {

        }

      </script>
    </head>
  <body>	
	  <form id="form1" runat="server">
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">&nbsp;Datos Adicionales</td></tr>
	</table><br/>
	
	<table style="width: 100%" class="contornotabla" id="Table1" runat="server">                         
      <tr><td colspan="2"></td></tr>
      <tr><td class="style1">&nbsp;</td></tr>
      <tr>
        <td class="style1">Registre sus Hobbies y/u otros datos adicionales</td>
        <td>
             <textarea ID="txtOtros" runat="server" class="contador datos_combo" cols="50" 
                        maxlength="500" rows="5" style="Width:632px;" onclick="return txtOtros_onclick()"></textarea>
                    <div ID="longitud_textarea">
           </td>
      </tr>
     
      <tr>
         <td class="style13"></td>
	    <td class="style12"><asp:HiddenField ID="hdcodigo_pso" runat="server" /></td>
	  </tr>
	  <tr><td>&nbsp;</td></tr>
	 </table>
	 <table style="width: 100%">
	  <tr>
        <td align="right">
          <asp:Button ID="Button2" runat="server" CssClass="guardar" Enabled="true" 
                Text="Anterior" SkinID="BotonGuardar" Width="96px" />&nbsp;&nbsp;&nbsp;
          <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar CallFancyBox_DeclaracionJurada" Enabled="true" Text="Finalizar" SkinID="BotonGuardar" />
          
       &nbsp;
          
        </td>
      </tr>
    </table>
<input type="hidden" value="0" id="aceptaDj" name="aceptaDj" runat="server"/>
<div style="display: none;">
    <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">DECLARACIÓN JURADA</span>
            <br />
           <br />
           <span class="head_name">YO <b><asp:Label ID="lblnombre" runat="server" Text="Label"></asp:Label></b>&nbsp;declaro que:</span>
            <br />
           <br />
           <span class="content_item">
           <b>1.-</b> Los datos que se consignan a continuación tienen  carácter de <b>DECLARACION JURADA</b>.<br /> 
           Solo el egresado es personalmente responsable de la veracidad de los mismos.      
           <br />
           <br />
           <b>2.-</b> La USAT facilitará el envío de la información consignada en el CV a las empresas que soliciten egresados USAT 
            <br />
           <br />
           <b>3.-</b>La información consignada en el CV deberá ser actualizada por el egresado, cada vez que ingrese al módulo <b>Bolsa de trabajo - alumniUSAT</b>.
              <br />
           <br />              
           </span>         
           <input type="button" class="guardar" value="Acepto" onclick="acepta();" />  
           <input type="button" class="guardar" value="No Acepto" onclick="javascript:jQuery.fancybox.close();" />          
        </div>
    </div>
</div>               
    </form>
  </body>
 </html>