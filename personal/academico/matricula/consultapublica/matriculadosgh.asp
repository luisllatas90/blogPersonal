<html>
<head>
	<meta http-equiv="Content-Language" content="es">
	<Title>Matriculados Por Grupo Horario</title>
	<script language="JavaScript" src="../../../../private/funciones.js"></script>
	<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
	<script>
	function EnviarDatos(pagina,cp,ca,cc)
	{
		location.href=pagina + "?cp=" + cp + "&ca=" + ca + "&cc=" + cc
		//alert (txtPto.value);
		
	}
	</script>
</head>

<body>

<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" >
  <tr>
    <td>
     <p align="center"><b><font face="Arial" size="4" color="#800000">
		Matriculados Por Grupo Horario</font></b><Br align="center" class="etiqueta">
    
    Informe Hasta: <%=now%>
    </td>
  </tr>
</table>


<%

Dim obj, objclicloacadema, objCarrera
Dim rs, rsclicloacadema, rsCarrera
Dim codigo_Cac
Dim codigo_cpf
Dim Ciclo


'Recuperar Parametros de Consulta

'codigo_Cac=request.Form("cbocicloacademico")
codigo_cpf=request.form("cboCarreraProfesional")
Ciclo=request.form("cboCiclo")


codigo_Cac="25" 
if trim(codigo_cpf)="" then codigo_cpf="-1"
if trim(ciclo)="" then ciclo="-1"




Set objclicloacadema=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
Set rsclicloacadema=Server.CreateObject("ADODB.Recordset")
Set rsclicloacadema= objclicloacadema.ConsultarCicloAcademico("RS","TO","")


Set objCarrera=Server.CreateObject("PryUSAT.clsDatCarreraProfesional")
Set rsCarrera=Server.CreateObject("ADODB.Recordset")
Set rsCarrera= objCarrera.ConsultarCarreraProfesional("RS","TC","")
%>

<form name="frmParametro" method="post" action="matriculadosgh.asp">

 <table width="60%" border="0" style="border-collapse: collapse" bordercolor="#111111" align="center">
 <tr>
  	<td width="20%" class="etiqueta">Carrera Profesional:</td>
    <td width="40%"><select name="cboCarreraProfesional" style="width: 100%"> 
	  	<option value="0">---TODAS LAS CARRERAS---</option>
			   <% do while not rsCarrera.eof
       				seleccionar="" 
					if(cint(codigo_cpf)=rsCarrera(0)) then 
						seleccionar="SELECTED"
						Carrera= rsCarrera("nombre_Cpf")
					end if
					%>
					
        			<option value="<%=rsCarrera(0)%>" <%=seleccionar%>>
					<%=rsCarrera("nombre_Cpf")%>
					</option>
					<% rsCarrera.movenext
				loop%>
        </select>
	  </td>
  </tr>
	  <tr>
        <td width="20%" class="etiqueta">Semestre Académico:</td>
        <td width="40%"><select name="cbocicloacademico" disabled style="width: 100%">
	         <option value="0" >---Seleccione cilclo Academico---</option>
				   <% do while not rsclicloacadema.eof 
					seleccionar="" 
					if (cint(codigo_cac)=rsclicloacadema(0)) then 
						seleccionar="SELECTED"
						descripcion_Cac=rsclicloacadema("descripcion_cac")
					end if
						
					%>
       				 <option value="<%=rsclicloacadema(0) %>" <%=seleccionar%>>
					<%=rsclicloacadema("descripcion_Cac")%>
					</option>
					<% rsclicloacadema.movenext
					loop
					%>
              </select>
		</td>
    </tr>
    <tr>
    	<td width="20%" class="etiqueta">Ciclo de Asignatura:</td>
    		
    		 <td width="40%"><select name="cboCiclo" style="width: 100%"> 
			  	<option value="0">---TODOS LOS CICLOS---</option>
				   <% For x=1 to 12
       						seleccionar="" 
						if(cint(Ciclo)= cint(x)) then 
							seleccionar="SELECTED"
						end if
					%>
        		 	<option value="<%=x%>" <%=seleccionar%>><%=x%> 
					</option>
					<% 
   				 Next%>
              </select>
    	</td>
   </tr>
   <tr align="right" >
		<td colsPan=2 >
			<Input type="Submit" value="Consultar" class="usatbuscar" >
            <input type="button" value="Exportar" name="cmdExportar" id="cmdexportar" class="excel" onClick="EnviarDatos('ExportarGruposHorarios.asp',<%=codigo_cpf%>,<%=codigo_cac%>,<%=ciclo%>)" disabled>
        </td> 
		
	</tr>

  </table>
