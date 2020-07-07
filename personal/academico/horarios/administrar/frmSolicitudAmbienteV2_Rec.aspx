<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSolicitudAmbienteV2_Rec.aspx.vb" Inherits="academico_horarios_frmSolicitudAmbienteV2" %>
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
    <h3>Administrar Solicitudes de Ambientes para Examenes de Recuperación</h3>
    <form id="form1" runat="server">
       <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <table>
    <tr>
        <td>&nbsp;Ciclo Académico</td>
        <td> 
        <asp:DropDownList ID="cboCiclo" runat="server" AutoPostBack="True">
                </asp:DropDownList>  
          
        </td>
        <td>Enviar</td>
        <td>
            <asp:CheckBox ID="chkCorreo" runat="server" Text="Correo Personalizado" />
        </td>
    </tr>
     <tr>
        <td>Escuela Profesional</td>
        <td> 
            <asp:DropDownList ID="ddlCco" runat="server">
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
     <tr>
        <td>Solicitante</td>
        <td> 
            <asp:DropDownList ID="ddlSolicitante" runat="server">
            </asp:DropDownList>
          
      </td>
        <td>Ambiente</td>
        <td>
            <asp:DropDownList ID="ddlAmbiente" runat="server">
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
                       <asp:TemplateField HeaderText="Aprobar" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnAprobarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="AprobarAmbiente" ImageUrl="~/academico/horarios/administrar/images/check.png" 
                             ToolTip="Aprobar Ambiente" />
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                     </asp:TemplateField> 
                     
                    
                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
                    <asp:BoundField DataField="Cco" HeaderText="Escuela Profesional" />
                    
                    
                 <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />               
                    <asp:BoundField DataField="capacidad_Amb" HeaderText="Cap." />
                    <asp:BoundField DataField="dia_Lho" HeaderText="Día" />
                    <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha Inicio" />
                    <asp:BoundField DataField="fechaFIn_lho" HeaderText="Fecha Fin" 
                        Visible="False" />
                    <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
                    <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
                    <asp:BoundField DataField="Ambiente" HeaderText="Ambiente Solicitado" />                                           
                     <asp:TemplateField HeaderText="Liberar" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnQuitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="LimpiarAmbiente" ImageUrl="~/administrativo/pec/image/eA.png" 
                             ToolTip="Liberar Ambiente" />
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                     </asp:TemplateField> 
                      <asp:TemplateField HeaderText="Actualizar" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">  
                     <ItemTemplate>
                         <asp:ImageButton ID="btnActualizaAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="ActualizaAmbiente" ImageUrl="images/edit.png" 
                             ToolTip="Actualizar Ambiente" />
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
    <table>
    <tr>
        <td class="celda1">        
               <asp:GridView ID="gridEditar" runat="server" AutoGenerateColumns="False" 
                CellPadding="2" ForeColor="#333333" DataKeyNames="codigo_Lho,codigo_amb">
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:BoundField DataField="codigo_Lho" HeaderText="codigo_Lho" 
                        Visible="false" />
                       
                    <asp:TemplateField HeaderText="Aprobar" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnAprobarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="AprobarAmbiente" ImageUrl="~/academico/horarios/administrar/images/check.png" 
                             ToolTip="Aprobar Ambiente" />
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                     </asp:TemplateField> 
                    <asp:BoundField DataField="Solicitante" HeaderText="Solicitante" />
                    <asp:BoundField DataField="Cco" HeaderText="Escuela Profesional" />
                    
                    
                 <asp:BoundField DataField="descripcion_lho" HeaderText="Descripción" />               
                    <asp:BoundField DataField="capacidad_Amb" HeaderText="Cap." />
                    <asp:BoundField DataField="dia_Lho" HeaderText="Día" />
                    <asp:BoundField DataField="fechaIni_lho" HeaderText="Fecha Inicio" />
                    <asp:BoundField DataField="fechaFIn_lho" HeaderText="Fecha Fin" 
                        Visible="False" />
                    <asp:BoundField DataField="HoraInicio" HeaderText="Hora Inicio" />
                    <asp:BoundField DataField="HoraFin" HeaderText="Hora Fin" />
                                        <asp:TemplateField HeaderText="Ambiente">
                        <ItemTemplate>
                            <asp:DropDownList ID="ddlAmbiente" runat="server" 
                              >
                            </asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
         
                     <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado" />
                      <asp:TemplateField HeaderText="" 
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnRegresar" runat="server" 
                              
                             ImageUrl="~/academico/horarios/administrar/images/back.png" 
                             ToolTip="Regresar" onclick="btnRegresar_Click" />
                     </ItemTemplate>

<ItemStyle HorizontalAlign="Center" Width="45px"></ItemStyle>
                     </asp:TemplateField> 
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
    <asp:Panel ID="PanelTexto" runat="server" style="text-align: center" 
           Visible="False">
        <b>Ingresar texto aquí:</b><br />
        <asp:TextBox ID="txtTexto" runat="server" Height="80px" TextMode="MultiLine" 
            Width="371px"></asp:TextBox>
        <br />
        <asp:Button ID="btnOk" runat="server" CssClass="btn" Text="Enviar" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk0" runat="server" CssClass="btn" Text="Cancelar" />
    </asp:Panel>
    </form>
</body>
</html>
