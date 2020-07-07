<!--#include file="../NoCache.asp"-->
<%
session("codigo_apl")=request.querystring("codigo_apl")
session("codigo_tfu")=request.querystring("codigo_tfu")
session("descripcion_apl")=request.querystring("descripcion_apl")
session("enlace_apl")=request.querystring("enlace_apl")
estilo_apl=request.querystring("estilo_apl")
        Set rsAcreUniv = Server.CreateObject("ADODB.Recordset")
        dim sw 
        sw=0
		Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			obj.AbrirConexion
			encuestas=obj.Ejecutar("ENC_ModoDeSer",true,session("codigo_alu"),0)
		    set rsAcreUniv = obj.consultar("AUN_ConsultarEstadoAcreditacionUniversitaria","FO", "TO",session("codigo_alu"))
            if rsAcreUniv.RecordCount > 0 then
		        sw=1
		    else
		        sw=0
		    end if
		    obj.CerrarConexion
		Set obj=nothing

if sw=0 then
   Response.redirect("../librerianet/encuesta/AcreditacionUniversitaria_generales.aspx?sesion=" & session("codigo_alu"))
Else
   if encuestas=true then
	   Response.redirect("mododeser.asp")
   else
   		'Set obj1=Server.CreateObject("PryUSAT.clsAccesoDatos")
			'obj1.AbrirConexion
			'set rsEleccion = obj1.Consultar("AVI_DelegadosSistemas2009","FO","CO",session("codigo_alu"),0,0)
			'obj1.CerrarConexion
			'if rsEleccion.RecordCount=0 and session("codigo_cpf")=3 then
			'	response.redirect "delegadosistemas/elecciondelegados.asp"	
			'else
			    response.redirect "principal.asp?pagina=" & session("enlace_apl")
			'end if

   end if
end if	
%>	