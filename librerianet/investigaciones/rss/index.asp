<% 
    '@yperez 18/04/2013    
    'Generar RSS para Investigaciones USAT 
    f = request.QueryString("f")
    'if (f <> "") then
        Response.ContentType = "text/xml"
        Response.CharSet="UTF-8"  
        xmldoc ="<?xml version='1.0' encoding='UTF-8'?>"
        xmldoc = xmldoc & "<rss version='2.0'>"
        xmldoc = xmldoc & "<channel>"
        xmldoc = xmldoc & "<title> Canal RSS de Investigaciones USAT</title>"
        xmldoc = xmldoc & "<link>https://intranet.usat.edu.pe</link>"
        xmldoc = xmldoc & "<description>Investigaciones USAT</description>"
        xmldoc = xmldoc & "<copyright>USAT - Desarrollo de Sistemas</copyright>"
        xmldoc = xmldoc & Inv_GetItems(cint(f))
        xmldoc = xmldoc & "</channel>"
        xmldoc = xmldoc & "</rss>" 
        response.Write xmldoc
    
    'end if
    
    Function Inv_GetItems(xfacultad)
        Set Obj = Server.CreateObject("PryUSAT.clsAccesoDatos")
	    Obj.AbrirConexion
	    Set rsInv = Obj.Consultar("INV_ConsultarInvestigacionesxFacultadRss","FO",xfacultad,100)     
        cadena = ""
        If Not(rsInv.BOF and rsInv.EOF) then
            Do while not rsInv.EOF
            authors =""
                cadena = cadena & "<item>"
		        cadena = cadena &  "<title>"  & rsInv("xtitle") & "</title>"
		        cadena = cadena &  "<link>"  & rsInv("xlink") & "</link>"
		        cadena = cadena &  "<description>"  & rsInv("xdescription") & "</description>"
		        
		        set rsAut = Obj.Consultar("INV_ConsultarAutoresxInvRss","FO",rsInv("id"))
		        If Not(rsAut.BOF and rsAut.EOF) then
		            authors =""
		            Do while not rsAut.EOF
		                if authors<>"" then authors = authors & " - "
		                authors = authors & rsAut("xauthor")
		                rsAut.movenext
		            loop
		        end if
		        	        
		        cadena = cadena &  "<author>"  &  authors & "</author>"		        
		        cadena = cadena &  "<author>"  &  "aa" & "</author>"	
		        cadena = cadena &  "<category>"  & rsInv("xcategory") & "</category>"
		        cadena = cadena &  "<pubdate>"  & rsInv("xpubdate") & "</pubdate>"
		        cadena = cadena &  "</item>"
		        
		        rsInv.movenext
	        loop	
	    end if   
	         
	    Obj.CerrarConexion
        Set Obj = nothing 
	    Inv_GetItems = cadena
	end function 
 %>