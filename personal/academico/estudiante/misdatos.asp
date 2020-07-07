<%
On Error Resume Next 
    codigo_alu=request.querystring("codigo_alu")
    'response.Write ("Codigo_alu: " & request.querystring("codigo_alu"))
    codigouniver_alu=request.querystring("codigouniver_alu")
    dim mostrarfichapersonal 
    mostrarfichapersonal = true


if session("codigo_tfu")="13" then
	mostrarfichapersonal=false ' no se puede mostrar la ficha de personal para los docentes	
end if 

  
if codigo_alu<>"" then
	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	   codigofoto=obEnc.CodificaWeb("069" & codigouniver_alu)
	set obEnc=Nothing

	'Cargar datos para cambiarlos
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsDatos=Obj.Consultar("ConsultarAlumno","FO","CO",codigo_alu)
		Set rsAlumno=Obj.Consultar("ConsultarAlumno","FO","RG",codigo_alu)
		set rsAlumnoPadre=Obj.Consultar("ADM_ConsultarAlumnoInscripcion", "FO",codigo_alu, "SP")
		set rsAlumnoMadre=Obj.Consultar("ADM_ConsultarAlumnoInscripcion", "FO",codigo_alu, "SM")
		set rsAlumnoApod=Obj.Consultar("ADM_ConsultarAlumnoInscripcion", "FO",codigo_alu, "SA")
		'Set rsMerito=Obj.Consultar("ConsultarPonderadoAlumno_Tercio","FO","TQ",codigo_alu)
		'Set rsMerito=Obj.Consultar("ACAD_ConsultarPromedioAlumno_TercioQuinto", "FO", codigo_alu)
		'Set rsPromedio=Obj.Consultar("ACAD_ConsultarPromedioPonderadoGeneral", "FO", codigo_alu)
		Set rsSeparacion=Obj.Consultar("ACAD_ConsultarSeparacionVigente", "FO",codigo_alu)
		'Agregado mvillavicencio 21-09-12
		Set rsHistoricoObservaciones=Obj.Consultar("EPRE_ListarBitacoraObservaciones", "FO",codigouniver_alu)
		set rsBeneficioBeca = Obj.Consultar("beca_ConsultarAlumnoBenificio","FO",codigo_alu)
		Set rsDatosTramiteRetiro=Obj.Consultar("TRL_VerificarTramiteRetiro","FO",codigo_alu) ' EPENA 17/10/2019 GLPI 24143 
	Obj.CerrarConexion
	Set Obj=nothing
	
	if not(rsDatos.BOF and rsDatos.EOF) then
		alumno=rsDatos("alumno")
		codigo_pso=rsDatos("codigo_pso")
		nombre_cpf=rsDatos("nombre_cpf")
		cicloIng_alu=rsDatos("cicloIng_alu")
		descripcion_pes=rsDatos("descripcion_pes")
		creditosEgresar_pes=rsDatos("creditosEgresar_pes")
		nombre_min=rsDatos("nombre_min")
		password_alu=rsDatos("password_alu")
		estadoActual_alu=rsDatos("estadoActual_alu")
		estadoDeu_alu=rsDatos("estadoDeuda_alu")
		condicion_alu=rsDatos("condicion_alu")
		preciocrd=rsDatos("preciocreditoact_alu")
		codigo_cpf=rsDatos("codigo_cpf")
		
		
		if estadoDeu_alu=0 then
			estadoDeu_alu="No"
		else
			estadoDeu_alu="S�"
		end if
		beneficioAcademico=rsDatos("beneficioAcademico")
		observacion_Dal = rsDatos("observacion_Dal")
		
		egresado = rsDatos("egresado")
		semestreEgreso = rsDatos("semestreEgreso")
			
		retirado= rsDatos("retirado")
		semestreRetiro = rsDatos("semestreRetiro")	
		
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
            emailusat =rsAlumno("emailusat")
            
            tieneDatosFamiliares = false
            
            'Datos familiares (NUEVO)
            if not(rsAlumnoPadre.BOF and rsAlumnoPadre.EOF) then
                tieneDatosFamiliares = true
                mostrarDatosPadre = "table-row"
                
                numeroDocIdentPadre = rsAlumnoPadre("numeroDocIdent_fam")
                apellidoPaternoPadre = rsAlumnoPadre("apellidoPaterno_fam")
                apellidoMaternoPadre = rsAlumnoPadre("apellidoMaterno_fam")
                nombresPadre = rsAlumnoPadre("nombres_fam")
                nombreCompletoPadre = apellidoPaternoPadre & " " & apellidoMaternoPadre & ", " & nombresPadre
                departamentoPadre = rsAlumnoPadre("nombre_Dep")
                provinciaPadre = rsAlumnoPadre("nombre_Pro")
                distritoPadre = rsAlumnoPadre("nombre_Dis")
                direccionPadre = rsAlumnoPadre("direccion_fam") & " (" & departamentoPadre & "/" & provinciaPadre & "/" & distritoPadre & ")"
                telefonoFijoPadre = rsAlumnoPadre("telefonoFijo_fam")
                telefonoCelularPadre = rsAlumnoPadre("telefonoCelular_fam")
                telefonPadre = telefonoFijoPadre & " | " & telefonoCelularPadre
                emailPadre = rsAlumnoPadre("email_fam")
                if rsAlumnoPadre("indRespPago_fam") then 
                    respPagoPadre = "Si"
                else
                    respPagoPadre = "No"
                end if 
            else
                mostrarDatosPadre = "none"
            end if
            
            if not(rsAlumnoMadre.BOF and rsAlumnoMadre.EOF) then
                tieneDatosFamiliares = true
                mostrarDatosMadre = "table-row"
                
                numeroDocIdentMadre = rsAlumnoMadre("numeroDocIdent_fam")
                apellidoPaternoMadre = rsAlumnoMadre("apellidoPaterno_fam")
                apellidoMaternoMadre = rsAlumnoMadre("apellidoMaterno_fam")
                nombresMadre = rsAlumnoMadre("nombres_fam")
                nombreCompletoMadre = apellidoPaternoMadre & " " & apellidoMaternoMadre & ", " & nombresMadre
                departamentoMadre = rsAlumnoMadre("nombre_Dep")
                provinciaMadre = rsAlumnoMadre("nombre_Pro")
                distritoMadre = rsAlumnoMadre("nombre_Dis")
                direccionMadre = rsAlumnoMadre("direccion_fam") & " (" & departamentoMadre & "/" & provinciaMadre & "/" & distritoMadre & ")"
                telefonoFijoMadre = rsAlumnoMadre("telefonoFijo_fam")
                telefonoCelularMadre = rsAlumnoMadre("telefonoCelular_fam")
                telefonMadre = telefonoFijoMadre & " | " & telefonoCelularMadre
                emailMadre = rsAlumnoMadre("email_fam")
                if rsAlumnoMadre("indRespPago_fam") then 
                    respPagoMadre = "Si"
                else
                    respPagoMadre = "No"
                end if 
            else
                mostrarDatosMadre = "none"
            end if
            
            if not(rsAlumnoApod.BOF and rsAlumnoApod.EOF) then
                tieneDatosFamiliares = true
                mostrarDatosApod = "table-row"
                
                numeroDocIdentApod = rsAlumnoApod("numeroDocIdent_fam")
                apellidoPaternoApod = rsAlumnoApod("apellidoPaterno_fam")
                apellidoMaternoApod = rsAlumnoApod("apellidoMaterno_fam")
                nombresApod = rsAlumnoApod("nombres_fam")
                nombreCompletoApod = apellidoPaternoApod & " " & apellidoMaternoApod & ", " & nombresApod
                departamentoApod = rsAlumnoApod("nombre_Dep")
                provinciaApod = rsAlumnoApod("nombre_Pro")
                distritoApod = rsAlumnoApod("nombre_Dis")
                direccionApod = rsAlumnoApod("direccion_fam") & " (" & departamentoApod & "/" & provinciaApod & "/" & distritoApod & ")"
                telefonoFijoApod = rsAlumnoApod("telefonoFijo_fam")
                telefonoCelularApod = rsAlumnoApod("telefonoCelular_fam")
                telefonApod = telefonoFijoApod & " | " & telefonoCelularApod
                emailApod = rsAlumnoApod("email_fam")
                
                if rsAlumnoApod("indRespPago_fam") then 
                    respPagoApod = "Si"
                else
                    respPagoApod = "No"
                end if 
            else
                mostrarDatosApod = "none"
            end if   
            
            cssDatosFamAntiguos = "table-row"
            if tieneDatosFamiliares then
                cssDatosFamAntiguos = "none"
            end if         
            
            'Campos de la tbl DATOSALUMNO
            fecharegistro_Dal=rsAlumno("fecharegistro_Dal")
            '-----Datos de la familia/contacto----------- Padre/Madre
            PersonaFam_Dal=rsAlumno("PersonaFam_Dal")
            direccionfam_dal=rsAlumno("direccionfam_dal")
            urbanizacionfam_dal=rsAlumno("urbanizacionfam_dal")
            nombreDisFam_Dal=rsAlumno("nombreDisFam_Dal")
            nombreProFam_Dal=rsAlumno("nombreProFam_Dal")
            nombreDepFam_Dal=rsAlumno("nombreDepFam_Dal")
            telefonofam_dal=rsAlumno("telefonofam_dal")
            
            if rsAlumnoPadre.recordcount > 0 then
                PersonaFam_Dal=rsAlumnoPadre("apellidoPaterno_fam") & " " & rsAlumnoPadre("apellidoMaterno_fam") & " " & rsAlumnoPadre("nombres_fam")
                direccionfam_dal=rsAlumnoPadre("direccion_fam")
                nombreDisFam_Dal=rsAlumnoPadre("nombre_Dis")
                nombreProFam_Dal=rsAlumnoPadre("nombre_Pro")
                nombreDepFam_Dal=rsAlumnoPadre("nombre_Dep")
                telefonofam_dal=rsAlumnoPadre("telefonoFijo_fam") & " | " & rsAlumnoPadre("telefonoCelular_fam")
            else
                if rsAlumnoMadre.recordcount > 0 then
                    PersonaFam_Dal=rsAlumnoMadre("apellidoPaterno_fam") & " " & rsAlumnoMadre("apellidoMaterno_fam") & " " & rsAlumnoMadre("nombres_fam")
                    direccionfam_dal=rsAlumnoMadre("direccion_fam")
                    nombreDisFam_Dal=rsAlumnoMadre("nombre_Dis")
                    nombreProFam_Dal=rsAlumnoMadre("nombre_Pro")
                    nombreDepFam_Dal=rsAlumnoMadre("nombre_Dep")
                    telefonofam_dal=rsAlumnoMadre("telefonoFijo_fam") & " | " & rsAlumnoMadre("telefonoCelular_fam")
                end if
            end if
            
            '-----Datos del Apoderado----------- Responsable del Pago
            PersonaApod_Dal=rsAlumno("PersonaApod_Dal")
            direccionApod_Dal=rsAlumno("direccionApod_Dal")
            urbanizacionApod_Dal=rsAlumno("urbanizacionApod_Dal")
            nombreDisApod_Dal=rsAlumno("nombreDisApod_Dal")
            nombreProApod_Dal=rsAlumno("nombreProApod_Dal")
            nombreDepApod_Dal=rsAlumno("nombreDepApod_Dal")
            TelefonoApod_Dal=rsAlumno("TelefonoApod_Dal")
            
            
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

    'Separaci�n de estudiantes
	codSeparacion=0
	if not(rsSeparacion.BOF and rsSeparacion.EOF) then
	    codSeparacion = rsSeparacion("codigo_tse")
	    tipoSeparacion = rsSeparacion("descripcion_tse")
	    motivoSeparacion = rsSeparacion("motivo_sep")
	    fechaFin = rsSeparacion("fechafin_sep")
	    nroSeparaciones = rsSeparacion("NroSeparaciones")
	    diasVencidosSeparacion = rsSeparacion("DiasVencidos")
	end if 
	
	'Verificar si tiene TR
	
