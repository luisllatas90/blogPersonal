<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteresumencargaprofesoresdpto-backup.aspx.vb" Inherits="rpteresumencargaprofesoresdpto" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Responsabilidades académicas y Administrativas</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       if(top.location==self.location)
        {location.href='../../../tiempofinalizado.asp'} //Depende de la ruta de la página
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
    <p class="usatTitulo">Consolidado de Carga Académica por Departamento Académico</p>
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
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="2" DataKeyNames="codigo_per" GridLines="Both" 
                    Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:HyperLinkField DataTextField="profesor" HeaderText="Apellidos y Nombre">
                            <ControlStyle CssClass="detalleprofesor" />
                            <ItemStyle CssClass="detalleprofesor" Width="20%" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="titulos" HeaderText="Títulos" >
                            <ItemStyle Font-Size="7pt" Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_ded" HeaderText="Dedicación" >
                            <ItemStyle Font-Size="7pt" />
                        </asp:BoundField>
                        <asp:BoundField DataField="horasR" HeaderText="HR" ></asp:BoundField>
                        <asp:BoundField DataField="inscritosR" HeaderText="IR"></asp:BoundField>
                        <asp:BoundField DataField="vacantesR" HeaderText="VR"></asp:BoundField>
                        <asp:BoundField DataField="gruposR" HeaderText="GR"></asp:BoundField>
                        <asp:BoundField DataField="CursosR" HeaderText="CR"></asp:BoundField>

                        <asp:BoundField DataField="horasE" HeaderText="HE" ></asp:BoundField>
                        <asp:BoundField DataField="inscritosE" HeaderText="IE"></asp:BoundField>
                        <asp:BoundField DataField="vacantesE" HeaderText="VE"></asp:BoundField>
                        <asp:BoundField DataField="gruposE" HeaderText="GE"></asp:BoundField>
                        <asp:BoundField DataField="CursosE" HeaderText="CE"></asp:BoundField>
                        
                        <asp:BoundField DataField="horasC" HeaderText="HC"></asp:BoundField>
                        <asp:BoundField DataField="inscritosC" HeaderText="IC"></asp:BoundField>
                        <asp:BoundField DataField="vacantesC" HeaderText="VC"></asp:BoundField>
                        <asp:BoundField DataField="gruposC" HeaderText="GC"></asp:BoundField>
                        <asp:BoundField DataField="CursosC" HeaderText="CC"></asp:BoundField>

			<asp:BoundField DataField="horasP" HeaderText="HP"></asp:BoundField>
                        <asp:BoundField DataField="inscritosP" HeaderText="IP"></asp:BoundField>
                        <asp:BoundField DataField="vacantesP" HeaderText="VP"></asp:BoundField>
                        <asp:BoundField DataField="gruposP" HeaderText="GP"></asp:BoundField>
                        <asp:BoundField DataField="CursosP" HeaderText="CP"></asp:BoundField>

                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado profesores registrados</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
    
    </form>
<br>En los Seminario de Tesis 2 y 3, no se diferencia el nro de estudiantes asesorados por asesor. Ya que existe un solo grupo para c/curso
<p><b><u>LEYENDA</u></b></p>
<ol>
    <li>HR: Total de Horas de cursos de Escuelas</li>
    <li>IR: Total de Matriculados en cursos de Escuelas</li>
    <li>VR: Total de Vacantes en cursos de Escuelas</li>
    <li>GR: Total de Grupos en cursos de Escuelas</li>
    <li>CR: Total de Cursos de Escuelas</li>
</ol>
<ol>
    <li>HE: Total de Horas de cursos de examen extraordinario</li>
    <li>IE: Total de Matriculados en cursos de examen extraordinario</li>
    <li>VE: Total de Vacantes en cursos de examen extraordinario</li>
    <li>GE: Total de Grupos en cursos de examen extraordinario</li>
    <li>CR: Total de Cursos de examen extraordinario</li>
</ol>
<ol>
    <li>HC: Total de Horas de cursos complementarios</li>
    <li>IC: Total de Matriculados en cursos complementarios</li>
    <li>VC: Total de Vacantes en cursos complementarios</li>
    <li>GC: Total de Grupos en cursos complementarios</li>
    <li>CR: Total de Cursos en cursos complementarios</li>
</ol>
<ol>
    <li>HP: Total de Horas de cursos de profesionalización/postgrado</li>
    <li>IP: Total de Matriculados en cursos de profesionalización/postgrado</li>
    <li>VP: Total de Vacantes en cursos de profesionalización/postgrado</li>
    <li>GP: Total de Grupos en cursos de profesionalización/postgrado</li>
    <li>CR: Total de Cursos en cursos de profesionalización/postgrado</li>
</ol>
</body>
</html>
