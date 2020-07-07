<!--#include file="clscursovirtual.asp"-->
<!--#include file="../../asignarvalores.asp"-->
<%
  idcursovirtual=Request.querystring("idcursovirtual")
  ambito=request.querystring("ambito")
  if ambito="" then ambito="T"
	Dim curso
		Set curso=new clscursovirtual
			with curso
				'Obterner datos del curso virtual
				ArrCurso=.Consultar("3",idcursovirtual,"","")
				If IsEmpty(ArrCurso)=false then
					Fechainicio=ArrCurso(1,0)
					Fechafin=ArrCurso(2,0)
					titulocursovirtual=ArrCurso(3,0)
					moderador=ArrCurso(17,0)
					emailmoderador=Arrcurso(18,0)
		
					if ambito="T" then
						'Obtener participantes del curso virtual
						Set Obj= Server.CreateObject("AulaVirtual.clsUsuario")
				  			arrUsuarios=Obj.Consultar("6","cursovirtual",idcursovirtual,"")
					  	Set Obj= Nothing
					  	
					  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
							for i=Lbound(arrUsuarios,2) to Ubound(arrUsuarios,2)
								clave=trim(arrUsuarios(6,i))
								if IsNull(clave) then clave=""
								a=Obj.enviardatosacceso(arrUsuarios(8,i),arrUsuarios(3,i),arrUsuarios(1,i),clave,arrUsuarios(7,i),titulocursovirtual,moderador,emailmoderador)
							next
						Set obj=nothing
						response.write "<script>alert('Se han enviado los datos de acceso a los participantes del curso');window.opener.location.reload();top.window.close()</script>"
					else
						ControlesEnvioDatosAcceso		
					  	Set Obj= Server.CreateObject("AulaVirtual.clsCursoVirtual")
						For I=LBound(ArrCodUsu) to UBound(ArrCodUsu)
							idtipousuario=Trim(ArrTipoUsu(I))
							idusuario=Trim(ArrCodUsu(I))
							nombreusuario=Trim(ArrNomUsu(I))
							clave=Trim(ArrClaveUsu(I))
							email=Trim(ArrEmailUsu(I))
							a=Obj.enviardatosacceso(idtipousuario,idusuario,nombreusuario,clave,email,titulocursovirtual,moderador,emailmoderador)
						Next
						Set obj=nothing
						response.write "<script>alert('Se han enviado los datos de acceso a los participantes del curso');top.window.close()</script>"						
					end if
				end if
			end with
		Set curso=nothing
		
%>