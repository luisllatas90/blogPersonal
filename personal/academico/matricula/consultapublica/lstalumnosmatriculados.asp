<!--#include file="../../../../funciones.asp"-->
<%
rpte=request.querystring("rpte")
codigo_cac=request.querystring("codigo_cac")
descripcion_cac=request.querystring("descripcion_cac")
estado_mat=request.querystring("estado_mat")
codigo_cpf=request.querystring("codigo_cpf")

identificador_cur=request.querystring("identificador_cur")
nombre_cur=request.querystring("nombre_cur")
grupohor_cup=request.querystring("grupohor_cup")
codigo_cup=request.querystring("codigo_cup")

if codigo_cac<>"" then
%>
<html>

<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
</head>
<body leftmargin="0">
<%
Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio

	if rpte="mat-esc" then
		Set obj=Server.CreateObject("PryUSAT.clsDatMatricula")
			Set rsMatriculados=obj.consultarMatricula("RS","10",codigo_cac,codigo_cpf,estado_mat)
		Set Obj=nothing

		ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Sem. Ingreso","Nº Créditos","Nº Cursos")
		ArrCampos=Array("codigouniver_alu","alumno","CicloIng_Alu","creditos_mat","cursos_mat")
		ArrCeldas=Array("10%","60%","10%","15%","15%")
		titulorpte="Consolidado de Matrículas " & descripcion_cac
	end if
	
	if rpte="mat-cur-gh" then
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
				Set rsMatriculados=obj.Consultar("ConsultarAlumnosMatriculados","FO",3,codigo_cac,estado_mat,codigo_cup)
			Obj.CerrarConexion
		Set Obj=nothing
	
		ArrEncabezados=Array("Código Universitario","Apellidos y Nombres","Escuela Profesional","Sem. Ingreso")
		ArrCampos=Array("codigouniver_alu","alumno","nombre_cpf","CicloIng_Alu")
		ArrCeldas=Array("10%","40%","30%","20%")
	end if
	
	call CrearRpteTabla(ArrEncabezados,rsMatriculados,"",ArrCampos,ArrCeldas,"S","","","S",null,null)
	call ValoresExportacion2(titulorpte,ArrEncabezados,rsMatriculados,Arrcampos,ArrCeldas)
%>
</body>
</html>
<%end if%>