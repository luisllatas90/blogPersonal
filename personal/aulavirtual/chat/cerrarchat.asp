<%
	Set Obj= Server.CreateObject("AulaVirtual.clschat")
		call Obj.EliminarMensajes(session("idsesion"))
	Set Obj=nothing
%>
<script>
	window.opener.location.reload()
	top.window.close()
</script>