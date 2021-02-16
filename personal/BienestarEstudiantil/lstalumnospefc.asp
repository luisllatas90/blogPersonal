<!--#include file="../../funciones.asp"-->
<% 
if session("codigo_tfu") = "" then
    Response.Redirect("../sinacceso.html")
end if

dim param

param=request.querystring("param")
resultado=request.querystring("resultado")
modulo=request.querystring("mod")

if (resultado="S" and trim(param)<>"") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		'Set rsAlumno= obj.Consultar("EVE_ConsultarAlumnosPorModulo","FO",tipoBus,modulo,param)
		Set rsAlumno= obj.Consultar("EVE_ConsultarAlumnosPorModulo_v2","FO",modulo,param)		
	obj.CerrarConexion
	set obj= Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","Código","Apellidos y Nombres","Escuela Profesional","Estado Actual","Tiene Deuda")
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
<link rel="stylesheet" type="text/css" href="../../private/estilo.css" />
<!--<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script> -->
<script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
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
            alert("Especifique el parámetro de búsqueda");
            $("#txtParam").focus();
        } else {
            pagina = "lstalumnospefc.asp?resultado=S&param=" + $("#txtParam").val() + "&mod=" + modulo
            $(location).attr('href', pagina)
        }
    }

    function AbrirFicha(pagina, obj, t, ctf) {
        var fila = document.getElementById(txtelegido.value)
        nombreficha = obj.innerText
        var codigo_alu = fila.cells[1].innerText
        var codigouniver_alu = fila.cells[2].innerText
        var alumno = fila.cells[3].innerText
        var nombre_cpf = fila.cells[4].innerText

        if (t == "1") {
            $("#fradetalle").attr("src", pagina + "?id=" + codigo_alu + "&ctf=" + ctf);
        }
        else {
            $("#fradetalle").attr("src", pagina + "?IncluirDatos=N&actualizoDatos=true&codigo_alu=" + codigo_alu + "&codigouniver_alu=" + codigouniver_alu + "&tipo=E&alumno=" + alumno + "&nombre_cpf=" + nombre_cpf);
        }
    }

    function ResaltarfilaDetalle(tbl, filaseleccionada, pagina) {
        if (pagina != "" || pagina != undefined) {
            $("#txtelegido").val($(filaseleccionada).attr("id"));
            $("#mensajedetalle").css("display", "none");
            $("#fradetalle").attr("src", pagina);
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

    function ResaltarPestana(numcol, colorfila, pagina) {
        if (colorfila == "" || colorfila == undefined) {
            colorfila = "#EEEEEE"
        }
        for (var c = 0; c < tab.length; c++) {
            var Celda = tab[c]
            if (numcol == c) {
                Celda.style.backgroundColor = colorfila
                Celda.style.color = "blue"
            }
            else {
                Celda.style.backgroundColor = "#C0C0C0"
                Celda.style.color = "#808080"
            }
        }

        if (pagina != "") {
            fracontenedor.location.href = pagina
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

    var nombreficha = ""
    
    function AbrirFichaPopUp(pagina, obj) {
        var fila = document.getElementById(txtelegido.value)
        nombreficha = obj.innerText
        var codigo_alu = fila.cells[1].innerText
        var codigouniver_alu = fila.cells[2].innerText
        var alumno = fila.cells[3].innerText
        var nombre_cpf = fila.cells[4].innerText

        //AbrirPopUp(pagina + "?IncluirDatos=A&actualizoDatos=true&codigo_alu=" + codigo_alu + "&codigouniver_alu=" + codigouniver_alu + "&tipo=E&alumno=" + alumno + "&nombre_cpf=" + nombre_cpf, "450", "750")
        AbrirPopUp(pagina + "&alu=" + codigo_alu, "450", "750")
    }

    function ImprimirFicha() {
        fradetalle.document.title = nombreficha
        fradetalle.focus();
        fradetalle.print();
    }

    function AbrirPopUp(pagina, alto, ancho, ajustable, bestado, barras, variable) {
        var izq = (screen.width - ancho) / 2
        var arriba = (screen.height - alto) / 2
        if (variable == "" || variable == undefined)
        { variable = "popup" }

        //var ventana = window.open(pagina, variable, "height=" + alto + ",width=" + ancho + ",statusbar=" + bestado + ",scrollbars=" + barras + ",top=" + arriba + ",left=" + izq + ",resizable=" + ajustable + ",toolbar=no,menubar=no");
        var ventana = window.open(pagina, variable, "height=" + alto + ",width=" + ancho + ",top=" + arriba + ",left=" + izq + ",scrollbars=yes");
        ventana.location.href = pagina
        //alert (izq + "-" + arriba)
        ventana = null
    } 
</script>
</head>

<body>
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse; border-color:#111111;">
    <tr> 
      <td height="5%" colspan="4" valign="top" class="usatTitulo" width="100%">
      <%
      select case modulo
      	case 1: response.write "Búsqueda de estudiantes de Escuela Pre-Universitaria"
      	case 2: response.write "Búsqueda de estudiantes de PreGrado"      	
		case 3: response.write "Búsqueda de estudiantes de Programas de Profesionalización"
		case 4: response.write "Búsqueda de estudiantes de Complementarios/Idiomas"
		case 5: response.write "Búsqueda de estudiantes de la Escuela de Posgrado" '<< Modificado por fatima.vasquez		
		case 6: response.write "Búsqueda de estudiantes de Educación Contínua"
		case 7: response.write "Búsqueda de estudiantes de Titulación"
	  end select		      
      %>
      
      </td>
    </tr>
    <tr>       
      <td  width="25%" height="5%">Apellidos y Nombres/DNI/Cód. Univ.:</td>
      <td valign="top" width="28%" height="5%">
        <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2">
      </td>
      <td valign="top" width="30%" height="5%">
        <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar">
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
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('1','','');AbrirFicha('frmhistorial.aspx',this,1,'<%=session("codigo_tfu")%>')">
                    Historial Académico</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                    
			<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('2','','');AbrirFicha('../librerianet/academico/admincuentaper.aspx',this,'1')">
	                    Estado de Cuenta</td>
        	        
                    	<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
   			<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('3','','');AbrirFicha('detallematricula.asp',this)">
                    Mov. Académicos</td>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="13%" onclick="ResaltarPestana2('4','','');AbrirFicha('horario.asp',this)">
                    Horarios</td>
                    
            <td class="pestanabloqueada" id="Td1" align="center" width="18%" onclick="ResaltarPestana2('2','','');AbrirFicha('detallematriculafc.asp',this)">
	                    Mov. Académicos F.C.</td>
        	        
                    	<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                   <td width="10%" height="10%" class="bordeinf" align="right">
		   			<img border="0" src="../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
                    <!--<img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatospe.asp',this)">-->
                    <img border="0" src="../images/editar.gif" style="cursor:hand" alt="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('../../administrativo/gestion_educativa/frmActualizarDatosPostulante.aspx?nv=1&accion=M&mod=<%=modulo%>&ctf=<%=session("codigo_tfu")%>&id=<%=session("codigo_usu")%>&cco=<%=0%>',this)" />
		            <img border="0" src="../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
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