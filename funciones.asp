<%
'***************************************************************************************
'CV-USAT
'Archivo			: funciones.asp
'Autor				: Gerardo Chunga Chinguel
'Fecha de Creación	: 01/02/2005 11:09 p.m.
'Fecha Modificación	: 11/03/2006 06:55 p.m.
'Observaciones		: Funciones para programas ASP
'***************************************************************************************

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function


function ConvRomano(ByVal Num)
  Dim I,ArrRom
  ArrRom=Array("I","II","III","IV","V","VI","VII","VIII","IX","X","XI","XII","XIII","XIV")
  	if Num<>"0" then
  		Num=int(Num)
		Num=Num-1
		for I=0 to Ubound(ArrRom)
			if Num=I then
				Num=ArrRom(I)
				exit for
			end if
		next
		ConvRomano=Num
	else
		ConvRomano="-"
	end if
end function

function ConvDia(byVal digitos)
	select case digitos
		case "LU"
			ConvDia="Lunes"
		case "MA"
			ConvDia="Martes"	
		case "MI"
			ConvDia="Miércoles"
		case "JU"
			ConvDia="Jueves"
		case "VI"
			ConvDia="Viernes"
		case "SA"
			ConvDia="Sábado"
		case "DO"
			ConvDia="Domingo"			
	end select
end function

Public Function ConvertirTitulo(ByVal Cadena)
    Dim Auxiliar,Resultado,Caracter
    Dim Espacio
    Dim I
    Espacio = True
    Auxiliar = LCase(Cadena)
    For I = 1 To Len(Cadena)
        Caracter = Mid(Auxiliar, I, 1)
        If Espacio Then
            Caracter = UCase(Caracter)
        End If
        Resultado = Resultado & Caracter
        Espacio = (Caracter = " ")
    Next
    ConvertirTitulo = Resultado
End Function

function Enviarfin(Byval variablefin,ByVal rutaactual)
	if (variablefin="" or isnull(variablefin)=true) then
		response.write "<script>top.location.href='" & rutaactual & "tiempofinalizado.asp'</script>"
	end if
end function

function extraercaracter(ByVal inicio,ByVal numero,ByVal cadena)
	extraercaracter=mid(cadena,inicio,2)
end function

Function IIf(i,j,k)
    	if i Then IIf = j Else IIf = k
End function

Function SeleccionarItem(tipocontrol,opt,ValorActual)
Dim Tag
	Tag=IIF(tipocontrol="cbo","SELECTED","CHECKED")
      If trim(Opt)=trim(ValorActual) then
      	SeleccionarItem=Tag
      End If
End Function


function verificacomaAlfinal(cadena)

	if cadena<>"" then
		If Right(cadena, 1) = "," Then
	        verificacomaAlfinal=Mid(cadena, 1, Len(cadena) - 1)
	    Else
	        verificacomaAlfinal=cadena
	    End If
	end if
end function

function QuitarCaracterFinal(cadena,caracter)
	If Right(cadena, 1) = caracter Then
        QuitarCaracterFinal=Mid(cadena, 1, Len(cadena) - 1)
    Else
        QuitarCaracterFinal=cadena
    End If
end function

function QuitarDia(Byval cadena)
	Dim Pos,cadIZQ,cadDER
	
  	Pos = InStr(cadena, ",")
	  	If Pos > 0 Then
    	  cadIZQ = Left(cadena, Pos - 1)
      	  CadDER = Right(cadena, Len(cadena) - Pos + Len(caracter))
      		QuitarDia=CadDER
		Else
      		QuitarDia=cadena
		End If
end function

Public Sub buscaralumno(pag,ruta,modulo)
	session("rutaactual")=ruta
%>
<script language="Javascript">
	function enviarConsulta()
	{
		var cu=document.all.txtcodigouniver_alu
		if (cu.value==""){
			alert("Debe ingresar el código universitario del Estudiante")
			cu.focus()
			return(false)
		}
		else{
			location.href="<%=ruta%>clsbuscaralumno.asp?codigouniver_alu=" + cu.value + "&rutaactual=<%=ruta%>&pagina=<%=pag%>&mod=<%=modulo%>"
		}
	}
