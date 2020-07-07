<%
	public function getClientip()
		dim ret
		ret=Request.ServerVariables("REMOTE_HOST")
		getClientip=ret
		
	end function
	public function servertime()
		dim Ret
		Ret=now()
		servertime=Ret
	end function
	
	'x: Codigo Universitario    -   y: Clave
	public function datos(x, y)
		on error resume next
		    'x -> Convertir a Base64
            'y -> Convertir a SHA1
            'Concatenar x + y
            'Retornar Valor
		if(Err.number <> 0) then
        '----------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: yperez
        'Motivo: Cambio de URL del servidor de la WebUSAT [www.usat.edu.pe->intranet.edu.pe]
        '----------------------------------------------------------------------
		 response.Redirect "https://intranet.usat.edu.pe/"
		end if        
    end function
%>