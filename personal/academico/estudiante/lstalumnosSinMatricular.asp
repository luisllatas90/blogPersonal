<!--#include file="../../../funciones.asp"-->
<% 
dim param

cpf=request.querystring("cpf")
fin=request.querystring("ccini")
inicia=request.querystring("ccfin")
modulo=request.querystring("mod")
resultado=request.querystring("resultado")

codigo_per=request.querystring("id")
if codigo_per="" then codigo_per=session("codigo_usu")

if (resultado="S") then
	Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion	
		Set rsAlumno= obj.Consultar("compara_matriculados_cicloaterior","FO",cpf,inicia,fin)
	obj.CerrarConexion
	set obj= Nothing	
  
  	Dim ArrCampos,ArrEncabezados,ArrCeldas,ArrCamposEnvio
	
	ArrEncabezados=Array("ID","Código","Apellidos y Nombres","Escuela Profesional","Estado Actual","Tiene Deuda")
	ArrCampos=Array("codigo_alu","codigouniver_alu","Alumno","nombre_cpf","estadoactual","estadodeuda")
	ArrCeldas=Array("5%","15%","35%","20%","10%","10%")
	ArrCamposEnvio=Array("codigo_alu","codigouniver_alu")
	pagina="misdatos.asp?tipo=" & tipoResp

	if Not(rsAlumno.BOF and rsAlumno.EOF) then
		alto="height=""98%"""
		resultado="S"
	else
		resultado="N"
	end if
end if
%>
<html>
<head>
<title>Buscar Responsable de Deuda</title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script language="JavaScript" src="../../../private/funciones.js"></script>
<script language="JavaScript" src="private/validaralumno.js"></script>
</head>

