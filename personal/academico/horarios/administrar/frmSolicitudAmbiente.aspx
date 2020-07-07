<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudAmbiente.aspx.vb" Inherits="academico_horarios_frmSolicitudAmbiente" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          background-color:#F0F0F0;	
        }
         .celda1
        {           
            background:white;
            padding:5px;
            border:1px solid #808080;           
            color:#2F4F4F;
            font-weight:bold;            
        }
       .titulo
       { 
           font-weight:bold; font-size: 13px; padding-bottom:10px;
       }
       .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:5px;
                height: 29px;
            }
       .sinTop { border-top:0px; }
       .sinleft{ border-left:0px; }
       .sinRight{ border-right:0px; }
       .sinBottom{ border-bottom:0px; }
       .celdaCa{ padding:10px;}
        .CeldaDetalle{background-color:#5D7B9D; color:White; border:1px solid silver;text-align:center;}
        ul {margin:0; padding:0}
        
    </style>
</head>
<body>
    <h3>Administrar Solicitudes de Ambientes</h3>
    <form id="form1" runat="server">
       <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <table>
    <tr>
        <td>&nbsp;Centro de Costos</td>
        <td> 
            <asp:DropDownList ID="ddlCco" runat="server">
            </asp:DropDownList>
        </td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
     <tr>
        <td>Fechas</td>
        <td> 
        <asp:DropDownList ID="ddlFecha" runat="server">
          <asp:ListItem Selected="True" Value="0">TODOS</asp:ListItem>
          <asp:ListItem Value="1">Hoy y Mañana</asp:ListItem>
          <asp:ListItem Value="3">Próximos 3 días</asp:ListItem>
          <asp:ListItem Value="7">Próximos 7 días</asp:ListItem>
          <asp:ListItem Value="14">Próximos 14 días</asp:ListItem>
          <asp:ListItem Value="21">Próximos 21 días</asp:ListItem>
          <asp:ListItem Value="31">Próximos 30 días</asp:ListItem>
          <asp:ListItem Value="-1">Anteriores a Hoy</asp:ListItem>
          </asp:DropDownList>
          
      </td>
        <td>Estado</td>
        <td>
            <asp:DropDownList ID="ddlEstado" runat="server">
        <asp:ListItem  Value="%">TODOS</asp:ListItem>
            
        <asp:ListItem Value="P" Selected="True">Pendiente</asp:ListItem>
        </asp:DropDownList>
        </td>
    </tr>
    <tr><td><asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn" /></td></tr>
    </table>
    <table>
    <tr>
        <td class="celda1">        
            <asp:GridView ID="gvSolicitud" runat="server" AutoGenerateColumns="False" 
                CellPadding="2" ForeColor="#333333" DataKeyNames="codigo_Lho,codigo_amb">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="codigo_Lho" HeaderText="codigo_Lho" 
                        Visible="false" />
                       
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                        DeleteText="Asignar" 
                        DeleteImageUrl="~/academico/horarios/administrar/images/check.png" />
                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
                    <asp:BoundField DataField="Cco" HeaderText="Cco" />
                    <asp:BoundField DataField="nombre_ambts" HeaderText="Tipo" />
                    
                 <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />               
                    <asp:BoundField DataField="capacidad_Amb" HeaderText="Cap." />
                    <asp:BoundField DataField="dia_Lho" HeaderText="Día" />
                    <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha Inicio" />
                    <asp:BoundField DataField="fechaFIn_lho" HeaderText="Fecha Fin" />
                    <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
                    <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
                                        <asp:TemplateField HeaderText="Ambiente">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAmbiente" runat="server" 
                              >
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Liberar Ambiente" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnQuitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="LimpiarAmbiente" ImageUrl="~/administrativo/pec/image/eA.png" 
                             ToolTip="Liberar Ambiente" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>    
         
                     <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado" />
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
    </form>
</body>
</html>
