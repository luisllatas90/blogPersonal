<% 
On error Resume next
Function DevuelveCadenaSQLMoodle()
'DevuelveCadenaSQLMoodle = "DRIVER={MySQL ODBC 5.1 Driver}; SERVER=172.16.1.6; DATABASE=bdaulavirtual;UID=desa99cixperu;PWD=usatcix@99" 
DevuelveCadenaSQLMoodle = "DRIVER={MySQL ODBC 5.1 Driver}; SERVER=172.16.1.9; DATABASE=bdaulavirtual;UID=desa99cixperu;PWD=usatcix@99"
'DevuelveCadenaSQLMoodle = "DRIVER={MySQL ODBC 5.1 Driver}; SERVER=10.10.14.250; DATABASE=moodle;UID=root;PWD=mysql" 
end function

Function DevuelvePrefix()
	DevuelvePrefix = "mdl_"
	'DevuelvePrefix = ""
end function

Function crear_categoria_nivel(nombreC,descripcion,codigo_pes,padre,ruta,dep,column)
    '#YPEREZ
    Dim Prefix, ObjMdlC
	Prefix = DevuelvePrefix
	idcategoriaC = 0
    nameC = nombreC
    description = descripcion 
    parent = padre 'Categoría de la que depende = Cpf.       
    sortorder = 999
    coursecount = 0
    visible = 1
    timemodified = ValorTiempoUnix(now)
    depth = dep' nivelpes: test/stest/cpf/pes  nivelcpf: test/stest/cpf
    path = ruta ' sin /idactual
    theme = "aardvark_automenu"
    'codigo_EdCon = codigo_pes
    
    if column= "codigo_edcon" then
        sql="insert into " & Prefix & "course_categories (name, description, parent, sortorder, coursecount, visible, timemodified, depth, path, theme,codigo_EdCon,cod_PsGr) values ('" &nameC& "' , '" & description & "' , " & parent & " , " & sortorder & " , " & coursecount & " , " & visible & " , " & timemodified & " , " & depth & " , '" & path & "' , '" & theme & "' , " & codigo_pes & ", 0)"
    end if
    
    if column= "cod_PsGr" then
        theme = "epostgrado"
        sql="insert into " & Prefix & "course_categories (name, description, parent, sortorder, coursecount, visible, timemodified, depth, path, theme,codigo_EdCon,cod_PsGr) values ('" &nameC& "' , '" & description & "' , " & parent & " , " & sortorder & " , " & coursecount & " , " & visible & " , " & timemodified & " , " & depth & " , '" & path & "' , '" & theme & "' , 0 ," & codigo_pes & ")"
    end if
    
    
    Dim objConn
    Set objConn = Server.CreateObject("ADODB.Connection") 	
	objConn.Open DevuelveCadenaSQLMoodle 
	objConn.Execute(sql)
	
	Set ObjMdlC = objConn.execute("select last_insert_id()")
	idcategoriaC = objMdlC(0)
	
	sql = "update "&Prefix & "course_categories set path = '" &path&"/"&idcategoriaC& "' where id = " &idcategoriaC
	objConn.Execute(sql)
	crear_categoria_nivel = idcategoriaC
end function