</script>
<table cellSpacing="0" cellPadding="5" width="100%" style="border-collapse: collapse" bordercolor="#111111" bgcolor="#FFF8DC" border="0" class="contornotabla">
      <tr>
        <td width="20%" height="17" align="right" class="etiqueta">&nbsp;Código Universitario</td>
        <td width="20%" height="17">
        <input class="cajas2" size="27" id="txtcodigouniver_alu" onkeyup="if(event.keyCode==13){enviarConsulta()}" name="txtcodigouniver_alu"></td>
        <td width="5%" height="17"><input type="button" class="buscar2" class="NoImprimir" onclick="enviarConsulta()" value="  Buscar..."></td>
      </tr>
    </table>
<%end sub

sub enviarimpresion()
	aURL=request.serverVariables("PATH_INFO")
	pDom=request.servervariables("HOST")
	pPath=pDom&aUrl
%>
<input onclick="imprimir('P','2')" type="button" value="    Imprimir" name="cmdImprimir" class="usatimprimir">
<%end sub

sub ImprimirRequest(tipo)
	dim control,valores(),i
	dim cnt,x,ArrRequest()
	dim nv()
	
	i=0
	if tipo="C" then
		for each control in Request.form
			redim preserve valores(i)
		   	valores(i)=Request.Form.Key(i+1)
			i=i+1
		next
	else
		for each control in Request.querystring
			redim preserve valores(i)
		   	valores(i)=Request.querystring.Key(i+1)
			i=i+1
		next
	end if
	cnt=0:x=0
	redim ArrRequest(i,1)

	do until cnt=i
		ArrRequest(cnt,x)=valores(cnt) 'relaciona nombre y valor
		x=x+1
		if tipo="C" then
			ArrRequest(cnt,x)=trim(Request.Form(valores(cnt)))
		else
			ArrRequest(cnt,x)=trim(Request.querystring(valores(cnt)))
		end if
		if(x=0)then
			x=1
		else
			x=0
		end if
	  cnt=cnt+1
	loop
	
	cnt=0:x=0
	DO until cnt=i
		redim preserve nv(cnt)
    	nv(cnt)= "<td align=center bgcolor=""#EBEBEB"">" & ArrRequest(cnt,x) & "</td>"
	    cnt=cnt+1
	loop
	cnt=0
	x=1
	DO until cnt=i
		nv(cnt)=nv(cnt) & "<td align=left bgcolor=""#EBEBEB"">" & ArrRequest(cnt,x) & "</td>"
		cnt=cnt+1
	loop
	cnt=0
	Response.Write("<table>")
	Response.Write("<tr><td colspan=4 align=center><h2>Controles del formulario</h2></td></tr>")
	Response.Write("<tr>")
	Response.Write("<td bgcolor=""#FEFFE1""><b>Nombre del Control</b></td>")
	Response.Write("<td bgcolor=""#FEFFE1""><b>Valor</b></td>")
	Response.Write("</tr>")  
  	do until cnt=i
		Response.Write("<tr>" & nv(cnt) & "<tr>")
		cnt=cnt+1
	loop
	Response.Write("</table>")  

end Sub

function BotonesAccion()
%>
<table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%" id="tblbotones" class="barraherramientas">
  	<tr><td>
	<input type="submit" value=" Guardar" class="guardar3" name="cmdGuardar">
	<input onClick="top.window.close()" type="button" value="   Cancelar" name="cmdCancelar" class="cerrar3">
	<span id="mensaje" style="color:#FF0000"></span>
	</td></tr>
</table>
<%end function

