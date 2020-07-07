<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudConfirmar.aspx.vb" Inherits="academico_horarios_administrar_frmSolicitudConfirmar" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
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
    TBODY {
	display: table-row-group;
}
	
        .style1
        {
            font-size: small;
        }
	
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <div>
      <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>Confirmar Solicitud de Ambientes&nbsp;               
                </b></td>
        </tr>
        
        
        </table>
        <br /><font style="color:Navy;">
    Solicitudes de ambientes dentro de los próximos <b>15 </b>días:<br /></font>
    
&nbsp;<asp:GridView ID="gridHorario" runat="server" AutoGenerateColumns="False"
                                                CaptionAlign="Top" DataKeyNames="codigo_lho,descripcion_lho,dia_lho,fechaIni_lho,ambiente"
                                                    BorderStyle="None" CellPadding="4" 
             AlternatingRowStyle-BackColor="#F7F6F4" >
             <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                 <asp:BoundField DataField="codigo_lho" HeaderText="codigo_lho" Visible="false">
                 <ItemStyle Font-Underline="false" ForeColor="#0066CC" />
                 </asp:BoundField>
                 <asp:BoundField DataField="dia_Lho" HeaderText="Día" >
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                 <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                  <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />
                 <asp:BoundField DataField="nombre_hor" HeaderText="Inicio" />
                 <asp:BoundField DataField="horaFin_Lho" HeaderText="Fin" >
                 <ItemStyle Font-Underline="false" />
                 </asp:BoundField>
                 <asp:BoundField DataField="codigo_amb" HeaderText="codigo_amb" Visible="false" >
                 <ItemStyle Font-Underline="false"  />
                 </asp:BoundField>  
                   <asp:BoundField DataField="preferencial_amb" HeaderText="Tipo"  
                     ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>                              
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" 
                     ItemStyle-HorizontalAlign="left">
                  <ItemStyle Font-Underline="false" />
                   </asp:BoundField>
                 
                 <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado" >
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Confirmar Solicitud"  
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnConfirmarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="ConfirmarAmbiente" ImageUrl="~/administrativo/pec/image/check.png" 
                             ToolTip="Confirmar Solicitud" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:TemplateField HeaderText="Cancelar Solicitud"
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnQuitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="LimpiarAmbiente" ImageUrl="~/administrativo/pec/image/eliminar.gif" 
                             ToolTip="Cancelar Solicitud" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 <asp:BoundField DataField="estado2_lho" HeaderText="Actividad" >
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>
                 <asp:BoundField DataField="CURSO" HeaderText="Curso" />
                 <asp:BoundField DataField="CCO" HeaderText="CCO" />
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                    No tiene solicitudes pendientes de confirmar
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>
        <br />
    
    </div>
    <center>
    <asp:Panel ID="pnlPregunta" runat="server" BorderColor="#5D7B9D" 
        BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
        Visible="False" Width="25%" BackColor="#F7F6F4">
        <b><span class="style1">
              ¿Desea</span></b>
        <asp:Label ID="lblAcción" runat="server" 
            style="font-weight: 700; color: #FF0000; font-size: small;" Text="Label"></asp:Label>
        &nbsp;<b><span class="style1">la actividad seleccionada ?</span></b><br />
        <asp:Label ID="lblFecha" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblActividad" runat="server" 
            style="color: #000000; font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblAmbiente" runat="server" 
            style="color: #666633; font-weight: 700; " Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSi" runat="server" CssClass="btn" Text="Sí" Width="50px"/>
        &nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" Width="50px" /><br />
        <asp:HiddenField ID="codigo_lho" runat="server" />
      
    </asp:Panel>
    </center>
    </form>
</body>
</html>
