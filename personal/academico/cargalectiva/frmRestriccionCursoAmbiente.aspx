<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRestriccionCursoAmbiente.aspx.vb" Inherits="academico_cargalectiva_frmRestriccionCursoAmbiente" %>

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
      
            .style1
            {
                height: 26px;
            }
                  
            .style2
            {
                height: 22px;
            }
                  
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7" style="font-size:14px"><b>Asignación de Ambientes por Curso Programado</b></td>            
        </tr>
    </table>
    <br />
    
     <table class="style1">
          
        <tr>
            <td>
                Semestre Académico</td>
            <td>
        <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True">
        </asp:DropDownList>
            </td>
        </tr>
  
        <tr>
            <td class="style1">
                Carrera Profesional</td>
            <td class="style1">
    <asp:DropDownList ID="ddlCarreraProfesional" runat="server" AutoPostBack="True">
    </asp:DropDownList>
            </td>
        </tr>
  
        <tr>
            <td class="style1">
                Ciclo del Curso</td>
            <td class="style1">
        <asp:DropDownList ID="ddlCicloCur" runat="server" AutoPostBack="True">
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
            <td class="style1" border="1" colspan="3">
                &nbsp;</td>
        </tr>
               <tr>
            <td>
                Ambientes Disponibles</td>
            <td>
    <asp:DropDownList ID="ddlAmbientes" runat="server" AutoPostBack="True">
    </asp:DropDownList>
            </td>
                   <td>
                <asp:Button ID="Button1" runat="server" Text="Asignar Ambiente" CssClass="btn" />
                   </td>
        </tr>
               <tr>
            <td class="style2" colspan="2">
                   <div id="msg" runat="server" style="padding:5px; font-size:10px;"  ></div>
                   </td>
        </tr>
               <tr>
            <td colspan="3">
              </td>
        </tr></table>
             
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" DataKeyNames="codigo,codigo_cup, curso">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegir" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo" HeaderText="codigo" Visible="False" />
                        <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="curso" HeaderText="Curso Programado" />
                        <asp:BoundField DataField="aula" HeaderText="Ambiente Asignado" />
                        
                         <asp:TemplateField HeaderText="Borrar Ambiente"
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnQuitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="LimpiarAmbiente" ImageUrl="~/administrativo/pec/image/eA.png" 
                             ToolTip="Borrar Ambiente" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 
                        <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                            Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                  
    
    </form>
</body>
</html>

