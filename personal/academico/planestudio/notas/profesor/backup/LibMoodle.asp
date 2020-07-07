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

Sub  crear_curso(categoria,nombre,codigo_cup,inicio,fin,nsemanas,codigo_per)
	
	Dim objConn , objRS , objRS1 , objRS2, objRS3, objRS4
	Dim Prefix 
	Prefix = DevuelvePrefix
	Set objConn = Server.CreateObject("ADODB.Connection") 
		
	objConn.Open DevuelveCadenaSQLMoodle 
	
	'' ***************** SABER QUE EL CURSO NO ESTE AGREGADO ***************
	Set ObjCur=Server.CreateObject("PryUSAT.clsAccesoDatos")
	
	ObjCur.AbrirConexion
		Set rsCurso=ObjCur.Consultar("MOODLE_ConsultarAlumnosParaMoodle","FO","CU",codigo_cup,0)
	
	ObjCur.CerrarConexion
		Set ObjCur=nothing
	if rsCurso.recordcount = 0 then ' curso no existe en Moodle > crear
		
		'Crear curso
		'function crear_curso(categoria,nombre,codigo_cup,inicio,fin,nsemanas)

		idcategoria=categoria
		ahora =   ValorTiempoUnix (now)'fecha y hora del sistema - formato UNIX
		password=""
		fullname=nombre
		shortname=codigo_cup
		idnumber=""
		summary="Resumen de " & nombre
		format="weeks"
		showgrades=1
		modinfo=""
		newsitems=5
		guest=0
		startdate=ValorTiempoUnix(inicio)
		enrolperiod=0
		numsections=nsemanas 'cantidad de semanas
		marker=0
		maxbytes=2097152 '2Mb
		showreports=1
		visible=1
		hiddensections=0
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
		
		sql="insert into " & Prefix & "course (category, sortorder, password, fullname, shortname, summary, format, showgrades, modinfo, newsitems,guest,startdate,enrolperiod, numsections, marker, maxbytes, showreports, visible, hiddensections,lang,theme,cost,timecreated, timemodified, expirythreshold, enrol) values (" & idcategoria & "," & sortorden & ",'','" & fullname & "', '" & shortname & "', '" & summary & "', '" & format & "', " & showgrades & ", '" & modinfo & "', " & newsitems & ", " & guest & "," & startdate & "," & enrolperiod & "," & numsections & "," & marker & "," & maxbytes & "," & showreports & "," & visible & "," & hiddensections & ",'" & lang & "','" & theme & "','" & cost & "'," & timecreacion & "," & timemodificacion & "," & expirythreshold & ",'" & enrol & "')"

			
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

		 blockid="25"
		 position="l"
		 weight="2"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="2"
		 position="l"
		 weight="3"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="9"
		 position="l"
		 weight="4"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="18"
		 position="r"
		 weight="0"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="8"
		 position="r"
		 weight="1"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 blockid="22"
		 position="r"
		 weight="2"
		 sql2="insert into " & Prefix & "block_instance (blockid,pageid,pagetype,position,weight,visible) values (" & blockid & "," & pageid & ",'course-view','" & position & "'," & weight & ",1)"
		 set objrs2 = objconn.execute(sql2)

		 sql3="insert into " & Prefix & "course_sections (course,section,visible) values(" & idcurso & ",0,1)"
		 set objrs3 = objconn.execute(sql3)
         
		 sql4="insert into " & Prefix & "context (contextlevel,instanceid,path,depth) values (50," & idcurso & ",'',2)"
		 set objrs4 = objconn.execute(sql4)
		 
		 '*******************************
		 'para arreglar lo de activar tareas
		 set rs = objConn.execute("select last_insert_id()")
		 idcontext=rs(0)

   		 sql5="update " & Prefix & "context set path =" & "'/1/22/"&idcontext&"' where id = " & idcontext
   		' response.Write(sql5)
   		 
   		 set objrs6 = objconn.execute(sql5)
		 '*******************************
		 
		 objrs.close
		 set objrs = nothing
		 
		 objconn.close
		 set objconn = nothing
	end if
	 
	 ' ******************* MATRICULAR A LOS ALUMNOS *******************
	 call crear_usuarioDocente (codigo_per)  ' --> Ver de donde llama codigo_per
	 call asignarcurso_docente (codigo_per, codigo_cup, inicio, fin)  ' --> Ver de donde llama codigo_per
	 call matricular("", codigo_cup, inicio,fin)
End sub

' **************************************************************************************************

Function ValorTiempoUnix (Byval fecha) 
	ValorTiempoUnix = DateDiff("s", CDate("31/12/1969 00:00:00"), fecha)
End function

' **************************************************************************************************
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

	hidden="0"
	timestart=ValorTiempoUnix(inicio)
	timeend=ValorTiempoUnix(fin)
	enrol="manual"
	rolid=5

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
		objconn.execute(sql)	
	end if
	
	 objconn.close
	 set objconn = nothing
End Sub



%>