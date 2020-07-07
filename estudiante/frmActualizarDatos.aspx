<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmActualizarDatos.aspx.vb" Inherits="frmActualizarDatos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
    <link href="private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../jquery/fancy/jquery.js" type="text/javascript"></script>
    <script src="../jquery/fancy/jquery.fancybox-1.3.4.pack.js" type="text/javascript"></script>
    <link href="../jquery/fancy/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" media="screen" />
    
    <style type="text/css">
        
     .filaTitulo
     {
         color:White; background:#8B0000; font-size:14px;padding:3px;
     }
              
        .filaborder
     {
        border-bottom:1px solid #8B0000;
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
            width: 156px;
        }
        
        .style6
        {
            font-size:11px;
            font-weight:normal;
         }
         .style7
        {
            width: 156px;
        }
        .guardar  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../expediente/images/guardar.gif') no-repeat 0% 80%; 
width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; height:25
        }
        
        .content
        {
                padding:2px;
                border:1px solid #8B0000;
                background-color:#FEFFE1;
                font-family:Verdana;
                font-size:10px;        
        }
        .head_titulo
        { 
            font-weight:bold; background-color:#8B0000; color:White;
            padding:4px;        
        }
         #form2
        {
            width: 602px;
        }
        .style9
        {
            text-align: center;
        }

        #form1
        {
            width: 767px;
        }

    </style>
</head>

<body>
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" class="" style="width: 100%;">
        <tr>
            <td style="color: White; background: #66262F; font-size: 14px; padding: 3px;">
                &nbsp;Actualización de Datos</td>
        </tr>
    </table>
    
    <table id="Table1" runat="server" class="contornotabla" style="width: 100%">
      <!--<tr><td colspan="2"><span class="azul" style="color:#66262F">&nbsp;Registrar Futuro Empleo</span></td></tr>-->
      <!--<tr><td class="style7">&nbsp;</td></tr>-->
      <%--<tr>
        <td class="style8"><b>¿Que Puesto te gustaría?</b></td>
        <td><asp:TextBox ID="txtpuesto" runat="server" Width="210px" CssClass="datos_combo"></asp:TextBox></td>
      </tr>--%>
        <tr>
            <td class="style8">
                Correo</td>
            <td>
                <asp:TextBox ID="txt_correo" runat="server" CssClass="datos_combo" 
                    Width="357px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                Teléfono Fijo</td>
            <td>
                <asp:TextBox ID="txt_telefono" runat="server" CssClass="datos_combo" 
                    Width="121px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                Celular:
                <asp:TextBox ID="txt_celular" runat="server" CssClass="datos_combo" 
                    Width="121px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                Dirección</td>
            <td>
                <asp:TextBox ID="txt_direccion" runat="server" CssClass="datos_combo" 
                    Width="359px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="filaborder" colspan="2">
                <br />
                <asp:Label ID="lblContador" runat="server" ForeColor="Maroon" 
                    Text="Datos Laborales:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="style8">
                Ultimo Trabajo</td>
            <td>
                <asp:TextBox ID="txt_ultimotrabajo" runat="server" CssClass="datos_combo" 
                    Width="355px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
                Fecha de Ultimo trabajo</td>
            <td>
                <asp:TextBox ID="txtarea2" runat="server" CssClass="datos_combo" Width="210px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style8">
            </td>
            <td class="style6">
                <asp:HiddenField ID="hdcodigo_pso" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style7">
                &nbsp;</td>
        </tr>
    </table>
    <table style="width: 100%">
        <tr>
            <td class="style9">
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="cmdGuardar" runat="server" CssClass="guardar" Enabled="true" 
                    SkinID="BotonGuardar" Text="Registrar" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
