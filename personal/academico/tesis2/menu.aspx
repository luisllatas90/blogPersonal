<%@ Page Language="VB" AutoEventWireup="false" CodeFile="menu.aspx.vb" Inherits="menu" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
</head>
<body bgcolor="#9999ff">
    <form id="form1" runat="server">
        <p style="font-family: Arial, Helvetica, sans-serif; font-size: medium; font-weight: bold; color: #FFFFCC">Opciones de menú</p>
                <asp:TreeView ID="TreeView2" runat="server" ImageSet="Arrows" BackColor="White" 
                    BorderColor="#006600" BorderStyle="Solid" BorderWidth="2px">
                    <ParentNodeStyle Font-Bold="False" />
                    <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" 
                        HorizontalPadding="0px" VerticalPadding="0px" />
                    <Nodes>
                        <asp:TreeNode Text="Director de Escuela" Value="Director de Escuela">
                            <asp:TreeNode Text="Aperturar Investigación" 
                                Value="Proyectos de Investigación" NavigateUrl="frmproyecto.aspx?tipo=0" 
                                Target="contenido"></asp:TreeNode>
                            <asp:TreeNode Text="Registar tema de investigación" Value="Solicitar asesor" 
                                NavigateUrl="frmproyecto.aspx?tipo=1" Target="contenido"></asp:TreeNode>
                            <asp:TreeNode Text="Consultar investigaciones" Value="Tesis" 
                                NavigateUrl="listainvestigaciones.aspx" Target="contenido"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="evaluacioninvescuela.aspx" Target="contenido" 
                                Text="Evaluar investigaciones" Value="Evaluar investigaciones">
                            </asp:TreeNode>
                            <asp:TreeNode Text="Sustentación de tesis" Value="Sustentaciones" 
                                NavigateUrl="sustentaciontesis.aspx" Target="contenido"></asp:TreeNode>
			<asp:TreeNode Text="Consulta de tesis" Value="Consulta de tesis" 
                                NavigateUrl="reporte2.asp" Target="contenido"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Asesor" Value="Asesor">
                            <asp:TreeNode Text="Registrar disponibilidad horaria" 
                                Value="Calendario de disponibilidad" 
                                NavigateUrl="frmdisponibilidadhoraria.aspx" Target="contenido"></asp:TreeNode>
                            <asp:TreeNode Text="Registrar asesorías" Value="Asesorías" 
                                NavigateUrl="listacitas.aspx" Target="contenido"></asp:TreeNode>
                            <asp:TreeNode NavigateUrl="evaluacioninvasesor.aspx" Target="contenido" 
                                Text="Evaluar investigaciones" Value="Evaluación de tesis"></asp:TreeNode>
                        </asp:TreeNode>
                        <asp:TreeNode Text="Tesista" Value="Estudiante">
                            <asp:TreeNode Text="Registrar citas" 
                                Value="Citas" NavigateUrl="separarcita.aspx" Target="contenido"></asp:TreeNode>
                            <asp:TreeNode Text="Registrar asesorías" 
                                Value="Asesorías" NavigateUrl="listacitastesista.aspx" Target="contenido"></asp:TreeNode>
                        </asp:TreeNode>
                    </Nodes>
                    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
                        HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
                </asp:TreeView>

    </form>
</body>
</html>
