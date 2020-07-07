<!--#include file="../../../../funciones.asp"-->
<%
criterio=request.querystring("criterio")
modo=request.querystring("modo")

if (modo="R" and criterio<>"") then
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsCursos=Obj.Consultar("ConsultarCurso","FO","DE",ReemplazarTildes(criterio),0)
		Obj.CerrarConexion
	Set obj=nothing
	
	if Not(rsCursos.BOF and rsCursos.EOF) then
		alto="95%"
		estado="R"
	end if
end if
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Administrar matrícula del estudiante</title>
<link rel="stylesheet" type="text/css" href="../../../../private/estilo.css">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<script>
function MostrarDetalleCurso(pagina)
{
	if (txtelegido.value!=""){
		var fila=document.getElementById(txtelegido.value)
		if (fila!=undefined){
			var cu=fila.cells[1].innerText
			fradetalle.location.href=pagina + "?accion=modificarcurso&codigo_cur=" + cu
		}
	}
}
</script>

</head>
<body>
<table border="0" cellpadding="2" cellspacing="0" style="border-collapse: collapse;" bordercolor="#111111" width="100%" height="<%=alto%>">
    <tr>
      <td width="100%" class="usattitulo" height="5%" colspan="3" valign="top">
      Modificar datos de asignaturas</td>
    </tr>
    <tr>
      <td width="30%" height="5%" valign="top">Especifique el nombre</td>
      <td width="50%" height="5%" valign="top">
      <input type="text" name="txtcriterio" id="txtcriterio" size="20" class="cajas2" value="<%=criterio%>"></td>
      <td width="20%" height="5%" valign="top"><img class="NoImprimir" style="cursor:hand" src="../../../../images/buscar.gif" onclick="actualizarlista('frmmodificar.asp?modo=R&criterio=' + txtcriterio.value)"></td>
    </tr>
    <%if estado="R" then%>    
    <tr>
        <td width="100%" height="50%" valign="top" colspan="3">
        <%
        	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio,pagina

			ArrEncabezados=Array("ID","Código","Asignatura","Departamento")
			ArrCampos=Array("codigo_cur","identificador_cur","nombre_cur","nombre_dac")
			ArrCeldas=Array("5%","10%","30%","25%")
			ArrCamposEnvio=Array("codigo_cur")

			pagina="frmcurso.asp?accion=modificarcurso"

			call CrearRpteTabla(ArrEncabezados,rscursos,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")
        %>
        </td>
        </tr>
	 	<tr>
  		<td width="100%" height="40%" colspan="3">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr>
					<td class="pestanaresaltada" id="tab" align="center" width="20%" height="10%" onclick="ResaltarPestana2('0','','');MostrarDetalleCurso('frmcurso.asp')">
                    Datos Informativos</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<td class="pestanabloqueada" id="tab" align="center" width="20%" height="10%" onclick="ResaltarPestana2('1','','');MostrarDetalleCurso('../../cargalectiva/frmcambiardptoplancurso.aspx')">
                    Planes de Estudio</td>
					<td width="65%" height="10%" class="bordeinf">&nbsp;</td>
				</tr>
	  			<tr>
		    	<td width="100%" height="90%" valign="top" colspan="4" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija 
                    la asignatura, para visualizar sus datos</span>
					<iframe id="fradetalle" src="frmcurso.asp" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  		</tr>			  
	<%elseif modo="R" then%>
		<tr><td width="100%" height="5%" colspan="3" class="usatsugerencia">&nbsp;&nbsp;&nbsp; 
          No se han registrado asignaturas con el criterio especificado</td></tr>
	<%end if%>
</table>
</body>
</html>