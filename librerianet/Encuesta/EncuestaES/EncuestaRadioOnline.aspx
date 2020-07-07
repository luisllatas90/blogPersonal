<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EncuestaRadioOnline.aspx.vb" Inherits="EncuestaRadioOnline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    table {
	font-family: Trebuchet MS;
	font-size: 8pt;
	margin-right: 0px;}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
}
        .style4
        {
            width: 312px;
        }
        
.guardar  	{border:1px solid #C0C0C0; background:#FEFFE1 url('../images/guardar.gif') no-repeat 0% 50%; width:100; font-family:Tahoma; font-size:8pt; font-weight:bold; }
.guardar {
	border: 1px solid #C0C0C0;
	background: #FEFFE1 url('../images/guardar.gif') no-repeat 0% 80%;
	width: 100;
	font-family: Tahoma;
	font-size: 8pt;
	font-weight: bold;
	height: 25;
}
        .style6
        {
            color: #800000;
        }
        
        .style7
        {
            text-align: justify;
            font-size: small;
        }
        .style12
        {
            text-align: justify;
            font-size: small;
            font-weight: bold;
            color: #292929;
        }
             .btn 
        { cursor:hand;
          font-size: 12px;
            }
        .style13
        {
            color: #800000;
            font-weight: bold;
            font-size: medium;
            text-align: center;
        }
        .style14
        {
            color: #993333;
            font-size: small;
        }
        .style8
        {
            padding: 2px 0px 2px 16px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" method="POST" action="EncuestaRadioOnline.aspx">
<center>
    <table style="width:42%; height: 232px;">
        <tr>
             <td align="left" class="style4">
                 <asp:Image ID="Image11" runat="server" 
                     ImageUrl="https://intranet.usat.edu.pe/usat/files/2011/02/Logo-USAT-300x150.png" 
                     Width="153px" Height="72px" />
            </td>
            <td align="LEFT">
                <b style="mso-bidi-font-weight:normal">
                <span lang="ES-MX" style=" font-size :12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"
                    class="style6">
                <br />
                facultad de humanidades</span><span lang="ES-MX" style="font-size:12.0pt;font-family:&quot;Candara&quot;,&quot;sans-serif&quot;;
                color:#292929;text-transform:uppercase;text-shadow:auto;mso-ansi-language:ES-MX"></BR> 
                escuela de comunicación<br />
                <asp:Label ID="lblSemestre" runat="server" Text="" 
                    style="color: #336699; font-family: Arial; font-size: x-small"></asp:Label>
                </span></b>
            </td>
        </tr>
        
        <tr>
             <td class="style13" colspan="2">
                 <br />
                 <span class="style14">ENCUESTA SOBRE LA CREACIÓN DE UNA RADIO ONLINE PARA 
                 LA UNIVERSIDAD CATÓLICA SANTO TORIBIO DE MOGROVEJO</span></td>
        </tr>
        
        <tr>
            <td colspan="2" class="style7">
                <br />
                <b>Objetivo:
                </b>Aceptación de una radio online en la Universidad Católica Santo Toribio de 
                Mogrovejo<br />
            </td>
        </tr>
        <tr>
            <td class="style12" colspan="2" style="text-align:center;font-size:10px;">
            
                <label id="txtObligatorio" runat="server">
                  </label>           
                  
                </td>
        </tr>
      
        <tr>
            <td class="style12" colspan="2">
            <br />
            <asp:Button ID="Button1" runat="server" Text="Guardar Respuestas" 
                    class="guardar btn"  /><br /><br />
            <div id="TablaPreguntas" runat="server">                                
                
            </div>
            <br />
             <asp:Button ID="Button2" runat="server" Text="Guardar Respuestas" class="guardar" />
            </td>
           
        </tr>
        </table>
</center>
    </form>
</body>
</html>
