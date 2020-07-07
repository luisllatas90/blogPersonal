<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteinscritoseventoxescuelacicloing.aspx.vb" Inherits="rpteinscritoseventoxescuelacicloing" Theme="Acero" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Lista de ingresantes por escuela y ciclo de ingreso.</title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../../private/jq/lbox/thickbox.js"></script>
		<script type="text/javascript" language="javascript">
	    function PintarFilaElegida(obj) {
	        if (obj.style.backgroundColor == "white") {
	            obj.style.backgroundColor = "#E6E6FA"//#395ACC
	        }
	        else {
	            obj.style.backgroundColor = "white"
	        }
	    }
	</script>
	<style>
	    body{ font-family:Trebuchet MS; font-size:11px;}
	    p { font-family:Trebuchet MS; font-size:11px; color:#1F5E70; font-weight:bold; }
	</style>
</head>
<body>
    <form id="form1" runat="server">
    <p style="font-size:medium;">Lista de Ingresantes</p>
    <table>
            <tr>
                <td><p>Carrera Profesional :</p></td>
                <td><asp:DropDownList ID="ddlEscuela" runat="server" Height="23px" Width="" ></asp:DropDownList></td>                            
                <td></td><td></td><td></td><td></td>
                <td><p>Semestre de Ingreso:</p></td>
                <td><asp:DropDownList ID="ddlCicloIngreso"  runat="server" Height="12px" Width="170px"></asp:DropDownList></td>
            </tr>
            <tr><td></td></tr>
            <tr>
                <td><asp:Button ID="cmdConsultar" runat="server" Text="Consultar" /></td>
                <td><asp:Button ID="cmdExportar" runat="server" SkinID="BotonAExcel" Text="Exportar" /></td>
            </tr>
    </table>
    <br />
    <br />
    <br />
       <asp:GridView ID="grwListaPersonas1" runat="server"
        AutoGenerateColumns="False"  CellPadding="4" ForeColor="#333333" 
        GridLines="None" Font-Names="Trebuchet MS" Font-Size=11px 
        HorizontalAlign="Center" >
           <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="TipoDoc" HeaderText="Tipo Doc.">
            </asp:BoundField>
            <asp:BoundField DataField="Nrodoc" HeaderText="Nro. Doc.">
            </asp:BoundField>
            <asp:BoundField HeaderText="Participante" DataField="participante" />
            <asp:BoundField DataField="sexo_Alu" HeaderText="Sexo" />
            <asp:BoundField DataField="codUniversitario" HeaderText="Cód. Univ." >
            </asp:BoundField>
            <asp:BoundField DataField="carrera" HeaderText="Escuela" />
            <asp:BoundField DataField="clave" HeaderText="Clave" Visible="False" />
            <asp:BoundField DataField="estado" HeaderText="Estado" />
            <asp:BoundField DataField="email_Usat" HeaderText="Email Usat" /> 
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="nombre_Dep" HeaderText="Departamento" />
            <asp:BoundField DataField="nombre_Pai" HeaderText="País" />
            <asp:BoundField DataField="direccion" HeaderText="Dirección" />
            <asp:BoundField DataField="TelFijo" HeaderText="Tlf. Fijo" />
            <asp:BoundField DataField="TelCelular" HeaderText="Celular" />
            <asp:BoundField DataField="fechaNacimiento_Alu" HeaderText="Fecha Nac." />
            <asp:BoundField DataField="nombre_Min" HeaderText="Modalidad Ingreso" />                
            <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costos" />
            <asp:BoundField DataField="condicion" HeaderText="Condición" />
            <asp:BoundField DataField="matriculado" HeaderText="Matriculado" />
        </Columns>
           <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" ForeColor="White" />
           <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
           <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
           <EditRowStyle BackColor="#2461BF" />
           <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
</form>
    
</body>
</html>