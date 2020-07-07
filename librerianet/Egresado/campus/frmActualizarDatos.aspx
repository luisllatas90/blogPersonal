<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizarDatos.aspx.vb" Inherits="campus_frmActualizarDatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    
    <style type="text/css">
        
     .filaTitulo
     {
         color:White; background:#e33439; font-size:14px;padding:3px;
     }
              
        .filaborder
     {
        border-bottom:1px solid #e33439;
     }
        .datos_combo
{
    font-size: 8pt;
    color: #002B56;
    font-family: verdana;
    background-color: aliceblue;
}

    TBODY    { display: table-row-group }

tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif; font-size: 8pt; color: #2F4F4F
	}

        .style8
        {
            font-size: 11px;
            font-weight: normal;
            width: 147px;
        }
        
        .style6
        {
            font-size:11px;
            font-weight:normal;
         }
         .style7
        {
            width: 147px;
        }
        .guardar  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../expediente/images/guardar.gif') no-repeat 0% 80%; 
width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25
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
         #form2
        {
            width: 749px;
        }
        .style9
        {
            text-align: center;
        }

    .cunia  	{border-right:1px solid #91A9DB; border-top:1px solid #91A9DB; border-bottom:1px solid #91A9DB; background-position: 0% 0%; width:17px;height:19px; background-image:url('../../images/cunia.gif'); 
background-repeat:no-repeat;
        }
.cunia {
	border-right: 1px solid #91A9DB;
	border-top: 1px solid #91A9DB;
	border-bottom: 1px solid #91A9DB;
	background-position: 0% 0%;
	width: 17px;
	height: 19px;
	background-image: url('../../images/cunia.gif');
	background-repeat: no-repeat;
}

    </style>
</head>
<body>
	  <form id="form2" runat="server">
     
	<table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
	  <tr><td style="color:White; background:#e33439; font-size:14px;padding:3px;">
          &nbsp;Actualización de Datos</td></tr>
	</table>
	
    <table width="100%" cellpadding=4>
	  <tr><td class="filaborder">
          <br />
          <asp:Label ID="lbl_DatosPersonales" runat="server" ForeColor="Maroon" 
              Text="Datos Personales:"></asp:Label>
          </td></tr>
	</table>
	
	<table style="width: 100%" class="contornotabla" id="Table1" runat="server">                         
      <tr>
        <td class="style8">Correo</td>
        <td>
          <asp:TextBox ID="txt_correo" runat="server" Width="358px" CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style8">Teléfono</td>
        <td>
          <asp:TextBox ID="txt_telefono" runat="server" Width="129px" 
                CssClass="datos_combo"></asp:TextBox>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Celular&nbsp;
          <asp:TextBox ID="txt_celular" runat="server" Width="129px" 
                CssClass="datos_combo"></asp:TextBox>
          </td>
      </tr>
      <tr>
        <td class="style8">Dirección</td>
        <td>
          <asp:TextBox ID="txt_direccion" runat="server" Width="359px" 
                CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
        <tr>
            <td class="filaborder" colspan="2">
                <br />
          <asp:Label ID="lblDatosLaborales" runat="server" ForeColor="Maroon" 
                    Text="Datos Laborales de Ultimo Trabajo:"></asp:Label>
            </td>
        </tr>
      <tr>
        <td class="style8">Nombre de Empresa</td>
        <td>
          <asp:TextBox ID="txt_empresa" runat="server" Width="355px" 
                CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style8">Cargo</td>
        <td>
          <asp:TextBox ID="txt_cargo" runat="server" Width="355px" 
                CssClass="datos_combo"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="style8">Pais</td>
        <td>
            <asp:DropDownList ID="dpPais" runat="server" Height="20px" Width="173px">
            </asp:DropDownList>
        </td>
      </tr>
      <tr>
        <td class="style8">Fecha</td>
        <td>
        <asp:TextBox ID="txt_Fecha" runat="server" Width="100px" CssClass="datos_combo">--/--/----</asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        </td>
      </tr>
      <tr>
         <td class="style8"></td>
	    <td class="style6"><asp:HiddenField ID="hdcodigo_pso" runat="server" /></td>
	  </tr>
	  <tr><td class="style7">
          &nbsp;</td></tr>
	  <tr><td class="style7">
          &nbsp;</td></tr>
	 </table>
	 <table style="width: 100%">
	  <tr>
        <td class="style9">
            &nbsp;&nbsp;&nbsp;

          <asp:Button ID="btn_Guardar" runat="server" CssClass="guardar" Enabled="true" 
                Text="Actualizar Datos" SkinID="BotonGuardar" />  
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

          <asp:Button ID="btn_Salir" runat="server" CssClass="guardar" Enabled="true" 
                Text="     Cancelar     " SkinID="BotonGuardar" />  
            <br />
        </td>
      </tr>
     </table>
      </form>
</body>
</html>
