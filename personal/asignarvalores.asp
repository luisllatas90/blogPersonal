<!--#include file="../funciones.asp"-->
<%
'---------------------------------------------------------------------------------
'CV-USAT
'Fecha de Creación: 30/05/2005
'Autor			: Gerardo Chunga Chinguel
'Observaciones	: Permite asignar los valores de los controles/querystring
'---------------------------------------------------------------------------------

Dim CursosProgramados,TipoCursos,CreditoCursos,VecesDesprobados
Dim ArrCP,ArrTC,ArrCR,ArrVD
Dim codigo_matricula
Dim PensionTotalCurNorm
Dim PensionTotalCurComp
Dim PrecioCrd


Dim codigo_per
Dim codigosmatriculados
Dim ArrCM,ArrNT,ArrCD

'Controles para menú
Dim descripcion_men
Dim enlace_men
Dim codigoraiz_men
Dim icono_men
Dim iconosel_men
Dim expandible_men
Dim orden_men
Dim variable_men
Dim nivel_men

Sub asignarcontrolesmatricula()
	CursosProgramados=verificacomaAlfinal(Request.querystring("CursosProgramados"))	
	TipoCursos=verificacomaAlfinal(Request.querystring("TipoCursos"))
	CreditoCursos=verificacomaAlfinal(Request.querystring("CreditoCursos"))
	VecesDesprobados=verificacomaAlfinal(Request.querystring("VecesDesprobados"))
		
	ArrCP=split(CursosProgramados,",")
	ArrTC=split(TipoCursos,",")
	ArrCR=split(CreditoCursos,",")
	ArrVD=split(VecesDesprobados,",")
End Sub

Sub asignarcontrolesnota()
	'Verifica si hay coma al final de c/variable querystring
	codigo_per=verificacomaAlfinal(Request.querystring("codigo_per"))	
	codigo_curso=verificacomaAlfinal(Request.querystring("cursosprogramados"))
	codigo_mat=verificacomaAlfinal(Request.querystring("codigosmatriculados"))
	nota_mat=verificacomaAlfinal(Request.querystring("notascursos"))
	cond_cur=verificacomaAlfinal(Request.querystring("condicioncursos"))
	
	ArrCP=split(codigo_curso,",")
	ArrCM=split(codigo_mat,",")
	ArrNT=split(nota_mat,",")
	ArrCD=split(cond_cur,",")
End Sub

 Sub asignarcontrolesmenuaplicacion()
	descripcion_men=Request.form("txtdescripcion_men")
	enlace_men=Request.form("txtenlace_men")
	codigoraiz_men=Request.form("cbocodigoraiz_men")
	icono_men=Request.form("txticono_men")
	iconosel_men=Request.form("txticonosel_men")
	expandible_men=Request.form("chkexpandible_men")
	orden_men=Request.form("txtorden_men")
	variable_men=Request.form("txtvariable_men")
	nivel_men=Request.form("cbonivel_men")
  End Sub
%>