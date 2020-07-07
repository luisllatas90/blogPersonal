<html>	

<head>
<meta http-equiv="Content-Language" content="es-mx">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<meta name="GENERATOR" content="Microsoft FrontPage 5.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<title>Registrar Detalle de Presupuesto</title>
<link rel="stylesheet" type="text/css" href="../private/estilo.css">
<script language="JavaScript" src="../private/funciones.js"></script>


<script language="JavaScript" >
function Volver(){
  history.back();	 
}

function validaSubmite(){ 
    if (document.form1.txtSubTotalGlobal.value == "0") 
	{
       alert("No ha ingresado valores en este detalle de Presupuesto") 
    	return (false);	

   }
   
		return (true);	
   	   //document.form1.action = "registrardetallepresupuesto.asp" 
       //document.form1.submit() 

} 


</script>

</head>
<body>

<%


function obtenerMes(nro)
            Select Case nro
                Case 1: obtenerMes = "ENERO"
                Case 2: obtenerMes = "FEBRERO"
                Case 3: obtenerMes = "MARZO"
                Case 4: obtenerMes = "ABRIL"
                Case 5: obtenerMes = "MAYO"
                Case 6: obtenerMes= "JUNIO"
                Case 7: obtenerMes= "JULIO"
                Case 8: obtenerMes= "AGOSTO"
                Case 9: obtenerMes= "SETIEMBRE"
                Case 10: obtenerMes= "OCTUBRE"
                Case 11: obtenerMes= "NOVIEMBRE"
                Case 12: obtenerMes= "DICIEMBRE"
            End Select
end function


Dim lngCodigo_Pto
Dim strCuenta_Art
Dim lngCodigo_Art
Dim strDescripcion_Art
Dim strUnidad_Art
Dim dblPrecioUnit_Art
Dim blnFuncion_Art
Dim strTipo_Pto
Dim strEstado_Pto

lngCodigo_Pto=request.querystring("cp")
strTipo_Pto=TRIM(request.querystring("tpto"))

'if trim(strTipo_Pto)="E" then
	'ModPrecio=" readonly "
'else
	'ModPrecio=""
'end if

'response.write "strTipo_Pto=" & strTipo_Pto

strCuenta_Art=request.querystring("ct")
lngCodigo_Art=request.querystring("ca")


'Nota: Al pasar la descripcion del Articulo por la URL, falla cuando la Descripcion tien comillas simples ('), Por eso la descripcion la busco nuevamente

Dim objArt, rsArt

Set objArt=Server.CreateObject("PryUSAT.clsDatArticuloConcepto")
Set rsArt=Server.CreateObject("ADODB.Recordset")
Set rsArt= objArt.ConsultarArticuloConcepto("RS","CO",lngCodigo_Art,strTipo_Pto)

strDescripcion_Art=trim(rsArt("descripcion_Art"))




'strDescripcion_Art=request.querystring("da")

strUnidad_Art=request.querystring("ua")
dblPrecioUnit_Art=request.querystring("pa")
blnFuncion_Art=request.querystring("ta")

ModPrecio=""

'Esto Sirve para impedir modificar precio
'-------------------------------------------------------------------------------------------
'if Session("codigo_Tfu")<> "1" then 'Solo el Administrador puede modificar los precios
'	if trim(strTipo_Pto)="E" then
'		 if dblPrecioUnit_Art >0 then 
'			ModPrecio=" readonly "
'		end if
'	End if
'End if
'-----------------------------------------------------------------------------------------------



	'Nota: cuando blnFuncion_Art =1 aparecera para indicar Cantidad,Caso Contrario Solo Aparece subtotal 
	
	'if blnFuncion_Art ="A" OR  blnFuncion_Art ="S"  then 
		blnFuncion_Art =1
	'else
	'	blnFuncion_Art =0
	'end if


strEstado_Pto=TRIM(request.querystring("epto"))


'Con el Codigo de Presupuesto Recibido Obtener Datos de Presupuesto
Dim objPresupuesto
Dim rsPresupuesto

Dim dtmFechaInicio
Dim dtmFechaFin
Dim strFormaDetalle
Dim strMoneda_Pto



Set objPresupuesto=Server.CreateObject("PryUSAT.clsDatPresupuesto")
Set rsPresupuesto=Server.CreateObject("ADODB.Recordset")
Set rsPresupuesto= objPresupuesto.ConsultarPresupuesto ("RS","CB",lngCodigo_Pto,"")

if rsPresupuesto.recordcount>0 then
	dtmFechaInicio=rsPresupuesto("fechaInicio_Pto")
	dtmFechaFin=rsPresupuesto("fechaFin_Pto")
	strFormaDetalle=rsPresupuesto("formaDetalle_Pto")

	if	rsPresupuesto("moneda_Pto")="S" Then
		strMoneda_Pto="S/."
	end if

	if	rsPresupuesto("moneda_Pto")="D" Then
		strMoneda_Pto="US$"
	end if
	
	if	rsPresupuesto("moneda_Pto")="E" Then
		strMoneda_Pto="€"
	end if

	
