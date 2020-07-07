<%
codigo_per=request.querystring("codigo_per")
codigo_cac=request.querystring("codigo_cac")

if codigo_per<>"" then

Sub MostrarCursosHijo(ByVal ca,ByVal cp)
dim rsCursosHijo,j

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		Set rsCursosHijo=Obj.Consultar("ConsultarCursoProgramado","FO","14",ca,cp,0,0)
		Obj.CerrarConexion
		
	Set obj=nothing
	%>
	<tr><td id="tbl<%=cp%>" style="display:none;" width="100%" colspan="11" align="right">
	<table width="95%" border="1" bordercolor="#DCDCDC" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	  <tr class="EncabezadoHorario"> 
    	<th width="3%">&nbsp;</th>
    	<th width="15%">Código</th>
	    <th width="50%">Nombre del Curso</th>
    	<th width="5%">Grupo horario</th>
	    <th width="10%">Escuela Profesional</th>
    	<th width="5%">Ciclo</th>
	    <th width="5%">Crd.</th>
	    <th width="5%">TH</th>
	  </tr>
  	  <%Do while not rsCursosHijo.EOF
  	  	j=j+1
  	  %>
	  <tr class="piepagina" id="fila<%=j%>"> 
    	<td width="3%">
        <input type="hidden" name="chkHijo" id="chkHijo<%=j%>" value="<%=rsCursosHijo("codigo_cup")%>" onClick="VerificaCheckMarcados(this,parent.document.all.cmdDesagrupar)">
        &nbsp;</td>
    	<td width="15%"><%=rsCursosHijo("identificador_Cur")%>&nbsp;</td>
	    <td width="50%"><%=rsCursosHijo("nombre_Cur")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCursosHijo("grupoHor_Cup")%>&nbsp;</td>
	    <td width="10%"><%=rsCursosHijo("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCursosHijo("ciclo_Cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCursosHijo("creditos_cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCursosHijo("totalhoras_cur")%>&nbsp;</td>
	  </tr>
  			<%rsCursosHijo.MoveNext
	 	Loop
    	Set rsCursosHijo=nothing%>
	</table>
	</td></tr>
	<%
end sub

	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		Set rsCarga=Obj.Consultar("ConsultarCargaAcademica","FO","11",codigo_cac,codigo_per)
		Obj.CerrarConexion
	Set obj=nothing
	
	if Not(rsCarga.BOF and rsCarga.EOF) then
		Agrupar=true
	end if	
%>
<html>

<head>
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Código</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script language="JavaScript" src="../private/validarcargaacademica.js"></script>
</head>
<body topmargin="0" leftmargin="0">
<table width="100%" border="1" bordercolor="#DCDCDC" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	  <tr class="etabla"> 
    	<th width="8%">&nbsp;</th>
    	<th width="15%">Código</th>
	    <th width="40%">Nombre del Curso</th>
    	<th width="5%">Grupo horario</th>
	    <th width="10%">Escuela Profesional</th>
    	<th width="5%">Ciclo</th>
	    <th width="5%">Crd.</th>
	    <th width="5%">TH</th>
	    <th width="5%">TH. Clase</th>
	    <th width="5%">TH. Asesoría</th>
	    <th width="5%">Total Hrs.</th>
	  </tr>
  		<%
  		dim totahoras,HR
  		totalhoras=0
  		HR=0
  		Do while not rsCarga.EOF
  			i=i+1
  			HC=totalhoras + rsCarga("totalhoras_cur")
  			if IsNull(rsCarga("subtotalhoras"))=true then
  				ST=0
  			else
  				ST=rsCarga("subtotalhoras")
  			end if
  			HR=HR + ST
  			HA=HA + rsCarga("totalhorasaula")
  			HS=HS + rsCarga("totalhorasasesoria")

  			if rsCarga("operadormod_car")="" or IsNull(rsCarga("operadormod_car"))=true then
	  			nombre_cur=rsCarga("nombre_Cur")
	  		else
	  	  		nombre_cur=rsCarga("nombre_Cur") & "<br><span class=rojo> Actualizado por " & rsCarga("operadormod_car") & " " & rsCarga("fechamod_car") & "</span>"		
	  		end if
	  		
	  		if rsCarga("nodos")>0 then
	  			HabilitarHijos=true
			else
				HabilitarHijos=false
	  		end if
	  	%>
	  <tr class="piepagina" id="fila<%=i%>"> 
    	<td width="8%">
    	<%if Agrupar=true then%>
        <input type="hidden" name="chk" id="chk<%=i%>" value="<%=rsCarga("codigo_cup")%>" onClick="pintarfilamarcada(this);VerificaCheckMarcados(this,parent.document.all.cmdAgrupar)">

        <%end if%>
        <%if HabilitarHijos=true then%><img src="../../../../images/mas.gif" id="img<%=rsCarga("codigo_cup")%>" onClick="MostrarTabla(tbl<%=rsCarga("codigo_cup")%>,this,'../../../images')"><%end if%>
        </td>
    	<td width="15%"><%=rsCarga("identificador_Cur")%>&nbsp;</td>
	    <td width="40%"><%=nombre_Cur%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("grupoHor_Cup")%>&nbsp;</td>
	    <td width="10%"><%=rsCarga("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCarga("ciclo_Cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCarga("creditos_cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCarga("totalhoras_cur")%>&nbsp;</td>
	    <td width="5%" align="center" bgcolor="#E1F1FB">
        <input name="totalhorasaula" value="<%=rsCarga("totalhorasaula")%>" class="cajas2" size="20" onkeypress="validarnumero()" onkeyup="validarhorascarga(this)" codigo_cup="<%=rsCarga("codigo_cup")%>" maxlength="2"></td>
	    <td width="5%" align="center" bgcolor="#E1F1FB">
        <input name="totalhorasasesoria" value="<%=rsCarga("totalhorasasesoria")%>" class="cajas2" size="20" onkeypress="validarnumero()" onkeyup="validarhorascarga(this)" maxlength="2"></td>
	    <td width="5%" align="center" bgcolor="#E1F1FB"><%=rsCarga("subtotalhoras")%>&nbsp;</td>
	  </tr>
  			<%	if HabilitarHijos=true then
  					Call MostrarCursosHijo(codigo_cac,rsCarga("codigo_cup"))
	  			end if
  			rsCarga.MoveNext
	 	Loop
    	Set rsCarga=nothing%>
	  <tr class="total"> 
	    <td colspan="7" align="right">Total</td>
    	<td align="center"><%=HC%>&nbsp;</td>
    	<td align="center"><%=HA%>&nbsp;</td>
    	<td align="center"><%=HS%>&nbsp;</td>
    	<td align="center"><%=HR%>&nbsp;</td>
	  </tr>
	</table>
</body>

</html>
<%end if%>