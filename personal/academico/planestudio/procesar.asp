<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../funciones.asp"-->
<%
'******************************************************************************************************
'CV-USAT
'Fecha de Creación	: 26/04/20059:41:09
'Autor		: USAT
'Observaciones	: Permite Agregar datos - Tabla curso
'actualizado el 11/06/05
'******************************************************************************************************
accion=request.querystring("accion")
codigo_pes=request.querystring("codigo_pes")
codigo_cur=request.querystring("codigo_cur")
estado=request.querystring("estado")

	if accion="agregarplanestudio" then
	Dim objplanestudio
	
	strdescripcion_Pes=Request.form("txtdescripcion_Pes")
	strabreviatura_Pes=Request.form("txtabreviatura_Pes")
	bytcodigo_cpf=Request.form("cbocodigo_cpf")
	strcicloacadinicio_pes=Request.form("txtcicloacadinicio_pes")
	'blnvigencia_pes=Request.form("chkvigencia_pes")
	blnvigencia_pes="1"
	dtmFechaTerminoVigencia_Pes="12/12/05"
	strnumerodoc_pes=Request.form("txtnumerodoc_pes")
	dtmfechadoc_pes=cdate(Request.form("txtfechadoc_pes"))
	'dtmfechadoc_pes="12/12/05"
	byttotalcreobl_pes=Request.form("txttotalcreobl_pes")
	inttotalcredelecobl_pes=Request.form("txttotalcredelecobl_pes")
	inttotalhoras_pes=Request.form("txttotalhoras_pes")
	strtipoperiodo_pes=Request.form("cbotipoperiodo_pes")
	bytcantidadperiodo_pes=Request.form("txtcantidadperiodo_pes")
	strsumillas_pes=Request.form("txtsumillas_pes")
	strmallacurricular_pes=Request.form("txtmallacurricular_pes")

	Set objplanestudio= Server.CreateObject("PryUSAT.clsDatPlanestudio")
	objplanestudio.AgregarPlanestudio strdescripcion_Pes, strabreviatura_Pes, bytcodigo_cpf, strcicloacadinicio_pes, blnvigencia_pes, dtmFechaTerminoVigencia_Pes, strnumerodoc_pes,dtmfechadoc_pes, byttotalcreobl_pes, inttotalcredelecobl_pes, inttotalhoras_pes, strtipoperiodo_pes, bytcantidadperiodo_pes, strsumillas_pes, strmallacurricular_pes
	Set objplanestudio=Nothing
	'for each control in request.form
		'response.write control & ": " & request.form(control) & "<br>"
	'next
	
	end if


	if accion="cambiarvigenciacurso" then
		cursos=split(request.form("chk"),",")
		
		Set obj=Server.CreateObject("PryUSAT.clsDatPlanestudio")		
			For I=LBound(cursos) to UBound(cursos)
				codigo_cur=Trim(cursos(I))
				call obj.CambiarVigenciaDatos("VCP",codigo_pes,codigo_cur,estado)
			next
	  	Set obj=nothing
	  	response.redirect "lstcursosplan.asp?codigo_pes=" & codigo_pes
	end if
	
	if accion="agregarplancurso" then
		Set objplancurso= Server.CreateObject("PryUSAT.clsDatPlanestudio")
		rpta = objplancurso.AgregarPlancurso (Request.form("txtcodplan"),Request.form("txtcodcur"),Request.form("cbotipo_cur"),Request.form("cbociclo_cur"),Request.form("optelectivo_cur"),Request.form("txthorasteo_cur"), Request.form("txthoraspra_cur"),Request.form("txttotalhorascur"),Request.form("txtcreditos_cur"),Request.form("txthoraslab_cur2"),Request.form("txthorasase_cur"),Request.form("chkpractica_cur"),Request.form("txtssdi_cur"),Request.form("txtcfu_cur"),Request.form("txtEstado"))
		if rpta <>"OK" THEN
		%>
		<script>
			alert("El curso ya existe en este plan");
		</script>		
		<%
		end if
		Set objplancurso=Nothing
	end if
	
	
	if accion="modificarplancurso" then
		codigo_cur=split(request.form("codigo_cur"),",")
		totalhoras_cur=split(request.form("totalhoras_cur"),",")
		estado_pcu=split(request.form("estado_pcu"),",")
		tipo_cur=split(request.form("tipo_cur"),",")
		creditos_cur=split(request.form("creditos_cur"),",")
		horasteo_cur=split(request.form("horasteo_cur"),",")
		horaspra_cur=split(request.form("horaspra_cur"),",")
		horaslab_cur=split(request.form("horaslab_cur"),",")
		horasase_cur=split(request.form("horasase_cur"),",")
		ciclo_cur=split(request.form("ciclo_cur"),",")
		electivo_cur=split(request.form("electivo_cur"),",")
	
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
			For I=LBound(codigo_cur) to UBound(codigo_cur)
				call obj.Ejecutar("modificarplancurso",false,codigo_Pes,Trim(codigo_Cur(I)),Trim(tipo_cur(I)),Trim(ciclo_cur(I)),Trim(electivo_cur(I)),Trim(horasteo_cur(I)),Trim(horaspra_cur(I)),Trim(totalhoras_cur(I)),Trim(creditos_cur(I)),Trim(horaslab_cur(I)),Trim(horasase_cur(I)),0,0,0,Trim(estado_pcu(I)),session("codigo_usu"))
			next
			call obj.Ejecutar("actualizardatosplanestudio",false,"P",codigo_pes)
			obj.CerrarConexion
	  	Set obj=nothing
	  	response.write("<script>alert('Se han guardado los cambio correctamente');location.href='lstcursosplan.asp?codigo_pes=" & codigo_pes & "'</script>")
	  	'response.redirect "lstcursosplan.asp?codigo_pes=" & codigo_pes
	end if
	
	if accion="modificarcurso" then
		complementario_Cur=request.form("chkcomplementario_Cur")
		if complementario_cur="" then complementario_cur=0
		
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")	
			Obj.AbrirConexion
				mensaje=obj.Ejecutar("ModificarCurso",true,codigo_cur,request.form("txtidentificador_cur"),request.form("txtnombre_cur"),complementario_Cur,request.form("cbocodigo_dac"),session("Usuario_bit"),null)
			Obj.CerrarConexion
	  	Set obj=nothing
	  	
		pagina="administrarcurso/frmcurso.asp?accion=" & accion & "&codigo_cur=" & codigo_cur	
		
		response.write "<script>alert('" & mensaje & "');location.href='" & pagina & "'</script>"
	end if
%>