<!--#include file="../../../../funciones.asp"-->
<%
accion=request.querystring("accion")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
th=request.querystring("th")

if accion="" then accion="agregarcargaacademica"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		'Set rsDocente=obj.Consultar("ConsultarDocente","FO","TD",0,0)
		'Set rsDocente=obj.Consultar("ConsultarDocente","FO","DYC",codigo_cac,0)    '03/11/2011 Muestra HorasCarga y ParametroHoras
		Set rsDocente=obj.Consultar("ConsultarDocente","FO","AC2",codigo_cac,0)     '05/11/2019 Muestra solo activos YPEREZ
	obj.CerrarConexion
Set obj=nothing
%>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Seleccione que usuarios tendrán acceso</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/jquery-1.4.2.min.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/jq/lbox/thickbox.js"></script>

<script language="JavaScript">
 function ValidarExcedeCarga(select)
 {
    var text = new Array();
	var value = new Array();
	var num = select.options.length;
  	for (var i = num - 1; i >= 0; i--)
		{
    	if (select.options[i].selected)
			{
      		text[text.length] = select.options[i].text;     //nombre docente
      		value[value.length] = select.options[i].value;  //codigo_per      		            
            var personahoras = select.options[i].text; 
            var pos = personahoras.indexOf("--");
            var poss = personahoras.indexOf("hrs.");
            var HorasCarga = personahoras.substr(pos+3, poss-pos-4);
            var ParametroHoras = personahoras.substr(poss+8);
            var th = <% =th %>;
            var codigo_cac = <% =codigo_cac %>;
      		      	          		      		
      		//var ded = "<% =dedicacion %>";      		      		                 		
      		
      		//Comprobar si se excede la carga académica
      		if (ParametroHoras != "") // Verificar si tiene ParametroHoras (solo es para docentes de Tiempo Completo y Medio Tiempo)
      		    {
      		    var nuevacarga = parseInt(HorasCarga) + parseInt(th);      		    
      		    if (nuevacarga > ParametroHoras)
      		        {
      		        var url = "frmCargaAcademicaDocente.aspx?codigo_per=" + select.options[i].value + "&codigo_cac="+codigo_cac+"&dedicacion=0&parametrohoras="+ParametroHoras+"&th="+th; 
      		        AbrirPopUp(url, 210, 365, "yes", "no", "yes", "no");
      		        
      		        //location.href="frmCargaAcademicaDocente.aspx?codigo_per=" + select.options[i].value + "&codigo_cac="+codigo_cac+"&dedicacion=0&parametrohoras="+ParametroHoras+"&KeepThis=true&TB_iframe=true&height=210&width=365&modal=true"      		        
                    //document.write("Location <a href='frmCargaAcademicaDocente.aspx?codigo_per=" + select.options[i].value +"&codigo_cac="+codigo_cac+"&dedicacion=0&parametrohoras="+ParametroHoras+"&KeepThis=true&TB_iframe=true&height=210&width=365&modal=true' title='Modificar horas' class='thickbox'></a>");      		                            
      		        }
      		    }    		          				
			}
		}
} 
 </script>
    <style type="text/css">
        .style1
        {
            height: 5%;
        }
    </style>
</head>
<body topmargin="0" leftmargin="0">
<form name="frmListaCorreos" onSubmit="return Validarlistaagregada(this)" method="post" ACTION="procesar.asp?accion=<%=accion%>&amp;codigo_cup=<%=codigo_cup%>&amp;codigo_cac=<%=codigo_cac%>&amp;th=<%=th%>">
<table cellpadding="2" cellspacing="0" border="0" width="100%" style="border-color:#C0C0C0; border-collapse: collapse" bordercolor="#111111" height="100%">
		<tr align="center">
          
      <td  width="90%"  height="5%" colspan="3" align="left" style="background-color: #C0C0C0">
   	<input type="submit" class="guardar2" value="Guardar" NAME="cmdGrabar">
    <input type="button" class="regresar2" onClick="history.back(-1)" value="Regresar" NAME="cmdCancelar"></td>            
          </tr>
		<tr align="center">
          
      <td  width="40%" class="style1">&nbsp; </td>
          <td width="10%" class="style1"></td>
          <td width="40%" class="style1"><font color="#800000"><b>Profesores 
          seleccionados</b></font></td></tr>
		<tr align="center">
          <td  width="40%" valign="top" height="90%">
          <%call llenarlista("ListaDe","",rsDocente,"codigo_per","docente","","","","multiple")%>
          <script type="text/javascript" language="javascript">frmListaCorreos.ListaDe.style.height="100%"</script>
		  </td>
			<td  width="10%" valign="top" height="90%">
			  <input type="button" value="Agregar-&gt;" style="width: 80" onClick="ValidarExcedeCarga(this.form.ListaDe); AgregarItem(this.form.ListaDe)" class="cajas">
			  <br>
		      <input type="button" value="&lt;-Quitar" style="width: 80" onClick="QuitarItem(this.form.ListaPara)" class="cajas"></td>
			<td  width="40%" valign="top" height="90%">
				<select multiple name="ListaPara" size="10" style="width: 100%; height:100%">
		</select></tr>
		</table>
</form>
</body>
</html>