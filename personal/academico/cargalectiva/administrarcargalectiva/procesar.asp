<!--#include file="../../../../NoCache.asp"-->
<!--#include file="../../../asignarvalores.asp"-->
<!--#include file="../../clsmensajes.asp"-->
<%
accion=request.querystring("accion")
pag=request.querystring("pag")
codigo_cup=request.QueryString("codigo_cup")
codigo_pes=request.querystring("codigo_pes")
codigo_cac=request.querystring("codigo_cac")
codigo_dac=request.querystring("codigo_dac")
codigo_per=request.querystring("codigo_per")
codigo_cur=request.querystring("codigo_cur")
codigoElegido=request.querystring("codigoElegido")
Coleccion=split(Request("ListaPara"),",")
usuario=session("Usuario_bit")
pagina=request.querystring("pagina")
th=request.querystring("th")
excedecarga=0
if pagina="" then pagina="detallecarga.asp"
  
	if accion="agregarcargaacademica" then
		coordinador_car=0
		totalhoras_car=0        
		Set ObjCarga= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objCarga.AbrirConexion		    
		    Set rs=ObjCarga.Consultar("ACAD_RetornaDocentesPermitidos","FO",codigo_cup)		    
		    if not(rs.BOF and rs.EOF) then
		        if((rs.fields("docentes") + UBound(Coleccion) + 1) <= rs.fields("nrodocente_cup")) then
		            For I=LBound(Coleccion) to UBound(Coleccion)
				        codigo_uap=Trim(Coleccion(I))
				        call ObjCarga.Ejecutar("AgregarCargaAcademica",false,codigo_uap,codigo_cup,coordinador_car, session("codigo_usu"))
			        Next			 
			    else
			        excedecarga=1
			        response.Write ("<script>alert('Superó el límite de docentes permitidos')</script>")
		        end if		        
			else
			    excedecarga=1
			    response.Write ("<script>alert('Superó el límite de docentes permitidos')</script>")
		    end if
			
		objCarga.CerrarConexion
		Set ObjCarga=nothing		
		response.redirect pagina & "?codigo_cup=" & codigo_cup & "&codigo_dac=" & codigo_dac & "&codigo_cac=" & codigo_cac & "&th=" & th & "&excedecarga=" & excedecarga
	end if
	
	if accion="eliminarcargaacademica" then
		Set ObjCarga= Server.CreateObject("PryUSAT.clsAccesoDatos")
		objCarga.AbrirConexion
			call ObjCarga.Ejecutar("EliminarCargaAcademica",false,codigo_per,codigo_cup,session("codigo_Usu"))
		objCarga.CerrarConexion
		Set ObjCarga=nothing
		response.redirect pagina & "?codigo_cup=" & codigo_cup & "&codigo_dac=" & codigo_dac & "&codigo_cac=" & codigo_cac & "&th=" & th
	end if
	
	if accion="ActualizarHorasCargaAcademica" then
		
		codigo_cup=verificacomaAlfinal(Request.querystring("cursosprogramados"))
		totalhorasaula=verificacomaAlfinal(Request.querystring("totalhorasaula"))
		totalhorasasesoria=verificacomaAlfinal(Request.querystring("totalhorasasesoria"))
		codigo_per=request.querystring("codigo_per")
		codigo_cac=request.querystring("codigo_cac")
		
		ArrCP=split(codigo_cup,",")
		ArrAU=split(totalhorasaula,",")
		ArrAS=split(totalhorasasesoria,",")
		usuario=session("Usuario_bit")

		Set ObjCarga= Server.CreateObject("PryUSAT.clsAccesoDatos")
			ObjCarga.AbrirConexion
			For I=LBound(ArrCP) to UBound(ArrCP)
				cp=Trim(ArrCP(I))
				aula=Trim(ArrAU(I))
				asesoria=Trim(ArrAS(I))
				call ObjCarga.Ejecutar("ActualizarHorasCargaAcademica",false,codigo_cac,CP,codigo_per,aula,asesoria,usuario)
			Next
			ObjCarga.CerrarConexion
		Set ObjCarga=nothing
		response.redirect "frmcursosprofesor.asp?codigo_per=" & codigo_per & "&codigo_cac=" & codigo_cac
	end if
	
	if accion="AsignarDocenteDepartamento" then
		Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
			Obj.AbrirConexion
			For I=LBound(Coleccion) to UBound(Coleccion)
				codigo_per=Trim(Coleccion(I))
				call Obj.Ejecutar("AsignarDocenteDepartamento",false,codigo_per,codigo_dac)
			Next
			Obj.CerrarConexion
		Set Obj=nothing
		if pag="1" then
			response.redirect "frmadscribiradpto.asp?modo=R&codigo_dac=" & codigoElegido
		else
			response.write CerrarPopUp
		end if
	end if	
%>