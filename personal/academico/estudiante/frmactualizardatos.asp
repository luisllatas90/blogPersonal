<!--#include file="../../../NoCache.asp"-->
<!--#include file="../../../funciones.asp"-->
<%
'on error resume next
codigo_alu=request.querystring("codigo_alu")
incluirDatos=request.querystring("incluirDatos")

'if codigo_alu="" then codigo_alu=session("codigo_alu")
'codigo_alu=session("codigo_alu")

Function DividirCadena(ByVal cadena,ByVal Caracter,ByRef CadDER)
  Dim Pos
  Pos = InStr(cadena, Caracter)
  If Pos > 0 Then
      DividirCadena = Left(cadena, Pos - 1)
      CadDER = Right(cadena, Len(cadena) - Pos + Len(Caracter))
  Else
      DividirCadena = cadena
  End If
End Function

if incluirDatos="" then incluirDatos="S"

if codigo_alu<>"" then
		
		'Cargar datos para cambiarlos
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
			Set rsAlumno=Obj.Consultar("ConsultarAlumno","FO","RG",codigo_alu)
		Obj.CerrarConexion
		Set Obj=nothing
		
		if not(rsAlumno.BOF and rsAlumno.EOF) then
		    'Campos de la tbl ALUMNO
			fechaNacimiento_alu=rsAlumno("fechaNacimiento_alu")
			dia=day(fechaNacimiento_alu)
			mes=month(fechaNacimiento_alu)
            		anio=year(fechaNacimiento_alu)		
            sexo_alu=rsAlumno("sexo_alu")		
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
            distritoFam_Dal=rsAlumno("distritoFam_Dal")
            nombreDisFam_Dal=rsAlumno("nombreDisFam_Dal")
            codigoProFam_Dal=rsAlumno("codigoProFam_Dal")
            nombreProFam_Dal=rsAlumno("nombreProFam_Dal")
            codigoDepFam_Dal=rsAlumno("codigoDepFam_Dal")
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
            estadocivil_Dal=rsAlumno("estadocivil_Dal")
			telefonoCasa_Dal=rsAlumno("telefonoCasa_Dal")
            telefonoMovil_Dal=rsAlumno("telefonoMovil_Dal")
            telefonoTrabajo_Dal=rsAlumno("telefonoTrabajo_Dal")
            izq=DividirCadena(telefonoTrabajo_Dal,"Ax.",der)
            if der<>"" then
                telefonoTrabajo_Dal=trim(izq)
                tipoanexo="Ax."
                anexo=mid(der,6,len(der))
                anexo=trim(anexo)
            else
                tipoanexo=""
            end if
            'response.write izq & "***" & der & "--" & anexo
            centrotrabajo_dal=rsAlumno("centrotrabajo_dal")
            
            '------Datos del Colegio---------------------
	    if IsNull(rsAlumno("codigo_col"))=true then
		    nombrecolegio_dal=rsAlumno("nombrecolegio_dal")
		    codigo_col=0
		    codigoDis_Col=1
		    codigoPro_Col=1
		    codigoDep_Col=26
		    codigoPai_Col=-2
            else
                codigo_col=rsAlumno("codigo_col")
                codigoDis_Col=rsAlumno("codigoDis_Col")
                nombreDis_Col=rsAlumno("nombreDis_Col")
                codigoPro_Col=rsAlumno("codigoPro_Col")
                nombrePro_Col=rsAlumno("nombrePro_Col")
                codigoDep_Col=rsAlumno("codigoDep_Col")
                nombreDep_Col=rsAlumno("nombreDep_Col")
                codigoPai_Col=rsAlumno("codigoPai_Col")
                nombrePai_Col=rsAlumno("nombrePai_Col")
            end if
			
	    tipocolegio_dal=rsAlumno("tipocolegio_dal")
	    anioegresosec_dal=rsAlumno("a�oegresosec_dal")

            fechaMod_Dal=rsAlumno("fechaMod_Dal")
            OperadorMod_Dal=rsAlumno("OperadorMod_Dal")
            OperadorReg_Dal=rsAlumno("OperadorReg_Dal")
			
		'Sacramentos
		bautismo=iif(rsAlumno("bautismo")=true,"checked","")
		eucaristia=iif(rsAlumno("eucaristia")=true,"checked","")
		confirmacion=iif(rsAlumno("confirmacion")=true,"checked","")
		matrimonio=iif(rsAlumno("matrimonio")=true,"checked","")
		orden=iif(rsAlumno("ordensacerdotal")=true,"checked","")

	if incluirDatos="S" then
	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
		codigofoto=obEnc.CodificaWeb("069" & session("Ident_Usu"))
	set obEnc=Nothing
	end if
			
	if email_alu<>"" then
		lectura="readonly"
	end if

	'sexo_alu="N"
	'estadocivil_Dal="NINGUNO"
	'tipocolegio_dal="NINGUNO"
	'fechaNacimiento_alu="0"
	'tipodocident_alu="DNI"
	'nrodocident_alu=""
	'dia="0"
	'mes="0"
	'anio=""
		
	set rsAlumno=nothing
