<!--#include file="../NoCache.asp"-->
<!--#include file="../funciones.asp"-->
<%
on error resume next        
    Dim codigo_alu
    codigo_alu = session("codigo_Usu")
	Set objCursos=Server.CreateObject("PryUSAT.clsAccesoDatos")
	objCursos.AbrirConexion
	Set rsCursos = objCursos.Consultar("ConsultarCursosEncuestaEstudiante","FO",session("codigo_alu"),session("codigo_cac"))
	objCursos.CerrarConexion
	Set objCursos = nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Encuesta de Evaluación Docente</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script type="text/javascript">
    function frameE(i) {
        var x = "";
        var y = "";
        var valor = "";
            
        x = document.getElementById('x' + i).value;
        y = document.getElementById('y' + i).value;
         valor = y + "" + x;
        document.getElementById('frameEncuesta').src = "../librerianet/encuesta/EvaluacionAlumnoDocente/EvaluacionDocente_Estudiante.aspx?ky=" + valor.toString();
        
        
  }
</script>
</head>
<body>
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 0px solid #C0C0C0; " bordercolor="#111111" width="100%">
  <tr>
    <td width="95%" height="3%" class="usatTitulo">Encuesta de Evaluación a Docentes</td>            
  </tr>
  <tr>
    <td width="95%" height="3%" >Estimado estudiante, le indicamos que debe responder a la encuesta antes de ingresar al campus virtual</td>    
  </tr>
  </table>
  <br />
  <table cellpadding="2" cellspacing="0" style="border-collapse: collapse; border: 1px solid #C0C0C0; " bordercolor="#111111">
  <tr class="usatceldatitulo" style="border-botton:1px solid C0C0C0; ">
  <td width="15%">Curso</td>
  <td width="15%">Estado de la Encuesta</td> 
  </tr>
  
  <%    
    'Set ObjCif = Server.CreateObject("PryCifradoNet.ClscifradoNet")
	'id = ObjCif.cifrado(trim(session("Ident_Usu") & codigo_alu), "EncuestaEstudiante")
	i = 0
    Do while not rsCursos.eof
       i=i+1
       response.Write("<tr>")
	   response.write("<td width='15%' height='20'>"&session("descripcion_Cac")& " - "& rsCursos("nombre_cur")& " - Ciclo " &ConvRomano(rsCursos("ciclo_cur"))& " - Grupo " &rsCursos("grupohor_cup")&"</td>")
	   
	    Set objCnx=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Set rsdatoscurso =  server.CreateObject("ADODB.Recordset")
		objCnx.AbrirConexion
		Set rsdatoscurso=ObjCnx.Consultar("VerificarEncuestaEstudiante","FO", "E",cdbl(codigo_alu),cdbl(rsCursos("codigo_cup")),session("Codigo_Cac"))
		objCnx.CerrarConexion	   
	   
	    'If Not(rsdatoscurso.BOF and rsdatoscurso.EOF) then
	    If rsdatoscurso.recordcount > 0 then
	        response.write("<td width='15%' height='20'>Realizada</td>")
	    else
	       'cup = ObjCif.cifrado(trim(session("Ident_Usu") & rsCursos("codigo_cup")), "EncuestaEstudiante")        	        
	       '*
'	       id = codigo_alu
           hora = cstr(replace( replace(replace(replace(NOW,"/",""),":","") ," ",""),".",""))
           hora = (StrReverse(hora)).toString
           cup = (rsCursos("codigo_cup"))
           
	       '* 
	        response.Write("<input id='y"&i&"' type='hidden' value ='"&hora&"'/>")
	        response.Write("<input id='x"&i&"' type='hidden' value ='"&cup&"'/>")
	        response.write("<td width='15%' height='20' style='color:red;'><a onclick='frameE("& i &");' >Responder, clic aquí</a></td>")
	    end if
	    response.Write("</tr>")
	    rsCursos.movenext
	loop
	rsCursos = nothing
	rsdatoscurso = nothing
	
	pagina = "abriraplicacion.asp?codigo_tfu=3&codigo_apl=8&descripcion_apl=Campus Virtual USAT&enlace_apl=" & "principal.asp" & "&estilo_apl=N"
	session("encuesta") = 1	
	
  %>
  </table>
  <br />
  
  <!--<h3><a href="<%=pagina%>">Responder después e Ingresar al Campus Virtual</a></h3>-->
  <h5><font color="red"><b>A partir del Jueves 22/11/2012, la encuesta es OBLIGATORIA.</b></font></h5>
  <iframe id="frameEncuesta" frameborder=0 width="100%" height="100%" scrolling="yes"></iframe>
  
</body>
</html>

