<title>registrar</title>
<%
Set conn = Server.CreateObject("ADODB.Connection")
Conn.open "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Server.MapPath("BASE.mdb")
set rs=Server.CreateObject("ADODB.recordset")

mdta = ""
mdta = mdta & request.form("favorita") & "', '" 
mdta = mdta & request.form("favorito") & "', '" 
mdta = mdta & session("codigouniver_alu") & "', '" 
mdta = mdta & now() 


'response.write(mdta & "<br>")

strSQL = "INSERT INTO votacion ("
strSQL = strSQL  & "favorita, "
strSQL = strSQL  & "favorito, "
strSQL = strSQL  & "codigousat, "
strSQL = strSQL  & "fechahora"
strSQL = strSQL  & ") values ('"
strSQL = strSQL  & mdta & "')" 
'response.write("<br>" & strSQL)
conn.Execute(strSQL) 
conn.Close 
set conn = nothing 
Response.Redirect("graciasinteresado.htm")
%>