<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTestRevisaDirPersonal.aspx.vb" Inherits="frmTestRevisaDirPersonal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Horario</title>
    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>
	<script type="text/javascript" language="JavaScript" src="../../private/jq/lbox/thickbox.js"></script>
	<link rel="stylesheet" href="../../private/jq/lbox/thickbox.css" type="text/css" media="screen" />
    
    <link href="css/estilo.css" rel="stylesheet" type="text/css" />
    
	
	<script src="../../private/funciones.js" type="text/javascript"></script>
	
	<link rel="stylesheet" href="http://yui.yahooapis.com/3.7.2/build/cssbutton/cssbutton.css" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    
	
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <div >  
            <table style="width:100%;">
            <tr>
                <td colspan="2">
                    <table style="width:100%">
                        <tr>
                            <td style="width:20%">
                                <asp:Label ID="Label38" runat="server" Text="FILTRAR POR ESTADOS"></asp:Label>
                            </td>
                            <td >
                                <asp:DropDownList ID="ddlFiltroEstadoHorario" runat="server" 
                                    AutoPostBack="True" Width="50%">
                                    <asp:ListItem Value="%">Todos</asp:ListItem>
                                    <asp:ListItem Value="P">Pendiente</asp:ListItem>
                                    <asp:ListItem Value="C">Conforme</asp:ListItem>
                                    <asp:ListItem Value="O">Observado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label41" runat="server" Text="CENTRO DE COSTOS"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCentroCostosHP" runat="server" AutoPostBack="True" 
                                    Width="50%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                             <td>
                                <asp:Label ID="Label39" runat="server" Text="SELECCIONE TRABAJADOR"></asp:Label>
                            </td>
                            <td>
                                
                                        <asp:DropDownList ID="ddlPersonal" runat="server" AutoPostBack="True" 
                                            Width="50%">
                                        </asp:DropDownList>        
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                
                                <asp:Label ID="Label40" runat="server" Text="EVALUACIÓN HORARIO" 
                                    ForeColor="Blue"></asp:Label>
                                
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEstadoHorario" runat="server" AutoPostBack="True" 
                                    BackColor="Yellow" Width="50%">
                                    <asp:ListItem>CALIFICAR</asp:ListItem>
                                    <asp:ListItem Value="C">Conforme</asp:ListItem>
                                    <asp:ListItem Value="O">Observado</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblObservacionHorario" runat="server" Text="OBSERVACIÓN" 
                                    Visible="False"></asp:Label>
                            </td>
                            <td colspan="5">
                                <asp:TextBox ID="txtObservacionHorario" runat="server" Width="97%" 
                                    Visible="False"></asp:TextBox>
                                <asp:TextBox ID="txtId" runat="server" Visible="False" Width="10px"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:20%">
                                        </td>
                                        <td style="width:20%">
                                            <asp:CheckBox ID="chkActivarBiblioteca" CssClass=""  runat="server" AutoPostBack="True" Text="Activar Biblioteca" />
                                        </td>
                                        <td style="width:20%">
                                            <asp:CheckBox ID="chkActivarCCSalud" runat="server" AutoPostBack="True" Text="Activar CC. Salud" />
                                        </td>
                                        <td style="width:40%">
                                            <asp:CheckBox ID="chkActivaEnfermeria" runat="server" AutoPostBack="True" Text="Activar Enfermeria" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <table style="width:100%">
                                    <tr>
                                        <td style="width:20%"></td>
                                        <td style="width:13.33%">
                                                <asp:Button ID="btnEnviar"  CssClass="yui3-button" runat="server" Text="Finalizar y Enviar"/>
                                        </td>
                                        <td style="width:13.33%">
                                               <asp:Button ID="btnActivarBiblioteca" CssClass="yui3-button" runat="server" Text="Activar Biblioteca" Width="110px" Enabled="False"/>
                                        </td>
                                        <td  style="width:13.33%">
                                                <asp:Button ID="btnActivarCCSalud" CssClass="yui3-button" runat="server" Text="Act.CC Salud" Width="110px" Enabled="False"/>
                                        </td>
                                        <td  style="width:13.33%">
                                            <asp:Button ID="cmdActivar" runat="server"  CssClass="yui3-button" Text="Activar" Width="110px"/>
                                        </td>
                                       
                                       <td  style="width:13.33%">
                                            <asp:Button ID="btnActivaEnfermeria" runat="server" CssClass="yui3-button" 
                                                Text="Enfermeria" Width="110px" Enabled="False"/>
                                        </td>
                                        <td  style="width:13.33%">
                                            <a href="frmListaDocentesTesis.aspx?accion=A&codigo_tes=0&mod=<%=Request.QueryString("mod")%>&ctf=<%=Request.QueryString("ctf")%>&id=<%=Request.QueryString("id")%>&KeepThis=true&TB_iframe=true&height=600&width=900&modal=true" title="Lista Docentes - Hrs Tesis" class="thickbox">
                                                <asp:Button ID="btnTesis" runat="server" CssClass="yui3-button" Text="Hrs, Tesis" Width="110px" Visible="true"/>
                                            </a>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:100%" colspan="7" align="left" >
                                <a href="frmListaDocentesTesis.aspx?accion=A&codigo_tes=0&mod=<%=Request.QueryString("mod")%>&ctf=<%=Request.QueryString("ctf")%>&id=<%=Request.QueryString("id")%>&KeepThis=true&TB_iframe=true&height=600&width=900&modal=true" title="Lista Docentes - Hrs Tesis" class="thickbox">
                                    &nbsp;</a><asp:Button ID="cmdActivarCIS" runat="server" CssClass="yui3-button" Text="Act. CIS" Width="110px" Visible="False"/>
                            </td>
                        </tr>
                        <tr>
                                <td class="style5" style="font-weight: bold" colspan="7">
                                    <hr />
                                </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                <asp:Label ID="lblObservacion" runat="server" Font-Size="Large" ForeColor="#FF6600"></asp:Label> <br />
                                <asp:Label ID="lblMensaje" runat="server" Font-Size="Larger" ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr valign="top">
                <td style="width:30%">
                     <table style="width:100%">
                            <tr>
                                <td style="font-weight: bold"  valign="top">
                                    <table style="width:522px">
                                        <tr>
                                            <td class="style22">
                                                <table style="border: 1px solid #000000; width:100%; background-color: #FFFFCC;" >
                                                    <tr>
                                                        <td colspan="2">
                                                            Datos Generales 
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td style="width:100px" rowspan="6" colspan="2">
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
                                                            <asp:Label ID="lblCeco" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label35" runat="server" Text="Dedicación"></asp:Label>
                                                        </td>
                                                        <td class="style31">
                                                            <asp:Label ID="lblDedicacion" runat="server" Font-Bold="False"></asp:Label>
                                                            &nbsp; &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label34" runat="server" Text="Fecha ingreso "></asp:Label>
                                                        </td>
                                                        <td class="style31">
                                                            <asp:Label ID="lblFechaIngreso" runat="server" Font-Bold="False"></asp:Label>
                                                            &nbsp; &nbsp; &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label33" runat="server" Text="Tipo"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblTipo" runat="server" Font-Bold="False"></asp:Label>
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
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label31" runat="server" Text="Nro.Horas Asesoría Tesis"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblHorasAsesoria" runat="server" Text="0"></asp:Label>
                                                        </td>
                                                        <td colspan="2">Contrato</td>
                                                    </tr>
                                                    <tr>                                                
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Estado Horario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEstadoHorario" runat="server" Text=""></asp:Label>
                                                        </td>
                                                        
                                                        <td><asp:Label ID="Label50" runat="server" Text="Estado"></asp:Label></td>
                                                        <td><asp:Label ID="lblEstado" runat="server" Text=""></asp:Label></td>
                                                        
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label42" runat="server" Text="Registro Horario"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEnvioDirector_per" runat="server"></asp:Label>
                                                        </td>
                                                        <td><asp:Label ID="Labe151" runat="server" Text="Inicio"></asp:Label></td>
                                                        <td><asp:Label ID="lblFechaInicio" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label43" runat="server" Text="Enviado a Personal"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblEnvioDirPersonal_per" runat="server"></asp:Label>
                                                        </td>
                                                        <td><asp:Label ID="Label52" runat="server" Text="Fin"></asp:Label></td>
                                                        <td><asp:Label ID="lblFechaFin" runat="server" Text=""></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><asp:Label ID="Label13" runat="server" Visible="false" Text="Tipo Contrato:"></asp:Label></td>
                                                        <td><asp:Label ID="lbltipocontrato" Visible="false" runat="server" Text=""></asp:Label></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                    <asp:Panel ID="pnlContrato" Visible="false" runat="server">
                                                        <tr>
                                                            <td><asp:Label ID="Label11" runat="server" Text="Contrato Vigente:"></asp:Label></td>
                                                            <td><asp:Label ID="lblContrato" runat="server" Text=""></asp:Label></td>
                                                            <td></td>
                                                            <td></td>
                                                        </tr>
                                                    </asp:Panel>
                                                    <tr>
                                                        <td><asp:Label ID="Label12" runat="server" Text="Carga Académica"></asp:Label></td>
                                                        <td><asp:Label ID="lblCarga" runat="server" Text=""></asp:Label></td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>                                        
                                        </tr>
                                        <tr>
                                            <td class="style5" style="font-weight: bold" colspan="7">
                                                <hr />
                                           </td>
                                        </tr>
                                        <tr>
                                            <td class="style5" style="font-weight: bold" colspan="7">
                                                        <hr />
                                           </td>
                                        </tr>
                                        <tr>
                                           <td style="font-weight: bold">
                                                <b>Registro de Horario:</b>
                                                    <asp:Label ID="Label9" runat="server" Font-Bold="False" ForeColor="Blue" Text="[Debe elegir el horario laboral y el refrigerio]"></asp:Label>
                                                                              &nbsp;&nbsp;
                                                    <asp:Button ID="btnBorrar" CssClass="yui3-button" runat="server" Text="Borrar Horario" />
                                           </td>
                                       </tr>
                                        <tr>
                                            <td valign="top">
                                                <table>
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
                                                                                                <asp:Button ID="btnCopiarHorarioAdministrativo" CssClass="yui3-button" runat="server" ForeColor="Blue" 
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
                                                                                    <asp:Button ID="btnRefrigerio1" runat="server" ForeColor="#339933" CssClass="yui3-button" Width="80px"
                                                                                        Text="Refrigerio 1" Font-Size="Smaller" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" ForeColor="#CC00CC" 
                                                                                        Text="- Refrigerio 3: 14:00 - 14:45" CssClass="style34"></asp:Label>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Button ID="btnRefrigerio3" runat="server" ForeColor="#CC00CC" CssClass="yui3-button"
                                                                                        Text="Refrigerio 3" Font-Size="Smaller" Width="80px" />
                                                                                </td>                                                                            
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    &nbsp;&nbsp;<asp:Label ID="Label19" runat="server" ForeColor="Blue" 
                                                                                        Text="- Refrigerio 2: 13:45 - 14:30"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button ID="btnRefrigerio2" runat="server" ForeColor="Blue" CssClass="yui3-button" 
                                                                                        Text="Refrigerio 2" Font-Size="Smaller" Width="80px" />
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Label ID="Label8" runat="server" ForeColor="#663300" 
                                                                                        Text="- Refrigerio 4: 14:15 - 15:00" CssClass="style34"></asp:Label>                                          
                                                                                </td>
                                                                                <td align="right">
                                                                                    <asp:Button ID="btnRefrigerio4" runat="server" ForeColor="#663300" CssClass="yui3-button"
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
                                                                                    <asp:Button ID="btnRefrigerio" runat="server" CssClass="yui3-button"
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
                                                                Text="[Haga clic sobre el botón Importar horario, si tuviera horas de Lectivas."
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
                                                                                                <asp:Button ID="btnImportarHorarioAcademico" runat="server" ForeColor="Green" CssClass="yui3-button"
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
                                                                                    <asp:Label ID="lblDescripcion" runat="server" Text="Observación"></asp:Label>
                                                                                </td>
                                                                                <td colspan="5">
                                                                                    <asp:TextBox ID="txtObservacion" runat="server" Font-Size="Smaller" Width="99%"></asp:TextBox>
                                                                                </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                            </tr>                                                                       
                                                                            <tr>
                                                                                <td></td>
                                                                                <td>
                                                                                    &nbsp;</td>
                                                                                <td> </td>
                                                                                <td></td>
                                                                                <td></td>
                                                                                <td align="right">
                                                                                    <asp:Button ID="btnAgregar" CssClass="yui3-button" runat="server" Text="Añadir" Width="100%" />
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
                                             </table>
                                            </td>
                                        </tr>
                                                          
                                  
                    </table>
                                </td>
                             </tr>
                             <tr>
                                <td>
                                    <asp:GridView ID="gvLeyenda" CssClass="mGrid" Width="100%" runat="server" 
                                        AutoGenerateColumns="False" DataKeyNames="codigo_td,color_td">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_td" HeaderText="codigo_td" Visible="False" />
                                            <asp:BoundField DataField="descripcion_td" HeaderText="TIPO ACTIVIDAD" />
                                            <asp:BoundField DataField="abreviatura_td" HeaderText="ABR." />
                                            <asp:BoundField DataField="color_td" HeaderText="color_td" Visible="False" />
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
                   </table>
                </td>
                <td style="width:50%">
                    <table style="width: 100%;">
                        <tr>
                             <td valign="top" style="width:90%">
                                <table>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width:50%" bgcolor="#D1DDEF" height="30px" rowspan="2">
                                                        <asp:Label ID="Label44" runat="server" Text="VIGENCIA DEL HORARIO" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td style="width:100%" bgcolor="#D1DDEF" height="30px">
                                                        <asp:DropDownList ID="ddlSemana" runat="server" 
                                                        AutoPostBack="True" Height="20px" Width="100%">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:50%">
                                                        <asp:Label ID="lblFechas" runat="server" ForeColor="Red"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="Label49" runat="server" Text="TOTAL HORAS POR SEMANA" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td style="width:50%" bgcolor="#D1DDEF" height="30px">
                                                        <asp:GridView ID="gvTotalHorasSemanas" 
                                                        CssClass="mGrid" Width="100%"
                                                        runat="server" BorderStyle="Solid" CellPadding="1" ForeColor="#333333" 
                                                            Font-Size="XX-Small" AutoGenerateColumns="False">
                                                        <RowStyle BackColor="#EFF3FB" />
                                                            <Columns>
                                                                <asp:BoundField DataField="NroSemana" HeaderText="NroSemana" />
                                                                <asp:BoundField DataField="TotalHoras" HeaderText="TotalHoras" />
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
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="Label45" runat="server" Text="T. HORAS DE ASESORIA TESIS" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="lblHorasTesis" runat="server" ForeColor="Green">0</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="Label46" runat="server" Text="T. HORAS LECTIVAS" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        
                                                        <asp:Label ID="lblTotalHorasLectivas" runat="server" Text="0"></asp:Label>
                                                        
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="Label47" runat="server" Text="T. HORAS SEMANALES" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="lblHorasSemanales" runat="server" ForeColor="Blue"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="Label48" runat="server" Text="T. HORAS MENSUALES" 
                                                            Font-Size="XX-Small"></asp:Label>
                                                    </td>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Label ID="lblHorasMensuales" runat="server" ForeColor="Blue"></asp:Label>
                                                        </td>
                                                </tr>
                                                <tr>
                                                    <td bgcolor="#98E54B" height="30px">
                                                        <div id="Hr" runat="server" visible="true" ></div>
                                                    </td>
                                                    <td bgcolor="#D1DDEF" height="30px">
                                                        <asp:Button ID="btnRefrescar" runat="server" Text="Refrescar" Width="100%" CssClass="yui3-button"
                                                            Height="22px" BackColor="#FFFF99" Font-Size="XX-Small" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <asp:GridView ID="gvEditHorario" CssClass="mGrid" runat="server" CellPadding="4" 
                                                ForeColor="#333333" Caption="DISTRIBUCION DE CARGA HORARIA" Width="100%" 
                                                AutoGenerateColumns="False" Font-Size="Smaller" GridLines="None">
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
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvListaCambios"  CssClass="mGrid" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" 
                                                Caption="HISTORICO DE ENVIOS DE HORARIOS" AutoGenerateColumns="False" Font-Size="Smaller" 
                                                Width="100%" Font-Bold="False">
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
                                </table>
                             </td>
                             <td style="font-size: small; width:10%"  valign="top" align="center">
                                <asp:GridView ID="gvVistaHorario" 
                                CssClass="mGrid"
                                runat="server" BorderStyle="Solid" 
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
                            <td>
                    <!--Popup -->
                <asp:ImageButton ID="ibtnMostrarPopUpInforme" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorInforme" runat="server" Style="display: none; Width:400px"  CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraInforme" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:300px">
                            
                            <table style="width: 100%;background-color:White;">
                                <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUpInforme" runat="server" Text="DECLARACIÓN JURADA"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                                <tr>
                                <td style="text-align:justify">
                                    <asp:Label ID="lblDeclarante" runat="server" Text=""></asp:Label>
                               </td>
                            </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvListaTraActivar" runat="server">
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White; height:30px">
                            <tr>
                                <td style="width: 50%" align="center">
                                    <asp:Button ID="btnGuardarInforme" 
                                    runat="server" Text="            Acepto" 
                                    CssClass="conforme1" 
                                    Height="35px" Width="100px" 
                                    ValidationGroup="btnGuardarInforme" 
                                    ToolTip="Guardar" />
                                </td>
                                 <td style="width: 50%" align="center">
                                    <asp:Button ID="btnCancelar" 
                                    runat="server" Text="           No Acepto" 
                                    CssClass="rechazar_inv" 
                                    Height="35px" Width="100px" 
                                    ToolTip="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender 
                    ID="mpeInforme" runat="server" 
                    TargetControlID="ibtnMostrarPopUpInforme"
                    PopupControlID="pnlContedorInforme"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraInforme" 
                    />
                            </td>
                            <td></td>
                        </tr>
                        
                                </table>
                </td>
            </tr>
          
                            
                                
                            <td class="style7" rowspan="2" valign="top" align="right">                                                        
                                    <asp:Button ID="btnReporteHorasTesis" runat="server" CausesValidation="False" 
                                        Text="Reporte Hrs. Asesoria Tesis" UseSubmitBehavior="False" Height="26px" 
                                        Width="170px" Visible="False" />
                                    <asp:Button ID="btnConsideraciones" runat="server" Font-Bold="True" 
                                        ForeColor="#CC0000" Text="?" style="text-align: right" Visible="False" />                            
                                <br style="text-align: right" />
                                
                        <tr>
                            <td valign="top" class="style45"> 
                                <table style="width:0%;">
                                    <tr>
                                        <td>
                                            
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
        </div>
    </form>
</body>
</html>
