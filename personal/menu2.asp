<html>
<body Onload="document.getElementById('frmmenu').submit()">
<form name="frmmenu" id="frmmenu" method="POST" action="carpetas2.aspx">
	<input type="hidden" name="id" value="<%=session("codigo_usu")%>" />
	<input type="hidden" name="ctf" value="<%=session("codigo_tfu")%>" />
	<input type="hidden" name="capl" value="<%=session("codigo_apl")%>" />
</form>

</body>
</html>
