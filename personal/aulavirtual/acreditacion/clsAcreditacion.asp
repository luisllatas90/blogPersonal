<!--#include file="../funcionesaulavirtual.asp"-->
<%
Class clsacreditacion

	function ConsultarEscuelas(ByVal tipo,ByVal param1,ByVal Param2,ByVal Param3)
		Set Obj= Server.CreateObject("AulaVirtual.clsAcreditacion")
			ConsultarEscuelas=Obj.ConsultarEscuelas(tipo,Param1,Param2,Param3)
		Set Obj=nothing	
	end function

	function ConsultarUsuario(ByVal tipo,ByVal Param1,ByVal Param2,ByVal Param3)
		Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
			ConsultarUsuario=Obj.ConsultarUsuario(tipo,Param1,Param2,Param3)
		Set Obj=nothing	
	end function

	function ConsultarModelo(ByVal tipo,ByVal param1,ByVal Param2,ByVal Param3)
		Set Obj= Server.CreateObject("AulaVirtual.clsAcreditacion")
			ConsultarModelo=Obj.ConsultarModeloAcreditacion(tipo,Param1,Param2,Param3)
		Set Obj=nothing
	end function
	
	function ConsultarEvaluacionModeloAcreditacion(ByVal tipo,ByVal param1,ByVal Param2,ByVal Param3)
		Set Obj= Server.CreateObject("AulaVirtual.clsAcreditacion")
			ConsultarEvaluacionModeloAcreditacion=Obj.ConsultarEvaluacionModeloAcreditacion(tipo,Param1,Param2,Param3)
		Set Obj=nothing
	end function
	
	Public Sub MostrarSeccionModelo(ByVal Idmodelo)
		Dim ArrSeccion
		ArrSeccion=ConsultarModelo("2",Idmodelo,0,0)
			
		If IsEmpty(ArrSeccion)=false then%>
		<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" width="98%" id="tblmodelo<%=idmodelo%>">
			<%for j=lbound(ArrSeccion,2) to Ubound(ArrSeccion,2)%>
			<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="MostrarTabla(tblseccion<%=ArrSeccion(0,j)%>,'../images/',imgseccion<%=ArrSeccion(0,j)%>)">
				<td width="2%" align="right"><img border="0" src="../../../images/beforenode.GIF"></td>
				<td width="2%" align="left">
                <img id="imgseccion<%=ArrSeccion(0,j)%>" border="0" src="file:///C:/Inetpub/wwwroot/dpdu/images/mas.gif"></td>
				<td width="96%" class="seccion"><i><b>Sección <%=ArrSeccion(2,j)%>:</b></i>&nbsp;<%=ArrSeccion(1,j)%>&nbsp;</td>
			</tr>
				<%MostrarVariableSeccion ArrSeccion(0,j)
			next%>
		</table>
		<%end if
	end sub
	

	Private Sub MostrarVariableSeccion(ByVal IdSeccion)
		Dim ArrVariable
		
		ArrVariable=ConsultarModelo("3",IdSeccion,0,0)
				
		If IsEmpty(ArrVariable)=false then%>
<tr>
	<td colspan="3">
		<table align="right" border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="98%" id="tblseccion<%=idseccion%>">
			<%for j=lbound(ArrVariable,2) to Ubound(ArrVariable,2)%>
			<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="MostrarTabla(tblvariable<%=ArrVariable(0,j)%>,'../images/',imgVariable<%=ArrVariable(0,j)%>)">
				<td width="1%" background="../../../images/beforenode.GIF">&nbsp;</td>
				<td width="2%" align="left">
                <img id="imgVariable<%=ArrVariable(0,j)%>" border="0" src="file:///C:/Inetpub/wwwroot/dpdu/images/mas.gif">&nbsp;</td>
				<td width="90%" class="variable"><i><b>Variable <%=ArrVariable(2,j)%>:</b></i>&nbsp;<%=ArrVariable(1,j)%>&nbsp;</td>
			</tr>
			<%MostrarAtributoVariable ArrVariable(0,j),idseccion
				next%>
		</table>
	</td>
