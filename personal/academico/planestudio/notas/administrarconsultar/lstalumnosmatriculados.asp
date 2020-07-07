<%
On Error resume next
codigo_cac=request.querystring("codigo_cac")
codigo_cup=request.querystring("codigo_cup")
nivel=request.querystring("nivel")
if (session("codigo_usu")="") then
Response.Write ("<h1>ACCESO DENEGADO</h1>")
 %> 
	<script type="text/jscript" language="javascript">
	    alert('Lo sentimos, Ud. no tiene acceso al curso')
	    top.window.close()
	   // top.location.href = "../../../index.asp"
	</script>
 <% 
 
  else

set obj=Server.createObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		Set rsValida=Obj.Consultar("ValidarCargaProfesor","FO", session("codigo_usu"),codigo_cup)		
	Obj.CerrarConexion
Set obj=nothing

if (rsValida.BOF and rsValida.EOF)  then
Response.Write ("<h1>ACCESO DENEGADO</h1>")
 %> 
	<script>
		alert('Lo sentimos, Ud. no tiene acceso al curso')
		top.window.close()
		//top.location.href="../../../index.asp"
	</script>
 <%   

else
'********************************************************
'Cambio: Solo para cursos de recuperación
'********************************************************
Dim sw 
sw = 0
set objRec=Server.createObject("PryUSAT.clsAccesoDatos")
	objRec.AbrirConexion
		Set rsRec=objRec.Consultar("ACAD_RetornaComplementario","FO", codigo_cup)		
		'Si no retorna registros NO es examen de recuperacion
		if not (rsRec.BOF and rsRec.EOF) then				    		    
		    Set rsFec = objRec.Consultar("ACAD_VerificaPermisosExamen","FO", codigo_cac)
		    'Si aun esta en el cronograma
		   ' if Not(rsFec.BOF and rsFec.EOF) then
		   if rsFec.recordcount =0 then
		        sw = 1
		    else
                sw = 0		        
		    end if		
		end if		
	objRec.CerrarConexion
Set objRec=nothing

    set obj=Server.createObject("PryUSAT.clsAccesoDatos")
        obj.AbrirConexion
	        Set rsAlumnos=Obj.Consultar("ConsultarAlumnosMatriculados","FO",5,codigo_cup,codigo_cac,0)
	        Set rsRegistro=Obj.Consultar("NOT_ConsultarRegistroNotas","FO",1,codigo_cup,session("codigo_usu"))
        Obj.CerrarConexion
    Set obj=nothing
    
    if Not(rsRegistro.BOF and rsRegistro.EOF) then
	    descripcion_cac=rsRegistro("descripcion_cac")
	    tipo_Cac=rsRegistro("tipo_cac")
	    notaminima_cac=rsRegistro("notaminima_Cac")
	    estadoControl=rsRegistro("estadoControl")
	    codigo_aut=rsRegistro("codigo_aut")
	    mensajeprofesor=rsRegistro("mensajeprofesor")
		
	    nombre_cur=rsRegistro("nombre_cur") & "(Grupo " & rsRegistro("grupohor_cup") & ")"
	    nombre_per=rsRegistro("nombre_per")
	    codigo_per=rsRegistro("codigo_per")
    end if
    Set rsRegistro=nothing
  
