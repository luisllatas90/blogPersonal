<!--#include file="../../../funciones.asp"-->
<%
on error resume next
codigo_usu=session("codigo_usu")
codigo_tfu=session("codigo_tfu")
codigo_sco=request.querystring("codigo_sco")
codigo_cco=request.querystring("codigo_cco")
fechainicio=request.querystring("fechainicio")
fechafin=request.querystring("fechafin")

if codigo_cco="" then codigo_cco="-2"
if codigo_sco="" then codigo_sco="-2"

if fechainicio="" then fechainicio= dateadd("m",-12,date)
if fechafin="" then fechafin=date

Set obj= Server.CreateObject("PryUSAT.clsAccesoDatos")
	obj.AbrirConexion
		
		
		'Consulta los cecos
		set rsCeco  = obj.Consultar("presu_consultarcentrocostos","FO","U", codigo_usu)
		
		
		
		'Consulta los servicios
		  Set rsServicio= obj.Consultar("PRESU_ConsultarServiciosEnDeuda","FO",codigo_cco)
		
		
		if codigo_cco<>"-2"  and codigo_sco>= 0 then
			
			Set rsMoneda=obj.Consultar("ConsultarServicioConcepto","FO","CO",codigo_sco)
			
			Set rsClientes=obj.Consultar("PRESU_ConsultarDeudasXCobrar","FO",0,fechainicio,fechafin,codigo_sco,0,codigo_cco,"","",-1,"","")		
			
			
			if Not(rsServicio.BOF and rsServicio.EOF) then
          		'moneda=ucase(rsMoneda("descripcion_tip"))
				HayReg=true
				
			end if
		end if
		
	obj.CerrarConexion
set obj= Nothing
%>
<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Language" content="es">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<title>Deudas por cobrar</title>
<link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
<script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
<script type="text/javascript" language="JavaScript" src="../../../private/calendario.js"></script>
<script type="text/javascript" language="javascript">


    function cargarServicios() {

        if (cbocodigo_cco.value == -2) {
            alert("Debe seleccionar un centro de costos primero");

        }
        else {
                pagina = "../administrativo/controlpagos/vstdeudascobrar_v2.asp?codigo_sco=" + cbocodigo_sco.value + "&fechainicio=" + txtFechaInicio.value + "&fechafin=" + txtFechaFin.value + "&moneda=S" + "&codigo_cco=" + cbocodigo_cco.value
                location.href = "../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
        }

    }


	function BuscarDeudas()
	{

	  if (cbocodigo_cco.value==-2){ 	
		alert("Debe seleccionar un centro de costos primero");	    

	  }
	  else{

	      if (cbocodigo_sco.value == -1) {
	          alert("Debe seleccionar un servicio primero");	    
	      }
	      else {
	          pagina = "../administrativo/controlpagos/vstdeudascobrar_v2.asp?codigo_sco=" + cbocodigo_sco.value + "&fechainicio=" + txtFechaInicio.value + "&fechafin=" + txtFechaFin.value + "&moneda=S" + "&codigo_cco=" + cbocodigo_cco.value
	          location.href = "../../aplicacionweb2/cargando.asp?rutapagina=" + pagina
	      }
 	   }

	}

</script>
</head>

<body bgcolor="#EEEEEE">
<p class="usatTitulo">Deudas por cobrar</p>
<%if rsCeco.BOF and rsCeco.EOF then%>
	<h5>UD. no tiene permiso para acceder a los servicios registrados. Por favor coordinar con Desarrollo de Sistemas</h5>
<%else%>
<table cellpadding="3" style="width: 100%" class="contornotabla">
	
	<tr>
		<td class="etiqueta" width="15%">Centro de costos</td>
		<td colspan="5">
		
		 <select name="cbocodigo_cco" id="cbocodigo_cco" onChange="cargarServicios()">
	          	<option value="-2">---Seleccione Centro de Costos--- </option>
		          <%  
		            do while not rsCeco.eof 
			  	    	seleccionar="" 
			    		if (cint(Codigo_Cco)=rsCeco("codigo_Cco")) then seleccionar="SELECTED " %>
		          		<option value= "<%=rsCeco("codigo_Cco")%>" <%=seleccionar%>> <%=rsCeco("descripcion_Cco") & "(" & rsCeco("codigo_Cco") & ")"%></option>
		          		<% rsCeco.movenext
			     loop
					rsCeco.Close
					Set rsCeco=Nothing 
					
			   %>
	        </select> 

		
		</td>
	</tr>
	
	
	<tr>
		<td class="etiqueta" width="15%">Servicio</td>
		<td colspan="5">
				
		    <select name="cbocodigo_sco" id="cbocodigo_sco">
	          	<option value="-1">---Seleccione un servicio--- </option>
		          <% 
		          seleccionar="" 
		          
		          if rsServicio.recordcount>0 then
		            If cint(Codigo_sco)=0 then seleccionar="SELECTED "
		                
		          %>
		            
		            <option value="0" <%=seleccionar%>>TODOS LOS SERVICIOS  </option>
		          <%
		            
		          end if
		          
		          do while not rsServicio.eof 
			  	    	
			    		if (cint(Codigo_sco)=rsServicio("codigo_sco")) then 
			    		        seleccionar="SELECTED " 
			    		else
			    		        seleccionar="" 
			    		end if
			    		%>
		          		<option value= "<%=rsServicio("codigo_sco")%>" <%=seleccionar%>> <%=rsServicio("nombre") %></option>
		          		<% rsServicio.movenext
			     loop
					rsServicio.Close
					Set rsServicio=Nothing 
					
			   %>
	        </select> 

		
		
		</td>
	</tr>
	
	
	<tr>
		<td style="width: 15%">&nbsp;</td>
		<td style="width: 5%; text-align: right;">Desde</td>
		<td style="width: 10%">
		<input readonly name="txtFechaInicio" type="text" class="Cajas" id="txtFechaInicio" value="<%=fechainicio%>" size="10"><input name="cmdinicio" type="button" class="cunia" onClick="MostrarCalendario('txtFechaInicio')" ></td>
		<td style="width: 5%">
		Hasta</td>
		<td style="width: 10%">
		<input readonly name="txtFechaFin" type="text" class="Cajas" id="txtFechaFin" value="<%=fechafin%>" size="10"><input name="cmdfin" type="button" class="cunia" onClick="MostrarCalendario('txtFechaFin')" ></td>
		<td style="width: 55%">
		Moneda:<%=moneda%></td>
	</tr>
	<tr>
		<td colspan="6" align="right">
		<input name="cmdBuscar" type="button" value="Consultar" class="buscar" onclick="BuscarDeudas()">
		<%call botonExportar("../../../","xls","deudasporcobrar","E","B")%>
		</td>
	</tr>
	</table>
