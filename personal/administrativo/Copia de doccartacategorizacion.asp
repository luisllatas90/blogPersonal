<!--#include file="../../funciones.asp"-->
<%

        apellidos_alu= "villavicencio montoya"
	    nombres_alu= "monica"
	    nombre_cpf="ing. de sistemas y computacion"
	    credito_alu="20.00"
	    codigouniver_alu="051c102799"	       	

	Response.ContentType = "application/msword"
	Response.AddHeader "Content-Disposition","attachment;filename=" & codigouniver_alu & ".doc"
		
%>
<html>
<head>
<meta http-equiv="Content-Language" content="es">
<meta name="GENERATOR" content="Microsoft FrontPage 12.0">
<meta name="ProgId" content="FrontPage.Editor.Document">
<meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
<script language="JavaScript" src="../../../../private/funciones.js"></script>
<style>
<!--
.etiqueta    { font-weight: bold }
.normal    { font-weight: normal }
.cabezera    { font-weight: bold; text-align: center; background-color: #C0C0C0; border-left-width:1; border-right-width:1; border-top-style:solid; border-top-width:1; border-bottom-style:solid; border-bottom-width:1}
body         { font-family: Belwe Lt BT; font-size: 12pt }
td           { font-size: 12pt }
p.primeralinea {
	text-indent: 40px;
        width: 565px;
    text-align:justify;
    }
    .style1
    {
        height: 24px;
    }
-->
</style>
</head>
<body style="margin-top:2cm">
<table cellpadding="2" cellspacing="0" style="border-collapse: collapse" width="100%">
  <THEAD>
  <tr>
    <td>
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse" bordercolor="#111111" width="100%">
            <tr>
                <th style="font-size: 12pt; font-weight:bold" width="100%" align="right" valign="top">
                Chiclayo, 24 de febrero de 2012
                </th>
            </tr>
            
            <tr>
                <td height="19" class="normal">
                <br />
                <br />
                <br />
                <br />
                Familia</td>
            </tr>
            <tr>    
                <td height="19" class="etiqueta">&nbsp;<%=apellidos_alu%>&nbsp;</td>
            </tr>
            <tr>
                <td height="19" class="normal">Ciudad.-</td>    
            </tr>   
            <tr>
                <td height="50"></td>
            </tr>  
            <tr>
                <td class="normal">   
                  <p class="primeralinea">
                  En nombre  de la Universidad Católica Santo Toribio de Mogrovejo, les                
                expreso mi cordial saludo y felicitación por el ingreso de su hijo (a) <%=nombres_alu%>
                a la Escuela Profesional de <%=nombre_cpf%>.
                  </p> 
                  
                  <p class="primeralinea">
                  En la evaluación de su Expediente Socio-económico la Comisión de
                Pensiones ha determinado asignarle un costo de crédito por ciclo académico de 
                S/. <%=credito_alu%> Nuevos Soles. La pensión académica está en función del costo por crédito        
                asignado y de la carga académica; la misma que podrá ser cancelada en 4 ó 5
                cuotas los 30 de cada mes. Esta categorización será supervisada periódicamente y
                podrá suspenderse o extinguirse de conformidad con el ítem IV (j) del 
                Reglamento de  Pensiones 2012-I.
                  </p>
                  
                  <p class="primeralinea">
                  Su hijo (a) podrá realizar su matrícula a través de nuestra página web: 
                  www.usat.edu.pe/campusvirtual, ingresando su código universitario <%=codigouniver_alu%>, 
                  cuya clave es <%=password_alu%>. Asimismo, adjunto encontrará el Reglamento de  Pensiones 2012-I 
                  para su  atenta lectura. 
                  </p>
                  
                  <p class="primeralinea">
                  Reciban nuestro agradecimiento por la confianza depositada en nuestra Universidad.
                  </p>
                  
                  <p class="primeralinea">
                  Sin otro particular, quedo de ustedes.
                  </p>
                  
                  <br />
                  <p class="primeralinea">
                  Atentamente,
                  </p>
                  <br />
                  <br />
                  <br />         
                </td>
          </tr>
          <tr>
              <td style="text-align:center">
               <img src="firma.JPG" />
              </td>
          </tr>
          <tr>
              <td style="text-align:center">
              Mgtr. Carlos Campana Marroquín  
              </td>
          </tr>
          <tr>
              <td style="text-align:center">
              Administrador General
              </td>
          </tr>  
        </table>
    </td>
  </tr> 
  </THEAD> 
</table>
<br/>
</body>
</html>
	