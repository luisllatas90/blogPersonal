<!--#include file="../../../../../funciones.asp"-->
<%
Class clsDatMatricula

	Public Function ConsultarMatricula(byVal tipo,byVal param1,ByVal param2,ByVal param3)
		Set Obj= Server.CreateObject("PryUSAT.clsDatMatricula")
			Set ConsultarMatricula=Obj.ConsultarMatricula("RS",tipo,param1,param2,param3)
		Set Obj=nothing
	End function
		
	Public Function HistorialAcademico(ByVal tipo,ByVal idAlu,ByVal idCac,ByVal idPes)
		Set Obj= Server.CreateObject("PryUSAT.clsDatNotas")
			Set HistorialAcademico=Obj.ConsultarNotas("RS",tipo,idAlu,IdCac,idPes)
		Set Obj=nothing
	End Function
	
	Public function Mensajevd(nummat)
		if nummat<>0 then
		   Mensajevd="<font color=""#FF0000"">" & nummat & "</font>"
		end if
	end function
	
	Public Function VerColorNota(ByVal TipoNota,ByVal Nota,ByVal Limite,ByVal AplicaDecimales)
		Dim Color
			Nota=cdbl(Nota)
			If TipoNota="I" then 'Ponderado por curso
				Color=IIF(Limite="D","#FF0000","#0000FF")
			Else'Ponderado por semestre
				Color=IIF(Nota<cdbl(Limite),"#FF0000","#0000FF")				
			End if
			Nota=IIF(AplicaDecimales=true,FormatNumber(Nota,2),Nota)
		VerColorNota="<font color=""" & Color & """>" & Nota & "</font>"
	End Function
	
	Public Sub PonderadoCAC(ByVal NotaCrd,ByVal TotalCrd,ByVal Limite)
		Dim PondCalc,Ponderado,Cadena
			if cdbl(NotaCrd)<>0 then
				PondCalc=cdbl(NotaCrd)/cdbl(TotalCrd)
				Ponderado=VerColorNota("T",cdbl(PondCalc),cdbl(Limite),true)
				'response.write Limite & "-" & Ponderado
			'Else
				'Ponderado="<font color=""#FF0000"">0,00</font>"
			end if
			response.write "<tr class=""etabla"">"
		    response.write "<td height=""14"" colspan=""5"" align=""right"">TOTAL </td>"
			response.write "<td height=""14"" align=""center"">" & TotalCrd & "</td>"
			response.write "<td height=""14"">&nbsp;</td>"
			response.write "<td height=""14"">&nbsp;</td>"
			response.write "<td height=""14"">" & Ponderado & "</td></tr>"
		End Sub
	
	Public Function VerCursoConvalidado(ByVal Curso,ByVal tipomatricula_dma,ByRef RetornaNombre)
		select case tipomatricula_dma
			case "C" 'Convalidado
				RetornaNombre=Curso & " <font color=""#008080"">(Convalidado)</font>"
				VerCursoConvalidado=True
			case "S" 'Suficiencia
				RetornaNombre="<font color=""#996633"">" & Curso & "</font>"
				VerCursoConvalidado=false			
			case "U" 'Exámen de Ubicación
				RetornaNombre="<font color=""#9900CC"">" & Curso & "</font>"
				VerCursoConvalidado=False
			case else
				RetornaNombre=Curso
				VerCursoConvalidado=false
		end select
	End Function
	
	Public function Modalidad(byVal tipo)
		Select case tipo		
			case "C"
				Modalidad="Convalidado"
			case "S"
				Modalidad="Suficiencia"			
			case "U"
				Modalidad="Ex. de Ubicación"
			case "A"
				Modalidad="<span class=azul>Agregado</span>"
			case else
				Modalidad="Normal"			
		end select
	end function

	Public function ContarCursos(byVal estado_dma,ByVal creditos_cur,byVal notacredito,ByRef CD,ByRef NC,ByRef TR)
		dim cursos,crd,ncrd,ret
		cursos=0	:	crd=0	:	ncrd=0	:	ret=0
		if (estado_dma<>"C" AND estado_dma<>"R") then
			cursos=1
			crd=creditos_cur
			ncrd=notacredito
		end if
		if estado_dma="R" then
			ret=1
		end if
		ContarCursos=cursos :	CD=crd	:	NC=ncrd	:	TR=ret
	end function

	Public Function CargarCursosHabiles(ByVal tipo,ByVal codigo_alu,byVal codigo_pes,byVal codigo_cac,byVal descripcion_cac,ByVal cicloIngreso,ByRef MatrCompl)
		Dim rsCursos,rsRequisitos
		Dim i,evento,controlimg
		Dim vacantesdisponibles
		Dim tipo_cur,ciclo_cur,creditos_cur
		Dim descripciontipo,posicion,estadocurso,nombre_cur
		Dim sumcreditos,cursounico,credMax,ccu,ciclobloqx
		Dim ExigirCompl
	
		'Inicializar y verificar variables locales
		i=0
		descripciontipo=iif(tipo="R","curricular","complementario")
		cursounico=""
		sumcreditos=0
		MatrCompl=0
		ExigirCompl=true
		
		Dim objMatricula
		
		Set ObjMatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
			ObjMatricula.AbrirConexion
				if tipo="C" then
					Set rsCursos=ObjMatricula.Consultar("ConsultarCursosHabilesComplementarios","FO",codigo_alu,codigo_pes,codigo_cac)
				else
					Set rsCursos=ObjMatricula.Consultar("ConsultarCursosHabilesMatricula","FO",codigo_alu,codigo_pes,codigo_cac)
				end if			
				Set rsRequisitos=ObjMatricula.Consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu,codigo_pes,codigo_cac)
			ObjMatricula.CerrarConexion
		Set ObjMatricula=nothing
		
		'Quitar ciclo académico de derecho y plan 36
		
		if (rsCursos.BOF And rsCursos.EOF) then
			response.write "<div id=""curso" & descripciontipo & """><p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han Programado Cursos para este ciclo académico " & descripcion_cac  & "</p></div>"
		Else
			credMax=IIf(IsNull(rsRequisitos("creditomaximomatricula"))=True,0,rsRequisitos("creditomaximomatricula"))
			totalcredAprob=IIf(IsNull(rsRequisitos("totalCredAprobados"))=True,0,rsRequisitos("totalCredAprobados"))
			
			session("credMax")=credMax
			session("totalcredAprob")=totalcredAprob
		%>
		<input type="hidden" name="txtcredMax" id="txtcredMax" value="<%=CredMax%>">
		<input type="hidden" name="txttotalcredAprob" id="txttotalcredAprob" value="<%=totalcredAprob%>">
		<table id="curso<%=descripciontipo%>" border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" width="100%" height="100%">
		<tr class="usatceldatitulo">
			<td width="3%" height="5%" align="center">&nbsp;</td>
			<td width="4%" height="5%" align="center">Nº</td>
			<td width="5%" height="5%" align="center">Tipo</td>
			<td width="10%" height="5%" align="center">Código</td>
			<td width="45%" height="5%" align="center">Nombre del Curso</td>
			<td width="5%" height="5%" align="center">Ciclo</td>
			<td width="5%" height="5%" align="center">Créd.</td>
			<td width="5%" height="5%" align="center">G.H.</td>
			<td width="5%" height="5%" align="center">Vac.</td>
			<td width="5%" height="5%" align="center">Vez</td>
			<td width="5%" height="5%" align="center">Agregar</td>
		</tr>
		<tr>
		<td width="100%" height="95%" colspan="11" valign="top" class="contornotabla">
		<div id="listadiv" style="height:100%" class="NoImprimir">
		<table id="tbl<%=descripciontipo%>" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="100%" bgcolor="#FFFFFF">
		  <%
		  Do while not rsCursos.EOF
		  	estadocurso=false
	  		i=i+1

			if trim(session("codigo_cpf"))="3" then
	  			ImprimeTablaHorario=HorarioCursoProgramado(rsCursos("codigo_cup"),Evento,ControlImg)
			end if
			
			
  			VacantesDisponibles=trim(rsCursos("vacantes_cup"))-trim(rsCursos("nromatriculados"))
  			if rsCursos("estado_cup")=0 then
  				VacantesDisponibles=0
  			end if
		
  			nombre_cur=VerificaVacantes(rsCursos("nombre_cur"),VacantesDisponibles,estadocurso)
  			tipo_cur=IIf(IsNull(rsCursos("tipo_cur"))=True,"CO",rsCursos("tipo_cur"))
  			ciclo_cur=IIf(IsNull(rsCursos("ciclo_cur"))=True,0,rsCursos("ciclo_cur"))
  			creditos_cur=IIf(IsNull(rsCursos("creditos_cur"))=True,0,rsCursos("creditos_cur"))
  			
  			'Verificar si se prematriculó en complementario para no volver a validar
  			MatrCompl=MatrCompl+ValidarCursoComplementario(rsCursos("EsCursoMatriculado"),tipo_cur,credMax)

  			'Agregar el texto (electivo) al nombre del curso
  			if rsCursos("electivo_cur")=true then
				electivo=1
				nombre_cur=nombre_cur & "<font color='#0000FF'>(Electivo)</font>"
			else
				electivo=0
			end if
			
  			'Bloquear cursos que superan el máximo de créditos y mostrar mensaje
  			bordeciclo=""
  			ccu=""
  			if trim(rsCursos("nombre_cur"))<>trim(cursounico) then
  				bordeciclo="class=""bordesup"""
  				cursounico=rsCursos("nombre_cur")
  			end if
  			
			'Validar caso de derecho del curso de matemática financiera para el plan 36
			
			select case cicloIngreso
				case "1999-I","1999-II","2000-I","2000-II","2001-I","2001-II","2002-I"
					if trim(rsCursos("codigo_cur"))=588 and codigo_pes=36 then
						cicloBloq=true
					else
						cicloBloq=false
					end if
				case else
					cicloBloq=false
			end select
			
			'response.write estadocurso
  		  %>
	  		<tr id="fila<%=rsCursos("codigo_cup")%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
	    	<td <%=bordeciclo%> width="3%" align="center" <%=Evento%>><%=ControlImg%>&nbsp;</td>
    		<td <%=bordeciclo%> width="3%" align="center"><%=i%>&nbsp;</td>
	    	<td <%=bordeciclo%> width="4%" align="center"><%=tipo_cur%>&nbsp;</td>
	    	<td <%=bordeciclo%> width="10%" align="center"><%=rsCursos("identificador_cur")%>&nbsp;</td>
		    <td <%=bordeciclo%> width="50%"><%=nombre_cur%>&nbsp;</td>
	    	<td <%=bordeciclo%> width="5%" align="center"><%=ConvRomano(ciclo_cur)%>&nbsp;</td>
		   	<td <%=bordeciclo%> width="5%" align="center"><%=creditos_cur%>&nbsp;</td>
    		<td <%=bordeciclo%> width="5%" align="center"><%=rsCursos("grupohor_cup")%>&nbsp;</td>
		    <td <%=bordeciclo%> width="5%" align="center"><%=VacantesDisponibles%>&nbsp;</td>
		    <td <%=bordeciclo%> width="5%" align="center"><%=mensajevd(rsCursos("vecesdesaprobada"))%>&nbsp;</td>
    		<td <%=bordeciclo%> width="5%" align="right">
    		<%if estadocurso=false and cicloBloq=false and rsCursos("EsCursoMatriculado")=0 then%>
	    	<input type="checkbox" onClick="Actualizar(this,'<%=CredMax%>','<%=tipo%>','<%=session("codigo_cpf")%>');pintafilamarcada(fila<%=rsCursos("codigo_cup")%>,this)" cp="<%=rsCursos("codigo_cup")%>" tc="<%=tipo_cur%>" vd="<%=rsCursos("vecesdesaprobada")%>" cc="<%=rsCursos("codigo_cur")%>" nc="<%=rsCursos("nombre_cur")%>" gh="<%=rsCursos("grupohor_cup")%>" ciclo="<%=ciclo_cur%>" electivo="<%=electivo%>" value="<%=creditos_cur%>" name="chkcursoshabiles" id="chk<%=rsCursos("codigo_cup")%>">
	    	<%end if%>
	    	</td>
		  </tr>
  		  <%response.write ImprimeTablaHorario
  			rsCursos.movenext
	  	  Loop  	 
	  	  %>
	  	</table>
		</div>
		</td>
		</tr>
	</table>	
		<%end if
		Set rsCursos=nothing
		Set rsRequisitos=nothing

	End function
	
	Private function ValidarCursoComplementario(ByVal esMatriculado,ByVal tipoCurso,ByVal MaxCreditaje)
		'Caso 01: Se matriculó primero en complementario y luego se agrega en otro curso curricular
		'response.write esMatriculado
		if int(esMatriculado)>0 AND (trim(tipoCurso)="CC" or trim(tipoCurso)="CO") then
	  		ValidarCursoComplementario=1
  		end if  		
	end function
	
	Public function VerificaVacantes(ByVal texto,ByVal vdisponibles,ByRef estado)
		vdisponibles=IIf(IsNull(vdisponibles)=True,0,vdisponibles)
		
		vdisponibles=cdbl(vdisponibles)
		if vdisponibles<=0 then
			VerificaVacantes=Texto & "<font color=""#FF0000"">(GRUPO CERRADO)</font>"
			estado=true
		else
			VerificaVacantes=Texto
			estado=false
		end if
	End function
	
	Public Function HorarioCursoProgramado(ByVal Codigo_Cup,ByRef EventoFila,ByRef ControlImg)
		Dim rsHorario,Cadena,Filas,inicio,fin
		Dim docente
		Dim totalHrs
		
		Set Obj= Server.CreateObject("PryUSAT.clsDatHorario")
			Set rsHorario=Obj.ConsultarHorarios("RS","4",Codigo_Cup,0,0)
		Set Obj=nothing
		
		EventoFila=""
		ControlImg=""
	
		If Not(rsHorario.BOF AND rsHorario.EOF) then
			EventoFila="onclick=""MostrarTabla(tblHorario" & Codigo_Cup & ",img" & Codigo_Cup & ",'../../../../images')"" "
			ControlImg="<img id=""img" & Codigo_Cup & """ border=""0"" src=""../../../../images/mas.gif"">"
			
			Cadena="<tr><td width=""100%"" colspan=""10"">"
			Cadena=Cadena & "<table id=""tblHorario" & Codigo_Cup & """ align=""center"" cellpadding=""2"" cellspacing=""0"" style=""display: none; border-collapse: collapse; border: 1px solid #808080"" bordercolor=""#111111"" width=""95%"">"
			Cadena=Cadena & "<tr class=""etabla"">"
			Cadena=Cadena & "<td width=""10%"">Día</td>"
			Cadena=Cadena & "<td width=""15%"">Inicio - Fin</td>"
			Cadena=Cadena & "<td width=""20%"">Ambiente</td>"
			Cadena=Cadena & "<td width=""35%"">Docente</td>"
			Cadena=Cadena & "<td width=""5%"">Tipo</td>"
			Cadena=Cadena & "<td width=""20%"">Observaciones</td>"
			Cadena=Cadena & "</tr>"
  			Do while Not rsHorario.EOF
  				inicio=Extraercaracter(1,2,rsHorario("nombre_hor"))
  				fin=Extraercaracter(1,2,rsHorario("horafin_Lho"))
				if IsNull(rsHorario("docente"))=false then
					docente=ConvertirTitulo(rsHorario("docente"))
				end if

  				if (IsNull(rsHorario("fechainicio_cup"))=false and IsNull(rsHorario("fechafin_cup"))=false) then
  					obs="Inicio: " & rsHorario("fechainicio_cup") & " Fin " & rsHorario("fechafin_cup")
  				end if
  				
				Filas=Filas & "<tr>"
				Filas=Filas & "<td width=""10%"">" & ConvDia(rsHorario("dia_Lho")) & ""
				Filas=Filas & "<input type=""hidden"" name=""txthorario" & Codigo_Cup & """ value=""" & rsHorario("dia_Lho") & inicio & fin & """>"
				Filas=Filas & "<input type=""hidden"" name=""txtambiente" & Codigo_Cup & """ value=""" & rsHorario("ambiente") & """>"
				Filas=Filas & "</td>"
				Filas=Filas & "<td width=""15%"">" & rsHorario("nombre_hor") & "-" & rsHorario("horafin_Lho") & "</td>"
				Filas=Filas & "<td width=""20%"">" & ConvertirTitulo(rsHorario("ambiente")) & "</td>"
				Filas=Filas & "<td width=""35%"">" & docente & "</td>"
				Filas=Filas & "<td width=""5%"">" & rsHorario("tipohoracur_lho") & "</td>"
				Filas=Filas & "<td width=""20%"">" & obs & "</td>"
				Filas=Filas & "</tr>"
				rsHorario.movenext
	  		Loop
	  		Cadena=Cadena & Filas
			Cadena=Cadena & "</table>"
			Cadena=Cadena & "</td></tr>"
			HorarioCursoProgramado=Cadena
		End If
		Set horario=nothing
		Set obj=nothing
	End Function
End Class
%>