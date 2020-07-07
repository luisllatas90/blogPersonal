<%
idvisita=session("idvisita_sistema")
if idvisita="" then idvisita=0
	Set Obj= Server.CreateObject("AulaVirtual.clsDatAplicacion")
        'Siguiente Linea anulada hasta que se evalue el problema de acceso
		'Gregorio Leon	
		s=Obj.AgregarVisitasRecurso("S",session("codigo_usu"),"cursovirtual",session("idcursovirtual"),session("Equipo_bit"),session("idvisita_sistema"),0)
	Set Obj=nothing
	response.Write ("<script>top.location.href='listaaplicaciones.asp'</script>")
%>