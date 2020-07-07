<!--#include file="../../../funciones.asp"-->
<% 
if session("codigo_tfu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

dim param

param=request.querystring("param")
resultado=request.querystring("resultado")
modulo=request.querystring("mod")

if (resultado="S" and trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion		
		Set rsAlumno= obj.Consultar("EVE_ConsultarAlumnosPorModulo_v2","FO",modulo,param)		
	obj.CerrarConexion
	set obj= Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","C�digo","Apellidos y Nombres","Escuela Profesional","Estado Actual","Tiene Deuda")
	ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","estadoactual","estadodeuda")
	ArrCeldas=Array("5%","15%","35%","20%","10%","10%")
	ArrCamposEnvio=Array("codigo_alu","codigouniver_alu")
	pagina="misdatos.asp?tipo=" & tipoResp

	if Not(rsAlumno.BOF and rsAlumno.EOF) then
		alto="height=""98%"""
		resultado="S"
	else
		resultado="N"
	end if
end if
%>
<html>
<head>
<title>Buscar Responsable de Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
<link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
    var AnteriorFila = 0;
    var UltimaFila = 0;
    
    $(document).ready(function() {
        $("#txtParam").focus();
        $('#txtParam').bind('keypress', function(e) {
            if (e.keyCode == 13) {
                BuscarAlumno('<%=modulo%>')          
            }
        });
        $("#cmdBuscar").click(function() {
            BuscarAlumno('<%=modulo%>')
        });
    });

    function BuscarAlumno(modulo) {
        if ($("#txtParam").val() == "") {
            alert("Especifique el par�metro de b�squeda");
            $("#txtParam").focus();
        } else {
            pagina = "lstalumnos.asp?resultado=S&param=" + $("#txtParam").val() + "&mod=" + modulo        
            $(location).attr('href', pagina)
        }
    }

    function Resaltarfila(tbl, filaseleccionada) {
        var ArrFilas = tbl.getElementsByTagName('tr')
        for (var i = 0; i < ArrFilas.length; i++) {
            var filaactual = ArrFilas[i]
            if (filaseleccionada == filaactual.id) {
                filaactual.className = "Selected"
            }
            else {
                filaactual.className = "SelOff"
            }
        }
    }
 
    function ResaltarfilaDetalle(tbl, filaseleccionada, pagina) {
        SeleccionarFila();        
        if (pagina != "" || pagina != undefined) {
            $("#txtelegido").val($(filaseleccionada).attr("id"));
            $("#mensajedetalle").css("display", "none");              
            $("#fradetalle").attr("href", pagina);
        }
    }

    function SeleccionarFila() {
        alert(window.event.srcElement);
        if (document.all)
            oRow = window.event.srcElement.parentElement;
        else
            oRow = event.srcElement.parentElement;

        if (oRow.tagName == "TR") {
            AnteriorFila.Typ = "Sel";
            AnteriorFila.className = AnteriorFila.Typ + "Off";
            AnteriorFila = oRow;
        }
        if (oRow.Typ == "Sel") {
            oRow.Typ = "Selected";
            oRow.className = oRow.Typ;
        }
    }

    function Resaltar(op, fila, mostrarhand, colorfila) {
        if (colorfila == "" || colorfila == undefined) {
            colorfila = "#FFFFFF"
        }

        if (mostrarhand == "S") {
            fila.style.cursor = "hand"
        }
        if (op == 1)
        { fila.bgColor = "#FBF5D2" }
        else
        { fila.bgColor = colorfila }
    }

    function ResaltarPestana2(numcol, clsResaltado, pagina) {
        if (clsResaltado == "" || clsResaltado == undefined) {
            clsResaltado = "pestanaresaltada"
        }
        for (var c = 0; c < tab.length; c++) {
            var Celda = tab[c]
            if (numcol == c)
            { Celda.className = clsResaltado }
            else
            { Celda.className = "pestanabloqueada" }
        }

        if (pagina != "") {
            fracontenedor.location.href = pagina
        }
    }

</script>
</head>

<body>
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="4" valign="top" width="100%"><span class="usatTitulo">B�squeda de estudiantes de Pregrado</span>
	</td>
    </tr>
    <tr> 
      <td width="25%" height="5%"> Apellidos y Nombres/DNI/Cod. Univ.:</td>      
      <td valign="top" width="40%" height="5%">
        <input name="txtParam" type="text" id="txtParam" style="width:100%"/>
      </td>
      <td valign="top" width="30%" height="5%">
        <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" />
      </td>
    </tr>
    <%if resultado="S" then%>
    <tr id="trLista">
  		<td width="100%" height="35%" colspan="4">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsAlumno,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")   
  		%>
  		</td>
	</tr>
	<tr id="trDetalle" >
  		<td width="100%" height="65%" colspan="4">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('0','','');AbrirFicha('misdatos.asp',this)">
                    Datos del Estudiante</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<% if session("codigo_tfu")<>13 AND session("codigo_tfu")<>2  then %>
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('1','','');AbrirFicha('../../../librerianet/academico/historial_personal.aspx',this,1,'<%=session("codigo_tfu")%>')">
                    Historial Acad�mico</td>
                    <% end if%>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('2','','');AbrirFicha('../../../librerianet/academico/admincuentaper.aspx',this,'1')">
                    Estado de Cuenta</td>
            <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('3','','');AbrirFicha('detallematricula.asp',this)">
                    Mov. Acad�micos</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="13%" onclick="ResaltarPestana2('4','','');AbrirFicha('horario.asp',this)">
                    Horarios</td>
                    <td width="10%" height="10%" class="bordeinf" align="right">
		   <img border="0" src="../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
		    <%if session("codigo_tfu")=1 or session("codigo_tfu")=16 or session("codigo_tfu")=33 or session("codigo_tfu")=7 then%>
                    <img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatos.asp',this)">
		    <%end if%>
                    <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
                    </td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el estudiante que desea verificar sus datos</span>
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  	</tr>
	<%end if%>
  </table>
</body>
</html>