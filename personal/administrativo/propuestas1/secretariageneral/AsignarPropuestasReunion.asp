<!--#include file="../../../../funciones.asp"-->


<html>
<head>
<title>Programar Agenda de Reuni&oacute;n De Consejo</title>

<link href="../../../../private/estilo.css" rel="stylesheet" type="text/css">	
<style type="text/css">
<!--
body {
	background-color: #f0f0f0;
}
-->
</style></head>
<script language="JavaScript">
function RegistrarInstitucion(){
	var Cad_Prp=""
	var codigo_rec=frminstitucion.txtcodigo_rec.value	
	if (frminstitucion.chkPropuestas!=undefined){

	var Ctrlmarcado=frminstitucion.chkPropuestas.length

	
	//alert (Ctrlmarcado)
	for (i=0; i<Ctrlmarcado;i++){
		var Control=frminstitucion.chkPropuestas[i]
		var CtrlOrden=frminstitucion.TxtOrden[i]		
			if (Control.checked){
				Cad_Prp+=Control.value + "|" + CtrlOrden.value+ ","
			}
	}
	}
if (Cad_Prp==""){
	alert("Debe seleccionar almenos una propuesta para programar ")
}

else{
	//alert("guardar")
	//elminina la ultima coma de la cadena
	//Cad_Prp=Cad_Prp.substring(0,Cad_Prp.length-1)
	if (confirm("Desea programar las propuestas seleccionadas para la Reunión de Consejo Universitario?")==true){
	//alert(codigo_rec)
	//alert(Cad_Prp)
	location.href="procesar.asp?accion=ProgramarAgenda&codigo_rec=" + codigo_rec + "&Cad_Prp=" + Cad_Prp
	
//	window.close()
	}

}
}

	function popUp(URL) {
	day = new Date();
	id = day.getTime();
	var izq = 300//(screen.width-ancho)/2
	//alert (izq)
   	var arriba= 200//(screen.height-alto)/2
	eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=NO,scrollbars=0,location=0,statusbar=0,status=0,menubar=0,resizable=1,width=1px,height=1px,left = "+ izq +",top = "+ arriba +"');");
	}
</script>
<script language="JavaScript" src="../../../../private/funciones.js"></script>	  
<body topmargin="0" rightmargin="0" leftmargin="0">
<form action="procesar.asp?accion=" method="post" name="frminstitucion" id="frminstitucion">
  <table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
      <td class="bordeinf"><table  width="95%" height="100%" border="0" align="center" cellpadding="0" cellspacing="5">
        <tr>
          <td valign="top"><input  name="cmdborrador" type="button" class="guardar_prp" id="cmdborrador" value="          Guardar"  onClick="RegistrarInstitucion()">
            &nbsp;&nbsp;&nbsp;</td>
          <td align="right" valign="top"><input onClick="window.close()"  name="cmdborrador2" type="button" class="noconforme1" id="cmdborrador2" value="         Cerrar" ></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="95%" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td><%
	  	Set prop=Server.CreateObject("PryUSAT.clsAccesoDatos")
	     	prop.AbrirConexion		
			set rsPROPUESTAS=prop.Consultar("ConsultarPropuestas","FO","PA",0,0,SESSION("codigo_usu"),0,0)
	    	prop.CerrarConexion
		set prop=nothing
			%>
			<input name="txtcodigo_rec" type="hidden" id="txtcodigo_rec" value="<%=Request.QueryString("codigo_rec")%>" >
			<br>
			<br></td>
        </tr>
        <tr>
          <td><table width="100%" border="0" cellpadding="0" cellspacing="0" class="contornotabla">
            <tr>
              <td width="4%" bgcolor="#E1F1FB" class="bordeinf">&nbsp;</td>
              <td width="49%" align="center" bgcolor="#E1F1FB" class="bordeinf"><strong>Propuesta</strong></td>
              <td width="13%" align="center" bgcolor="#E1F1FB" class="bordeinf"><strong>Fecha</strong></td>
              <td width="11%" align="center" bgcolor="#E1F1FB" class="bordeinf"><strong>Revisiones</strong></td>
              <td width="10%" align="center" bgcolor="#E1F1FB" class="bordeinf"><strong>Orden</strong></td>
              <td width="13%" align="center" bgcolor="#E1F1FB" class="bordeinf"><strong>Estado</strong></td>
            </tr>
          <%do while not rsPROPUESTAS.EOF %>
		    <tr>
              <td valign="top" bgcolor="#FFFFFF"><input name="chkPropuestas" type="checkbox" id="chkPropuestas" value="<%=rsPROPUESTAS("codigo_prp")%>"></td>
              <td valign="top" bgcolor="#FFFFFF"><%=rsPROPUESTAS("nombre_prp")%></td>
              <td align="center" valign="top" bgcolor="#FFFFFF"><%=rsPROPUESTAS("fecha_prp")%></td>
              <td align="center" valign="top" bgcolor="#FFFFFF"><%
				Set propu=Server.CreateObject("PryUSAT.clsAccesoDatos")
					propu.AbrirConexion		
					set rsAtendidos=propu.Consultar("ConsultarInvolucradoPropuesta","FO","VA",rsPROPUESTAS("codigo_prp"),0)
					propu.CerrarConexion					
				set propu=nothing	
					Response.Write(rsAtendidos(0))
				rsAtendidos.close
			%>  </td>
              <td align="center" valign="top" bgcolor="#FFFFFF"><input name="TxtOrden" type="text" id="TxtOrden" size="2" maxlength="2" onKeyPress="validarnumero()"></td>
              <td align="center" valign="top" bgcolor="#FFFFFF"><%=rsPROPUESTAS("estado")%></td>
            </tr>
          <%
		  rsPROPUESTAS.MoveNext
		  loop%>
		  </table></td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
        <tr>
          <td>&nbsp;</td>
        </tr>
      </table></td>
    </tr>
  </table>
</form>
</body>
</html>
