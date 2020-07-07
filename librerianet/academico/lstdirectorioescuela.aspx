<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstdirectorioescuela.aspx.vb" Inherits="lstdirectorio" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Directorio de Estudiantes</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <p class="usatTitulo">
        Directorio de Estudiantes</p>
    <form id="form1" runat="server">
    
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td style="width:80%">
                &nbsp;&nbsp;&nbsp; Escuela / Programa: <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
                &nbsp;&nbsp;Ciclo Matriculado
                <asp:DropDownList ID="dpciclo" runat="server">
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Buscar" CssClass="buscar2" />
            </td>
            <td style="width:20%" align="right">
        &nbsp;&nbsp;
        <asp:Button ID="cmdExportar" runat="server" Text="Exportar" CssClass="excel2" />
            </td>
            
        </tr>
        <tr bgcolor="#C8D9EE" style="height:20px">
            <td style="width:80%" class="azul">
                &nbsp;&nbsp; Haga clic en los encabezados de la tabla para ordenar sus resultados 
                forma ascendente y descendente.</td>
            <td style="width:20%" align="right">
                &nbsp;</td>
            
        </tr>
        <tr>
            <td style="margin-left: 80px" valign="top" colspan="2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" GridLines="Horizontal" BorderStyle="None" DataKeyNames="codigo_alu" 
                    
                    AllowSorting="True" PageSize="20" 
                    >
        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
        <EmptyDataRowStyle CssClass="sugerencia" />
        <Columns>
            <asp:BoundField HeaderText="#">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" 
                SortExpression="codigouniver_alu">
                <ItemStyle Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="alumno" HeaderText="Estudiante" 
                SortExpression="alumno">
                <ItemStyle Width="20%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="abreviatura_cpf" HeaderText="Escuela Profesional" 
                SortExpression="abreviatura_cpf">
                <ItemStyle Width="10%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ing." 
                SortExpression="cicloIng_alu">
                <ItemStyle Width="10%" Font-Size="7pt"/>
            </asp:BoundField>
            <asp:BoundField DataField="sexo_Alu" HeaderText="Sexo" >            
                <ItemStyle Width="10%" Font-Size="7pt"/>
            </asp:BoundField>    
            <asp:BoundField DataField="Domicilio" HeaderText="Dirección" 
                SortExpression="Domicilio" >
                <ItemStyle Width="45%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="telefonoCasa_Dal" HeaderText="Teléfono" 
                SortExpression="telefonoCasa_Dal" >
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="telefonoMovil_Dal" HeaderText="Celular" />
            <asp:BoundField DataField="email_Usat" HeaderText="Email Usat" 
                SortExpression="email_Usat" />
            <asp:BoundField DataField="eMail_Alu" HeaderText="Email 1" 
                SortExpression="eMail_Alu" />
            <asp:BoundField DataField="email2_Alu" HeaderText="Email 2" 
                SortExpression="email2_Alu" />
            <asp:BoundField DataField="condicion_alu" HeaderText="Condición" 
                SortExpression="condicion_alu" />
            <asp:BoundField DataField="precioCreditoAct_Alu" HeaderText="Pr. Cred." />
            <asp:BoundField DataField="religion_dal" HeaderText="Religión" />
            <asp:BoundField DataField="Nombre_ied" HeaderText="Colegio" />
            <asp:BoundField DataField="nombre_dis" HeaderText="Distrito" />
            <asp:BoundField DataField="nombre_pro" HeaderText="Provincia" />
            <asp:BoundField DataField="nombre_dep" HeaderText="Departamento" />
            <asp:BoundField DataField="fechaNacimiento_Alu" HeaderText="Fecha Nac." />
        </Columns>
        <EmptyDataTemplate>
            No se encontrarios estudiantes según los criterios seleccionados
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    
    
            </td>
        </tr>
        <tr bgcolor="#91b4de" style="height:30px">
            <td colspan="2">
            &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
