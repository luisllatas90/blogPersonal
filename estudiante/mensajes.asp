<!--#include file="../NoCache.asp"-->
<%
Dim mensaje
proceso=request.querystring("proceso")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Mensaje de Deuda</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<style>
<!--
p            { font-size: 9pt }
li           { font-size: 9pt }
-->
</style>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
</head>

<body bgcolor="#F2F2F2">
<center>
<table class="contornotabla" border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="90%" id="AutoNumber1" height="100%">
  <tr>
    <td align="center" valign="top" colspan="2" class="usatboton" style="height: 5%">
    <h3>ADVERTENCIA</h3>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" width="5%" bgcolor="#FFFFCC" height="95%">
    <img alt="Mensaje informativo" src="../images/alerta.gif" vspace="10"></td>
    <td valign="top" width="95%" bgcolor="#FFFFCC" height="95%">
	<h4>Estimado estudiante:</h4>
	<%select case proceso
		case "H"%>
	<p>Lo sentimos, <b><font size="3" color="#0000FF">no se podrá mostrar su Historial académico.</font></b>, 
    hasta que haya regularizado su <i><b> 
    <font color="#FF0000">Estado de Cuenta</font></b></i> o Consulte en la Oficina de Pensiones de la USAT.<br>
    (<font size="1"><a href="../librerianet/academico/adminestadocuenta.aspx?id=<%=session("codigo_alu")%>">Haga 
	
    click aquí para ver su Estado de Cuenta</a></font>)</p>
	<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>¿Qué hago si ya pagué en el Banco mi deuda pendiente?</i></b></li>
      <br>
      Esperar 24 horas después de realizado el pago, para que el Banco remita su 
    	información a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el día lunes a partir de las 
      09:00 am, para que se actualice dicha información.<br>
&nbsp;<li><i><b>¿Qué hago si ya pagué en la Oficina de Pensiones de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
    </ol>
	<%case "M"%>
	<p><span style="font-weight: 400">Lo sentimos, Ud. 
		
		</span><span style="font-weight: 700"><font color="#0000FF" size="3">no 
    podrá matricularse</font></span><span style="font-weight: 400"> hasta que 
    haya regularizado su 
		
		</span><span style="font-weight: 700"><i><font color="#FF0000">Estado de 
    Cuenta</font></i></span><span style="font-weight: 400">,
		
		</span>(<font size="1"><a href="estadocuenta.asp">Haga click aquí para 
    ver su Estado de Cuenta</a></font>)<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>¿Qué hago si ya pagué en el Banco mi deuda pendiente?</i></b></li>
      <br>
      Esperar 24 horas después de realizado el pago, para que el Banco remita su 
    	información a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el día lunes para que se 
      actualice dicha información.<br>
