<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../../funciones.asp"-->
<%
dim codigo_per

codigo_per=session("codigo_usu")

call Enviarfin(session("codigo_usu"),"../../../../")

modo=request.querystring("modo")
accion=request.querystring("accion")
Tipo_Dpe=request.querystring("Tipo_Dpe")
codigo_Ped=request.querystring("codigo_ped")
codigo_Dpe=request.querystring("codigo_dpe")
'idIngreso=request.querystring("idIngreso")
IdLibro=Request.QueryString("IdLibro")
codigo_eped=request.querystring("codigo_eped")

function MensajeConfirmacion(ByVal tipo,ByVal mensaje)
	on error resume next
	Dim arrMensaje
	Dim script

'	if instr(mensaje,"|")>0 then
'		arrMensaje=split(mensaje,"|")
'		mensaje=trim(arrMensaje(0))
'		session("codigo_ped")=trim(arrMensaje(1))
'	end if
		
'	if err.number>0 then
'		mensaje="Ha ocurrido un error al procesar la información"
'		retcodigo=0
'	end if
	
	select case tipo
		case "M"
			script="<script>alert('" & mensaje & "');window.parent.opener.location.reload();top.window.close()</script>"
		case "A"
			script="<script>alert('" & mensaje & "');top.window.close()</script>"
	end select
	MensajeConfirmacion=script
end function

if accion="agregardetallepedido" then

	codigo_cco=request.querystring("codigo_cco")
	precio=Request.QueryString("precio")
	if precio ="" then precio=0
	moneda=Request.QueryString("moneda")
	Cantidad_Dpe=request.form("txtCantidad_Dpe")
	codigo_Jpe=request.form("cbocodigo_Jpe")

	if trim(codigo_Jpe)="" then
		codigo_Jpe=null
		Justificacion_Dpe=ucase(trim(request.form("txtJustificacion_Dpe")))
	end if
	
	Codigo_cur=request.form("txtCodigo_cur") 
	
	if trim(Codigo_cur)="" then
		Codigo_cur=null
	end if

	Tema_Dpe=trim(request.form("txtTema_Dpe"))
	codigo_eped=1
	
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
	Obj.AbrirConexion
		mensaje=Obj.Ejecutar("AgregarDetallePedido",true,modo,codigo_Dpe,Codigo_Ped,codigo_cco,Tipo_Dpe,IdLibro,Cantidad_Dpe,Justificacion_Dpe,Codigo_cur,ucase(Tema_Dpe),codigo_eped,codigo_per,codigo_Jpe,moneda,precio,null)
		Obj.CerrarConexion
	Set obj=nothing
''  Response.Write(precio)	
''  Response.Write(moneda)
	response.Write("<script>window.opener.location.href='lstborrador.asp'; window.close();</script>")

	'response.write(MensajeConfirmacion(modo,mensaje))
''	response.write request.form & "<br>" & request.querystring
end if

if accion="eliminardetallepedido" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			codigo_ped=Obj.Ejecutar("EliminarDetallePedido",true,codigo_dpe,session("codigo_ped"),null)
		Obj.CerrarConexion
	Set obj=nothing
	session("codigo_ped")=codigo_ped
	
	response.redirect "lstdetallepedido.asp"
end if

if accion="EnviarPedido" then
	tipoenvio=request.querystring("tipoenvio")
	obs_evp=request.querystring("txtobs_evp")
	tipo=Request.QueryString("tipo")
	if obs_evp="" then obs_evp=null
	
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			Obj.Ejecutar "BI_CalificarPedido",false,Request.QueryString("Codigo_Ped"), session("codigo_usu")
		Obj.CerrarConexion
	Set obj=nothing
	
	codigo_ins=1
	session("codigo_ped")=0
	if tipoenvio="P" then
		pagina="lstpedidos.asp"
	else
		pagina="lstborrador.asp"
	end if
	response.write "<script>alert('" & "Se ha enviado el pedido a la instancia siguiente" & "');location.href='" & pagina & "?tipobandeja=S&codigo_ins=" & codigo_ins & "&codigo_eped=1&menu=Pedidos bibliográficos pendientes&tipo=" & tipo &"&codigo_usu=" & codigo_per & "'</script>"
end if

if accion="eliminarpedido" then
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion
			call Obj.Ejecutar("EliminarPedido",false,codigo_ped)
		Obj.CerrarConexion
	Set obj=nothing
	
	response.redirect "lstborrador.asp"
end if