Sub  crear_curso(categoria,nombre,codigo_cup,inicio,fin,sf,sn,codigo_per,codigo_test,codigo_pes,grupoHor_cup, descripcion_pes,nombre_cpf,cac,ciclo)
	
	Dim objConn , objRS , objRS1 , objRS2, objRS3, objRS4,objRSC,objNC
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
		
	objConn.Open DevuelveCadenaSQLMoodle 
	
	'' ***************** SABER QUE EL CURSO NO ESTE AGREGADO ***************
	Set ObjCur = Server.CreateObject("PryUSAT.clsAccesoDatos")
	
	ObjCur.AbrirConexion
	'	Set rsCurso=ObjCur.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","CU",codigo_cup,0)
	    
	'if rsCurso.recordcount = 0 then ' curso no existe en Moodle > crear
		
		
		
		'******verificar si es curso de educacion continua o postgrado #YPEREZ
		if codigo_test = 6 or codigo_test = 5 then				        	        
		      
		      column = ""
		      if codigo_test = 6 then
		        column = "codigo_edcon"
		      else
		        column = "cod_PsGr"
		      end if
		      
		      'Buscar id Categoria nivel 4
		      
    	      Set objRSC = objConn.Execute("select count(1), id, "& column &" from " & Prefix & "course_categories where " & column &  " = " & codigo_pes & " and name = '"& descripcion_pes &" - " &grupoHor_cup&"'")
		         
		      if objRSC(1) <>"" then 
                  categoria = objRSC(1)
              else
		           
		           Set rsCAT=ObjCur.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","CT",codigo_cup,0)
		           
		           nombreCat = descripcion_pes& " - " & grupoHor_cup
		           descripcionCat = "Plan de Estudios: " &descripcion_pes& ", Grupo Horario: "&grupoHor_cup
		            
		           
		           Set objMdl = objConn.Execute("select count(1),id, path  from " & Prefix & "course_categories where " & column & " = '" & rsCAT("codigo_cpf")&"'")
		           		           
		           padre = objMdl(1)		           
    	           ruta = objMdl(2)
		           
		           if objMdl(1) <> "" then
		            'existe categoria nivel 3 => crear categoria nivel 4
		            categoria =	crear_categoria_nivel (nombreCat,descripcionCat,codigo_pes,padre,ruta,4)
                   else
                    'no existe categoria nivel 3 (CPF) => crear categoria nivel 3
                     Set objMdl = objConn.Execute("select id, path  from " & Prefix & "course_categories where " &column &" = '" & rsCAT("codigo_stest")&"'")
                     
		             padre = objMdl(0)
    	             ruta = objMdl(1)
                     categoria = crear_categoria_nivel (nombre_cpf,nombre_cpf,rsCAT("codigo_cpf"),padre,ruta,3,column)
                     
                     'crear nivel 4
                     Set objMdl = objConn.Execute("select id, path  from " & Prefix & "course_categories where id = '" &categoria& "'")
		             padre = objMdl(0)
    	             ruta = objMdl(1)
    	             categoria =	crear_categoria_nivel (nombreCat,descripcionCat,codigo_pes,padre,ruta,4,column)
    	              
                   end if
		      end if		
		end if
		'*******
		 
		ObjCur.CerrarConexion
		Set ObjCur=nothing
		
		'****
		'Crear curso
		idcategoria = categoria
		ahora =   ValorTiempoUnix (now)'fecha y hora del sistema - formato UNIX
		password=""
		fullname=nombre
		shortname=codigo_cup
		idnumber=""
		summary= nombre
		format= sf'"weeks"
		showgrades=1
		modinfo=""
		newsitems=5
		guest=0
		startdate=ValorTiempoUnix(inicio)
		enrolperiod=0
		numsections=sn 'cantidad de semanas
		marker=0
		maxbytes=10485760  'se subio de 2mb a 5mb y de 5 a 10 2012-II
		showreports=1
		visible=1
		hiddensections=1
		groupmode=0
		groupmodeforce=0
		timecreacion=ahora
		timemodificacion=ahora
		metacourse=0
		enrollable=0 '0-curso no abierto 1-curso abierto para matricula
		expirythreshold=ValorTiempoUnix(fin)
		lang=""
		theme=""
		cost=""
		enrol="manual"

		Set objRS = objConn.Execute("select sortorder from " & Prefix & "course order by sortorder desc limit 0,1")
		
		
		ultimo_orden = objRS.Fields("sortorder")
			
		sortorden = cdbl(ultimo_orden) - 1
		
		sql="insert into " & Prefix & "course (category, sortorder, password, fullname, shortname, summary, format, showgrades, modinfo, newsitems,guest,startdate,enrolperiod, numsections, marker, maxbytes, showreports, visible, hiddensections,lang,theme,cost,timecreated, timemodified, expirythreshold, enrol, enrollable, ct, cc, ci) values (" & idcategoria & "," & sortorden & ",'','" & fullname & "', '" & shortname & "', '" & summary & "', '" & format & "', " & showgrades & ", '" & modinfo & "', " & newsitems & ", " & guest & "," & startdate & "," & enrolperiod & "," & numsections & "," & marker & "," & maxbytes & "," & showreports & "," & visible & "," & hiddensections & ",'" & lang & "','" & theme & "','" & cost & "'," & timecreacion & "," & timemodificacion & "," & expirythreshold & ",'" & enrol & "', "& enrollable & "," & codigo_test & "," & cac& "," & ciclo  & ")"

			
		 objconn.execute(sql)
		 		 
		 '$idcurso=$cnx->lastinsertid();   'ultimo curso insertado
		 set rs = objConn.execute("select last_insert_id()")
		 idcurso=rs(0)

		 pageid=idcurso
		 
		 blockid="20"
		 position="l"
		 weight="0"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="1"
		 position="l"
		 weight="1"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		

		 blockid="2"
		 position="l"
		 weight="2"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

    	 blockid="12"
		 position="r"
		 weight="0"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

         blockid="35"
		 position="r"
		 weight="1"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		
		 blockid="8"
		 position="r"
		 weight="2"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)
		 
		 
		 sql3="insert into " & Prefix & "course_sections (course,section,visible) values(" & idcurso & ",0,1)"
		 set objrs3 = objconn.execute(sql3)
		 
         
         set rs = objConn.Execute("select path, depth from mdl_context where instanceid = " & idcategoria & " and contextlevel = 40")
	     path = rs(0)          
	     depth = rs(1)
	
         
		 sql4="insert into " & Prefix & "context (contextlevel,instanceid,path,depth) (50," & idcurso & ",'" & path & "'," & depth & ")"
		 set objrs4 = objconn.execute(sql4)
		 
		 set rs = objConn.Execute("select last_insert_id()")
	     idcontext=rs(0)

         path = path & "/" & idcontext 
         
   		 sql5="update mdl_context set path ='" & path & "' where id = " & idcontext
   		 set objrs6 = objconn.execute(sql5)
   		 		 
		 objrs.close
		 set objrs = nothing
		 objRSC = nothing
		 objconn.close
		 set objconn = nothing
		 

	'end if
	 
	 ' ******************* MATRICULAR A LOS ALUMNOS *******************
	 'call crear_usuarioDocente (codigo_per)  ' --> Ver de donde llama codigo_per	 
	 'call asignarcurso_docente (codigo_per, codigo_cup, inicio, fin)  ' --> Ver de donde llama codigo_per
	 '¿response.Write("Antes del call")
     ' call matricular("", codigo_cup, inicio,fin)
      'response.Write("Despues del call") 
