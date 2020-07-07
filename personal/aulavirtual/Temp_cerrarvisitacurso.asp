<%
idvisita=session("idvisita_sistema")
if idvisita="" then idvisita=0
	Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")	
	'linea siguiente anulada hasta que se revise el problema
	' Gregorio León./Arreglado 02-04-2009
	s=Obj.AgregarVisitasRecurso("S",session("codigo_usu"),"cursovirtual",session("idcursovirtual"),session("Equipo_bit"),session("idvisita_sistema"),session("idcursovirtual"))
	Set Obj=nothing
response.Write ("<script>top.location.href='listaaplicaciones.asp'</script>")
%>