end if

If Err.Number<>0 then
    session("pagerror")="estudiante/frmactualizardatos.asp"
    session("nroerror")=err.number
    session("descripcionerror")=err.description    
	response.write("<script>top.location.href='../../../error.asp'</script>")
End If
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Actualizaci�n de Datos Personales</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validardatosalumno.js"></script>
</head>
<body bgcolor="#FFF8DC" onload="ResaltarPestana2('0','','')<%=mensaje%>">
<form name="frmFicha" method="post" action="procesardatosalumno.asp?IncluirDatos=<%=incluirDatos%>&codigo_alu=<%=codigo_alu%>">
<input type="hidden" name="txtpasos" value="P">
<%if incluirDatos="S" then%>
<table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
  <tr class="usattitulo">
    <td width="100%" height="13" bgcolor="#DFDBA4" class="contornotabla">Ficha de Actualizaci�n de Datos</td>
  </tr>
  <tr>
    <td width="100%" height="13">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante">
  <tr>
    <td width="15%" valign="top">
        <!--
            '---------------------------------------------------------------------------------------------------------------
            'Fecha: 29.10.2012
            'Usuario: dguevara
            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
            '---------------------------------------------------------------------------------------------------------------

        -->
        <img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="110" height="118" alt="Sin Foto"></td>
    <td width="85%" valign="top">
        <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
          <tr>
    <td width="90%" colspan="4" class="usatCeldaTitulo"><%=session("alumno")%></td>
          </tr>
          <tr>
    <td width="15%">C�digo Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="75%" colspan="3">: <%=session("codigouniver_alu")%></td>
          </tr>
          <tr>
    <td width="15%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="75%" colspan="3">: <%=session("alumno")%></td>
          </tr>
          <tr>
    <td width="15%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="50%" colspan="3">: <%=session("nombre_cpf")%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="15%">Ciclo de Ingreso</td>
	    	<td class="usatsubtitulousuario" width="30%">: <%=session("cicloIng_alu")%>&nbsp;</td>
	    	<td width="6%" align="right">Modalidad</td>
	    	<td class="usatsubtitulousuario" width="25%">: <%=session("nombre_min")%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="15%">Plan de Estudio</td>
	    	<td class="usatsubtitulousuario" width="50%" colspan="3">: <%=session("descripcion_pes")%>&nbsp;</td>
          </tr>
	  <tr>
	    	<td width="15%">Advertencia:</td>
	    	<td class="rojo" width="50%" colspan="3">
		Por favor debe especificar el n�mero de su documento de Identidad. Si no actualiza este dato no podr� hacer uso de los Servicios de biblioteca
		<br>Si no tuviera DNI, realizar los tr�mites respectivos en RENIEC para la obtenci�n del mismo.
		</td>
          </tr>
          </table>
    </td>
  </tr>
  </table>  
    </td>
  </tr>
