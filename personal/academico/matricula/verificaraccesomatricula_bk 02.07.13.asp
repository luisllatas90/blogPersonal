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
	
	Set ObjBloqueo=Server.CreateObject("PryUSAT.clsAccesoDatos")
	ObjBloqueo.AbrirConexion
	Set rsBloqueo=ObjBloqueo.Consultar("MAT_BloqueoCampusAsesor","FO",codigo_alu,Codigo_Cac)
	ObjBloqueo.CerrarConexion
	set ObjBloqueo = nothing
	
	Set Obj=Server.CreateObject("PryUSAT.clsAccesoDatos")
		Obj.AbrirConexion
		'Permitir solo administrador para Juan Carlos Iberico, Hugo Saavedra y Carlo Senmache
		'if(session("codigo_Usu") = 2316 OR session("codigo_Usu") = 684 OR session("codigo_Usu") = 1290) then
		    'codigo_tfu = 1
		    'response.Redirect(rutaActual)    
		'else 
		    'codigo_tfu = 2
		'end if			        
					    	    
	    if (rsBloqueo.BOF and rsBloqueo.EOF) then
            pagina = "administrar/frmadminmatricula.asp?apto=S&codigo_alu="&codigo_alu
        else
            set rsMatricula=Obj.Consultar("ConsultarAccesoMatriculaPorAsesor","FO",codigo_alu,Codigo_Cac,Codigo_Pes,rutaActual,session("codigo_tfu"))		
	        pagina=rsMatricula(0)
	        rsMatricula.close
	        Set rsMatricula=nothing
	    end if		
	Obj.CerrarConexion
	Set Obj=nothing	        
	'response.Write(codigo_alu & ", " & Codigo_Cac & ", " & Codigo_Pes & ", " & rutaActual & ", " & session("codigo_tfu"))
    'response.Write(pagina)
	response.redirect(pagina)
	'response.write (codigo_alu & "," & Codigo_Cac & "," & Codigo_Pes & "," & rutaActual & "," & session("codigo_tfu"))
end if
%>