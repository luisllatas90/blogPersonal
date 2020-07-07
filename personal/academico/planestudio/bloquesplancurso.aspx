<%@ Page Language="VB" AutoEventWireup="false" CodeFile="bloquesplancurso.aspx.vb" Inherits="academico_cargalectiva_bloquesplancurso" %>

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
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
           
      .Ocultar{ display:none; padding-left:30px;}
      
      .Mostrar{ display:block; padding-left:30px;}
        .style1
        {
            height: 20px;
        }
        .style2
        {
            width: 226px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7" style="font-size:14px"><b>Registro de Bloques de Horas por Curso y Plan de Estudios
                <asp:Label ID="lblPaso" runat="server"></asp:Label>
                </b></td>            
        </tr>
      </table>
    <br />
    <table class="style1">
        <tr>
            <td>
                Escuela Profesional</td>
            <td class="style2">
    <asp:DropDownList ID="ddlCarreraProfesional" runat="server" AutoPostBack="True">
    </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Plan Estudios
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlPlanEstudio" runat="server" 
        AutoPostBack="True">
    </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
    Ciclo Estudios 
            </td>
            <td class="style2">
                <asp:DropDownList ID="ddlCicloEstudios" runat="server" 
        AutoPostBack="True">
        <asp:ListItem Value="1">1°</asp:ListItem>
        <asp:ListItem Value="2">2°</asp:ListItem>
        <asp:ListItem Value="3">3°</asp:ListItem>
        <asp:ListItem Value="4">4°</asp:ListItem>
        <asp:ListItem Value="5">5°</asp:ListItem>
        <asp:ListItem Value="6">6°</asp:ListItem>
        <asp:ListItem Value="7">7°</asp:ListItem>
        <asp:ListItem Value="8">8°</asp:ListItem>
        <asp:ListItem Value="9">9°</asp:ListItem>
        <asp:ListItem Value="10">10°</asp:ListItem>
        <asp:ListItem Value="11">11°</asp:ListItem>
        <asp:ListItem Value="12">12°</asp:ListItem>
        <asp:ListItem Value="13">13°</asp:ListItem>
        <asp:ListItem Value="14">14°</asp:ListItem>
    </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Listado</td>
            <td class="style2">
                &nbsp;</td>
            <td id="tdHeader" class="Ocultar" runat="server" valign="top">
            <b>Registrar / Modificar Bloques:     <asp:Label ID="lblCurso" Font-Size="12px" runat="server" Text="Label"></asp:Label>
            &nbsp;(<asp:Label ID="lblCiclo" runat="server" Text="Label"></asp:Label>
            Ciclo)</b></td>
        </tr>
        <tr>
            <td colspan="2" rowspan="4">
                <asp:HiddenField ID="hdTotalHoras" runat="server" />
                <asp:HiddenField ID="hdHorasAsignadas" runat="server" />
                <asp:GridView ID="dgvData" runat="server" 
        AutoGenerateColumns="False" 
        DataKeyNames="codigo_Pes,codigo_Cur" CellPadding="4" 
        ForeColor="#333333" GridLines="Both" Width="100%" > 
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <Columns>
           
            <asp:BoundField DataField="codigo_Pes" HeaderText="codigo_Pes" 
                Visible="False" />
            <asp:BoundField DataField="codigo_Cur" HeaderText="codigo_Cur" 
                Visible="False" />
            <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nombre_Cur" HeaderText="Curso" />
            <asp:BoundField DataField="totalHoras_Cur" HeaderText="Total Horas" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="asignadas" HeaderText="Horas Asignadas" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="nbloques" HeaderText="N° Bloques" >
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:CommandField ShowSelectButton="True"  />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#999999" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
            </td>
            <td id="tdContent" class="Ocultar" runat="server" valign="top">
                N°Horas: <asp:TextBox ID="txtNroHoras" runat="server" MaxLength="2" 
                    Width="38px"></asp:TextBox>
    &nbsp;para el bloque N°
    <asp:Label ID="lblNroBloque" runat="server" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnRegistrar" runat="server" Text="Agregar" CssClass="btn" />
            </td>
        </tr>
       
        <tr>
            <td id="tdFooter" class="Ocultar" runat="server" valign="top">
    <asp:GridView ID="gvBloque" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_bplc" 
                        DataSourceID="SqlDataSource1" Width="60%" CellPadding="4" 
                        ForeColor="#333333">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                        
                            <asp:BoundField DataField="codigo_bplc" HeaderText="codigo_bplc" 
                                Visible="False" />
                            <asp:BoundField DataField="codigo_pes" HeaderText="codigo_pes" 
                                Visible="False" />
                            <asp:BoundField DataField="codigo_cur" HeaderText="codigo_cur" 
                                Visible="False" />
                            <asp:BoundField DataField="nombre_Cur" HeaderText="nombre_Cur" 
                                ReadOnly="True" Visible="False" />
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
            </td>
        </tr>
       
        <tr>
            <td style="padding-left:30px;">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#CC3300" Text=""></asp:Label>
           </td>
        </tr>
        </table>
    <br />
    
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="HConsultar_planCursoBloques" SelectCommandType="StoredProcedure" 
                        DeleteCommand="HEliminar_planCursoBloques"  DeleteCommandType="StoredProcedure"  
                        UpdateCommand="Hregistrar_planCursoBloques" UpdateCommandType="StoredProcedure"
                         >
                        <SelectParameters>
                            <asp:ControlParameter ControlID="dgvData" Name="codigo_Pes" 
                                PropertyName="SelectedDataKey.Values[codigo_Pes]" Type="Int32" />
                            <asp:ControlParameter ControlID="dgvData" Name="codigo_Cur" 
                                PropertyName="SelectedDataKey.Values[codigo_Cur]" Type="Int32" />
                        </SelectParameters>
                        <DeleteParameters>
                            <asp:ControlParameter ControlID="gvBloque" Name="codigo_bplc" 
                                PropertyName="SelectedDataKey.Values[codigo_bplc]" Type="Int32" />                           
                        </DeleteParameters>
                        <UpdateParameters>
                        <asp:Parameter Name="codigo_pes" Type="Int32" DefaultValue="0" />
                        <asp:Parameter Name="codigo_cur" Type="Int32" DefaultValue="0" />
                         <asp:ControlParameter ControlID="gvBloque" Name="codigo_bplc" PropertyName="SelectedDataKey.Values[codigo_bplc]" Type="Int32" />
                        
                        <asp:Parameter Name="numerohoras" Type="Int32"  />
                        
                        
                        </UpdateParameters>
                       
                    </asp:SqlDataSource>
    <br />
    <br />
    
                   
    </form>
</body>
</html>
