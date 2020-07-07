<%
'*----------------------------------------------------
'Asignar los valores de los controles a variables
'*----------------------------------------------------
'-----------------------------------------------------
'Variables generales
'-----------------------------------------------------
'Dim fechainicio,fechafin
Dim idcategoria
Dim lugar,descripcion,modalidad
Dim idtabla,nombretabla,IdPermiso
Dim idtipopublicacion,cbxidtipopublicacion,idestadorecurso

'-----------------------------------------------------
'Variables de agenda
'-----------------------------------------------------
Dim tituloagenda
Dim prioridad

'-----------------------------------------------------
'Variables de cursovirtual
'-----------------------------------------------------
Dim titulocursovirtual
Dim creartemas
Dim temapublico
Dim integrartematarea
Dim integrarrptatarea

Dim codigo_prof
Dim	codigo_alu
Dim	nombre_alu
Dim email_alu
Dim ArrTipoUsu
Dim ArrCodUsu
Dim ArrNomUsu
Dim ArrClaveUsu
Dim ArrEmailUsu

'-----------------------------------------------------
'Variables del documento
'-----------------------------------------------------

Dim titulodocumento
Dim versiondoc
Dim escritura

'-----------------------------------------------------
'Variables de contenidos
'-----------------------------------------------------

Dim titulocontenido
Dim ordencontenido

'-----------------------------------------------------
'Variables de Evaluacion
'-----------------------------------------------------
Dim tituloevaluacion
Dim instrucciones
Dim enlinea
Dim mostrarresultados
Dim incluirimagenes
Dim modificarrespuesta
Dim preguntaporpregunta
Dim retrocederpaginas
Dim respuestacorrecta
Dim vecesacceso
Dim minutos

'-----------------------------------------------------
'Variables de Pregunta
'-----------------------------------------------------
Dim titulopregunta
Dim ordenpregunta
Dim pjebueno
Dim pjemalo
Dim pjeblanco
Dim obligatoria
Dim duracion
Dim valorpredeterminado


'-----------------------------------------------------
'Variables de noticia
'-----------------------------------------------------
Dim titulonoticia
'-----------------------------------------------------

'-----------------------------------------------------
'Variables de tarea
'-----------------------------------------------------
Dim titulotarea
Dim recurso
Dim idtipotarea
Dim calificacion
Dim permitirreenvio
'-----------------------------------------------------

'-----------------------------------------------------
'Variables de foro
'-----------------------------------------------------
Dim tituloforo,permitircalificar,tipocalificacion,numcalificacion
'-----------------------------------------------------

'-----------------------------------------------------
'Variables de mensajes foro
'-----------------------------------------------------
Dim titulomensaje,descripcionmensaje
'-----------------------------------------------------


	fechainicio=request.form("fechainicio") & " " & request.form("horainicio") & ":" & request.form("mininicio") & ":00 " & request.form("turnoinicio")
	fechafin=request.form("fechafin") & " " & request.form("horafin") & ":" & request.form("minfin") & ":00 " & request.form("turnofin")
	if request.form("fechainicio")="" then fechainicio=now
	if request.form("fechafin")="" then fechafin=session("fincursovirtual")

	idcategoria=request.form("idcategoria")
	lugar=request.form("lugar")
	descripcion=request.form("descripcion")
	idtipopublicacion=request("idtipopublicacion")
	cbxidtipopublicacion=request("cbxidtipopublicacion")
	idestadorecuro=request("idestadorecurso")
	
	idtabla=Request.querystring("idtabla")
	nombretabla=Request.querystring("nombretabla")
