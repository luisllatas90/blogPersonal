<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmBecaxEscuela.aspx.vb" Inherits="BecaEstudio_frmBecaxCiclo" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estilos.css?xxx=2" rel="stylesheet" type="text/css" />
    
    </head>
<body>

<form id="form1" runat="server">
    <table border="0">      
        <tr>
            <td colspan="3" bgcolor="#EFF3FB" height="35px" >
            <asp:Label ID="Label1" runat="server" Text="Asignación de Becas de Estudio por Ciclo Académico" style="font-weight: 700"></asp:Label>
            </td>                            
        </tr>        
        <tr>
           <td><b>Seleccionar Ciclo Académico:</b></td>
           <td><asp:DropDownList ID="ddlCiclo" runat="server"></asp:DropDownList>&nbsp;&nbsp; </td>         
           <td>       
           <asp:Button ID="btnBuscar" runat="server" Text="BUSCAR" CssClass="btnBuscar" 
               Width="" Height="30px" />                         
           </td>
        </tr>
         <tr>
        <td colspan="2">
         
       </td>
       <td style="text-align:right;">
       
           <asp:Button ID="btnCalcular" runat="server" CssClass="btnPublicar" Height="30px" 
               Text="   Calcular" Width="" />
             </td>
       
        </tr>       
        
        <tr>
            <td colspan="3"><asp:GridView ID="gvBecasAOtorgar" runat="server" BackColor="White" 
                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                    ForeColor="Black" GridLines="Vertical" Width="100%">
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
            
        </tr>
        <tr><td></br></bvr></td>
        </tr>
        <tr>
        <td colspan="2">
            <b>Carreras Profesionales</b>
       </td>
       <td style="text-align:right;">
       
           <asp:Button ID="Button1" runat="server" Text="Asignar" CssClass="btnCalcular" 
                    Width="89px" Height="30px" />
       
       </td>
       
        </tr>
       <tr>
       <td colspan="3">
      
           <asp:GridView ID="gvBecaxEscuela" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_cpf" Width="100%" CellPadding="4" 
                ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                        
                            <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf" 
                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_cpf" 
                                Visible="False"/>
                                                                
                            <asp:BoundField DataField="descripcion_Cac" HeaderText="CICLO"
                                SortExpression="Ciclo Acad." ReadOnly="True" >
                                
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                                
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERAS" 
                                SortExpression="Escuela Profesional" ReadOnly="True" >
                                
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Small" />
                            </asp:BoundField>
                                
                          <asp:BoundField DataField="TotalMatriculados" HeaderText="MATRICULADOS" 
                                SortExpression="Escuela Profesional">                                                                                                  
                                
                            <HeaderStyle Font-Size="X-Small" />
                            <ItemStyle Font-Size="Small" HorizontalAlign="Center" />
                            </asp:BoundField>
                                
                          <asp:BoundField DataField="TotalIngresantes" HeaderText="INGRESANTES" 
                                SortExpression="Escuela Profesional" ReadOnly="True" 
                                ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Right" >
                                
                            <HeaderStyle Font-Size="X-Small" />
                                
<ItemStyle HorizontalAlign="Center" Width="20px" Font-Size="Small"></ItemStyle>
                            </asp:BoundField>
                                
                          <asp:TemplateField>
                                <HeaderTemplate>
                                      <label>MATRICULADOS - INGRESANTES</label>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDiferencia" runat="server" Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="X-Small" />
                               <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Middle" 
                                    Font-Size="Small"/>
                            </asp:TemplateField>
                            
                               <asp:TemplateField>
                                <HeaderTemplate>
                                      <label>BECAS A ASIGNAR</label>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtBecasAsignar" runat="server" Width="30px"></asp:Label>
                                </ItemTemplate>
                                   <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Middle" 
                                       BackColor="#FFFFCC" Font-Bold="True" Font-Size="Small"/>
                            </asp:TemplateField>  
                            
                             <asp:BoundField DataField="Becas" HeaderText="SOLICITUD BECAS" 
                                SortExpression="Solicitudes" ReadOnly="True" ItemStyle-Width="20px" 
                                ItemStyle-HorizontalAlign="Right" >
                         
                            <HeaderStyle Font-Size="X-Small" />
                         
<ItemStyle HorizontalAlign="Center" Width="20px" BackColor="#99CCFF" Font-Bold="True" Font-Size="Small"></ItemStyle>
                            </asp:BoundField>
                         
                           <asp:TemplateField>
                                <HeaderTemplate>
                                      <label>BECA COMPLETA</label>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBecaCompleta" runat="server" Width="25px"></asp:TextBox>
                                </ItemTemplate>
                                   <HeaderStyle Font-Size="X-Small" />
                                   <ItemStyle Width="25px" HorizontalAlign="Center" VerticalAlign="Middle" 
                                    Font-Size="Small"/>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                      <label>BECA PARCIAL</label>
                                 </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBecaParcial" runat="server" Width="25px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle Font-Size="X-Small" />
                                <ItemStyle Width="25px" HorizontalAlign="Center" Font-Size="Small"/>
                            </asp:TemplateField>
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
                        SelectCommand="Beca_ConsultarBecaxEscuela2" SelectCommandType="StoredProcedure">
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
