<%


	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	codigofoto=obEnc.CodificaWeb("069" & session("codigoUniver_alu"))



        Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
	obj.Abrirconexion

	Set rs = obj.consultar("ConsultarIngresantes","FO",36,0)
%>
<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="70%" id="tbldatosestudiante" class="contornotabla" align =center >
<tr>
    <td colspan = 5> LISTA DE INGRESANTES <%=rs("cicloIngreso")%> </td>
</tr>
   <tr style="font-family: Arial; font-style: normal; color: #FFFFFF; background-color: #0066FF;">
    <td width="10%" align =center  >NRO</td>
    <td width="10%">FOTO</td>
    <td width="10%">CODIGO</td>
    <td width="30%">APELLIDOS Y NOMBRES</td>
    <td width="30%">CARRERA</td>
    <td width="10%">E-MAIL</td>
    
</tr>
<%	
    nro  = 0
    do while not rs.eof
        codigofoto=obEnc.CodificaWeb("069" & rs("codigoUniver_alu"))
        nro = nro +1
%>

       <TR style="font-family: Arial, Helvetica, sans-serif; font-size: 0px; font-weight: normal">
             <td style="font-family: Arial; font-size: x-small" align=center ><%=nro%></td>
             <!--
                '---------------------------------------------------------------------------------------------------------------
                'Fecha: 29.10.2012
                'Usuario: dguevara
                'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                '---------------------------------------------------------------------------------------------------------------
            -->
             <td><img border="0" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="104" height="118" alt="Sin Foto"></td>
             <td style="font-family: Arial; font-size: x-small"><%=rs("codigoUniver_alu")%></td>
             <td style="font-family: Arial; font-size: x-small"><%=rs("alumno")%></td>
             <td style="font-family: Arial; font-size: x-small"><%=rs("nombre_cpf")%></td>
             <td style="font-family: Arial; font-size: x-small"><%=rs("email_alu")%></td>
             
       </TR>
       
       <%
		rs.moveNext

	loop

set obj = Nothing
set obEnc=Nothing

%>