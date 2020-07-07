<META HTTP-EQUIV="Cache-Control" CONTENT ="no-cache"> 
<%
Set conn = Server.CreateObject("ADODB.Connection")
Conn.open "DRIVER={Microsoft Access Driver (*.mdb)}; DBQ=" & Server.MapPath("base.mdb")
set rs=Server.CreateObject("ADODB.recordset")
'response.Write(request.Form("chkBoleta"))
'if request.Form("optFactura") <> "" then
'	documento = "F"
'elseif request.Form("chkBoleta") <>"" then
'	documento = "B"
'end if

dim Prejornada
dim Jornada
Prejornada=0
Jornada=0
if request.form("Prejornada")<>"" then Prejornada=1
if request.form("Jornada")<>"" then Jornada=1

sqlcad="select docidentidad from interesados where docidentidad = '" & request.form("txtdocumento") &"'"
set rs=conn.execute(sqlcad)
'response.write(sqlcad)
	'rs.open sqlcad, conn,1,2

if (rs.BOF=false and rs.EOF=false) then
	response.Write("<script>alert('Ya se encuentra registrado una persona con el mismo documento de identidad')</script>")
	conn.close
	set conn = nothing 	
	response.write("<script> top.location.href='pag_04.htm'</script>")	
else
    mdta = ""
    mdta = mdta & request.form("txtnombres") & "', '" 
    mdta = mdta & request.form("txtapellidos") & "', '" 
    mdta = mdta & request.form("txtdireccion")& "', '" 
    mdta = mdta & request.form("txtdistritodir")& "', '" 
    mdta = mdta & request.form("txttelefono") & "', '" 
    mdta = mdta & request.form("txtcelular") & "', '" 
    mdta = mdta & request.form("txtemail") & "', '" 
    mdta = mdta & request.form("cbodia") & "/" &  request.form("cbomes") & "/" & request.form("cboanio") & "', '" 
    mdta = mdta & request.form("optsexo") & "', '" 
    mdta = mdta & request.form("txtdocumento") & "', '" 
    mdta = mdta & request.form("txtestudios1") & "', '" 
    mdta = mdta & request.form("txtinstitucion1") & "', '" 
    mdta = mdta & request.form("txtdesde1") & "', '" 
    mdta = mdta & request.form("txttitulo1") & "', '" 
    mdta = mdta & request.form("txtestudios2") & "', '" 
    mdta = mdta & request.form("txtinstitucion2") & "', '" 
    mdta = mdta & request.form("txtdesde2") & "', '" 
    mdta = mdta & request.form("txttitulo2") & "', '" 
    mdta = mdta & request.form("txtcentrolaboral") & "', '" 
    mdta = mdta & request.form("txtcargo") & "', '" 
	mdta = mdta & request.form("txttelefonotrab") & "', '" 
    mdta = mdta & request.form("txtDireccionCentroLab") & "', '" 
    mdta = mdta & request.form("txtmailreporta") & "', '" 
    mdta = mdta & request.form("optpago") & "', '" 
    mdta = mdta & Prejornada  & "', '" 
    mdta = mdta & Jornada & "', '" 
    mdta = mdta & now() 

    'response.write(mdta & "<br>")

    strSQL = "INSERT INTO interesados ("
    strSQL = strSQL  & "nombres, "
    strSQL = strSQL  & "apellidos, "
    strSQL = strSQL  & "direccion, "
    strSQL = strSQL  & "distrito, "
    strSQL = strSQL  & "telfdomicilio, "
    strSQL = strSQL  & "telfcelular, "
    strSQL = strSQL  & "email, "
    strSQL = strSQL  & "fechanac, "
    strSQL = strSQL  & "sexo, "
    strSQL = strSQL  & "docidentidad, "
    strSQL = strSQL  & "estudios1, "
    strSQL = strSQL  & "institucion1, "
    strSQL = strSQL  & "desde1, "
    strSQL = strSQL  & "titulo1, "
    strSQL = strSQL  & "estudios2, "
    strSQL = strSQL  & "institucion2, "
    strSQL = strSQL  & "desde2, "
    strSQL = strSQL  & "titulo2, "
    strSQL = strSQL  & "empresa, "
    strSQL = strSQL  & "cargo, "
    strSQL = strSQL  & "telefonoemp, "
    strSQL = strSQL  & "direccionemp, "
    strSQL = strSQL  & "emailemp, "
    strSQL = strSQL  & "planpago, "
	strSQL = strSQL  & "Prejornada, "
	strSQL = strSQL  & "Jornada, "
    strSQL = strSQL  & "fechainscripcion"
    strSQL = strSQL  & ") values ('"
    strSQL = strSQL  & mdta & "')" 

    'response.write("<br>" & strSQL)
    conn.Execute(strSQL) 

'    SET rsid =conn.execute("select @@IDENTITY")
    'response.write(rsid(0))
    'Response.Redirect("graciasinteresado.html")
    response.write("<script>alert('Se ha registrado su pre - inscripción satisfactoriamente')</script>")
        'response.Redirect("EnviaCorreoPE.aspx?id=" & rsid(0))
	     response.write("<script> top.location.href='pag_04.htm'</script>")	
    'rsid.close
    'conn.Close 
    set conn = nothing 
End if%>