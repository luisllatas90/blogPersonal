<!--#include file="../../../funciones.asp"-->
<%
if session("codigo_tfu") = "" then
    Response.Redirect("../../../sinacceso.html")
end if

codigo_tfu=session("codigo_tfu")
codigo_usu=session("codigo_usu")

variable = time() 'Yperez 10.01.18 variable tiempo para refrescar caché del servidor en URL de descarga del silabo

codigo_cac=request.querystring("codigo_cac")
codigo_cpf=request.querystring("codigo_cpf")
descripcion_cac=request.querystring("descripcion_cac")
modulo=request.querystring("mod")

if codigo_cac="" then codigo_cac=session("codigo_cac")
if codigo_cac="" then codigo_cac="-2"
if codigo_cpf="" then codigo_cpf="-2"

Set obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
	
	Set rsCiclo= Obj.Consultar("ConsultarCicloAcademico","FO","TO",0)
	Set rsEscuela= obj.Consultar("EVE_ConsultarCarreraProfesional","FO",modulo,codigo_tfu,codigo_usu)

	if codigo_cac<>"-2" and codigo_cpf<>"-2" then
		Set rsCursoPlan= Obj.Consultar("ConsultarCursoProgramado","FO",8,codigo_cpf,codigo_cac,0,0)
		set rsCronograma = Obj.Consultar("ACAD_ConsultarCronogramaxTipo", "FO", "SI", codigo_cac, Request.QueryString("mod"))

		if Not (rsCursoPlan.BOF and rsCursoPlan.EOF) then
			activo=true
			alto="height=""99%"""
		end if
	end if

    obj.CerrarConexion
Set obj=nothing
%>
<!DOCTYPE html>
<html lang="es">
<head>
   <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Administrar s&iacute;labos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
  <!--  <meta http-equiv="Content-Type" content="text/html; charset=windows-1252" >-->



    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css'/>
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    

    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>
    <script type="text/javascript" src='../../assets/js/materialize.js'></script>  




<!-- -->
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
    <script language="JavaScript" src="../../../private/funciones.js"></script>
    <script language="JavaScript" src="private/validarsilabosAdministrador.js"></script>
    
    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
        
        
       .form-control2
        {             
        display: block;
        width: 100%;
       /* height:34px;*/
        height:24px;
        padding: 2px 4px; 
        font-size: 12px;
        line-height: 1.42857143;
        color: #555;
        background-color: #fff;
        background-image: none;
        border: 1px solid #ccc;
        border-radius: 4px;
        -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
        -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
        -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
      }
    </style>
</head>
<body>
<form id="frm1" class="form form-horizontal">
 <div class="container-fluid">
        <div class="messagealert" id="alert_container"></div>
         <div class="panel panel-primary" id="pnlLista"  style="padding:0px;">
            
            <div class="panel panel-heading" >
                <h4>Administrador de S&iacute;labos de Cursos Programados</h4>
            </div>
            <div class="panel panel-body"  style="padding:3px;">            
            <div class="row">
            
            
            </div>
            </div>
            
        </div>
        </div>
</form>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" <%=alto%>>
  <tr class="usattitulo">
    <td width="100%" colspan="5" height="5%">Administrador de Sílabos de Cursos 
	Programados</td>
  </tr>