Public Sub CrearRpteTabla(Byval ArrEncabezados,Byval rsDatos,ByVal titulorpte,Byval ArrCampos,ByVal ArrCeldas,ByVal incluyeContador,Byval modoeditar,byVal pagina,byval IncluirBarras,byval arrCamposEnvio,ByVal OtroScript)
	Dim accionEvento,datosenvio

	'Imprimir control para resalta fila y almacenar ID de consulta
	'Tener en cuenta que si se declara la pagina se mostrará
	'en un iframe
	
	response.write "<input id=""txtelegido"" type=""hidden"" value=0>"
	
	if IsEmpty(Arrcampos)=false then
		if rsDatos.recordcount>0 then%>
			<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbllista" height="100%">
			  <%if titulorpte<>"" then%>	
		      <caption class="usattitulo"><%=titulorpte%></caption>
		      <%end if%>
			  <tr class="etabla">
			  	<%if incluyeContador="S" then%><td width="5%" height="5%">Nº</td><%end if
			  	for i=lbound(ArrEncabezados) to ubound(ArrEncabezados)%>
			    <td width="<%=ArrCeldas(i)%>" height="5%"><%=ArrEncabezados(i)%>&nbsp;</td>
			    <%next%>
			</tr>
			<%if IncluirBarras="S" then%>
			<tr><td colspan="<%=ubound(ArrEncabezados)+2%>" width="100%" valign="top">
			<div id="listadiv" style="height:100%" class="NoImprimir">
			<table border="0" cellpadding="3" cellspacing="0" width="100%">  
			<%
			end if
			  	x=0:datosenvio="":y=0
			  	Do while not rsDatos.EOF
			  		x=x+1:y=0:datosenvio=""
			  		if modoeditar="I" and pagina<>"" then
						'El detalle se mostrará en un iframe
						if IsEmpty(arrCamposEnvio)=false then
							for y=lbound(ArrCamposEnvio) to Ubound(ArrCamposEnvio)
								datosenvio=ArrCamposEnvio(y) & "=" & rsDatos(ArrCamposEnvio(y)) & "&" & datosenvio
							next
						end if
						datosenvio=pagina & "&" & QuitarCaracterFinal(datosenvio,"&")
				  		accionEvento="onClick=""ResaltarfilaDetalle(tbllista,fila" & x & ",'" & datosenvio & "');" & OtroScript & """"
					elseif modoeditar="V" and pagina<>"" then
				  		'El detalle se mostrará en Ventana PopUp
				  		accionEvento="onClick=""Resaltarfila(tbllista,'fila" & x & "');" & pagina & """ "
				  	end if
			  %>
			  <tr class="Sel" Typ="Sel" id="fila<%=x%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" <%=accionEvento%>>
			  	<%if incluyeContador="S" then%><td width="5%"><%=x%>&nbsp;</td><%end if
			  	for j=lbound(ArrEncabezados) to ubound(ArrEncabezados)
			    	response.write "<td width=""" & ArrCeldas(j) & """>" & rsDatos(ArrCampos(j)) & "</td>"
			    next%>
			  </tr>
			  	<%rsDatos.movenext
			  Loop
			  
			  if IncluirBarras="S" then%>
			  </table>
			  </div>
			  </td>
			  </tr>
			  <%end if%>
			</table>
		<%else
			response.write "<p class=usatsugerencia><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado registros en la Base de datos</p>"
		end if
	else
		response.write "<p class=usatsugerencia><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Especifique los campos a mostrar</p>"
	end if
end sub

sub crearlistabusqueda(ByVal rs,Byval nombrectrl,byval valorctrl,Byval campovalor,ByVal campotexto)%>
	<input type="text" ONKEYUP="AutocompletarCombo(this,document.all.<%=nombrectrl%>,'text',true)" name="listatexto" style='position:relative;width:90.3%;margin-right:15;margin-top:-1px;' class="cajas2"  value="<%=valorctrl%>" size="20">
	<select class="cajas2" name="<%=nombrectrl%>" style='display:inline;width:99%;position:absolute;Clip:rect(auto auto auto 96%);' onchange="elegirItem(document.all.listatexto,this)">
			<%
			If rs.recordcount>0 then
				Do while Not rs.EOF%>
					<option value="<%=rs(campovalor)%>" <%=SeleccionarItem("cbo",valorctrl,rs(campovalor))%>><%=rs(campotexto)%></option>
				<%rs.movenext
				Loop
			End if%>
		</select>
<%end sub

sub llenarlista(byVal nombrelista,byval evento,byVal rs,Byval campovalor,ByVal campotexto,Byval varseleccion,ByVal textolista,byval incluirtodos,byVal multiple)%>
	<%if trim(evento)<>"" then
		evento="onChange=""" & evento & """ "
	end if%>
  <select id="<%=nombrelista%>" name="<%=nombrelista%>" <%=multiple%> class="cajas2" <%=evento%>>
  <%if textolista<>"" then%>
  <option value="-2">>><%=textolista%><<</option>
  <%end if
  if incluirtodos="S" then%>
  <option value="-1" <%=SeleccionarItem("cbo",varseleccion,-1)%>>[ TODOS ]</option>
  <%end if
  if not(rs.BOF and rs.EOF) then
	  do while not rs.eof%>
  		<option value="<%=rs(campovalor)%>" <%=SeleccionarItem("cbo",varseleccion,rs(campovalor))%>><%=rs(campotexto)%></option>
	  	<%rs.movenext
	  loop
  end if%>
  </select>
<%end sub

sub planalumno(byval modo,byval evento,byval codigo_alu,byval codigo_cpf,byval varcodigo_pes,byval varseleccion,byval textolista)
	dim rsPlan,objPlan
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanEstudio")
    	if trim(modo)="EXUBIC" then
	      	Set rsPlan= objPlan.ConsultarPlanEstudio("RS","CO",1,"")
		else'if trim(modalidad)="NORMAL" then
			'Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",codigo_cpf,"")
		'else
			Set rsPlan= objPlan.ConsultarPlanSuficiencia("RS","SU",codigo_alu,"")
		end if
	Set objPlan=nothing
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
    %>   
  	<select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2" <%=evento%>>
  	<%if textolista="S" then%>
  	<option value="-2">--Seleccione el Plan de Estudio---</option>
  	<%end if
  	if not(rsPlan.BOF and rsPlan.EOF) then
	  	do while not rsPlan.eof%>
  			<option <%=iif(rsPlan("codigo_pes")=varcodigo_pes,"class=""usatCeldaTabDesactivo""","")%> value="<%=rsPlan("codigo_pes")%>" <%=SeleccionarItem("cbo",varseleccion,rsPlan("codigo_pes"))%>><%=rsPlan("descripcion_pes")%></option>
	  	<%rsPlan.movenext
		loop
	end if%>
  </select>
