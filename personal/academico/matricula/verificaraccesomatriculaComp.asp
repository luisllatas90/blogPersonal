<%
rutaActual=request.querystring("rutaActual")

codigo_alu=session("codigo_alu")
codigo_cac=session("codigo_cac")
codigo_pes=session("codigo_pes")
estadoDeuda_alu=session("estadoDeuda_alu")
UltimoEstado=session("UltimoEstado")
codigo_cpf=session("codigo_cpf")

If 	estadodeuda_alu=1 and _
	UltimoEstadoMatricula="N" and _
	codigo_cpf<>"25"  then

	response.redirect "mensajes.asp?proceso=" & modo
else
	'****************************************
	'Verificar acceso a pre-matrícula
	'****************************************
	
	'Set ObjBloqueo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	'ObjBloqueo.AbrirConexion
	'Set rsBloqueo=ObjBloqueo.Consultar("MAT_BloqueoCampusAsesorComp","FO",codigo_alu,Codigo_Cac, 4)
	'ObjBloqueo.CerrarConexion
	'set ObjBloqueo = nothing
	
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
	
	    set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesorComp","FO",codigo_alu,Codigo_Cac,Codigo_Pes,rutaActual,session("codigo_tfu"),4)		
        pagina=rsMatricula(0)
        rsMatricula.close
        Set rsMatricula=nothing
	        
	Obj.CerrarConexion
	Set Obj=nothing	        
	'response.Write(codigo_alu & ", " & Codigo_Cac & ", " & Codigo_Pes & ", " & rutaActual & ", " & session("codigo_tfu"))
	'response.end
    'response.Write(pagina)
	'response.redirect(pagina)
	
	 if(pagina <> "") then
        response.Redirect(pagina)
    end if
    

	Set ObjBloqueo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjBloqueo.AbrirConexion
	Set rsBloqueo=ObjBloqueo.Consultar("ConsultarAccesoMatriculaPorAsesorBloqueoAlu_2015","FO",codigo_alu,Codigo_Cac,rutaActual,session("codigo_tfu"))
	pagina2=rsBloqueo(0)
	ObjBloqueo.CerrarConexion
	set ObjBloqueo = nothing
	'response.Write(pagina)
	
	if (pagina2="") then		    
		response.redirect(pagina)
	else	    
		response.redirect(pagina2)
	end if
	'response.write (codigo_alu & "," & Codigo_Cac & "," & Codigo_Pes & "," & rutaActual & "," & session("codigo_tfu"))
end if
%>