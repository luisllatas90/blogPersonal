<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consultarmeritoegresados.aspx.vb" Inherits="academico_consultarmeritoegresados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

.usatTitulo {
	font-family: Arial;
	font-size: 12pt;
	font-weight: bold;
	color: #1F5E70;
}

table {
	font-family: Trebuchet MS;
	font-size: 8pt;
}
TBODY {
	display: table-row-group;
}
tr {
	font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	color: #2F4F4F;
	cursor: pointer;
}
select {
	font-family: Verdana;
	font-size: 8.5pt;
}
.excel2 {
	border: 1px solid #666666;
	background: #FEFFE1 url('../../images/xls.gif') no-repeat 0% center;
	width: 80;
	font-family: Tahoma;
	font-size: 8pt;
	height: 20;
	cursor: hand;
}
        span
        {
            cursor: pointer
        }
        
        a:Link
        {
        	color: #000000;
	        text-decoration: underline;
        }
    a:Link {
	color: #000000;
	text-decoration: none;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table cellpadding="3" cellspacing="0" style="width:100%;" id="tblCriterios">
        <tr>
            <td height="5%" width="20%">Carrera Profesional</td>
            <td height="5%" width="80%">
                <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">Semestre Egreso:</td>
            <td height="5%" width="80%">                
                <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
            &nbsp;&nbsp;Según:
                <asp:DropDownList ID="dpFiltro" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="C">Cuadro Completo</asp:ListItem>
                    <asp:ListItem Value="T">Tercio Superior</asp:ListItem>
                    <asp:ListItem Value="Q">Quinto Superior</asp:ListItem>
                </asp:DropDownList>
            &nbsp;<asp:Button ID="cmdConsultar" runat="server" Text="Consultar" />
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%">&nbsp;</td>
            <td height="5%" width="80%">
                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" 
                    Text="  Exportar" 
                    onclientclick="document.all.tblmensaje.style.display='none'" />
            </td>
        </tr>
        <tr>
            <td height="5%" width="20%" colspan="2" style="width: 100%">
                <asp:Label ID="Label3" runat="server" ForeColor="Red" 
                    
                    Text="- Para la ubicación del cuadro de mérito en el caso que dos estudiantes tengan igual promedio ponderado acumulado se preferirá aquel estudiante que no desaprobó ninguna asignatura. (Art. 45)" 
                    Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label4" runat="server" ForeColor="Red" 
                    
                    
                    Text="- Son considerados egresados todo aquel alumno que ha sido asignado como tal por la Dirección de Escuela a través del Registro de Egresados del Campus Virtual" 
                    Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td height="5%" width="100%" colspan="2">
                <!--<div id="listadiv" style="height:400px" class="contornotabla">-->
                <!--</div>-->
            </td>
        </tr>
        </table>
    <table id="tblmensaje" border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse;display:none" bordercolor="#111111" width="100%" height="100%" class="contornotabla">
	    <tr>
	    <td width="100%" align="center" class="usatTitulo" bgcolor="#FEFFE1">
	    Procesando<br>
	    Por favor espere un momento...<br>
	    <img border="0" src="../../images/cargando.gif" width="209" height="20">
	    </td>
	    </tr>
    </table>
    <asp:GridView ID="gvMeritoEgresados" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None">
        <RowStyle BackColor="#EFF3FB" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
    </body>
</html>
