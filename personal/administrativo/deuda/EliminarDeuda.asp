<%@ Language=VBScript %>
<%
    Dim objDeuda
	Set objDeuda= Server.CreateObject("PryUSAT.clsDatDeuda")
	

	for each coddeuda in request.Form("chkEliminar")
		objDeuda.EliminarDeuda coddeuda
	next

	Set objDeuda=Nothing	
	a=Request.form("txtCodResp")
	b=Request.form("txtTipoResp")
	ruta="frmConsultarDeuda.asp?" & "id=" & a & "&" & "tr=" & b
	response.Redirect(ruta)
%>