end if

rsPresupuesto.close
set  rsPresupuesto=Nothing

%>

<table width="80%" align="center">
	<tr>
	 <!--<td align="center"><img src="../images/detalle-pres.jpg"></td>-->
    <th align="center" class="table">REGISTRAR DETALLE DE PRESUPUESTO - CENTRO DE COSTOS</th>
    </tr>
    <tr>
		<td><hr></td>
    </tr>
</table>
<form onSubmit="return validaSubmite()" action="Procesos.asp?Tipo=I002" method="post" id=form1 name=form1>
<table align="center" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
  <tr>
    <td class="etabla" width="10%">Categoria:</td>
    <td width="65%" colspan="6"><%=strCuenta_Art%>&nbsp;</td>
  </tr>
  <tr>
    <td width="10%" class="etabla" >Concepto:</td>
    
    <%if strEstado_Pto="A" then%>
		<td width="50%" colspan="2"><%=strDescripcion_Art%>&nbsp;</td>
	<%else%>
		<td width="50%"><%=strDescripcion_Art%>&nbsp;</td>
		<td width="12%"><a href="frmbuscararticulo.asp?cp= <%=lngCodigo_Pto%>&tpto=<%=strTipo_Pto%>" style="text-decoration:none"><img src="../images/buscar.gif" ></a></td>
		
	
	<%end if%>
    <td width="8%" class="etabla" >Unidad</td>
    <td width="10%"><%=strUnidad_Art%>&nbsp;</td>
    <td width="10%" class="etabla" >Precio Unit.</td>
    <!--<td width="5%"><input type="text" name="txtPrecio" onChange="recalcular()" value="<%'=replace(dblPrecioUnit_Art,",",".")%>" size="5"></td> -->
    <td width="5%"><input type="text" name="txtPrecio"  <%=ModPrecio%>  onChange="recalcular()" value="<%=dblPrecioUnit_Art%>" size="5" onKeyPress="validarnumero()"></td> 
  </tr>
</table>
<table align="center" border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="80%">
<tr class="etabla">
	<td width="50%">PERIODO</td>
	<td width="10%">AÑO</td>
	
	<% 

	if cbool(blnFuncion_Art)=true then 
			
			%>
		<td width="10%">CANTIDAD</td>
	<%end if%>
	<td width="15%">SUBTOTAL (<%=strMoneda_Pto%>)</td>

</tr>

<%
Dim ano
Dim mi
Dim anoIni
Dim anoFin
Dim mesIni
Dim mesFin
Dim totalAnos

anoIni = Year(dtmFechaInicio)
anoFin = Year(dtmFechaFin)
mesIni = Month(dtmFechaInicio)
mesFin = Month(dtmFechaFin)


Dim rsDetalleEjecucion
Set objPresupuesto=Server.CreateObject("PryUSAT.clsDatPresupuesto")
Set rsDetalleEjecucion=Server.CreateObject("ADODB.Recordset")

if strFormaDetalle="M" then
		totalAnos = anoFin - anoIni + 1
        
        mi = mesIni
        ano = anoIni       
        
        For x = 1 To totalAnos
        
            If x = totalAnos Then
                'Es el ultimo año
                For y = mi To mesFin

                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value=<%=obtenerMes(y)%>>
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;<%=obtenerMes(y)%>&nbsp;</td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	

						mes=obtenerMes(y)
						Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,mes,ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")
							
						else
							cant="0"
							subt="0"

						end if
						
						if cbool(blnFuncion_Art)=true then 
					    %> 
							<td align="center"><input type="text" name="txtCantidad" value="<%=cant%>" id="txtCantidad<%=y%>"  onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="validarnumero()" size="5"></td>
							<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" readonly size="10" id="txtSubTotal<%=y%>" onChange="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
						<%else%>
							<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onChange="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
						<%end if %>
							
						</tr>	                   
                <%
					
                Next
                                
            Else
                For y = mi To 12 'hasta Diciembre
						mes=obtenerMes(y)
                        Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,mes,ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")
						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value=<%=obtenerMes(y)%>>
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;<%=obtenerMes(y)%>&nbsp;</td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyPress="validarnumero()" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" readonly value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>
								
						</tr>	                   
                <%

                Next
                mi = 1
            End If
            
            ano = ano + 1
        Next

end if

if strFormaDetalle="B" then
	totalAnos = anoFin - anoIni + 1
        
        mi = mesIni
        ano = anoIni       
        
        For x = 1 To totalAnos
        
            If x = totalAnos Then
                'Es el ultimo año
                contar=0
                For y = mi To mesFin step 2
					contar = Int((y + 1) / 2)
					
					  Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="BIMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;BIMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>
						</tr>	                   
                <%
                Next
                                
            Else
                contar=0
                For y = mi To 12  step 2 'hasta Diciembre
                     contar = Int((y + 1) / 2)           
					   Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="BIMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;BIMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>

						</tr>	                   
                <%

                Next
                mi = 1
            End If
            
            ano = ano + 1
            
            
        Next

