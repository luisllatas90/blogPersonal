<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmactualizarversionprograma.aspx.vb" Inherits="frmactualizarversionprograma" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Actualización de versión de Programas de Profesionalización</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">Actualización de Grupos de Programas de Profesionalización</p>
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td style="width:90%">
                &nbsp;&nbsp;&nbsp;Programa: 
                <asp:DropDownList ID="dpPlanEstudio" 
                    runat="server" Font-Size="7pt">
                </asp:DropDownList>
                &nbsp;&nbsp; Grupo: <asp:DropDownList ID="dpVersion" 
                    runat="server">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" CssClass="buscar2" />
            &nbsp;&nbsp;
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
            </td>
            <td style="width:10%" align="right">
    <asp:Button ID="cmdGuardar0" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Guardar" Visible="False" CssClass="guardar2" />
        &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="margin-left: 80px" valign="top" colspan="2">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="2" GridLines="Horizontal" BorderStyle="None" DataKeyNames="codigo_alu">
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
            <asp:BoundField DataField="cicloIng_alu" HeaderText="Ciclo Ing.">
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField DataField="Domicilio" HeaderText="Dirección" >
                <ItemStyle Width="40%" Font-Size="7pt" />
            </asp:BoundField>
            <asp:BoundField DataField="telefonoCasa_Dal" HeaderText="Teléfono" >
                <ItemStyle Width="5%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Estado Actual">
                <ItemTemplate>
                    <asp:Label ID="lblEstado" runat="server" Text='<%# iif(eval("estadoactual_alu")=0,"Inactivo","Activo") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle Width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Grupo">
                <ItemTemplate>
                    <asp:DropDownList ID="dpversionprograma" runat="server" 
                        SelectedValue='<%# eval("edicionProgramaEspecial_Alu") %>'>
                        <asp:ListItem Value="0">-Seleccione-</asp:ListItem>
                        <asp:ListItem Value="1">1</asp:ListItem>
                        <asp:ListItem Value="2">2</asp:ListItem>
                        <asp:ListItem Value="3">3</asp:ListItem>
                        <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
			<asp:ListItem Value="11">11</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>
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
            <td align="right" colspan="2">
    <asp:Button ID="cmdGuardar1" runat="server" Font-Bold="False" Font-Overline="False" 
        Font-Underline="False" 
        Text="Guardar" Visible="False" CssClass="guardar2" />
            &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
