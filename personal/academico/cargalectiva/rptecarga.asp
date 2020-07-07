<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_usu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

dim obj
codigo_cac=request.querystring("cboCiclo")
codigo_dac=request.querystring("cboDpto")
codigo_test=request.querystring("cboTipoEstudio")
cargo = request.querystring("txtcargo")

'sel1= request.querystring("chkPre")
'sel2= request.querystring("chkCom")
'sel3= request.querystring("chkEsp")

if codigo_cac="" then codigo_cac=session("codigo_cac")

Set obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	Set rsCac= obj.ConsultarCicloAcademico ("RS","TO","")
Set obj=nothing

Set obj=Server.CreateObject("PryUSAT.clsDatDepartamentoAcademico")
	Set rsDpto= obj.ConsultarDepartamentoAcademico ("RS","TO","")
Set obj=nothing

if(CINT(codigo_test) = -2) then
    codigo_test = -1
end if

'if cargo="" then
	sel1="checked"
	sel2="checked"
	sel3="checked"
'end if

%>
<html>
<head>
<title>Consultar Carga Academica</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="private/validarcargaacademica.js"></script>

<script>
    function enviar() {
        if (frmCarga.cboTipoEstudio.value == "-2") {
            alert("Debe seleccionar un tipo de estudio!!");
        }
		if (frmCarga.cboDpto.value=="-2")
		{
			alert("Debe seleccionar un departamento académico!!");
		
		}
		else {
		  form_res.innerHTML = ""
		  document.getElementById("cargando").style.display = "block"
		  document.getElementById("boton_enviar").disabled = "disabled"
	      document.frmCarga.action = "rptecarga.asp" 
	      document.frmCarga.submit();
	    }  
	}
</script>

<style>

