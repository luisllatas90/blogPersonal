<%
on error resume next
Set ObjCif = Server.CreateObject("PryCifradoNet.ClscifradoNet")
id = ObjCif.cifrado(trim(session("Ident_Usu") & "16668"), "EncuestaEstudiante")
cup = ObjCif.cifrado(trim(session("Ident_Usu") & "133470"), "EncuestaEstudiante")
response.Write("ID = "&id)
response.Write("CUP = "&id)
response.Write("Ident_Usu = "&session("Ident_Usu"))

%>


