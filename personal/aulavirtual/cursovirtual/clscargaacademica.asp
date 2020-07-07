<%
'***************************************************************************************
'CV-USAT
'Archivo			: clscargaacademica
'Autor				: USAT
'Fecha de Creación	: 09/11/200511:09:58 a.m.
'Observaciones		: Clase para procesos de módulo de notas
'***************************************************************************************

Class clscargaacademica

Public Function ConsultarDocente(byval tipo,byval param1,byval param2)
	Set Obj=Server.CreateObject("PryUSAT.clsDatDocente")
	Set ConsultarDocente= Obj.ConsultarDocente("RS",tipo,param1,param2)
	Set Obj=nothing
End Function

Public Function ConsultarCicloAcademico(byval tipo,byval param1)
	Set Obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	'Set rsDoc=Server.CreateObject("ADODB.Recordset")
	Set ConsultarCicloAcademico= Obj.ConsultarCicloAcademico ("RS",tipo,param1)
	Set Obj=nothing
End Function

Public function ConsultarCargaAcademica(Byval codigo_cac,Byval codigo_per)
	Set Obj=Server.CreateObject("PryUSAT.clsDatDocente")
	Set ConsultarCargaAcademica=Obj.ConsultarCargaAcademica("RS","1",codigo_cac,codigo_per)
	Set Obj=nothing
End function


Public function ConsultarRegistroNotas(Byval codigo_cup)
	Set Obj=Server.CreateObject("PryUSAT.clsDatMatricula")
	Set ConsultarRegistroNotas=Obj.ConsultarAlumnosMatriculados("RS","RN",codigo_cup,"","")
	Set Obj=nothing
End Function

End Class
%>