</table>
<%end if%>
<br>
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
		<td width="100%" height="3%">
			<table cellspacing="0" cellpadding="4" style="border-collapse: collapse" bordercolor="#111111" width="100%" border="0" height="80%">
				<tr align="center">
					<td id="tab" class="bordeinf" height="6%" onclick="EnviarFicha(frmFicha,'P')" width="25%">Datos 
                    Personales</td>
					<td class="bordeinf">&nbsp;</td>
					<td id="tab" class="bordeinf" height="6%" onclick="EnviarFicha(frmFicha,'A')" width="25%">
                    Datos Acad�micos</td>
                    <td class="bordeinf">&nbsp;</td>
					<td id="tab" class="bordeinf" height="6%" onclick="EnviarFicha(frmFicha,'C')" width="25%">
                    Datos del Tutor/Apoderado</td>
					<td width="30%" class="bordeinf" align="right">
                    <font color="#FF0000">(*) Campos obligatorios </font> </td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td valign="top" width="100%" height="88%" colspan="3">
        <table id="tblpersonal" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="pestanarevez" bgcolor="#EEEEEE" height="100%">
          <tr>
	    	<td width="10%">Sexo</td>
	    	<td width="10%"><select size="1" name="sexo_alu" class="cajas">
	        <option value="N" <%=SeleccionarItem("cbo",sexo_alu,"N")%>>--Seleccione--</option>
            <option value="M" <%=SeleccionarItem("cbo",sexo_alu,"M")%>>Masculino</option>
            <option value="F" <%=SeleccionarItem("cbo",sexo_alu,"F")%>>Femenino</option>
            </select></td>
	    	<td width="10%" align="right">Estado Civil</td>
	    	<td colspan=2>
            <select size="1" name="estadocivil_Dal" class="cajas">
	    <option value="NINGUNO" <%=SeleccionarItem("cbo",estadocivil_Dal,"NINGUNO")%>>--Seleccione--</option>
            <option value="SOLTERO" <%=SeleccionarItem("cbo",estadocivil_Dal,"SOLTERO")%>>Soltero</option>
            <option value="CASADO" <%=SeleccionarItem("cbo",estadocivil_Dal,"CASADO")%>>Casado</option>
            <option value="DIVORCIADO" <%=SeleccionarItem("cbo",estadocivil_Dal,"DIVORCIADO")%>>Divorciado</option>
            <option value="VIUDO" <%=SeleccionarItem("cbo",estadocivil_Dal,"VIUDO")%>>Viudo</option>
            </select></td>
          </tr>
          <tr>
	    	<td width="10%" class="rojo">* Fecha Nac.</td>
	    	<td width="9%">
	    	<select size="1" name="dia" class="cajas">
	    	<option value="0">[D�a]</option>
	    	<%for i=1 to 31
	    	    dianac=iif(len(i)=1,"0" & i,i)%>
	    		<option value="<%=dianac%>" <%=SeleccionarItem("cbo",dia,i)%>><%=dianac%></option>
	    	<%next%>
	    	</select>
	    	<select size="1" name="mes" class="cajas">
	    	<option value="0">[Mes]</option>	    	
	    	<%for i=1 to 12
	    	    mesnac=iif(len(i)=1,"0" & i,i)%>
	    		<option value="<%=mesnac%>" <%=SeleccionarItem("cbo",mes,i)%>><%=monthname(i)%></option>
	    	<%next%>
            </select>
            <input type="text" onkeypress="validarnumero()" name="anio" size="4" class="cajas" maxlength="4" value="<%=anio%>"> 
            (4 d�gitos)</td>
	    	<td width="8%" class="rojo" align="right">*
	    	Doc. Identidad</td>
	    	<td colspan=2>
            <select size="1" name="tipodocident_alu" class="cajas">
            <option value="DNI" <%=SeleccionarItem("cbo",tipodocident_alu,"DNI")%>>DNI</option>
            <option value="CARN� DE EXTRANJER�A" <%=SeleccionarItem("cbo",tipodocident_alu,"CARN� DE EXTRANJER�A")%>>Carn� de Extranjer�a</option>
            </select>
            <input type="text" name="nrodocident_alu" size="11" class="cajas" maxlength="11" onkeypress="validarnumero()" value="<%=nrodocident_alu%>"></td>
          </tr>
          <tr>
	    	<td width="10%" class="rojo">* E-mail principal</td>
	    	<td width="10%">
            <input <%=lectura%> type="text" name="email_alu" size="20" class="cajas2" value="<%=email_alu%>"></td>
	    	<td width="10%" align="right">E-mail alternativo</td>
	    	<td class="style1" colspan=2>
	    	<input type="text" name="email2_alu" size="20" class="cajas2" value="<%=email2_alu%>"></td>
          </tr>
          <tr>
	    	<td width="10%" class="rojo">* Direcci�n </td>
	    	<td width="20%" colspan="2">
            <input type="text" name="direccion_dal" size="20" class="cajas2" value="<%=direccion_dal%>"></td>
	    	<td class="style1" colspan=2>
            <input type="text" name="urbanizacion_dal" size="20" class="cajas2" value="<%=urbanizacion_dal%>"></td>
          </tr>
          <tr>
	    	<td width="10%" class="rojo">&nbsp;&nbsp; en la localidad</td>
	    	<td width="17%" colspan=2>Nombre de la Calle o Avenida y N�mero</td>
	    	<td colspan=2>Urb/Residencial/Lugar</td>
          </tr>
          <tr>
	    	<td width="10%" class="rojo" height="60" colspan=2>&nbsp;</td>
	    	<td width="25%" colspan="3" height="60" valign="top"><input type="hidden" name="distrito_dal" value="<%=distrito_dal%>">
            <iframe name="fralugarpersonal" src="../../../vstlugares.asp?IP=N&ID=N&IC=N&fondo=S&codigo_dep=<%=codigoDep_Dal%>&codigo_pro=<%=codigoPro_Dal%>&codigo_dis=<%=distrito_Dal%>" height="100%" width="100%" scrolling="no" border="0" frameborder="0">
            El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.</iframe></td>
          </tr>
          <tr>
	    	<td width="10%">Tel�fono Casa</td>
	    	<td class="usatsubtitulousuario" width="9%">            
            <input type="text" onkeypress="validarnumero()" name="telefonoCasa_Dal" size="20" class="cajas2" value="<%=telefonoCasa_Dal%>" maxlength="11"></td>
	    	<td width="8%" align="right">            
            Tel�fono M�vil</td>
	    	<td class="style2" colspan=2>            
            <input type="text" onkeypress="validarnumero()" name="telefonoMovil_Dal" size="20" class="cajas2" value="<%=telefonoMovil_Dal%>" maxlength="11"></td>
          </tr>
          <tr>
	    	<td width="10%">Centro de Trabajo</td>
	    	<td width="15%">
            <input type="text" name="centrotrabajo_dal" size="20" class="cajas2" value="<%=centrotrabajo_dal%>"></td>
	    	<td width="1%" align="right">Tel�fono de trabajo</td>
	    	<td width="15%" colspan=2>
            <input type="text" onkeypress="validarnumero()" name="telefonoTrabajo_Dal" size="11" class="cajas" value="<%=telefonoTrabajo_Dal%>" maxlength="11">
            <select size="1" name="tipoanexo" class="cajas" onchange="MostrarAnexo(this)">
            <option value="">Sin Anexo</option>
	    	<option value="Ax." <%=SeleccionarItem("cbo",tipoanexo,"Ax.")%>>Anexo</option>
	    	</select><input type="text" onkeypress="validarnumero()" name="anexo" size="5" class="cajas" value="<%=anexo%>" style="display:<%=iif(tipoanexo="","none","")%>" maxlength="5"></td>
          </tr>
          <tr>
	    	<td width="10%">Religi�n</td>
	    	<td width="10%">
            <select size="1" name="religion_dal" class="cajas2">
            <option value="CATOLICO" <%=SeleccionarItem("cbo",religion_dal,"CATOLICO")%>>Cat�lico</option>
            <option value="NO CATOLICO" <%=SeleccionarItem("cbo",religion_dal,"NO CATOLICO")%>>No Cat�lico</option>
            </select></td>
	    	<td width="10%" align="right" valign="top" class="azul">Sacramentos Recibidos</td>
	    	<td class="azul">
                <input <%=bautismo%> type="checkbox" value="1" name="bautismo"/>Bautismo<br />
                <input <%=eucaristia%> type="checkbox" value="1" name="eucaristia"/>Eucaristia<br />
                <input <%=confirmacion%> type="checkbox" value="1" name="confirmacion"/>Confirmaci�n
            </td>
            <td class="azul">
                <input <%=matrimonio%> type="checkbox" value="1" name="matrimonio"/>Matrimonio<br />
                <input <%=orden%> type="checkbox" value="1" name="orden"/>Orden Sacerdotal
            </td>
          </tr>
          </table>
	      <table id="tblacademico" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" class="pestanarevez" height="100%" bgcolor="#F2F2F2">
          <tr>
	    	<!--<td width="15%" valign="top">Tipo de Instituci�n Educativa</td>-->
	    	<!--<td class="usatsubtitulousuario" width="32%" valign="top">-->
            <!--<select size="1" name="tipocolegio_dal">-->
            <!--<option value="NINGUNO" <%=SeleccionarItem("cbo",tipocolegio_dal,"NINGUNO")%>>--Seleccione--</option>-->
            <!--<option value="NACIONAL" <%=SeleccionarItem("cbo",tipocolegio_dal,"NACIONAL")%>>Nacional</option>-->
            <!--<option value="PARTICULAR" <%=SeleccionarItem("cbo",tipocolegio_dal,"PARTICULAR")%>>Particular</option>-->
            <!--<option value="MILITAR" <%=SeleccionarItem("cbo",tipocolegio_dal,"MILITAR")%>>Militar</option>-->
            <!--</select></td>-->
	    	<td colspan="4"  class="rojo" valign="top">* A�o en el 
            que culmin� estudios secundarios 
            <input type="text" onkeypress="validarnumero()" name="anioegresosec_dal" size="4" class="cajas" maxlength="4" value="<%=anioegresosec_dal%>"> 
            Ej. 1995</td>
          </tr>
          <tr>
	    	<td width="40%" colspan="4" height="150" valign="top"><input type="hidden" name="codigo_col" value="<%=codigo_col%>">
            <iframe name="fralugarcolegio" src="../../../vstlugares.asp?IP=S&ID=S&IC=S&fondo=S&codigo_Pai=<%=codigoPai_Col%>&codigo_dep=<%=codigoDep_Col%>&codigo_pro=<%=codigoPro_Col%>&codigo_dis=<%=codigoDis_Col%>&codigo_col=<%=codigo_col%>" height="100%" width="100%" scrolling="no" border="0" frameborder="0">
            El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.</iframe></td>
          </tr>
          <tr id="otrocolegio" style="display:<%=iif(nombrecolegio_dal="","none","")%>">
	    	<td width="15%">&nbsp;</td>
	    	<td class="usatsubtitulousuario" width="50%" colspan="3">Especifique 
            del nombre de la Instituci�n Educativa:
            <input type="text" name="nombrecolegio_dal" size="20" class="cajas2" value="<%=nombrecolegio_dal%>"></td>
          </tr>
          </table>
           <table id="tblcontacto" border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" class="pestanarevez" bgcolor="#F2F2F2" height="100%">
          <tr>
	    	<td width="16%" class="rojo">* Apellidos y Nombres</td>
	    	<td width="24%">
            <input type="text" name="PersonaFam_Dal" size="20" class="cajas2" value="<%=PersonaFam_Dal%>"></td>
	    	<td width="40%" colspan="2">Apellido Paterno, Materno y Nombres</td>
          </tr>
          <tr>
	    	<td width="16%" class="rojo">* Direcci�n</td>
	    	<td width="39%" colspan="2"><input type="text" name="direccionfam_dal" size="20" class="cajas2" value="<%=direccionfam_dal%>"></td>
	    	<td width="25%">
            <input type="text" name="urbanizacionfam_dal" size="20" class="cajas2" value="<%=urbanizacionfam_dal%>"></td>
          </tr>
          <tr>
	    	<td width="16%" class="rojo">&nbsp;</td>
	    	<td width="11%" colspan="2">Nombre de la Calle o Avenida y N�mero</td>
	    	<td width="8%">Urb/Residencial/Zona</td>
          </tr>
          <tr>
	    	<td width="40%" colspan="4" height="80" valign="top"><input type="hidden" name="distritofam_dal" value="<%=distritoFam_Dal%>">
            <iframe name="fralugarcontacto" src="../../../vstlugares.asp?IP=N&ID=S&IC=N&fondo=S&codigo_dep=<%=codigoDep_Fam%>&codigo_pro=<%=codigoPro_Fam%>&codigo_dis=<%=distritoFam_Dal%>" height="100%" width="100%" scrolling="no" border="0" frameborder="0">
            El explorador no admite los marcos flotantes o no est� configurado actualmente para mostrarlos.</iframe></td>
          </tr>
          <tr>
	    	<td width="21%" class="rojo" valign="top">* Tel�fono</td>
	    	<td class="usatsubtitulousuario" width="24%">
	    	<input type="text" onkeypress="validarnumero()" name="telefonofam_dal" size="20" class="cajas2" value="<%=telefonofam_dal%>" maxlength="11">
            </td>
	    	<td width="40%" colspan="2" valign="top">Incluir el c�digo de la 
            ciudad, si est� fuera de la localidad.</td>
          </tr>
          </table>
         </td>
		</tr>
   </table>
   <!--<h3 class="rojo" align="center">ACEPTO, que la Informaci�n ingresada es fidedigna y correcta para uso exclusivo de la Universidad en los procesos acad�micos y administrativos.</h3>-->
   <p align="center">
   <input type="button" value="Guardar" name="cmdGuardar" class="guardar" onClick="EnviarFicha(frmFicha,txtpasos.value)">
   <input onclick="top.location.href='abriraplicacion.asp'" type="button" value="Cancelar" name="cmdCancelar" class="salir"> 
   <span id="mensaje" class="rojo"></span>
   </p>
</form>
</body>
</html>
<%end if%>