﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstasignarhorascargaacademicaescuela.aspx.vb" Inherits="lstasignarhorascargaacademicaescuela" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignar Horas de  Carga Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
        
        if(top.location==self.location)
        {location.href='../../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
       
    </script>
    <style type="text/css">
        .lineaprofesor
        {
            color: #0000FF;
            border-left-width: 1;
	        border-right-width: 1;
	        border-top: 1px solid #808080;
	        border-bottom-width: 1;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Actualización de Horas de Carga Académica:&nbsp;
    <asp:DropDownList ID="dpCodigo_cac" runat="server">
    </asp:DropDownList>
    </p>
<table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                &nbsp;
                Carrera Profesional:
                <asp:DropDownList ID="dpEscuela" runat="server">
                </asp:DropDownList>
                &nbsp;<asp:DropDownList ID="dpFiltro" runat="server">
                    <asp:ListItem Value="2">Mostrar Sólo Docentes Asignados</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                    CssClass="buscar2" Height="22px" />&nbsp;
                <asp:Button ID="cmdVer" runat="server" CssClass="boton" Height="22px" 
                    Text="Ver Consolidado" />
                </td>
        </tr>
        </table>
    <br />
    <table border="1" bordercolor="#808080" style="border-collapse: collapse; width:100%" cellpadding="2" cellspacing="0">
        <tr style="background-color: #e8eef7; color: #3366CC; font-weight: bold; text-align: center;">
            <td rowspan="2" style="width:3%">Ciclo</td>
            <td rowspan="2" style="width:20%">Curso</td>
            <td rowspan="2" style="width:5%">Grupo</td>
            <td rowspan="2" style="width:7%">Departamento</td>
            <td colspan="2" style="width:25%">Docente Asignado</td>
            <td colspan="3" style="width:40%">Asignación de Horas Académicas</td>
        </tr>
        <tr style="background-color: #e8eef7; color: #3366CC; font-weight: bold; text-align: center;">
            <td style="width:15%">Apellidos y Nombres</td>
            <td style="width:10%">Función</td>
            <td style="width:10%">Clase</td>
            <td style="width:5%">Asesoría</td>
            <td style="width:5%">Total</td>
        </tr>
        <tr>
            <td colspan="9" style="width:100%">
            <div id="listadiv" style="width:100%; height:450px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    Width="100%" DataKeyNames="codigo_cup,codigo_perAsig" 
                    GridLines="Vertical" AllowSorting="True" ShowHeader="False" 
                    CellPadding="1">
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BorderStyle="None" 
                        BorderWidth="0px" />
                    <Columns>
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                            <ItemStyle HorizontalAlign="Center" Width="3%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Curso">
                            <ItemTemplate>
                                <asp:Label ID="lblCurso" runat="server" Text='<%# Bind("nombre_cur") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lblPlan" runat="server" Font-Italic="True" 
                                    Text='<%# Bind("abreviatura_pes") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" Font-Size="7pt" />
                        </asp:TemplateField>
                        
                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                        <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="abreviatura_dac" HeaderText="Departamento">
                            <ItemStyle Width="10%" Font-Size="7pt" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="profesor" HeaderText="Profesor">
                            <ItemStyle Font-Size="7pt" Width="16%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Función">
                            <ItemTemplate>
                                <asp:DropDownList ID="dpcodigo_fun" runat="server" 
                                    SelectedValue='<%# eval("codigo_fun") %>' Font-Size="7pt">
                                    <asp:ListItem Value="1">Docente Coordinador</asp:ListItem>
                                    <asp:ListItem Value="3">Docente</asp:ListItem>
                                    <asp:ListItem Value="4">Asesor</asp:ListItem>
                                </asp:DropDownList>
                            </ItemTemplate>
                            <ItemStyle Font-Size="7pt" Width="10%" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Clase">
                            <ItemTemplate>
                                <asp:DropDownList ID="cboTipoClase" runat="server" 
                                    SelectedValue='<%# eval("tipoHorasAula_Car") %>' Font-Size="7">
                                    <asp:ListItem Value="TE">Teoría</asp:ListItem>
                                    <asp:ListItem Value="PR">Práctica</asp:ListItem>
                                    <asp:ListItem Value="TO">Todo</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtHorasClase" runat="server" Columns="3" CssClass="cajas" 
                                    MaxLength="2" Text='<%# Bind("totalHorasAula") %>' Visible="False"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Asesoría">
                            <ItemTemplate>
                                <asp:TextBox ID="txtHorasAsesoria" runat="server" Columns="3" CssClass="cajas" 
                                    MaxLength="2" Text='<%# Bind("totalHorasAsesoria") %>' Visible="False"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Total" DataField="totalHoras_Car" >
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontrarios cursos programados con asignación académica.
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
            </div>
            </td>
        </tr>
        <tr style="background-color: #e8eef7; color:White; height: 30px;">
            <td colspan="9" style="width:100%" align="right"><asp:Button ID="cmdGuardar" 
                    runat="server" CssClass="guardar2" 
                    Text="    Guardar cambios" Width="110px" Height="22px" Visible="False" />&nbsp;<asp:Label 
                    ID="lblmensaje" runat="server" Font-Bold="True" 
                    Font-Italic="False" Font-Size="10pt" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
