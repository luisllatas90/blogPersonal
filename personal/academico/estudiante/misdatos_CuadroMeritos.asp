<%
on error resume next
codigo_alu=request.querystring("codigo_alu")
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
		'Set rsMerito=Obj.Consultar("ConsultarPonderadoAlumno_Tercio","FO","TQ",codigo_alu)
		'Set rsMerito=Obj.Consultar("ACAD_ConsultarPromedioAlumno_TercioQuinto", "FO", codigo_alu) 'yperez comentado por nuevo sp
		Set rsMerito = Obj.Consultar("ACAD_InsertarPromedioAlumno_TercioQuinto", "FO", codigo_alu) 
		Set rsPromedio=Obj.Consultar("ACAD_ConsultarPromedioPonderadoGeneral", "FO", codigo_alu)
		Set rsSeparacion=Obj.Consultar("ACAD_ConsultarSeparacionVigente", "FO",codigo_alu)
		Set rsHistoricoObservaciones=Obj.Consultar("EPRE_ListarBitacoraObservaciones", "FO",codigouniver_alu)		
	Obj.CerrarConexion
	Set Obj=nothing
	
	if not(rsDatos.BOF and rsDatos.EOF) then
		alumno=rsDatos("alumno")
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
		preciocrd = CDBL(preciocrd) * CDBL(5)
		codigo_cpf=rsDatos("codigo_cpf")
		if estadoDeu_alu=0 then
			estadoDeu_alu="No"
		else
			estadoDeu_alu="Sí"
		end if
		beneficioAcademico=rsDatos("beneficioAcademico")
		observacion_Dal = rsDatos("observacion_Dal")
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
            emailusat=rsAlumno("emailusat")
            
            'Campos de la tbl DATOSALUMNO
            fecharegistro_Dal=rsAlumno("fecharegistro_Dal")
            '-----Datos de la familia/contacto----------- padre/madre
            PersonaFam_Dal=rsAlumno("PersonaFam_Dal")
            direccionfam_dal=rsAlumno("direccionfam_dal")
            urbanizacionfam_dal=rsAlumno("urbanizacionfam_dal")
            nombreDisFam_Dal=rsAlumno("nombreDisFam_Dal")
            nombreProFam_Dal=rsAlumno("nombreProFam_Dal")
            nombreDepFam_Dal=rsAlumno("nombreDepFam_Dal")
            telefonofam_dal=rsAlumno("telefonofam_dal")
            
            '-----Datos del Apoderado----------- Responsable del Pago
            PersonaApod_Dal=rsAlumno("PersonaApod_Dal")
            direccionApod_Dal=rsAlumno("direccionApod_Dal")
            urbanizacionApod_Dal=rsAlumno("urbanizacionApod_Dal")
            nombreDisApod_Dal=rsAlumno("nombreDisApod_Dal")
            nombreProApod_Dal=rsAlumno("nombreProApod_Dal")
            nombreDepApod_Dal=rsAlumno("nombreDepApod_Dal")
            TelefonoApod_Dal=rsAlumno("TelefonoApod_Dal")
            
            
            '------Datos dónde reside el alumno----------
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
            	telefonoTrabajo_Dal="&nbsp;( Teléf." & telefonoTrabajo_Dal & ")"
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
			anioegresosec_dal=rsAlumno("añoegresosec_dal")

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

    'Separación de estudiantes
	codSeparacion=0
	if not(rsSeparacion.BOF and rsSeparacion.EOF) then
	    codSeparacion = rsSeparacion("codigo_tse")
	    tipoSeparacion = rsSeparacion("descripcion_tse")
	    motivoSeparacion = rsSeparacion("motivo_sep")
	    fechaFin = rsSeparacion("fechafin_sep")
	end if 
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es" />
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252" />
<title>Mis datos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css" />
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print" />
<script language="JavaScript" src="../../../private/funciones.js"></script>
<style type="text/css">
    A:hover {color: red; font-weight: bold}
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
        .style6
        {
            height: 25px;
        }
    </style>
    
    <script type="text/javascript">
        function AbrirVentanaObservaciones() {            
            window.open("../../../librerianet/academico/frmBitacoraObservaciones.aspx?codigouniv=" + "<%=codigouniver_alu%>", "", "toolbar=yes, location=no, directories=no, status=no, menubar=no, resizable=yes, width=800, height=300, top=50");
        }
    </script> 
