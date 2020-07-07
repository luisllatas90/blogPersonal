<!--#include file="../funciones.asp"--><%
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
	   Mensajevd=nummat
	else
	   Mensajevd="&nbsp;"
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
			case "U" 'ExÃ¡men de UbicaciÃ³n
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
		Dim sumcreditos,codigoUnico,credMax,BloquearCurso
		Dim ExigirCompl
	
		'Inicializar y verificar variables locales
		i=0
		descripciontipo=iif(tipo="R","curricular","complementario")
		cursounico=""
		sumcreditos=0
		MatrCompl=0
		ExigirCompl=true
		Par=false
		
		Dim objMatricula
		
		Set ObjMatricula= Server.CreateObject("PryUSAT.clsAccesoDatos")
			ObjMatricula.AbrirConexion
				if tipo="C" then
					Set rsCursos=ObjMatricula.Consultar("ConsultarCursosHabilesComplementarios","FO",codigo_alu,codigo_pes,codigo_cac)
				else
					Set rsCursos=ObjMatricula.Consultar("ConsultarCursosHabilesMatricula","FO",codigo_alu,codigo_pes,codigo_cac)
				end if			
				Set rsRequisitos=ObjMatricula.Consultar("ConsultarRequisitosMatricula_v2","FO",codigo_alu,codigo_pes,codigo_cac)
					
		'Quitar ciclo acadÃ©mico de derecho y plan 36
		session("credMax")=25 
		if (rsCursos.BOF And rsCursos.EOF) then
			response.write "<div id=""curso" & descripciontipo & """><p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han Programado Cursos para este ciclo acadÃ©mico " & descripcion_cac  & ", segÃºn su Plan de Estudios. Counsultar con la DirecciÃ³n de su Escuela Profesional</p></div>"
		Else
			credMax=IIf(IsNull(rsRequisitos("creditomaximomatricula"))=True,0,rsRequisitos("creditomaximomatricula"))
			totalcredAprob=IIf(IsNull(rsRequisitos("totalCredAprobados"))=True,0,rsRequisitos("totalCredAprobados"))
			
			session("credMax")=24 'credMax 'Crédito por defecto
			session("totalcredAprob")=totalcredAprob
		%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD>
<meta http-equiv="Content-Language" content="es" >
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
</HEAD>
<body>
<table id="curso<%=descripciontipo%>" border="0" cellpadding="0" cellspacing="0" style="BORDER-COLLAPSE: collapse" width="100%" height="100%">
	<tr class="usatceldatitulo">
		<td width="3%" height="5%" align="center">
		<input type="hidden" name="txtcredMax" id="txtcredMax" value="<%=CredMax%>" >
		<input type="hidden" name="txttotalcredAprob" id="txttotalcredAprob" value="<%=totalcredAprob%>" >
		
		</td>
		<td width="4%" height="5%" align="center">#</td>
		<td width="5%" height="5%" align="center">Tipo</td>
		<td width="10%" height="5%" align="center">Código</td>
		<td width="45%" height="5%" align="left">Descripción</td>
		<td width="5%" height="5%" align="center">Ciclo</td>
		<td width="5%" height="5%" align="center">Créd.</td>
	</tr>
	<tr>
		<td width="100%" height="95%" colspan="7" valign="top" class="contornotabla">
		<div id="listadiv" style="height: 100%" class="NoImprimir">
			<table id="tbl<%=descripciontipo%>" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" width="100%" bgcolor="#FFFFFF">
			<%
			Do while not rsCursos.EOF
		  		i=i+1
			  	electivo=0
			  	nombre_cur=rsCursos("nombre_cur")
			  	BloquearCurso=false
	
				'*******************************************************************************
				'Verificar vacantes disponibles para el GH
				'*******************************************************************************
				VacantesDisponibles=cdbl(rsCursos("vacantes_cup"))-cdbl(rsCursos("nromatriculados"))
	  			if rsCursos("estado_cup")=0 then
	  				VacantesDisponibles=0
	  			end if
			
				if VacantesDisponibles<=0 then
					estadocurso=true
					VacantesDisponibles="[GRUPO CERRADO]"
				else
					estadocurso=false
					VacantesDisponibles=VacantesDisponibles & " vacantes disponibles"
				end if
	  			
				'*******************************************************************************
	  			'Contar los cursos complementarios matriculados
				'*******************************************************************************
				if int(rsCursos("EsCursoMatriculado"))>0 then
					if ((trim(rsCursos("tipo_cur"))="CO" or trim(rsCursos("tipo_cur"))="CC") AND int(rsCursos("creditos_cur"))=0) then
	  					MatrCompl=MatrCompl+1
					end if
		  		end if

				'*******************************************************************************
	  			'Agregar el texto (electivo) al nombre del curso
				'*******************************************************************************	
	  			if rsCursos("electivo_cur")=true then
					electivo=1
					nombre_cur=nombre_cur & "<font color='#0000FF'>(Electivo)</font>"
				end if

				'*******************************************************************************
	  			'Bloquear Matemática Financiera para Derecho, Plan 99 hasta Ingresantes 2002-I
	  			'*******************************************************************************
	  			if int(codigo_pes)=36 and cdbl(rsCursos("codigo_cur"))=588 AND _
	  				(trim(cicloIngreso)="1999-I" OR _
	  				trim(cicloIngreso)="1999-II" OR _
	  				trim(cicloIngreso)="2000-I" OR _
	  				trim(cicloIngreso)="2000-II" OR _
	  				trim(cicloIngreso)="2001-I" OR _
	  				trim(cicloIngreso)="2001-II" OR _
	  				trim(cicloIngreso)="2002-I") then
					
					BloquearCurso=true
				end if
				
				'response.write "ciclo ingreso: " & cicloIngreso & " plan=" & codigo_pes & "-->" & BloquearCurso
				'*******************************************************************************
	  			'Bloquear Computación Aplicativa II para Ing. de sistemas y matemática, computación en todos los planes
	  			'*******************************************************************************
				if cdbl(rsCursos("codigo_cur"))=1020 and (int(session("codigo_cpf"))=3 or int(session("codigo_cpf"))=7) then
				    BloquearCurso=true
				end if
				
				'*******************************************************************************
	  			'Mostrar sólo cursos desbloqueados
	  			'*******************************************************************************
	  			
	  			if cdbl(rsCursos("codigo_cur"))<>cdbl(codigoUnico_cur) AND BloquearCurso=false then
	  				codigoUnico_cur=rsCursos("codigo_cur")
	  				j=j+1
	  				
					'if Par=True then
					'	Clase="Par"
					'else
					'	Clase="Impar"
					'end if
					'Par=Not Par
  			%>
  				
  				<tr style="cursor:hand" onclick="AbrirCurso('<%=codigoUnico_cur%>')" id="curso_padre<%=codigoUnico_cur%>" clase="<%=Clase%>">
				<td class="bordesup" width="3%" align="center"><img src="../images/mas.gif" alt="Abrir Grupos horarios" id="img<%=codigoUnico_cur%>"></td>
				<td class="bordesup" width="3%" align="center"><%=j%></td>
				<td class="bordesup" width="4%" align="center"><%=rsCursos("tipo_cur")%></td>
				<td class="bordesup" width="10%" align="center"><%=rsCursos("identificador_cur")%></td>
				<td class="bordesup" width="50%"><%=nombre_cur%>
				<%
				'*******************************************************************************
	  			'Mostrar sólo cursos desbloqueados y que no se haya matriculado
	  			'*******************************************************************************
				if BloquearCurso=false and rsCursos("EsCursoMatriculado")=0 then%>
				<input style="display:none" name="chkcursos" type="checkbox" id="chkcursoUnico<%=codigoUnico_cur%>" tc="<%=rsCursos("tipo_cur")%>" cc='<%=rsCursos("codigo_cur")%>' ciclo="<%=rsCursos("ciclo_cur")%>" electivo="<%=electivo%>" value="<%=rsCursos("creditos_cur")%>">
				<%end if%>
				</td>
				<td class="bordesup" width="5%" align="center"><%=ConvRomano(rsCursos("ciclo_cur"))%></td>
				<td class="bordesup" width="5%" align="center"><%=rsCursos("creditos_cur")%></td>
				</tr>
			<%
  				end if
  		  	%>
			<tr valign="top" style="display:none" id="codigo_cur<%=rsCursos("codigo_cur")%>">
				<td colspan="7" width="3%" align="right">
				<table style="border-collapse:collapse" width="100%" class="bordesup">
				<tr onmouseover="Resaltar(1,this,'S')" onmouseout="Resaltar(0,this,'S')">
				<td class="rojo" width="10%" align="center"><%=VacantesDisponibles%></td>
				<td width="10%" align="center">GRUPO <%=rsCursos("grupohor_cup")%></td>
				<td width="50%">
				<%
					i=0
					Set rsHorario=ObjMatricula.Consultar("ConsultarHorarios","FO","4",rsCursos("codigo_cup"),0,0)

					If Not(rsHorario.BOF AND rsHorario.EOF) then
						response.write "<table width='100%'>"
						Do while Not rsHorario.EOF
							i=i+1
							clase=""
			  				inicio=Extraercaracter(1,2,rsHorario("nombre_hor"))
			  				fin=Extraercaracter(1,2,rsHorario("horafin_Lho"))
							if IsNull(rsHorario("docente"))=false then
								docente=ConvertirTitulo(rsHorario("docente"))
							end if
							
							if i>1 then clase="class='lineahorario'"
		
		  					obs="Inicio: " & rsHorario("fechainicio_cup") & " Fin " & rsHorario("fechafin_cup")
			  				response.write "<tr><td " & clase & ">" & vbcrlf
							response.write("<input type=""hidden"" name=""txthorario" & rsCursos("codigo_cup") & """ value=""" & rsHorario("dia_Lho") & inicio & fin & """>")
							response.write("<input type=""hidden"" name=""txtambiente" & rsCursos("codigo_cup") & """ value=""" & rsHorario("ambiente") & """>")
							
							response.write(ConvDia(rsHorario("dia_Lho")) & " " & rsHorario("nombre_hor") & "-" & rsHorario("horafin_Lho") & "<br>")
							response.write(ConvertirTitulo(rsHorario("ambiente")) & "(Hrs. " & rsHorario("tipohoracur_lho") & ")") & vbcrlf
							response.write "</td><td " & clase & ">" & vbcrlf
							response.write(docente & "<br>" & obs) & vbcrlf
							response.write "</td></tr>"
							rsHorario.movenext
				  		Loop
				  		response.write "</table>"
					else
						response.write "<span class='cursoC'>[No se ha registrado horarios del grupo a elegir]</span>"
					End If
				Set horario=nothing
				%>				
				</td>
				<td width="5%" align="center">
				<%
				if int(rsCursos("vecesdesaprobada"))>0 then
					response.write rsCursos("vecesdesaprobada") & " vez."
				end if
				%>&nbsp;
				</td>
				<td width="5%" align="right">
				<%if estadocurso=false and BloquearCurso=false and rsCursos("EsCursoMatriculado")=0 then%>
				<input type="checkbox" onclick="Actualizar(this,'<%=CredMax%>','<%=tipo%>')" preciocurso='<%=rsCursos("preciocurso")%>' cp='<%=rsCursos("codigo_cup")%>' tc="<%=rsCursos("tipo_cur")%>" vd='<%=rsCursos("vecesdesaprobada")%>' cc='<%=rsCursos("codigo_cur")%>' nc='<%=rsCursos("nombre_cur")%>' gh='<%=rsCursos("grupohor_cup")%>' ciclo="<%=rsCursos("ciclo_cur")%>" electivo="<%=electivo%>" value="<%=rsCursos("creditos_cur")%>" name="chkcursoshabiles" id='chk<%=rsCursos("codigo_cup")%>' Posicion="<%=i%>">
				<%end if%>
				</td>
				</tr>
				</table>
			</tr>
				<%
  			rsCursos.movenext
	  	  Loop  	 
	  	  %>
			</table>
		</div>
		</td>
	</tr>
</table>
</body>
</html>
		<%end if
		ObjMatricula.CerrarConexion
		Set ObjMatricula=nothing
		
		Set rsCursos=nothing
		Set rsRequisitos=nothing

	End function
	
End Class
%>