<%end sub

sub planalumnoescuela(byval evento,byval codigo_alu,byval varcodigo_pes,byval varseleccion,byval textolista,codigo_test)
	dim rsPlan,rsEscuela,objPlan,objEscuela,clase
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
	
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanEstudio")
  	Set objEscuela=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
	Set rsEscuela= objEscuela.ConsultarCarreraProfesional("RS","TE",codigo_test)

	%>   
  	<select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2" <%=evento%>>
  	<%
  	Do while Not rsEscuela.EOF
	    response.write "<optgroup label=""" & rsEscuela("nombre_cpf") & """>"
		Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",rsEscuela("codigo_cpf"),0)
		Do while not rsPlan.eof%>
  			<option label <%=iif(rsPlan("codigo_pes")=varcodigo_pes,"class=""usatCeldaTabDesactivo""",iif(rsPlan("defecto_pes")=true,"class=""rojo""",""))%> value="<%=rsPlan("codigo_pes")%>" <%=SeleccionarItem("cbo",varseleccion,rsPlan("codigo_pes"))%>>-<%=rsPlan("descripcion_pes")%></option>
	  		<%rsPlan.movenext
		loop
		response.write "</optgroup>"
		Set rsPlan=nothing
		rsEscuela.movenext
	Loop
	%>
	</select>
	<%
	Set objEscuela=nothing
	Set objPlan=nothing
	Set rsEscuela=nothing
end sub