%>
<html>
<head>
    <meta http-equiv="Content-Language" content="es">
    <meta name="GENERATOR" content="Microsoft FrontPage 5.0">
    <meta name="ProgId" content="FrontPage.Editor.Document">
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <title>misdatos</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
    <link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css"
        media="print" />

    <script language="JavaScript" src="../../../private/funciones.js"></script>

    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>

    <link rel="stylesheet" href="../../../private/jq/lbox/thickbox.css" type="text/css"
        media="screen" />
    <style fprolloverstyle>
        A:hover
        {
            color: red;
            font-weight: bold;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 24%;
        }
        .style2
        {
            color: #000080;
            font-weight: bold;
            width: 16%;
        }
        .style3
        {
            width: 0%;
        }
        .style4
        {
            color: #000080;
            font-weight: bold;
            width: 15%;
        }
        .style5
        {
            width: 24%;
            height: 25px;
        }
        .style7
        {
            width: 24%;
            height: 19px;
        }
        .style8
        {
            color: #000080;
            font-weight: bold;
            height: 19px;
        }
        .style9
        {
            height: 19px;
        }
    </style>

    <script type="text/javascript">
        function AbrirVentanaObservaciones() {
            window.open("../../../librerianet/academico/frmBitacoraObservaciones.aspx?codigouniv=" + "<%=codigouniver_alu%>", "", "toolbar=yes, location=no, directories=no, status=no, menubar=no, resizable=yes, width=800, height=300, top=50");
        }
    </script>

