<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTestRevisaHorario.aspx.vb" Inherits="frmTestRevisaHorario" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Horario</title>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    <style type="text/css">

        .style7
        {
        }
 
        .style1
        {
        }
        .style4
        {
            width: 123px;
            height: 22px;
        }
        .style8
        {
            height: 20px;
        }
        .style22
        {
            width: 50%;
        }
        .style30
        {
            height: 15px;
        }
        .style31
        {
        }
        .style20
        {
            width: 616px;
        }
        .style10
        {
            width: 340px;
        }
        .style34
        {
            font-size: x-small;
        }
        .style35
        {
            width: 73px;
        }
        .style36
        {
            width: 171px;
        }
        .style42
        {
            width: 340px;
            height: 9px;
        }
        .style45
        {
            width: 590px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="font-family: Verdana; font-size: 11px">
    <div>
    
        <table style="width:100%;">
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="Label38" runat="server" Text="Filtrar por Estados"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlFiltroEstadoHorario" runat="server" 
                                AutoPostBack="True">
                                <asp:ListItem Value="%">Todos</asp:ListItem>
                                <asp:ListItem Value="P">Pendiente</asp:ListItem>
                                <asp:ListItem Value="C">Conforme</asp:ListItem>
                                <asp:ListItem Value="O">Observado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label39" runat="server" Text="Seleccione Trabajador"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPersonal" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="Label40" runat="server" Text="Evaluación Horario" 
                                ForeColor="Blue"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEstadoHorario" runat="server" AutoPostBack="True" 
                                BackColor="Yellow">
                                <asp:ListItem>CALIFICAR</asp:ListItem>
                                <asp:ListItem Value="C">Conforme</asp:ListItem>
                                <asp:ListItem Value="O">Observado</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Button ID="btnEnviar" runat="server" Text="Finalizar y Enviar" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblObservacionHorario" runat="server" Text="Observación" 
                                Visible="False"></asp:Label>
                        </td>
                        <td colspan="5">
                            <asp:TextBox ID="txtObservacionHorario" runat="server" Width="97%" 
                                Visible="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                            <td class="style5" style="font-weight: bold" colspan="7">
                                <hr />
                            </td>
                    </tr>
                    <tr>
                    <td colspan="7">
                    <asp:Label ID="lblObservacion" runat="server" Font-Size="Large" 
                        ForeColor="#FF6600"></asp:Label> <br />
                    <asp:Label ID="lblMensaje" runat="server" Font-Size="Small" 
                        ForeColor="Red"></asp:Label>
                                
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="font-weight: bold"  >
                                <table style="width:522px">
                                    <tr>
                                        <td class="style22">
                                            <table style="border: 1px solid #000000; width:100%; background-color: #FFFFCC;" >
                                                <tr>
                                                    <td colspan="2">
                                                        Datos Generales 
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td  style="width:100px" rowspan="6">
                                                        <asp:Image ID="imgFoto" runat="server" BorderColor="Black" BorderStyle="Solid" 
                                                            BorderWidth="1px" Height="122px" Width="113px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label37" runat="server" Text="Nombre"></asp:Label>
                                                    </td>
                                                    <td style="width:120PX">
                                                        <asp:Label ID="lblNombre" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label36" runat="server" Text="Centro Costos"></asp:Label>
                                                    </td>
                                                    <td class="style30">
                                                        <asp:Label ID="lblCeco" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label35" runat="server" Text="Dedicación"></asp:Label>
                                                    </td>
                                                    <td class="style31">
                                                        <asp:Label ID="lblDedicacion" runat="server" Font-Bold="False" Text=""></asp:Label>
                                                        &nbsp; &nbsp;
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label34" runat="server" Text="Fecha ingreso "></asp:Label>
                                                    </td>
                                                    <td class="style31">
                                                        <asp:Label ID="lblFechaIngreso" runat="server" Font-Bold="False" Text=""></asp:Label>
                                                        &nbsp; &nbsp; &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label33" runat="server" Text="Tipo"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTipo" runat="server" Font-Bold="False" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:Label ID="Label32" runat="server" Text="Nro. Horas Semanales"></asp:Label>
                                                    </td>
                                                    <td class="style31">
                                                        <asp:TextBox ID="txtHoras" runat="server" Width="53px" Enabled="False" 
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>                                                                                                       
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label31" runat="server" Text="Nro.Horas Asesoría Tesis"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHorasAsesoria" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label41" runat="server" 
                                                            Text="Nro.Horas Asesoría Tesis Go y Prof"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblHorasAsesoriaGOPP" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>                                                
                                                <td>
                                                    <asp:Label ID="Label3" runat="server" Text="Estado Horario"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEstadoHorario" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        
                                    </tr>
                                    
                                </table>
                            <td class="style7" rowspan="2" valign="top" align="right">                                                        
                                            <asp:Button ID="btnConsideraciones" runat="server" Font-Bold="True" 
                                                ForeColor="#CC0000" Text="?" style="text-align: right" />                            
                                <br style="text-align: right" />
                                <table style="width: 200px;">
                                    <tr>
                                        <td class="style22" rowspan="3" valign="top" align="right">                                            
                                            <table style="width:250px;">
                                                <tr>
                                                    <td align="left" class="style8" valign="top">
                                                        Vigencia del Horario:<asp:DropDownList ID="ddlSemana" runat="server" 
                                                            AutoPostBack="True" Height="20px" Width="100px">
                                                        </asp:DropDownList>
                                                        <br />
                                                        <asp:Label ID="lblFechas" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <td align="center">
                                                        <asp:GridView ID="gvTotalHorasSemanas" runat="server" BorderStyle="Solid" 
                                                        CellPadding="1" ForeColor="#333333" Font-Size="XX-Small" 
                                                            Caption="Total Horas por Semana">
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    
                                                    </td>
                                                <tr>
                                                    <td align="left" style="font-size: small" valign="top">
                                                        Total Horas Asesoría Tesis:
                                                        <asp:Label ID="lblHorasTesis" runat="server" ForeColor="Green" 
                                                            Font-Size="Small">0</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                        Total Horas Lectivas:&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Label ID="lblTotalHorasLectivas" runat="server" Text="0"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: small" valign="top">
                                                        Total Horas Semanales:
                                                        <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                                        <br />
                                                            <div id="Hr" runat="server" >
                                                        </div>
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td align="left" style="font-size: small" valign="top">
                                                        Total Horas Mensuales:
                                                        <asp:Label ID="lblHorasMensuales" runat="server" ForeColor="Blue"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" Width="100%" 
                                                            Height="22px" BackColor="#FFFF99" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: small" valign="top" align="center">
                                                       <asp:GridView ID="gvVistaHorario" runat="server" BorderStyle="Solid" 
                                                        CellPadding="1" ForeColor="#333333" Font-Size="XX-Small">
                                                        <RowStyle BackColor="#EFF3FB" />
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView> 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: small" valign="top">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="border: 1px solid #000000; font-size: x-small" valign="top">
                                                        <table align="center" bgcolor="White" style="width: 200px;">
                                                            <tr>
                                                            <td class="style4"  style="font-weight: bold">
                                                                    Leyenda:</td>
                                                            </tr>
                                                            <tr>                                                         
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label> 
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                <asp:Label ID="lblE" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblD" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                 </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align = "center" valign="middle">
                                                                    <asp:Label ID="lblCP" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblH" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblT" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblC" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblI" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>     
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGR" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td>
                                                                
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblCA" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>    
                                                                </td >
                                                                <td class="style4" align = "center" valign="middle">
                                                                <asp:Label ID="lblPE" runat="server" Text="Label" Height="35px" Width="100px" ></asp:Label>                                                                
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">

                                                                    <asp:Label ID="lblU" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>
                                                               
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblTG" runat="server" Text="Label" Height="35px" Width="100px"></asp:Label>                                                                            
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGA" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>    
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblGP" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                                                                                            
                                                            <tr>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    <asp:Label ID="lblF" runat="server" Text="Label" Height="35px" Width="100px" 
                                                                        Visible="False"></asp:Label>    
                                                                </td>
                                                                <td class="style4" align = "center" valign="middle">
                                                                    &nbsp;</td>
                                                            </tr>
                                                                                                                            
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                        <tr>
                            <td valign="top" class="style45"> 
                                <table style="width:0%;">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td style="font-weight: bold">
                                                        <b>Registro de Horario:</b>
                                                        <asp:Label ID="Label9" runat="server" Font-Bold="False" ForeColor="Blue" 
                                                            Text="[Debe elegir el horario laboral y el refrigerio]"></asp:Label>
                                                                          &nbsp;&nbsp;
                                                                          <asp:Button ID="btnBorrar" runat="server" Text="Borrar Horario" />
                                                    </td>
                                                </tr>
                                        
                                                <tr>
                            <td>
                                            <table style="width: 522px;">
                                                <tr>
                                                    <td style="border: 1px solid #0000FF; font-weight: bold; background-color: White">                                                                                                                                                                       
                                                        <table style="width: 100%; font-weight: normal;">                                                            
                                                            <tr>
                                                                <td class="style20" style="font-weight: bold; background-color: #DDEEFF;">
                                                                    Paso Nº 1. Horario Administrativo<br />
                                                                    <asp:Label ID="Label28" runat="server" Font-Bold="False" ForeColor="Black" 
                                                                        Text="[Haga Clic sobre el boton asignar horario, para registrar un horario administrativo]" 
                                                                        
                                                                        ToolTip="El tipo de hora que se considerará será el: Administrativo Institucional, para definir otro tipo de hora utilice el registro de Horario personalizado." 
                                                                        Font-Size="Smaller"></asp:Label>
                                                                    <br />
                                                                    <table style="width:100%;">
                                                                        <tr>
                                                                            <td>
                                                                                <table style="width: 100%; font-weight: normal;">
                                                                                    <tr>
                                                                                        <td class="style42">
                                                                                            &nbsp;
                                                                                            <asp:Label ID="lblHAdministrativo0" runat="server" Font-Bold="False" ForeColor="#0000CC" 
                                                                                                Text="- Horario administrativo: Lunes - Viernes de 08:00 - 16:45" 
                                                                                                Font-Size="Smaller"></asp:Label>
                                                                                        </td>
                                                                                        <td align="right">
                                                                                            <asp:Button ID="btnCopiarHorarioAdministrativo" runat="server" ForeColor="Blue" 
                                                                                                Text="Asignar horario" Font-Size="Smaller" style="text-align: center" 
                                                                                                Width="120px"/>
                                                                                        </td>                                                                                       
                                                                                    </tr>                                                                                    
                                                                                </table>                                                                               
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr> 
                                                            <tr>
                                                                <td>
                                                                <table style="border: 1px;">
                                    <tr>
                                        <td style="font-weight: bold; background-color:  #DDEEFF;">
                                            Paso Nº 2. Registro de refrigerio: <br />
                                            <asp:Label ID="Label16" runat="server" Font-Bold="False" ForeColor="Black" 
                                                
                                                
                                                
                                                
                                                Text="[Puede elegir un refrigerio predefinido o registrar un refrigerio personalizado]"></asp:Label>
                                            <br />
                                            <table style="font-weight: normal;">
                                                <tr>
                                                <br />
                                                    <td style="font-weight: bold; background-color: #DDEEFF;">
                                                        - Refrigerios predefinidos<br />
                                                        <asp:Label ID="Label17" runat="server" Font-Bold="False" ForeColor="Black" 
                                                            Text="[Haga clic sobre el botón del refrigerio que desea asumir para el presente ciclo (Administrativo institucional)]" 
                                                            
                                                            
                                                            ToolTip="El tipo de hora que se considerará será el: Administrativo Institucional, para definir otro tipo de hora utilice el registro de Horario personalizado." 
                                                            Font-Size="Smaller"></asp:Label>
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td>                                                                    
                                                                    <table style="font-weight: normal;">
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                                <asp:Label ID="Label18" runat="server" ForeColor="#339933" 
                                                                                    Text="- Refrigerio 1: 13:00 - 13:45"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnRefrigerio1" runat="server" ForeColor="#339933" 
                                                                                    Text="Refrigerio 1" Font-Size="Smaller" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label6" runat="server" ForeColor="#CC00CC" 
                                                                                    Text="- Refrigerio 3: 14:00 - 14:45" CssClass="style34"></asp:Label>
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnRefrigerio3" runat="server" ForeColor="#CC00CC" 
                                                                                    Text="Refrigerio 3" Font-Size="Smaller" Width="80px" />
                                                                            </td>                                                                            
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;&nbsp;<asp:Label ID="Label19" runat="server" ForeColor="Blue" 
                                                                                    Text="- Refrigerio 2: 13:45 - 14:30"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnRefrigerio2" runat="server" ForeColor="Blue" 
                                                                                    Text="Refrigerio 2" Font-Size="Smaller" Width="80px" />
                                                                            </td>
                                                                            <td>
                                                                                <asp:Label ID="Label8" runat="server" ForeColor="#663300" 
                                                                                    Text="- Refrigerio 4: 14:15 - 15:00" CssClass="style34"></asp:Label>                                          
                                                                            </td>
                                                                            <td align="right">
                                                                                <asp:Button ID="btnRefrigerio4" runat="server" ForeColor="#663300" 
                                                                                    Text="Refrigerio 4" Font-Size="Smaller" Width="80px" />
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>                                                
                                                <tr>
                                                    <td style="font-weight: bold; background-color: #DDEEFF;">
                                                        - Refrigerio personalizado
                                                        <br />
                                                        <asp:Label ID="Label25" runat="server" Font-Bold="False" ForeColor="Black" 
                                                            
                                                            
                                                            
                                                            
                                                            Text="[Seleccione la hora de inicio y automáticamente se cargarán 45 min. de refrigerio. Luego haga click en Refrigerio]" 
                                                            Font-Size="Smaller"></asp:Label>
                                                        <br />
                                                        <table>
                                                            <tr>
                                                                <td>                                                                    
                                                                    <table style="font-weight: normal;">
                                                                        <tr>
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" Text="Dia"
                                                                            CssClass="style34" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlDiaRefrigerio" runat="server">
                                                                                <asp:ListItem Value="TD">Todos Los Dias</asp:ListItem>
                                                                                <asp:ListItem Value="LU">Lunes</asp:ListItem>
                                                                                <asp:ListItem Value="MA">Martes</asp:ListItem>
                                                                                <asp:ListItem Value="MI">Miercoles</asp:ListItem>
                                                                                <asp:ListItem Value="JU">Jueves</asp:ListItem>
                                                                                <asp:ListItem Value="VI">Viernes</asp:ListItem>
                                                                                <asp:ListItem Value="SA">Sábado</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                            <td >
                                                                                &nbsp;
                                                                                 <asp:Label ID="Label1" runat="server"
                                                                                    Text="Inicio" CssClass="style34" Font-Bold="True"></asp:Label>
                                                                            </td>
                                                                            <td  class="style35">                                                                                
                                                                                <asp:DropDownList ID="ddlRefrigerioInicio" runat="server" AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="style36">
                                                                                <asp:Label ID="Label2" runat="server"
                                                                                    Text="Fin" CssClass="style34" Font-Bold="True"></asp:Label>
                                                                                    
                                                                                &nbsp;&nbsp;
                                                                                    
                                                                                <asp:Label ID="lblRefrigerioFin" runat="server" Text="00:00"></asp:Label>
                                                                                   
                                                                            </td>
                                                                            
                                                                           
                                                                                                                                              
                                                                            <td align="right">
                                                                                <asp:Button ID="btnRefrigerio" runat="server" 
                                                                                    Text="Refrigerio" Font-Size="Smaller" Width="80px" />
                                                                            </td>
                                                                        </tr>
                                                                        </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                    <td style="font-weight: bold; background-color: #DDEEFF;">
                                    <table style="width: 100%; font-weight: normal;"> 
                                    <tr>
                                    <td colspan="2" style="font-weight: bold; background-color: #DDEEFF;">
                                        <asp:Label ID="Label5" runat="server" Text="Paso Nº 3. Importar Hrs. Lectivas" 
                                            Font-Bold="True"></asp:Label>
                                            <br />
                                                <asp:Label ID="Label10" runat="server" Font-Bold="False" ForeColor="Black" 
                                                            Text="[Haga clic sobre el botón Importar horario, si tuviera horas de lectivas."
                                                            Font-Size="Smaller"></asp:Label>
                                            
                                    </td>
                                    </tr>                                                                                   
                                      <tr>
                                       <td class="style10" style="font-weight: bold; background-color: #DDEEFF;">
                                        &nbsp;
                                        <asp:Label ID="Label7" runat="server" Font-Bold="False" ForeColor="#339933" 
                                                                                                Text="- Horario académico" Font-Size="Smaller"></asp:Label>
                                                                                        </td>
                                                                                        <td align = "right" style="font-weight: bold; background-color: #DDEEFF;">
                                                                                            <asp:Button ID="btnImportarHorarioAcademico" runat="server" ForeColor="Green" 
                                                                                                Text="Importar horario" Font-Size="Smaller" style="text-align: center" 
                                                                                                Width="120px"/>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                    </td>
                                    </tr>
                                    </table>
                                                                    
                                                                </td>
                                                            </tr>                                                           
                                                            <tr>
                                                            
                                                                <td class="style20" style="font-weight: bold; background-color: #DDEEFF;">
                                                                    Paso Nº 4: Personalizar horario<br />
                                                                    <asp:Label ID="Label29" runat="server" Font-Bold="False" ForeColor="Black" 
                                                                        
                                                                        
                                                                        Text="[Especifique la información correspondiente  al horario y haga clic en Añadir]" 
                                                                        Font-Size="Smaller"></asp:Label>
                                                                    <br />
                                                                    <table style="width:100%">
                                                                        <tr>
                                                                            <td width="25%" class="style34">
                                                                                Día</td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlDia" runat="server" Font-Size="Smaller">
                                                                                    <asp:ListItem Value="LU">Lunes</asp:ListItem>
                                                                                    <asp:ListItem Value="MA">Martes</asp:ListItem>
                                                                                    <asp:ListItem Value="MI">Miercoles</asp:ListItem>
                                                                                    <asp:ListItem Value="JU">Jueves</asp:ListItem>
                                                                                    <asp:ListItem Value="VI">Viernes</asp:ListItem>
                                                                                    <asp:ListItem Value="SA">Sábado</asp:ListItem>
                                                                                    <asp:ListItem Value="DO">Domingo</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="style34">Inicio</td>
                                                                            <td>
                                                                            <asp:DropDownList ID="ddlHoraInicio" runat="server" Font-Size="Smaller">                                                                                    
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td class="style34">Fin</td>
                                                                            <td align="right">
                                                                            <asp:DropDownList ID="ddlHoraFin" runat="server"
                                                                                    Font-Size="Smaller">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>                                                                        
                                                                        <tr>
                                                                        <td class="style34">Tipo Actividad</td>
                                                                        <td colspan="5">
                                                                        <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="True" 
                                                                                    Font-Size="Smaller" Width="100%">
                                                                                    <asp:ListItem Value="D">Docencia</asp:ListItem>
                                                                                    <asp:ListItem Value="P">Práctica Externa</asp:ListItem>
                                                                                    <asp:ListItem Value="T">Asesoría de Tesis</asp:ListItem>
                                                                                    <asp:ListItem Value="I">Investigación</asp:ListItem>
                                                                                    <asp:ListItem Value="A">Administrativo Institucional</asp:ListItem>
                                                                                    <asp:ListItem Value="E">Apoyo Administrativo en Escuela/Facultad</asp:ListItem>
                                                                                    <asp:ListItem Value="F">Tutoría</asp:ListItem>
                                                                                    <asp:ListItem Value="G">Gestión Académica</asp:ListItem>
                                                                                    <asp:ListItem Value="H">Horas Asistenciales CIS/Clínica USAT</asp:ListItem>
                                                                                </asp:DropDownList>
                                                                        </td>
                                                                        <td></td>
                                                                        <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style34">
                                                                                <asp:Label ID="lblEsFacuDep" runat="server" Text="Escuela/Facultad/Dpto"></asp:Label>
                                                                            </td>
                                                                            <td colspan="5" align="center">
                                                                                   <asp:RadioButton ID="rdbDepartamento" runat="server" GroupName="Opciones" 
                                                                                                Text="Departamento" Font-Size="Smaller" AutoPostBack="True" />
                                                                                            
                                                                                                <asp:RadioButton ID="rdbFacultad" runat="server" GroupName="Opciones" 
                                                                                                Text="Facultad" Font-Size="Smaller" AutoPostBack="True" />
                                                                                               <asp:RadioButton ID="rdbEscuela" runat="server" GroupName="Opciones" 
                                                                                                Text="Escuela" Font-Size="Smaller" AutoPostBack="True" />
                                                                            
                                                                                <asp:DropDownList ID="ddlEsFacuDep" runat="server" Width="100%" 
                                                                                    Font-Size="Smaller" AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td class="style34">
                                                                                Centro costos</td>
                                                                            <td colspan="5">
                                                                                <asp:DropDownList ID="ddlCentroCostos" runat="server" Width="100%" 
                                                                                    Font-Size="Smaller" AutoPostBack="True">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Label ID="Label30" runat="server" Text="Encargo" 
                                                                                    ToolTip="Encargo administrativo" CssClass="style34"></asp:Label>
                                                                            </td>
                                                                            <td colspan="2">
                                                                                <asp:TextBox ID="txtEncEscuela" runat="server"  Font-Size="Smaller" Width="150px"></asp:TextBox>
                                                                            </td>
                                                                            <td class="style34">Resolución</td>
                                                                            <td colspan="2"><asp:TextBox ID="txtResEscuela" runat="server" Font-Size="Smaller" Width="97%"></asp:TextBox></td>
                                                                            <td></td>
                                                                            <td></td>
                                                                       </tr>
                                                                        <tr>
                                                                            <td class="style34">
                                                                                Observación</td>
                                                                            <td colspan="5">
                                                                                <asp:TextBox ID="txtObservacion" runat="server" Font-Size="Smaller" Width="99%"></asp:TextBox>
                                                                            </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>                                                                       
                                                                        <tr>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td> </td>
                                                                            <td></td>
                                                                            <td></td>
                                                                            <td align="right"><asp:Button ID="btnAceptar" runat="server" Text="Añadir" 
                                                                                    Font-Size="Smaller" Width="120px" /></td>
                                                                            <td></td>
                                                                            <td></td>
                                                                        </tr>
                                                                        </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                                    </tr>
                            <tr>
                                <td style="font-weight: bold"></td>
                           &nbsp;</tr>
                                                <tr>
                            <td style="font-weight: bold">                                
                                 
                                                    </tr>                                
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <asp:GridView ID="gvEditHorario" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Distribución de carga Horaria" Width="522px" 
                                                AutoGenerateColumns="False">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="Cod" HeaderText="Cod" />
                                                    <asp:BoundField DataField="DIA" HeaderText="DIA" />
                                                    <asp:BoundField DataField="HInicio" HeaderText="HInicio" />
                                                    <asp:BoundField DataField="HFin" HeaderText="HFin" />
                                                    <asp:BoundField DataField="Tipo" HeaderText="Tipo" />
                                                    <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                                                    <asp:BoundField DataField="CCostos" HeaderText="CCostos" />
                                                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                                                        DeleteImageUrl="../../images/eliminar.gif" DeleteText="" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="#AEC9FF" />
                                            </asp:GridView>                                        
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                    <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Periodo Laborable: 
                                            <asp:DropDownList ID="cboPeriodo" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                            <asp:GridView ID="gvListaCambios" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" 
                                                Caption="Histórico de envíos de horarios" AutoGenerateColumns="False" 
                                                Font-Size="Smaller">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="Periodo" HeaderText="Periodo" />
                                                    <asp:BoundField DataField="MesVigente" HeaderText="MesVigente" />
                                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                                    <asp:BoundField DataField="Hora" HeaderText="Hora" />
                                                    <asp:BoundField DataField="RegistradorPor" HeaderText="RegistradoPor" />
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" 
                                                        SelectImageUrl="../../images/resultados.gif" />
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
