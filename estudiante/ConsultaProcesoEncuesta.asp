<!-- #include file="../funciones.asp" -->
<html>
<head>
<title></title>
<%
'	Response.ContentType = "application/msexcel"
'	Response.AddHeader "Content-Disposition","attachment;filename=Cotizacion.xls"
'		filas =mensaje.recordcount
'		columnas = mensaje.Fields.Count
	'Response.Write(columnas)%>
<link href="../private/estilo.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
.Estilo1 {
	font-size: 10pt;
	font-weight: bold;
}
.Estilo2 {
	color: #000000;
	font-weight: bold;
}
-->
</style>
<script>
function carrera (cbo){
document.all.txtPregunta.value = cbo.options[cbo.selectedIndex].text
}
</script>
</head>
<body>
<form id="form1" name="form1" method="post" action="ProcesoEncuesta.asp">
  <table width="90%" border="0" align="center">
    <tr>
      <td align="center" class="pestanabloqueada"><span class="Estilo1">Seleccione la Pregunta y Escuela Profesional </span></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
    <tr>
      <td><table width="100%" border="0">
        <tr>
          <td width="15%"><span class="Estilo2">Pregunta</span></td>
          <td width="85%"><select size="1" name="cboPregunta" onChange="carrera(this)">
            <option value="SELECCIONAR">&gt;&gt;Seleccionar&lt;&lt;</option>
            <option value="P1">LA EXIGENCIA ACADEMICA EN: TRABAJOS DE INVESTIGACION</option>
            <option value="P2">LA EXIGENCIA ACADEMICA EN: EVALUACIONES</option>
            <option value="P3">LA EXIGENCIA ACADEMICA EN: TALLERES</option>
            <option value="P4">LA EXIGENCIA ACADEMICA EN: ASESORIAS</option>
			
            <option value="P5">LA CAPACIDAD DE LOS PROFESORES ES: SOBRE DOMINIO DE LA ASIGNATURA</option>
            <option value="P6">LA CAPACIDAD DE LOS PROFESORES ES: SOBRE TRATO AL ESTUDIANTE</option>
            <option value="P7">LA CAPACIDAD DE LOS PROFESORES ES: SOBRE LLEGADA AL ESTUANTE</option>
            <option value="P8">LA CAPACIDAD DE LOS PROFESORES ES: SOBRE EL TIEMPO Y DISPONIBILIDAD QUE BRINDA EL PROFESOR AL ESTUDIANTE </option>
			
            <option value="P9">EL CONTACTO DE LAS AUTORIDADES  POR PARTE: DEL DECANO DE MI FACULTAD</option>
            <option value="P10">EL CONTACTO DE LAS AUTORIDADES  POR PARTE: DEL DIRECTOR DE MI ESCUELA</option>
            <option value="P11">EL CONTACTO DE LAS AUTORIDADES  POR PARTE: DE LOS PROFESORES</option>
            <option value="P12">EL CONTACTO DE LAS AUTORIDADES  POR PARTE: DE OTRAS AUTORIDADES</option>
			
            <option value="P13">LA INFRAESTRUCTURA E IMPLEMENTACIÓN EN: SALONES</option>
            <option value="P14">LA INFRAESTRUCTURA E IMPLEMENTACIÓN EN: LABORATORIOS</option>
            <option value="P15">LA INFRAESTRUCTURA E IMPLEMENTACIÓN EN: BIBLIOTECA</option>
            <option value="P16">LA INFRAESTRUCTURA E IMPLEMENTACIÓN EN: CAFETERIA</option>
            <option value="P17">LA INFRAESTRUCTURA E IMPLEMENTACIÓN EN: BA&Ntilde;OS</option>
			
            <option value="P18">EL TRABAJO DE LA UNIVERSIDAD EN CUANTO A: ORIENTACIÓN ESPIRITUAL</option>
            <option value="P19">EL TRABAJO DE LA UNIVERSIDAD EN CUANTO A: COMUNICACI&Oacute;N INTERNA</option>
            <option value="P20">EL TRABAJO DE LA UNIVERSIDAD EN CUANTO A: BOLSA DE TRABAJO</option>
            <option value="P21">EL TRABAJO DE LA UNIVERSIDAD EN CUANTO A: POSIBILIDADES DE INTERCAMBIOS</option>
			
            <option value="P22">LA IDENTIFICACIÓN CON LA USAT EN: LAS OPINIONES Y APORTES VERTIDOS POR LOS ESTUDIANTES SON CONSIDERADOS </option>
            <option value="P23">LA IDENTIFICACIÓN CON LA USAT EN: EXPRESO LIBREMENTE MI PUNTO DE VISTA A LAS AUTORIDADES Y PROFESORES </option>
            <option value="P24">LA IDENTIFICACIÓN CON LA USAT EN: EXISTEN ACTIVIDADES DE INTEGRACION ENTRE ESTUDIANTES,PROFESORES Y AUTORIDADES </option>
            <option value="P25">LA IDENTIFICACIÓN CON LA USAT EN: TENGO PUESTA LA CAMISETA DE LA USAT</option>
			
            <option value="P26">LA PERCEPCIÓN DE LA IMAGEN DE LA USAT EN: LA FAMILIA</option>
            <option value="P27">LA PERCEPCIÓN DE LA IMAGEN DE LA USAT EN: LOS AMIGOS</option>
            <option value="P28">LA PERCEPCIÓN DE LA IMAGEN DE LA USAT EN: LOSMEDIOS DE COMUNICACI&Oacute;N</option>
                              </select>
            <label>
            <input name="txtPregunta" type="hidden" id="txtPregunta" />
            </label></td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td><table width="100%" border="0">
        <tr>
          <td width="15%"><span class="Estilo2">Escuela</span></td>
          <td width="85%">
<% 	
'		dim mensaje as  recordset
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				set mensaje=obj.Consultar("ConsultaProcesodeEncuesta","FO","ESC","0")
			Obj.CerrarConexion
		Set obj=nothing

		 	CALL llenarlista("cboCarrera","",mensaje,"escuelas","escuelas","","Seleccionar","","")
			set mensaje = nothing
			%>		  
		  </td>
        </tr>
      </table></td>
    </tr>
    <tr>
      <td align="center"><label>
        <input name="Submit" type="submit" class="buscar2" value="Consultar" />
      </label></td>
    </tr>
    <tr>
      <td><label></label></td>
    </tr>
    <tr>
      <td>&nbsp;</td>
    </tr>
  </table>
</form>
</body>
</HTML>