<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmcambiardptoplancurso.aspx.vb" Inherits="librerianet_cargaacademica_frmcambiardptoplancurso" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cambiar Departamento Plan Curso</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p align="right">
        <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Size="11pt" 
            ForeColor="Red"></asp:Label>
    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" />
    </p>
    
    <asp:GridView ID="grwPlanEscuela" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
        DataKeyNames="codigo_pes">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="codigo_pes" HeaderText="ID" />
            <asp:BoundField DataField="descripcion_pes" HeaderText="Plan de Estudios" />
            <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" />
            <asp:TemplateField HeaderText="Dpto. Académico">
                <ItemTemplate>
                    <asp:DropDownList ID="dpCodigo_dac" runat="server" DataTextField="nombre_dac" DataValueField="codigo_dac">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
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
