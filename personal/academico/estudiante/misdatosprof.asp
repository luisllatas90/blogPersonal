<%
On Error Resume Next 
    codigo_alu=request.querystring("codigo_alu")
    'response.Write ("Codigo_alu: " & request.querystring("codigo_alu"))
    codigouniver_alu=request.querystring("codigouniver_alu")
    dim mostrarfichapersonal 
    mostrarfichapersonal = true

If err.Number <> 0 Then ' Si se encuentra un error
Response.Write "Error numero = " & err.Number & "<p>"
Response.Write "Descripcion = " & err.Description & "<p>"
Response.Write "Fuente = " & err.Source
err.Clear 'Limpiamos el error
END IF

accion = Request.Form("accion")
calu = Request.Form("codigoAlu")
if accion="cambiarprograma" and cint(calu) > 0 then
    frm_plan = cint(Request.Form("lstPlanes"))
    frm_cco = cint(Request.Form("lstProgramas"))
    frm_pesAnterior = cint(Request.Form("codigopes_anterior"))    
    frm_ccoAnterior =  cint(Request.Form("codigocco_anterior"))    
    host = request.ServerVariables("REMOTE_HOST") 
    Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
	Obj.Ejecutar "CambioPrograma_Actualizar",false,calu,frm_plan,frm_cco,frm_pesAnterior,frm_ccoAnterior,host,session("codigo_usu")
    Obj.CerrarConexion
	Set Obj=nothing    
	
end if



  
if codigo_alu<>"" then
	Set obEnc=server.createobject("EncriptaCodigos.clsEncripta")
		codigofoto=obEnc.CodificaWeb("069" & codigouniver_alu)
	set obEnc=Nothing

	'Cargar datos para cambiarlos
	Set Obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	Obj.AbrirConexion
		Set rsDatos= Obj.Consultar("CambioPrograma_ConsultarAlumno","FO",codigo_alu)		
	if not(rsDatos.BOF and rsDatos.EOF) then
		alumno=rsDatos("alumno")
		nombre_cpf=rsDatos("nombre_cpf")
		cicloIng_alu=rsDatos("cicloIng_alu")
		descripcion_pes=rsDatos("descripcion_pes")
		nombre_min=rsDatos("nombre_min")				
		codigo_cpf=rsDatos("codigo_cpf")
		programa_actual=rsDatos("descripcion_Cco")	
		codigo_cco=	rsDatos("codigo_Cco")	
		codigo_pes = rsDatos("codigo_pes")	
		'Set rsProgramas = Obj.Consultar("CambioPrograma_ConsultarPP","FO",rsDatos("codigoRaiz_Cco"))
		Set rsProgramas = Obj.Consultar("CambioPrograma_ConsultarPP","FO",rsDatos("codigo_Cco"))
		Set rsPlanes = Obj.Consultar("CambioPrograma_ConsultarPlanEstudio","FO",codigo_cpf)
	end if	    
    Obj.CerrarConexion
	Set Obj=nothing
	
	lstProgramas=""
	selected=""
	if not(rsProgramas.BOF and rsProgramas.EOF) then
	    lstProgramas = lstProgramas & "<select name='lstProgramas' id ='lstProgramas'>"
	    lstProgramas = lstProgramas & "<option value='0' selected=selected><--Seleccione--></option>"
	        do while not rsProgramas.EOF
	                    if  rsProgramas("codigo_Cco") =  codigo_cco then selected=" selected='selected' " else selected ="" end if
	                    lstProgramas = lstProgramas & "<option" & selected & " value='"& rsProgramas("codigo_Cco") & "'>" &  rsProgramas("descripcion_Cco")  & "</option>"
			            rsProgramas.movenext
            loop 
        lstProgramas = lstProgramas &"</select>"
    end if
    
    lstPlanes=""
	if not(rsPlanes.BOF and rsPlanes.EOF) then
	    lstPlanes = lstPlanes & "<select name='lstPlanes' id='lstPlanes'>"
	    lstPlanes = lstPlanes & "<option value='0' selected=selected><--Seleccione--></option>"
	        do while not rsPlanes.EOF
	                    if  rsPlanes("codigo_pes") =  codigo_pes then selected=" selected='selected' " else selected ="" end if
	                    lstPlanes = lstPlanes & "<option" & selected & "  value='"& rsPlanes("codigo_pes") & "'>" &  rsPlanes("descripcion_Pes")  & "</option>"
			            rsPlanes.movenext
            loop 
        lstPlanes = lstPlanes &"</select>"
    end if
	


	
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>misdatos</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<link rel="stylesheet" type="text/css" href="../../../private/estiloimpresion.css" media="print"/>
<script language="JavaScript" src="../../../private/funciones.js"></script>
<style fprolloverstyle>A:hover {color: red; font-weight: bold}
</style>
    <style type="text/css">
        .style1
        {
            width: 24%;
        }
        .style2
        {
            color: #000080;
            font-weight: bold;
            width: 16%;
        }
        .style3
        {
            width: 0%;
        }
        .style4
        {
            color: #000080;
            font-weight: bold;
            width: 15%;
        }
        .style5
        {
            width: 24%;
            height: 25px;
        }
        .style6
        {
            height: 25px;
        }
    </style>
    
    <script type="text/javascript">
        function BtnCambiarPrograma() {
            if (!confirm("¿Está seguro que desea cambiar de programa y/o plan de estudios al estudiante?")) {
                return false;
            } else {

                plan = document.getElementById("lstPlanes").getAttribute("value");
                programa = document.getElementById("lstProgramas").getAttribute("value");
                    if (programa == 0) {
                        alert("Debe seleccionar un programa");
                        return false
                    }else{
                        if (plan == 0) {
                            alert("Debe seleccionar un plan de estudio");
                            return false
                        }
                        else {
                            frmCambiarPrograma.submit();
                        }
                    }
            }           
        }
    </script> 
