<!--#include file="../../../funcionesaulavirtual.asp"-->
<%
Class clsRecepcion
	function ConsultarParametrosArchivo(ByVal tipo)
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			ConsultarParametrosArchivo=Obj.ConsultarParametrosArchivo(tipo)
		Set Obj=nothing	
	end function
	
	function ConsultarUltimoNumeroArchivo(ByVal idanio)
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			ConsultarUltimoNumeroArchivo=Obj.ConsultarUltimoNumeroArchivo(idanio)
		Set Obj=nothing	
	end function
	
	function verificanuevodocumento(ByVal fechareg)
		if fechareg=date then
			verificanuevodocumento="class=""rojo"""
		end if
	end function
	
	function ConsultarArchivosRegistrados(ByVal tipo,byVal param1,ByVal Param2,ByVal Param3)
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			ConsultarArchivosRegistrados=Obj.ConsultarArchivosRegistrados(tipo,Param1,Param2,Param3)
		Set Obj=nothing	
	end function
	
	function ConsultarProcedencia(ByVal tipo,ByVal idprocedencia)
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			ConsultarProcedencia=Obj.ConsultarProcedencia(tipo,idprocedencia)
		Set Obj=nothing
	end function
	
	function ConsultarDestinatario(ByVal tipo,ByVal iddestinatario)
		Set Obj= Server.CreateObject("aulavirtual.clsRecepcion")
			ConsultarDestinatario=Obj.ConsultarDestinatario(tipo,iddestinatario)
		Set Obj=nothing
	end function
	
	Sub mostrarpropiedades(Byval tipo,id)
		dim ArrPropiedad,nombrecbx,propiedad,estilo,agregaritem
		
		select case tipo
			case "P" 'Procedencia
				nombrecbx="idprocedencia"
				propiedad="onChange=""elegirItem(document.all.txtprocedencia,this)"""
				estilo=";display:inline;width:91%;position:absolute;Clip:rect(auto auto auto 96%)"
				agregaritem="<option value='0'>[Seleccione la procedencia del Documento]</value>"
				ArrPropiedad=ConsultarProcedencia("1","0")
			case "D" 'Destinatario
				nombrecbx="iddestinatario"
				propiedad="onChange=""elegirItem(document.all.txtdestinatario,this)"""
				estilo=";display:inline;width:91%;position:absolute;Clip:rect(auto auto auto 96%)"
				agregaritem="<option value='0'>[Seleccione el destino del Documento]</value>"			
				ArrPropiedad=ConsultarDestinatario("1","0")
			case "T" 'tipo de archivo
				nombrecbx="idtipoarchivo"
				ArrPropiedad=ConsultarParametrosArchivo("2")
				estilo="width: 90%"
			case "E" 'Estado del documento
				nombrecbx="idestadoarchivo"
				estilo="width: 90%"
				ArrPropiedad=ConsultarParametrosArchivo("4")
			case "A1" 'Aras de archivo
				nombrecbx="idareaarchivo"
				estilo="width: 90%"
				ArrPropiedad=ConsultarParametrosArchivo("3")
			case "A2" 'Aras de archivo
				nombrecbx="idareaarchivo2"
				estilo="width: 90%"
				ArrPropiedad=ConsultarParametrosArchivo("3")	
		end select%>
		<select name="<%=nombrecbx%>" style="<%=estilo%>" <%=propiedad%>>
			<%If IsEmpty(ArrPropiedad)=false then
				response.write agregaritem
				for i=lbound(ArrPropiedad,2) to Ubound(ArrPropiedad,2)%>
					<option value="<%=ArrPropiedad(0,I)%>" <%=seleccionar(id,ArrPropiedad(0,I))%>><%=ArrPropiedad(1,I)%></option>
				<%Next
			End if%>
		</select>
	<%end sub
	
	Sub mostrarlistasuntos()
		dim arrasuntos
		arrasuntos=ConsultarArchivosRegistrados("5",0,0,0)%>
		
		<select name="cbxasunto" style='display:inline;width:99%;position:absolute;Clip:rect(auto auto auto 96%);' onchange="elegirItem(document.all.asunto,this)">
			<%If IsEmpty(arrasuntos)=false then
				for i=lbound(arrasuntos,2) to Ubound(arrasuntos,2)%>
					<option value="<%=arrasuntos(0,I)%>"><%=arrasuntos(0,I)%></option>
				<%Next
			End if%>
		</select>
	<%end sub
End Class

%>