</head>
<body bgcolor="#EEEEEE">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante">
  <tr>
    <td align="center" width="17%" valign="top">
    <!--
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
    -->
   
    
        <img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="100" height="118" alt="Sin Foto"> 
        </td>
        <td width="83%" valign="top">
        
    <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse; border-color:#111111;"  width="100%" class="contornotabla" bgcolor="#FFFFFF">
      <tr>
        <td width="90%" colspan="4" class="usatCeldaTitulo" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1"><%=alumno%></td>
      </tr>
      <tr>
        <td style="width: 24%">Código Universitario&nbsp;</td>
        <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=codigouniver_alu%></td>
      </tr>
      <tr>
        <td style="width: 24%">Apellidos y Nombres</td>
        <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=alumno%></td>
      </tr>
      <tr>
        <td style="width: 24%">Escuela Profesional&nbsp;</td>
        <td class="usatsubtitulousuario" width="46%" colspan="3">: <%=nombre_cpf%>&nbsp;</td>
      </tr>
      <tr>
	  	<td style="width: 24%">Ciclo de Ingreso</td>
	   	<td class="usatsubtitulousuario" style="width: 16%">: <%=cicloIng_alu%>&nbsp;</td>
	   	<td width="9%">Modalidad</td>
	   	<td class="usatsubtitulousuario" width="28%">: <%=nombre_min%>&nbsp;</td>
      </tr>
      <tr>
	    	<td style="width: 24%">Plan de Estudio</td>
	    	<td class="usatsubtitulousuario" style="width: 16%">: <%=descripcion_pes%>&nbsp;</td>
	    	<td width="9%" align="right">Créd. Egre.:</td>
	    	<td class="usatsubtitulousuario" width="28%">: <%=creditosEgresar_pes%>&nbsp;</td>
      </tr>
          
          <%  '788:Aljobin - 684 Hugo Saavedra

		if (session("codigo_tfu")="1" or session("codigo_tfu")="30" or session("codigo_usu")="788" or session("codigo_usu")="684" or session("codigo_usu")="33" ) and mostrarfichapersonal =true  then%>
          <tr>
	    	<td style="width: 24%">Password</td>
	    	<td class="usatsubtitulousuario" width="46%" colspan="3">: <%=password_alu%></td>
          </tr>
          <%end if
         if mostrarfichapersonal=true then %>
          <tr>
	    	<td class="style1">Tiene Deuda</td>
	    	<td class="style2">: <%=estadoDeu_alu%></td>
	    	<td width="46%" class="style3">Condición</td>
	    	<td class="style4" width="46%">
	    	<%
	    	IF condicion_alu="P" then
	    		response.write "POSTULANTE"
	    	ELSE
	    		response.write "INGRESANTE"
	    	END IF
	    	%>
	    	</td>
          </tr>
          <tr>
	    	<td style="width: 24%">Separación</td>
	    	<td class="usatsubtitulousuario" colspan="3">: 
	    	<%
	    	    if codSeparacion >0 then
	    	        if codSeparacion = 2 then
	    	            response.Write("<font color='red'>" + tipoSeparacion + " <br/>" + motivoSeparacion + "</font>" )
	    	        else
	    	            response.Write("<font color='green'>" + tipoSeparacion + " hasta " +  cstr(fechaFin)  + "</font>")
	    	        end if
	    	    else
	    	        response.Write("No tiene")
	    	    end if 
	    	 %>
	    	</td>
          </tr>
          <tr>
	    	<td style="width: 24%">Beneficio de beca</td>
	    	<td class="usatsubtitulousuario" colspan="3">: 
	    	<%=beneficioAcademico%>
	    	</td>
          </tr>
          <tr>
	    	<td class="style5">Precio por crédito actual</td>
	    	<td class="style6" colspan="3">:
	    	<%=preciocrd %>
	    	
	    	</td>
          </tr>
          
          <% if session("codigo_tfu") = "16" or session("codigo_tfu") = "25" _
                or session("codigo_tfu") = "18" or session("codigo_tfu") = "16" _
                or session("codigo_tfu") = "1" OR session("codigo_tfu") = "85" _
                OR session("codigo_tfu") = "16" OR session("codigo_tfu") = "181" _
                OR session("codigo_tfu") = "7" then 
          	    Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	            Obj.AbrirConexion            
		            'Set rsMerito=Obj.Consultar("ACAD_ConsultarPromedioAlumno_TercioQuinto", "FO", codigo_alu)								
					Set rsMerito=Obj.Consultar("ACAD_InsertarPromedioAlumno_TercioQuinto", "FO", codigo_alu)
		            Set rsPromedio=Obj.Consultar("ACAD_ConsultarPromedioPonderadoGeneral", "FO", codigo_alu)
		        Obj.CerrarConexion
	            Set Obj=nothing
	            promedioAprobados=rsPromedio("promedioAprobados")
                if Not(rsPromedio.BOF and rsPromedio.EOF) then
	                promedio_est=formatnumber(rsPromedio("promedio_est"),2)	
	                promedio_egr=formatnumber(rsPromedio("promedio_egr"),2)	
	                
	            end if 
          	    if Not(rsMerito.BOF and rsMerito.EOF) then
		            if cdbl(rsMerito("promedio_it")) > 0.0 then 
		                HayTercio_est = "Si"
		                cicloIngreso = rsMerito("ciclo_i")
		            else
		                HayTercio_est = "No"
		            end if 
		            if cdbl(rsMerito("promedio_iq")) > 0.0 then 
		                HayQuinto_est = "Si"
		            else
		                HayQuinto_est = "No"
		            end if 
            		
            		IF rsMerito("Ciclo_Egr") >0 and rsMerito("ciclo_e") = "" THEN
                        HayTercio_egr = "No"
                        HayQuinto_egr = "No"
                        ''response.Write("ver aca")
            	    END IF
            		

				'	RESPONSE.WRITE rsMerito("promedio_et")

		                if cdbl(rsMerito("promedio_et")) > 0.0 then 
		                    HayTercio_egr = "Si"
		                    cicloEgreso = rsMerito("ciclo_Egr")
		                else
		                    HayTercio_egr = "No"
		                end if     
		                if cdbl(rsMerito("promedio_eq")) > 0.0 then 
		                    HayQuinto_egr = "Si"
		                else
		                    HayQuinto_egr = "No"
		                end if  
            		
     
	            End if
	          
	            'Actualización
	            'Mostrar info de promedio ponderado/tercio/quinto según condición: estudiante o egresado	            
	            '21.01.13 @yperez            
	            
	            es_estudiante = false
	            es_egresado = false
	            strCicloReferencia = ""
                	            	            
	            '-- response.Write (promedio_egr)            
	            if (promedio_egr>0.00) then 
	                es_egresado = true 
	            else 
	                es_estudiante= true 
	            end if	
	          
	            if rsMerito("Ciclo_Egr") >0 then	                
	              'response.Write "OK: " & rsPromedio("titul")    
                  if rsPromedio("titul")="SI"  then	                    
	                    'text1 ="(Egr)" 
	                    text1 ="(Egr: "+  rsMerito("CicloEgresado") +")"
	                   ' text2 = "*"
	                    promedio_general = cstr(promedio_egr) + " [TITULACIÓN]" 
	                    	                 
	                    tercio = HayTercio_egr
	                    quinto = HayQuinto_egr            

	              else
	                    text1 ="(Egr: "+  rsMerito("CicloEgresado") +")"
	                    'text2 = "*"
	                    promedio_general = promedio_egr
	                    tercio = HayTercio_egr
	                    quinto = HayQuinto_egr
	               end if 
	            else
	                strCicloReferencia = rsMerito("CicloReferencia")
