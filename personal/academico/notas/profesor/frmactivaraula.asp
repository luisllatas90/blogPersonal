<!--#include file="../../../../funciones.asp"-->
<!--#include file="LibMoodle.asp"-->
<%
if (request.querystring("codigo_cac")<>"")then
    codigo_cac = request.querystring("codigo_cac")
else
    '#codigo_cac=session("codigo_cac")   
    codigo_cac = 0
end if


codigo_per=session("codigo_usu")

Function DevuelvePrefix()
	DevuelvePrefix = "mdl_"
	'DevuelvePrefix = ""
end function

Dim Prefix 
Prefix = DevuelvePrefix


if codigo_per<>"" then
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		'#
		if (request.querystring("codigo_cac")<>"") then
	        Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","13",codigo_cac,codigo_per)
	    end if
	    
		Set rsCac= Obj.Consultar("ConsultarCargaAcademica","FO","15",codigo_cac,codigo_per)
	Obj.CerrarConexion
	Set Obj=nothing
	
   ' Set objConn = Server.CreateObject("ADODB.Connection") 
   ' objConn.Open DevuelveCadenaSQLMoodle	    
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../private/validaraula.js"></script>
<title>Carga Acad�mica</title>
<script type="text/javascript" language="javascript">
    function ConfirmarCurso() {
        var mensaje = ""
        mensaje = "�Est� completamente seguro que desea crear Habilitar el aula virtual para las asignaturas seleccionadas?"

        if (confirm(mensaje) == true) {
            frmcurso.cmdGuardar.disabled = true
            mensaje.innerHTML = "<b>&nbsp;Espere un momento por favor...</b>"
            frmcurso.submit()
        }
    }

    function AbrirDocsAnteriores(id) {
        var fila = event.srcElement.parentElement
        fila = fila.parentElement
        var curso = fila.cells[3].innerText + " (Grupo " + fila.cells[4].innerText + ")"

        AbrirPopUp("frmimportardocs.asp?idcursovirtualdestino=" + id + "&curso=" + curso, "500", "600", "yes", "yes", "yes")
    }

    function OcultarPreguntas() {
        if (divpreguntas.style.display == "none") {
            divpreguntas.style.display = ""
        }
        else {
            divpreguntas.style.display = "none"
        }
    }

</script>

</head>

<body bgcolor="#F0F0F0">
<p><span class="usatTitulo">Habilitar cursos para uso del Aula Virtual</span></p>
<% '#if (rsCarga.BOF and rsCarga.EOF) then
  	'#	response.write "<h5 class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado carga acad�mica registrada para el ciclo acad�mico actual</h5>"
  	'#else
  	
   if (rsCac.BOF and rsCac.EOF) then
      response.write "<h5 class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado ciclos acad�micos activos</h5>"
   else
  	
%>
<!--'#rsCarga("login_per")-->
<form name="frmcurso" method="POST" Action="procesar.asp?accion=agregarcursoMoodle&codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&login_per=<%=session("codigo_usu")%>">
<table cellpadding="3" width="100%" class="contornotabla">
	<tr>
		<td class="etiqueta">Seleccionar Ciclo Acad�mico </td>
		<td width="75%"><%'#call llenarlista("cboCiclo","actualizarlista('frmactivaraula.asp?codigo_cac=' + this.value)",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")
		                    call llenarlista("cboCiclo","actualizarlista('frmactivaraula.asp?codigo_cac=' + this.value)",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"Seleccione","","")
		%></td>
	</tr>
	<tr>
		<td width="25%" class="etiqueta">Profesor</td>
		<td>: <%=session("nombre_usu")%>&nbsp;</td>
	</tr>
</table>
<br>	
-


<!--<input type="button" class="enviaryrecibir1"  value="        Habilitar"  
    disabled=true onClick="alert('Las aulas ser�n activadas esta semana.')" 
    name="cmdGuardar" style="width: 170px">-->
<br>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  <tr class="etabla">
    <td width="3%" height="26">&nbsp;</td>
    <td width="5%" height="26">#</td>
    <td width="12%" height="26">C�digo</td>
    <td width="30%" height="26">Descripci�n</td>
    <td width="10%" height="26">GH</td>
    <td width="5%" height="26">Ciclo</td>
    <td width="30%" height="26">Escuela Profesional</td>
    <td width="5%" height="26">Matriculados</td>
    <td width="5%" height="26">S�labo</td>
    <!--<td width="5%" height="26">Importar Documentos</td>-->
    <!--<td width="5%" height="26">Aula Virtual</td>-->
  </tr>
  <%
  	'#do while not rsCarga.EOF 	
 	'#i=i+1
 	
 	if (request.querystring("codigo_cac")= "")  then
        response.write "<h5 class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Debe seleccionar un ciclo acad�mico para listar la carga acad�mica</h5>"
    else
        if (rsCarga.BOF and rsCarga.EOF) then
          response.write "<h5 class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No existe carga acad�mica para el ciclo seleccionado </h5>"
        else
  	       do while not rsCarga.EOF 	
 	        i=i+1
 	
  %>
  <tr>
        <td width="3%" align="center" bgcolor="#FFFFFF" height="20">