<%
if codigo_cco<>"-2" and  codigo_sco>=0 then
	if HayReg=false then%>
		<h5>No se han encontrado clientes que con deudas registradas en el sistema</h5>
	<%else
		ArrEncabezados=Array("Fecha","Código","Cliente","Area/Escuela","Cargos","Abonos","Saldos")
		ArrCampos=Array("fecha","cod. resp.","cliente","carrera","cargos","abonos","diferencia")
		ArrCeldas=Array("15%","10%","30%","15%","10%", "10%","10%")
	
		call ValoresExportacion("Deudas por Cobrar",ArrEncabezados,rsClientes,Arrcampos,ArrCeldas)
	%>
	<h5 class="usatsugerencia">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Los participantes con letra azul tienen saldo cero
	y los de color rojo tienen saldo pendiente</h5>
	<table bgcolor="white" bordercolor="silver" border="1" cellpadding="3" style="width: 100%;border-collapse:collapse">
		<tr class="usatCeldaTitulo">
			<td align="center">#</td>
			<td align="center">Fecha</td>	
			<td align="center">Código</td>
			<td align="center">Cliente</td>
			<td align="center">Área/Escuela</td>
			<td align="center">Cargos</td>
			<td align="center">Abonos<br />
                (Efectivo + Notas de Credito)<br/> Clic aquí para ver detalle</td>
			<td align="center">Saldos</td>
		</tr>
		<%
		cantEntradas=0
		
		tc=0:ta=0:ts=0
		a=0:f=0
		datos =""
		
		Do while not rsClientes.EOF
			i=i+1
			
			cantEntradas = cdbl(cantEntradas) + cdbl(cantidad)
			
			tc=cdbl(tc) + cdbl(rsClientes("cargos"))
			ta=cdbl(ta) + cdbl(rsClientes("abonos"))
			ts=cdbl(ts) + cdbl(rsClientes("diferencia"))
			
			clase=""
			if cdbl(rsClientes("diferencia"))=0 then
				clase="class=azul"
				a=a+1
			else
				clase="class=rojo"
				f=f+1
			end if
			
			
			datos  = "CODIGO:" &  rsClientes("cod. resp.") & " | NOMBRES:" & rsClientes("cliente") 
			 			
		%>
		<tr <%=clase%> onMouseOver="Resaltar(1,this,'S')" onMouseOut="Resaltar(0,this,'S')">
			<td align="right"><%=i%>&nbsp;</td>
			<td><%=rsClientes("fecha")%>&nbsp;</td>
			<td><%=rsClientes("cod. resp.")%>&nbsp;</td>
			<td><%=rsClientes("cliente")%>&nbsp;</td>
			<td><%=rsClientes("carrera")%>&nbsp;</td>
			<td align="right"><%=formatnumber(trim(rsClientes("cargos")),2)%>&nbsp;</td>
			<td align="right"><a href="DetalleAbono.asp?cc=<% =rsClientes("codigo_Deu")%>&dat=<%=datos%>" target=_blank > <%=formatnumber(trim(rsClientes("abonos")),2)%>&nbsp; </a></td>
			<td align="right"><%=formatnumber(trim(rsClientes("diferencia")),2)%>&nbsp;</td>
		</tr>
		<%rsClientes.movenext
		Loop
		'rsClientes.close
		'Set rsClientes=nothing
		%>
		<tr class="usatTablaInfo">
			<td colspan="5" style="text-align: right; font-weight: 700;">TOTAL</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(tc,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(ta,2)%>&nbsp;</td>
			<td align="right" style="font-weight: 700"><%=formatnumber(ts,2)%>&nbsp;</td>		
		</tr>
	</table>
<table style="width: 50%" class="contornotabla">
	<tr>
		<td style="width: 90%">Deudas que tienen saldo cero</td>
		<td width="10%">: <%=a%></td>
	</tr>
	<tr>
		<td style="width: 90%">Deudas que tienen saldo mayor de cero</td>
		<td width="10%">: <%=f%>&nbsp;</td>
	</tr>
	<tr>
		<td style="width: 311px">&nbsp;</td>
		<td>&nbsp;</td>
	</tr>
</table>
	<%end if
	end if
end if
if Err.number <> 0 then
    response.Write "Error: " & Err.Description
end if
%>
</body>
</html>