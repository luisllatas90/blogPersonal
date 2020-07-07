<%
codigo_cpf=request.querystring("codigo_cpf")
codigo_cup=request.querystring("codigo_cup")
codigo_cac=request.querystring("codigo_cac")
curso=request.querystring("curso")

if codigo_cup<>"" then
Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Set rsCursosHijo=Obj.Consultar("ConsultarCursoProgramado","FO","14",codigo_cac,codigo_cup,0,0)
	Obj.CerrarConexion		
Set obj=nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Lista de Cursos Agrupados</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="javascript">
function DesagruparCursos()
{
	if (confirm("¿Está completamente seguro que desea desagruparlo?")==true){
		var chk=document.all.chkcodigoHijo_cup
		var Hijos=""
		
		if (chk.length==undefined)
			{Hijos=chk.value}
		else{
			for (var i=0; i < chk.length; i++){
				if (chk[i].checked==true){
			   		Hijos+=chk[i].value + ','
				}
			}
    	}
		//alert(Hijos)
		location.href="procesar.asp?accion=desagruparcursos&Hijos=" + Hijos + "&codigo_cac=<%=codigo_cac%>&codigo_cpf=<%=codigo_cpf%>"
	}
}
</script>
    <style type="text/css">
        .style1
        {
            height: 33px;
        }
    </style>
</head>
<body>
<p class="EncabezadoHorario">
<%=curso%>
</p>
<table width="100%" border="1" bordercolor="#DCDCDC" style="border-collapse: collapse" cellpadding="3" cellspacing="0">
	  <tr class="etabla"> 
    	<th width="3%" class="style1"></th>
    	<th width="15%" class="style1">Código</th>
	    <th width="45%" class="style1">Descripción</th>
    	<th width="5%" class="style1">GH</th>
	    <th width="10%" class="style1">Escuela Profesional</th>
    	<th width="5%" class="style1">Ciclo</th>
	    <th width="5%" class="style1">Crd.</th>
	    <th width="5%" class="style1">TH</th>
	    <th width="5%" class="style1">Primer Ciclo</th>
	  </tr>
  	  <%Do while not rsCursosHijo.EOF
  	  %>
	  <tr class="piepagina"> 
    	<td width="3%" align="center">
        <input type="checkbox" name="chkcodigoHijo_cup" value="<%=rsCursosHijo("codigo_cup")%>" onclick="VerificaCheckMarcados(this,cmdGuardar)" />
        </td>
    	<td width="15%"><%=rsCursosHijo("identificador_Cur")%>&nbsp;</td>
	    <td width="45%"><%=rsCursosHijo("nombre_Cur")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCursosHijo("grupoHor_Cup")%>&nbsp;</td>
	    <td width="10%"><%=rsCursosHijo("abreviatura_Cpf")%>&nbsp;</td>
    	<td width="5%" align="center"><%=rsCursosHijo("ciclo_Cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCursosHijo("creditos_cur")%>&nbsp;</td>
	    <td width="5%" align="center"><%=rsCursosHijo("totalhoras_cur")%>&nbsp;</td>
	    <td width="5%" align="center"><% if(rsCursosHijo("soloPrimerCiclo_cup")= true) then response.Write("Si") else response.Write("No") end if %>&nbsp;</td>
	  </tr>
  			<%rsCursosHijo.MoveNext
	 	Loop
    	Set rsCursosHijo=nothing%>
</table>
<br>
<input type="button" name="cmdGuardar" value="Desagrupar" class="eliminar" onclick="DesagruparCursos()" disabled="true" />
<input type="button" name="cmdAtras" value="Atras" class="salir" onclick="history.back(-1)" />
</body>
</html>
<%end if%>