End sub

' **************************************************************************************************

Function ValorTiempoUnix (Byval fecha) 
	ValorTiempoUnix = DateDiff("s", CDate("31/12/1969 00:00:00"), fecha)
End function

' **************************************************************************************************

sub crear_asistencia(codigo_cup, ciclo)

    Dim objConn
    Set objConn = Server.CreateObject("ADODB.Connection") 	
	objConn.Open DevuelveCadenaSQLMoodle 
	
	Set ObjMdlC = objConn.execute("select id from mdl_course where shortname ='" & codigo_cup & "'")
	idcur = objMdlC(0)
	
	
    Set ObjAs = Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjAs.AbrirConexion
	ObjAs.Ejecutar "MDL_CrearAttforblock", false , idcur,ciclo
	ObjAs.CerrarConexion
	ObjAs = nothing
	
	objconn.close
    set objconn = nothing
end sub

Sub crear_usuario(username,password,firstname,lastname,email)
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
	objConn.Open DevuelveCadenaSQLMoodle 
	auth="manual"           ' // cambiar para enlace .net
	confirmed = 1
	password=md5(password)   ' MD5
	mnethostid=1
	lang="es_utf8"
	idnumber=""          ' podria ir aqui el id del alumno .net
	country="PE"
	theme=""
	sql="INSERT INTO " & Prefix & "user (auth ,confirmed ,mnethostid,username,password,idnumber,firstname,lastname,email,country,lang,theme)VALUES ('" & auth & "', '" & confirmed & "','" & mnethostid & "', '" & username & "', '" & password & "' , '" & idnumber & "', '" & firstname & "', '" & lastname & "','" & email & "','" & country & "', '" & lang & "', '" & theme & "')"
	objconn.execute(sql)
