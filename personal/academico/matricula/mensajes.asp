<%
Dim mensaje
proceso=request.querystring("proceso")
%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252"/>
<title>Mensaje de Deuda</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css"/>

<style>A:hover {color: red; font-weight: bold}
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
    <img alt="Mensaje informativo" src="../../../images/alerta.gif" vspace="10"></td>
    <td valign="top" width="95%" bgcolor="#FFFFCC" height="95%">
	<%select case proceso
		case "H"%>
	<p>Lo sentimos, <b><font size="3" color="#0000FF">no se podr� mostrar su Historial acad�mico 
    del estudiante</font></b>, 
    hasta que haya regularizado su <i><b> 
    <font color="#FF0000">Estado de Cuenta</font></b></i> o consulte en el Aula 
    111 del Campus Universitario USAT (Of. de Informes)</p>
	<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>�Qu� hago si ya pagu� en el Banco mi deuda pendiente?</i></b></li>
      <br>
      Esperar 24 horas despu�s de realizado el pago, para que el Banco remita su 
    	informaci�n a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el d�a lunes a partir de las 
      09:00 am, para que se 
      actualice dicha informaci�n.<br/>
&nbsp;<li><i><b>�Qu� hago si ya pagu� en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
    </ol>
	<%case "M"
		session("qb")="S"
	%>
	<!--#include file="../fradatos.asp"-->
	<%if session("tipo_cac")="N" then%>
	<p>
	<span style="font-weight: 400">Lo sentimos, el estudiante 
	</span><span style="font-weight: 700"><font color="#0000FF" size="3">no 
    podr� matricularse</font></span><span style="font-weight: 400"> hasta que 
    haya regularizado su 
		
		</span><span style="font-weight: 700"><i><font color="#FF0000">Estado de 
    Cuenta</font></i></span><span style="font-weight: 400">
		
		</span>
		<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>�Qu� hago si ya pagu� en el Banco mi deuda pendiente?</i></b></li>
      <br/>
      Esperar 24 horas despu�s de realizado el pago, para que el Banco remita su 
    	informaci�n a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el d�a lunes para que se 
      actualice dicha informaci�n.<br/>
&nbsp;<li><i><b>�Qu� hago si ya pagu� en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
     
    </ol>
    <%else%>
    <p><span style="font-weight: 400">Lo sentimos, el estudiante 
		
		</span><span style="font-weight: 700"><font color="#0000FF" size="3">no 
    podr� inscribirse</font></span><span style="font-weight: 400"> hasta que 
    haya regularizado su 
		
		</span><span style="font-weight: 700"><i><font color="#FF0000">Estado de 
    Cuenta</font></i></span><span style="font-weight: 400">
		
		</span>
		<p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>�Qu� hago si ya pagu� en el Banco mi deuda pendiente?</i></b></li>
      <br/>
      Esperar 24 horas despu�s de realizado el pago, para que el Banco remita su 
    	informaci�n a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el d�a lunes para que se 
      actualice dicha informaci�n.<br/>