sub plancomplementario(byval evento,byval codigo_alu,byval varcodigo_pes,byval varseleccion,byval textolista,ByVal codigo_cpf)
	dim rsPlan,rsEscuela,objPlan,objEscuela,clase
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
	
	Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		obj.AbrirConexion
			Set rsEscuela=obj.Consultar("ConsultarCarreraProfesional","FO","CC",codigo_cpf)
	%>   
  	<select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2" <%=evento%>>
  	<%
  	Do while Not rsEscuela.EOF
	    response.write "<optgroup label=""" & rsEscuela("nombre_cpf") & """>"
  		Set rsPlan=obj.Consultar("ConsultarPlanEstudio","FO","AC",rsEscuela("codigo_cpf"),0)
		Do while not rsPlan.eof
			if trim(varseleccion)=trim(rsPlan("codigo_pes")) or rsPlan("codigo_pes")=1 then%>
  			<option label value="<%=rsPlan("codigo_pes")%>" <%=SeleccionarItem("cbo",varseleccion,rsPlan("codigo_pes"))%>>-<%=rsPlan("descripcion_pes")%></option>
	  		<%
	  		end if
	  		rsPlan.movenext
		loop
		response.write "</optgroup>"
		Set rsPlan=nothing
		rsEscuela.movenext
	Loop
	%>
	</select>
	<%
		obj.CerrarConexion
		set rsEscuela=nothing
		Set rsPlan=nothing
	Set obj=nothing
end sub

sub escuelaprofesional(byval evento,byval varseleccion,byval textolista)
	dim rsPlan,rsEscuela,objPlan,objEscuela,clase
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
	
  	Set objEscuela=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
    Set rsEscuela= objEscuela.ConsultarCarreraProfesional("RS","MA",0)
	%>   
  	<select id="cbocodigo_cpf" name="cbocodigo_cpf" class="cajas2" <%=evento%>>
	<%if textolista<>"" then%><option value="-2"><%=TextoLista%></option><%end if%>
  	<%
  	Do while Not rsEscuela.EOF%>
  		<option value="<%=rsEscuela("codigo_cpf")%>" <%=seleccionarItem("cbo",varseleccion,rsEscuela("codigo_cpf"))%>><%=rsEscuela("nombre_cpf")%></option>
		<%
		rsEscuela.movenext
	Loop
	%>
	</select>
	<%
	Set objEscuela=nothing
	Set rsEscuela=nothing
end sub

sub ciclosAcademicos(byVal evento,byval varseleccion,byval IncluirTexto,Byval incluirtodos)
	dim texto,objciclo,rsCiclo
	if incluirTexto="S" then texto="Seleccione el ciclo académico"

	Set objCiclo=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
		Set rsCiclo= objCiclo.ConsultarCicloAcademico("RS","TO","")
    Set objCiclo=nothing
         	
    call llenarlista("cbocodigo_cac",evento,rsCiclo,"codigo_cac","descripcion_cac",varseleccion,texto,incluirtodos,"")
end sub

sub planescuela(byval evento,byval varseleccion,ByVal modo)
	dim rsPlan,rsEscuela,objPlan,objEscuela
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
	modo=iif(modo="N","MA","CO")
	
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanEstudio")
  	Set objEscuela=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
    Set rsEscuela= objEscuela.ConsultarCarreraProfesional("RS",modo,varseleccion)
	%>   
  	<select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2" <%=evento%>>
  	<option value="-2">--Seleccionar el Plan de Estudio--</option>
  	<%
  	Do while Not rsEscuela.EOF
	    response.write "<optgroup label=""" & rsEscuela("nombre_cpf") & """>"
		Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",rsEscuela("codigo_cpf"),0)
		Do while not rsPlan.eof%>
  			<option label value="<%=rsPlan("codigo_pes")%>" <%=SeleccionarItem("cbo",varseleccion,rsPlan("codigo_pes"))%>>-<%=rsPlan("descripcion_pes")%></option>
	  		<%rsPlan.movenext
		loop
		response.write "</optgroup>"
		Set rsPlan=nothing
		rsEscuela.movenext
	Loop
	%>
	</select>
	<%	
	Set objEscuela=nothing
	Set objPlan=nothing
	Set rsEscuela=nothing
end sub

sub planescuela2(byval evento,byval varseleccion,ByVal rsEscuela)
	dim rsPlan,objPlan
	
	if trim(evento)<>"" then  evento="onChange=""" & evento & """ "
	
	Set objPlan=Server.CreateObject("PryUSAT.clsDatPlanEstudio")
	%>   
  	<select id="cbocodigo_pes" name="cbocodigo_pes" class="cajas2" <%=evento%>>
  	<option value="-2">--Seleccionar el Plan de Estudio--</option>
  	<%
  	Do while Not rsEscuela.EOF
	    response.write "<optgroup label=""" & rsEscuela("nombre_cpf") & """>"
		Set rsPlan= objPlan.ConsultarPlanEstudio("RS","AC",rsEscuela("codigo_cpf"),0)
		Do while not rsPlan.eof%>
  			<option label value="<%=rsPlan("codigo_pes")%>" <%=SeleccionarItem("cbo",varseleccion,rsPlan("codigo_pes"))%>>-<%=rsPlan("descripcion_pes")%></option>
	  		<%rsPlan.movenext
		loop
		response.write "</optgroup>"
		Set rsPlan=nothing
		rsEscuela.movenext
	Loop
	%>
	</select>
	<%	
	Set objPlan=nothing
	Set rsEscuela=nothing
end sub

sub limpiarsesionalumno()
	session("codigo_alu")=""
	session("codigoUniver_alu")=""
	session("alumno")=""
	session("CicloActual")=""
	session("nombre_cpf")=""
	session("nombre_fac")=""
	session("Codigo_Pes")=""
	session("descripcion_pes")=""
	session("TipoPension")=""
	session("PrecioCredito")=""
	session("MonedaPrecioCredito")=""
	session("totalCredAprobados")=""
	session("codigo_cpf")=""
	session("urlpagina")=""
	session("cicloIng_alu")=""
	session("UltimaMatricula")=""
end sub

sub botonExportar(byVal ruta,byval tipoarchivo,byval archivo,byval incluyecontador,modobtn)
	dim pagina
	pagina=ruta & "clsexportar.asp?tipoarchivo=" & tipoarchivo & "&archivo=" & archivo & "&incluyecontador=" & incluyecontador
	if modobtn="B" then%><input style="display:none" class="<%=iif(tipoarchivo="doc","word","excel")%>" onClick="location.href='<%=pagina%>'" type="button" value="     Exportar..." id="exportar" ><%else%><img style="cursor:hand;display:none" src="<%=ruta%>images/<%=tipoarchivo%>.gif" onClick="location.href='<%=pagina%>'" value="     Exportar..." id="exportar"><%end if
end sub

sub ValoresExportacion(byval titulorpte,byval ArrEncabezados,ByVal rsDatos,byval Arrcampos,byval ArrCeldas)
	if Not(rsDatos.BOF AND rsDatos.EOF) then
		session("titulorpte")=titulorpte
		Set session("rsDatos")=rsDatos
		session("ArrEncabezados")=ArrEncabezados
		session("ArrCampos")=ArrCampos
		session("ArrCeldas")=ArrCeldas
		'Activar boton de exportación
		response.write "<script>document.all.exportar.style.display=''</script>"
	end if
end sub

sub LimpiarExportacion()
	session("titulorpte")=""
	session("rsDatos")=""
	session("ArrEncabezados")=""
	session("ArrCampos")=""
	session("ArrCeldas")=""
end sub

sub botonExportar2(byVal ruta,byval tipoarchivo,byval archivo,byval incluyecontador,modobtn)
	dim pagina
	pagina=ruta & "clsexportar2.asp?tipoarchivo=" & tipoarchivo & "&archivo=" & archivo & "&incluyecontador=" & incluyecontador
	if modobtn="B" then%><input style="display:none" class="<%=iif(tipoarchivo="doc","word","excel")%>" onClick="location.href='<%=pagina%>'" type="button" value="     Exportar..." id="exportar2" ><%else%><img style="cursor:hand;display:none" src="<%=ruta%>images/<%=tipoarchivo%>.gif" onClick="location.href='<%=pagina%>'" value="     Exportar..." id="exportar2"><%end if
end sub

sub ValoresExportacion2(byval titulorpte,byval ArrEncabezados,ByVal rsDatos,byval Arrcampos,byval ArrCeldas)
	if Not(rsDatos.BOF AND rsDatos.EOF) then
		session("titulorpte2")=titulorpte
		Set session("rsDatos2")=rsDatos
		session("ArrEncabezados2")=ArrEncabezados
		session("ArrCampos2")=ArrCampos
		session("ArrCeldas2")=ArrCeldas
		response.write "<script>parent.exportar2.style.display=''</script>"
	else
		response.write "<script>parent.exportar2.style.display=''</script>"
	end if
end sub

function celdaeditable(byval ncelda,byval valor,byval anchocaracter,byval validarnumero)
	if validarnumero="S" then validarnumero=" onkeypress=""validarnumero()"" "
	celdaeditable="<input type=""textbox"" " & validarnumero & " name=""" & ncelda & """ value=""" & valor & """ class=""cajas3"" size=""" & anchocaracter & """>"
end function

function Agrupar(ByVal varGrupo,varComp)
	If (IsNull(varGrupo)=false) then
		If trim(varComp)<>trim(varGrupo) then
			varComp=varGrupo
			Agrupar="class=""bordesup"""
		End if
	end if