End Sub
'  ***************************************************************************************************


' **************************************************************************************************
Sub crear_usuarioDocente2(username) 'dangela       
   Dim objConn 
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
	objConn.Open DevuelveCadenaSQLMoodle 
	
	'saber si docente existe en moodle
	
	Set objRS = objConn.Execute("select count(*) as num from " & Prefix & "user where username = "& codigo_per)
        existeDocente = cint (objRS.Fields("num"))
	
	if (existeDocente = 0 )then 'Si no existe, crearlo
	    
	    Set ObjUserDocente=Server.CreateObject("PryUSAT.clsAccesoDatos")
		ObjUserDocente.AbrirConexion
		Set rsDocente=ObjUserDocente.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","PE",0,username)
		ObjUserDocente.CerrarConexion
	    Set ObjUserDocente=nothing
	    
	        firstname = rsDocente("nombres_Pso")
			lastname = rsDocente("apellidoPaterno_Pso") +  " " +  rsDocente("apellidoMaterno_Pso")  
 			email = rsDocente("emailPrincipal_Pso")
			
			auth="manual"           ' // cambiar para enlace .net
			confirmed = 1
			password=""  ' MD5
			mnethostid=1
			lang="es_utf8"
			idnumber=""          ' podria ir aqui el id del alumno .net
			country="PE"
			theme=""
			sql="INSERT INTO " & Prefix & "user (auth ,confirmed ,mnethostid,username,password,idnumber,firstname,lastname,email,country,lang,theme)"
			sql= sql & "VALUES ('" & auth & "', '" & confirmed & "','" & mnethostid & "', '" & username & "', '" & password & "' , '" & idnumber & "', '" & firstname & "', '" & lastname & "','" & email & "','" & country & "', '" & lang & "', '" & theme & "')"
			objconn.execute(sql)
	end if
End Sub

Sub crear_usuarioDocente(username)'original
   	'' ***************** SABER SI EL DOCENTE EXISTE ***************
	
	
	Set ObjUserDocente=Server.CreateObject("PryUSAT.clsAccesoDatos")
		ObjUserDocente.AbrirConexion
			Set rsDocente=ObjUserDocente.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","PE",0,username)
		ObjUserDocente.CerrarConexion
	Set ObjUserDocente=nothing
    
	'' ***************** DECLARACION DE MYSQL ***************
    Dim objConn 
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
	objConn.Open DevuelveCadenaSQLMoodle 
	
	if rsDocente.eof= false and rsDocente.bof = false then
			rsDocente.movefirst
			firstname = rsDocente("nombres_Pso")
			lastname = rsDocente("apellidoPaterno_Pso") +  " " +  rsDocente("apellidoMaterno_Pso")  
 			email = rsDocente("emailPrincipal_Pso")
 						
			auth="manual"           ' // cambiar para enlace .net
			confirmed = 1
			password=""  ' MD5
			mnethostid=1
			lang="es_utf8"
			idnumber=""          ' podria ir aqui el id del alumno .net
			country="PE"
			theme=""
			sql="INSERT INTO " & Prefix & "user (auth ,confirmed ,mnethostid,username,password,idnumber,firstname,lastname,email,country,lang,theme)"
			sql= sql & "VALUES ('" & auth & "', '" & confirmed & "','" & mnethostid & "', '" & username & "', '" & password & "' , '" & idnumber & "', '" & firstname & "', '" & lastname & "','" & email & "','" & country & "', '" & lang & "', '" & theme & "')"
			
			objconn.execute(sql)
	end if
End Sub

'  ***************************************************************************************************

