<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaCalendarioActividades.aspx.vb" Inherits="BecaEstudio_frmBecaCalendarioActividades" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table>
            <tr>
                <td colspan="2"><h1 class="title-cont">Actualizar Cronograma de becas por Ciclo Académico</h1></td>
            </tr>
            <tr>
                <td>Ciclo Académico: </td>
                 <td><span class="combobox large"><asp:DropDownList ID="cboCicloAcademico" runat="server" AutoPostBack="True">
                    </asp:DropDownList></span>
                </td>
            </tr>
           <tr><td><br /></td></tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvCronograma" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_Cro" 
                        DataSourceID="SqlDataSource1" Width="100%" CellPadding="4" 
                        ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="codigo_Cro" HeaderText="codigo_Cro" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_Cro" 
                                Visible="False" />
                            <asp:BoundField DataField="Ciclo Acad." HeaderText="Ciclo Acad." 
                                SortExpression="Ciclo Acad." ReadOnly="True" />
                            <asp:BoundField DataField="Actividad" HeaderText="Actividad" 
                                SortExpression="Actividad" ReadOnly="True" />
                            <asp:BoundField DataField="Fecha_Inicio" HeaderText="Fecha Inicio" 
                                SortExpression="Fecha_Inicio" DataFormatString="{0:dd/MM/yyyy hh:mi}" />
                            <asp:BoundField DataField="Fecha_Fin" HeaderText="Fecha Fin" 
                                SortExpression="Fecha_Fin" DataFormatString="{0:dd/MM/yyyy}"/>
                            <asp:BoundField DataField="Observación" HeaderText="Observación" 
                                SortExpression="Observación" />
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="BECA_ConsultarCronograma" SelectCommandType="StoredProcedure"><%--revisar--%>
                        <SelectParameters>
                            <asp:ControlParameter ControlID="cboCicloAcademico" Name="codigo_cac" 
                                PropertyName="SelectedValue" Type="Int32" />
                            
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
          
        </table>
    
    </div>
    
    </form>
</body>
</html>
