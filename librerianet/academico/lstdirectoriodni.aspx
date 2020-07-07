              <%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstdirectoriodni.aspx.vb" Inherits="lstdirectoriodni" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Revisión de Directorio de estudiantes</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />    
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">
        <table style="width:100%;">
            <tr>
                <td class="usatTitulo">
                    Actualización de DNI</td>
                <td>
                    &nbsp;</td>
                <td align="right">
                    <asp:Label ID="lblNroRegistros" runat="server" ForeColor="Blue"></asp:Label>
                </td>
            </tr>
        </table>
    </p>
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td style="width:90%">
                &nbsp; Buscar:
                <asp:TextBox ID="txtbuscar" runat="server" MaxLength="20" Width="200px"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtbuscar" 
                    ErrorMessage="Ingrese el término de búsqueda" ValidationGroup="Buscar">*</asp:RequiredFieldValidator>
                &nbsp;Escuela Profesional: <asp:DropDownList ID="dpCodigo_cpf" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:CheckBox ID="chkActualizados" runat="server" 
                    Text="Mostrar actualizados" />
&nbsp;&nbsp;</td>
        </tr>
        <tr bgcolor="#91b4de" style="height:30px">
            <td style="width:90%">
                &nbsp; <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                    Font-Bold="False" Width="65px" ValidationGroup="Buscar" />
                <asp:Button ID="cmdExportar" runat="server" 
                    Text="Exportar" Width="65px" />
            &nbsp;<asp:LinkButton ID="lnkReniec" runat="server" Font-Bold="False" 
                    ForeColor="Blue">Dar clic aquí para verificar DNI en RENIEC</asp:LinkButton>
            </td>
        </tr>
        </table>
    <asp:GridView ID="grwAlumnos" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" BorderStyle="None" 
        DataKeyNames="codigo_alu">
        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <EmptyDataRowStyle CssClass="sugerencia" />
        <Columns>
            <asp:BoundField HeaderText="#">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="alumno" HeaderText="Estudiante">
                <ItemStyle Width="20%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="tipodocident_alu" HeaderText="Tipo Doc." />
            <asp:BoundField DataField="dni" HeaderText="DNI Nuevo" >
            <ItemStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
            </asp:BoundField>
            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Escuela Profesional">
                <ItemStyle Width="10%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ing.">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="Domicilio" HeaderText="Dirección" >
                <ItemStyle Width="50%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Modificar" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Dar Acceso" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            No se encontrarios estudiantes según los criterios seleccionados
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" />
    
    </form>
</body>
</html>