Sub matricular(username,codcurso,inicio,fin)
    hidden = "0"
	timestart = ValorTiempoUnix(inicio)
	timeend = ValorTiempoUnix(fin)
	enrol = "manual"
	rolid = 5
    
	'' ***************** DECLARACION DE SQL ***************
	Set ObjMat=Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjMat.AbrirConexion
		Set rsAlumnos=ObjMat.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","MA",codcurso,0)
		
	ObjMat.CerrarConexion
	Set ObjMat=nothing

	'' ***************** DECLARACION DE MYSQL ***************
	Dim objConn 
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
	objConn.Open DevuelveCadenaSQLMoodle 
	
	'' ****************** RECORRIDO DE ALUMNOS PARA MARICULAR ****************
	if rsAlumnos.eof= false and rsAlumnos.bof = false then
		rsAlumnos.movefirst 
	    
		sqlcurso="select id from " & Prefix & "course where shortname='" & codcurso & "'"
		set rscurso=objconn.execute(sqlcurso)
		cursoid=rscurso(0)
	    
		sqlcontext="select id from " & Prefix & "context where contextlevel=50 and instanceid='" & cursoid & "'"
		set rscontext=objconn.execute(sqlcontext)
			contextid=rscontext(0)
		
		sql="INSERT INTO " & Prefix & "role_assignments (roleid ,contextid ,userid,timestart,timeend,enrol) VALUES "
		sql2=""
		
		Do while not rsAlumnos.eof
				sqlusuario="select id from " & Prefix & "user where username='" & rsAlumnos("codigo_pso") & "'"
				set rsusuario=objconn.execute(sqlusuario)
				if rsusuario.bof = false and rsusuario.eof = false then
						userid=rsusuario(0)
				sql2= sql2 & "('" & rolid & "', '" & contextid & "','" & userid & "', '" & timestart & "', '" & timeend & "' , '" & enrol & "'),"
				end if
				rsAlumnos.movenext
		loop 
		
		if len(sql2) > 0 then
			sql = sql + sql2
		else
			sql = ""
		end if
		
	end if
	'' ************************************************************************
	tam=len(sql)
	if tam > 0 then
		sql=left(sql,tam-1)
		objconn.execute(sql)	
	end if
	
End Sub

'' *************************************************************************
'' INSCRIBIR DOCENTE EN CURSO
'' *************************************************************************

Sub asignarcurso_docente(codigo_per,codcurso,inicio,fin)

	hidden="0"
	timestart=ValorTiempoUnix(inicio)
	timeend=ValorTiempoUnix(fin)
	enrol="manual"
	rolid=3

	'' ***************** DECLARACION DE SQL ***************
	Set ObjAsignar=Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjAsignar.AbrirConexion
	Set rsAsignar=ObjAsignar.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","AD",codcurso,codigo_per)
	ObjAsignar.CerrarConexion
	Set ObjAsignar=nothing

	'' ***************** DECLARACION DE MYSQL ***************
	Dim objConn 
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
	objConn.Open DevuelveCadenaSQLMoodle 
	
	if rsAsignar.eof= false and rsAsignar.bof = false then
		rsAsignar.movefirst 
		
        username  = rsAsignar("codigo_Pso")
		sqlcurso="select id from " & Prefix & "course where shortname='" & codcurso & "'"
		set rscurso=objconn.execute(sqlcurso)
		cursoid=rscurso(0)
	
		sqlcontext="select id from " & Prefix & "context where contextlevel=50 and instanceid='" & cursoid & "'"
		set rscontext=objconn.execute(sqlcontext)
		contextid=rscontext(0)
		
		sql="INSERT INTO " & Prefix & "role_assignments (roleid ,contextid ,userid,timestart,timeend,enrol) VALUES "

		sqlusuario="select id from " & Prefix & "user where username='" & username & "'"
		set rsusuario=objconn.execute(sqlusuario)
		userid=rsusuario(0)

		sql= sql & "('" & rolid & "', '" & contextid & "','" & userid & "', '" & timestart & "', '" & timeend & "' , '" & enrol & "')"
		objconn. execute(sql)	
	end if
	
	 objconn.close
	 set objconn = nothing
End Sub



%>