</form>

<%

rsclicloacadema.Close 
set rsclicloacadema = nothing
Set objclicloacadema=nothing

rsCarrera.Close 
set rsCarrera=nothing
set objCarrera=nothing

IF codigo_cpf="0" THEN codigo_cpf="%%"
IF ciclo="0" THEN ciclo="%%"

Set obj = Server.CreateObject("pryUSAT.clsDatMatricula")
Set rs = Server.CreateObject("ADODB.RecordSet")
Set rs= obj.ConsultarAlumnosMatriculados("RS","27",codigo_cpf,codigo_cac,ciclo)

%>
<table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="AutoNumber1">
  <tr>
    <td width="73%"><font color="#800000"><b>&nbsp;Leyenda:<br>&nbsp;</b></font><b>&nbsp;&nbsp;&nbsp;&nbsp;- MATR:</b> 
    Número&nbsp; de alumnos que se han inscrito en 
    cada curso y que <b>han cancelado el concepto de matrícula</b>.
    <br><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;- PRE MAT:</b> Número de alumnos que se han inscrito en cada curso y que 
    <b>no han 
    cancelado aún el concepto de matrícula. </b>
    <br><font color="#800000"><b>&nbsp;Importante:</b></font><br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Tener en consideración que los Pre Matriculados pasarán a ser 
    matriculados cuando cancelen el concepto de matrícula.    </td>
    <td width="27%"><table width="100%" border="0" cellspacing="3" cellpadding="0">
      <tr>
        <td><strong>LEYENDA</strong></td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td align="center"><span style="cursor:hand"><IMG  src="../../../../images/CR.GIF" width="14" height="14" border="0" align="middle"></span></td>
        <td>QUEDAN 5 VACANTES O MENOS </td>
      </tr>
      <tr>
        <td align="center"><img src="../../../../images/CA.GIF" width="14" height="14" border="0"></td>
        <td>QUEDAN DE 6 A 10 VACANTES </td>
      </tr>
      <tr>
        <td align="center"><span style="cursor:hand"><IMG  src="../../../../images/CV.GIF" width="14" height="14" border="0" align="middle"></span></td>
        <td>QUEDAN M&Aacute;S DE 10 VACANTES </td>
      </tr>
      <tr>
        <td align="center"><span class="rojo"><strong>(Aut.)</strong></span></td>
        <td>GENERADO AUTOMATICAMENTE POR EL SISTEMA </td>
      </tr>
      <tr>
        <td align="center"><span class="cursoM"><strong>(Cerr.)</strong></span></td>
        <td>CERRADO POR L&Iacute;MITE DE VACANTES </td>
      </tr>
    </table></td>
  </tr>
</table>
<br>
<table align="center" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" >
	<tr>
		<td class="etiqueta">
			Resultado de la Consulta:<!--( <%=Carrera%> | <%=descripcion_Cac%> | <%=ciclo%> )--> </td>
	</tr>
