<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRestriccionAmbiente.aspx.vb" Inherits="academico_horarios_administrar_frmRestriccionAmbiente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     body
        { font-family:Trebuchet MS;
          font-size:11.5px;
          cursor:hand;
          background-color:white;	 
        }
        .style1
        {
            width: 100%;
        }
        .style2
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7" style="font-size:14px"><b>Registro de Restricciones de Ambientes
                
                </b></td>            
        </tr>
    </table>
    <br />
    
    <table class="style1">
        <tr>
            <td>
                Tipo Ambiente</td>
            <td>
        <asp:DropDownList ID="ddlTipo" runat="server" AutoPostBack="True">
        </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="style2">
                Ubicación</td>
            <td class="style2">
        <asp:DropDownList ID="ddlUbicacion" runat="server" AutoPostBack="True">
        </asp:DropDownList>
                </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
            <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
                    DataKeyNames="codigo_ramb,codigo_amb">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="codigo_amb" HeaderText="codigo_amb" 
                Visible="False" />
            <asp:BoundField DataField="nombre" HeaderText="AMBIENTE" ReadOnly="True" />
            <asp:BoundField DataField="descripcion_ube" HeaderText="UBICACIÓN" 
                ReadOnly="True" />
            <asp:BoundField DataField="codigo_ramb" HeaderText="codigo_ramb" 
                Visible="False" />
           
            <asp:TemplateField HeaderText="LU" SortExpression="D1">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD1" runat="server" SelectedValue='<%# Bind("D1")%>' >
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="1"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="hi1" HeaderText="HI" />
            <asp:BoundField DataField="hf1" HeaderText="HF" />
            
            
              <asp:TemplateField HeaderText="MA" SortExpression="D2">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD2" runat="server" SelectedValue='<%# Bind("D2")%>'>
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="2"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            
            <asp:BoundField DataField="hi2" HeaderText="HI" />
            <asp:BoundField DataField="hf2" HeaderText="HF" />
            
            
              <asp:TemplateField HeaderText="MI" SortExpression="D3" >
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD3" runat="server" SelectedValue='<%# Bind("D3")%>'>
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="3"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="hi3" HeaderText="HI" />
            <asp:BoundField DataField="hf3" HeaderText="HF" />
            
            
              <asp:TemplateField HeaderText="JU" SortExpression="D4">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD4" runat="server" SelectedValue='<%# Bind("D4")%>'>
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="4"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
                        
            <asp:BoundField DataField="hi4" HeaderText="HI" />
            <asp:BoundField DataField="hf4" HeaderText="HF" />
            
              <asp:TemplateField HeaderText="VI" SortExpression="D5">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD5" runat="server" SelectedValue='<%# Bind("D5")%>'>
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="5"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="hi5" HeaderText="HI" />
            <asp:BoundField DataField="hf5" HeaderText="HF" />
            
            
              <asp:TemplateField HeaderText="SA" SortExpression="D6">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD6" runat="server" SelectedValue='<%# Bind("D6")%>'>
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="6"></asp:ListItem>
                        
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:BoundField DataField="hi6" HeaderText="HI" />
            <asp:BoundField DataField="hf6" HeaderText="HF" />
            <asp:CommandField ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
                </td>
        </tr>
    </table>
    <div>
        <br />
        <br />
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="hRestriccionAmbiente_Consultar" SelectCommandType="StoredProcedure" 
                        UpdateCommand="hRestriccionAmbiente_Registrar" UpdateCommandType="StoredProcedure" 
                        
                        >
                        
                        <SelectParameters>
                        <asp:ControlParameter ControlID="ddlTipo" Name="codigo_tam" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter ControlID="ddlUbicacion" Name="codigo_ube" PropertyName="SelectedValue" Type="Int32" />                            
                        </SelectParameters>
                          <UpdateParameters>
                          <asp:ControlParameter ControlID="GridView1" Name="codigo_ramb" 
                                PropertyName="SelectedDataKey.Values[codigo_ramb]" Type="Int32" />   
                          <asp:ControlParameter ControlID="GridView1" Name="codigo_amb" 
                                PropertyName="SelectedDataKey.Values[codigo_amb]" Type="Int32" />   
                        
                        <asp:Parameter Name="D1" Type="Int32"  />
                        <asp:Parameter Name="hi1" Type="Int32"  />
                        <asp:Parameter Name="hf1" Type="Int32"  />
                                                
                        <asp:Parameter Name="D2" Type="Int32" />
                        <asp:Parameter Name="hi2" Type="Int32"  />
                        <asp:Parameter Name="hf2" Type="Int32"  />
                        
                        <asp:Parameter Name="D3" Type="Int32" />
                        <asp:Parameter Name="hi3" Type="Int32"  />
                        <asp:Parameter Name="hf3" Type="Int32"  />
                        
                        <asp:Parameter Name="D4" Type="Int32" />
                        <asp:Parameter Name="hi4" Type="Int32"  />
                        <asp:Parameter Name="hf4" Type="Int32"  />
                        
                        <asp:Parameter Name="D5" Type="Int32" />
                        <asp:Parameter Name="hi5" Type="Int32"  />
                        <asp:Parameter Name="hf5" Type="Int32"  />
                        
                        <asp:Parameter Name="D6" Type="Int32" />
                        <asp:Parameter Name="hi6" Type="Int32"  />
                        <asp:Parameter Name="hf6" Type="Int32"  />
                        
                        </UpdateParameters>
                        
                       

                       
                       
                    </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>