&nbsp;<li><i><b>�Qu� hago si ya pagu� en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
     
    </ol>
    
    <%end if%>
   
    <%case "RM"%>
    <%if session("tipo_cac")="N" then%>
	<p>Se restableci� la matr�cula del estudiante <b><font color="#0000FF" size="3">Regresar al men� principal</font></b>.<br>
    <br/>
    Por favor verifique el Cronograma, del Proceso de Matr�cula. <%=session("descripcion_cac")%>
    <%else%>
    <p>Se restableci� la inscripci&oacute;n del estudiante <b><font color="#0000FF" size="3">Regresar al men� principal</font></b>.<br>
    <br/>
    Por favor verifique el Cronograma, del Proceso de Inscripcion de Verano. <%=session("descripcion_cac")%>    
    <%end if%>
    
    
	<%case "F"%>
	<%if session("tipo_cac")="N" then%>
	<p>Lo sentimos, Actualmente la matr�cula <b><font color="#0000FF" size="3">no se encuentra disponible</font></b>.<br>
	<br/>    
    Por favor verifique el Cronograma, del Proceso de Matr�cula. <%=session("descripcion_cac")%>
    <%else%>
    <p>Lo sentimos, Actualmente la inscripci&oacute;n de verano <b><font color="#0000FF" size="3">no se encuentra disponible</font></b>.<br>
    <br/>    
    Por favor verifique el Cronograma, del Proceso de Inscripci&oacute;n de Verano. <%=session("descripcion_cac")%>
    <%end if%>
    
    
    <%case "EPP"%>
    <%if session("tipo_cac")="N" then%>
	<p>Lo sentimos, Actualmente tiene su <b><font color="#0000FF" size="3">inscripci&oacute;n pendiente de pago</font></b>.<br/>
    <br/>
    Por favor verifique el Cronograma de pagos del proceso de matr�cula de verano. <%=session("descripcion_cac")%>
    <%else%>
    <p>Lo sentimos, Actualmente tiene su <b><font color="#0000FF" size="3">inscripci&oacute;n pendiente de pago</font></b>.<br/>
    <br/>
    Por favor verifique el Cronograma de pagos del proceso de Inscripci&oacute;n de Verano. <%=session("descripcion_cac")%>
    <%end if%>
    
    <%case "EGR"%>
	<p>Lo sentimos, Actualmente no puede matricularse, usted ya es <b><font color="#0000FF" size="3">egresado de la carrera</font></b>.<br/>
    <br/>
    Por favor acercarse a su direcci�n de escuela. 
    
    <%case "SUN"%>
    <%if session("tipo_cac")="N" then%>
	<p>Lo sentimos, <b><font color="#0000FF" size="3">matr�cula no disponible</font></b>.<br/>
    <br/>
    Por favor acercarse a direcci�n acad�mica. 
    <%else%>
    <p>Lo sentimos, <b><font color="#0000FF" size="3">Inscripci&oacute;n de Verano no disponible</font></b>.<br/>
    <br/>
    Por favor acercarse a direcci�n acad�mica.     
    <%end if%>
    
    <%case "ENE"%>
    <%if session("tipo_cac")="N" then%>
	<p>Lo sentimos, No tiene generada su <b><font color="#0000FF" size="3">Matricula</font></b>.<br/>
    <br/>
    Por favor acercarse al &aacute;rea de caja y pensiones. 
    <%else%>
    
    <p>Lo sentimos, No tiene generada su <b><font color="#0000FF" size="3">Inscripci&oacute;n del curso de verano</font></b>.<br/>
    <br/>
    Por favor acercarse al &aacute;rea de caja y pensiones. 
    <%end if%>
    
    
    <%case "NPP"%>
    <%if session("tipo_cac")="N" then%>
	    <p>Lo sentimos, Actualmente tiene su <b><font color="#0000FF" size="3">matr&iacute;cula pendiente de pago</font></b>.<br/>
        <br/>
        Por favor verifique el Cronograma de pagos del proceso de matr�cula del ciclo <%=session("descripcion_cac")%>.
    <%else%>
        <p>Lo sentimos, Actualmente tiene su <b><font color="#0000FF" size="3">Inscripci&oacute;n de Verano pendiente de pago</font></b>.<br/>
        <br/>
        Por favor verifique el Cronograma de pagos del proceso de Inscripci&oacute;n de Verano del ciclo <%=session("descripcion_cac")%>.
    <%end if%>
    
    <%case "NNE"%>
    <%if session("tipo_cac")="N" then%>
	    <p>Lo sentimos, No tiene generada su <b><font color="#0000FF" size="3">matr&iacute;cula del ciclo <%=session("descripcion_cac")%> </font></b>.<br/>
        <br/>
        Por favor acercarse al &aacute;rea de caja y pensiones. 
    <%else%>
        <p>Lo sentimos, No tiene generada su <b><font color="#0000FF" size="3">Inscripci&oacute;n de verano del ciclo <%=session("descripcion_cac")%> </font></b>.<br/>
        <br/>
        Por favor acercarse al &aacute;rea de caja y pensiones. 
    <%end if %>
    
	<%case "L"%>    
    <h3 align=left>Lo sentimos el estudiante no puede matricularse ya que tiene <font color='#FF0000'>DEUDA DE LIBROS EN BIBLIOTECA.</font><br>&nbsp;</h3>
    <h3 align=left>Favor de acercarse a la Biblioteca de la USAT, ha regularizar su Estado, caso contrario no podr� ingresar al Campus Virtual.</h3>
    <h4 align=right>&nbsp;</h4>
    <h4 align=right>Direcci�n de Biblioteca</h4>
    <%case "B"%>
    <h4>Lo sentimos, por el momento este proceso va a demorar, por favor intente mas tarde...</h4>
