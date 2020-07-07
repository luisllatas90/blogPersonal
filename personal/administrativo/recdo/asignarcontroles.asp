<%
'Declarar controles para procedencia
dim idprocedencia,tipoprocedencia,razon,direccion,telefono,email

idprocedencia=request.QueryString("idprocedencia")

sub controlesprocedencia()
	tipoprocedencia=request.Form("tipo")
	razon=request.Form("razon")
	direccion=request.Form("direccion")
	telefono=request.Form("telefono")
	email=request.Form("email")
end sub

'Declarar controles para destinatario
dim iddestinatario,nombre,tipodestinatario

iddestinatario=request.QueryString("iddestinatario")

sub controlesdestinatario()
	nombre=request.Form("nombre")
	tipodestinatario=request.Form("tipo")
end sub

'Declarar controles para documento archivado

Dim idarchivo,idarchivonuevo,fechaarchivo,horaarchivo,numeroexpediente,numerotipo,idtipoarchivo,asunto,obs,prioridad

idarchivo=request.QueryString("idarchivo")

sub controlesarchivo()
	fechaarchivo=request.form("dia") & "/" & request.form("mes") & "/" & session("nombreanio") 
	horaarchivo=request.form("hora") & ":" & request.form("min") & ":00 " & request.form("turno")
	numeroexpediente=request.Form("numeroexpediente")
	numerotipo=request.Form("numerotipo")
	idtipoarchivo=request.Form("idtipoarchivo")
	idprocedencia=request.Form("idprocedencia")
	iddestinatario=request.Form("iddestinatario")
	asunto=request.Form("asunto")
	obs=request.Form("obs")
	prioridad=request.Form("prioridad")
end sub

'Declarar controles de movimiento de archivo
Dim ip,idmovimiento,fechamovimiento,horamovimiento,idareaarchivo,idareaarchivo2,otrodestino
Dim numcargo,motivo,confirmacion

  Sub controlesmovimiento()
	ip=Request.ServerVariables("REMOTE_ADDR")
	idmovimiento=Request.querystring("idmovimiento")
	numeroexpediente=request.querystring("numeroexpediente")
	
	fechamovimiento=request.form("dia") & "/" & request.form("mes") & "/" & session("nombreanio") 
	horamovimiento=request.form("hora") & ":" & request.form("min") & ":00 " & request.form("turno")
	idareaarchivo=Request.form("idareaarchivo")
	idareaarchivo2=Request.form("idareaarchivo2")
	otrodestino=Request.form("otrodestino")
	numcargo=Request.form("numcargo")
	motivo=Request.form("motivo")
	confirmacion=Request.form("confirmacion")
  End Sub

%>