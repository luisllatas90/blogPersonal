<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReporteDetalle.aspx.vb" Inherits="BecaEstudio_frmReporteDetalle" %>

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
            <td colspan="2"><h1 class="title-cont">Detalle de Estudiantes Becados</h1></td>                            
        </tr>
        <tr>
            <td>Ciclo</td>                
            <td><span class="combobox large"><asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True"></asp:DropDownList></span></td>
        </tr>
        <tr>
            <td>Escuela Profesional</td>
            <td><span class="combobox large"><asp:DropDownList ID="ddlEscuela" runat="server" AutoPostBack="True"></asp:DropDownList></span></td>
        </tr>
        <tr>
            <td>Tipo de Beca Estudio</td>
            <td><span class="combobox large"><asp:DropDownList ID="ddlTipoBeca" runat="server" AutoPostBack="True"></asp:DropDownList></span></td>
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
                        Cod.Univ</td>
                    <td class="cell cell-1">
                        Estudiante</td>
                    <td class="cell cell-1">
                        Escuela Profesional</td>
                    <td class="cell cell-1">
                        &nbsp;Tipo Beca&nbsp;</td>
                   
                </tr>
                <tr class="row-0">
                   
                    <td class="cell cell-2">
                        121OT36533</td>
                    <td class="cell cell-3">
                        RAMOS PEÑALOZA, JEANS DIEGO</td>
                    <td class="cell cell-4">
                        INGENIERÍA DE SISTEMAS Y COMPUTACIÓN</td>
                    <td class="cell cell-5">
                        BECA COMPLETA</td>
               
                </tr>
                <tr class="row-1">
                   
                    <td class="cell cell-2">
                        052AD02983</td>
                    <td class="cell cell-3">
                        BURGA MENDOZA, ISMAEL AGUSTIN</td>
                    <td class="cell cell-4">
                        ADMINISTRACIÓN DE EMPRESAS</td>
                    <td class="cell cell-5">
                        BECA COMPLETA</td>
                    
                </tr>
                 <tr class="row-0">
                   
                    <td class="cell cell-2">
                        06TD105149</td>
                    <td class="cell cell-3">
                        AGUILAR DELGADO, TERESA ELIZABETH</td>
                    <td class="cell cell-4">
                        ENFERMERÍA</td>
                    <td class="cell cell-5">
                        MEDIA BECA</td>
               
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
