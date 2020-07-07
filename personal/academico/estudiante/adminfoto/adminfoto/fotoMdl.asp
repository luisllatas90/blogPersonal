<%   
On error resume next

   Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
   Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
    
   obj.Abrirconexion
   Set rs = obj.consultar("Moodle_FotoAlumno","FO","T")
  ' do while not rs.eof      
	    'codigofoto = obEnc.CodificaWeb("069" & rs("ucode"))	    
	    'obj.consultar("Moodle_FotoAlumno","FO","U",rs("username"),codigofoto)	    
       ' response.Write "Cod. Univ. " &rs("ucode")&" </br>"                
        'rs.movenext
  ' loop
   obj.CerrarConexion
   set obj=nothing    
   set rs = nothing

If Err.Number<>0 then


	response.write Err.Description
end if   
%>