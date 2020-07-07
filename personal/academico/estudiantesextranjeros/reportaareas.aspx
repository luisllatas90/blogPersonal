<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reportaareas.aspx.vb" Inherits="librerianet_estudiantesextranjeros_reportaareas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Relacion de Alumnos Extranjeros Vigentes en la USAT</title>
    
    <style type="text/css">
  .kclass1
  {
  font: 10pt Verdana;
 color: black;
 text-decoration:none;
}
.kdiv
{
background:#DCFFFF;
}
table
{
background : #DCFFFF;
width:94%; 
font-family: Arial; font-size: 11pt;
}
body
{ background : #DCFFFF;
}
input { border: 1px solid #9999FF; padding: 0; background-color: #D2F0FF }
textarea     { border: 1px solid #99CCFF; padding: 0; background-color: #BBDDFF }
select       { border: 1px solid #9FE0FF; padding-left: 4px; padding-right: 4px; 
               padding-top: 1px; padding-bottom: 1px; background-color: 
               #BFEBFF }
     .style1
     {
         width: 406px;
     }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
    <tr>
    <td class="style1" >
    <p align="center"><b><font size="4" color="#800000">PIMEU</font> - Programa 
Internacional de Movilidad de Estudiantes de la USAT<br>
Dirección de Relaciones Internacionales</b><p align="center">Módulo de Reporte para Áreas</td>
    </tr>
    </table>
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
            SelectCommand="SELECT * FROM dbo.EstudianteExtranjero WHERE (visible = 'S')">
        </asp:SqlDataSource>
    
    </div>
    <asp:GridView ID="GridView1" runat="server" 
        AllowSorting="True" CellPadding="0" DataSourceID="SqlDataSource1" 
        ForeColor="#333333" GridLines="None" 
        style="font-family: Arial; font-size: small" AutoGenerateColumns="False" 
        BorderStyle="Double" BorderWidth="1px" PageSize="25" Width="762px">
        <RowStyle BackColor="#EFF3FB" BorderStyle="Solid" BorderWidth="1px" />
        <Columns>
            <asp:BoundField DataField="carnet" HeaderText="Carné" 
                SortExpression="carnet" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="nombres" HeaderText="Nombres" 
                SortExpression="nombres" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="apellidos" HeaderText="Apellidos" 
                SortExpression="apellidos" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="universidadOrigen" HeaderText="Universidad de Origen" 
                SortExpression="universidadOrigen" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaInicial" DataFormatString="{0:d}" 
                HeaderText="fecha Inicial" SortExpression="fechaInicial" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="FechaFinal" DataFormatString="{0:d}" 
                HeaderText="Fecha Final" SortExpression="fechaFinal" >
                <ItemStyle BorderStyle="Solid" BorderWidth="1px" />
            </asp:BoundField>
            <asp:BoundField DataField="pais_residencia" HeaderText="País" 
                SortExpression="pais_residencia" />
            <asp:BoundField DataField="convenio" HeaderText="Convenio" 
                SortExpression="convenio">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="sexo" HeaderText="Sexo" ReadOnly="True" 
                SortExpression="sexo">
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="#000066" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
            BorderStyle="Solid" BorderWidth="1px" />
        <EditRowStyle BackColor="#2461BF" BorderColor="Black" BorderStyle="Solid" 
            BorderWidth="1px" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
