
<!--#include file="LibMoodle.asp"-->
<%
On error Resume next
accion=request.querystring("accion")
if accion="agregarcursovirtual" then 'Matricular en curso de la usat
		dim mensaje,raiz,carpeta
			tipo=request.form("chkagrupar")
			if tipo="" then tipo="I"
			ArrCP=split(request.form("chkcursoshabiles"),",")
			codigo_per=request.querystring("codigo_per")
			codigo_cac=request.querystring("codigo_cac")
			login_per=request.querystring("login_per")
			raiz=Server.MapPath("../../../archivoscv/")
			'raiz="t:\documentos aula virtual\archivoscv"
			
			Set fso = CreateObject("Scripting.FileSystemObject")
			Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			Set objCurso=Server.CreateObject("AulaVirtual.clsCursoVirtual")
			
			obj.AbrirConexion
			redim mensaje(Ubound(arrCP))
			for i=lbound(ArrCP) to Ubound(ArrCP)
			    codigo_cup=trim(ArrCP(i))
				if codigo_cup="" then codigo_cup=0
				mensaje(i)=Obj.Ejecutar("AgregarCursoVirtual",true,tipo,codigo_per,codigo_cup,codigo_cac,login_per,0,null)
				
'			    reponse.write(tipo & " ," & codigo_per& " ," & codigo_cup& " ," & codigo_cac& " ," & login_per& " ," & 0& " ," & "<br/>")
			    
			    arrCurso=ObjCurso.Consultar(5,codigo_cup,0,0)
				carpeta=raiz & "/" & arrCurso(0,0)
				
				  'Verificar si existe la carpeta con el curso virtual
			      If (Not fso.FolderExists(carpeta)) then
				        Set fol = fso.CreateFolder(carpeta)
						Set fol = fso.CreateFolder(carpeta & "\documentos")
				        Set fol = fso.CreateFolder(carpeta & "\tareas")
			    		Set fol = fso.CreateFolder(carpeta & "\images")
				  End if
				'response.write carpeta
			next
			Obj.CerrarConexion
			Set Obj=nothing
			Set ObjCurso=nothing
			Set fso=nothing
			Set fol=nothing
			
			response.write "<link rel=""stylesheet"" type=""text/css"" href=""../../../private/estilo.css"">"
			response.write "<table border='0' cellpadding='3' class='contornotabla' cellspacing='0' style='border-collapse: collapse' width='100%'>"
			response.write "<tr><td align='center' class='usattitulousuario'><u>Reporte de cursos habilitados</u></td></tr>"
			response.write "<tr><td align='right' style='cursor:hand' onClick='history.back(-1)'><i>Haga click aquí para Regresar a la Página anterior</i></td></tr>"
			response.write "<tr><td width='100%'><ul>"
			for j=lbound(mensaje) to ubound(mensaje)
				response.write "<li>" & mensaje(j) & "</li>"
			next
			response.write "</ul></td></tr>"
			response.write "</table>"
end if

if accion="agregarcursoMoodle" then
			tipo=request.form("chkagrupar")
			
			if tipo="" then tipo="I"
			
			ArrCP=split(request.form("chkcursoshabiles"),",")
			
			codigo_per=request.querystring("codigo_per")
			
			codigo_cac=request.querystring("codigo_cac")
		
			login_per=request.querystring("login_per")
			
			secfor = request.Form("avformatsections")
			
		    secnum = request.Form("avnumbersections")
		    
        	'raiz="t:\documentos aula virtual\archivoscv"

			raiz=Server.MapPath("../../../../documentos aula virtual/archivoscv/")
	
			Set fso = CreateObject("Scripting.FileSystemObject")
	        
	        
			Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
			
			Set objCurso=Server.CreateObject("AulaVirtual.clsCursoVirtual")
			obj.AbrirConexion
			redim mensaje(Ubound(arrCP))
			for i=lbound(ArrCP) to Ubound(ArrCP)
				codigo_cup=trim(ArrCP(i))
				if codigo_cup="" then codigo_cup=0
				Dim nombre_del_curso
				Dim IdCategoria 
				dim codigo_pes
				dim codigo_test
				IdCategoria = 0
				codigo_pes = 0
				codigo_test = 0
				
				set ArrNombreCurso = obj.consultar("ConsultarCursoProgramado","FO","24",codigo_cup,"","","")
				    
				    
					if ArrNombreCurso.eof = false and ArrNombreCurso.bof = false  then
					
						ArrNombreCurso.movefirst
						inicioCup =	ArrNombreCurso("fechainicio_cup")
				        finCup    =	ArrNombreCurso("fechafin_cup")
					    nombre_del_curso = ArrNombreCurso("descripcion_Cac") & " - " & ArrNombreCurso("nombre_cur") & " - " & ArrNombreCurso("grupoHor_Cup")								
					    
					    if 	ArrNombreCurso("codigo_test") = 2 or ArrNombreCurso("codigo_test")= 7 or ArrNombreCurso("codigo_test")= 3  or ArrNombreCurso("codigo_test")= 4  or ArrNombreCurso("codigo_test")= 1 then
							IdCategoria = ArrNombreCurso("idCatAula_cpf")
							codigo_test = ArrNombreCurso("codigo_test")
						elseif ArrNombreCurso("codigo_test") = 6 or ArrNombreCurso("codigo_test") = 5 or ArrNombreCurso("codigo_test") = 8 then
                           codigo_pes = cint(ArrNombreCurso("codigo_pes"))
                           IdCategoria = 0
                           codigo_test = ArrNombreCurso("codigo_test")
						end if
					end if
					
			crear_curso  IdCategoria , nombre_del_curso  , codigo_cup , inicioCup ,finCup, secfor,secnum, codigo_per, codigo_test, codigo_pes,ArrNombreCurso("grupoHor_cup"),ArrNombreCurso("descripcion_Pes"),ArrNombreCurso("nombre_cpf"),codigo_cac,ArrNombreCurso("ciclo_cur")
			    
			crear_asistencia codigo_cup ,ArrNombreCurso("descripcion_Cac")
			
			do while not ArrNombreCurso.EOF
			    asignarcurso_docente ArrNombreCurso("codigo_Per"), codigo_cup, ArrNombreCurso("fechainicio_cup"),ArrNombreCurso("fechafin_cup")
			    ArrNombreCurso.movenext
            loop 
            
            matricular "", codigo_cup, inicioCup , finCup
            
			mensaje(i) = "Registro Correcto : " & nombre_del_curso
			next
			
			Obj.CerrarConexion
			Set Obj=nothing
			Set ObjCurso=nothing
			Set fso=nothing
			Set fol=nothing
			
			response.write "<link rel=""stylesheet"" type=""text/css"" href=""../../../private/estilo.css"">"
			response.write "<table border='0' cellpadding='3' class='contornotabla' cellspacing='0' style='border-collapse: collapse' width='100%'>"
			response.write "<tr><td align='center' class='usattitulousuario'><u>Reporte de cursos habilitados</u></td></tr>"
			'response.write "<tr><td align='right' style='cursor:hand' onClick='history.back(-1)'><i>Haga click aquí para Regresar a la Página anterior</i></td></tr>"
			response.write "<tr><td width='100%'><ul>"

			for j=lbound(mensaje) to ubound(mensaje)
				response.write "<li>" & mensaje(j) & "</li>"
			next
			response.write "</ul></td></tr>"
			response.write "</table>" 
end if

%>

