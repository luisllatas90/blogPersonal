<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteresumencargaprofesoresdpto.aspx.vb" Inherits="rpteresumencargaprofesoresdpto" %>

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
        .celda1
        { border-right:2px solid #FF9900;}
        .celda2
        { border-right:2px solid #339933;}
        .celda3
        { border-right:2px solid #CC3300;}
        .etiqueta1
        { padding:2px; background-color:#FF9900; max-width:10px;min-width:10px;}
        .etiqueta2
        { padding:2px; background-color:#339933; max-width:10px;min-width:10px;}
        .etiqueta3
        { padding:2px; background-color:#CC3300; max-width:10px;min-width:10px;}
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
                    CellPadding="2" DataKeyNames="codigo_per" 
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
                        
                        <asp:BoundField DataField="horasR2" HeaderText="HR" >
                        <HeaderStyle BackColor="#FF9900" />
                        <ItemStyle Font-Bold="True" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="inscritosR" HeaderText="IR">
                        <HeaderStyle BackColor="#FF9900" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vacantesR" HeaderText="VR">
                        <HeaderStyle BackColor="#FF9900" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="gruposR" HeaderText="GR">
                        <HeaderStyle BackColor="#FF9900" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CursosR" HeaderText="CR">
                        <HeaderStyle BackColor="#FF9900" />
                        <ItemStyle CssClass="celda1" VerticalAlign="Middle" />
                        </asp:BoundField>

                        
                        
                        <asp:BoundField DataField="horasC2" HeaderText="HC">
                        <HeaderStyle BackColor="#339933" />
                        <ItemStyle Font-Bold="True" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="inscritosC" HeaderText="IC">
                        <HeaderStyle BackColor="#339933" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vacantesC" HeaderText="VC">
                        <HeaderStyle BackColor="#339933" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="gruposC" HeaderText="GC">
                        <HeaderStyle BackColor="#339933" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CursosC" HeaderText="CC">
                        <HeaderStyle BackColor="#339933" />
                         <ItemStyle CssClass="celda2" VerticalAlign="Middle" />
                        </asp:BoundField>

			            
			            <asp:BoundField DataField="horasP2" HeaderText="HP">
                        <HeaderStyle BackColor="#CC3300" />
                        <ItemStyle Font-Bold="True" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="inscritosP" HeaderText="IP">
                        <HeaderStyle BackColor="#CC3300" />
                         <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="vacantesP" HeaderText="VP">
                        <HeaderStyle BackColor="#CC3300" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="gruposP" HeaderText="GP">
                        <HeaderStyle BackColor="#CC3300" />
                        <ItemStyle  VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CursosP" HeaderText="CP">
                        <HeaderStyle BackColor="#CC3300" />
                         <ItemStyle CssClass="celda3" VerticalAlign="Middle" />
                         
                        </asp:BoundField>

                    </Columns>
                    <EmptyDataTemplate>
                        <p class="rojo"><b>No se han encontrado profesores registrados</b></p>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#3366CC" ForeColor="White" />
                </asp:GridView>
    
    </form>
<br>En los Seminario de Tesis 2 y 3, no se diferencia el nro de estudiantes asesorados por asesor. Ya que existe un solo grupo para c/curso
<p><b><u>LEYENDA</u></b></p>

<span class="etiqueta1">&nbsp;&nbsp;&nbsp;</span> Cursos en Pregrado Regular


<ol>
    <li><b>HR:</b> Total de Horas</li>
    <li>IR: Total de Matriculados</li>
    <li>VR: Total de Vacantes</li>
    <li>GR: Total de Grupos</li>
    <li>CR: Total de Cursos</li>
</ol>

<span class="etiqueta2">&nbsp;&nbsp;&nbsp;</span> Cursos Complementarios
<ol>
    
    <li>HC: Total de Horas</li>
    <li>IC: Total de Matriculados</li>
    <li>VC: Total de Vacantes</li>
    <li>GC: Total de Grupos</li>
    <li>CP: Total de Cursos</li>
</ol>

<span class="etiqueta3">&nbsp;&nbsp;&nbsp;</span> Cursos en Otros Tipos de Estudio

<ol>
    
    <li>HP: Total de Horas</li>
    <li>IP: Total de Matriculados</li>
    <li>VP: Total de Vacantes</li>
    <li>GP: Total de Grupos</li>
    <li>CP: Total de Cursos</li>
</ol>
</body>
</html>