.pre         { color: #0000FF }
.esp         { color: #008080 }
.com         { color: #FF0000 }
.edu         { color: #DF7401 }
.pos         { color: #084B8A }
.go         { color: purple }
    .style1
    {
        width: 6%;
    }

</style>
</head>
<body>
<form name="frmCarga" method="get"> 
<input type="hidden" name="txtcargo" value="1">
<table width="100%" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse">
  <tr>
    <td height="5%" class="usattitulo" width="529">Reporte de Carga Académica</td>
    <td height="5%" width="100">&nbsp;</td>
  </tr>
  <tr>
    <td colspan="2" height="10%" valign="top" width="635">
	<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="635">
      <tr>
        <td width="102">Semestre académico</td>
        <td colspan="3" width="432"><%'call llenarlista("cboCiclo","ActualizarListaCarga('rptecarga.asp')",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>		
        <%call llenarlista("cboCiclo","",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>		
        </td>
        </tr>
      <tr>
        <td width="102">Departamento</td>
        <td colspan="3" width="432"><%'call llenarlista("cboDpto","ActualizarListaCarga('rptecarga.asp')",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico","S","")%>
        <%call llenarlista("cboDpto","",rsDpto,"codigo_dac","nombre_dac",codigo_dac,"Seleccionar el Dpto Académico","S","")%>
        
        </td>
        </tr>
      <tr>
        <td width="102">Tipo de Estudio:</td>
        <td width="241">
            <%
            Set obj1=Server.CreateObject("PryUSAT.clsAccesoDatos")
	        obj1.AbrirConexion
		    Set rsTipoEstudio= obj1.Consultar("ACAD_ConsultarTipoEstudio","FO","TO",0)		        
            obj1.CerrarConexion
            Set obj1=nothing            
            
            call llenarlista("cboTipoEstudio","",rsTipoEstudio,"codigo_test","descripcion_test",codigo_test,"Seleccionar tipo de Estudio","S","")
            %>
          </td>
        <td width="185" bgcolor="#FFFFCC"><b>&lt;--- Importante:</b> Verifique las opciones 
            seleccionadas antes de realizar la consulta</td>
        <td width="83"><label>
          <input type="button" id="boton_enviar" name="Submit" value="Consultar" onClick="enviar()" >
        </label></td>
      </tr>
    </table></td>
  </tr>
</table>
<p id="cargando" style="display:none" class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Verificando Carga Académica...</p>
</form>
<form id="form_res">
<%
if codigo_dac<>"-2" and codigo_dac<>"" then
	Set Obj=CreateObject("PryUSAT.clsDatDocente")
	'Set rsDocente=Obj.ConsultarCargaAcademica("RS", "DCA",codigo_cac,codigo_dac)
	'Dim rsDocente
    set objC=Server.CreateObject("PryUSAT.clsAccesoDatos")
    objC.AbrirConexion
    Set rsDocente= objC.Consultar("ACAD_ListaDocentexTipoEstudio","FO",codigo_cac,codigo_dac,codigo_test)
    objC.CerrarConexion
    Set objC=nothing     
		
	i=0
	If Not (rsDocente.BOF AND rsDocente.EOF) then
		Do While Not rsDocente.EOF
 			i=i+1
 %>
 
	<table id"tabla_res" width="100%" border="1" bordercolor="#C0C0C0" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	  <tr bgcolor="#FFFFFF">
	  	<td bgcolor="#FFFFCC" width="10%"><b>Docente:</b></td>
   		<td colspan="2"><a target="_blank" href="../expediente/consultas/personal.aspx?id=<%=rsDocente("codigo_per")%>"><%=rsDocente("Docente")%></a>&nbsp;</td>
   		<td width="10%" colspan="8"><b>Dedicación:<%=rsDocente("Descripcion_Ded")%></b></td>
      </tr>
	  <%
	    Dim codigo_per 
	    codigo_per = rsDocente("codigo_per")	    
  		'Set rsCarga = Obj.ConsultarCargaAcademica("RS", "9",codigo_cac,codigo_per)    		
  		set objC=Server.CreateObject("PryUSAT.clsAccesoDatos")
        objC.AbrirConexion
        Set rsCarga= objC.Consultar("ACAD_ListaCursosxDocenteHDO","FO",codigo_cac,codigo_per,codigo_test)
        objC.CerrarConexion
        Set objC=nothing 
        
	  	if Not(rsCarga.BOF AND rsCarga.EOF) then	  		
  	  %>
	 <tr class="etabla"> 
    	<th width="10%">Código</th>
    	<th width="15%">T. Estudio</th>
	    <th width="25%">Nombre del Curso</th>
    	<th width="10%">Grupo horario</th>
	<th class="style1">Vacantes</th>
	<th width="5%">Matriculados</th>
	<th width="10%">Escuela Profesional</th>
    	<th width="5%">Ciclo</th>
    	<!--<th width="5%">Hrs. Clase</th>-->
    	<!--<th width="5%">Hrs. Asesoría</th>-->
	    <!--<th width="5%">Total Hrs.</th>-->
	    <th width="5%">Hrs. Teoría</th>
	    <th width="5%">Hrs. Práctica</th>
	    <th width="5%">Hrs. Total</th>
	  </tr>
  		<%
  		totalhoras=0
  		totalComp=0
  		totalEsp=0
  		totalEdu=0
  		totalPos=0
  		totalGo =0
  		clase=""
  		  		
  		Do while not rsCarga.EOF  		    
  			clase=""
  			if IsNull(rsCarga("totalcarga"))=true then
  				TC=0
  			else
  				'TC=rsCarga("totalcarga")
  				TC= rsCarga("totalhorasteoria") +  rsCarga("totalhoraspractica")
  			end if
  			
			'if (rsCarga("codigo_cpf") <> 19 And rsCarga("codigo_cpf") <> 25) then
			if (rsCarga("codigo_test") = 2) then
				if sel1="checked" then
		  			totalhoras=totalhoras + TC
		  			clase="class='pre'"
		  		end if
		  			
		  	else
		  		'if rsCarga("codigo_cpf") = 19 then		  		
		  		if (rsCarga("codigo_test") = 4) then
				  if sel2="checked" then
			  		totalComp= totalComp + TC
		  			clase="class='com'"			  		
		  		  end if	
				else
					'if rsCarga("codigo_cpf") = 25 then
					if (rsCarga("codigo_test") = 3) then					  
						totalEsp= totalEsp + TC
						clase="class='esp'"					  	
					end if
					if (rsCarga("codigo_test") = 6) then
				    if sel3="checked" then
					    totalEdu = totalEdu + TC
					    clase="class='edu'"
				      end if	
				    end if
    				
				    if (rsCarga("codigo_test") = 5) then
				      if sel3="checked" then
					    totalPos= totalPos + TC
					    clase="class='pos'"
				      end if	
				    end if	
				    
				     if (rsCarga("codigo_test") = 10) then
				      if sel3="checked" then
					    totalGo= totalGo + TC
					    clase="class='pos'"
				      end if	
				    end if	
				    
				end if
			end if		
	  
		%>
		
	  
	  <% if ((rsCarga("codigo_cpf") <> 19 And rsCarga("codigo_cpf") <> 25) and sel1="checked") or (rsCarga("codigo_cpf") = 19 and sel2="checked") or (rsCarga("codigo_cpf") = 25 and sel3="checked")   then%>	
	  <tr <%=clase%>> 
    	<td width="10%"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
    	<td width="10%"><%=rsCarga("descripcion_test")%>&nbsp;</td>
	    <td width="40%"><a href="frmCursoComentarios.aspx?id=<% response.write(codigo_per) %>&cup=<%response.write(rsCarga("codigo_cup"))%>&curso=<%=rsCarga("nombre_Cur")%>" target="_blank"><%=rsCarga("nombre_Cur")%>&nbsp;</a></td>
    	<td width="10%" align="center"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
	    <td align="center" class="style1"><%=rsCarga("vacantes_cup")%></td>
        <td width="5%" align="center"><%=rsCarga("nromatriculados_cup")%></td>
	    <td width="10%"><%=rsCarga("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("ciclo_Cur")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("totalhorasteoria")%>&nbsp;</td> <!--rsCarga("totalhorasaula")-->
    	<td width="5%" align="center"><%=rsCarga("totalhoraspractica")%>&nbsp;</td> <!--rsCarga("totalhorasasesoria")-->
	    <td width="5%" align="center"><%=rsCarga("totalhorasteoria") + rsCarga("totalhoraspractica") %>&nbsp;</td> <!--rsCarga("totalcarga")-->
	  </tr>
	  
	 <%end if%>		  
	  
  			<%rsCarga.MoveNext
	 	Loop
    	Set rsCarga=nothing%>

	  <tr bgcolor="#FFFFCC"> 
  	    <td colspan="9" align="left" width="90%"><b>
  	        <span class='pre'>HRS. PREGRADO:<%=totalHoras%></span>&nbsp;&nbsp;&nbsp;&nbsp;
  	        <span class='com'>HRS. COMPL.:<%=totalComp%></span>&nbsp;&nbsp;&nbsp;&nbsp;
  	        <span class='esp'>HRS. PROF.:<%=totalEsp%></span>&nbsp;&nbsp;&nbsp;&nbsp;
  	        <span class='edu'>HRS. ED. CONT.:<%=totalEdu%></span>&nbsp;&nbsp;&nbsp;&nbsp;
  	        <span class='pos'>HRS. POST.:<%=totalPos%></span>&nbsp;&nbsp;</b>  	        
  	        <span class='go'>HRS. GO.:<%=totalGo%></span>&nbsp;&nbsp;</b>  	        
  	    </td>
    	<td align="center" width="5%"><b>HORAS TOTALES</b></td>
    	<td align="center" width="5%"><b><%=totalHoras + totalComp + totalEsp + totalEdu + totalPos + totalGo %>&nbsp;</b></td>
	  </tr>


  	  <%end If%>
	</table>
	<br>            
	    <%rsDocente.MoveNext
	 	Loop
	 	
	else
		response.write "<p class=""usatsugerencia"">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;No se han encontrado profesores con Carga Académica</p>"
	end if
Set rsdocente=nothing
Set Obj=nothing
Set rsCac=nothing
Set rsDpto=nothing
end if

%>
</form>
</body>
</html>