''	              if es_estudiante = true then	                        
	                    text1 = "(Est) - Ref.:" + strCicloReferencia
	                    'text2 = "*"
	                    promedio_general = promedio_est
	                    tercio = HayTercio_est
	                    quinto = HayQuinto_est                    
	            end if
          %>
          
          <tr class="usatTablaInfo">
	    	<td style="width: 24%; ">Promedio Ponderado Acumulado <%=text1%> <%response.Write("") %> </td>
	    	<td style="width: 16%;">: <%= promedio_general%></td>	 
	    	<td style="width: 16%;">Ponderado Ciencias Salud (Psi. <= 8vo ciclo || Med. Enf. Odo. Sin Internado )</td>	 
	    	<td style="width: 16%;">: <%= formatnumber(promedioAprobados,2) %></td>	 
          </tr>
          <tr class="usatTablaInfo">
	    	<td style="width: 24%; ">Tercio Superior <%=text1 & text2%></td>
	    	<td style="width: 16%;">: <%= tercio%>	    	
	    	</td>	 
	    	<td>   		    		    	</td>
	    	<td>   		    		    	</td>
          </tr>
          <tr class="usatTablaInfo">
	    	<td style="width: 24%">Quinto Superior <%=text1 & text2%></td>
	    	<td style="width: 16%">: <%=quinto %></td>	
	    	<td>   		    		    	</td>
	    	<td>   		    		    	</td>														
          </tr>
          
          <tr class="usatTablaInfo">
           <% if (HayTercio_est)="Si"  then %>
	    	<td style="width: 24%" colspan="2"> 	    	
	    	    <input class="buscar2" name="cmdVer" type="button" value="    Méritos Estudiante" style="width: 110px"        
                onclick="AbrirPopUp('promedioTercioQuinto.aspx?codigo_alu=<%=codigo_alu%>&tipo=EST&cpf=<%=codigo_cpf %>&ci=<%=cicloIngreso %>','400','780','yes','yes','yes')">
                
		    </td>
		    <% end if %>
		    <% if (HayTercio_egr)="Si" then %>
	    	<td style="width: 16%;" colspan="2"> 	    	    
			    <input class="buscar2" name="cmdVer0" type="button" value="    Méritos Egresado" style="width: 110px" 
                onclick="AbrirPopUp('promedioTercioQuinto.aspx?codigo_alu=<%=codigo_alu%>&tipo=EGR&cpf=<%=codigo_cpf %>&ce=<%=cicloEgreso %>','400','850','yes','yes','yes')">                
			</td>
			<% end if %>			
          </tr>
         <!-- Sol. mmares 11.05.15 Incluir estudiantes de titulación en cuadro de méritos egresado            
          <tr class="usatTablaInfo">
	    	<td colspan="4">* Para el cálculo de méritos no se consideran a los estudiantes de titulación.</td>
          </tr>  
          -->          
         <% end if         
         end if        	
         %>          
          </table>
    </td>
  </tr>
  </table>  
  <%if fichapersonal=true and mostrarfichapersonal=true then%>          
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
	    	<td width="10%" class="usatCeldaTitulo">E-mail USAT</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=emailusat%>&nbsp;</td>
	    	<td width="17%" align="right">E-mail Personal</td>
	    	<td  width="16%" class="usatsubtitulousuario">:  <%=email_alu%>&nbsp;|&nbsp;<%=email2_alu%></td>
          </tr>
          <tr>
	    	<td width="10%">Dirección</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=direccion_dal%>.&nbsp;<%=urbanizacion_dal%> (<%=UCASE(nombreDis_dal)%>/<%=UCASE(nombrePro_Dal)%>)&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Teléfono Casa</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=telefonoCasa_dal%>&nbsp;</td>
	    	<td width="17%" align="right">Teléfono Móvil</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=telefonoMovil_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Centro de Trabajo</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=centrotrabajo_dal & TelefonoTrabajo_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Religión</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=religion_dal%>&nbsp;</td>
	    	<td width="17%" align="right">Último Sacramento</td>
	    	<td  width="16%" class="usatsubtitulousuario">: <%=ultimosacramento_dal%>&nbsp;</td>
          </tr>                          
          <tr>
	    	<td width="10%">&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          <%  
          if not(rsSeparacion.BOF and rsSeparacion.EOF) or observacion_Dal <> "" then
          %>
           <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4">VER HISTÓRICO DE OBSERVACIONES</td>
	      </tr>
	      <tr>
	    	<td width="10%" style="color:Red">Observación</td>
	    	<td  width="17%" class="usatsubtitulousuario" style="color:red">: <%=observacion_Dal%>&nbsp;</td>
	    	<td width="17%" align="right" colspan="2">
	    	    <input id="btnBitacoraObservaciones" type="button" 
	    	     value="Ver histórico de observaciones" onclick="AbrirVentanaObservaciones();"
                 runat="server" visible="false" />
	    	</td>	    	
          </tr>  
	      <tr>
	    	<td width="10%">&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          <%  
          end if
          %>
          <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">DATOS ACADÉMICOS</td>
	    	</tr>
          <tr>
	    	<td width="10%">Tipo de Colegio</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=tipocolegio_dal%>&nbsp;</td>
	    	<td width="17%" align="right">Año de Egresado</td>
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
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">DATOS DEL PADRE/MADRE</td>
	    	</tr>
          <tr>
	    	<td width="10%">Contacto</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=PersonaFam_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Dirección</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=direccionfam_dal%>.&nbsp;<%=urbanizacionfam_dal%>(<%=ucase(nombreDisFam_dal)%>/<%=ucase(nombreProFam_dal)%>/<%=ucase(nombreDepFam_dal)%>)&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Teléfono</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=telefonofam_dal%>&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          <tr>
	    	<td width="65%" class="usatCeldaTitulo" colspan="4" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">DATOS DEL RESPONSABLE DE PAGO</td>
	    	</tr>
          <tr>
	    	<td width="10%">Contacto</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=PersonaApod_dal%>&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Dirección</td>
	    	<td  width="50%" colspan="3" class="usatsubtitulousuario">: <%=direccionApod_dal%>.&nbsp;<%=urbanizacionApod_dal%>(<%=ucase(nombreDisApod_dal)%>/<%=ucase(nombreProApod_dal)%>/<%=ucase(nombreDepApod_dal)%>)&nbsp;</td>
          </tr>
          <tr>
	    	<td width="10%">Teléfono</td>
	    	<td  width="17%" class="usatsubtitulousuario">: <%=telefonoApod_dal%>&nbsp;</td>
	    	<td  width="17%">&nbsp;</td>
	    	<td  width="16%">&nbsp;</td>
          </tr>
          </table>
	<p class="rojo" align="right">Fecha de Actualización: <%=fechareg%></p>
	<%end if%>
</body>
</html>
<%
end if

if Err.number <> 0 then
    response.Write "Error: " & Err.Description
end if
%>