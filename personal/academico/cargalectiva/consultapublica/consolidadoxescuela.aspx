<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consolidadoxescuela.aspx.vb" Inherits="academico_cargalectiva_consultapublica_consolidadoxescuela" %>


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
        .tit
        {
            background-color: #E8EEF7; font-weight: bold;  padding: 10px 10px 10px 0px;
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
    <table >
        <tr>
            <td colspan="4">
                <h3>Reporte Consolidado de Cursos por Escuela</h3></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Ciclo Académico</td>
            <td>
                <asp:DropDownList ID="ddlCiclo" runat="server" >
                </asp:DropDownList>
            </td>
            <td>
                Plan de Estudios</td>
            <td>
                <asp:DropDownList ID="ddlPlan" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Escuela&nbsp; Profesional</td>
            <td>
                <asp:DropDownList ID="ddlEscuela" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                        <asp:Button ID="Button1" runat="server" Text="Consultar Cursos" class="btn"/>
            </td>
            <td>
                &nbsp;</td>
        </tr>
    </table><br /><br />
      <table><tr><td><div style="max-height:500px; overflow:scroll">
          <asp:GridView ID="gData" runat="server" AutoGenerateColumns="False" 
            CellPadding="4"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4"  DataKeyNames="codigo_cup">
            <RowStyle BorderColor="#C2CFF1" />
            <Columns>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="BtnMostrar" runat="server" Text="Mostrar" class="btn" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="MostrarDetalle"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" 
                    Visible="False" />
                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
                 <asp:BoundField DataField="nro" HeaderText="Nro" />
            </Columns>
           <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se encontraron registros.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
        </asp:GridView></div>
  </td>
  
  <td valign="top" style="padding-left:30px;">
    
        Detalle de Programación<br />
        <div style="max-height:500px; overflow:scroll">
        <asp:GridView ID="gDataDetalle" runat="server" AutoGenerateColumns="False" 
            CellPadding="4"  BorderStyle="None" 
             AlternatingRowStyle-BackColor="#F7F6F4"  >
            <RowStyle BorderColor="#C2CFF1" />
            <Columns>
                <asp:BoundField DataField="nombre_cpf" HeaderText="Escuela" />
                <asp:BoundField DataField="descripcion_pes" HeaderText="Plan Estudios" />
                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                <asp:BoundField DataField="nombre_cur" HeaderText="Curso" />
            </Columns>
           <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No registra agrupación de cursos.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
        </asp:GridView></div>
            
        </td></tr></table>
    
    </form>
</body>
</html>
