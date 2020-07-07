﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaPostular.aspx.vb" Inherits="frmBecaPostular" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilos.css?a=21&b=2" rel="stylesheet" type="text/css" />
    <style type="text/css">
    body{ padding:10px;}  
    </style>
    <script>
        function confirmar() {
            var r = confirm("EL REGISTRO DE SOLICITUD DE BECA IMPLICA UN COSTO DE S./ 5.00, DESEA SOLICITAR LA BECA?");
            if (r == true) {
            
                form1.submit();
            }
            else {
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" method="post" action="frmBecaPostular.aspx">
    <div id="content">
    <br />
    <div>
    <img src="images/becas.png" style="float:left;" /><h1 class="title-cont" style="padding-top:10px;">Solicitud de Beca de Estudio</h1>
    </div>
    <br />
    <div style="border:1px solid #C62121; padding:5px; width:80%; color:#2F4F4F">-Tramitar la Solicitud de Becas tiene un costo de <b>S./ 5.00</b><br />
        
        </b>
       
                -Fecha de Publicación de Resultados <b>26 de Agosto del 2015</b> <br /> <asp:Label ID="Label1" runat="server" Text=""></asp:Label></div>
    <br />
    <a href="../../librerianet/reglamentos/ReglamentoBecas.pdf" target="_blank"><img src="images/agreement.png" />  <b>Ver Reglamento de Becas</b></a>
    <br /> 
    
    <div id="panelRequisitos" runat="server" visible="false">
    <div class="error">Registro de solicitud no disponible, consultar con vicerrectorado de estudiantes.</div>
    </div>
    
    <div id="tablaRequisitos" runat="server" visible="true">
    <table class="table-cont" >      
    <tr class="row-title">                  
                    <td class="cell cell-3">Requisito</td>
                    <td class="cell cell-3">Requerido</td>
                    <td class="cell cell-4">Cumplimiento</td>
                    <td class="cell cell-5"></td>                    
    </tr>                
                <div id="tb" runat="server"></div>    
                <tr><td colspan="4">
               

                <br /><div id="btn" runat="server"></div></td></tr>    
        <asp:HiddenField ID="HiddenField1" runat="server" value="no" />
        </table>
      </div>   
            <div id="panelEnvio" runat="server" visible="false">
            <h1 class="title-cont">Estado de Solicitud</h1>   
            <table class="table-cont">
            <tr class="row-title">                  
                <td class="cell cell-3">Cod.Universitario</td>
                <!--<td class="cell cell-3">Estudiante</td>-->
                <td class="cell cell-4">Solicitud</td>
                <td class="cell cell-5">Fecha Envío</td>                    
                <td class="cell cell-5">Estado</td>                    
            </tr>
            
            <div id="tbEnviado" runat="server">
            
            
            </div>    
            
            </table>
            </div>
            <table>
            <tr>
            <td><div class="success" id="mensaje" runat="server"></div></td>
           <td><div class="success" id="mensajecro" runat="server"></div></td>
            </tr>
            </table>
    </div>  
    </form>
</body>
</html>
