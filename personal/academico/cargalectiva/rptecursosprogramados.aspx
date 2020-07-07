<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptecursosprogramados.aspx.vb" Inherits="rptecursosprogramados" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Cursos Programados por Escuela Profesional</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
</head>
<body>
    <p class="usatTitulo">Reporte de Cursos Programados</p>
    <form id="form1" runat="server">
<table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                &nbsp;
                Escuela Profesional:
                <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
&nbsp; Ciclo:
                <asp:DropDownList ID="dpCiclo" runat="server">
                </asp:DropDownList>
                &nbsp; 
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                    CssClass="buscar2" Height="22px" />&nbsp;
                <asp:Button ID="cmdExportar" 
                    runat="server" Text="Exportar" 
                    CssClass="excel2" Height="22px" />
            &nbsp;<asp:Label ID="lblmensaje" runat="server" Font-Bold="True" ForeColor="White"></asp:Label>
            </td>
        </tr>
        </table>
        <br />
        <asp:GridView ID="grwCursosProgramados" runat="server" AutoGenerateColumns="False" 
                    Width="100%" DataKeyNames="codigo_cup,refcodigo_cup" CellPadding="2" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" 
                    GridLines="Vertical">
                    <RowStyle BorderColor="#C2CFF1" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BorderStyle="None" 
                        BorderWidth="0px" />
                    <Columns>
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Curso">
                            <ItemTemplate>
                                <asp:Label ID="lblcurso" runat="server" Font-Bold="True" 
                                    Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblNombre_cpf" runat="server" Font-Italic="True" 
                                    Text='<%# eval("nombre_cpfO") %>' ForeColor="Maroon"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Underline="True" />
                            <ItemStyle Width="30%" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_dac" HeaderText="Dpto. Académico">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="nombre_curO" HeaderText="Cursos Agrupados" />
                        <asp:BoundField DataField="nroMatriculados_Cup" HeaderText="Mat">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nroPreMatriculados_Cup" HeaderText="Pre-Mat" 
                            Visible="False">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Total" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lbltotal" runat="server" 
                                    Text='<%# eval("nroMatriculados_Cup") + eval("nroPreMatriculados_Cup") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                    </Columns>
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontrarios cursos programados según los criterios seleccionados
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
    </form>
</body>
</html>