if accion="aprobarpedido" then
	on error resume next

	modo="D"
	codigo_eped=3
	
	arrcodigo_dpe=split(request.form("chkcodigo_dpe"),",")
	arrcantidad_dpe=split(request.form("txtcantidad_dpe"),",")
		
	if Len(Request.form("cmdAprobar"))>0 then
		modo="A"
		codigo_eped=2
	end if
	
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexionTrans
					
			for i=0 to Ubound(arrcodigo_dpe)
				codigo_dpe=trim(arrcodigo_dpe(i))
				cantidad_dpe=trim(arrcantidad_dpe(i))
				if codigo_dpe<>"" then
					call Obj.Ejecutar("aprobarpedido",false,i,modo,codigo_eped,codigo_ped,codigo_dpe,cantidad_dpe,session("codigo_usu"),"")
				end if
			next
			
			if err.number>0 then
				Obj.CancelarConexionTrans
				mensaje="Ha ocurrido un Error al Enviar el Pedido"
			else
				Obj.CerrarConexionTrans
				mensaje="Se ha enviado correctamente el Pedido"				
			end if
	Set obj=nothing

	CerrarPopUp="<script>window.parent.location.reload()</script>"
end if

if accion="agregarnvocatalogo" then
	codigo_cco=request.querystring("codigo_cco")
	titulo_cat=Request.Form("titulo_cat")
	autor_cat=Request.Form("autor_cats")
	cboautor_cat=Request.Form("cboautor_cat")
	materia_cat=Request.Form("materia_cat")
	cbomateria_cat=Request.Form("cbomateria_cat")
	cbomateria_cat=null
	editorial_cat=Request.Form("editorial_cat")
	cboeditorial_cat=Request.Form("cboeditorial_cat")
	tipoMaterial_Cat=Request.Form("tipoMaterial_Cat")
	cbotipoMaterial_cat=Request.Form("cbotipoMaterial_cat")
	NumEdic=Request.Form("NumEdic")
	CaractEdicion_Cat=Request.Form("CaractEdicion_Cat")
	cboCaractEdic_cat=Request.Form("cboCaractEdic_cat")
	lugar_cat=Request.Form("lugar_cat")
	cbolugar_cat=Request.Form("cbolugar_cat")
	edicion_cat=Request.Form("edicion_cat")
	isbn_cat=Request.Form("isbn_cat")
	cbomoneda_cat=Request.Form("cbomoneda_cat")
	preciounit_cat=Request.Form("preciounit_cat")
	obs_cat=Request.Form("obs_cat")
	'response.write replace(Request.Form,"&","<br>")

	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexion	
		'mensaje=Obj.Ejecutar("PED_RegistrarLibro",true,cbotipoMaterial_cat,isbn_cat,cboautor_cat,titulo_cat,session("codigo_usu"),0)
		mensaje=Obj.Ejecutar("PED_RegistrarLibro",true,cbotipoMaterial_cat,isbn_cat,cboautor_cat,titulo_cat,cboeditorial_cat,cbolugar_cat,NumEdic,cboCaractEdic_cat ,edicion_cat,cbomateria_cat ,obs_cat,session("codigo_usu"),0)
		Obj.CerrarConexion
	Set Obj=nothing
	
	arrmensaje=split(mensaje,"|")

	pagina="frmdetallepedido.asp?accion=agregardetallepedido&tipoBD=C&codigo_cco=" & codigo_cco & "&textoBD=NUEVA BIBLIOGRAFIA&idLibro=" & arrmensaje(0) & "&moneda=" & cbomoneda_cat & "&precio=" & preciounit_cat
	
	if arrmensaje(0)="0" then
		response.write "<script>alert('" & arrmensaje(0) & "');history.back(-1)</script>"
	else
		response.write "<script>alert('Se ha registrado los datos de la bibliografía, proceda a registrar los datos del pedido.');location.href='" & pagina & "'</script>"

	end if
end if

if accion="aprobar" then
	'modo="D"
	'codigo_eped=6
	cantidad_dpe=request.form("txtcantidad_dpe")
	'if Len(Request.form("cmdAprobar"))>0 then
	'	modo="A"
		'codigo_eped=codigo_eped + 1
	'end if
	
	Set Obj= Server.CreateObject("Biblioteca.clsAccesoDatos")
		Obj.AbrirConexionTrans
		if codigo_Dpe<>"" then
			if request.QueryString("codigo_ins")=1 then
				call Obj.Ejecutar("aprobarpedido",false,0,modo,codigo_eped,codigo_ped,codigo_Dpe,cantidad_dpe,session("codigo_usu"),"")
			else
				call Obj.Ejecutar("aprobarpedido",false,1,modo,codigo_eped,codigo_ped,codigo_Dpe,cantidad_dpe,session("codigo_usu"),"")
			end if
		end if
			
		if err.number>0 then
			Obj.CancelarConexionTrans
			mensaje="Ha ocurrido un Error al Enviar el Pedido"
		else
			Obj.CerrarConexionTrans
			mensaje="Se ha enviado correctamente el Pedido"				
		end if
	Set obj=nothing
	response.redirect("detallepedido.asp?codigo_ped=" & codigo_ped)
end if
%>