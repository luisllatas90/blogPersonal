<!--#include file="../../funciones.asp"-->  
<% 	
	on error resume next
	distritoPadre = 1
	distritoApod = 1
	tipoDoc = URLDecode(Server.URLEncode(Request.Form("dpTipoDoc")))
	dni = Request.Form("txtdni")
	apaterno = URLDecode(Server.URLEncode(Request.Form("txtAPaterno")))
	amaterno = URLDecode(Server.URLEncode(Request.Form("txtAMaterno")))
	nombres = URLDecode(Server.URLEncode(Request.Form("txtNombres")))
	fechaNac = Request.Form("txtFechaNac")
	sexo = Request.Form("dpSexo")
	estadoCivil = Request.Form("dpEstadoCivil")
	paisNac = Request.Form("dpPaisNacimiento")
	depNac = Request.Form("dpdepartamentonac")
	provNac = Request.Form("dpprovincianac")
	distNac = Request.Form("dpdistritonac")
	email1 = URLDecode(Server.URLEncode(Request.Form("txtemail1")))
	email2 = URLDecode(Server.URLEncode(Request.Form("txtemail2")))
	direccion = URLDecode(Server.URLEncode(Request.Form("txtdireccion")))
	distDireccion = Request.Form("dpdistrito")
	telefono = Request.Form("txttelefono")
	celular = Request.Form("txtcelular")
	operador = Request.Form("dpOperador")
	hdcodigo_pso = Request.Form("hdcodigo_pso")	
	hdurlaccion = Request.Form("hdurlaccion")	
	hdurlpso = Request.Form("hdurlpso")	
	hdurlid = Request.Form("hdurlid")
	dpCicloIng_alu = Request.Form("dpCicloIng_alu")
	dpCodigo_cpf = Request.Form("dpCodigo_cpf")
	dpModalidad = Request.Form("dpModalidad")
	hdurcodigo_test = Request.Form("hdurcodigo_test")
	hdurlcco = Request.Form("hdurlcco")
	hdtxtAPaterno = URLDecode(Server.URLEncode(Request.Form("hdtxtAPaterno")))
	hdtxtAMaterno = URLDecode(Server.URLEncode(Request.Form("hdtxtAMaterno")))
	hdtxtNombres = URLDecode(Server.URLEncode(Request.Form("hdtxtNombres")))
	hdtxtFechaNac = Request.Form("hdtxtFechaNac")
	hddpSexo = Request.Form("hddpSexo")
	hddpTipoDoc = URLDecode(Server.URLEncode(Request.Form("hddpTipoDoc")))
	hdtxtdni = Request.Form("hdtxtdni")
	hdtxtemail1 = URLDecode(Server.URLEncode(Request.Form("hdtxtemail1")))
	hdtxtemail2 = URLDecode(Server.URLEncode(Request.Form("hdtxtemail2")))
	hdtxtdireccion = URLDecode(Server.URLEncode(Request.Form("hdtxtdireccion")))
	hddpdistrito = Request.Form("hddpdistrito")
	hdtxttelefono = Request.Form("hdtxttelefono")
	hdtxtcelular = Request.Form("hdtxtcelular")
	hddpOperador = Request.Form("hddpOperador")
	hddpEstadoCivil = Request.Form("hddpEstadoCivil")
	hddpPaisNacimiento = Request.Form("hddpPaisNacimiento")	
	hddpdistritonac = Request.Form("hddpdistritonac")	
	hdcodigobd = Request.Form("hdcodigobd")
	hdcodigopso = Request.Form("hdcodigopso")
	dpPaisColegio = Request.Form("dpPaisColegio")
	dpdistritocolegio = Request.Form("dpdistritocolegio")
	dpCodigo_col = Request.Form("dpCodigo_col")
	dpPromocion = Request.Form("dpPromocion")		
	nombresPadre = URLDecode(Server.URLEncode(Request.Form("txtNombresPadre")))
	direccionPadre = URLDecode(Server.URLEncode(Request.Form("txtdireccionPadre")))
	urbanizacionPadre = URLDecode(Server.URLEncode(Request.Form("txturbanizacionPadre")))
	distritoPadre = Request.Form("dpdistritoPadre")
	telefonoPadre = Request.Form("txttelefonoPadre")
	telefonooficinaPadre = Request.Form("txttelefonooficinaPadre")
	celularPadre = Request.Form("txtcelularPadre")
	OperadorPadre = Request.Form("dpOperadorPadre")
	emailPadre = URLDecode(Server.URLEncode(Request.Form("txtemailPadre")))
	hdPaso = Request.Form("hdPaso")	
	nombresApod = URLDecode(Server.URLEncode(Request.Form("txtNombresApoderado")))
	direccionApod = URLDecode(Server.URLEncode(Request.Form("txtdireccionApoderado")))
	urbanizacionApod = URLDecode(Server.URLEncode(Request.Form("txturbanizacionApoderado")))
	distritoApod = Request.Form("dpdistritoApoderado")
	telefonoApod = Request.Form("txttelefonoApoderado")
	telefonooficinaApod = Request.Form("txttelefonooficinaApoderado")
	celularApod = Request.Form("txtcelularApoderado")
	OperadorApod = Request.Form("dpOperadorApoderado")
	emailApod = URLDecode(Server.URLEncode(Request.Form("txtemailApoderado")))
	observacion = Request.Form("txtObservaciones")
	hdDisAuditiva = 0
	hdDisFisica = 0
	hdDisVisual = 0	
	hdDisAuditiva = Request.Form("hdchkDisAuditiva")	
	hdDisFisica = Request.Form("hdchkDisFisica")	
	hdDisVisual =Request.Form("hdchkDisVisual")			    
	codigo_pso=0
	chkCentroAplicacion = 0
	categorias=URLDecode(Server.URLEncode(Request.Form("txtSelectedMLValues2")))	
	beneficios=URLDecode(Server.URLEncode(Request.Form("txtSelectedMLValues")))
	costocrd =Request.Form("txtcc")	
	'rbtCostoEstudio =Request.Form("rbtCostoEstudio")		

	if Request.Form("chkCentroAplicacion") = "on" then
		chkCentroAplicacion = 1		
	End if
					
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	codigo_alu=0	
	
	If (hdPaso = "1") Then		
		codigo_pso=-2		
		If (ValidaForm = True) Then
			codigo_pso=-3
			If (ValidarNroIdentidad = True) Then             
				
				'Me.grwDeudas.Visible = False
                'Me.lblmensaje.Text = ""

                tcl = "E"
                cli = 0                                                                         
				
				If hdurlaccion = "A" Then
					pso = hdcodigo_pso		
				Else
					pso = hdurlpso		
				End If	
               
                'obj.IniciarTransaccion()
				'=================================================================
				'Grabar a la persona: Aqu� se verifica si EXISTE.
				'=================================================================						
				codigo_pso = Obj.Ejecutar("PERSON_Agregarpersona",true,pso,hdtxtAPaterno, hdtxtAMaterno, hdtxtNombres, fechaNac, sexo, tipoDoc, dni, email1, email2, direccion,distDireccion,telefono,celular,estadoCivil,"",hdurlid,paisNac,0)
				Obj.CerrarConexion
				'obj.TerminarTransaccion()				
				Set Obj=nothing	
								
				If codigo_pso = 0 Then
                    obj.AbortarTransaccion()                    
                End If								
			End If		
			Response.Write codigo_pso								
		End If	
		
	Elseif (hdPaso = "2") Then
		'HCANO
		if hdtxtdni = 0 and txtdni <>0 then
		    hdtxtdni=txtdni
		end if			
		'obj.IniciarTransaccion()
		codigo_alu = Obj.Ejecutar("EVE_AgregarParticipanteEPPGestionaNotas_v3",true,hdurlcco,dpCicloIng_alu,dpCodigo_cpf,dpModalidad,hdurcodigo_test,hdtxtAPaterno,hdtxtAMaterno,hdtxtNombres,hdtxtFechaNac,hddpSexo,hddpTipoDoc,hdtxtdni,hdtxtemail1,hdtxtemail2,hdtxtdireccion,hddpdistrito,hdtxttelefono,hdtxtcelular,hddpEstadoCivil,GeneraClave,hdurlid,hdcodigopso,hddpPaisNacimiento,hddpdistritonac,hddpOperador,hdDisAuditiva,hdDisFisica,hdDisVisual,0)	
		'response.Write("EVE_AgregarParticipanteEPPGestionaNotas_v3 " & hdurlcco & "," & dpCicloIng_alu & "," & dpCodigo_cpf & "," & dpModalidad & "," & hdurcodigo_test & "," & hdtxtAPaterno & "," & hdtxtAMaterno & "," & hdtxtNombres & "," & hdtxtFechaNac & "," & hddpSexo & "," & hddpTipoDoc & "," & hdtxtdni & "," & hdtxtemail1 & "," & hdtxtemail2 & "," & hdtxtdireccion & "," & hddpdistrito & "," & hdtxttelefono & "," & hdtxtcelular & "," & hddpEstadoCivil & "," & GeneraClave & "," & hdurlid & "," & hdcodigopso & "," & hddpPaisNacimiento & "," & hddpdistritonac & "," & hddpOperador & "," & hdDisAuditiva & "," & hdDisFisica & "," & hdDisVisual & "," & "0")
		
		'===============================================================================================================
        'Grabar como ESTUDIANTE: Siempre gestione notas: consultar el tipo de estudio para asignar NIVEL
        '===============================================================================================================
        
        '*-*' SE RETIRDAN TODAS LAS MODALIDADES DE PLAN INGRESO DIRECTO - HCANO 13/06/16
        
        '-' dpModalidad = 14 : CONVENIOS, SE RETIRA MODALIDAD DE PLAN INGRESO DIRECTO - HCANO 31/05/16
        
        ''If dpModalidad = 14 Or dpModalidad = 6 Or dpModalidad = 3 Or dpModalidad = 23 Or dpModalidad = 29 Then
        'If dpModalidad = 6 Or dpModalidad = 3 Or dpModalidad = 23 Or dpModalidad = 29 Then
        '-'
			'var = obj.Ejecutar("PEC_ActualizarPlanIngresoDirecto", true, cint(codigo_alu), cint(dpCodigo_cpf))
        'End If
        
        '*-*'        
		'===============================================================================================================
        
        
        If codigo_alu = "0" Then
            obj.AbortarTransaccion()            
        End If
        
		If codigo_alu = "-1" Then
            obj.AbortarTransaccion()            
        End If
		
		'=================================================================
        'Enviar a su cuenta de correo el codigo universitario y password
        '=================================================================

        If hdtxtemail1 <> "" Then
			                             		
		    Set rsAlumno=Obj.Consultar("PERSON_ConsultarAlumnoPersona","FO",0, hdcodigobd, hdurlcco, 0)
	        	        	                
	        If (rsAlumno.BOF and rsAlumno.EOF) then       	    
	        else  				
                codigouniversitario = rsAlumno("codigoUniver_Alu")
	            password= rsAlumno("password_Alu")
				
				Set objMail= Server.CreateObject("PryUSAT.ClsMail")				
				objMail.AbrirConexion()  
												
				mensaje = "<br><br>Su registro ha sido activado.<br><br> Su c�digo universitario es " & _
				codigouniversitario & " y su password es " & password & _
				". <br><br>Atte.<br><br>Campus Virtual - USAT."

				'xvar=objMail.EnviarMail("serviciosti@usat.edu.pe", "Escuela Pre Universitaria", txtemail1.Text, "Registro Activado", mensaje, True)
				xvar=objMail.EnviarMail("serviciosti@usat.edu.pe", "Escuela Pre Universitaria", "monica_vm88@hotmail.com", "Registro Activado", mensaje, True)				
				
				objMail.CerrarConexion()
				objMail.TerminarTransaccion()				
				Set objMail=nothing
	        End if							
        End If	

		'=================================================================
        'Grabar categorias y beneficios
        '=================================================================		
		'Eliminar todos registros		
		var = Obj.Ejecutar("EPRE_BorrarCategoriasPostulacionAlumno", true, codigo_alu)
		var = Obj.Ejecutar("EPRE_BorrarBeneficiosPostulacionAlumno", true, codigo_alu)

        If Cstr(categorias) <> "" Then
            'Insertar items
            Dim tabla			
            tabla = Split(categorias, ",")							           
			For Each item In tabla
                var = Obj.Ejecutar("EPRE_GuardarCategoriaPostulacion", true, codigo_alu, item)
            Next
        End If
		
        If Cstr(beneficios) <> "" Then                        
            'Insertar items
            Dim tabla2
            tabla2 = Split(beneficios, ",")			                 
			For Each item In tabla2
                var = Obj.Ejecutar("EPRE_GuardarBeneficioPostulacion", true, codigo_alu, item)
            Next
        End If
			
		



		Obj.CerrarConexion
			'obj.TerminarTransaccion()				
	        Set Obj=nothing
		
		Response.Write codigo_alu
	Elseif (hdPaso = "3") Then
		
		If dpPaisColegio <> 156 Then 'Si no es Per�, el colegio es NULO
			colegio = 1
		Else
			colegio = dpCodigo_col
		End If

		codigo_alu = Obj.Ejecutar("EVE_AgregarParticipanteEPPDatosSecundaria",true,hdcodigobd,dpPaisColegio,colegio,dpPromocion, "", 0,chkCentroAplicacion,0)
		
		var = Obj.Ejecutar("EPRE_AlumnoPrecioCredito",true,codigo_alu,costocrd)


		Obj.CerrarConexion
		'obj.TerminarTransaccion()
		Set Obj=nothing				
		Response.Write codigo_alu
	Elseif (hdPaso = "4") Then
		codigo_alu = hdcodigobd
		codigo_alu = Obj.Ejecutar("EVE_AgregarParticipanteEPPDatosPadre",true,hdcodigobd,nombresPadre,direccionPadre,urbanizacionPadre,distritoPadre,telefonoPadre,telefonooficinaPadre,celularPadre,OperadorPadre,emailPadre,0)
		Obj.CerrarConexion
		'obj.TerminarTransaccion()
		Set Obj=nothing				
		Response.Write codigo_alu
	Elseif (hdPaso = "5") Then
		codigo_alu = hdcodigobd
		codigo_alu = Obj.Ejecutar("EVE_AgregarParticipanteEPPDatosApoderado",true,hdcodigobd,nombresApod,direccionApod,urbanizacionApod,distritoApod,telefonoApod,telefonooficinaApod,celularApod,OperadorApod,emailApod,observacion,0)
		Obj.CerrarConexion
		'obj.TerminarTransaccion()
		Set Obj=nothing				
		Response.Write codigo_alu	
	End if
	
	Function GeneraClave() 
        Randomize()
        GeneraClave = Right(UCase(hdtxtAPaterno), 1) & _
            Left(UCase(hdtxtNombres), 1) & _
            CInt(Rnd() * 4) & CInt(Rnd() * 5) & CInt(Rnd() * 9) & CInt(Rnd() * 7)
    End Function
	
	Function ValidaForm()
		If tipoDoc = "DNI" Then
            If (dni <> "" And Len(dni) = 8) Then
                Return True
            Else
                Return False
            End If			
        End If
        Return True
	End Function
	
	Function ValidarNroIdentidad()
        
		If tipoDoc = "DNI" Then
            'No validar si el DNI est� vac�o
            If Len(dni) = 0 Then
                ValidarNroIdentidad = True
            ElseIf Len(dni) <> 8 Or IsNumeric(dni) = False Or dni = "00000000" Then
                'Me.lblmensaje.Text = "El n�mero de DNI es incorrecto. M�nimo 8 caracteres"
                'Me.lblmensaje.Visible = True
                ValidarNroIdentidad = False
                'If IrAlFoco = True Then Me.txtdni.Focus()                
            Else                
                ValidarNroIdentidad = True
            End If			
        ElseIf tipoDoc = "CARN� DE EXTRANJER�A" And Len(dni) < 9 Then
            'Me.lblmensaje.Text = "El n�mero de pasaporte es incorrecto. M�nimo 9 caracteres"
            'Me.lblmensaje.Visible = True
            ValidarNroIdentidad = False
            'If IrAlFoco = True Then Me.txtdni.Focus()            
        Else            
            ValidarNroIdentidad = True
        End If		
    End Function

%>



<%
FUNCTION URLDecode(str)
'// This function:
'// - decodes any utf-8 encoded characters into unicode characters eg. (%C3%A5 = �)
'// - replaces any plus sign separators with a space character
'//
'// IMPORTANT:
'// Your webpage must use the UTF-8 character set. Easiest method is to use this META tag:
'// <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
'//
    Dim objScript
    Set objScript = Server.CreateObject("ScriptControl")
    objScript.Language = "JavaScript"
    URLDecode = objScript.Eval("decodeURIComponent(""" & str & """.replace(/\+/g,"" ""))")
    Set objScript = NOTHING
END FUNCTION
%>