<tr>
	<td height="3%" style="width: 20%">Semestre Académico:</td>
	<td height="3%" style="width: 15%">
	<%call llenarlista("cbocodigo_cac","",rsCiclo,"codigo_cac","descripcion_cac",codigo_cac,"","","")%>			
	</td>
	<td height="3%" style="width: 20%" align="right">Carrera Profesional:</td>
	<td height="3%" style="width: 65%">
	<%call llenarlista("cbocodigo_cpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"Seleccione la Carrera Profesional","","")%>
	</td>
	<td height="3%" style="width: 10%" align="right">
    <img alt="Buscar cursos programados" src="../../../images/menus/buscar_small.gif" class="imagen" onclick="AccionSilabos('C','<%=modulo%>')" width="25" height="24">
	</td>
</tr>
  <tr>
	<td height="2%" colspan="5"> 
	<% 
	  if codigo_cac<>"-2" and codigo_cpf<>"-2" then
	    if  Not (rsCronograma.BOF and rsCronograma.EOF) then
	        response.Write ("<font color='blue'>Fechas definidas para subir sílabos: </font><font color='red'>" & rsCronograma("fechaini_cro") & " - " & rsCronograma("fechafin_cro") & " </font>")
	    else 
	        response.Write ("")
	    end if 
	  end if 
	%>
	</td>
  </tr>
  <%if activo=true then%>
  <tr>
    <td width="100%" colspan="5" height="50%">
    <table border="1" cellpadding="2" cellspacing="0" style="border-collapse: collapse" bordercolor="#808080" width="100%"  <%=alto%> id="tblcursoprogramado">
      <tr class="etabla">
        <td width="5%" height="3%">Ciclo</td>
        <td width="5%" height="3%">Tipo</td>
        <td width="10%" height="3%">Código</td>
        <td width="28%" height="3%">Nombre del Curso</td>
        <td width="5%" height="3%">Créditos</td>
        <td width="5%" height="3%">TH</td>
        <td width="5%" height="3%">Grupo Horario</td>
        <td width="20%" height="3%">Docente</td>
        <td width="10%" height="3%">Sílabos</td>
      </tr>
      <tr>
        <td width="100%" colspan="9">
        <div id="listadiv" style="height:100%" class="NoImprimir">
		<table width="100%" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#ccccccc">
		<%	i=0:n=0:p=0
			Ciclo=1
			Do while not rsCursoPlan.eof
				i=i+1
				if Isnull(rsCursoPlan("fechasilabo_cup"))=true then
					estado=false
					n=n+1
				else
					estado=true
					p=p+1
				end if
				bordeciclo=Agrupar(rsCursoPlan("ciclo_cur"),Ciclo)				
		%>
			<tr class="<%=iif(estado=false,"rojo","azul")%>" height="20px" id="fila<%=i%>" onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td <%=bordeciclo%> align="center" width="5%"><%=ConvRomano(rsCursoPlan("ciclo_Cur"))%>&nbsp;</td>
			<td <%=bordeciclo%> width="5%"><%=rsCursoPlan("tipo_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="12%"><%=rsCursoPlan("identificador_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> width="28%"><%=rsCursoPlan("nombre_Cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("creditos_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="5%"><%=rsCursoPlan("totalhoras_cur")%>&nbsp;</td>
			<td <%=bordeciclo%> align="center" width="8%"><%=rsCursoPlan("grupohor_cup")%>&nbsp;</td>
			<td <%=bordeciclo%> width="20%"><span style="font-size: 7pt"><%=ConvertirTitulo(rsCursoPlan("profesor_cup"))%>&nbsp;</span></td>
			<td <%=bordeciclo%> align="center" width="10%">
			<%if estado=false then%>
				<img src="../../../images/agregar.gif" ALT="Agregar Sílabos" class="imagen" onClick="AccionSilabos('A','<%=descripcion_cac%>','<%=codigo_cpf%>','<%=rsCursoPlan("codigo_cup")%>')">
			<%else%>
				<a href="../../../silabos/<%=descripcion_cac%>/<%=rsCursoPlan("codigo_cup")%>.zip<%="?x=" & variable%>"><img src="../../../images/previo.gif" ALT="Ver Silabus" border=0></a>
				<img src="../../../images/eliminar.gif" alt="Borrar Silabus" class="imagen" border=0 onClick="AccionSilabos('E','<%=codigo_cac%>','<%=codigo_cpf%>','<%=rsCursoPlan("codigo_cup")%>','<%=descripcion_cac%>', '<%=modulo%>')">
			<%end if%>
			</td>		
			</tr>
				<%rsCursoPlan.movenext
			loop
			set rsCursoPlan=nothing
		%>
		</table>
		</div>
	    </td>
      </tr>
      <tr>
    	<td width="100%" colspan="9" height="5%" bgcolor="#E6E6FA" align="right"><span class="azul">
		&nbsp;&nbsp;&nbsp;&nbsp; Sílabos registrados: <%=p%></span> | <span class=rojo><b>Sílabos No 
		registrados: <%=n%></b></span></td>
	  </tr>
      </table>
  </td>
  </tr>
  <%end if%>   
</table>
</body>
</html>