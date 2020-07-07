<%
codigo_alu=request.querystring("codigo_alu")
codigouniver_alu=request.querystring("codigouniver_alu")

codigo_alu=74988
codigouniver_alu="181AD78489"

if codigo_alu<>"" then
	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
		codigofoto=obEnc.CodificaWeb("069" & codigouniver_alu)
	set obEnc=Nothing

	'Cargar datos para cambiarlos
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsDatos=Obj.Consultar("ConsultarAlumno","FO","CO",codigo_alu)
		Set rsAlumno=Obj.Consultar("ConsultarAlumno","FO","RG",codigo_alu)
	Obj.CerrarConexion
	Set Obj=nothing
	
	if not(rsDatos.BOF and rsDatos.EOF) then
		alumno=rsDatos("alumno")
		nombre_cpf=rsDatos("nombre_cpf")
		cicloIng_alu=rsDatos("cicloIng_alu")
		descripcion_pes=rsDatos("descripcion_pes")
		nombre_min=rsDatos("nombre_min")
		password_alu=rsDatos("password_alu")
		estadoDeu_alu=rsDatos("estadoDeuda_alu")
		if estadoDeu_alu=0 then
			estadoDeu_alu="S�"
		else
			estadoDeu_alu="No"
		end if
	end if
	
	fichaPersonal=false
	if not(rsAlumno.BOF and rsAlumno.EOF) then
			fichapersonal=true
		    'Campos de la tbl ALUMNO
			fechaNacimiento_alu=rsAlumno("fechaNacimiento_alu")
            sexo_alu=rsAlumno("sexo_alu")
            if sexo_alu="F" then
            	sexo_alu="FEMENINO"
            else
				sexo_alu="MASCULINO"            
            end if
            tipodocident_alu=rsAlumno("tipodocident_alu")
            nrodocident_alu=rsAlumno("nrodocident_alu")
            email_alu=rsAlumno("email_alu")
            email2_alu=rsAlumno("email2_alu")
            
            'Campos de la tbl DATOSALUMNO
            fecharegistro_Dal=rsAlumno("fecharegistro_Dal")
            '-----Datos de la familia/contacto-----------
            PersonaFam_Dal=rsAlumno("PersonaFam_Dal")
            direccionfam_dal=rsAlumno("direccionfam_dal")
            urbanizacionfam_dal=rsAlumno("urbanizacionfam_dal")
            nombreDisFam_Dal=rsAlumno("nombreDisFam_Dal")
            nombreProFam_Dal=rsAlumno("nombreProFam_Dal")
            nombreDepFam_Dal=rsAlumno("nombreDepFam_Dal")
            telefonofam_dal=rsAlumno("telefonofam_dal")
            
            '------Datos d�nde reside el alumno----------
			direccion_dal=rsAlumno("direccion_dal")
            urbanizacion_dal=rsAlumno("urbanizacion_dal")
            distrito_dal=rsAlumno("distrito_dal")
            nombreDis_Dal=rsAlumno("nombreDis_Dal")
            codigoPro_Dal=rsAlumno("codigoPro_Dal")
            nombrePro_Dal=rsAlumno("nombrePro_Dal")
            codigoDep_Dal=rsAlumno("codigoDep_Dal")
            nombreDep_Dal=rsAlumno("nombreDep_Dal")
            religion_dal=rsAlumno("religion_dal")
            ultimosacramento_dal=rsAlumno("ultimosacramento_dal")
            estadocivil_Dal=rsAlumno("estadocivil_Dal")
			telefonoCasa_Dal=rsAlumno("telefonoCasa_Dal")
            telefonoMovil_Dal=rsAlumno("telefonoMovil_Dal")
            telefonoTrabajo_Dal=rsAlumno("telefonoTrabajo_Dal")
            centrotrabajo_dal=rsAlumno("centrotrabajo_dal")

            if telefonoTrabajo_Dal<>"" then
            	telefonoTrabajo_Dal="&nbsp;( Tel�f." & telefonoTrabajo_Dal & ")"
	    end if
            
            '------Datos del Colegio---------------------
			if IsNull(rsAlumno("codigo_col"))=true then
			    nombrecolegio_dal=rsAlumno("nombrecolegio_dal")	& "(OTRO COLEGIO)"
            else
            	nombrecolegio_dal=rsAlumno("nombre_col")
                nombreDis_Col=rsAlumno("nombreDis_Col")
                nombrePro_Col=rsAlumno("nombrePro_Col")
                nombreDep_Col=rsAlumno("nombreDep_Col")
                nombrePai_Col=rsAlumno("nombrePai_Col")
                nombrecolegio_dal=nombrecolegio_Dal & " (" & ucase(nombreDis_col) & "/" & ucase(nombrePro_col) & "/" & ucase(nombreDep_Col) & ")"
			end if
			
			tipocolegio_dal=rsAlumno("tipocolegio_dal")
			anioegresosec_dal=rsAlumno("a�oegresosec_dal")

            fechaMod_Dal=rsAlumno("fechaMod_Dal")
            OperadorMod_Dal=rsAlumno("OperadorMod_Dal")
            OperadorReg_Dal=rsAlumno("OperadorReg_Dal")
            if Isnull(fechaMod_Dal) then
            	fechaReg=fechaRegistro_Dal
            else
            	fechaReg=fechaMod_Dal
            end if
	end if
	set rsAlumno=nothing
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title></title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<style type="text/css">
.style1 {
	font-weight: normal;
}
</style>
</head>
<body>
<table style="width: 100%">
	<tr>
		<td>
		<img alt="USAT" src="../../../images/logorpte.jpg"></td>
		<td>&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
