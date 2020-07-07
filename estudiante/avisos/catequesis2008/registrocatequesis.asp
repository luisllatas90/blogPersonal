<%
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion			
		'	response.Write("codigo_usu: " & session("codigo_usu") & " - fono: " &  Request.Form("txtTelefono") & " - celu: " &  Request.Form("txtcelular") & " - sacramento: " &  Request.Form("cboSacramento") & " - direccion: " &  Request.Form("txtdireccion"))
		 obj.Ejecutar "ALU_ActualizarDatosAlumnoCapellania",false,session("codigo_usu"), Request.Form("txtTelefono"), Request.Form("txtcelular"), Request.Form("cboSacramento"), Request.Form("txtdireccion"),Request.Form("txtemail")
					
		obj.CerrarConexion
		Response.Redirect("catequesis.asp")
%>