&nbsp;<li><i><b>¿Qué hago si ya pagué en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
     
    </ol>
	<%case "SEP" 'Tiene convenio de pago en pensiones y debe cuotas 
		Set objCnx=Server.CreateObject("PryUSAT.clsAccesoDatos")
		objCnx.AbrirConexion
		set rsSeparacion=objCnx.Consultar("ACAD_ConsultarSeparacionVigente","FO",session("codigo_alu"))
		objCnx.CerrarConexion
		Set objCnx=nothing
	%>    
		<h3>
			Ud. No puede realizar su prematricula porque tiene registrado:</br></br>
			<% if rsSeparacion("codigo_tse") = 1 then response.write( rsSeparacion("descripcion_tse") & " desde el " & rsSeparacion("fechaIni_sep") & " hasta " & rsSeparacion("fechafin_sep")) else response.write( rsSeparacion("descripcion_tse")) %> 
			</br>Por motivo: <%=rsSeparacion("motivo_sep")%>.</br></br>
			Si desea mayor información acerquese a su Director de Escuela.</br>
    	</h3>
	<%case "F"%>
	<p>Lo sentimos, actualmente la matrícula <b><font color="#0000FF" size="3">no se encuentra disponible</font></b>.<br>
    <br>
    Por favor verifique el Cronograma, del Proceso de Matrícula. <%=session("descripcion_cac")%>
	<%case "L"%>    
    <h3 align=left>Lo sentimos Ud. no puede ingresar al Campus Virtual, ya que tiene <font color='#FF0000'>DEUDA DE LIBROS EN BIBLIOTECA.</font><br>&nbsp;</h3>
    <h3 align=left>Favor de acercarse a la Biblioteca de la USAT, ha regularizar su Estado, caso contrario no podrá ingresar al Campus Virtual.</h3>
    <h4 align=right>&nbsp;</h4>
    <h4 align=right>Dirección de Biblioteca</h4>
    <%case "B"%>
    <h4>Lo sentimos, por el momento este proceso va a demorar, por favor intente mas tarde...</h4>
    <%case "AV"%>
	<p><span style="font-weight: 400">Lo sentimos, Ud. 
		
		</span><span style="font-weight: 700"><font color="#0000FF" size="3">no 
    podrá Ingresar al Aula Virtual</font></span><span style="font-weight: 400"> hasta que 
    haya regularizado su 
		
		</span><span style="font-weight: 700"><i><font color="#FF0000">Estado de 
    Cuenta</font></i></span><span style="font-weight: 400">,
		
		</span>(<font size="1"><a href="estadocuenta.asp">Haga click aquí para 
    ver su Estado de Cuenta</a></font>)<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>¿Qué hago si ya pagué en el Banco mi deuda pendiente?</i></b></li>
      <br>
      Esperar 24 horas después de realizado el pago, para que el Banco remita su 
    	información a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el día lunes para que se 
      actualice dicha información.<br>
		<li><i><b>¿Qué hago si ya pagué en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
    </ol>
	<%case "FM" 'Falta prematricularse
	%>
		<h4>
			Ud. no está pre-matriculado en el ciclo <%=Session("descripcion_cac")%>.<br>
			Debe ingresar a la opción Pre-matricula para elegir las asignaturas que llevará en el ciclo
		</h4>
	
    <%case "PCR" 'Falta precio por crédito
	%>
    	<h4>
    		Lo sentimos Ud. no ha sido categorizado.<br>
    		Comuníquese con la Oficina de Pensiones para regularizar su categorización
    	</h4>
    <%case "EMAT" 'Existe matriculado
	%>
    	<h4>
    		Ud. ya ha realizado la prematricula de asignaturas.<br><br>
    		Si desea puede realizar agregados o retiros a través del menú [Agregados / Retiros]
    	</h4>
	<%case "BMG" 'Bloqueo de matrícula para estudiantes en forma general
	%> 
    	<h4>
			Actualmente la matrícula no se encuentra disponible para Ud.<br>
			Acudir a las oficinas de su Escuela Profesional para realizar su pre-matrícula <br>
			<br><br>
			Dirección de Escuela de <%=session("nombre_cpf")%>
    	</h4>    	
	<%case "E-PRE" 'bloqueado INGRESANTES PRE
	%>
    	<h4>
			Ud. no puede matricularse por motivo de que actualmente se encuentran procesando convalidaciones.<br>
			<br><br>
			A partir del día Martes 11 de marzo podrá ingresar a realizar su pre-matrícula.
    	</h4>
    <%case "MCA" 'ciclo anterior
	%>
    	<h4>
			Ud. no puede pre-matricularse ya que tiene notas pendientes por registrar.<br>
			<br><br>
			Puede acudir a su dirección de Escuela a realizar su pre-matrícula.
    	</h4>
    <%case "BVD" 'VECES desaprobados
	%>
    	<h4>
			Ud. no puede pre-matricularse ya que tiene más de 2 veces asignaturas desaprobadas (no incluye complementarios).<br>
			<br><br>
			Debe acudir a su dirección de Escuela a realizar su pre-matrícula.
    	</h4>
    <%case "DCA" 'cursos desaprobados
	%>
    	<h4>
			Ud. no puede matricularse ya que ha desaprobado más de 2 asignaturas en ciclos anteriores (no incluye complementarios).<br>
			<br><br>
			Debe acudir a su dirección de Escuela a realizar su pre-matrícula.
    	</h4>
    <%case "ODO","medicina" 'Es Odontología, Medicina
	%>
    	<h4>
			Ud. deberá acercarse a las Oficinas de la Dirección de Escuela para realizar su pre-matrícula.<br>
			Dirección de Escuela.
    	</h4>		
	<%case "enfermeria" 'Es Enfermeria
	%>
    	<h4>
			Ud. deberá acercarse a las Oficinas de la Dirección de Escuela o al Laboratorio 106 (1er piso, Ed. antíguo) para realizar su pre-matrícula.
			Dirección de Escuela de Enfermería
    	</h4> 
    <%case "DCON" 'Tiene convenio de pago en pensiones y debe cuotas
	%>
    	<h4>
			Ud. No puede realizar su prematricula porque tiene pendiente cuotas por convenio de pagos realizado con pensiones.<br>
			Acudir a la Oficina de Pensiones a regularizar su estado.<br>
			Dirección de Pensiones
    	</h4>	
	<%case "bloqueocarnet" 'Tiene convenio de pago en pensiones y debe cuotas
	%>
    	<h4>
			Ud. no puede realizar su prematricula porque no ha recogido el Carné de Pensiones, favor acercarse a la Oficina de Pensiones en el siguiente horario: De lunes a viernes: 9:00 a.m. a 04:00 p.m.
			<br />
			Dirección de Pensiones
    	</h4>	
	<%case "ODONT" 'BLOQUEO PARA ODONTOLOGIA
	%>
    	<h4>
			Le informamos que por el momento no tendrá acceso al Campus Virtual debido a que se están realizando cambios académicos<br /><br />
			Dirección de Escuela de Odontología.
    	</h4>		
    <% Case else %>
        <h4>
			Ud. se encuentra bloqueado para realizar esta acción.<br>
			Puede acudir a su dirección de Escuela a realizar su pre-matrícula.<br>
    	</h4>		
    <%end select%>
        
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" colspan="2" class="usatboton">&nbsp;</td>
  </tr>
</table>
</center>
</body>
</html>