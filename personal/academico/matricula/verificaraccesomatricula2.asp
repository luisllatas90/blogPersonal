<%
rutaActual=request.querystring("rutaActual")

codigo_alu=session("codigo_alu")
codigo_pes=session("codigo_pes")
estadoDeuda_alu=session("estadoDeuda_alu")
UltimoEstado=session("UltimoEstado")
codigo_cpf=session("codigo_cpf")

'---------------------------------------------
'INDICAR MANUAL CODIGO_CAC Y DESCRIPCION_CAC
'---------------------------------------------
tipo_cac="E"
codigo_cac=35
descripcion_Cac="2010-0"
'---------------------------------------------


If 	estadodeuda_alu=1 and _
	UltimoEstadoMatricula="N" and _
	codigo_cpf<>"25"  then

	response.redirect "mensajes.asp?proceso=" & modo
else
	'****************************************
	'Verificar acceso a pre-matrícula
	'****************************************
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesorComplementario","FO",codigo_alu,Codigo_Cac,Codigo_Pes,rutaActual,session("codigo_tfu"))
	pagina=rsMatricula(0)
	Obj.CerrarConexion
	Set Obj=nothing
	rsMatricula.close
	Set rsMatricula=nothing
	
	response.redirect(pagina)
	'response.write (codigo_alu & "," & Codigo_Cac & "," & Codigo_Pes & "," & rutaActual & "," & session("codigo_tfu"))
end if
%>