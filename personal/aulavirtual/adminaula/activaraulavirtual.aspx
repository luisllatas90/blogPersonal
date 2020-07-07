<%@ Page Language="VB" AutoEventWireup="false" CodeFile="activaraulavirtual.aspx.vb" Inherits="academico_notas_profesor_activaraulavirtual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        
    <style type="text/css">
        body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
         /* background-color:#F0F0F0;	*/
          padding:25px;
        }
        
        .celda1
        {
            width: 20%;
            background:white;
            padding:5px;
            border:1px solid #808080;
            border-right:0px;
            color:#2F4F4F;
            font-weight:bold;
            
        }
        .celda2
        {
            width: 80%;
            background:white;
            padding:5px;
            border:1px solid #808080;
            border-left:0px;
            color:#2F4F4F;
            font-weight:bold;
        }
       .celda3
       {    width: 80%;
            background:white;
            padding:5px;
            border:1px solid #808080;                  
            color:#2F4F4F;
            font-weight:bold;
       }
       
       #celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
      .celdaGrid
       {
          color:#5D7B9D;   padding:5px;font-weight:bold; font-style:italic;
       }
       .titulo
       { 
           font-weight:bold; font-size: 10px; 
       }
       .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
            text-align: center;
        }
                
        .sinborderTop
        { border-top:0px;
            
            }
            
                .sinborderBottom
        { border-bottom:0px;
            
            }
    </style>
        
</head>
<body>

<form runat="server" name="frm">

    <h3>Activar Aula Virtual</h3>
   <table cellpadding="0" cellspacing="0" >
    
    <tr>
       
        <td><b>Semestre Académico</b></td>
        <td>
        <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True" 
                style="height: 22px"></asp:DropDownList>
        </td>
    </tr>
      <tr>
       
        <td><b>Seleccionar Docente</b></td>
        <td>
        <asp:DropDownList ID="ddlDocentes" runat="server" AutoPostBack="True"></asp:DropDownList>
            <br />
        </td>
        
      
        
    </tr>
    
      <tr>
       
        <td>
                    <asp:Button ID="btnActivarAula0" runat="server" 
                        Text="Actualizar Lista" CssClass="btn" 
                ToolTip="* Actualiza la lista de estudiantes en el aula virtual"  />
                </td>
        <td>
                <asp:Label ID="lblMensaje0" runat="server" ForeColor="Maroon" style="color: #CC0000"></asp:Label>
        </td>
        
      
        
    </tr>
    
    <tr><td><br /></td></tr>
    <tr>
    <td colspan="2" class="" >
    <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="codigo_cup" Width="100%" CellPadding="4" ForeColor="#333333">
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" ItemStyle-HorizontalAlign="left" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_cup" HeaderText="Código Curso" />
                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
                <asp:BoundField DataField="grupoHor_cup" HeaderText="GH" 
                    ItemStyle-HorizontalAlign="Center"   >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                <asp:BoundField DataField="matriculados" HeaderText="Mat." 
                    ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="mdl" HeaderText="mdl" Visible="true" />
            </Columns>
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            
        </asp:GridView>
        <p runat="server" id ="celdaGrid" visible="false"></p>
    </td>
    <td class="" >
        &nbsp;</td>
    <td class="" >
        &nbsp;</td>
    </tr>
    <tr><td><br /></td></tr>
     <tr>
        <td colspan="2" class ="celda1 celda3">Configuración adicional para los cursos seleccionados:<br />
        <table>
            <tr>
                <td>Formato:&nbsp; <asp:DropDownList ID="ddlFormato" runat="server">
                             <asp:ListItem Value="weeks">Semanas</asp:ListItem>
                             <asp:ListItem Value="topics">Temas</asp:ListItem>
                             </asp:DropDownList>
                </td>
                <td>
                &nbsp;&nbsp;&nbsp;&nbsp;
                Nro Secciones:&nbsp; <asp:DropDownList ID="ddlSeccion" runat="server"></asp:DropDownList>
                </td>
                <td>
                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnActivarAula" runat="server" Text="Activar" CssClass="btn"  />
                </td>
                <td>
                <asp:Label ID="lblMensaje" runat="server" ForeColor="Maroon"></asp:Label>
                </td>
            </tr>
        </table>            
        </td>
        
    </tr>
   </table>


   </form>
</body>
</html>
