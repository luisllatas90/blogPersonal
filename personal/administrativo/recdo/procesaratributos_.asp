<!--#include file="../../../NoCache.asp"-->
<!--#include file="asignarcontroles.asp"-->
<%
if session("codigo_usu")="" then response.Redirect "../../../tiempofinalizado.asp"

tabla=request.querystring("tabla")
modo=request.QueryString("modo")

select case modo
	case "agregarprocedencia"
		controlesprocedencia		
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.agregarprocedencia(tipoprocedencia,razon,direccion,telefono,email)
		Set Obj=nothing
	case "modificarprocedencia"
		controlesprocedencia
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.modificarprocedencia(idprocedencia,tipoprocedencia,razon,direccion,telefono,email)
		Set Obj=nothing
		
	case "eliminarprocedencia"
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.eliminarprocedencia(idprocedencia)
		Set Obj=nothing
	case "agregardestinatario"
		controlesdestinatario		
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.agregardestinatario(nombre,tipodestinatario)
		Set Obj=nothing
	case "modificardestinatario"
		controlesdestinatario		
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.modificardestinatario(iddestinatario,nombre,tipodestinatario)
		Set Obj=nothing
		
	case "eliminardestinatario"
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			call Obj.eliminardestinatario(iddestinatario)
		Set Obj=nothing
end select

'Enviar a página de búsqueda

if tabla="procedencia" then
	controltbl=request.form("razon")
else
	controltbl=request.form("nombre")
end if

response.Redirect "frmbuscadorprop.asp?tabla=" & tabla & "&criterio=" & controltbl
%>