end if

if strFormaDetalle="T" then
	totalAnos = anoFin - anoIni + 1
        
        mi = mesIni
        ano = anoIni       
        
        For x = 1 To totalAnos
        
            If x = totalAnos Then
                'Es el ultimo año
                contar=0
                For y = mi To mesFin step 3
					contar = Int((y + 2) / 3)
					  Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
					
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="TRIMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;TRIMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>

						</tr>	                   
                <%
                Next
                                
            Else
                contar=0
                For y = mi To 12  step 3 'hasta Diciembre
                     contar = Int((y + 2) / 3)           
					   Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="TRIMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;TRIMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>
						</tr>	                   
                <%

                Next
                mi = 1
            End If
            
            ano = ano + 1
            
            
        Next

end if

if strFormaDetalle="S" then
		totalAnos = anoFin - anoIni + 1
        
        mi = mesIni
        ano = anoIni       
        
        For x = 1 To totalAnos
        
            If x = totalAnos Then
                'Es el ultimo año
                contar=0
                For y = mi To mesFin step 6
					contar = Int((y + 5) / 6)
					  Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="SEMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;SEMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
							<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>

						</tr>	                   
                <%
                Next
                                
            Else
                contar=0
                For y = mi To 12  step 6 'hasta Diciembre
                     contar = Int((y + 5) / 6)           
					 
					   Set rsDetalleEjecucion= objPresupuesto.ConsultarDetalleEjecucion("RS","PE",lngCodigo_Pto,lngCodigo_Art,obtenerMes(y),ano)

						if rsDetalleEjecucion.recordcount >0 then
							cant=rsDetalleEjecucion("cantidad_Dej")
							subt=rsDetalleEjecucion("subtotal_Dej")

						else
							cant="0"
							subt="0"
						end if        
                    %>    
						<tr>	                   
							<input type="hidden" name = "txtPeriodo" value="SEMESTRE<%=contar%>">
							<input type="hidden" name="txtAno" value=<%=ano%>>

							<td>&nbsp;&nbsp;&nbsp;SEMESTRE<%=contar%></td>
							<td align="center"><%=ano%>&nbsp;</td>
						<%	if cbool(blnFuncion_Art)=true then %>
								<td align="center"><input type="text" name="txtCantidad" value ="<%=cant%>" id="txtCantidad<%=y%>" size="5" onKeyUp="calcularvalores(txtCantidad,txtCantGlobal);calcularsubtotal(txtCantidad<%=y%>,txtSubTotal<%=y%>,txtPrecio);calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)" onKeyPress="noescribir()"></td>
							<%else%>
							
								<td align="center"><input type="text" name="txtSubTotal" value="<%=subt%>" size="10" id="txtSubTotal<%=y%>" onKeyUp="calcularvalores(txtSubTotal,txtSubTotalGlobal)"></td>
							<% end if %>

						</tr>	                   
                <%

                Next
                mi = 1
            End If
            
            ano = ano + 1
            
            
        Next


end if


rsDetalleEjecucion.close
Set rsDetalleEjecucion = Nothing

Dim rsTotalPresupuesto
Set rsTotalPresupuesto=Server.CreateObject("ADODB.Recordset")
Set rsTotalPresupuesto= objPresupuesto.ConsultarTotalPresupuesto ("RS","AR",lngCodigo_Pto,lngCodigo_Art)

if  rsTotalPresupuesto.recordcount >0 then
	cant=rsTotalPresupuesto("cantidadTotal_Dpr")
    tot=rsTotalPresupuesto("total_Dpr")
else
	cant="0"
    tot="0"

end if



%>
<tr>
	<td align="right">
	   
	   <%if strEstado_Pto<>"A" then%>
		<input type="submit" value="Guardar" class="guardar">
	   <%end if%>
		<input type="Button" value="Cancelar" class="salir" onclick="Volver()">
	</td>
	<td class="etabla" >TOTAL</td>
	<%	if cbool(blnFuncion_Art)=true then %>
		<td align="center"><input type="textbox" name="txtCantGlobal" readonly id="txtCantGlobal" value="<%=cant%>" size="5" onKeyPress="noescribir()"></td>
	<%end if%>
	<td align="center"><input type="textbox" name="txtSubTotalGlobal" readonly id="txtSubTotalGlobal" value="<%=tot%>"  size="10" onKeyPress="noescribir()"></td>

</tr>

<input type="hidden" name="txtCodigo_Pto" value="<%=lngCodigo_Pto%>">
<input type="hidden" name="txtCodigo_Art" value="<%=lngCodigo_Art%>">
<input type="hidden" name="txtTipoArt" value="<%=blnFuncion_Art%>">

<%
rsTotalPresupuesto.close
set rsTotalPresupuesto=Nothing
set objPresupuesto = Nothing

%>

</table>
</form>
</body>