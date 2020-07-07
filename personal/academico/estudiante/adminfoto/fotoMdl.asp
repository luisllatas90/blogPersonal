<%
   
On Error resume next
   
   Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
   Set Obj = Server.CreateObject("PryUSAT.clsAccesoDatos")
   
   
   obj.Abrirconexion
   'set rs = Obj.Consultar("Moodle_FotoAlumno","FO","T","a","a")
   set rsAlumno=Obj.Consultar("Moodle_FotoAlumno","FO","T","username","url","alu")
   
   
   
   response.Write rsAlumno.recordcount 
   do while not rsAlumno.eof      
	    codigofoto = obEnc.CodificaWeb("069" & rsAlumno("ucode"))	    
	    rs = obj.Ejecutar("Moodle_FotoAlumno",false,"U",rsAlumno("username"),codigofoto,rsAlumno("alu"))	    
        response.Write "Cod. Univ. " & codigofoto & " </br>"                
        rsAlumno.movenext        
   loop   
  obj.CerrarConexion
   
  ' set obj=nothing    
  ' set rs = nothing


if err.number<>0 then
response.write Err.Description &"<br>"
end if
%>