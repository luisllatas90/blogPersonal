<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listaemail.aspx.vb" Inherits="librerianet_outlookusat_listaemail" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-MAIL USAT</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">

    <p class="usattitulo">E-mail de estudiantes. Ciclo Vigente:
    <asp:Label ID="lblCiclo" runat="server"></asp:Label></p>
    <p>
    Filtrar:
    <asp:DropDownList ID="dpFiltro" runat="server" AutoPostBack="True">
        <asp:ListItem Value="2">Estudiantes que les falta correo USAT</asp:ListItem>
        <asp:ListItem Value="1">Todos los alumnos matriculados</asp:ListItem>
    </asp:DropDownList>
    <asp:HiddenField ID="hdCodigo_cac" runat="server" />
    </p>
    <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
        AutoGenerateColumns="False" CellPadding="4" DataSourceID="SqlDataSource1" 
        ForeColor="#333333" GridLines="None" Width="100%">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField HeaderText="Nro" />
            <asp:BoundField DataField="codigouniver_Alu" HeaderText="Código" 
                SortExpression="codigouniver_Alu">
                <HeaderStyle Font-Underline="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Alumno" HeaderText="Alumno" 
                SortExpression="Alumno" />
            <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" 
                SortExpression="nombre_cpf" />
            <asp:BoundField DataField="CicloIng_alu" HeaderText="Ciclo Ingreso" 
                SortExpression="CicloIng_alu" />
            <asp:BoundField DataField="Email_alu" HeaderText="Email 1" 
                SortExpression="Email_alu" />
            <asp:BoundField DataField="Email2_alu" HeaderText="Email 2" 
                SortExpression="Email2_alu" />
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="ConsultarEmailUSAT" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:ControlParameter ControlID="dpFiltro" DefaultValue="2" Name="tipo" 
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="hdCodigo_cac" DefaultValue="" Name="param1" 
                PropertyName="Value" Type="String" />
        </SelectParameters>
       
    </asp:SqlDataSource>

    </form>
</body>
</html>