</head>
<body bgcolor="#EEEEEE">
<table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" id="tbldatosestudiante">
  <tr>
    <td align="center" width="17%" valign="top">
    <!--
        '---------------------------------------------------------------------------------------------------------------
        'Fecha: 29.10.2012
        'Usuario: dguevara
        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
        '---------------------------------------------------------------------------------------------------------------
    -->
        <img border="1" src="//intranet.usat.edu.pe/imgestudiantes/<%=codigofoto%>" width="100" height="118" alt="Sin Foto"> 
        </td>
        <td width="83%" valign="top">
        
        <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
          <tr>
    <td width="90%" colspan="4" class="usatCeldaTitulo" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1"><%=alumno%></td>
          </tr>
          <tr>
    <td style="width: 24%">Código Universitario&nbsp;</td>
    <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=codigouniver_alu%></td>
          </tr>
          <tr>
    <td style="width: 24%">Apellidos y Nombres</td>
    <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=alumno%></td>
          </tr>
          <tr>
    <td style="width: 24%">Escuela Profesional&nbsp;</td>
    <td class="usatsubtitulousuario" width="46%" colspan="3">: <%=nombre_cpf%>&nbsp;</td>
          </tr>
          <tr>
	    	<td style="width: 24%">Ciclo de Ingreso</td>
	    	<td class="usatsubtitulousuario" style="width: 16%">: <%=cicloIng_alu%>&nbsp;</td>
	    	<td width="9%" align="right">Modalidad</td>
	    	<td class="usatsubtitulousuario" width="28%">: <%=nombre_min%>&nbsp;</td>
          </tr>
          <tr>
	    	<td style="width: 24%">Plan de Estudio</td>
	    	<td class="usatsubtitulousuario" width="46%" colspan="3">: <%=descripcion_pes%>&nbsp;</td>
          </tr>

          </table>
    </td>
  </tr>     
  </table>  
  <br />
  <form name="frmCambiarPrograma" id="frmCambiarPrograma" action="misdatosprof.asp?tipo=&codigouniver_alu=<%=codigouniver_alu %>&codigo_alu=<%=codigo_alu%>" method=post>
  <table border="0" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%" class="contornotabla" bgcolor="#FFFFFF">
        <input type="hidden" name="codigoAlu" value=<%=codigo_alu%> />
        <input type="hidden" name="accion" value="cambiarprograma" />
        <input type="hidden" name="codigopes_anterior" value="<%=codigo_pes%>" />
        <input type="hidden" name="codigocco_anterior" value="<%=codigo_cco%>" />
        <tr>
            <td width="90%" colspan="4" class="usatCeldaTitulo" style="border-left-width: 1; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1">
                DATOS DEL PROGRAMA DE PROFESIONALIZACIÓN  
            </td>
        </tr>
        <tr>
           <td style="width: 24%">Programa Actual</td>
           <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=programa_actual%></td>
        </tr>        
        <tr>
           <td style="width: 24%">Programa Nuevo</td>
           <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=lstProgramas%></td>
        </tr>      
         <tr>
           <td style="width: 24%">Plan de Estudios Nuevo</td>
           <td class="usatsubtitulousuario" width="71%" colspan="3">: <%=lstPlanes%></td>
        </tr>   
        
        <tr>
            <td valign="top" width="30%" height="5%">
                <input name="cmdActualizar" type="button" id="cmdActualizar" value="Actualizar" class="usatModificar" onClick="BtnCambiarPrograma();">
            </td>     
        </tr>        
  </table>
  
  </form>
</body>
</html>
<%end if%>