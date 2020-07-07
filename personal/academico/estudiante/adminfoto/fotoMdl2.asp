<%
   
On Error resume next
   
   'Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
   Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
   response.Write "C"   
   dim rs 
   obj.Abrirconexion
   'set rs = Obj.Consultar("Moodle_FotoAlumno","FO","T","a","a")
   set rsAlumno=Obj.Consultar("Moodle_FotoAlumno","FO","T","a","a")
   response.Write "D <br/>"  
   
   
   'response.Write rs.recordcount 
   'do while not rs.eof      
	    'codigofoto = obEnc.CodificaWeb("069" & rs("ucode"))	    
	    'obj.consultar("Moodle_FotoAlumno","FO","U",rs("username"),codigofoto)	    
      '  response.Write "Cod. Univ. " &rs("ucode")&" </br>"                
    '    rs.movenext
        
   'loop
   
  obj.CerrarConexion
  response.Write "E"   
  ' set obj=nothing    
  ' set rs = nothing


if err.number<>0 then

response.write Err.Description &"<br>"
end if
%>