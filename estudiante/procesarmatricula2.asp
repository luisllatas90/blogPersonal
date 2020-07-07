<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
'*************************************************************************************
'CV-USAT
'Fecha de Creación: 29/07/2006
'Autor			: Gerardo Chunga Chinguel
'Fecha de Modificación: 23/02/2007
'Observaciones	: Permite procesar la matrícula, agregado/retiro de asignaturas
'*************************************************************************************

call Enviarfin(session("codigo_usu"),"../")
On error resume next

'*************************************************************************************
'Declarar variables
'*************************************************************************************
Dim CursosProgramados
Dim VecesDesprobados

'accion = Request.querystring("param4")
codigo_alu = session("codigo_alu")
codigo_cac = session("codigo_cac")
'codigo_pes = session("Codigo_Pes")
'codigo_mat = Request.querystring("param5")
'on error resume next 
Set objRequisito = Server.CreateObject("PryUSAT.clsAccesoDatos")
objRequisito.AbrirConexion
    objRequisito.Ejecutar "MAT_ActualizarRequisitosCursoAsesor", false, codigo_alu, codigo_cac
objRequisito.CerrarConexion
Session("prereq") = "S"
'response.Write "MAT_ActualizarRequisitosCursoAsesor " & codigo_alu & " , " & codigo_cac
'objMatricula.Ejecutar "Mat_actualizarprerequisitos", false, codigo_alu, 0, codigo_cac 
'Response.Redirect("frmmatricula.asp?accion=" & accion & "&codigo_pes=" & codigo_pes & "&codigo_mat=" & codigo_mat)
Response.Redirect("frmmatricula.asp")

'response.Write err.Description

%>