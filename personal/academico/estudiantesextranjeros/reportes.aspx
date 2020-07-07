<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation ="false" CodeFile="reportes.aspx.vb" Inherits="librerianet_estudiantesextranjeros_reportes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reportes</title>
    
        <style type="text/css">

        #txtApellidos  { width: 380px; }
        #txtNombres    { width: 380px; }
        #txtEmail      { width: 380px; }
        #txtDireccionPermanente   {  width: 380px; }
        #txtLocalidad    { width: 380px;}
        #txtPais  {width: 380px; }
        #txtUniversidadOrigen {width: 380px;}
        #txtPaisUniversidad { width: 380px;}
        #txtFacultad { width: 380px; }
        #txtGpoSanguineo {width: 380px;}
        #TextArea1 {width: 380px;height: 72px;}
        #Button1 {height: 26px;}
        #txtNombres0 {width: 380px;}
        #Button2 {height: 26px;}
        </style>
        
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
width:127%; 
font-family: Arial; font-size: 11pt;
}
body
{ background : #DCFFFF;
}
input { border: 1px solid #9999FF; padding: 0; background-color: #D2F0FF;
        height: 18px;
    }
textarea     { border: 1px solid #99CCFF; padding: 0; background-color: #BBDDFF }
select       { border: 1px solid #9FE0FF; padding-left: 4px; padding-right: 4px; 
               padding-top: 1px; padding-bottom: 1px; background-color: 
               #BFEBFF }
    .style1
    {
        font-family: Arial;
        font-size: small;
        text-align: center;
    }
    .style2
    {
        width: 969px;
        text-align: center;
    }
    .style3
    {
        width: 215px;
    }
    .style4
    {
        width: 576px;
    }
    .style7
    {
        text-align: center;
        font-weight: bold;
    }
    .style8
    {
        width: 325px;
    }
    .style9
    {
        width: 295px;
    }
    .style10
    {
        width: 235px;
    }
    .style11
    {
        color: #990000;
        font-size: medium;
    }
</style>        

</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 972px">
    <table style="border: thin solid #000000; width: 737px; border-collapse: collapse; border-spacing: inherit;" 
            bgcolor="#92B3CB" border="1px" cellpadding="1">
            <tr>
                <td class="style7" bgcolor="#99CCFF" colspan="6">
                    <span class="style11">PIMEU </span>-<b> Programa Internacional de Movilidad de 
                    Estudiantes - USAT<br>
Dirección de Relaciones Internacionales</b></td>

            </tr>
            <tr>
                <td class="style7" bgcolor="#99CCFF" colspan="6">
                    Reportes de Estudiantes Extranjeros 
                    <br />
                    Módulo de Consulta</td>

            </tr>
            <tr>
                <td class="style2">
                    <span class="style1">Ingrese el Año</span></td>
                <td class="style3" align="center">
                    <span class="style1">Area de estudios </span>
                </td>
                <td class="style10" align="center">
                    <span class="style1">Sexo</span></td>
                    
                                    <td class="style9" align="center">
                                        <span class="style1">Universidad de Origen </span></td>
                <td class="style8" align="center">
                    &nbsp;<span class="style1">Pais de Origen</span></td>

                <td class="style4" align="center">
                    <asp:Button ID="Button2" runat="server" Text="Procesar" Width="105px" />
                </td>

            </tr>
            <tr>
                <td class="style2">
                    <asp:TextBox ID="TextBox1" 
            runat="server" Width="37px"></asp:TextBox>
    
                </td>
                <td class="style3">
                    <asp:TextBox ID="TextBox2" 
            runat="server" Width="163px" Height="17px">%</asp:TextBox>
    
                </td>
                <td class="style10">
                    <asp:TextBox ID="TextBox3" 
            runat="server" Width="28px" Height="17px">%</asp:TextBox>
                </td>
                 <td class="style9">
                     &nbsp;<asp:TextBox ID="TextBox4" 
            runat="server" Width="163px" Height="17px">%</asp:TextBox>
    
                </td>
                <td class="style8">
                    &nbsp;<asp:TextBox ID="TextBox5" 
            runat="server" Width="163px" Height="17px">%</asp:TextBox>
    
                </td>

                <td class="style4">
        <asp:Button ID="Button3" runat="server" Text="Exportar Excel" Width="111px" />
                </td>

            </tr>
            </table>
    
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AutoGenerateColumns="False" AllowSorting="True" 
            CellPadding="2" ForeColor="#333333" Width="970px" PageSize="15">
            <RowStyle BackColor="#EFF3FB" Font-Names="Arial" Font-Size="Small" />
            <Columns>
                <asp:BoundField DataField="carnet" HeaderText="Carnet" 
                    SortExpression="carnet" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="nombres" HeaderText="Nombres" 
                    SortExpression="nombres" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="apellidos" HeaderText="Apellidos" 
                    SortExpression="apellidos" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="sexo" HeaderText="Sexo" SortExpression="sexo" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="convenio" HeaderText="Convenio" 
                    SortExpression="convenio" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="universidadOrigen" HeaderText="Universidad de Origen" 
                    SortExpression="universidadOrigen" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="paisUniversidadOrigen" 
                    HeaderText="Pais de Universidad de Origen" 
                    SortExpression="paisUniversidadOrigen" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="escuelausat" HeaderText="Escuela USAT" 
                    SortExpression="escuelausat" />
                <asp:BoundField DataField="año" HeaderText="Año" 
                    SortExpression="año" ReadOnly="True" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="area" HeaderText="Area de Estudios" 
                    SortExpression="area" >
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="#000066" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" 
                Font-Size="Small" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        &nbsp;<span class="style1">&nbsp;</span>&nbsp;&nbsp;&nbsp;
        <br />
        <span class="style1">&nbsp;&nbsp; </span>&nbsp;&nbsp;&nbsp;
        <br />
        <span class="style1">&nbsp;</span>&nbsp;&nbsp;&nbsp; 
            
        <br />
        <span class="style1">&nbsp;&nbsp; </span>&nbsp;&nbsp;&nbsp; 
            
        <br />
        <br />
        
        <br />
&nbsp;<br />
    
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        
        
        
        
        SelectCommand="SELECT carnet, nombres, apellidos, sexo, convenio, universidadOrigen, paisUniversidadOrigen, escuelausat, year(fechaInicial) as [año], faculty as area  FROM dbo.EstudianteExtranjero 
where year(fechainicial) = @p1 and faculty like '%'+@p2+'%' and sexo like '%'+@p3+'%' and universidadOrigen like '%'+@p4+'%' and paisUniversidadOrigen like '%'+@p5+'%'">
        <SelectParameters>
            <asp:ControlParameter ControlID="TextBox1" DefaultValue="2010" Name="p1" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBox2" DefaultValue="%" Name="p2" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBox3" DefaultValue="" Name="p3" 
                PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBox4" Name="p4" PropertyName="Text" />
            <asp:ControlParameter ControlID="TextBox5" Name="p5" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