<body >
<table width="100%" <%=alto%> border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse" bordercolor="#111111">
    <tr> 
      <td height="5%" colspan="5" valign="top" class="usatTitulo" width="100%">Búsqueda de estudiantes 
          sin matricula</td>
    </tr>    
    <tr>
        <td height="5%" width="15%">No matriculados en: </td>
        <td valign="top" height="5%" width="15%" >                            
            <select name="cboCicloActual">
            <%  Set obj=Server.CreateObject("PryUSAT.clsDatCicloAcademico")
	            Set rsCac= obj.ConsultarCicloAcademico ("RS","TO","")                
	            'call llenarlista("cboCicloActual","",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")
	            Do while not rsCac.eof		            		            	                	                
		            response.Write ("<option value='" & rsCac("codigo_cac") & "'")
		                if(inicia <> "") then
		                    if(CINT(inicia) = CINT(rsCac("codigo_cac"))) then
		                        response.Write(" SELECTED ")
		                    end if                            
                        end if
		            response.Write(">")		            
		            response.Write (rsCac("descripcion_cac"))		            
		            response.Write ("</option>")
		            rsCac.movenext
		        Loop
            %>  
            </select>
        </td>   
        <td>Matriculados en:</td>      
        <td valign="top" width="15%" height="5%">
            <select name="cboCicloAnterior">
            <%  
                rsCac.movefirst
                Do while not rsCac.eof		            
		            response.Write ("<option value='" & rsCac("codigo_cac") & "'")
		                if(fin <> "") then
		                    if(CINT(fin) = CINT(rsCac("codigo_cac"))) then
		                        response.Write(" SELECTED ")
		                    end if                            
                        end if	    
                    response.Write(">")	        
		            response.Write (rsCac("descripcion_cac"))
		            response.Write ("</option>")
		            rsCac.movenext
		        Loop
                
	            'call llenarlista("cboCicloAnterior","",rsCac,"codigo_cac","descripcion_cac",codigo_cac,"","","")
            %>      
            </select>
        </td>        
        <td valign="top" height="5%" width="40%">  </td>     
    </tr>
    <tr> 
      <td  width="15%" height="5%">Buscar por:</td>
      <td colspan="4" valign="top" height="5%">
            <select name="cboCpf" id="cboCpf" style="width:52.6%">
                <option value="0" > --------------------------- [TODOS] --------------------------- </option>             
            <%
                Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	            obj.AbrirConexion	
	                'Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","TO",0)
	                Set rsEscuela= obj.Consultar("ConsultarCarreraProfesional","FO","PG",codigo_per)
		        obj.CerrarConexion	           	           	                
		        
		        Do while not rsEscuela.eof		            
		            response.Write ("<option value='" & rsEscuela("codigo_Cpf") & "'>")
		            response.Write (rsEscuela("nombre_Cpf"))
		            response.Write ("</option>")
		            rsEscuela.movenext
		        Loop
                
	            set obj= Nothing	
            %> 
            </select>  
            <!-- <%call llenarlista("cboCpf","",rsEscuela,"codigo_cpf","nombre_cpf",codigo_cpf,"","S","")%> -->            
            <input name="cmdBuscar" type="button" id="cmdBuscar" value="Buscar" class="usatbuscar" onClick="BuscarAlumnoSinMatricula('<%=modulo%>')" style="height:22px; width:100px" />      
        </td>      
      <!-- <input name="txtParam" type="text" id="txtParam" size="30" class="cajas2" onkeyup="if(event.keyCode==13){BuscarAlumno()}"></td> -->      
      <!--<td valign="top" width="30%" height="5%">
      
       <% if session("codigo_tfu")<> 37 then %>
		  <input name="cmdFichaPre" type="button" class="boton" value="Ficha E-Pre" onClick="AbrirFicha('rpteficha.asp',tab[0])">
      <% end if%> 
	 </td>	--> 
    </tr>
    <%if resultado="S" then%>
    <tr id="trLista">
  		<td width="100%" height="35%" colspan="5">
  		<%
  		call CrearRpteTabla(ArrEncabezados,rsAlumno,"",ArrCampos,ArrCeldas,"S","I",pagina,"S",ArrCamposEnvio,"ResaltarPestana2('0','','')")   
  		%>
  		</td>
	</tr>
	<tr id="trDetalle" >
  		<td width="100%" height="65%" colspan="5">
			<table cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" height="100%">
				<tr height="8%">
					<td class="pestanaresaltada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('0','','');AbrirFicha('misdatos.asp',this)">
                    Datos del Estudiante</td>
					<td width="1%" height="10%" class="bordeinf">&nbsp;</td>
					<% if session("codigo_tfu")<> 37 and session("codigo_tfu")<>13  then %>
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('1','','');AbrirFicha('historial.asp',this)">
                    Historial Académico</td>
                    <% end if%>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
                    <%if (session("codigo_tfu")=1 or session("codigo_tfu")=35 OR session("codigo_tfu")=19 or session("codigo_tfu")=6 or session("codigo_tfu")=18 or session("codigo_tfu")=30 or session("codigo_tfu")=27 ) and session("codigo_tfu") <> 13  then%>
					<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('2','','');AbrirFicha('movimientopagos.asp',this)">
                    Estado de Cuenta</td>
                    <%end if%>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
   					<% if session("codigo_tfu")<> 37 and session("codigo_tfu") <> 13  then %>
			<td class="pestanabloqueada" id="tab" align="center" width="18%" onclick="ResaltarPestana2('3','','');AbrirFicha('detallematricula.asp',this)">
                    Mov. Académicos</td>
                    <% end if%>
                    <td width="1%" height="10%" class="bordeinf">&nbsp;</td>
			<td class="pestanabloqueada" id="tab" align="center" width="13%" onclick="ResaltarPestana2('4','','');AbrirFicha('horario.asp',this)">
                    Horarios</td>
                    <td width="10%" height="10%" class="bordeinf" align="right">
		   <img border="0" src="../../../images/imprimir.gif" style="cursor:hand" ALT="Imprimir Ficha" onClick="ImprimirFicha()">
		    <%if session("codigo_tfu")=1 or session("codigo_tfu")=16 or session("codigo_tfu")=33 or session("codigo_tfu")=7 then%>
                    <img border="0" src="../../../images/editar.gif" style="cursor:hand" ALT="Modificar Datos del Alumno" onClick="AbrirFichaPopUp('frmactualizardatos.asp',this)">
		    <%end if%>
                    <img border="0" src="../../../images/maximiza.gif" style="cursor:hand" ALT="Maximizar ventana" onClick="Maximizar(this,'../../../','100%','65%')">
                    </td>
				</tr>
	  			<tr height="92%">
		    	<td width="100%" valign="top" colspan="10" class="pestanarevez">
					<span id="mensajedetalle" class="usatsugerencia">&nbsp; &nbsp;&nbsp;&nbsp;Elija el estudiante que desea verificar sus datos</span>
					<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0">
					</iframe>
				</td>
			  </tr>
			</table>
  		</td>
  	</tr>
	<%end if%>
  </table>
</body>
</html>