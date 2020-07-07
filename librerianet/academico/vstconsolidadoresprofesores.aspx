<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstconsolidadoresprofesores.aspx.vb" Inherits="personal_academico_tesis_vstconsolidadoresprofesores" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Responsabilidades académicas y Administrativas</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       if(top.location==self.location)
        {location.href='../../tiempofinalizado.asp'} //Depende de la ruta de la página
    </script>
    <style type="text/css">
        .detalleprofesor
        {
            font-size: 9px;
            color: #0000FF;
            text-decoration: underline;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Consolidado de responsabilidades académicas y adminstrativas</p>
    <p><b>Departamento Académico:</b>
    <asp:DropDownList ID="dpDpto" runat="server" Font-Size="7pt">
    </asp:DropDownList>
    &nbsp; <b>Semestre académico</b>:
    <asp:DropDownList ID="dpCiclo" runat="server">
    </asp:DropDownList>
    &nbsp;
    <asp:Button ID="cmdBuscar" runat="server" CssClass="buscar2" Text="     Buscar" Width="60px" />
    &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="excel2" 
            Text="    Exportar" Visible="False" />
    </p>
    <p style="text-align:right">
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" ShowFooter="True" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px">
                    <RowStyle Font-Bold="False" />
                    <Columns>
                        <asp:BoundField HeaderText="Dedicación" DataField="descripcion_ded" >
                        </asp:BoundField>
                        <asp:BoundField DataField="total" HeaderText="Total" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                     <FooterStyle 
            HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado Profesores registrados</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
    </p>
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" DataKeyNames="codigo_per" GridLines="Vertical" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:HyperLinkField DataTextField="profesor" HeaderText="Apellidos y Nombre">
                            <ControlStyle CssClass="detalleprofesor" />
                            <ItemStyle CssClass="detalleprofesor" Width="20%" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="titulos" HeaderText="Títulos" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_ded" HeaderText="Dedicación" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cur" HeaderText="Asignaturas" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="totalHoras_Car" HeaderText="Horas Acad." >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Cco" HeaderText="Área administrativa" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Cgo" HeaderText="Cargo Administrativo" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Horas Adm.">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado profesores registrados</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
    
    </form>
</body>
</html>
