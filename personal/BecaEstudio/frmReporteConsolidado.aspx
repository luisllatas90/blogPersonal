<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReporteConsolidado.aspx.vb" Inherits="BecaEstudio_frmReporteConsolidado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />       
</head>
<body>
<div id="registrar-cont">
<form id="form1" runat="server">
    <table>
      
        <tr>
            <td colspan="2"><h1 class="title-cont">Consolidado de Becas de Estudio</h1></td>                            
        </tr>
        <tr><td>Ciclo</td>                
            <td><span class="combobox large"><asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True"></asp:DropDownList></span></td>
        </tr>        
        <tr><td><br /><br /></td></tr>
        <tr>
        <td colspan="2">
         <div id="content" >
            <table class="table-cont">
                 <tr>
                <td>                
                <input class="input-button send" type="button" value="Exportar" />
                </td>
                
                </tr>
                <tr><td><br /></td></tr>   
             <tr class="row-title">
                    <td class="cell cell-1">
                        Escuela Profesional.</td>
                    <td class="cell cell-2">
                        BECA COMPLETA</td>
                    <td class="cell cell-3">
                        MEDIA BECA</td>
                    <td class="cell cell-4">
                        BECA PERSONAL USAT</td>
                    <td class="cell cell-5"> BECA ORFANDAD
                        </td>
                   <td class="cell cell-6"> BECA ESCUELA PRE
                        </td>
                </tr>
                
                <tr  class="row-0">
                    
                    <td class="cell cell-1">ADMINISTRACIÓN DE EMPRESAS</td>
                    <td class="cell cell-2">5</td>
                    <td class="cell cell-3">3</td>
                    <td class="cell cell-4">
                       5</td>
                   <td class="cell cell-5">
                        2</td>
                    <td class="cell cell-6">
                       10</td>
                </tr>
                <tr  class="row-1">
                    
                    <td class="cell cell-1">INGENIERIA DE SISTEMAS Y COMPUTACION</td>
                    <td class="cell cell-2">
                       5</td>
                    <td class="cell cell-3">
                        3</td>
                    <td class="cell cell-4">
                       5</td>
                   <td class="cell cell-5">
                        2</td>
                    <td class="cell cell-6">
                       10</td>
               
                </tr>
             <tr  class="row-0">
                    
                    <td class="cell cell-1">INGENIERIA CIVIL</td>
                    <td class="cell cell-2">
                       5</td>
                    <td class="cell cell-3">
                        3</td>
                    <td class="cell cell-4">
                       5</td>
                   <td class="cell cell-5">
                        2</td>
                    <td class="cell cell-6">
                       10</td>
               
                </tr>
                  <tr  class="row-1">
                    
                    <td class="cell cell-1">INGENIERIA INDUSTRIAL</td>
                    <td class="cell cell-2">
                       5</td>
                    <td class="cell cell-3">
                        3</td>
                    <td class="cell cell-4">
                       5</td>
                   <td class="cell cell-5">
                        2</td>
                    <td class="cell cell-6">
                       10</td>
               
                </tr>
             
            </table>
             
             <br />
      </div>
             
        </td>
            
        </tr>
    </table>    
   </form>
   </div>
</body>
</html>

