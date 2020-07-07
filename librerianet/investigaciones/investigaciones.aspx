<%@ Page Language="VB" AutoEventWireup="false" CodeFile="investigaciones.aspx.vb" Inherits="investigaciones_investigaciones" %>
<!--
'----------------------------------------------------------------------
'Fecha: 30.10.2012
'Usuario: gcastillo
'Motivo: Cambio de URL del servidor de la WebUSAT
'----------------------------------------------------------------------
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>INvestigaciones de Alumnos</title>
     <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript"  language="JavaScript" src="../../private/funciones.js"></script>

<style type="text/css">
<!--
body,td,th {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 10px;
	color: #000000;
}
body {
	margin-left: 0px;
	margin-top: 0px;
	margin-right: 0px;
	margin-bottom: 0px;
	background-image: url(images/fondo.gif);
}
a:link {
	color: #006699;
	text-decoration: none;
}
a:visited {
	text-decoration: none;
	color: #006699;
}
a:hover {
	text-decoration: underline;
	color: #0033CC;
}
a:active {
	text-decoration: none;
}
a {
	font-weight: bold;
}
.Estilo11 {color: #006699; font-weight: bold; font-size: 10px; }
.contornotablaNegro { border: 1px solid #000000 }
-->


BODY {
	scrollbar-face-color:#FFFFFF;
	scrollbar-highlight-color:#FFFFFF;
	scrollbar-3dlight-color:#FFFFFF;
	scrollbar-darkshadow-color:#FFFFFF;
	scrollbar-shadow-color:#EEEEEE;
	scrollbar-arrow-color:#000000;
	scrollbar-track-color:#FFFFFF;
	background-image: url(../../images/fondo3_.gif);
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</style>
<script type="text/javascript">
//http://javascript.tunait.com/
//tunait@yahoo.com
function fecha(){
	fecha = new Date()
	mes = fecha.getMonth()
	diaMes = fecha.getDate()
	diaSemana = fecha.getDay()
	anio = fecha.getFullYear()
	dias = new Array('Domingo','Lunes','Martes','Miercoles','Jueves','Viernes','Sábado')
	meses = new Array('Enero','Febrero','Marzo','Abril','Mayo','Junio','Julio','Agosto','Septiembre','Octubre','Noviembre','Diciembre')
	document.write('<span id="fecha">')
	document.write (diaMes + " de " + meses[mes] + " de " + anio)
//	document.write (dias[diaSemana] + " " + diaMes + " de " + meses[mes] + " de " + anio)
	document.write ('</span>')
}
</script>    
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"></head>
<body style="margin:0,0,0,0">
<center>
    <form id="form1" runat="server">
           <table width="527" cellpadding="0" cellspacing="0" style="width: 666px; height: 100%;" class="contornotablaNegro">
             <!--DWLayoutTable-->
               <tr>
                   <td height="87" colspan="5" valign="top" >
                     <asp:Image ID="Image1" runat="server" ImageUrl="../../images/cabecera.jpg" />                   </td>
               </tr>
               <tr>
                 <td height="23" colspan="5" valign="top" style="height: 8px"><table width="100%" border="0" cellpadding="0" cellspacing="0" background="../../images/fondo3_.gif" style="background:'../../images/fondo3_.gif'" >
                   <!--DWLayoutTable-->
                   <tr >
                     <td width="4%" align="center" valign="middle">
					 <img alt="imagen" src="../../images/inicio_bi.gif" width="25" height="20" />					 </td>
                     <td width="15%">
					 <span class="Estilo11">
					 <!-- <a href="http://www.usat.edu.pe/biblioteca_/linksbiblioteca.htm" >Inicio</a> -->
					 <a href="//intranet.usat.edu.pe/biblioteca_/linksbiblioteca.htm" >Inicio</a>
					 </span>
					 </td>
                     <td width="41%"><strong><span style="font-size: 10pt; color: #276387"> &nbsp;&nbsp;Bienvenidos</span></strong></td>
                     <td width="40%"><div align="right">Chiclayo,   
                         <script type="text/javascript"> fecha(); </script>
                    </div></td>
                   </tr>
                   <tr>
                     <td height="1px" bgcolor="#276387" colspan="4" align="center" valign="middle"></td>
                   </tr>
                 </table></td>
               </tr>
               
               <tr>
                   <td width="300" height="17" style="border-right: black 1px double; background-color: ghostwhite" align="center">
                       <br />
                       <asp:DropDownList ID="DDLEscuela" runat="server" AutoPostBack="True" style="font-size: 7pt; color: black; font-family: verdana">
                    </asp:DropDownList></td>
                   <td width="469" colspan="4" rowspan="3" valign="top">&nbsp;<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Seleccione una asignatura en la parte izquierda para visualizar sus investigaciones.</span><iframe name="fradetalle" id="fradetalle" scrolling="auto" frameborder="0" width="99%" height="500px"></iframe></td>
               </tr>
            
               <tr>
                   <td height="24" style="border-right: black 1px double; width: 223px; height: 24px; background-color: ghostwhite">
                       <table cellpadding="0" cellspacing="0" style="width: 299px">
                           <tr>
                               <td style="font-weight: bold; font-size: 8pt; width: 235px; color: white; font-family: verdana;
                                   height: 15px; background-color: Firebrick">
                                   &nbsp;Asignatura</td>
                               <td align="center" style="font-weight: bold; font-size: 8pt; color: white; font-family: verdana;
                                   height: 15px; background-color: Firebrick">
                                   &nbsp;Total</td>
                           </tr>
                       </table>                   </td>
               </tr>
            <tr>
                <td valign="top" bgcolor="#FFFFFF" class="contornotabla">
                    <asp:Panel ID="PanCursos" runat="server" Height="455px" ScrollBars="Vertical" Width="300px">
                        <asp:GridView ID="GridCursos" runat="server" AutoGenerateColumns="False" GridLines="None"
                            Width="100%" CellPadding="2" ShowHeader="False">
                            <Columns>
                                <asp:BoundField DataField="Curso" HeaderText=" Curso" >
                                    <HeaderStyle HorizontalAlign="Left" />                                </asp:BoundField>
                                <asp:BoundField DataField="Total" HeaderText="Total">
                                    <ItemStyle HorizontalAlign="Center" />                                </asp:BoundField>
                                <asp:CommandField ButtonType="Image" SelectImageUrl="../../images/previo.gif" ShowSelectButton="True" Visible="False" />
                            </Columns>
                            <RowStyle Font-Names="Verdana" Font-Size="7pt" />
                            <HeaderStyle Font-Names="Verdana" Font-Size="9pt" BackColor="Firebrick" ForeColor="White" />
                            <EmptyDataTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 12px">
                                        </td>
                                        <td style="font-weight: bold; font-size: 10pt; color: sienna; font-family: verdana; text-align: center">
                                            <br />
                                            &nbsp;Investigaciones de Estudiantes</td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 12px">
                                        </td>
                                        <td style="font-size: 8pt; color: midnightblue; font-family: verdana; text-align: justify">
                                            <br />
                                            Para observar las investigaciones realizadas debe de seleccionar una Escuela Profesional.<br />
                                            <br />
                                            Posteriormente aparecerá un listado con los cursos correspondientes a la Escuela
                                            y su respectivo total de investigaciones que tienen hasta la fecha.<br />
                                            <br />
                                            Haciendo click en un curso en la parte derecha se desplegarán las investigaciones
                                            ordenadas en forma decreciente por el Semestre de Estudios y su fecha de publicación.<br />
                                            <br />
                                            Puede descargar el documento haciendo click en el ícono izquierdo (PDF)</td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:GridView>
              </asp:Panel>                </td>
             </tr>
            
               
            <asp:HiddenField  ID="txtelegido" runat="server" />
    <asp:HiddenField ID="txtTipo" runat="server" />
    <asp:HiddenField ID="txtEstado" runat="server" />
    <asp:HiddenField ID="txtMenu" runat="server" />
      </table>
    </form>
</center>
</body>
</html>
