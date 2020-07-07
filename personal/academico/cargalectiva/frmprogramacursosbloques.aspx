<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmprogramacursosbloques.aspx.vb" Inherits="academico_cargalectiva_frmprogramacursosbloques" %>

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
    <table>
        <tr>
            <td id="tdHeader" class="Ocultar" runat="server" valign="top">
                <b>
                <asp:Label ID="lblCurso" Font-Size="12px" runat="server" Text="Label" 
                    style="background-color:#DFDBA4; padding:4px;" ></asp:Label>
                
                
                <br />
                <br />
                                Registrar / Modificar Bloques<br />
                
                
                <asp:HiddenField ID="hdTotalHoras" runat="server" />
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC3300" Text=""></asp:Label>
                <asp:HiddenField ID="hdHorasAsignadas" runat="server" />
                <asp:HiddenField ID="hdCodigoPes" runat="server" />
                <asp:HiddenField ID="hdCodigoCur" runat="server" />
                    </td>      
        </tr>
        <tr>
            <td id="tdContent"  runat="server" valign="top">
            N°Horas: <asp:TextBox ID="txtNroHoras" runat="server" MaxLength="2" 
            Width="38px"></asp:TextBox>
                &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Text="Agregar" CssClass="btn"  />
            </td>
        
        </tr>
          <tr>
            <td id="tdFooter"  runat="server" valign="top">
    <asp:GridView ID="gvBloque" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_bcup" 
                        Width="60%" CellPadding="4" 
                        ForeColor="#333333" DataSourceID="SqlDataSource1">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                        
                            <asp:BoundField DataField="codigo_bcup" HeaderText="codigo_bcup" 
                                Visible="False" />
                            <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                                Visible="False" />       
                                                                             
                            <asp:BoundField DataField="numerohoras" HeaderText="N° Horas" >
                        
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                        
                            <asp:CommandField ShowDeleteButton="True" />
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
                        SelectCommand="HConsultar_cursoProgramadoBloques" SelectCommandType="StoredProcedure" 
                        DeleteCommand="HEliminar_cursoProgramadoBloques"  DeleteCommandType="StoredProcedure"  
                        UpdateCommand="Hregistrar_cursoProgramadoBloques" UpdateCommandType="StoredProcedure"
                         >
                        <SelectParameters>
                                <asp:SessionParameter Name="codigo_cup" SessionField="XCodigoCup" Type="Int32" />                                
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:ControlParameter ControlID="gvBloque" Name="codigo_bcup" 
                                PropertyName="SelectedDataKey.Values[codigo_bcup]" Type="Int32" />                           
                        </DeleteParameters>
                        <UpdateParameters>
                        <asp:ControlParameter ControlID="gvBloque" Name="codigo_bcup" PropertyName="SelectedDataKey.Values[codigo_bcup]" Type="Int32" />
                        <asp:Parameter Name="numerohoras" Type="Int32"  />
                        </UpdateParameters>
                        </asp:SqlDataSource>
                   
            </td>
        </tr>
       
        <tr>
            <td style="padding-left:30px;">
                &nbsp;</td>
        </tr>
        </table>
    </form>
</body>
</html>