<%case "AV"%>
	<p><span style="font-weight: 400">Lo sentimos, Ud. 
		
		</span><span style="font-weight: 700"><font color="#0000FF" size="3">no 
    podr� Ingresar al Aula Virtual</font></span><span style="font-weight: 400"> hasta que 
    haya regularizado su 
		
		</span><span style="font-weight: 700"><i><font color="#FF0000">Estado de 
    Cuenta</font></i></span><span style="font-weight: 400">,
		
		</span><p><u><b>Preguntas Frecuentes</b></u></p>
	<ol>
      <li><b><i>�Qu� hago si ya pagu� en el Banco mi deuda pendiente?</i></b></li>
      <br>
      Esperar 24 horas despu�s de realizado el pago, para que el Banco remita su 
    	informaci�n a la USAT y se actualicen los saldos de las deudas 
      pendientes. Si Ud. ha cancelado su 
      deuda un fin de semana, debe esperar hasta el d�a lunes para que se 
      actualice dicha informaci�n.<br>
		<li><i><b>�Qu� hago si ya pagu� en la Oficina de Caja de la Universidad?</b></i><br>
      Ingresar al Campus Virtual, para realizar sus 
      consultas correspondientes.</li>
    </ol>
	<%case "FM" 'Falta prematricularse%>
		<h4>
			Ud. no est� pre-matriculado en el ciclo <%=Session("descripcion_cac")%>.<br>
			Debe ingresar a la opci�n Pre-matricula para elegir las asignaturas que llevar� en el ciclo
		</h4>
    <%case "EMAT" 'Existe matriculado%>
    	<h4>
    		Ud. ya ha realizado la prematricula de asignaturas.<br><br>
    		Si desea puede realizar agregados o retiros a trav�s del men� [Agregados / Retiros]
    	</h4>
    <%case "CAT" 'No est� categorizado%>
    	<h4>
    Lo sentimos el Estudiante&nbsp; no ha sido 
    categorizado.<br>
    Comun�quese con la Oficina de Pensiones para regularizar su categorizaci�n
    	</h4>
    <%case "DES" 'No est� habilitado%>
    	<h4>
    Lo sentimos el Estudiante&nbsp; se encuentra inactivo por no haberse matriculado en ciclos anteriores .<br>
    Comun�quese con la Oficina de Pensiones para regularizar su estado.
    	</h4>
    <%case "DCON" 'Tiene convenio de pago en pensiones y debe cuotas%>
    	<h4>
			Lo sentimos, el estudiante no puede realizar su prematricula, porque tiene pendiente cuotas por convenio de pagos realizado con pensiones.<br>
			Indicarle que debe acudir a la Oficina de Pensiones a regularizar su estado.<br>
			Direcci�n de Pensiones
    	</h4>		
    <%case "SEP" 'Tiene separaci�n definitiva%>
    	<h4>
			Lo sentimos, el estudiante no puede realizar su matricula, porque tiene una separaci�n definitiva.<br/>			
			Por favor acercarse a Direcci�n de Escuela.
    	</h4>	
    <%case "SEPT" 'Tiene separaci�n temporal%>
    	<h4>
			Lo sentimos, el estudiante no puede realizar su matricula, porque tiene una separaci�n temporal.<br/>			
			Por favor acercarse a Direcci�n de Escuela.
    	</h4>		
    <%case "CUR" 'Veces desaprobadas%>
    	<h4>
			Lo sentimos, tiene un bloqueo por veces desaprobadas.<br/>			
			Por favor acercarse a Direcci�n de Escuela.
    	</h4>	
    <%case "ING" 'Ingresantes%>
    <%if session("tipo_cac")="N" then%>
    	<h4>
			Usted es INGRESANTE. La matr�cula para ingresantes 2020-I es de manera autom�tica.<br />
            Su ficha de matr�cula se mostrar� a partir del 12 de marzo.
    	</h4>	
    <%else%>
    <h4>
			Usted es INGRESANTE. La inscripcion de verano para ingresantes 2020-I es de manera autom�tica.<br />
            Su ficha de matr�cula se mostrar� a partir del 12 de marzo.
    	</h4>	
    <%end if%>
    <%case "DM"
		session("qb")="S"
	%>
	<%if session("tipo_cac")="N" then%>
	<!--#include file="../fradatos.asp"-->
	<p><span style="font-weight: 400">Lo sentimos, el estudiante 
		
		</span><span style="font-weight: 500"><font color="#0000FF" size="3">no 
    podr� matricularse</font></span><span style="font-weight: 400"> hasta que 
    haya cancelado su 
		
		</span><span style="font-weight: 800"><i><font color="#FF0000">PAGO DE MATR�CULA</font></i></span>		
		<p><u><b>Preguntas Frecuentes</b></u></p>
	  <ol>
      <li><b><i>Me aparece deuda y ya cancel�</i></b></li>
      <br/>
              Ac�rcate con tu voucher de pago a la oficina de Tesorer�a
     
    </ol>
	
	<%else
	    if session("tipo_cac")="E" then
	    %>
	<p><span style="font-weight: 400">Lo sentimos, el estudiante 
		
		</span><span style="font-weight: 500"><font color="#0000FF" size="3">no 
        podr� inscribirse</font></span><span style="font-weight: 400"> hasta que 
        haya cancelado su 
		</span><span style="font-weight: 800"><i><font color="#FF0000">PAGO DE INSCRIPCION DE VERANO</font></i></span>
		<span style="font-weight: 400">, en caso no tenga la deuda,  debe generarlo ingresando a su campus virtual estudiante en la opci�n Procesos en l�nea>>Inscripci�n de Cursos de Verano. Adem�s indicarle que para poder generarlo no debe tener deudas vencidas.
		</span>
		<p><u><b>Preguntas Frecuentes</b></u></p>
	    <ol>
        <li>
        <b><i>Me aparece deuda y ya cancel�</i></b></li>
        <br/>
        Ac�rcate con tu voucher de pago a la oficina de Tesorer�a
     
    </ol>
	<%  end if
	    end if %>
	
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