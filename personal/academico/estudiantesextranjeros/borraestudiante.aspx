<%@ Page Language="VB" AutoEventWireup="false" CodeFile="borraestudiante.aspx.vb" Inherits="librerianet_estudiantesextranjeros_borraestudiante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script src="../../../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
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
Dirección de Relaciones Internacionales</b></td>
    </tr>
    </table>
        <asp:GridView ID="gvEstudiante" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="1" 
            DataKeyNames="id_Estudiante" DataSourceID="SqlDataSource1" 
            ForeColor="#333333" BorderColor="#3399FF" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Arial" 
            Font-Size="Small">
            <RowStyle BackColor="#EFF3FB" CssClass="kclass1" />
            <Columns>
                <asp:CommandField ShowSelectButton="True" SelectText="." 
                    SelectImageUrl="arrow.jpg" />
                <asp:BoundField DataField="Carnet" HeaderText="Carné" 
                    SortExpression="carnet" />
                <asp:BoundField DataField="nombres" HeaderText="Nombres" 
                    SortExpression="nombres" />
                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" 
                    SortExpression="apellidos" />
                <asp:BoundField DataField="sexo" HeaderText="Sexo" 
                    SortExpression="sexo" />
                <asp:BoundField DataField="pasaporte" HeaderText="Pasaporte" 
                    SortExpression="pasaporte" />
                <asp:BoundField DataField="nacionalidad" HeaderText="Nacionalidad" 
                    SortExpression="nacionalidad" />
                <asp:BoundField DataField="fechaNacimiento" HeaderText="Fecha de Nacimiento" 
                    SortExpression="fechaNacimiento" />
                <asp:BoundField DataField="telefonos" HeaderText="Telefonos" 
                    SortExpression="telefonos" />
                <asp:BoundField DataField="fechaInicial" HeaderText="Fecha Inicio" 
                    SortExpression="fechaInicial" />
                <asp:BoundField DataField="fechaFinal" HeaderText="Fecha Final" 
                    SortExpression="fechaFinal" />
                <asp:BoundField DataField="eMail_Alu" HeaderText="Correo" 
                    SortExpression="eMail_Alu" />

                <asp:TemplateField>
                <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" OnClientClick="return confirm('¿Desea eliminar el registro?');" ImageUrl="../../../images/eliminar.gif" CommandName="Delete" />
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" 
                BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" 
                CssClass="kclass1" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="SELECT * FROM [EstudianteExtranjero]"></asp:SqlDataSource>
        
    </form>
</body>
</html>
