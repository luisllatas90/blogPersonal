<%
dim total_men
if session("codigo_usu")="" then response.redirect "../../../tiempofinalizado.asp"

	Set ObjUsuario= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objUsuario.AbrirConexion
		set rsAccesos=ObjUsuario.consultar("CM_ConsultarFacultad","FO",0,session("codigo_tfu"),session("idcursovirtual"),session("codigo_usu"))
		objUsuario.CerrarConexion
	Set ObjUsuario=nothing
	
	Public function ImagenAlerta(byval a)
		Dim Rojo, Amarillo, Verde
		Dim ValMen, ValInterm, ValSup
    
	    Rojo = "<img border=""0"" src=""../../../images/rojo.gif"">"
    	Amarillo = "<img border=""0"" src=""../../../images/amarillo.gif"">"
	    Verde = "<img border=""0"" src=""../../../images/verde.gif"">"
    
    	ValMen = (100 / 2) - 1
	    ValInterm = (100) - 1
    	ValSup = 100
    	PjeObt=int(a)
    
	    If PjeObt < ValMen Then
    	    ImagenAlerta= Rojo
	    ElseIf PjeObt <= ValInterm Then
    	        ImagenAlerta= Amarillo
        	ElseIf PjeObt >= ValSup Then
            	ImagenAlerta= Verde
	    End If
	End Function
	
Sub ImprimirMenu(codigo_dac,nombre_dac,nombre_fac,profesores,asistentes)
	dim vinculo

		vinculo="location.href='profesoresasistentes.asp?codigo_dac=" & codigo_dac & "&nombre_dac=" & nombre_dac & "'"
		if instr(nombre_fac,"NO DEF")=0 then
			nombre_fac="<u>FACULTAD " & nombre_fac & "</u>"
		else
			nombre_fac=""
		end if
		
		response.write "<table class='Menu' width='100%' border='0' align='center' cellpadding='4' cellspacing='0' onMouseOver=""ResaltarMenuElegido(1,this)"" onMouseOut=""ResaltarMenuElegido(0,this)"" onClick=""" & vinculo & """>" & vbcrlf & vbtab & vbtab
		response.write "<tr>"
		response.write "<td height='5' colspan='2'>" & nombre_fac & "</td>"
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "<tr>" & vbcrlf & vbtab & vbtab
		response.write "<td height='60' width='5%' rowspan='2'>" & ImagenAlerta(asistentes) & "</td>" & vbcrlf & vbtab & vbtab		
		response.write "<td height='30'>" & nombre_dac & "<BR>" & obs & "</td>" & vbcrlf & vbtab & vbtab
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "<tr>" & vbcrlf & vbtab & vbtab
		response.write "<td height='30' class='MenuDescripcion'>" & "Profesores adscritos: " &  Profesores & "<br>Profesores Asistentes: " &  asistentes & "</td>" & vbcrlf & vbtab & vbtab
		response.write "</tr>" & vbcrlf & vbtab & vbtab
		response.write "</table>" & vbcrlf & vbtab & vbtab
end Sub

%>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<title>Asistencia de profesores</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<style type="text/css">
<!--
.Menu {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	font-weight: bold;
	font-size: 12pt;
	font-family:Arial Narrow;
}
.menuElegido {
	border-style: solid;
	border-width: 2px;
	border-color: #96965E;
	background-color: #EBE1BF;
	cursor:hand;
	font-weight: bold;
}
.menuNoElegido {
	border-style: solid;
	border-width: 0px;
	border-color: #96965E;
	background-color: #FFFFFF;
	font-weight: bold;
	font-size: 12pt;
}
.MenuDescripcion {
	font-size: xx-small;
	font-weight:normal
}
-->
</style>
<script language="javascript">
function ResaltarMenuElegido(op,fila)
{
	if(op==1)
		{fila.className="menuElegido"}
	else
		{fila.className="menuNoElegido"}
}

</script>
</head>
<body>
<p class="usatTitulo">Asistencia de profesores por Facultad</p>
<table style="border-style:none; width=70%" height="50%" cellspacing="4" cellpadding="4" align="center">
	<%
	codigo_apl=-1
	i=0
	total=rsAccesos.recordcount
	
	Do while not rsAccesos.EOF
	
		'Salir del bucle sin terminaron los registros
		if (rsAccesos.EOF) then
			exit do
		end if

		if (i mod 2 = 0) then
	%>
	<tr>
		<td style="height: 25%; width: 20%;">
		<%
			ImprimirMenu rsAccesos("codigo_dac"),rsAccesos("nombre_dac"),rsAccesos("nombre_fac"),rsAccesos("profesores"),rsAccesos("asistentes")
		%>	
		</td>
		<td style="height: 25%; width: 5%;">&nbsp;</td>
		<%else%>
		<td style="height: 25%; width: 20%;">
		<%
			ImprimirMenu rsAccesos("codigo_dac"),rsAccesos("nombre_dac"),rsAccesos("nombre_fac"),rsAccesos("profesores"),rsAccesos("asistentes")
		%>
		</td>
	</tr>
		<%end if
		i=i+1
		rsAccesos.movenext
	loop
	Set accesos=nothing
	%>
	</table>
</body>
</html>