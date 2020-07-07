<!--#include file="clshorarios.asp"-->
<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cac=request.querystring("codigo_cac")
ciclo_cur=request.querystring("ciclo_cur")
grupohor_cup=request.querystring("grupohor_cup")
if codigo_cpf="" then codigo_cpf="-2"
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
<%    
    set ObjCnx= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Set obj= Server.CreateObject("PryUSAT.clsDatCurso")
		Set rsCursos=obj.ConsultarCursoProgramado("RS","5",codigo_cpf,codigo_cac,ciclo_cur,grupohor_cup)
	Set obj=nothing

	if Not(rsCursos.BOF and rsCursos.EOF) then
		if ((cint(codigo_cac) <= cint(session("codigo_cac"))) or (session("codigo_tfu")=147 _
		        or session("codigo_tfu")=15 or session("codigo_tfu")=41 _
		        or session("codigo_tfu")=9 or session("codigo_tfu")= 109 _
		        or session("codigo_tfu")= 11 or session("codigo_tfu")= 25 _
		        or session("codigo_tfu")= 103 or session("codigo_tfu")= 23 _
				or session("codigo_tfu")= 1 or session("codigo_tfu")= 116 or session("codigo_tfu")= 85 or session("codigo_tfu")= 212 )) then
		Set obj= Server.CreateObject("PryUSAT.clsDathorario")
			Do while not rsCursos.EOF
				i=i+1
                %>
                <table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tblcursosprogramados" class="bordepestana" bgcolor="#FFFCBF">
                  <tr>
                    <td width="12%" class="etiqueta">ASIGNATURA</td>
                    <td width="100%" colspan="5"><%=rsCursos("nombre_cur")%></td>
                  </tr>
                  <tr>
                    <td width="12%" class="etiqueta">CÓDIGO</td>
                    <td width="20%"><%=rsCursos("identificador_cur")%></td>
                    <td width="20%" align="right" class="etiqueta">CICLO:</td>
                    <td width="20%"><%=ConvRomano(rsCursos("ciclo_cur"))%></td>
                    <td width="20%" class="etiqueta" align="right">GRUPO HORARIO:</td>
                    <td width="20%"><%=rsCursos("grupohor_cup")%>&nbsp;</td>
                  </tr>
                </table>
                <%
				'Set rsHorario = obj.ConsultarHorarios("RS","5",rsCursos("codigo_cup"),1,0)				
	            ObjCnx.AbrirConexion	
	            Set rsHorario = ObjCnx.Consultar("ACAD_HorarioCursoClonado", "FO", rsCursos("codigo_cup"))
	            ObjCnx.CerrarConexion
	            
				response.write Vistahorario(rsHorario)
				if i mod 3 = 0 then
					response.write "<p class=SaltoDePagina>&nbsp;</p>"
				else
					response.write "<br>"
				end if
				rsCursos.movenext
			Loop
		Set rsCursos=nothing
		Set obj=nothing
		Set rsHorario=nothing
		else
		    response.write "<h3>Usted no puede visualizar horarios de semestres superiores al actual</h3>"
		end if 
	else
		response.write "<h3>No se han programado cursos para el semestre académico actual</h3>"
	end if
	
%>
</body>
</html>