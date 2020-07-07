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

sqlcad="select docidentidad from interesados where docidentidad = '" & request.form("txtdocumento") &"'"
set rs=conn.execute(sqlcad)

response.write(sqlcad)

'rs.open sqlcad, conn,1,2

if (rs.BOF=false and rs.EOF=false) then
	response.Write("<script>alert('Ya se encuentra registrado una persona con el mismo documento de identidad')</script>")
	conn.close
	set conn = nothing 	
	response.write("<script> location.href='http://www.usat.edu.pe/campusvirtual/librerianet/codigo-procesal/inscripcion.asp'</script>")
else
    mdta = ""
    mdta = mdta & request.form("txtnombres") & "', '" 
    mdta = mdta & request.form("txtapellidos") & "', '" 
    mdta = mdta & request.form("txtdireccion")& "', '" 
    mdta = mdta & request.form("txtdistrito")& "', '" 
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
    mdta = mdta & request.form("txtarea") & "', '" 
    mdta = mdta & request.form("txtDireccionCentroLab") & "', '" 
    mdta = mdta & request.form("txtdistrito") & "', '" 
    mdta = mdta & request.form("txttelefonotrab") & "', '" 
    mdta = mdta & request.form("txtfax") & "', '" 
    mdta = mdta & request.form("txtreporta") & "', '" 
    mdta = mdta & request.form("txtmailreporta") & "', '" 
    mdta = mdta & request.form("optdoc") & "', '" 
    mdta = mdta & request.form("txtruc") & "', '" 
    mdta = mdta & request.form("txtfacturara") & "', '" 
    mdta = mdta & request.form("txtDireccionfact") & "', '" 
    mdta = mdta & request.form("optpago") & "', '" 
    mdta = mdta & request.form("txtnrocuotas") & "', '" 
    mdta = mdta & request.form("optreferencias") & "', '" 
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
    strSQL = strSQL  & "area, "
    strSQL = strSQL  & "direccionemp, "
    strSQL = strSQL  & "distritoemp, "
    strSQL = strSQL  & "telefonoemp, "
    strSQL = strSQL  & "faxemp, "
    strSQL = strSQL  & "reportaa, "
    strSQL = strSQL  & "emailemp, "
    strSQL = strSQL  & "facturacion, "
    strSQL = strSQL  & "ruc, "
    strSQL = strSQL  & "facturara, "
    strSQL = strSQL  & "direccionfact, "
    strSQL = strSQL  & "plan, "
    strSQL = strSQL  & "nrocuotas, "
    strSQL = strSQL  & "referencias, "
    strSQL = strSQL  & "fechainscripcion"
    strSQL = strSQL  & ") values ('"
    strSQL = strSQL  & mdta & "')" 

    'response.write("<br>" & strSQL)
    conn.Execute(strSQL) 

    SET rsid =conn.execute("select @@IDENTITY")
    'response.write(rsid(0))
    'Response.Redirect("graciasinteresado.html")
    response.write("<script>alert('Se ha registrado su pre - inscripción satisfactoriamente')</script>")
    'response.write("window.close()")
    response.Redirect("EnviaCorreoPE.aspx?id=" & rsid(0))
    rsid.close
    conn.Close 
    set conn = nothing 
End if%>