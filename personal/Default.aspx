<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        BODY
        {
            font-size:10px; font-family:Trebuchet MS
            
            }
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <b><h4>Validación de Avance de Tesis</h4></b></div>
    <table>
        <tr>
            <td>
                Semestre Académico:</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem>2018-I</asp:ListItem>
                </asp:DropDownList>
            </td>
			<td>
                Mes:</td>
            <td>
                <asp:DropDownList ID="DropDownList2" runat="server">
                    <asp:ListItem>1: 15/03/2018 - 15/04/2018</asp:ListItem>
					<asp:ListItem>2: 16/04/2018 - 15/05/2018</asp:ListItem>
					<asp:ListItem>3: 16/05/2018 - 15/06/2018</asp:ListItem>
					<asp:ListItem>4: 16/06/2018 - 15/07/2018</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Grupo Horario:</td>
            <td>
                <asp:DropDownList ID="DropDownList25" runat="server">
                    <asp:ListItem>SEMINARIO DE TESIS I</asp:ListItem>
                    <asp:ListItem>SEMINARIO DE TESIS II</asp:ListItem>
                    <asp:ListItem>SEMINARIO DE TESIS III</asp:ListItem>
                </asp:DropDownList>
            </td>
			<td>
                Estado:</td>
            <td>
                <asp:DropDownList ID="DropDownList31" runat="server">
                    <asp:ListItem>Pendientes</asp:ListItem>
					<asp:ListItem>Sí</asp:ListItem>
					<asp:ListItem>No</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
    </table>
	<br/>
    <table border="1">
        <tr style="background-color:lightgray;">
            <td>
                <b>Código Univ.</b></td>
            <td>
                <b>Estudiante</b></td>
            <td width="40%">
                <b>Tesis</b></td>
            <td>
                <b>Asesor</b></td>
            <td style="text-align:center;">
                <b>Nro. Asesorías</b></td>
            <td>
                <b>Evidencia Avance el estudiante</b></td>
        </tr>
        <tr>
            <td>
                132TE46803</td>
            <td>
                ALBERTO MANAY, BRYAN ENRIQUE</td>
            <td>
                APLICACIÓN WEB BASADA EN SISTEMA EXPERTO PARA APOYAR LAS TERAPIAS COGNITIVAS DE 
                PERSONAS CON ALZHEIMER</td>
            <td>
                ALARCÓN GARCÍA, ROGER ERNESTO</td>
           <td style="text-align:center;">
               <a href="#"> 01 </a>
            </td>
            <td style="text-align:center;">
                <asp:Button ID="Button1" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text=" NO " />
            </td>
        </tr>
        <tr>
            <td>
                131CV44484</td>
            <td>
                BULLON CASTAÑEDA, ROSA RAQUEL</td>
            <td>
                SISTEMA WEB BASADO EN LA NTP 202.001 2016 PARA APOYAR EN EL CONTROL DE CALIDAD 
                DE LA LECHE EN LA ASOCIACIÓN DE PRODUCTORES AGROPECUARIOS DEL DISTRITO DE 
                LAGUNAS-MOCUPE.</td>
            <td>
                DEL CASTILLO CASTRO, CONSUELO</td>
           <td style="text-align:center;">
               <a href="#"> 05 </a>
            </td>
           <td style="text-align:center;">
                <asp:Button ID="Button3" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" Text=" NO " />
                </td>
        </tr>
        <tr>
            <td>
                131CV42133</td>
            <td>
                CHANCAFE LEYVA, ELA MILAGROS</td>
            <td>
                APLICACIÓN MÓVIL PARA CONTRIBUIR EN EL PROCESO DE IDENTIFICACIÓN DE AVES QUE 
                HABITAN EN LOS HUMEDALES DE PUERTO ETEN.</td>
            <td>
                ZUÑE BISPO, LUIS AUGUSTO</td>
           <td style="text-align:center;">
               <a href="#"> 02 </a>
            </td>
           <td style="text-align:center;">
                <asp:Button ID="Button5" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button6" runat="server" Text=" NO " />
            </td>
        </tr>
        <tr>
            <td>
                141CV51044</td>
            <td>
                CHANDUVI SIESQUEN, DANIEL ALONSO</td>
            <td>
                APLICACIÓN MÓVIL INFORMATIVA QUE AYUDE AL TURISTA A OBTENER INFORMACIÓN DE SU 
                INTERÉS DURANTE SU ESTADÍA POR LA REGIÓN LAMBAYEQUE</td>
            <td>
                LEON TENORIO, GREGORIO</td>
            <td style="text-align:center;">
               <a href="#"> 04 </a>
            </td>
            <td style="text-align:center;">
                <asp:Button ID="Button8" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button7" runat="server" Text=" NO " />
            </td>
        </tr>
        <tr>
            <td>
                141CV49137</td>
            <td>
                CHINCHAY FARROÑAY, IGOR</td>
            <td>
                IMPLEMENTACIÓN DE UN SISTEMA DE GESTIÓN DE RECURSOS HUMANOS, INCLUYENDO UN 
                DISPOSITIVO BIOMÉTRICO DE HUELLAS DIGITALES PARA OPTIMIZAR EL PROCESO DE CONTROL 
                DE ASISTENCIA, EN UNA ESTACIÓN DE SERVICIOS UBICADA EN LAMBAYEQUE DE 2017</td>
            <td>
                BRAVO JAICO, JESSIE</td>
            <td style="text-align:center;">
               <a href="#"> 03 </a>
            </td>
           <td style="text-align:center;">
                 <asp:Button ID="Button866" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button766" runat="server" Text=" NO " />
            </td>
        </tr>
    </table>
    </form>
    </body>
</html>