</tr>
<%end if
	end sub
	
	Private Sub MostrarAtributoVariable(ByVal idVariable,ByVal idseccion)
		Dim ArrAtributo
		
		ArrAtributo=ConsultarModelo("4",idVariable,0,0)
		
		If IsEmpty(ArrAtributo)=false then
		%>
<tr>
	<td colspan="3">
		<table align="right" border="0" cellpadding="1" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="97%" id="tblvariable<%=idVariable%>">
			<%for j=lbound(ArrAtributo,2) to Ubound(ArrAtributo,2)%>
			<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" valign=top onClick="AbrirPopUp('tarea.asp?idindicador=<%=ArrAtributo(0,j)%>&nombreindicador=<%=ArrAtributo(2,j)%>&idvariable=<%=idvariable%>&idseccion=<%=idseccion%>','450','700','yes','yes','yes')">
				<td width="1%" background="../../../images/beforechild.GIF">&nbsp;</td>
				<td width="1%" align="left">-</td>
				<td width="87%" class="indicador"><i><b>Indicador <%=ArrAtributo(3,j)%>:</b></i>&nbsp;<%=ArrAtributo(2,j)%>&nbsp;</td>
				<td align="right" class="<%=iif(ArrAtributo(4,j)="0","rojo","azul")%>"><%=ArrAtributo(4,j)%>&nbsp; tarea(s)</td>
				<td width="2%" align="right"><img border="0" src="../../../images/nota.GIF"></td>
			</tr>
			<%next%>
		</table>
	</td>
