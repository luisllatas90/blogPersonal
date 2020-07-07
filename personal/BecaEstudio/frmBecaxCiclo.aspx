<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaxCiclo.aspx.vb" Inherits="BecaEstudio_frmBecaxCiclo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilos.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
<form id="form1" runat="server">
    <table>
        <tr>
            <td colspan="2"><h1 class="title-cont">Configuración de Requisitos</h1></td>
        </tr>
        <tr>
            <td>Ciclo</td>                
            <td><span class="combobox small"><asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True"></asp:DropDownList></span></td>
        </tr>
        <tr>
            <td>&nbsp;</td>                
            <td>&nbsp;</td>
        </tr>
        <tr><td></td></tr>
        <tr>
        <td colspan="2">
        <asp:GridView ID="gvBecaxCiclo" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_bxc" 
                        DataSourceID="SqlDataSource1" Width="100%" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="codigo_bxc" HeaderText="codigo_bxc" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_bxc" 
                                Visible="False" />
                               
                            <asp:BoundField DataField="descripcion_Cac" HeaderText="Ciclo Acad." 
                                SortExpression="Ciclo Acad." ReadOnly="True" />
                                                                                                                          
                            <asp:BoundField DataField="descripcion_req" HeaderText="Requisito" 
                                SortExpression="requisito" ReadOnly="True" />                                
                                
                            <asp:BoundField DataField="valor_bxc" HeaderText="Valor" 
                                SortExpression="Valor" DataFormatString="{0:N0}" />
                            
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                        
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="Beca_ConsultarBecaxCiclo" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlCiclo" Name="codigo_cac" 
                                PropertyName="SelectedValue" Type="Int32" />                            
                        </SelectParameters>
                    </asp:SqlDataSource>
        </td>
        </tr>
    </table>    
   </form>
</body>
</html>
