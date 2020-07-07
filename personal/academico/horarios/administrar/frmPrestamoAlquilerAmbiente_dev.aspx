<%@ Page    Language="VB" AutoEventWireup="false" CodeFile="frmPrestamoAlquilerAmbiente.aspx.vb" Inherits="academico_horarios_frmPrestamoAlquilerAmbiente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
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
    TBODY {
	display: table-row-group;
}
	
        #txtDesde, #txtHasta
        {
            background-color: #C9DDF5;
        }
          .style1
        {
            font-size: small;
        }
        .style2
        {
            color: #FF0000;
        }
  </style>
  
  <link rel="stylesheet" href="jquery/jquery-ui.css">   
  <script src="jquery/jquery-1.10.2.js" type="text/javascript"></script>   
  <script src="jquery/jquery-ui.js" type="text/javascript"></script>    
  <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
  
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 0
            });
            $("#txtHasta").datepicker({
                firstDay: 0
            });
        });

        function confirmardia() {
           // var txt;
            //var r = confirm("Press a button!");
            //if (r == true) {
              //  return true
            //} else {
              //  return false
            //}
            alert("a");
        }
       
       
    </script>    
</head>
<body>
    
    <form id="form1" runat="server">
      <table  cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:95%" >
        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
            <td colspan="7"><b>Préstamos de Ambientes&nbsp;
                <asp:Label ID="lblPaso" runat="server"></asp:Label>
                </b></td>            
        </tr>
        </table>

    
    <table>
    <tr><td><br />
      <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Horario" CssClass="btn" />
        <br />
        </td><td><br />
         <asp:Button ID="btnExportar" runat="server" Text="Exportar" CssClass="btn" />
        </td></tr>
    <tr><td>
        <asp:CheckBox ID="CheckBox1" runat="server" Text="Incluir Finalizados" 
            AutoPostBack="True" />
    &nbsp;<asp:CheckBox ID="CheckBox2" runat="server" Text="Incluir Asignados y Pendientes" 
            AutoPostBack="True" />  
    </td><td>
                        &nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblLimite" runat="server" Text="Label" style="color: #FF6600"></asp:Label>
    &nbsp;(*)Excepto Solicitud de Auditorios</td></tr>
    </table>
        <asp:Panel ID="pnlRegistrar" runat="server"
        GroupingText="Ingresar Horario" Width="55%">
        <table style="width: 95%;" border="0" cellpadding="2">
            <tr>
            <td>Fecha Inicio</td>
            <td>
                <asp:DropDownList ID="ddlInicioDia" runat="server" Visible="False">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInicioMes" runat="server" Visible="False">
                <asp:ListItem Value="01">Enero</asp:ListItem>
                <asp:ListItem Value="02">Febrero</asp:ListItem>
                <asp:ListItem Value="03">Marzo</asp:ListItem>
                <asp:ListItem Value="04">Abril</asp:ListItem>
                <asp:ListItem Value="05">Mayo</asp:ListItem>
                <asp:ListItem Value="06">Junio</asp:ListItem>
                <asp:ListItem Value="07">Julio</asp:ListItem>
                <asp:ListItem Value="08">Agosto</asp:ListItem>
                <asp:ListItem Value="09">Setiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInicioAño" runat="server" Visible="False">
                </asp:DropDownList>
                <input ID="txtDesde" runat="server" type="text"/></td>
                <td>
                    
                    <asp:CheckBox ID="chkAudi" runat="server" Text="Auditorios (*)" 
                        style="font-weight: 700" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            
            <tr>
            <td>Hora Inicio</td>
            <td>
                <asp:DropDownList ID="ddlInicioHora" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlInicioMinuto" runat="server">
                </asp:DropDownList>
                &nbsp;</td>
                <td>
                    Nro. Ambientes</td>
                <td>
                    <asp:DropDownList ID="ddlNro" runat="server">
                        <asp:ListItem Selected="True">1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:DropDownList>
            
                </td>
            </tr>
            <tr>
            <td>Hora Fin</td>
            <td>
                <asp:DropDownList ID="ddlFinHora" runat="server">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFinMinuto" runat="server">
                </asp:DropDownList>
                
                </td>
                <td>
                    Capacidad</td>
                <td>
                    <asp:DropDownList ID="ddlCap" runat="server">
              
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td>Fecha Fin</td>
            <td>
            <asp:CheckBox ID="chkVarias" runat="server" Text="Crear varias sesiones" 
             CssClass="fuente"  AutoPostBack=true/><br />
                <asp:DropDownList ID="ddlFinDia" runat="server" Visible="False">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFinMes" runat="server" Visible="False">
                <asp:ListItem Value="01">Enero</asp:ListItem>
                <asp:ListItem Value="02">Febrero</asp:ListItem>
                <asp:ListItem Value="03">Marzo</asp:ListItem>
                <asp:ListItem Value="04">Abril</asp:ListItem>
                <asp:ListItem Value="05">Mayo</asp:ListItem>
                <asp:ListItem Value="06">Junio</asp:ListItem>
                <asp:ListItem Value="07">Julio</asp:ListItem>
                <asp:ListItem Value="08">Agosto</asp:ListItem>
                <asp:ListItem Value="09">Setiembre</asp:ListItem>
                <asp:ListItem Value="10">Octubre</asp:ListItem>
                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlFinAño" runat="server" Visible="False">
                </asp:DropDownList>
                <input ID="txtHasta" runat="server" type="text" /></td>
                <td>
                    Día de Sesión<br />
                        <asp:DropDownList ID="ddlDiaSelPer" runat="server">
                        <asp:ListItem Value="LU">Lunes</asp:ListItem>
                        <asp:ListItem Value="MA">Martes</asp:ListItem>
                        <asp:ListItem Value="MI">Miércoles</asp:ListItem>
                        <asp:ListItem Value="JU">Jueves</asp:ListItem>
                        <asp:ListItem Value="VI">Viernes</asp:ListItem>
                        <asp:ListItem Value="SA">Sábado</asp:ListItem> 
                            <asp:ListItem Value="DO">Domingo</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
          
             <tr>
                 <td>
                     Motivo Solicitud</td>
                 <td>
                     <asp:DropDownList ID="ddlTipSolicitud" runat="server" AutoPostBack="True">
                     </asp:DropDownList>
                 </td>
                 <td>
                     Alquiler de ambiente</td>
                 <td>
                     <asp:CheckBox ID="chkAlquiler" runat="server" Text="Sí" />
                 </td>
            </tr>
            
            <tr><td>
                 <asp:Label ID="lblTipoEstudio" runat="server" Text="Tipo de Estudio" 
                     Visible="False"></asp:Label>
                 </td><td colspan="3">
                     <asp:DropDownList ID="ddlTipoEstudio" runat="server" AutoPostBack="True" 
                         Visible="False">
                     </asp:DropDownList>
                 </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCarreras" runat="server" Text="Carrera Profesional" 
                        Visible="False"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlCarreras" runat="server" Visible="False">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBachilleres" runat="server" Text="Bachiller(es)" 
                        Visible="False"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtBachilleres" runat="server" Visible="False" Width="413px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción del Evento"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescripcion" runat="server" Columns="40" MaxLength="500" 
                        Rows="4" TextMode="MultiLine"></asp:TextBox>
                    <asp:HiddenField ID="hdtfu" runat="server" Value="NULL" />
                    <br />
                </td>
            </tr>
            
            <tr>
            <td>
                Requerimiento<br />Audiovisual <asp:CheckBox ID="chkRequerimieto" runat="server"  AutoPostBack="true" Checked=false/>
            </td>
            <td colspan="2">    
            <asp:TextBox ID="txtRequerimiento" runat="server" TextMode="MultiLine" Width="100%" Visible=false></asp:TextBox><br />            
            <asp:Label ID="lblReqInfoEmail1" runat="server" ForeColor="Blue" Visible=false>Se enviará correo a:</asp:Label>
            <asp:Label ID="lblReqInfoEmail2" runat="server" ForeColor="Red" Visible=false>audiovisuales@usat.edu.pe</asp:Label>
            </td>            
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td colspan="3">
                    <asp:Label ID="lblMsj" runat="server" 
                        style="font-style: italic; color: #CC3300;"></asp:Label>
                </td>
            </tr>
            <tr><td>&nbsp;</td>
            <td colspan="3">
            <asp:Button ID="btnRegistrarPers" runat="server" Text="Registrar" CssClass="btn" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnCancelar" runat="server" CssClass="btn" Text="Cancelar" />
                <br />
            </td>
            </tr>
                 </table>
    </asp:Panel>
     <asp:Panel ID="pnlConsultar" runat="server"
      Width="100%" >
      
    
         <asp:GridView ID="gridHorario" runat="server" AutoGenerateColumns="False"
                                                CaptionAlign="Top" DataKeyNames="codigo_lho"
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
                   <asp:BoundField DataField="preferencial_amb" HeaderText="Tipo"  ItemStyle-HorizontalAlign="Center" ReadOnly="True">
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>                              
                 <asp:BoundField DataField="Ambiente" HeaderText="Ambiente" ItemStyle-HorizontalAlign="left">
                  <ItemStyle Font-Underline="false" />
                   </asp:BoundField>
                 
                 <asp:BoundField DataField="estadoHorario_lho" HeaderText="Estado" >
                   <ItemStyle Font-Underline="false" />
                  </asp:BoundField>
                
                 <asp:TemplateField HeaderText="Solicitar Ambiente"  
                     ItemStyle-HorizontalAlign="Center" ItemStyle-Width="45px">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnSolicitarAmbiente" runat="server" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="SolicitarAmbiente" ImageUrl="~/administrativo/pec/image/Asol.png" 
                             ToolTip="Solicitar Ambiente" />
                     </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:TemplateField>
                 
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
                 <asp:CommandField ButtonType="Image" 
                     DeleteImageUrl="~/administrativo/pec/image/eH.png" DeleteText="Borrar Horario" 
                     HeaderText="Borrar Horario" ItemStyle-HorizontalAlign="Center" 
                     ItemStyle-Width="45px" ShowDeleteButton="True">
                 <ItemStyle HorizontalAlign="Center" />
                 </asp:CommandField>
                 
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#3266DB; background-color:#E8EEF7; padding:5px; font-style:italic;">
                     No se ha registrado horarios.
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
         </asp:GridView>
         <br />
    </asp:Panel>
 
      <center>
    <asp:Panel ID="pnlPregunta" runat="server" BorderColor="#5D7B9D" 
        BorderStyle="Solid" BorderWidth="1px" style="text-align: center; padding:5px;" 
        Visible="False" Width="25%" BackColor="#F7F6F4">
        <b><span class="style1">
              ¿Desea registrar la actividad para el día <span class="style2">Domingo</span>?</span></b><br />
        <asp:Label ID="lblFecha" runat="server" 
            style="color: #3366CC;  font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="lblActividad" runat="server" 
            style="color: #000000; font-weight: 700;" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnSi" runat="server" CssClass="btn" Text="Sí" Width="50px" />
        &nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" CssClass="btn" Text="No" Width="50px" />
        <br />
      
    </asp:Panel>
    </center>

 

</form> 
</body>
</html>
