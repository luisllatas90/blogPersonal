<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmasignarambientev2.aspx.vb" Inherits="academico_horarios_administrar_frmasignarambientesv2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.1/themes/base/jquery-ui.css" />
    <script src="jquery/jquery-1.9.1.js" type="text/javascript"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    <script src="jquery/jquery.ui.datepicker-es.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 1
            });
            $("#txtHasta").datepicker({
                firstDay: 1
            });
        });
        function ejecutar(a) {
            window.open("frmasignarambientev2Detalle.aspx?codigo_amb=" + a, "_blank", "toolbar=no, scrollbars=yes, resizable=no, top=350, left=500, width=750, height=400,menubar=no");
        }
    </script>    
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
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:5px; 
       }
       .sinTop { border-top:0px; }
       .sinleft{ border-left:0px; }
       .sinRight{ border-right:0px; }
       .sinBottom{ border-bottom:0px; }

        

        .style2
        {
        }
        .style3
        {
            width: 90px;
        }



        .style4
        {
        }



       </style>
</head>
<body>
    <form id="form1" runat="server">     
            <table cellspacing="0" cellpadding="0">
                <tr>
                    <td class="celda1 sinBottom" colspan="6"><asp:Label ID="lblEscuela" runat="server"  Font-Bold="True" Font-Size="Medium" Text="CARRERA PROFESIONAL"></asp:Label></td>
                </tr>
                <tr>
                    <td class="celda1 sinBottom sinRight sinTop style3">Desde</td>
                    <td class="celda1 sinBottom sinleft sinRight sinTop "><input type="text"  runat="server" id="txtDesde"/></td>
                    <td class="celda1 sinBottom sinleft sinRight sinTop style4">Hora Inicio</td>
                    <td class="celda1 sinBottom sinleft sinTop " colspan="3"><asp:DropDownList ID="ddlInicioHora" runat="server"></asp:DropDownList>
                        :<asp:DropDownList ID="ddlInicioMinuto" runat="server"></asp:DropDownList>
                    </td>
                    
                </tr>
                <tr>
                    <td class="celda1 sinBottom sinRight sinTop style3">Hasta</td>
                    <td class="celda1 sinBottom sinleft sinRight sinTop"><input type="text" runat="server" id="txtHasta"/></td>
                    <td class="style4 celda1 sinBottom sinleft sinRight sinTop">Hora Fin</td>
                    <td class="celda1 sinBottom sinleft sinTop " colspan="3"><asp:DropDownList ID="ddlFinHora" runat="server" ></asp:DropDownList>
                        :<asp:DropDownList ID="ddlFinMinuto" runat="server"></asp:DropDownList>
                    </td>
                   
                </tr>
                <tr>
                    <td class="celda1 sinBottom sinRight sinTop style3">
                        Días</td>
                    <td class="celda1 sinBottom sinleft sinRight sinTop">
                        <asp:DropDownList ID="ddlDias" runat="server">
                            <asp:ListItem Value="TO">TODOS</asp:ListItem>
                            <asp:ListItem Value="LU">Lunes</asp:ListItem>
                            <asp:ListItem Value="MA">Martes</asp:ListItem>
                            <asp:ListItem Value="MI">Miércoles</asp:ListItem>
                            <asp:ListItem Value="JU">Jueves</asp:ListItem>
                            <asp:ListItem Value="VI">Viernes</asp:ListItem>
                            <asp:ListItem Value="SA">Sábado</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style4 celda1 sinBottom sinleft sinTop" colspan="4">&nbsp;</td>
                   
                </tr>
                <tr>
                    <td class="celda1 sinRight sinTop  style3">&nbsp;</td>
                    <td class="celda1 sinRight sinTop sinleft">
                        <asp:Button ID="BtnGuardar" runat="server" Text="Registrar" CssClass="btn" /></td>
                    <td class=" celda1 sinleft sinRight sinTop style4">&nbsp;</td>
                    <td class="celda1  sinleft sinTop " colspan="3"><asp:Button ID="BtnEnviarMail" runat="server" Text="Enviar Correo" CssClass="btn" /></td>
                     
                </tr>
                <tr>
                    <td class="style3">
                        &nbsp;</td>
                    <td class=" ">
                        &nbsp;</td>
                    <td class="style4">&nbsp;</td>                                        
                    <td class="style2">&nbsp;</td>                                        
                    <td class="style2">&nbsp;</td>                                        
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="celda1 sinRight style3">Semestre Académico<br />
                        <asp:DropDownList ID="ddlCiclo" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                    <td class="celda1 sinleft">Tipo de Estudio<br />
                        <asp:DropDownList ID="ddlTipoEstudio" runat="server" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="2">PREGRADO</asp:ListItem>
                            <asp:ListItem Value="1">ESCUELA PRE</asp:ListItem>
                        </asp:DropDownList></td>
                    <td class="style4">
                        &nbsp;</td>                                        
                    <td class="style4 celda1 sinRight">
                        Tipo Ambiente<br />
                        <asp:DropDownList ID="ddlTipoAmbiente" runat="server" AutoPostBack="True"></asp:DropDownList></td>                                        
                    <td class="style4 celda1 sinRight">
                        &nbsp;</td>                                        
                    <td class="celda1 sinleft">Estado Ambiente<br />
                        <asp:DropDownList ID="ddlEstadoAmbiente" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="style3">&nbsp;</td>
                    <td>&nbsp;</td>
                    <td class="style4">&nbsp;</td>                                        
                    <td class="style2">
                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Blue"></asp:Label>
                    </td>                                        
                    <td class="style2">
                        &nbsp;</td>                                        
                    <td class=" ">&nbsp;</td>
                </tr>
            </table>                          
   
   <div style="padding-top:10px;">
       <div style="float:left">
       <table cellpadding="0" cellspacing="0">   
       <tr>
       <td class="celda1">
           <asp:GridView ID="gridEscuelas" runat="server" CellPadding="4" 
               ForeColor="#333333" GridLines="Horizontal" DataKeyNames="codigo_cpf, nombre_cpf" 
               AutoGenerateColumns="False">
               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
               <Columns>           
                  <asp:CommandField ButtonType="Image" HeaderText="" 
                       SelectImageUrl="~/academico/horarios/administrar/images/sel.png" 
                       ShowSelectButton="True">               
                   </asp:CommandField>           
                   <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf" 
                       Visible="False" />
                   <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional"  />
               </Columns>
               <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />        
               <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
               <EditRowStyle BackColor="#999999" />
               <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
           </asp:GridView>
       </td>
       </tr> 
       </table>
       </div>
       <div style="float:left; width:30px;"></div>
       <div runat="server" id="divgridamb" style="float:left; font-weight:normal;" >           
           <asp:GridView ID="gridAmbientes" runat="server" AutoGenerateColumns="False" 
               BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
               CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="codigo_amb, codigo_aam">
               <RowStyle BackColor="#F7F7DE" />
               <Columns>
                    <asp:TemplateField HeaderText="" >
                    <ItemTemplate>                
                        <asp:CheckBox ID="chkElegir" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:BoundField DataField="codigo_amb" HeaderText="codigo_amb" Visible="false" />
                   <asp:BoundField DataField="nombre" HeaderText="AMBIENTE FICTICIO" />           
                   <asp:BoundField DataField="real" HeaderText="AMBIENTE REAL" />           
                   <asp:BoundField DataField="cap" HeaderText="CAP." />                                      
                   <asp:BoundField DataField="codigo_aam" HeaderText="codigo_aam" Visible="false"/>
                   <asp:TemplateField HeaderText="Detalle" >
                    <ItemTemplate>                        
                      <asp:LinkButton ID="lblOtrasEscuelas" runat="server">(0)</asp:LinkButton>                                       
                    </ItemTemplate>
                    </asp:TemplateField>
               </Columns>
               <FooterStyle BackColor="#CCCC99" />
               <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
               <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
               <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
               <AlternatingRowStyle BackColor="White" />
               
           </asp:GridView>
        
      </div>
  </div>
 
   
   </form>
</body>
</html>

