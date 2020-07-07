<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LstUsuariosMVA.aspx.vb" Inherits="LstUsuariosMVA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Estudiantes Extranjeros Registrados</title>
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
width:97%; 
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
     .style2
     {
         width: 397px;
     }
     .style3
     {
         width: 393px;
     }
     .style4
     {
     }
     .style5
     {
     }
     .style6
     {
     }
     .style7
     {
     }
     .style8
     {
         width: 217px;
     }
     .style9
     {
     }
     .style10
     {
         width: 372px;
     }
 </style>


</head>
<body>
    <form id="form1" runat="server">
    <div class="kdiv">
    <table>
    <tr>
    <td class="style1" >
    <p align="center" style="width: 1030px"><b><font size="4" color="#800000">MVA</font> - Módulo Virtual de Auto Evaluación</b><br>
Universidad Católica Santo Toribio de Mogrovejo<p><strong>Usuarios Registrados</strong></p></td>
    </tr>
    </table>
    
        <asp:GridView ID="GridView1" runat="server" 
            AllowSorting="True" AutoGenerateColumns="False" CellPadding="1"
            DataSourceID="SqlDataSource1" 
            ForeColor="#333333" BorderColor="#3399FF" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Arial" 
            Font-Size="Small">
            <RowStyle BackColor="#EFF3FB" CssClass="kclass1" />
            <Columns>
                <asp:CommandField ShowSelectButton="false" InsertText="."
                    SelectImageUrl="../../images/arrow.gif" />
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" 
                    SortExpression="Nombres" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" 
                    SortExpression="Apellidos" />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" 
                    SortExpression="Usuario" />
                <asp:BoundField DataField="Funcion" HeaderText="Funcion" 
                    SortExpression="Funcion" />
                <asp:BoundField DataField="Estado_Usu" HeaderText="Estado_Usu" 
                    SortExpression="Estado_Usu" />
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#000066" HorizontalAlign="Center" />
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
        SelectCommand="
        SELECT DISTINCT p.nombres_Per AS [Nombres],p.apellidoPat_Per+' '+p.apellidoMat_Per AS [Apellidos],p.usuario_per as [Usuario]
	,tf.descripcion_Tfu AS [Funcion],descripcion_Est as [Estado_Usu]
	from vstpersonal p 
		left join personal pe on P.codigo_Per = pe.codigo_Per
		left join UsuarioAplicacion UA ON ua.codigo_Uap = p.codigo_Per
		left JOIN Aplicacion a ON a.codigo_Apl = ua.codigo_Apl
		left JOIN TipoFuncion tf ON tf.codigo_Tfu = ua.codigo_Tfu
	WHERE A.codigo_Apl=52
        
        "></asp:SqlDataSource>
    <hr />
   
    
    </form>
</body>
</html>