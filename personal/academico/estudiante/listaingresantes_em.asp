<%
    'dim ema as string
	'dim cad1 as string
	'dim cad2 as string
	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
	codigofoto=obEnc.CodificaWeb("069" & session("codigoUniver_alu"))
    Set obj=server.CreateObject("PryUSAT.clsAccesodatos")
    obj.Abrirconexion
    Set rs1 = obj.consultar("dbo.ConsultarCarreraProfesional","FO","MA", 0)	
    Set rs = obj.consultar("dbo.ACAD_ConsultarIngresantes_est_mat","FO",36,0,"T",0)
    if len(Request.Querystring("filtro_dma")) > 0 or len(Request.Querystring("filtro_escuela")) > 0 then
	       if (Request.Querystring("filtro_dma") = "Nada" or Request.Querystring("filtro_dma") = "T") and Request.Querystring("filtro_escuela") = 0 then
		       Set rs = obj.consultar("dbo.ACAD_ConsultarIngresantes_est_mat","FO",36,0,"T",0)
		   else
               Set rs = obj.consultar("dbo.ACAD_ConsultarIngresantes_est_mat","FO",36,0,Request.Querystring("filtro_dma"),Request.Querystring("filtro_escuela"))
           end if			   
    else	
           Set rs = obj.consultar("dbo.ACAD_ConsultarIngresantes_est_mat","FO",36,0,"T",0)
	End if   
        Response.write("Si no se realiza consulta vuelva a insistir")

%>

<form method="GET" action ="listaingresantes_em.asp">
	<p>
	Estado:
	<select size="1" name="filtro_dma">
	<option value="Nada"></option>
	<option value="M">Matriculados</option>
	<option value="R">Retirados</option>
	<option value="T">Todos</option>
	</select>
	Escuela:
	<select size="1" name="filtro_escuela">
	<option value ="0">TODAS</option>
	<% do while not rs1.eof %>
         <option value="<% response.write(rs1("codigo_cpf")) %>"><% response.write(rs1("nombre_cpf"))%></option>
  	<% rs1.moveNext %>
	<% loop  %>
	<% rs1.close %>
	
	
	</select>
	<input type="submit" value="Visualizar" name="ver"><input type="reset" value="Limpiar" name="limpiar">
	</p>


<table border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="70%" id="tbldatosestudiante" class="contornotabla" align =center >

<tr>
    <td colspan = 7><center> LISTA DE INGRESANTES <%=rs("cicloIng_Alu")%>  CON ESTADO DE MATRICULA &nbsp;
    <%
    if Len(Request.Querystring("filtro_dma")) > 0 then response.write(Request.Querystring("filtro_dma"))
    if Len(Request.Querystring("filtro_escuela")) > 0  then response.write(" - " & rs("nombre_cpf"))
    %>
    
    </center></td>
</tr>

   <tr style="font-family: Arial; font-style: normal; color: #FFFFFF; background-color: #0066FF;">
    <td style="font-family: Arial; font-size: 10px" width="5%" align =center  >NRO</td>
    <td style="font-family: Arial; font-size: 10px" width="10%">FOTO</td>
    <td style="font-family: Arial; font-size: 10px" width="10%">CODIGO</td>
    <td style="font-family: Arial; font-size: 10px" width="23%">APELLIDOS Y NOMBRES</td>
    <td style="font-family: Arial; font-size: 10px" width="12%">CARRERA</td>
    <td style="font-family: Arial; font-size: 10px" width="20%">E-MAIL</td>
    <td style="font-family: Arial; font-size: 10px" width="9%">EST.MATRIC</td> 
    
</tr>

<%	
    nro  = 0
    do while not rs.eof
        codigofoto=obEnc.CodificaWeb("069" & rs("codigoUniver_alu"))
        nro = nro +1
%>

       <TR style="font-family: Arial, Helvetica, sans-serif; font-size: 0px; font-weight: normal">
             <td style="font-family: Arial; font-size: 10px" align=center ><%=nro%></td>
             <!--
                    '---------------------------------------------------------------------------------------------------------------
                    'Fecha: 29.10.2012
                    'Usuario: dguevara
                    'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                    '---------------------------------------------------------------------------------------------------------------
               
             -->
             <td><img border="0" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="104" height="118" alt="Sin Foto"></td>
             <td style="font-family: Arial; font-size: 10px"><%=rs("codigoUniver_alu")%></td>
             <td style="font-family: Arial; font-size: 10px"><%=rs("alumno")%></td>
             <td style="font-family: Arial; font-size: 10px"><%=rs("nombre_cpf")%></td>
             <td style="font-family: Arial; font-size: 10px">
			 <%
			  ema =rs("email_alu")
			  l = len(ema)
			  x = instr(ema,"@")
			  x = abs(x - 1)
			  y = abs(l - x)
			  response.write(left(ema,x))
			  response.write("<BR>")
			  response.write(right(ema,y)) 
			  %>
			 </td>
             <td style="font-family: Arial; font-size: 10px"><center><%=rs("estado_dma")%></center></td>
           
             
       </TR>
       
       <%
		rs.moveNext
  	loop

set obj = Nothing
set obEnc=Nothing
%>
</table>
</form>