<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ExperienciaLaboralRegistrar.aspx.vb" Inherits="ExperienciaLaboralRegistrar" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
  <head runat="server">
    <title>Experiencia Profesional</title>
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../jquery/jquery-ui.css" />
    <script src="../jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="../jquery/jquery-ui.js" type="text/javascript"></script>
    <script src="../jquery/empresas.js" type="text/javascript"></script>
    <script src="../jquery/ciudades.js" type="text/javascript"></script>
    <script src="../jquery/sectores.js" type="text/javascript"></script>
    <script src="../jquery/areas.js" type="text/javascript"></script>
    <script src="../jquery/cargos.js" type="text/javascript"></script>
    <script src="../jquery/tipocontrato.js" type="text/javascript"></script>   
    
    
    <script type="text/javascript">
        $(document).ready(function() {
            $(".contador").each(function() {
                var longitud = $(this).val().length;
                if (longitud == 0) {
                    longitud = 5000;
                }
                $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + longitud + ' caracteres</i>');
                $(this).keyup(function() {
                    var nueva_longitud = $(this).val().length;
                    $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + (5000 - nueva_longitud) + ' caracteres</i>');
                    if (nueva_longitud >= "5000") {
                        $('#longitud_textarea').css('color', '#ff0000');
                        var text = $(".contador").val();
                        var new_text = text.substr(0, 5000);
                        $(".contador").val(new_text);
                        var nueva_longitud = $(this).val().length;
                        $(this).parent().find('#longitud_textarea').html('<i>Quedan: ' + (5000 - nueva_longitud) + ' caracteres</i>');
                    }
                });
            });

        });        
