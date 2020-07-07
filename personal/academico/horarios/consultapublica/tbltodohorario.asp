<!--#include file="../../../../funciones.asp"-->
<%
on error resume next
dim rsAmbiente
codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
ciclo_cur=request.querystring("ciclo_cur")
codigo_tam=request.querystring("codigo_tam")
codigo_ube=request.querystring("codigo_ube")
mat = request.querystring("mat")

if(codigo_tam = "-1") then    
    codigo_tam = 0
end if

if(codigo_ube = "-1") then    
    codigo_ube = 0
end if

function AnchoHora(byVal cad)
	if len(cad)<2 then
		AnchoHora="0" & cad
	else
		anchohora=cad
	end if
end function
%>
<html>
<head>

<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" /> 

<title>horario</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css" />
<script type="text/javascript" language="JavaScript" src="../../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../../private/tooltip.js"></script>

<style type="text/css">
td {
	font-size: xx-small;
	text-align: center;
}
.CU {
	background-color: #FFCC00;	
	cursor:hand
}
.CU1 {	
	background-color: #73C5DA;
	cursor:hand
}
.etiquetaTabla {
	background-color: #EAEAEA; 	
	color: #0000FF;
}
</style>
<script type="text/javascript" language="Javascript">
    var contador = 0
    function pintaHora(celda) {
        if (celda.className == "CU" || celda.className == "CU1") {
            AbrirPopUp('lstcursosambiente.asp?dia=' + celda.id + "&codigo_cac=<%=codigo_cac%>&codigo_amb=" + celda.codigo_amb, '400', '700', 'yes', 'yes', 'yes')
        }
    }

    /*
    $(document).ready(function() {       
        $('#tblHorario').dataTable({
            "sPaginationType": "full_numbers"
        });
        new FixedHeader(document.getElementById('tblHorario'));    
    });    
    */
</script>
</head>

<body style="background-color: #DCDCDC;">
<%
dim dia,hora
dim diaBD,inicioBD,finBD
dim TextoCelda
dim marcas

marcas=0
hora=0

'Server.ScriptTimeout=1000


Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
obj.AbrirConexion    
    'Set rsAmbiente=obj.Consultar("ACAD_HorariosAmbiente","FO",codigo_tam, codigo_cpf, codigo_cac, codigo_ube, ciclo_cur)    
    Set rsAmbiente=obj.Consultar("ACAD_HorariosAmbiente","FO",0, 1, codigo_cac, 0, 0)    
obj.CerrarConexion
Set Obj=nothing
'******************************************************
'Imprimir cabezeras de días
'******************************************************
'response.write "<table id='tblHorario' cellpadding=2 style='border-collapse: collapse;' border='1' bgcolor='white' bordercolor='#CCCCCC'>" & vbcrlf
'response.write vbtab & "<thead>" & vbcrlf
'response.write vbtab & "<tr class='etiquetaTabla'>" & vbcrlf
'response.write vbtab & "<th rowspan='2' width='300px'>AMBIENTE</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>LUNES</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>MARTES</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>MIÉRCOLES</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>JUEVES</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>VIERNES</th>" & vbcrlf
'response.write vbtab & "<th colspan='15'>SÁBADO</th>" & vbcrlf
'response.write vbtab & "</tr>" & vbcrlf	
%>
<table id='tblHorario' cellpadding=2 style='border-collapse: collapse;' border='1' bgcolor='white' bordercolor='#CCCCCC'>
<thead>
<tr class='etiquetaTabla'>
<th rowspan='2' width='300px'>AMBIENTE</th>
<th colspan='15'>LUNES</th>
<th colspan='15'>MARTES</th>
<th colspan='15'>MIÉRCOLES</th>
<th colspan='15'>JUEVES</th>
<th colspan='15'>VIERNES</th>
<th colspan='15'>SÁBADO</th>
<tr class='etiquetaTabla'>
</tr>

</thead>
<tbody>
<%
dim texto1

strAmbiente = ""
'if Not(rsAmbiente.BOF and rsAmbiente.EOF) then
if (rsAmbiente.recordcount > 0) then
	Do while not rsAmbiente.EOF
		for c=0 to 90	
		    if c=0 then
			    'response.write "<td width='300px' class='etiquetaTabla' style='text-align:left'>" & "[" & rsAmbiente("ambiente_real") & "]</td>"  
				texto1 = texto1 & "<td width='300px' class='etiquetaTabla' style='text-align:left'>" & "[" & rsAmbiente("ambiente_real") & "]</td>" 
            else
				if c=1 then dia="LU"
				if c=16 then dia="MA"
				if c=31 then dia="MI"
				if c=46 then dia="JU"
				if c=61 then dia="VI"
				if c=76 then dia="SA"				
			end if
		next
		
		rsAmbiente.movenext
	Loop
	response.write  texto1
else
	response.write "<tr height='30px'><td></td><td colspan='91' style='text-align:left' class='usattitulousat'>No se han encontrado horarios registrados en la base de datos</td></tr>"
end if
	
rsAmbiente.close
Set rsAmbiente=nothing
'if(Err.number <> 0) then
    Response.Write Err.Description
'end if

%>
</tbody>
</table>
</body>
</html>