end function


Function ReemplazarTildes(Str)
    Dim CurLtr
    Dim X
    For X = 1 To Len(Str)
        CurLtr = Mid(Str, X, 1)
        Select Case CurLtr
            Case "e", "é", "è", "ê", "ë", "E", "É", "È", "Ê", "Ë"
                ReemplazarTildes = ReemplazarTildes & "[eéèêëEÉÈÊË]"
            Case "a", "á", "à", "â", "ä", "A", "À", "Â", "Ä", "Á"
                ReemplazarTildes = ReemplazarTildes & "[aáàâäAÀÂÄÁ]"
            Case "i", "í", "ì", "ï", "î", "I", "Ì", "Ï", "Î", "Í"
                ReemplazarTildes = ReemplazarTildes & "[iíïîìIÏÎÌÍ]"
            Case "o", "ó", "ô", "ö", "ò", "O", "Ô", "Ö", "Ò", "Ó"
                ReemplazarTildes = ReemplazarTildes & "[oóôöòOÔÖÒÓ]"
            Case "u", "ú", "ù", "û", "ü", "U", "Ù", "Û", "Ü", "Ú"
                ReemplazarTildes = ReemplazarTildes & "[uúûüùUÛÜÙÚ]"
            Case "c", "ç", "C", "Ç"
                ReemplazarTildes = ReemplazarTildes & "[cCçÇ]"
            Case Else
                ReemplazarTildes = ReemplazarTildes & CurLtr
        End Select
    Next
End Function

sub EscribirMensaje(mensaje,icono,tipo)
%>
<html>

<head>
<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>
<title>ADVERTENCIA</title>
<script language="javascript" type="text/javascript">
	function Cerrar()
	{
	<%if tipo="P" then%>window.opener.location.reload()<%end if%>
	top.window.close()
	}
</script>
</head>
<body style="margin: 0; background-color: #EAEAEA">

<table width="100%" border="0" height="100%">
	<tr height="90%" valign="top">
		<td style="width: 5%" valign="top"><img src="<%=Request.ServerVariables("SERVER_NAME2")%>/campusvirtual/images/error.gif"></td>
		<td valign="top"><%=mensaje%></td>
	</tr>
	<tr height="10%">
		<td valign="top" colspan="2" align="center"><input name="cmdAceptar" type="button" value="Aceptar" onclick="Cerrar()">&nbsp;</td>
	</tr>
</table>
</body>
</html>
<%
end sub
%>