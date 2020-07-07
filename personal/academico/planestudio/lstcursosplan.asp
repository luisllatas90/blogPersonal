<!--#include file="../../../funciones.asp"-->
<%
codigo_pes=request.querystring("codigo_pes")
modo=request.querystring("modo")
%>
<html>
<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>lista de cursos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validarplan.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<%
	dim rsCurso,obj
	set obj=Server.CreateObject("PryUSAT.clsDatPlanEstudio")
		set rsCursos=obj.ConsultarCursoPlan("RS","PL2",codigo_pes,0,0)
	set obj=nothing		
	if modo="D" then
			
	end if
%>
		<form name="frmlistacursos" METHOD="POST" ACTION="procesar.asp?accion=modificarplancurso&codigo_pes=<%=codigo_pes%>">
		<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%"  height="100%" tbl="lista">
      	<tr class="usatceldatitulo">
    	<td width="3%" height="5%"><input type="checkbox" name="chkSeleccionar" onclick="MarcarTodoCheck()" value="0">&nbsp;</td>
    	<td width="5%" height="5%">&nbsp;</td>
    	<td width="5%" height="5%">Tipo</td>
	    <td width="10%" height="5%">Código</td>
    	<td width="32%" height="5%">Descripción del curso</td>
	    <td width="5%" height="5%">Crd.</td>
    	<td width="5%" height="5%">HT</td>
    	<td width="5%" height="5%">HP</td>
    	<td width="5%" height="5%">HL</td>
    	<td width="5%" height="5%">HA</td>
    	<td width="5%" height="5%">TH</td>
	    <td width="5%" height="5%">Ciclo</td>
	    <td width="5%" height="5%">Oblig.</td>
	    <td width="5%" height="5%">Vigencia</td>
      	</tr>
      	<tr>
    	<td width="100%" colspan="14">
    	<div id="listadiv" style="height:100%" class="NoImprimir">
    	<table border="0" cellpadding="3" cellspacing="0" width="100%">
    	<%i=0
			Do while Not rsCursos.EOF
				i=i+1
				estado=iif(rsCursos("estado_Pcu")=true,"1","0")
			%>
	      <tr id="fila<%=i%>">
    	    <td width="3%"><input type="checkbox" name="chk" id="chk<%=i%>" value=<%=rsCursos("codigo_cur")%> onClick="pintarfilamarcada(this)"></td>
    	    <td width="5%"><%=i%>
    	    <input type="hidden" name="codigo_cur" value=<%=rsCursos("codigo_cur")%>>
    	    <input type="hidden" id="totalhoras_cur<%=i%>" name="totalhoras_cur" value=<%=rsCursos("totalhoras_cur")%>>
    	    <input type="hidden" name="estado_pcu" value="<%=estado%>">
    	    <input type="hidden" name="tipo_cur" value=<%=rsCursos("tipo_cur")%>>

    	    </td>
        	<td width="5%">
        	<!--<select name="tipo_cur" class="cajas3">
				<option value="FG" <%=SeleccionarItem("cbo","FG",rsCursos("tipo_cur"))%>>FG</option>
				<option value="FB" <%=SeleccionarItem("cbo","FB",rsCursos("tipo_cur"))%>>FB</option>
				<option value="FE" <%=SeleccionarItem("cbo","FE",rsCursos("tipo_cur"))%>>FE</option>
    		</select>
    		-->
    		<%=rsCursos("tipo_cur")%>
    		</td>
    		<td width="10%"><%=rsCursos("identificador_cur")%>&nbsp;</td>
		    <td width="33%"><%=rsCursos("nombre_cur")%>&nbsp;</td>
    		<td width="5%" align="center">
    		<input type="hidden" onkeypress="validarnumero()" name="creditos_cur" value="<%=rsCursos("creditos_cur")%>" class="cajas3" size="2">
    		<%=rsCursos("creditos_cur")%>
			</td>
    		<td width="5%" align="center">
    		<input type="text" onkeypress="validarnumero()" onkeyup="CalcularTotalhoras('<%=i%>')" id="horasteo_cur<%=i%>" name="horasteo_cur" value="<%=rsCursos("horasteo_cur")%>" class="cajas3" size="2">
			</td>
    		<td width="5%" align="center">
    		<input type="text" onkeypress="validarnumero()" onkeyup="CalcularTotalhoras('<%=i%>')" id="horaspra_cur<%=i%>" name="horaspra_cur" value="<%=rsCursos("horaspra_cur")%>" class="cajas3" size="2">
			</td>
    		<td width="5%" align="center">
    		<input type="text" onkeypress="validarnumero()" onkeyup="CalcularTotalhoras('<%=i%>')" id="horaslab_cur<%=i%>" name="horaslab_cur" value="<%=rsCursos("horaslab_cur")%>" class="cajas3" size="2">
			</td>
    		<td width="5%" align="center">
    		<input type="text" onkeypress="validarnumero()" onkeyup="CalcularTotalhoras('<%=i%>')" id="horasase_cur<%=i%>" name="horasase_cur" value="<%=rsCursos("horasase_cur")%>" class="cajas3" size="2">
			</td>
    		<td width="5%" align="center"><%=rsCursos("totalhoras_cur")%></td>
    		<td width="8%" align="center">
    		<select name="ciclo_cur" class="cajas3">
    		<%for j=1 to 12
    			response.write "<option value=""" & j & """ " & SeleccionarItem("cbo",j,rsCursos("ciclo_cur")) & ">" & ConvRomano(j) & "</option>"
    		next%>
    		</select>
    		</td>
			<td width="5%" align="center">
			<select name="electivo_cur" class="cajas3">
				<option value="0" <%=SeleccionarItem("cbo",false,rsCursos("electivo_cur"))%>>Sí</option>
				<option value="1" <%=SeleccionarItem("cbo",true,rsCursos("electivo_cur"))%>>No</option>
    		</select>
    		</td>
        	<td width="5%" height="5%" class="<%=iif(estado="0","rojo","azul")%>"><%=iif(estado="0","Inactivo","Activo")%></td>
      	  </tr>
      		<%rsCursos.movenext
	      Loop
	      Set rsCursos=nothing%>
	      </table>
		  </div>
		 </td>
		 </tr>
	    </table>
</body>
</html>