<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRestriccionCursoProgramado.aspx.vb" Inherits="academico_horarios_administrar_frmRestriccionAmbiente" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
     body
        { font-family:Trebuchet MS;
          font-size:12px;
          cursor:hand;
          background-color:white;	 
        }
         .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
      
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
     
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7" style="font-size:14px"><b>Registro de Restricciones de Horario
                
                del Curso Programado</b></td>            
        </tr>
    </table>
    <br />
    
    <table class="style1">
        <tr>
            <td>
                Semestre Académico</td>
            <td>
        <asp:DropDownList ID="ddlCiclo" runat="server">
        </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Ciclo del Curso</td>
            <td>
        <asp:DropDownList ID="ddlCicloCur" runat="server">
            <asp:ListItem Value="0">TODOS</asp:ListItem>
            <asp:ListItem>1</asp:ListItem>
            <asp:ListItem>2</asp:ListItem>
            <asp:ListItem>3</asp:ListItem>
            <asp:ListItem>4</asp:ListItem>
            <asp:ListItem>5</asp:ListItem>
            <asp:ListItem>6</asp:ListItem>
            <asp:ListItem>7</asp:ListItem>
            <asp:ListItem>8</asp:ListItem>
            <asp:ListItem>9</asp:ListItem>
            <asp:ListItem>10</asp:ListItem>
            <asp:ListItem>11</asp:ListItem>
            <asp:ListItem>12</asp:ListItem>
            <asp:ListItem>13</asp:ListItem>
            <asp:ListItem>14</asp:ListItem>
            <asp:ListItem>15</asp:ListItem>
        </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Buscar por nombre</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="btn" Text="Buscar" />
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="2">
            <br />    </td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            DataSourceID="SqlDataSource1" CellPadding="4" ForeColor="#333333" 
                    DataKeyNames="codigo_rcup,codigo_cup">
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
            <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" Visible="False" />
            <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela Profesional" />
            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
            <asp:BoundField DataField="nombre_cur" HeaderText="Curso Programado" ReadOnly="True" />
            <asp:BoundField DataField="codigo_rcup" HeaderText="codigo_rcup" Visible="False" />
           
            <asp:TemplateField HeaderText="LU" SortExpression="D1">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlD1" runat="server" SelectedValue='<%# Bind("D1")%>' >
                        <asp:ListItem Text="Seleccionar" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Puede" Value="1"></asp:ListItem>
                       
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
           
            <asp:BoundField DataField="hi1" HeaderText="HI" >
            
            </asp:BoundField>
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
            
    <div>
        <br />
        <br />
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="hRestriccionCursoProgramado_Consultar" SelectCommandType="StoredProcedure" 
                        UpdateCommand="hRestriccionCursoProgramado_Registrar" UpdateCommandType="StoredProcedure"  
                        
                        >
                        
                        <SelectParameters >                        
                        <asp:ControlParameter ControlID="ddlCiclo" Name="codigo_cac" PropertyName="SelectedValue" Type="Int32" />                        
                        <asp:ControlParameter ControlID="ddlCicloCur" Name="ciclo_cur" PropertyName="SelectedValue" Type="Int32" />                        
                        <asp:QueryStringParameter Name="codigo_per" QueryStringField="id" Type="Int32" />                                               
                        <asp:ControlParameter  ControlID="TextBox1" Name="nombre" PropertyName="Text" Type="String" ConvertEmptyStringToNull="true" DefaultValue=" " />                        
                        </SelectParameters>
                        
                        <UpdateParameters>
                          
                        <asp:ControlParameter ControlID="GridView1" Name="codigo_rcup" PropertyName="SelectedDataKey.Values[codigo_rcup]" Type="Int32" />   
                        <asp:ControlParameter ControlID="GridView1" Name="codigo_cup"  PropertyName="SelectedDataKey.Values[codigo_cup]" Type="Int32" />                             
                          
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
