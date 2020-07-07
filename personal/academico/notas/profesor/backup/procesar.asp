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
				IdCategoria = 0
				
				set ArrNombreCurso = obj.consultar("ConsultarCursoProgramado","FO","21",codigo_cup,"","","")
								 
					if ArrNombreCurso.eof = false and ArrNombreCurso.bof = false  then
						ArrNombreCurso.movefirst
						
						nombre_del_curso = ArrNombreCurso("descripcion_Cac") & " - " & ArrNombreCurso("nombre_cur") & " - " & ArrNombreCurso("grupoHor_Cup")								
						IdCategoria = ArrNombreCurso("idCatAula_cpf")
						
					end if
			crear_curso  IdCategoria , nombre_del_curso  , codigo_cup , "15/08/2011" , "20/12/2011" , 17, codigo_per 			
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

