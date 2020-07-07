<%
Function Encode(txt)
    dim x, y, abfrom, abto
    Encode="": ABFrom = ""

    For x = 0 To 25: ABFrom = ABFrom & Chr(65 + x): Next 
    For x = 0 To 25: ABFrom = ABFrom & Chr(97 + x): Next 
    For x = 0 To 9: ABFrom = ABFrom & CStr(x): Next 

    abto = Mid(abfrom, 14, Len(abfrom) - 13) & Left(abfrom, 13)
    For x=1 to Len(txt): y = InStr(abfrom, Mid(txt, x, 1))
        If y = 0 Then
             Encode = Encode & Mid(txt, x, 1)
        Else
             Encode = Encode & Mid(abto, y, 1)
        End If
    Next
End Function 

'texto = trim(Encode("Textocifrado123"))
'response.write "Cifrado: " & texto

dim stringainput
inputstring = "Textocifrado123456"
Response.Write "original: " & inputstring & " [" & Len(inputstring)& "]<br>"
  
dim cryptedstring, decryptedstring
cryptedstring = EnCrypt(inputstring )
decryptedstring = DeCrypt(cryptedstring)
Response.Write "encriptado: " & cryptedstring & " [" & Len(cryptedstring)& "]<br>"
Response.Write "desencriptado: " & decryptedstring & " [" & Len(decryptedstring)& "]<br>"
  
Function EnCrypt(strCryptThis)
  Dim strChar, iKeyChar, iStringChar, i,g_Key, saltKey
  saltKey = "T9z94hFLUT84MLD4XpeJn6Sq4dPRPAbyk6d7VuC2a82A4CYt"
	g_keypos=0
	for i=0 to len(strCryptThis)
		g_Key=g_Key & mid(saltKey,1,g_keypos)
		g_keypos=g_keypos+1
		if g_keypos>len(saltKey) Then g_keypos=0
	next
	for i = 1 to Len(strCryptThis)
		iKeyChar = Asc(mid(g_Key,i,1)) 
		iStringChar = Asc(mid(strCryptThis,i,1))
		iCryptChar = iKeyChar Xor iStringChar
		iCryptCharHex = Hex(iCryptChar)
		iCryptCharHexStr = cstr(iCryptCharHex)
		if len(iCryptCharHexStr)=1 then iCryptCharHexStr = "0" & iCryptCharHexStr
		strEncrypted = strEncrypted & iCryptCharHexStr
	next
	EnCrypt = strEncrypted
End Function
  
Function DeCrypt(strEncrypted)
	Dim strChar, iKeyChar, iStringChar, i,g_Key, saltKey
	saltKey = "T9z94hFLUT84MLD4XpeJn6Sq4dPRPAbyk6d7VuC2a82A4CYt"
	LenGKey=Len(strEncrypted)/2
	g_keypos=0
	For i=0 to LenGKey
		g_Key=g_Key & mid(saltKey,1,g_keypos)
		g_keypos=g_keypos+1
		if g_keypos>len(saltKey) Then g_keypos=0
	Next
	for i = 1 to Len(strEncrypted) /2
		iKeyChar = (Asc(mid(g_Key,i,1))) 
		iStringChar2 = mid(strEncrypted,(i*2)-1,2)
		iStringChar = CLng("&H" & iStringChar2)
		iDeCryptChar = iKeyChar Xor iStringChar
		strDecrypted = strDecrypted & chr(iDeCryptChar)
	next
	DeCrypt = strDecrypted
End Function

%>