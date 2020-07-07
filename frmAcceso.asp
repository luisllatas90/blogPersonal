<%
    on Error resume next            
    'Dim blnResultado 
    blnResultado = false
    semilla = Session.SessionID
    '******************** Desencriptando Token ********************
    if(session("tkn") <> "") then
        dato1 = Left(session("tkn"), 2) 
        dato2 = Mid(session("tkn"), 5, Len(session("tkn")) - 10)
        dato3 = Left(Right(session("tkn"), 4), 2)
        
        semillaReversa = dato3 & dato2 & dato1 
        
        if(StrReverse(semillaReversa) = semilla) then
            blnResultado = true
        end if                
    end if
    '**************************************************************
    
    tipo = request.Form("cbxtipo") 
    usuario = request.Form("Login") 
    if(blnResultado = true) then 
        if(tipo <> "") then 
            if(tipo = "A") then            
                clave = request.Form("Clave") 
                if(usuario <> "" and clave <> "") then                                       
                    response.Redirect "estudiante/asignaAcceder.asp?param1=" & Base64Encode(usuario) & "&param2=" & Base64Encode(clave)
                else
                    response.Redirect("index.asp")    
                end if            
            elseif(tipo = "P") then              
                response.Redirect "personal/acceder.asp?cbxtipo=P"
            end if
        else
            response.Redirect("index.asp")
        end if
    else
        response.Redirect("index.asp")
    end if    
    
    
    
    Function MyASC(OneChar)
        If OneChar = "" Then MyASC = 0 Else MyASC = Asc(OneChar)
    End Function
    
    Function Base64Encode(inData)
        'rfc1521
        '2001 Antonin Foller, Motobit Software, http://Motobit.cz
         
        Const Base64 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"
        Dim cOut, sOut, I
         
        'For each group of 3 bytes
         
        For I = 1 To Len(inData) Step 3
        Dim nGroup, pOut, sGroup
         
        'Create one long from this 3 bytes.
         
        nGroup = &H10000 * Asc(Mid(inData, I, 1)) + _
        &H100 * MyASC(Mid(inData, I + 1, 1)) + MyASC(Mid(inData, I + 2, 1))
         
        'Oct splits the long To 8 groups with 3 bits
         
        nGroup = Oct(nGroup)
         
        'Add leading zeros
         
        nGroup = String(8 - Len(nGroup), "0") & nGroup
         
        'Convert To base64
         
        pOut = Mid(Base64, CLng("&o" & Mid(nGroup, 1, 2)) + 1, 1) + _
        Mid(Base64, CLng("&o" & Mid(nGroup, 3, 2)) + 1, 1) + _
        Mid(Base64, CLng("&o" & Mid(nGroup, 5, 2)) + 1, 1) + _
        Mid(Base64, CLng("&o" & Mid(nGroup, 7, 2)) + 1, 1)
         
        'Add the part To OutPut string
         
        sOut = sOut + pOut
         
        'Add a new line For Each 76 chars In dest (76*3/4 = 57)
        'If (I + 2) Mod 57 = 0 Then sOut = sOut + vbCrLf
         
        Next
        Select Case Len(inData) Mod 3
        Case 1: '8 bit final
         
        sOut = Left(sOut, Len(sOut) - 2) + "=="
        Case 2: '16 bit final
         
        sOut = Left(sOut, Len(sOut) - 1) + "="
        End Select
        Base64Encode = sOut
    End Function
    
    
	if(Err.number <> 0) then
	    response.Write Err.Description
	    'response.Redirect("index.asp")
	end if	
%>