</table>
<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" >
  <tr  height="20" class="etabla">
    
    <td width="35%" align="left">&nbsp;ASIGNATURA</td>
    <td align="center"  width="3%">G.H.</td>
    <td align="center"  width="4%">CICLO</td>
    <td align="left"  width="30%">&nbsp;CARRERA</td>
    <td align="center" width="5%">MATR</td>
    <td align="center" width="5%">PRE MAT</td>
    <td align="center" width="5%">TOTAL</td>
	<td align="center" width="5%">VACANTES</td>
	<td align="center" width="5%">FALTANTES</td>
	<td align="center" width="5%">ESTADO</td>
	<td align="center" width="5%">HORARIO</td>

  </tr>
  <%
    Inicial="0"
    color="white"
    
  	Do while Not rs.eof
  		
		if trim(Inicial)=trim(rs("nombre_Cur"))then
			if color="white" then
				color="white"
			else
				color="#EEEEEE"
			end if
		else
			if color="white" then
				color="#EEEEEE"
			else
				color="white"
			end if
		
		end if
		
		%>
	
	
    <tr height="20" bgcolor="<%=color%>"  onMouseOver="Resaltar(1,this)" onMouseOut="Resaltar(0,this)" >
		
	  <td width="35%" align="left" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=M'">&nbsp;<%=rs("nombre_Cur")%><span class="rojo"><strong>
		<%if rs("obs_Cup")<>""  then
		response.write("(Aut.)")
		end if
		%>
	  </strong></span>
		<span class="cursoM cursoM"><strong>
		<%if rs("estado_Cup")=0  then
		response.write("(Cerr.)")
		end if
		%>	  
	      </strong></span></td>
		<td width="3%" align="center" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=M'"><%=rs("GrupoHor_Cup")%>&nbsp;</td>
		<td width="4%" align="center" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=M'"><%=rs("ciclo_Cur")%>&nbsp;</td>
		<td width="30%" align="left" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=M'">&nbsp;<%=rs("nombre_Cpf")%></td>
		<td width="5%" align="center" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=M'"><b><%=rs("NroMatriculados")%></b>&nbsp;</td>
		<td width="5%" align="center" style="cursor:hand" onClick="location.href='listaAlumnosMatAgrupados.asp?codigo_cup=<%=rs("codigo_Cup")%>&codigo_cac=<%=codigo_cac%>&nc=<%=rs("nombre_Cur")%>&gh=<%=rs("GrupoHor_Cup")%>&cp=<%=rs("nombre_Cpf")%>&estado=P'"><%=rs("NroPreMatriculados")%>&nbsp;</td>
		<%total=cint(rs("NroMatriculados"))+ cint(rs("NroPreMatriculados"))
		   if cint(total) >54 then%>
			<td width="5%" align="center" ><font color="#800000"><b><%=total%></b></font>&nbsp;</td>
		   <%else%>
			<td width="5%" align="center" ><%=total%>&nbsp;</td>
		   <%end if	%>
		<td width="5%" align="center" ><%=rs("vacantes_cup")%>&nbsp;</td>
		<td width="5%" align="center" ><%=CINT(rs("vacantes_cup"))-CINT(TOTAL)%>&nbsp;</td>
		<%IF CINT(total) >= cint(rs("vacantes_cup")) -5 THEN %>
		<td  width ="5%" align="center"><IMG align="middle" border="0"  src="../../../../images/CR.GIF"></td>
		<%ELSE%>
		<%IF (CINT(total) >= cint(rs("vacantes_cup")) -10) AND (CINT(total) < cint(rs("vacantes_cup")) -5) THEN %>
		<td  width="5%" align="center" ><IMG  src="../../../../images/CA.GIF" width="14" height="14" border="0" align="middle"></td>
		<%ELSE%>
		<td   width="5%" align="center"><IMG  src="../../../../images/CV.GIF" width="14" height="14" border="0" align="middle"></td>
		<%END IF%>		
		<%END IF%>
		<td   width="5%" align="center"><IMG  src="../../../../images/menu3.gif" width="16" height="16" border="0" align="middle" onClick="AbrirPopUp('vsthorariocup.asp?codigo_cup=<%=rs("codigo_cup")%>&nombre_cur=<%=rs("nombre_cur")%>','300','600','no','no','no','horario')" style="cursor:hand"s></td>
		


	</tr>
	
	<%
	Inicial=rs("nombre_Cur")
	rs.Movenext
   Loop
	%>
  
</table>

<%
if Inicial<>"0" then%>
	<script>
		 frmParametro.cmdexportar.disabled=false
	</script>
<%else%>
	<script>
		 frmParametro.cmdexportar.disabled=true
	</script>
<%end if

rs.Close
Set rs = Nothing
set obj =Nothing
%>
</body>
</html>