</tr>
<%end if
	end sub
	
	Private Sub MostrarTareasAsignadasIndicador(ByVal idAtributo)
		Dim ArrTareas
		ArrTareas=ConsultarEvaluacionModeloAcreditacion("5",idAtributo,0,0)
		
		If IsEmpty(ArrTareas) then%>
			<tr>
				<td colspan="4">
					<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; color: #000080;display:none" width="95%" id="tblatributo<%=idatributo%>">
						<tr><td>No se han registrado tareas para evaluar el Indicador</td></tr>
					</table>
				</td>
			</tr>
		<%else%>
		<tr>
			<td colspan="4">
				<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; color: #000080;display:none" width="98%" id="tblatributo<%=idatributo%>">
					<%for j=lbound(ArrTareas,2) to Ubound(ArrTareas,2)%>
					<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" onClick="MostrarTabla(tbltarea<%=ArrTareas(0,j)%>,'../images/',imgtarea<%=ArrTareas(0,j)%>)">
						<td width="2%" align="right"><img border="0" src="../../../images/beforenode.GIF"></td>
						<td width="3%" align="left">
                        <img id="imgtarea<%=ArrTareas(0,j)%>" border="0" src="file:///C:/Inetpub/wwwroot/dpdu/images/mas.gif"></td>
						<td width="60%"><b><i>Tarea <%=j+1%></b></i>: <%=ArrTareas(1,j)%>&nbsp;</td>
						<td><i></i>Desde: <%=ArrTareas(2,j)%>&nbsp;- Hasta&nbsp; <%=ArrTareas(3,j)%></i>&nbsp;</td>
					</tr>
						<%'Call MostrarDocsUltimos(ArrAtributo(0,j))
					next%>
				</table>
			</td>
		</tr>
		<%end if
	end sub
	
	Public Sub MostrarAtributosEvaluados(ByVal idVariable)
		Dim ArrAtributo
		ArrAtributo=ConsultarEvaluacionModeloAcreditacion("3",idVariable,0,0)
		
		If IsEmpty(ArrAtributo)=false then%>
		<tr>
			<td colspan="3">
				<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; color: #000080" width="98%" id="tblvariable<%=idVariable%>">
					<%for j=lbound(ArrAtributo,2) to Ubound(ArrAtributo,2)%>
					<tr onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')" <%if ArrAtributo(5,j)>0 then%>onClick="MostrarTabla(tblatributo<%=ArrAtributo(0,j)%>,'../images/',imgatributo<%=ArrAtributo(0,j)%>)" <%end if%>>
						<td width="2%" align="right"><img border="0" src="../../../images/beforenode.GIF"></td>
						<td width="2%"><%if ArrAtributo(5,j)>0 then%><img id="imgatributo<%=ArrAtributo(0,j)%>" border="0" src="file:///C:/Inetpub/wwwroot/dpdu/images/mas.gif"><%end if%>&nbsp;</td>
						<td width="90%" class="indicador" align="left"><b><i>Indicador <%=j+1%></b></i>: <%=ArrAtributo(1,j)%>&nbsp;</td>
						<td width="2%" align="right"><%=ImagenAlerta(ArrAtributo(3,j))%></td>
					</tr>
						<%Call MostrarDocsUltimos(ArrAtributo(0,j))
					next%>
				</table>
			</td>
		</tr>
		<%end if
	end sub
	Private Sub MostrarDocsUltimos(ByVal idAtributo)
		Dim ArrDcto
		ArrDcto=ConsultarEvaluacionModeloAcreditacion("4",idAtributo,0,0)
		
		If IsEmpty(ArrDcto)=false then%>
		<tr>
			<td colspan="4">
				<table align="right" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; color: #000080;display:none" width="97%" id="tblatributo<%=idatributo%>">
					<%for j=lbound(ArrDcto,2) to Ubound(ArrDcto,2)%>
					<tr>
						<td width="2%"><img border="0" src="../../../images/beforenode.GIF"></td>
						<td width="95%"><%=j+1%> - <%=RutaArchivo(ArrDcto(3,j),ArrDcto(2,j),ArrDcto(5,j),ArrDcto(1,j),ArrDcto(6,j))%> (avance: <%=ArrDcto(4,j)%>%)</td>
					</tr>
					<%next%>
				</table>
			</td>
		</tr>
		<%end if
	end sub

	Public function ImagenAlerta(byval PjeObt)
		Dim Rojo, Amarillo, Verde
		Dim ValMen, ValInterm, ValSup
    
	    Rojo = "<img border=""0"" ALT=""" & PjeObt & "%"" src=""images/R2.gif"">"
    	Amarillo = "<img border=""0"" ALT=""" & PjeObt & "%"" src=""images/A2.gif"">"
	    Verde = "<img border=""0"" ALT=""" & PjeObt & "%"" src=""images/V2.gif"">"
    
    	ValMen = (100 / 2) - 1
	    ValInterm = (100) - 1
    	ValSup = 100
    	PjeObt=int(PjeObt)
    
	    If PjeObt < ValMen Then
    	    ImagenAlerta= Rojo
	    ElseIf PjeObt <= ValInterm Then
    	        ImagenAlerta= Amarillo
        	ElseIf PjeObt >= ValSup Then
            	ImagenAlerta= Verde
	    End If
	End Function
	
	Public function ImagenAlertaSeccion(ByVal modo,Byval PjeObt)
		Dim Rojo, Amarillo, Verde
		Dim ValMen, ValInterm, ValSup
    
    	if modo="1" then
		    Rojo = "<img border=""0"" src=""images/R3.gif"">"
    		Amarillo = "<img border=""0"" src=""images/A3.gif"">"
	    	Verde = "<img border=""0"" src=""images/V3.gif"">"
    
	    	ValMen = (100 / 2) - 1
		    ValInterm = (100) - 1
    		ValSup = 100
	    	PjeObt=int(PjeObt)
    
		    If PjeObt < ValMen Then
    		    ImagenAlertaSeccion= Rojo
		    ElseIf PjeObt <= ValInterm Then
    		        ImagenAlertaSeccion= Amarillo
        		ElseIf PjeObt >= ValSup Then
            		ImagenAlertaSeccion= Verde
		    End If
		else
			ImagenAlertaSeccion="<img border=""0"" src=""images/N3.gif"">"
		end if
	End Function
	
	Public function fondoseccion(ByVal modo,Byval PjeObt)
		Dim ValMen, ValInterm, ValSup
    
    	if modo="1" then   
	    	ValMen = (100 / 2) - 1
		    ValInterm = (100) - 1
    		ValSup = 100
	    	PjeObt=int(PjeObt)
    
		    If PjeObt < ValMen Then
    		    fondoseccion= "class=""fondorojo"""
		    ElseIf PjeObt <= ValInterm Then
    		        fondoseccion= "class=""fondoamarillo"""
        		ElseIf PjeObt >= ValSup Then
            		fondoseccion= "class=""fondoverde"""
		    End If
		else
			fondoseccion="class=""fondonulo"""
		end if
	End Function

	
	Public function graficoavanceacreditacion(ByVal modo,ByVal PjeObt)
	
		Dim ValMen, ValInterm, ValSup,fondo,texto
    
    	if modo="0" then
    		fondo="class='fondonulo'":texto="&nbsp;No se ha empezado"
    	else
    	    ValMen = (100 / 2) - 1
		    ValInterm = (100) - 1
    		ValSup = 100
	    	PjeObt=int(PjeObt)
	    	
	    	If PjeObt < ValMen Then
    		    fondo="class='fondorojo'":texto="menos del 50%"
		    ElseIf PjeObt <= ValInterm Then
    		        fondo="class='fondoamarillo'":texto="más del 50%"
        		ElseIf PjeObt >= ValSup Then
            		fondo="class='fondoverde'":texto="Listo"
		    End If
	    end if
		graficoavanceacreditacion="<div style='width=60%' " & fondo & ">" & texto & "</div>"
	end function
	
	Private function RutaArchivo(ByVal tArchivo,ByVal nArchivo,ByVal usuario,ByVal fecha,byVal nombretarea)
		Dim rutaactual,icono,comentario
		
		'---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------

		rutaactual="https://intranet.usat.edu.pe/dpdu/acreditacion/archivos/" & nArchivo
		icono=right(nArchivo,3) & ".gif"
		icono="<img border=""0"" src=""images/" & icono & """>&nbsp;"
		comentario=nombretarea & " , realizado por " & usuario & " - Fecha de Registro " & fecha
		
		RutaArchivo="<a title='" & comentario & "' href='" & rutaactual & "' TARGET='_blank'>" & icono & tArchivo & "</a>"
		
	end function
		
	'---------------------------------------------------------
	'Fecha de creación: 14/07/2005
	'Grabar procesos en TABlAS DE BASE DE DATOS
	'---------------------------------------------------------
	
	Public Sub Agregartareaevaluacion(idindicador,titulo,idacred,fechai,fechaf,tipoa,admin)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
				Obj.Agregartareaevaluacionindicador idindicador,titulo,idacred,fechai,fechaf,tipoa,admin
		set Obj=nothing
	End Sub
	
	Public Sub Modificartareaevaluacion(id,titulo,fechai,fechaf,tipoa,admin)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
			Obj.modificartareaevaluacionindicador id,titulo,fechai,fechaf,tipoa,admin
		set Obj=nothing
	End Sub
	
	Public Sub Eliminartareaevaluacion(id,idevalind)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
			Obj.eliminartareaevaluacionindicador id,idevalind
		set Obj=nothing
	end Sub
	
	Public Sub Agregarresponsabletarea(idusu,idtarea,tipo,destino,mensaje,fecha,carrera,norigen,ndestino)
		set Obj=server.CreateObject("AulaVirtual.clsacreditacion")
			Obj.agregarresponsabletareaevaluacion idusu,idtarea,tipo,destino,mensaje,fecha,carrera,norigen,ndestino
		set Obj=nothing
	End Sub
	
	Public Sub Modificarresponsabletarea(id,idusu,tipo)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
			Obj.modificarresponsabletareaevaluacion id,idusu,tipo
		set Obj=nothing
	End Sub
	
	Public Sub Eliminarresponsabletarea(id)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
			Obj.eliminarresponsabletareaevaluacion id
		set Obj=nothing
	end Sub
	
	Public Sub Eliminaravancetarea(id,idtareaeval,idevalind,idacred,idvar,idsecc,nombrearch)
		set Obj=server.CreateObject ("AulaVirtual.clsacreditacion")
			Obj.eliminaravancetareaevaluacion id,idtareaeval,idevalind,idacred,idvar,idsecc
		set Obj=nothing
		Call BorrarArchivoReg("archivos/" & nombrearch)
	end Sub
End Class
%>