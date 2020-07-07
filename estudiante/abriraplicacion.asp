<!--#include file="../NoCache.asp"-->
<%
on error resume next
session("codigo_apl")=request.querystring("codigo_apl")
session("codigo_tfu")=request.querystring("codigo_tfu")
session("descripcion_apl")=request.querystring("descripcion_apl")
session("enlace_apl")=request.querystring("enlace_apl")
estilo_apl=request.querystring("estilo_apl")

		'Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			'Obj.AbrirConexion
				'encuestas=obj.Ejecutar("ENC_ModoDeSer",true,session("codigo_alu"),0)
			'Obj.CerrarConexion
		'Set obj=nothing

'if encuestas=true then        
	'Response.redirect("mododeser.asp")
'else    
	'********************** ANTES 18-04-2012 **********************
	'response.redirect "principal.asp?pagina=" & session("enlace_apl")		
	
	'********************** DESPUES 18-04-2012 **********************	
	'response.Write "OK Abrir aplicacion"
	
	'para encuesta DD Yperez
	'if Session("TieneEncuestaDocente")>0 and request.querystring("op") <> "1"  then
       ' response.Redirect ("AsignaSesiones.aspx?x=" & Session("TieneEncuestaDocente") & "&y=" & session("codigo_alu") & "&z=../librerianet/Encuesta/EvaluacionAlumnoDocente/EvaluacionDocente_Estudiante.aspx")
    'end if
    
	response.Redirect ("AsignaSesiones.aspx?x=" & session("Codigo_Cac") & "&y=" & session("codigo_alu") & "&z=principal.asp")'tgd cambiar a principal 2 para egresados
	'response.redirect "principal.asp"
'end if	
if Err.number <> 0 then    
    response.Write "<br/>" & (err.Description)
end if
%>