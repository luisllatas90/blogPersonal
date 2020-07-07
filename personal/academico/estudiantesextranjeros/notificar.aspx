<%@ Page Language="VB" AutoEventWireup="false" CodeFile="notificar.aspx.vb" Inherits="librerianet_estudiantesextranjeros_Notificar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
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
Dirección de Relaciones Internacionales</b></td>
    </tr>
    </table>
    
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        
            
            SelectCommand="SELECT *  FROM [EstudianteExtranjero] ORDER BY [fechaInicial] DESC">
    </asp:SqlDataSource>
    
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" DataSourceID="SqlDataSource1" ForeColor="#333333" 
            GridLines="None" AllowPaging="True" AllowSorting="True" 
            DataKeyNames="id_Estudiante" PageSize="15">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:CommandField SelectText=".." ShowSelectButton="True" />
                <asp:BoundField DataField="carnet" HeaderText="carnet" 
                    SortExpression="carnet" />
                <asp:BoundField DataField="nombres" HeaderText="nombres" 
                    SortExpression="nombres" />
                <asp:BoundField DataField="apellidos" HeaderText="apellidos" 
                    SortExpression="apellidos" />
                <asp:BoundField DataField="fechaInicial" DataFormatString="{0:D}" 
                    HeaderText="Fecha Inicial" SortExpression="fechaInicial" />
                <asp:BoundField DataField="fechaFinal" DataFormatString="{0:d}" 
                    HeaderText="Fecha Final" SortExpression="fechaFinal" />
                <asp:BoundField DataField="visible" HeaderText="visible" 
                    SortExpression="visible" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" BorderColor="#333399" BorderWidth="1px" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
    
        <p style="font-family: Arial; font-size: small">
            En la columna <b>Visible</b> activar con &quot;S&quot; los alumnos que durante el semestre 
            actual estarán visibles para las otras áreas de la universidad que lo requieran</p>

    
        <asp:Button ID="Button1" runat="server" Text="Cambiar Estado" 
        Height="20px" /> &nbsp; &nbsp; 
    <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="46px">
        <asp:ListItem>S</asp:ListItem>
        <asp:ListItem>N</asp:ListItem>
    </asp:DropDownList>
    </form>

    
        </body>
</html>