<h2 align="center">Ficha de Datos</h2>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante">
  <tr>
    <td width="17%" valign="top">
    <!--
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
    -->
        <img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="100" height="118" alt="Sin Foto"></td>
    <td width="83%" valign="top">
        <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
          <tr>
    <td width="90%" colspan="4" class="usatCeldaTitulo" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1"><%=alumno%></td>
          </tr>
          <tr>
    <td width="18%">C�digo Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=codigouniver_alu%></td>
          </tr>
          <tr>
    <td width="18%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=alumno%></td>
          </tr>
          <tr>
    <td width="18%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="46%" colspan="3">: <%=nombre_cpf%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="18%">Ciclo de Ingreso</td>
	    	<td class="usatsubtitulousuario" width="20%">: <%=cicloIng_alu%>&nbsp;</td>
	    	<td width="9%" align="right">Modalidad</td>
	    	<td class="usatsubtitulousuario" width="28%">: <%=nombre_min%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="18%">Plan de Estudio</td>
	    	<td class="usatsubtitulousuario" width="46%" colspan="3">: <%=descripcion_pes%>&nbsp;</td>
          </tr>
          <%if session("codigo_tfu")="1" or session("codigo_tfu")="30" then%>
          <tr>
	    	<td width="18%">Password</td>
	    	<td class="usatsubtitulousuario" width="46%" colspan="3">: <%=password_alu%></td>
          </tr>
          <%end if%>
          <tr>
	    	<td width="18%">Habilitado</td>
	    	<td class="usatsubtitulousuario" width="46%" colspan="3">: <%=estadoDeu_alu%></td>
          </tr>
          </table>
    </td>
  </tr>
  </table>  
  <%if fichapersonal=true then%>          
        <br>
        <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF" id="AutoNumber2">
          <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1">DATOS PERSONALES</td>
          </tr>
          <tr>
	    	<td width="10%">Sexo</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=sexo_alu%>&nbsp;</td>
	    	<td width="17%" align="right">Fecha Nac.</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=fechanacimiento_alu%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Doc. Identidad</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=tipodocident_alu%>.&nbsp;<%=nrodocident_alu%>&nbsp;</td>
	    	<td width="17%" align="right">Estado Civil</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=estadocivil_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">E-mail principal</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=email_alu%>&nbsp;</td>
	    	<td width="17%" align="right">E-mail alternativo</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=email2_alu%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Direcci�n</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=direccion_dal%>.&nbsp;<%=urbanizacion_dal%> (<%=UCASE(nombreDis_dal)%>/<%=UCASE(nombrePro_Dal)%>)&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Tel�fono Casa</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=telefonoCasa_dal%>&nbsp;</td>
	    	<td width="17%" align="right">Tel�fono M�vil</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=telefonoMovil_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Centro de Trabajo</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=centrotrabajo_dal & TelefonoTrabajo_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Religi�n</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=religion_dal%>&nbsp;</td>
	    	<td width="17%" align="right">�ltimo Sacramento</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=ultimosacramento_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">DATOS ACAD�MICOS</td>
	    	</tr>
          <tr>
	    	<td width="10%">Tipo de Colegio</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=tipocolegio_dal%>&nbsp;</td>
	    	<td width="17%" align="right">A�o de Egresado</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=anioegresosec_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Colegio</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=nombrecolegio_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">&nbsp;</td>
	    	<td  width="50%" colspan="3">&nbsp;</td>
          </tr>
          <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">DATOS DE PERSONA DE CONTACTO</td>
	    	</tr>
          <tr>
	    	<td width="10%">Contacto</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=PersonaFam_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Direcci�n</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=direccionfam_dal%>.&nbsp;<%=urbanizacionfam_dal%>(<%=ucase(nombreDisFam_dal)%>/<%=ucase(nombreProFam_dal)%>/<%=ucase(nombreDepFam_dal)%>)&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Tel�fono</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=telefonofam_dal%>&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          </table>
	<p class="rojo" align="right">Fecha de Actualizaci�n: <%=fechareg%></p>
	<%end if%>
<h5><strong>Observaciones:</strong></h5>
<table style="width: 100%;height:15%">
	<tr>
		<td class="bordesup">&nbsp;</td>
	</tr>
	<tr>
		<td class="bordesup">&nbsp;</td>
	</tr>
	<tr>
		<td class="bordesup">&nbsp;</td>
	</tr>
</table>
<h5 align="center">&nbsp;</h5>
<h5 align="center">
<span class="bordesup"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
FIRMA DEL ESTUDIANTE&nbsp;&nbsp; &nbsp;&nbsp;
</b>
</span>
</h5>
</body>
</html>
<%end if%>