</head>
<body bgcolor="#EEEEEE">
    <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse"
        bordercolor="#111111" width="100%" id="tbldatosestudiante">
        <tr>
            <td align="center" width="17%" valign="top">
                <!--
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
    -->
                <img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>"
                    width="100" height="118" alt="Sin Foto">
            </td>
            <td width="83%" valign="top">
                <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse"
                    bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
                    <tr>
                        <td width="90%" colspan="3" class="usatCeldaTitulo" style="border-left-width: 1;
                            border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid;
                            border-bottom-width: 1">
                            <%=alumno%>
                        </td>
                        <td align="right" class="usatCeldaTitulo" style="border-left-width: 1; border-right-width: 1;
                            border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                            <a href="../../administrativo/gestion_educativa/frmFichaInscripcionPDF.aspx?alu=<%=codigo_alu%>&KeepThis=true&TB_iframe=true&height=400&width=700&modal=false">
                                <img src='../../Images/ext/pdf.gif' border="0" /></a>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            C�digo Universitario&nbsp;
                        </td>
                        <td class="usatsubtitulousuario" width="71%" colspan="3">
                            :
                            <%=codigouniver_alu%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            Apellidos y Nombres
                        </td>
                        <td class="usatsubtitulousuario" width="71%" colspan="3">
                            :
                            <%=alumno%>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            Escuela Profesional&nbsp;
                        </td>
                        <td class="usatsubtitulousuario" width="46%" colspan="3">
                            :
                            <%=nombre_cpf%>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            Ciclo de Ingreso
                        </td>
                        <td class="usatsubtitulousuario" style="width: 16%">
                            :
                            <%=cicloIng_alu%>&nbsp;
                        </td>
                        <td width="9%">
                            Modalidad
                        </td>
                        <td class="usatsubtitulousuario" width="28%">
                            :
                            <%=nombre_min%>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            Plan de Estudio
                        </td>
                        <td class="usatsubtitulousuario" style="width: 16%">
                            :
                            <%=descripcion_pes%>&nbsp;
                        </td>
                        <td width="9%">
                            Cr�d. Egre.
                        </td>
                        <td class="usatsubtitulousuario" width="28%">
                            :
                            <%=creditosEgresar_pes%>&nbsp;
                        </td>
                    </tr>
                    <%  '788:Aljobin - 684 Hugo Saavedra

		if (session("codigo_tfu")="1" or session("codigo_tfu")="30" or session("codigo_usu")="788" or session("codigo_usu")="684" or session("codigo_usu")="33") and mostrarfichapersonal =true  then%>
                    <tr>
                        <td style="width: 24%">
                            Password
                        </td>
                        <td class="usatsubtitulousuario" width="46%" colspan="3">
                            :
                            <%=password_alu%>
                        </td>
                    </tr>
                    <%end if
         if mostrarfichapersonal=true then %>
                    <tr>
                        <td class="style1">
                            Tiene Deuda
                        </td>
                        <td class="style2">
                            :
                            <%=estadoDeu_alu%>
                        </td>
                        <td width="46%" class="style3">
                            Condici�n
                        </td>
                        <td class="style4" width="46%">
                            <%
	    	IF condicion_alu="P" then
	    		response.write ": POSTULANTE"
	    	ELSE
	    		response.write ": INGRESANTE"
	    	END IF
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="style7">
                            Separaci�n <a href="#" style="color: Red" onclick="javascript:window.open('FrmVerSeparacionDetalle.aspx?p=<%= codigo_alu %>','mypopuptitle','width=800,height=400');">
                                (<%=nroSeparaciones%>)</a>
                        </td>
                        <td class="style8" colspan="3">
                            :
                            <%
	    	'response.Write(tipoSeparacion & "<br>")
	    	'response.Write(diasVencidosSeparacion)
	    	    if codSeparacion > 0 then
	    	        if codSeparacion = 2 or  codSeparacion = 4 then
	    	            'response.Write("<font color='red'>" + tipoSeparacion + "</font>" )
	    	            'if (diasVencidosSeparacion < 0) then
	    	                response.Write("<font color='red'>" + tipoSeparacion + " <br/>" + motivoSeparacion + "</font>" )
	    	            'else
	    	            '    response.Write("-")
	    	            'end if	    	            
	    	        else
	    	            if (diasVencidosSeparacion < 0) then
	    	                response.Write("<font color='green'>" + tipoSeparacion + " hasta " +  cstr(fechaFin)  + "</font>")
                        else
	    	                response.Write("-")
	    	            end if		    	            
	    	        end if
	    	    else
	    	       response.Write("No tiene")
	    	        
	    	    end if 
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 24%">
                            Beneficio de beca
                        </td>
                        <td class="usatsubtitulousuario">
                            :
                            <%=rsBeneficioBeca("Beneficio")%>
                        </td>
                        <td>
                            Egresado
                        </td>
                        <td>
                            <%
	    	   
	    	        if egresado = 1 then
	    	            response.Write("<font color='red'>: SI (" & semestreEgreso & ")</font>" )
	    	        else
	    	            response.Write("<font color='green'>: NO</font>")
	    	        end if
	    	   
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="style5">
                            Precio por cr�dito actual
                        </td>
                        <td class="usatsubtitulousuario">
                            :
                            <%= Cdbl(preciocrd) * 5 %>
                        </td>
                        <td>
                            Retiro Semestre
                        </td>
                        <td>
                            <%
	    	' response.Write(egresado)
	    	    if egresado = 0 then
	    	        if retirado = 1 then
	    	            response.Write("<font color='red'>: SI (" & semestreRetiro &   ")</font>" )
	    	        else
	    	            response.Write("<font color='green'>: NO</font>")
	    	        end if
	    	    else 
	    	                 response.Write("<font color='green'>: NO</font>")
	    	   end if 	    	   
	    	  
	    	  	    	   
                            %>
                        </td>
                    </tr>
                    <tr>
                        <td class="style9">
                        </td>
                        <td class="style9">
                        </td>
                        <td class="style9">
                            Retiro Definitivo
                        </td>
                        <td class="style9">
                            <%          
           if cint(rsDatosTramiteRetiro.recordcount) >0 then
           response.Write(": ")
                            %>
                            <a href="#" style="color: Red" onclick="javascript:window.open('FrmVerSeguimientoTramiteRetiro.aspx?p=<%= codigo_alu %>&opc=1','mypopuptitle','width=800,height=400');">
                                <%
           response.Write("<font color='red'>SI (" & rsDatosTramiteRetiro("descripcion_Cac") &   ")*</font>" )
                                %></a><br />
                            &nbsp; <a href="#" style="color: Red" onclick="javascript:window.open('FrmVerSeguimientoTramiteRetiro.aspx?p=<%= codigo_alu %>&opc=2','mypopuptitle','width=800,height=400');">
                                Tiene seguimiento Tr�mite de retiro y anulaci&oacute;n (*)</a>
                            <%
           else
         
                response.Write("<font color='green'>: NO</font>")
          
	       end if   	    	   
                            %>
                        </td>
                    </tr>
                    <% 
         end if
         	
                    %>
                </table>
            </td>
        </tr>
    </table>
    <%if fichapersonal=true and mostrarfichapersonal=true then%>
    <br>
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse"
        bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF" id="AutoNumber2">
        <tr>
            <td width="65%" class="usatCeldaTitulo" colspan="4" style="border-top-style: solid;
                border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                DATOS PERSONALES
            </td>
        </tr>
        <tr>
            <td width="10%">
                Sexo
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=sexo_alu%>&nbsp;
            </td>
            <td width="17%" align="right">
                Fecha Nac.
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=fechanacimiento_alu%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                Doc. Identidad
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=tipodocident_alu%>.&nbsp;<%=nrodocident_alu%>&nbsp;
            </td>
            <td width="17%" align="right">
                Estado Civil
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=estadocivil_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%" class="usatCeldaTitulo">
                E-mail USAT
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=emailusat%>&nbsp;
            </td>
            <td width="17%" align="right">
                E-mail Personal
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=email_alu%>&nbsp;|&nbsp;<%=email2_alu%>
            </td>
        </tr>
        <tr>
            <td width="10%">
                Direcci�n
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=direccion_dal%>.&nbsp;<%=urbanizacion_dal%>
                (<%=UCASE(nombreDis_dal)%>/<%=UCASE(nombrePro_Dal)%>)&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                Tel�fono Casa
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=telefonoCasa_dal%>&nbsp;
            </td>
            <td width="17%" align="right">
                Tel�fono M�vil
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=telefonoMovil_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                Centro de Trabajo
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=centrotrabajo_dal & TelefonoTrabajo_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                Religi�n
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=religion_dal%>&nbsp;
            </td>
            <td width="17%" align="right">
                �ltimo Sacramento
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=ultimosacramento_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                &nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="16%">
                &nbsp;
            </td>
        </tr>
        <%  
          if not(rsSeparacion.BOF and rsSeparacion.EOF) or observacion_Dal <> "" then
        %>
        <tr>
            <td width="65%" class="usatCeldaTitulo" colspan="4">
                VER HIST�RICO DE OBSERVACIONES
            </td>
        </tr>
        <tr>
            <td width="10%" style="color: Red">
                Observaci�n
            </td>
            <td width="17%" class="usatsubtitulousuario" style="color: red">
                :
                <%=observacion_Dal%>&nbsp;
            </td>
            <td width="17%" align="right" colspan="2">
                <input id="btnBitacoraObservaciones" type="button" value="Ver hist�rico de observaciones"
                    onclick="AbrirVentanaObservaciones();" runat="server" visible="false" />
            </td>
        </tr>
        <tr>
            <td width="10%">
                &nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="16%">
                &nbsp;
            </td>
        </tr>
        <%  
          end if
        %>
        <tr>
            <td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1;
                border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid;
                border-bottom-width: 1">
                DATOS ACAD�MICOS
            </td>
        </tr>
        <tr>
            <td width="10%">
                Tipo de Colegio
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=tipocolegio_dal%>&nbsp;
            </td>
            <td width="17%" align="right">
                A�o de Egresado
            </td>
            <td width="16%" class="usatsubtitulousuario">
                :
                <%=anioegresosec_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                Colegio
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=nombrecolegio_dal%>&nbsp;
            </td>
        </tr>
        <tr>
            <td width="10%">
                &nbsp;
            </td>
            <td width="50%" colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1;
                border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                DATOS DEL PADRE
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td>
                DNI
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=numeroDocIdentPadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td>
                Contacto
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=nombreCompletoPadre %>
            </td>
            <td align="right">
                Tel�fono
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=telefonPadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td>
                Direcci�n
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=direccionPadre %>
            </td>
            <td align="right">
                Email
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=emailPadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td>
                Resp. Pago
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=respPagoPadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosPadre%>">
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1;
                border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                DATOS DE LA MADRE
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td>
                DNI
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=numeroDocIdentMadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td>
                Contacto
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=nombreCompletoMadre %>
            </td>
            <td align="right">
                Tel�fono
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=telefonMadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td>
                Direcci�n
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=direccionMadre %>
            </td>
            <td align="right">
                Email
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=emailMadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td>
                Resp. Pago
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=respPagoMadre %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosMadre%>">
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1;
                border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                DATOS DEL APODERADO
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td>
                DNI
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=numeroDocIdentApod %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td>
                Contacto
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=nombreCompletoApod %>
            </td>
            <td align="right">
                Tel�fono
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=telefonApod %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td>
                Direcci�n
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=direccionApod %>
            </td>
            <td align="right">
                Email
            </td>
            <td class="usatsubtitulousuario">
                :
                <%=emailApod %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td>
                Resp. Pago
            </td>
            <td colspan="3" class="usatsubtitulousuario">
                :
                <%=respPagoApod %>
            </td>
        </tr>
        <tr style="display: <%=mostrarDatosApod%>">
            <td>
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1;
                border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid;
                border-bottom-width: 1">
                DATOS DEL PADRE/MADRE
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Contacto
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=PersonaFam_dal%>&nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Direcci�n
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=direccionfam_dal%>.&nbsp;<%=urbanizacionfam_dal%>(<%=ucase(nombreDisFam_dal)%>/<%=ucase(nombreProFam_dal)%>/<%=ucase(nombreDepFam_dal)%>)&nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Tel�fono
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=telefonofam_dal%>&nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="16%">
                &nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1;
                border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid;
                border-bottom-width: 1">
                DATOS DEL RESPONSABLE DE PAGO
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Contacto
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=PersonaApod_dal%>&nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Direcci�n
            </td>
            <td width="50%" colspan="3" class="usatsubtitulousuario">
                :
                <%=direccionApod_dal%>.&nbsp;<%=urbanizacionApod_dal%>(<%=ucase(nombreDisApod_dal)%>/<%=ucase(nombreProApod_dal)%>/<%=ucase(nombreDepApod_dal)%>)&nbsp;
            </td>
        </tr>
        <tr style="display: <%=cssDatosFamAntiguos%>">
            <td width="10%">
                Tel�fono
            </td>
            <td width="17%" class="usatsubtitulousuario">
                :
                <%=telefonoApod_dal%>&nbsp;
            </td>
            <td width="17%">
                &nbsp;
            </td>
            <td width="16%">
                &nbsp;
            </td>
        </tr>
    </table>
    <p class="rojo" align="right">
        Fecha de Actualizaci�n:
        <%=fechareg%></p>
    <%end if%>
</body>
</html>
<%end if
If err.Number <> 0 Then ' Si se encuentra un error
Response.Write "Error numero = " & err.Number & "<p>"
Response.Write "Descripcion = " & err.Description & "<p>"
Response.Write "Fuente = " & err.Source
'err.Clear 'Limpiamos el error
END IF

%>