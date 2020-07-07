<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rpteprogramacionacademica.aspx.vb" Inherits="rpteprogramacionacademica" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Programación Académica</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="javascript">
       if(top.location==self.location)
        {location.href='../../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usattitulo">Reporte de Programación Académica</p>
    <table cellpadding="0" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" border="0">
        <tr bgcolor="#91b4de" style="height:30px">
            <td>
                &nbsp;
                Ciclo <asp:DropDownList ID="dpCiclo" runat="server"></asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="dpEscuela" 
                    runat="server" Font-Size="7pt">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" CssClass="buscar2" />
                &nbsp;<asp:Button ID="cmdExportar" runat="server" Text="Exportar" 
                    CssClass="excel2" Visible="False" />
            &nbsp;</td>
        </tr>
        </table>
        <br />
        <asp:GridView ID="grwCursosProgramados" runat="server" AutoGenerateColumns="False" 
                    Width="100%" DataKeyNames="codigo_cup" CellPadding="2" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                    <RowStyle BorderColor="#C2CFF1" />
                    <EmptyDataRowStyle Font-Bold="True" ForeColor="Red" BorderStyle="None" 
                        BorderWidth="0px" />
                    <Columns>
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="tipo_cur" HeaderText="Tipo" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
                        <asp:BoundField DataField="creditos_cur" HeaderText="Crd." >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="totalhoras_cur" HeaderText="TH" Visible="false" >
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="abreviatura_dac" HeaderText="Dpto. Académico">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fechainicio_cup" HeaderText="Inicio" 
                            DataFormatString="{0:d}">
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="fechafin_cup" DataFormatString="{0:d}" 
                            HeaderText="Fin" />
                        <asp:BoundField DataField="profesor" HeaderText="Profesor" Visible="false" />
                        
                        <asp:BoundField DataField="inscritos" HeaderText="Matriculados" >
                        
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                         <asp:BoundField DataField="descripcion_Pes" HeaderText="Plan Estudios" Visible="false">
                        
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        &nbsp;&nbsp;&nbsp;&nbsp; No se encontrarios cursos programados según los criterios seleccionados
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:GridView>
    <p>
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" 
            CssClass="rojo"></asp:Label>
            </p>
    </form>
</body>
</html>
