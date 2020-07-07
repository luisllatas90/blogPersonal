<%@ Page Language="VB" AutoEventWireup="false" CodeFile="futuroempleo.aspx.vb" Inherits="frmpersona" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head id="Head1" runat="server">
    <title>Futuro Empleo</title>
	<link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    
    <style type="text/css">
        .style6
        {
            font-size:11px;
            font-weight:normal;
         }
         .style7
        {
            width: 217px;
        }
        .style8
        {
            font-size: 11px;
            font-weight: normal;
            width: 217px;
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
         <script>
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
         </script>
         
    </head>
      <body>	
	  <form id="form1" runat="server">
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">&nbsp;Futuro Empleo</td></tr>
	</table><br/>
	
	<table style="width: 100%" class="contornotabla" id="Table1" runat="server">                         
      <!--<tr><td colspan="2"><span class="azul" style="color:#e33439">&nbsp;Registrar Futuro Empleo</span></td></tr>-->
      <!--<tr><td class="style7">&nbsp;</td></tr>-->
      <%--<tr>
        <td class="style8"><b>¿Que Puesto te gustaría?</b></td>
        <td><asp:TextBox ID="txtpuesto" runat="server" Width="210px" CssClass="datos_combo"></asp:TextBox></td>
      </tr>--%>
      <tr>
        <td class="style8">Cargo</td>
        <td>
          <asp:TextBox ID="txtcargo" runat="server" Width="210px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style8">Tipo de Contrato</td>
        <td>
            <asp:DropDownList ID="dpTContrato" runat="server" CssClass="datos_combo"></asp:DropDownList>
          </td>
      </tr>
      <tr>
        <td class="style8">Área dentro de Empresa</td>
        <td>
          <asp:TextBox ID="txtarea" runat="server" Width="210px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style8">Lugar donde deseas trabajar</td>
        <td>
          <asp:DropDownList ID="dpcomunidad" runat="server" CssClass="datos_combo">
          </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="style8">Sector que te gustaría trabajar</td>
        <td>
           <asp:DropDownList ID="dpSector" runat="server" CssClass="datos_combo">             
            </asp:DropDownList>
         </td>
      </tr>
      <tr>
        <td class="style8">Expectativa remunerativa</td>
        <td>
          <asp:DropDownList ID="dpexpectativa" runat="server" CssClass="datos_combo">
            <asp:ListItem Value="-1">--seleccione--</asp:ListItem>
            <asp:ListItem Value="1000 - 1500">1000 - 1500</asp:ListItem>
            <asp:ListItem Value="1500 - 2000">1500 - 2000</asp:ListItem>
            <asp:ListItem Value="2000 - 2500">2000 - 2500</asp:ListItem>
            <asp:ListItem Value="2500 - 3000">2500 - 3000</asp:ListItem>
            <asp:ListItem Value="3000 - 3500">3000 - 3500</asp:ListItem>
            <asp:ListItem Value="3500 - 4000">3500 - 4000</asp:ListItem>
            <asp:ListItem Value="4000 - 4500">4000 - 4500</asp:ListItem>
            <asp:ListItem Value="4500 - 5000">4500 - 5000</asp:ListItem>
            <asp:ListItem Value="5000 a más">5000 a más</asp:ListItem>
          </asp:DropDownList>
         </td>
      </tr>
      <tr>
         <td class="style8"></td>
	    <td class="style6"><asp:HiddenField ID="hdcodigo_pso" runat="server" /></td>
	  </tr>
	  <tr><td class="style7">
          <asp:Label ID="Label1" runat="server" Font-Italic="True" Text="Label"></asp:Label>
          </td></tr>
	 </table>
	 <table style="width: 100%">
	  <tr>
        <td align="right">
            &nbsp;&nbsp;&nbsp;

          <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Enabled="true" 
                Text="Registrar" SkinID="BotonGuardar" />  
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
     