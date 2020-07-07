<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        BODY
        {
            font-size:10px; font-family:Trebuchet MS
            
            }
 
        .style1
        {
            width: 127px;
        }
        .style2
        {
            width: 127px;
            font-weight: bold;
        }
 
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <b><h4>Validación de Pago por Asesoría de Tesis</h4></b></div>
    <table>
        <tr>
            <td>
                Departamento Académico: </td>
            <td>
                <asp:DropDownList ID="DropDownList32" runat="server">
                <asp:ListItem>INGENIERÍA</asp:ListItem>
                    <asp:ListItem>CIENCIAS DE LA SALUD</asp:ListItem>					
                    <asp:ListItem>CIENCIAS EMPRESARIALES</asp:ListItem>
                    <asp:ListItem>DERECHO</asp:ListItem>
                    <asp:ListItem>FILOSOFÍA Y TEOLOGÍA</asp:ListItem>
                    <asp:ListItem>HUMANIDADES</asp:ListItem>
                </asp:DropDownList>
            </td>
			<td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
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
                <b>Departamento Acad.</b></td>
            <td class="style2">
                Docente</td>
            <td style="text-align:center;">
                <b>Nro. Asesorías</b></td>
            <td style="text-align:center;">
                Validación Docente</td>
            <td>
                <b>Validar Pago</b></td>
        </tr>
        <tr>
            <td>
                INGENIERÍA</td>
            <td class="style1">
                ALARCÓN GARCÍA, ROGER ERNESTO</td>
           <td style="text-align:center;">
               <a href="#"> 01 </a>
            </td>
           <td style="text-align:center;">
               SÍ</td>
            <td style="text-align:center;">
                <asp:Button ID="Button1" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button2" runat="server" Text=" NO " />
            </td>
        </tr>
        <tr>
            <td>
                INGENIERÍA</td>
            <td class="style1">
                DEL CASTILLO CASTRO, CONSUELO</td>
           <td style="text-align:center;">
               <a href="#"> 05 </a>
            </td>
           <td style="text-align:center;">
               NO</td>
           <td style="text-align:center;">
                <asp:Button ID="Button3" runat="server" Text=" SÍ " />
&nbsp;&nbsp;&nbsp;
                <asp:Button ID="Button4" runat="server" Text=" NO " />
                </td>
        </tr>
        <tr>
            <td>
                INGENIERÍA</td>
            <td class="style1">
                BRAVO JAICO, JESSIE</td>
            <td style="text-align:center;">
               <a href="#"> 03 </a>
            </td>
            <td style="text-align:center;">
                SÍ</td>
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