'-----------------------------------------------------------------------------------
	Sub ControlesAgenda()
		tituloagenda=request.form("tituloagenda")
		prioridad=request.form("prioridad")
	End Sub
	
	Sub Controlescursovirtual()
		fechainicio=request.form("fechainicio")
		fechafin=request.form("fechafin")
		titulocursovirtual=request.form("titulocursovirtual")
		modalidad=Request.form("modalidad")
		if modalidad="" then modalidad="presencial"
		creartemas=Request.form("creartemas")
		creartemas=iif(creartemas="",0,creartemas)
		temapublico=Request.form("temapublico")
		temapublico=iif(temapublico="",0,temapublico)
		integrartematarea=Request.form("integrartematarea")
		integrartematarea=iif(integrartematarea="",0,integrartematarea)
		integrarrptatarea=Request.form("integrarrptatarea")
		integrarrptatarea=iif(integrarrptatarea="",0,integrarrptatarea)
	End Sub
	
	Sub ControlesMatriculaCurso()
		fechainicio=request.querystring("fechainicio")
		fechafin=request.querystring("fechafin")
		titulocursovirtual=request.querystring("titulocursovirtual")
		descripcion=request.querystring("descripcion")
		codigo_prof=Request.querystring("codigo_prof")
		codigo_prof=replace(codigo_prof,"/","\")
		codigo_alu=verificacaracterfinal(Request.querystring("codigosuniversitarios"),";")
		nombre_alu=verificacaracterfinal(Request.querystring("nombrealumnos"),";")
		email_alu=verificacaracterfinal(Request.querystring("emailalumnos"),";")
		ArrCodUsu=split(codigo_alu,";")
		ArrNomUsu=split(nombre_alu,";")
		ArrEmailUsu=split(email_alu,";")
	End Sub
	
	Sub ControlesEnvioDatosAcceso()
		dim tipo_usu,codigo_usu,nombre_usu,clave_usu,email_usu
		
		tipo_usu=verificacaracterfinal(Request.querystring("tipos"),";")
		codigo_usu=verificacaracterfinal(Request.querystring("codigos"),";")
		nombre_usu=verificacaracterfinal(Request.querystring("nombres"),";")
		clave_usu=verificacaracterfinal(Request.querystring("claves"),";")
		email_usu=verificacaracterfinal(Request.querystring("emails"),";")
		
		ArrTipoUsu=split(tipo_usu,";")
		ArrCodUsu=split(codigo_usu,";")
		ArrNomUsu=split(nombre_usu,";")
		ArrClaveUsu=split(clave_usu,";")
		ArrEmailUsu=split(email_usu,";")
	End Sub
	
	Sub ControlesDocumento()
		titulodocumento=request.form("titulodocumento")
		versiondoc=request.form("versiondoc")
		escritura=request.form("escritura")
	End Sub
	
	Sub ControlesContenido()
		ordencontenido=request.form("ordencontenido")
		titulocontenido=request.form("titulocontenido")
	End Sub
	
	Sub ControlesEvaluacion()
		tituloevaluacion=request.form("tituloevaluacion")
		instrucciones=request.form("instrucciones")	
		enlinea=request.form("enlinea")
		enlinea=IIf(enlinea="",0,enlinea)
		mostrarresultados=request.form("mostrarresultados")
		mostrarresultados=IIf(mostrarresultados="",0,mostrarresultados)
		incluirimagenes=request.form("incluirimagenes")
		incluirimagenes=IIf(incluirimagenes="",0,incluirimagenes)
		modificarrespuesta=request.form("modificarrespuesta")
		modificarrespuesta=IIf(modificarrespuesta="",0,modificarrespuesta)
		preguntaporpregunta=request.form("preguntaporpregunta")
		preguntaporpregunta=IIf(preguntaporpregunta="",0,preguntaporpregunta)
		retrocederpaginas=request.form("retrocederpaginas")
		retrocederpaginas=IIf(retrocederpaginas="",0,retrocederpaginas)
		respuestacorrecta=request.form("respuestacorrecta")
		respuestacorrecta=IIf(respuestacorrecta="",0,respuestacorrecta)
		minutos=request.form("minutos")
		minutos=IIf(minutos="",0,minutos)
		vecesacceso=request.form("vecesacceso")
		vecesacceso=IIF(vecesacceso="",1,vecesacceso)
	End Sub
	
	Sub ControlesPregunta()
		idtipopregunta=request.form("idtipopregunta")
		titulopregunta=request.form("titulopregunta")
		ordenpregunta=request.form("ordenpregunta")
		
		pjebueno=request.form("pjebueno")
		pjemalo=request.form("pjemalo")
		pjeblanco=request.form("pjeblanco")
		obligatoria=request.form("obligatoria")
		duracion=request.form("duracion")
		URL=" "
		valorpredeterminado=request.form("valorpredeterminado")
		if pjebueno="" then pjebueno=0
		if pjemalo="" then pjemalo=0
		if pjeblanco="" then pjeblanco=0
		if obligatoria="" then obligatoria=0
		if duracion="" then duracion=0
	End Sub
	
	Sub ControlesNoticia()
		titulonoticia=request.form("titulonoticia")
	End Sub
	
	Sub ControlesTarea()
		titulotarea=request.form("titulotarea")
		recurso=request.form("cbxrecurso")
		idtipotarea=request.form("idtipotarea")
		calificacion=request.form("calificacion")
		calificacion=iif(calificacion="",0,calificacion)
		permitirreenvio=request.form("permitirreenvio")
		permitirreenvio=iif(permitirreenvio="",0,permitirreenvio)
		
		if (idtipotarea="3" or idtipotarea=5) then
			recurso="frmasignardocumentos.asp"
		end if
	End Sub
	
	Sub ControlesForo()
		tituloforo=Request.form("tituloforo")
		permitircalificar=Request.form("permitircalificar")
		tipocalificacion=Request.form("tipocalificacion")
		numcalificacion=Request.form("numcalificacion")
		if permitircalificar="" then permitircalificar=0
		if tipocalificacion="" then tipocalificacion=0
		if numcalificacion="" then numcalificacion=0
  	End Sub
  	
  	Sub ControlesMensajeForo()
		titulomensaje=Request.form("titulomensaje")
		descripcionmensaje=Request.form("web")
  	End Sub
%>