%>
    <html>
    <head>
    <meta http-equiv="Content-Language" content="es" />    
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
    <title>Registro de notas</title>
    <link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
    <script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/validarnotas.js"></script>
    <style type="text/css">
    input        { font-family: Verdana; font-size: 8.5pt;width:100% }
    .letrapequena {
	    font-size: xx-small;
    }
    .mensajeAviso {
	    font-size: 14px;
	    color:red
    }
    .Aprobado {
	    color: #0000FF;
    }
    .Desaprobado{
	    color: #FF0000;
    }
    .Retirado {
	    color: #008080;
    }
    </style>
    </head>
    <body bgcolor="#EEEEEE">
    <form name="frmRegistro" method="post" action="procesar.asp?accion=agregarnotas&codigo_cac=<%=codigo_cac%>&codigo_per=<%=codigo_per%>&nivel=<%=nivel%>&codigo_cup=<%=codigo_cup%>">
    <input name="hdnotaminima_cac" type="hidden" value="<%=notaminima_cac%>" />
    <input name="hdtotal" type="hidden" value="<%=rsalumnos.recordcount%>" />
    <% response.Write "ESTADO: "
        response.Write sw
    %>
    <table cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%">
  	    <tr><td width="60%">
	    <%if (isnull(mensajeprofesor)=true or mensajeprofesor="") then%>	
	    <input onclick="EnviarNotaNueva()" type="button" value=" Guardar" class="usatGuardar" name="cmdGuardar" <%=estadoControl%> tooltip="<b>Guardar</b><br>Permite grabar el registro de notas en el sistema<br> Esta acción se debe de realizarse una sola vez,<br>caso contrario solicitar <b>Autorización</b> a Dirección Académica, para el cambio de notas" />
	    <%end if%>
	    <input onclick="history.back(-1)" type="button" value="    Regresar" name="cmdCancelar" class="usatSalir"> </td>
        <td width="40%" align="right" id="mensaje" class="azul">
        <input type="checkbox" name="chkretirados" id="chkretirados" value="none" style="width: 5%" onclick="OcultarRetirados()" /> 
        <span id="textoOcultar">Ocultar estudiantes retirados</span></td>
      </tr>
    </table>
    <br>
    <table border="0" cellpadding="4" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="92%">
      <tr>
        <td width="12%"  height="4%" class="etiqueta">Docente</td>
        <td width="88%"  height="4%" id="nombre_per">:&nbsp;<%=nombre_per%></td>
      </tr>
      <tr>
        <td width="12%" height="4%" class="etiqueta">Asignatura</td>
        <td width="88%" height="4%" id="nombre_cur">:&nbsp;<%=nombre_cur%></td>
      </tr>
      <tr><td class="mensajeaviso" colspan="2"><%=mensajeprofesor%></td></tr>  
      <tr>
        <td width="100%" colspan="2" valign="top" align= "center" height="90%">   
		    <table height="100%" width="99%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;" class="contornotabla">
			    <tr class="etabla">
			    <td height="5%" width="5%">Nº</td>
			    <td height="5%" width="15%">Escuela Profesional</td>
			    <td height="5%" width="10%">Código</td>
			    <td width="43%">Apellidos y Nombres</td>
			    <td height="5%" width="8%">Nota final</td>
			    <td height="5%" width="10%">Condición</td>
			    <td height="5%" width="10%">Acción</td>
			    </tr>
			    <tr>
			    <td colspan="7" valign="top" width="5%" height="90%">
			    <input type="hidden" value="<%=codigo_aut%>" name="txtcodigo_aut" />
			    <div id="listadiv" style="height:100%">
			    <table width="100%" border="0" cellpadding="2" cellspacing="0" id="tlbRegistro">
			    <%
			    aprobados=0:desaprobados=0:retirados=0
			    codigoAlu_Actual=""
			    estadodma_Actual=""
    			
			    Do while not rsAlumnos.eof
				    'if codigoalu_Actual<>rsAlumnos("codigo_alu") then
					    i=i+1
					    codigoAlu_Actual=rsAlumnos("codigo_alu")
					    if rsAlumnos("estado_dma")="R" then 
						    retirados=retirados+1
					    elseif cdbl(rsAlumnos("notafinal_dma"))>cdbl(notaminima_cac) then
							    aprobados=aprobados+1
					    else
							    desaprobados=desaprobados+1
					    end if
    		
			    %>
				    <tr onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" class="letrapequena">
				    <td width="5%" align="center">
				    <%=i%>
				    <input name="hdcodigo_dma<%=i%>" type="hidden" value="<%=rsAlumnos("codigo_dma")%>" />
				    </td>
				    <td width="15%"><%=rsAlumnos("nombre_cpf")%>&nbsp;</td>
				    <td align="right" width="12%"><%=rsAlumnos("codigoUniver_Alu")%></td>
				    <td align="left" width="45%"><%=rsAlumnos("alumno")%>&nbsp;</td>
				    <td align="center"  width="8%">
				    <%if rsAlumnos("estado_dma")="M" and sw = 0 then%>
					    <input name="txtnotafinal_dma<%=i%>" type="textbox" onfocus="this.select()" onkeypress="validarnumero()" onblur="if(this.value==''){this.value=0}" value="<%=rsAlumnos("notafinal_dma")%>" onkeyup="validarnota(this,'<%=notaminima_cac%>',txtcondicion_dma<%=i%>)" size="2" maxlength="2" <%=estadoControl%> />
				    <%end if%>
				    </td>
				    <td class="<%=rsAlumnos("condicion_dma")%>" id="txtcondicion_dma<%=i%>">
				    <%=rsAlumnos("condicion_dma")%>
				    </td>
				    <td align="right" width="10%" style="cursor:hand">
				    <%if session("codigo_tfu")<>13 then %>
				    <img src="../../../../images/credito.gif" onclick="AbrirHistorial('<%=rsAlumnos("codigouniver_alu")%>')" alt="Haga click aquí para ver el HISTORIAL DEL ESTUDIANTE" />
				    <%end if%>
				    <%if session("codigo_tfu")=13 then %>
				    <img src="../../../../images/credito.gif" onclick="VerInformacionEstudiante('<%=rsAlumnos("codigouniver_alu")%>','<%=rsAlumnos("codigo_Alu")%>')" alt="Haga click aquí para ver el HISTORIAL DEL ESTUDIANTE" />
				    <%end if%>
				    <%
				    if codigo_aut<>0 AND rsAlumnos("estado_dma")="M" then%>
				    <img src="../../../../images/editar.gif" onclick="AbrirModificarNota('<%=notaminima_cac%>','<%=rsAlumnos("codigo_dma")%>')" alt="Haga click aquí para MODIFICAR LA NOTA" />
				    <%end if%>
				    </td>
				    </tr>
				    <%'end if
				    rsAlumnos.movenext
			    Loop
			    rsAlumnos.close
			    set rsAlumnos=Nothing
			    %>
			    </table>
			    </div>
    			
    			
			    </td>
			    </tr>
			    <tr>
				    <td width="100%" height="5%" colspan="7" class="usattablainfo">
				    &nbsp;NOTA MÍNIMA para el Ciclo Académico [<%=descripcion_cac%>]:&nbsp; <%=notaminima_cac%> |
				    &nbsp;&nbsp;&nbsp;&nbsp;<span class="azul">Aprobados:<%=aprobados%></span>  | 
				    <span class="rojo">Desaprobados: <%=desaprobados%> | </span> 
				    <span class ="cursos">Retirados: <%=retirados%>&nbsp;</span>
				    </td>
			    </tr>
		    </table>
        </td>
      </tr>
     </table>
</form>
</body>
</html>
<%
end if
end if
if Err.number <> 0 then
    response.Write(Err.Description)
end if
%>