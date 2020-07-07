<%@ Page Language="VB" AutoEventWireup="false" CodeFile="otros.aspx.vb" Inherits="otros" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hoja de Vida :: Otros datos de interes</title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/tooltip.js"></script>
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="private/expediente.js"></script>
    
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .tab_pasar
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_sobre.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_seleccionado
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_seleccion.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_normal
        {
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_normal.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .style1
        {
            width: 100%;
        }
       
        .style2
        {
            height: 22px;
        }
       
    body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
	}
       
    </style>
</head>
<body>
<center>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <table cellpadding="0" cellspacing="0" class="style1">
                <tr>
                    <td align="left" valign="top" width="75%">
            <table cellpadding="0" cellspacing="0" class="tabla_personal" style="width: 100%;">
            <tr>
                <td align="left" class="titulo_tabla" style="height: 29px">
                    &nbsp;Registro de Datos Adicionales</td>
                <td align="right" class="titulo_tabla" style="height: 29px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 05 de 06"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                    font-family: verdana; height: 27px" colspan="2">
                    &nbsp;Registro de Datos Adicionales</td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 5px;" valign="top" colspan="2">
                    <table cellpadding="0" cellspacing="0" style="width: 98%; height: 525px;">
                        <tr>
                            <td align="center" class="borde_tab" valign="top">
                                <table cellpadding="0" cellspacing="0" style="width: 100%">
                                    <!--
									<tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                            &nbsp;Agregue una Descripción 
                                            <br />
                                            <span style="font-size: 12pt; color: #000000; font-family: Times New Roman">&nbsp;</span>Personal si desea.</td>
                                        <td align="left">
                                            <asp:TextBox ID="TxtDescripcion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Height="69px" MaxLength="2000" Style="font-weight: normal;
                                                font-size: 10pt; color: navy; font-family: verdana" TextMode="MultiLine" Width="97%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" colspan="1" style="width: 167px">
                                        </td>
                                        <td align="left" colspan="1" class="titulo_items">
                                            Max. 2000 caracteres</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" colspan="1" style="width: 167px">
                                        </td>
                                        <td colspan="1" align="left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                            &nbsp;Registre sus Habilidades</td>
                                        <td align="left">
                                            <asp:TextBox ID="TxtHabilidades" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Height="73px" MaxLength="2000" Style="font-weight: normal;
                                                font-size: 10pt; color: navy; font-family: verdana" TextMode="MultiLine" Width="97%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="left" class="titulo_items">
                                            Max. 2000 caracteres</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="left">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                            &nbsp;Registre sus Limitaciones</td>
                                        <td align="left">
                                            <asp:TextBox ID="TxtLimitaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Height="83px" MaxLength="2000" Style="font-weight: normal;
                                                font-size: 10pt; color: navy; font-family: verdana" TextMode="MultiLine" Width="97%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="left" class="titulo_items">
                                            Max. 2000 caracteres</td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="left">
                                            &nbsp;</td>
                                    </tr>-->
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                            &nbsp;Registre sus Hobbies y/o 
                                            <br />
                                            &nbsp;otros datos adicionales.</td>
                                        <td align="left">
                                            <asp:TextBox ID="TxtHobbies" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Height="83px" MaxLength="160" Style="font-weight: normal;
                                                font-size: 10pt; color: navy; font-family: verdana" TextMode="MultiLine" Width="97%"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="left" class="titulo_items">
                                            Max. 160 caracteres</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                        </td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            &nbsp;</td>
                                    </tr> <!--
                                    <tr>
                                        <td align="right" class="titulo_items" style="width: 167px">
                                            &nbsp;</td>
                                        <td align="right">
                                            <br />
                                            &nbsp;&nbsp;
                                            <asp:Button ID="CmdGuardar0" runat="server" CssClass="tab_normal"
                    Height="26px" Text="&lt;&lt; Anterior" Width="86px" />&nbsp;&nbsp;
                                            <asp:Button ID="CmdGuardar" runat="server" CssClass="tab_normal" Height="24px" Text="Finalizar"
                                                ValidationGroup="experiencia" Width="85px" /></td>
                                    </tr> -->
                                    <tr>
                <td align="right" style="height: 45px; border-top: gold 1px solid;" colspan="2">
                &nbsp;<asp:Button ID="CmdVolver" runat="server" Text="<< Anterior" 
                        CssClass="tab_normal" Width="86px" Height="26px" />&nbsp;<asp:Button 
                        ID="Button1" runat="server" Text="Siguiente&gt;&gt;" CssClass="tab_normal" 
                        Width="86px" Height="26px" />&nbsp;
                </td>
            </tr>
                                    <tr>
                                        <td>
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
                                                       <td style="text-align:justify">
                                                            <asp:Label ID="Label21" runat="server" Text="Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
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
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    &nbsp;</td>
            </tr>
        </table>
                    </td>
                    <td align="left" valign="top" width="25%">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td width="20%">
                                <table align="left">
                                    <tr>
                                        <td>
                                        <a href="personales.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="1. Datos personales" 
                                                src="images/hojavida/datospersonales.gif" /></td>
                                        </a>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="perfil.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="2. Perfil profesional" src="images/hojavida/perfilprofesional.gif" />
                                        </a>
                                        </td>
                                        
                                    </tr>
                                    <tr align="left">
                                        <td align="left" class="style2">
                                        <a href="educacionuniversitaria.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="3. Formación Académic" src="images/hojavida/formacionacademica.gif" />
                                        </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="experiencia.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="4. Experiencia laboral y asistencia a eventos" src="images/hojavida/experiencialaboral.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="otros.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="5. Otros datos" src="images/hojavida/otrosdatos_r.gif" />
                                        </a>
                                            </td>
                                    </tr>
									<tr>
                                        <td>
                                        <a href="futuroempleo.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="6. Futuro Empleo" src="images/hojavida/futuroempleo.gif" />
                                        </a>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80%" align="center" valign="top">
                                <table cellpadding="3" cellspacing="3" class="tabla_personal" 
                                    style="background-color: #FFFFCC; border: 1px solid #000080">
                                    <!--<tr>
                                        <td align="left">
                                            <font face="Arial" size="2"><span style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">
                                            <b>Estimado trabajador:</b><br />
                                            <br />
                                            Sírvase completar esta información para actualizar su Ficha Personal.<br />
                                            <br />
                                            Esta información será verificada y utilizada por la Universidad, para fines 
                                            académicos y administrativos.<br />
                                            <br />
                                            Muchas Gracias</span></font></td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>-->
                                    <tr>
                                       <td style="text-align:justify">
                                           <asp:Label ID="Label29" runat="server" Text="DECLARACIÓN JURADA" 
                                               Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                     </tr>
                                    <tr>
                                       <td style="text-align:justify">
                                            <asp:Label ID="Label23" runat="server" Text="Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                        </td>
                                     </tr>
                                </table>
                                    </td>
                        </tr>
                    </table>
                    </td>
                </tr>
            </table>
            
        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                <asp:PostBackTrigger ControlID="CmdGuardar"/>
                <asp:PostBackTrigger ControlID="btnCancelar"/>
         </Triggers>
    </asp:UpdatePanel>
    </form>
    </center>
</body>
</html>
