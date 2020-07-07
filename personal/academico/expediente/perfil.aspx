<%@ Page Language="VB" AutoEventWireup="false" CodeFile="perfil.aspx.vb" Inherits="perfil" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Perfil Profesional</title>
    <link rel="STYLESHEET" href="private/estilo.css"/>
        <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
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
       
    </style>
</head>
<body >
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td align="left" valign="top" width="75%">
        <table cellpadding="0" cellspacing="0" border="0" class="tabla_personal" width=100%>
            <tr>
                <td align="left" style="height: 29px;" class="titulo_tabla">
                    &nbsp;Registro de Perfil Profesional</td>
                <td align="right" style="height: 29px;" class="titulo_tabla">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 02 de 07"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="left" 
                    style="font-weight: bold; color: darkblue; border-bottom: gold 1px solid; height: 24px" 
                    colspan="2">
                    &nbsp; Perfil Profesional</td>
            </tr>
            <tr>
                <td align="center" style="height: 327px" valign="top" colspan="2">
                    <table style="width: 500px">
                        <tr>
                            <td rowspan="1" valign="top">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-size: 8pt; color: black; font-family: arial; text-align: justify">
                                            Realice una breve descripción no mayor a 15 líneas de los datos más resaltantes
                                            en cuanto a su desempeño profesional y/o laboral.<br />
                                            Esta información será mostrada en los diferentes módulos tanto de Personal y &nbsp;Alumnos USAT así como personas externas a la universidad.</td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:TextBox ID="TxtPerfil" runat="server" MaxLength="700" Rows="18" TextMode="MultiLine"
                                                Width="97%" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="9pt"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="font-size: 8pt; color: black; font-family: verdana">
                                            &nbsp; Max. 700 caracteres.</td>
                                    </tr>
                                </table>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 45px; border-top: gold 1px solid;" colspan="2">
                &nbsp;<asp:Button ID="CmdVolver" runat="server" Text="<< Anterior" 
                        CssClass="tab_normal" Width="86px" Height="26px" />&nbsp;<asp:Button 
                        ID="CmdGuardar" runat="server" Text="Siguiente&gt;&gt;" CssClass="tab_normal" 
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
                                    <asp:Label ID="Label21" runat="server" Text="1.- Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label19" runat="server" Text="2.- La información consignada deberá ser utilizada por la Universidad para informar a SUNAT en forma obligatoria, conforme  a las normas legales vigentes."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label22" runat="server" Text="3.- Dicha información deberá ser actualizada obligatoriamente por mi persona, previa coordinación con la Direcciónde Personal cuando haya un cambio en los datos, o cuando la USAT lo solicite."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label24" runat="server" Text="4.- Luego del presente registro me comprometo a entregar en un plazo de 5 días hábiles, las copias de los documentos sustentatorios de los datos consignados (DNI, actas de nacimiento y de matrimonio, etc) las cuales serán fiel copia de los originales que obran en mi poder."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label25" runat="server" Text="5.- Reconozco que la información que no sustente documentalmente no estará validada por la Dirección de Personal, y por tanto no será tomada en cuenta."></asp:Label>
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
            <td align="left" valign="top" width="25%">
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td width="20%">
                                <table align="left">
                                    <tr>
                                        <td>
                                        <a href="personales.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Datos personales" src="images/hojavida/datospersonales.gif" /></td>
                                        </a>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="perfil.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Perfil profesional" src="images/hojavida/perfilprofesional_r.gif" />
                                        </a>
                                        </td>
                                        
                                    </tr>
                                    <tr align="left">
                                        <td align="left" class="style2">
                                        <a href="educacionuniversitaria.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Títulos y grados académicos" src="images/hojavida/titulosygrados.gif" />
                                        </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="idiomas.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Idiomas y otros cursos" src="images/hojavida/idiomasyotros.gif" />
                                        </a>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="experiencia.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Experiencia laboral y asistencia a eventos" src="images/hojavida/exepriencia.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="distinciones.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Distinciones y honores" src="images/hojavida/distinciones.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="otros.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="Otros datos" src="images/hojavida/otrosdatos.gif" />
                                        </a>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80%" valign="top">
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
                                            <asp:Label ID="Label23" runat="server" Text="1.- Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                        </td>
                                     </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label26" runat="server" Text="2.- La información consignada deberá ser utilizada por la Universidad para informar a SUNAT en forma obligatoria, conforme  a las normas legales vigentes."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label27" runat="server" Text="3.- Dicha información deberá ser actualizada obligatoriamente por mi persona, previa coordinación con la Direcciónde Personal cuando haya un cambio en los datos, o cuando la USAT lo solicite."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label28" runat="server" Text="4.- Luego del presente registro me comprometo a entregar en un plazo de 5 días hábiles, las copias de los documentos sustentatorios de los datos consignados (DNI, actas de nacimiento y de matrimonio, etc) las cuales serán fiel copia de los originales que obran en mi poder."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                                         <td style="text-align:justify">
                                                <asp:Label ID="Label20" runat="server" Text="5.- Reconozco que la información que no sustente documentalmente no estará validada por la Dirección de Personal, y por tanto no será tomada en cuenta."></asp:Label>
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
                <asp:PostBackTrigger ControlID="btnCancelar"/>
        </Triggers>
    </asp:UpdatePanel>        
    </form>
</body>
</html>