</script>
 
    
    <style type="text/css">
        body {
            color: #2F4F4F;
            font-family: "Trebuchet MS","Lucida Console",Arial,san-serif;
            
            
           
        }      
        .style18
        {
           
            font-weight: normal;
            }
        
           
          .content
            {
                padding:5px;
                border:1px solid #e33439;
                background-color:#FEFFE1;
                font-family:Verdana;
                font-size:12px;        
            }
        .head_titulo
        { 
            font-weight:bold; background-color:#e33439; color:White;
            padding:4px;        
        }
    
        .guardar {border:1px solid #C0C0C0; background:#FEFFE1; width:80; font-family:Tahoma; font-size:8pt; font-weight:bold; }
        a { text-decoration:none; color:Black;}
     form 
     {
         margin:0px;
        }
 
        #txtdescripcion
        {
            width: 483px;
        }
 
        .style21
        {
          
        }
 
    </style>
  </head>
  <body>	
	  <form id="form1" runat="server" style="">	    
	  <asp:Panel ID="PanelDJ" runat="server" meta:resourcekey="PanelDJResource1">
        <div id="divdj" runat="server">
        <div class="content">       
           <br />
           <span class="head_titulo">DECLARACIÓN JURADA</span>
            <br />
            <br />
           <br />
           <span class="">YO <b><asp:Label ID="lblnombre" runat="server" Text="Label" 
                meta:resourcekey="lblnombreResource1"></asp:Label></b>&nbsp;declaro que:</span>
            <br />
           <br />
           <span class="">
           <b>1.-</b> Los datos que se consignan a continuación tienen  carácter de <b>DECLARACION JURADA</b>.<br /> 
           Solo el egresado es personalmente responsable de la veracidad de los mismos.      
           <br />
           <br />
            <br />
           <b>2.-</b> La USAT facilitará el envío de la información consignada en el CV a las empresas que soliciten egresados USAT 
            <br />
            <br />
           <br />
           <b>3.-</b>La información consignada en el CV deberá ser actualizada por el egresado, cada vez que ingrese al módulo <b>Bolsa de trabajo - alumniUSAT</b>.
              <br />
            <br />
           <br />              
               <asp:Button ID="btnAcepta" runat="server"  CssClass="guardar" Text="Acepto" 
                meta:resourcekey="btnAceptaResource1" />
               &nbsp;
               <asp:Button ID="btnNoAcepta" runat="server"  CssClass="guardar" 
                Text="No Acepto" meta:resourcekey="btnNoAceptaResource1" />
           </span>         
        
        </div></div>
                         </asp:Panel>
                       
	  <asp:Panel ID="PanelRegistro" runat="server" 
          meta:resourcekey="PanelRegistroResource1">
      <table style="width: 100%" class="" id="" cellpadding="2">  
             <tr><td style="color:White; background:#e33439;padding:3px;" colspan=2>
          &nbsp;Registrar/Actualizar Experiencia Laboral</td></tr>          
              <tr>
                <td class="style18">Especificar Mes y Año</td>
                <td class="style21">
                    Inicio:&nbsp;<asp:DropDownList ID="dpMInicio" runat="server" 
                        ToolTip="Mes de Inicio" Width="90px" meta:resourcekey="dpMInicioResource1">
                        <asp:ListItem Value="0" meta:resourcekey="ListItemResource1">MES</asp:ListItem>
                        <asp:ListItem Value="01" meta:resourcekey="ListItemResource2">Enero</asp:ListItem>
                        <asp:ListItem Value="02" meta:resourcekey="ListItemResource3">Febrero</asp:ListItem>
                        <asp:ListItem Value="03" meta:resourcekey="ListItemResource4">Marzo</asp:ListItem>
                        <asp:ListItem Value="04" meta:resourcekey="ListItemResource5">Abril</asp:ListItem>
                        <asp:ListItem Value="05" meta:resourcekey="ListItemResource6">Mayo</asp:ListItem>
                        <asp:ListItem Value="06" meta:resourcekey="ListItemResource7">Junio</asp:ListItem>
                        <asp:ListItem Value="07" meta:resourcekey="ListItemResource8">Julio</asp:ListItem>
                        <asp:ListItem Value="08" meta:resourcekey="ListItemResource9">Agosto</asp:ListItem>
                        <asp:ListItem Value="09" meta:resourcekey="ListItemResource10">Setiembre</asp:ListItem>
                        <asp:ListItem Value="10" meta:resourcekey="ListItemResource11">Octubre</asp:ListItem>
                        <asp:ListItem Value="11" meta:resourcekey="ListItemResource12">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12" meta:resourcekey="ListItemResource13">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="dpAñoInicio" runat="server" 
                        ToolTip="Año de Inicio" Width="90px" 
                        meta:resourcekey="dpAñoInicioResource1">
                    </asp:DropDownList>
                    &nbsp;&nbsp;<br />
<asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" Checked="True" 
                        Text="Hasta la actualidad" meta:resourcekey="CheckBox1Resource1" />
                  </td>
              </tr>
                 <tr>
                     <td class="style18">&nbsp;</td>
                     <td class="style21">
                         <asp:Label ID="Label1" runat="server" Text="Fin:  " Visible="False" 
                             meta:resourcekey="Label1Resource1"></asp:Label>
                         &nbsp;&nbsp;&nbsp;
                         <asp:DropDownList ID="dpMFin" runat="server" 
                             ToolTip="Mes de Fin" Visible="False" Width="90px" 
                             meta:resourcekey="dpMFinResource1">
                             <asp:ListItem Value="0" meta:resourcekey="ListItemResource14">MES</asp:ListItem>
                             <asp:ListItem Value="01" meta:resourcekey="ListItemResource15">Enero</asp:ListItem>
                             <asp:ListItem Value="02" meta:resourcekey="ListItemResource16">Febrero</asp:ListItem>
                             <asp:ListItem Value="03" meta:resourcekey="ListItemResource17">Marzo</asp:ListItem>
                             <asp:ListItem Value="04" meta:resourcekey="ListItemResource18">Abril</asp:ListItem>
                             <asp:ListItem Value="05" meta:resourcekey="ListItemResource19">Mayo</asp:ListItem>
                             <asp:ListItem Value="06" meta:resourcekey="ListItemResource20">Junio</asp:ListItem>
                             <asp:ListItem Value="07" meta:resourcekey="ListItemResource21">Julio</asp:ListItem>
                             <asp:ListItem Value="08" meta:resourcekey="ListItemResource22">Agosto</asp:ListItem>
                             <asp:ListItem Value="09" meta:resourcekey="ListItemResource23">Setiembre</asp:ListItem>
                             <asp:ListItem Value="10" meta:resourcekey="ListItemResource24">Octubre</asp:ListItem>
                             <asp:ListItem Value="11" meta:resourcekey="ListItemResource25">Noviembre</asp:ListItem>
                             <asp:ListItem Value="12" meta:resourcekey="ListItemResource26">Diciembre</asp:ListItem>
                         </asp:DropDownList>
                         <asp:DropDownList ID="dpAñoFin" runat="server" 
                             ToolTip="Año de Fin" Visible="False" Width="90px" 
                             meta:resourcekey="dpAñoFinResource1">
                         </asp:DropDownList>
                     </td>
                 </tr>
              <tr>
                <td class="style18">Institución/Empresa</td>
                <td class="style21">
                    <asp:TextBox ID="txtinstitucion" runat="server" Width="483px" 
                        meta:resourcekey="txtinstitucionResource1" ></asp:TextBox>      
                </td>
              </tr>
              <tr>
                <td class="style18">Tipo de Sector</td>
                <td class="style21">
                    <asp:TextBox ID="txtSector" runat="server" Width="290px" 
                        meta:resourcekey="txtSectorResource1" ></asp:TextBox>
                    </td>
              </tr>
              <tr>
                <td class="style18">Lugar de trabajo</td>
                <td class="style21">
                    <asp:TextBox ID="txtciudad" runat="server" Width="483px" 
                        meta:resourcekey="txtciudadResource1"></asp:TextBox>
                    <i>
                    <br />
                    Distrito, Provincia, Departamento.<br />
                    </i>
                    <asp:CheckBox ID="chkExtranjero" runat="server" AutoPostBack="True" 
                        Text="En el Extranjero:" />
                    &nbsp;<asp:TextBox ID="txtlugarextranjero" runat="server" Width="360px"></asp:TextBox>
                  </td>
              </tr>
              
              <tr>
                <td class="style18">Logros/Descripción</td>
                <td class="style21">
                    <textarea ID="txtdescripcion" runat="server" class="contador" 
                        maxlength="5000" rows="3"></textarea>
                    <div ID="longitud_textarea"></div>
                  </td>
              </tr>
              <tr>
                <td class="style18">Área</td>
                <td class="style21">
                    <asp:TextBox ID="txtarea" runat="server" Width="290px" 
                        meta:resourcekey="txtareaResource1"></asp:TextBox>
                 </td>
           
               
              </tr>
              
	             <tr><td class="style18">
                  Tipo Contrato</td>
                  <td class="style21">
                      <asp:TextBox ID="txttipocontrato" runat="server" Width="290px" 
                          meta:resourcekey="txttipocontratoResource1"></asp:TextBox>
                  </td>
                 </tr>
              
	             <tr><td class="style18">
                     Cargo</td>
                  <td class="style21">
                      <asp:TextBox ID="txtcargo" runat="server" Width="290px" 
                          meta:resourcekey="txtcargoResource1"></asp:TextBox>
                  </td>
                 </tr>
	             <tr>
                     <td class="style18">
      <asp:HiddenField ID="codigo_Exp" runat="server" Value="0" />       
                     </td>
                     <td class="style21">
                         <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" 
                             Text="Guardar" meta:resourcekey="cmdGuardarResource1" />
                         <asp:Button ID="cmdGuardar1" runat="server" CssClass="guardar" 
                             Text="Cancelar" meta:resourcekey="cmdGuardar1Resource1" />
                     </td>
                 </tr>	             <tr>
                     <td colspan="2">
                         <asp:Label ID="lblMensajeFrm" runat="server" ForeColor="#CC0000" 
                             style="color: #003366; font-style: italic" 
                             meta:resourcekey="lblMensajeFrmResource1"></asp:Label>
                     </td>
                 </tr>
	         </table>
	         	
	            
          </asp:Panel>           

             
 


  </form>
 </body>
 </html>
