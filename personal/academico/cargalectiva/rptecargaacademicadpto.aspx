<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptecargaacademicadpto.aspx.vb" Inherits="rptecargaacademicadpto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignar Horas de  Carga Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        /*
        if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
        */
    </script>
    <style type="text/css">
        .lineaprofesor
        {
            color: #0000FF;
            font-weight: bold;
            border-left-width: 1;
	        border-right-width: 1;
	        border-top: 1px solid #808080;
	        border-bottom-width: 1;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Informe de Carga Académica en Proceso:&nbsp;
    </p>
<table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Ciclo Académico:
    <asp:DropDownList ID="dpCodigo_cac" runat="server">
    </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="dpFiltro" runat="server">
                    <asp:ListItem Value="1">Mostrar profesores que dictan en otro departamento</asp:ListItem>
                    <asp:ListItem Value="0">Mostrar todas las asignaciones académicas</asp:ListItem>
                </asp:DropDownList>
&nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                    CssClass="buscar2" Height="22px" />&nbsp;
                <asp:Button ID="cmdExportar" 
                    runat="server" Text="Exportar" 
                    CssClass="excel2" Height="22px" />
                </td>
        </tr>
        </table>
    <br />
    <asp:Label 
                    ID="lblmensaje" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Size="10pt" ForeColor="Red"></asp:Label>
            <asp:GridView ID="grwCargaAcademica" runat="server" AutoGenerateColumns="False" 
                    Width="100%" DataKeyNames="codigo_per" 
                    GridLines="Vertical" AllowSorting="True" BorderColor="#999999">
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BorderStyle="None" 
                        BorderWidth="0px" />
                    <Columns>
                        
                        <asp:BoundField DataField="docente" HeaderText="Profesor">
                            <ItemStyle Font-Size="7pt" Width="17%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dptoprofesor" HeaderText="Departamento" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="descripcion_fun" HeaderText="Función">
                        <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="totalHorasAula" HeaderText="Hrs. Clase">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="totalHorasAsesoria" HeaderText="Hrs. Asesoría">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Total Hrs." DataField="totalHoras_Car" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Curso">
                            <ItemTemplate>
                                <asp:Label ID="lblCurso" runat="server" Font-Italic="True" Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblDpto" runat="server" Text='<%# eval("dptocurso") %>'></asp:Label>.
                                <asp:Label ID="lblEscuela" runat="server" Text='<%# eval("abreviatura_cpf") %>'></asp:Label>
                                (<asp:Label ID="lblPlan" runat="server" Text='<%# eval("abreviatura_pes") %>'></asp:Label>)
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vacantes_cup" HeaderText="Vacantes">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Inscritos">
                            <ItemTemplate>
                                <asp:Label ID="lblinscritos" runat="server" 
                                    Text='<%# eval("nroMatriculados_Cup") + eval("nroPreMatriculados_Cup") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Registrado:">
                            <ItemTemplate>
                                <asp:Label ID="lblFecha" runat="server" 
                                    Text='<%# eval("fechareg_car","{0:g}") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblLogin" runat="server" Text='<%# eval("login_per") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontrarios cursos programados según los criterios seleccionados
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
            <p class="rojo">
                (*) No se toman en cuenta las horas asignadas en los Programas de 
                Profesionalización o Examenes Extraordinarios.</p>
    </form>
</body>
</html>