<!--<input type="checkbox" onClick="VerificaCheckMarcados(chkcursoshabiles,cmdGuardar)" name="chkcursoshabiles" value="<%=rsCarga("codigo_cup")%>">-->
    <%
     '#
        'Evaluar si no esta el curso registrado, no permitir marcar. yperez
        'Set objRS = objConn.Execute("select count(1) as num from " & Prefix & "course where shortname = " & rsCarga("codigo_cup"))
           ' existeCursoenMoodle = cint (objRS.Fields("num"))            
           if (rsCarga("mdl") > 0) then
                check = "<input type='checkbox' disabled='' checked='' onClick='' name='' value='" & rsCarga("codigo_cup") & "'>"
           else 
                check = "<input type='checkbox'  onClick='validarcargaacademica()' name='chkcursoshabiles' value='" & rsCarga("codigo_cup") & "'>"
           end if
           response.Write(check)
    %>
    
    </td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=i%>&nbsp;</td>
    <td width="12%" bgcolor="#FFFFFF" height="20"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_Cur")%>&nbsp;
	<input type="hidden" name="C<%=rsCarga("codigo_cup")%>" id="C<%=rsCarga("codigo_cup")%>" value="<%=rsCarga("nombre_Cur")%>"  />
	</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
    <td width="5%" align="center" bgcolor="#FFFFFF" height="20"><%=ConvRomano(rsCarga("ciclo_cur"))%>&nbsp;</td>
    <td width="30%" bgcolor="#FFFFFF" height="20"><%=rsCarga("nombre_cpf")%><span style="font-size: 7pt"> (<%=rsCarga("descripcion_pes")%>)</span>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%=rsCarga("matriculados")%>&nbsp;</td>
    <td width="10%" align="center" bgcolor="#FFFFFF" height="20"><%
            dim silabo
            if Isnull(rsCarga("fechasilabo_cup"))=true then
                silabo = "<img src='../../../../images/bloquear.gif' border='0' alt='S�labo no disponible'>"
            else
               silabo = "<a href='../../../../silabos/" & rsCarga("descripcion_cac")& "/" & rsCarga("codigo_cup")& ".zip'><img src='../../../../images/zip.gif' ALT='Ver Silabus Registrado' border=0></a>" 
            end if
            response.Write (silabo)
        %>&nbsp;</td>
    
   
  </tr>
  	<%
  	    '#rsCarga.movenext
        '#loop
        '#Set rsCarga=nothing
            rsCarga.movenext
            loop
            Set rsCarga=nothing
            end if
        end if        
   %>
</table>
  <br /><br />
  <span id="Span1" style="color:blue; font-size:12px;">Configuraci�n Adicional, para los cursos seleccionados:</span>
  
  <table border=0 width="40%" >
  
  <tr>
    <td width="50%" align="left" bgcolor="#FFFFFF" height="20">
        Formato: <select name="avformatsections"><option value="topics">Temas</option><option value="weeks">Semanas</option></select>
    </td>
    
    <td width="50%" align="left" bgcolor="#FFFFFF" height="20">Nro Secciones: <select name="avnumbersections"><%
        for i = 1 to  52 
               if i = 17 then
                 sel = " selected=""selected"" "
               end if               
            response.Write("<option value='"& i &"' " & sel & " >" & i & "</option>")             
            sel = ""
        next
    %></select>
    </td>
    <td><input type="button" class="enviaryrecibir1"  value="        Habilitar Aula"  disabled=true onClick="ConfirmarCurso()" name="cmdGuardar" style="width: 170px"> </td>
  </tr><!--
  <tr><td colspan=2> </br><span id="Span2" style="color:#000000; font-size:9px;">Usted puede cambiar esta configuraci�n desp�es de habilitar el aula, en el bloque de Administraci�n del Aula Virtual</span>
  </td></tr>-->
</table>
</form>

<p class="usattitulousuario" style="cursor:hand">
 
<!-- <br /><br />AVISO</p>
<p>Estimado Docente, el aula virtual ser� habilitada a partir del <b>25/07/2012</b><br /></p>-->



<!--<p class="usattitulousuario" style="cursor:hand" onclick="OcultarPreguntas()">-->
<p class="usattitulousuario" style="cursor:hand">
 
 <br /><br />Preguntas Frecuentes</p>
 
<!--<div id="divpreguntas">-->
<div>

<p class="usatEtiqOblig">1. �C�mo activar un curso en el aula virtual?</p>
<p> Si tiene carga ac�demica registrada, ver� un listado de los cursos. Para activar deber� marcar el check del curso y luego hacer clic
en el bot�n <b>Habilitar Aula Virtual</b></p>

<p class="usatEtiqOblig">2. �C�mo acceder a mis cursos virtuales?</p>
<p>Para acceder a los cursos virtuales activados, deber� dirigirse al enlace <b>"Ingresar a Aula Virtual"</b> y luego dar clic en <b>"Clic para Ingresar"</b></p>

<!--<p class="usatEtiqOblig">1. �Qu� debo hacer para agrupar asignaturas en el aula virtual?</p>
<p>Solicitar por email a evaluaci�n y registros, el agrupamiento de asignaturas indicando las asignaturas que se agrupar�n y el motivo.</p>
-->

<!--<p class="usatEtiqOblig">2. �C�mo copio documentos de asignaturas anteriores a una asignatura en el ciclo actual? </p>
<p>Primero, debe <strong>habilitar el aula virtual</strong> para la asignatura correspondiente, luego debe hacer clic en el �cono
<img src="../../../../images/menus/attachfiles_small.gif" width="25" height="25"> para indicar de qu� asignatura se copiar�n los documentos..</p>
-->

<p class="usatEtiqOblig">3. �Qu� debo hacer si no tengo asignada mi carga acad�mica para el ciclo actual?</p>
<p>Solicitar por email a <strong>Evaluaci�n y Registros</strong>, el registro de la carga acad�mica en el Sistema.</p>

</div>

<%end if%>
</body>
</html>
<%end if%>
