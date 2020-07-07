<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hojavida.aspx.vb" Inherits="Consultas_hojavida" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link rel="STYLESHEET" href="../private/estilo.css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%; border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center" colspan="3" style="width: 100%">
                    <asp:Button ID="pdp" runat="server" CssClass="word" Font-Bold="True" Text="      PDP"
                        Width="84px" />
                    <asp:Button ID="Button1" runat="server" CssClass="word" Font-Bold="True" Text="      CV"
                        Width="84px" />&nbsp;&nbsp;<input id="Button3" class="salir" style="width: 77px" type="button" value="      Regresar" onclick="javascript:history.back(); return false;" />
                </td>
            </tr>
            </table>
    
    </div>
        <asp:Table ID="Table2" runat="server" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"
            CellPadding="0" CellSpacing="0" Height="98px" HorizontalAlign="Center" Width="144px">
            <asp:TableRow ID="TableRow1" runat="server">
                <asp:TableCell ID="TableCell1" runat="server">
                    <table cellpadding="0" cellspacing="0" style="width: 713px">
                        <tbody>
                            <tr>
                                <td align="center" colspan="3">
                                    &nbsp;<asp:Image ID="ImgFoto" runat="server" BorderColor="Black" BorderWidth="1px"
                                        Height="130px" Style="margin: 5px" Width="100px" /></td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Label ID="LblNombres" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="Medium"
                                        ForeColor="Black" Style="text-transform: uppercase; color: navy; font-family: verdana"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td align="left" colspan="2">
                                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="X-Small" Font-Underline="True"
                                        ForeColor="Black" Text="DATOS GENERALES"></asp:Label></td>
                            </tr>
                            <tr style="text-decoration: underline">
                                <td>
                                </td>
                                <td colspan="2">
                                    <table cellpadding="1" cellspacing="0">
                                        <tbody>
                                            <tr style="font-size: 9pt">
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        DNI</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblDNI" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        Dirección</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblDireccion" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        Teléfono</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblTelefono" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        Celular</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblCelular" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        E-Mail</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblEmail" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        Estado Civil</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblCivil" runat="server"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td style="font-weight: normal; font-size: 9pt; width: 128px; color: darkred; font-family: VERDANA;
                                                    text-align: left">
                                                        Edad</td>
                                                <td align="left" style="font-size: 9pt; color: navy; font-family: verdana">
                                                    <asp:Label ID="LblEdad" runat="server"></asp:Label></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 22px; height: 21px">
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="LblPerfilEtiqueta" runat="server" Font-Bold="True" Font-Size="X-Small"
                                        Font-Underline="True" ForeColor="Black" Text="PERFIL"></asp:Label><asp:Label ID="LblPerfil"
                                            runat="server" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Style="text-align: justify"></asp:Label>
                                    <asp:Label ID="LblGrados" runat="server" Font-Bold="True" Font-Size="X-Small" Font-Underline="True"
                                        ForeColor="Black" Text="GRADOS ACADÉMICOS Y TÍTULOS PROFESIONALES"></asp:Label>
                                    <asp:Label ID="LblGrado" runat="server" Font-Bold="True" ForeColor="Black" Text="GRADOS ACADÉMICOS"></asp:Label>
                                    <asp:DataList ID="DatGrados" runat="server" CellPadding="0" DataSourceID="Grados"
                                        Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana; width: 160px;">
                                                                &nbsp;Grado</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="nombre_graLabel" runat="server" Font-Bold="True" Text='<%# Eval("nombre_gra") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                                &nbsp;Mención</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="mencion_GPrLabel" runat="server" Text='<%# Eval("mencion_GPr") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                                &nbsp;Universidad</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="nombre_insLabel" runat="server" Text='<%# Eval("nombre_ins") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;Situación</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_SitLabel" runat="server" Text='<%# Eval("descripcion_Sit") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;Fecha de Estudios</td>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        Ingreso :
                                                        <asp:Label ID="anioIngreso_GPrLabel" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            ForeColor="Navy" Text='<%# Eval("anioIngreso_GPr") %>'></asp:Label>
                                                        Egreso :
                                                        <asp:Label ID="anioEgreso_GPrLabel" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                            ForeColor="Navy" Text='<%# Eval("anioEgreso_GPr") %>'></asp:Label>
                                                                Graduacion :<asp:Label ID="anioGrad_GPrLabel" runat="server" Font-Names="Verdana"
                                                            Font-Size="8pt" ForeColor="Navy" Text='<%# Eval("anioGrad_GPr") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" style="font-size: 9pt; color: maroon; font-family: verdana">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <strong style="font-weight: bold; font-size: 8pt; text-transform: uppercase; color: maroon;
                                                direction: ltr; font-family: verdana; text-align: left"></strong>
                                        </HeaderTemplate>
                                    </asp:DataList><asp:Label ID="LblTitulos" runat="server" Font-Bold="True" ForeColor="Black"
                                        Text="TITULOS PROFESIONALES"></asp:Label>
                                    <asp:DataList ID="DatTitulos" runat="server" DataSourceID="ObjTitulos" Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Titulo Profesional</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_tpfLabel" runat="server" Font-Bold="True" Text='<%# Eval("descripcion_tpf") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;Institución</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="nombre_InsLabel" runat="server" Text='<%# Eval("nombre_Ins") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;Situación</td>
                                                    <td align="left" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_SitLabel" runat="server" Text='<%# Eval("descripcion_Sit") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;Fecha Estudios</td>
                                                    <td align="left" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        Ingreso :
                                                        <asp:Label ID="anioIngreso_TPrLabel" runat="server" ForeColor="Navy" Text='<%# Eval("anioIngreso_TPr") %>'></asp:Label>
                                                        Egreso :
                                                        <asp:Label ID="anioEgreso_TPrLabel" runat="server" ForeColor="Navy" Text='<%# Eval("anioEgreso_TPr") %>'></asp:Label>
                                                                Titulación :
                                                        <asp:Label ID="anioGrad_TPrLabel" runat="server" ForeColor="Navy" Text='<%# Eval("anioGrad_TPr") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td align="left" colspan="2" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label ID="LblActualizaciones" runat="server" Font-Bold="True" Font-Size="X-Small"
                                        Font-Underline="True" ForeColor="Black" Text="ACTUALIZACIONES Y OTROS CURSOS"></asp:Label>
                                    <asp:DataList ID="DatOtros" runat="server" DataSourceID="ObjOtros" Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" width="100%">
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana; font-variant: normal">
                                                                &nbsp;Estudio Realizado</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="estudiosLabel" runat="server" Font-Bold="True" Text='<%# Eval("estudios") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana; font-variant: normal">
                                                                &nbsp;Area de Estudios</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="Des_AreaEsLabel" runat="server" Text='<%# Eval("Des_AreaEs") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana; font-variant: normal">
                                                        &nbsp;Institución</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="centroestudiosLabel" runat="server" Text='<%# Eval("centroestudios") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana; font-variant: normal">
                                                        &nbsp;Fecha Estudios</td>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        Inicio :
                                                        <asp:Label ID="OtrosinicioLabel" runat="server" ForeColor="Navy" Text='<%# Eval("Otrosinicio") %>'></asp:Label>
                                                        Termino :
                                                        <asp:Label ID="OtrosFinLabel" runat="server" ForeColor="Navy" Text='<%# Eval("OtrosFin") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana; font-variant: normal">
                                                                &nbsp;Modalidad</td>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        <asp:Label ID="modalidadLabel" runat="server" ForeColor="Navy" Text='<%# Eval("modalidad") %>'></asp:Label>
                                                                - Ciclo Actual :
                                                        <asp:Label ID="CicloActualLabel" runat="server" ForeColor="Navy" Text='<%# Eval("CicloActual") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: 8pt; color: maroon; font-family: verdana; font-variant: normal">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label ID="LblExperiencia" runat="server" Font-Bold="True" Font-Size="X-Small"
                                        Font-Underline="True" ForeColor="Black" Text="EXPERIENCIA PROFESIONAL Y LABORAL"></asp:Label>
                                    <asp:DataList ID="DatExperiencia" runat="server" DataSourceID="ObjExperiencia" Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" style="width: 100%">
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Cargo</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_CarLabel" runat="server" Font-Bold="True" Text='<%# Eval("descripcion_Car") %>'></asp:Label>
                                                        -
                                                        <asp:Label ID="Funcion_ExpLabel" runat="server" Font-Bold="True" Text='<%# Eval("Funcion_Exp") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Empresa/Institución</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="empresaLabel" runat="server" Font-Bold="True" Text='<%# Eval("empresa") %>'></asp:Label>
                                                        -
                                                        <asp:Label ID="ciudadLabel" runat="server" Font-Bold="True" Text='<%# Eval("ciudad") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Descripción del cargo</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="Descripcion_EXpLabel" runat="server" Text='<%# Eval("Descripcion_EXp") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Tipo Contrato</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_TcoLabel" runat="server" Text='<%# Eval("descripcion_Tco") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Fecha de Labores</td>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                        Inicio :
                                                        <asp:Label ID="inicioLabel" runat="server" ForeColor="Navy" Text='<%# Eval("inicio") %>'></asp:Label>&nbsp;
                                                        Termino :
                                                        <asp:Label ID="finLabel" runat="server" ForeColor="Navy" Text='<%# Eval("fin") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Cese por</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="motivoceseLabel" runat="server" Text='<%# Eval("motivocese") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" rowspan="1">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label ID="LblInvestigacion" runat="server" Font-Bold="True" Font-Italic="False"
                                        Font-Size="X-Small" Font-Underline="True" ForeColor="Black" Text="PRODUCCIÓN ACADÉMICA E INVESTIGACIÓN"></asp:Label><asp:DataList
                                            ID="DatInvestigacion" runat="server" DataSourceID="ObjInvestigaciones" Width="100%">
                                            <ItemTemplate>
                                                <table cellpadding="1" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                            &nbsp;Investigación</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="descripcion_CarLabel" runat="server" Font-Bold="True" Text='<%# Eval("titulo_inv") %>'></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                            &nbsp;Fecha Inicio</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="empresaLabel" runat="server" Font-Bold="False" Text='<%# Format(Eval("fechainicio_inv"),"dd-MM-yyyy") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                            &nbsp;Fecha Termino</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="Descripcion_EXpLabel" runat="server" Text='<%# Format(Eval("fechafin_inv"),"dd-MM-yyyy") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                            &nbsp;Estado</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="descripcion_TcoLabel" runat="server" Text='<%# Eval("descripcion_Ein") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                            &nbsp;Tipo Investigación</td>
                                                        <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                            <asp:Label ID="inicioLabel" runat="server" ForeColor="Navy" Text='<%# Eval("descripcion_tin") %>'></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    <asp:Label ID="LblSeminarios" runat="server" Font-Bold="True" Font-Size="X-Small"
                                        Font-Underline="True" ForeColor="Black" Text="ASISTENCIA A SEMINARIOS - TALLERES - CONGRESOS"></asp:Label><asp:DataList
                                            ID="DatSeminarios" runat="server" DataSourceID="ObjEventos" Width="100%">
                                            <ItemTemplate>
                                                <table cellpadding="1" cellspacing="0" style="width: 100%">
                                                    <tr>
                                                        <td colspan="2" style="font-size: 8pt; color: maroon; font-family: verdana">
                                                            &nbsp;<asp:Label ID="TipoeventoLabel" runat="server" Font-Bold="True" Text='<%# Eval("Tipoevento") %>'></asp:Label>
                                                            -
                                                            <asp:Label ID="descripcion_tevLabel" runat="server" Font-Bold="True" Text='<%# Eval("descripcion_tev") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 80px; color: maroon; font-family: verdana">
                                                                &nbsp;Nombre</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="descripcionLabel" runat="server" Font-Bold="True" Text='<%# Eval("descripcion") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 160px; color: maroon; font-family: verdana">
                                                                &nbsp;Organizado por</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="organizadoLabel" runat="server" Text='<%# Eval("organizado") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                            &nbsp;Tipo Participacion</td>
                                                        <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                            <asp:Label ID="descripcion_tpaLabel" runat="server" Text='<%# Eval("descripcion_tpa") %>'></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size: 8pt; width: 80px; color: maroon; font-family: verdana">
                                                                &nbsp;Fechas</td>
                                                        <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                            Inicio :
                                                            <asp:Label ID="inicioLabel" runat="server" ForeColor="Navy" Text='<%# format(Eval("inicio"),"dd-MM-yyyy") %>'></asp:Label>
                                                            Termino
                                                            <asp:Label ID="finLabel" runat="server" ForeColor="Navy" Text='<%# format(Eval("fin"),"dd-MM-yyyy") %>'></asp:Label></td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    <asp:Label ID="LblDistinciones" runat="server" Font-Bold="True" Font-Size="X-Small"
                                        Font-Underline="True" ForeColor="Black" Text="DISTINCIONES Y HONORES"></asp:Label>
                                    <asp:DataList ID="DatDistinciones" runat="server" DataKeyField="codigo_dis" DataSourceID="ObjDistinciones"
                                        Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" style="font-size: 8pt; color: maroon; font-family: verdana"
                                                width="100%">
                                                <tr>
                                                    <td style="width: 160px; font-weight: bold; color: maroon;">
                                                                &nbsp;Distinción
                                                    </td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana; font-weight: bold;">
                                                        <asp:Label ID="descripcion_tdisLabel" runat="server" Text='<%# Eval("descripcion_tdis") %>'></asp:Label>
                                                        -
                                                        <asp:Label ID="nombre_disLabel" runat="server" Text='<%# Eval("nombre_dis") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                                &nbsp;Otorgado por</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="otorgado_disLabel" runat="server" Text='<%# Eval("otorgado_dis") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon;">
                                                                &nbsp;Ciudad</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana;">
                                                        <asp:Label ID="ciudad_disLabel" runat="server" Text='<%# Eval("ciudad_dis") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                        &nbsp;Fecha de Entrega</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="fechaentrega_disLabel" runat="server" Text='<%# format(Eval("fechaentrega_dis"),"dd-MM-yyyy") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                                &nbsp;Motivo de Entrega</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="motivo_disLabel" runat="server" Text='<%# Eval("motivo_dis") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:Label ID="LblIdiomas" runat="server" Font-Bold="True" Font-Size="X-Small" Font-Underline="True"
                                        ForeColor="Black" Text="IDIOMAS EXTRANJEROS"></asp:Label>
                                    <asp:DataList ID="DatIdiomas" runat="server" DataSourceID="ObjIdiomas" Width="100%">
                                        <ItemTemplate>
                                            <table cellpadding="1" cellspacing="0" style="font-size: 8pt; color: maroon; font-family: verdana"
                                                width="100%">
                                                <tr>
                                                    <td style="width: 160px; font-weight: bold; color: maroon;">
                                                                &nbsp;Idioma</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="descripcion_IdiLabel" runat="server" Style="font-weight: bold; text-transform: uppercase"
                                                            Text='<%# Eval("descripcion_Idi") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                        &nbsp;Institución</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="nombre_insLabel" runat="server" Text='<%# Eval("nombre_ins") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                                &nbsp;Nivel de idioma</td>
                                                    <td style="font-size: 8pt; color: maroon; font-family: verdana">
                                                                Lee :
                                                        <asp:Label ID="leeLabel" runat="server" Style="color: navy" Text='<%# Eval("lee") %>'></asp:Label>
                                                                &nbsp; &nbsp; Habla :
                                                        <asp:Label ID="hablaLabel" runat="server" Style="color: navy" Text='<%# Eval("habla") %>'></asp:Label>
                                                                &nbsp; &nbsp; Escribe :
                                                        <asp:Label ID="escribeLabel" runat="server" Style="color: navy" Text='<%# Eval("escribe") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td style="color: maroon">
                                                                &nbsp;Graduación</td>
                                                    <td style="font-size: 8pt; color: navy; font-family: verdana">
                                                        <asp:Label ID="aniograduacionLabel" runat="server" Text='<%# Eval("aniograduacion") %>'></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="font-size: 8pt; color: navy; font-family: verdana">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList></td>
                            </tr>
                        </tbody>
                    </table>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    <asp:ObjectDataSource ID="Grados" runat="server" SelectMethod="ObtieneDatosGrados"
                                                TypeName="Personal">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="String" />
                                                    <asp:Parameter Name="tipo" DefaultValue="GR" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjOtros" runat="server" SelectMethod="ObtieneDatosOtros"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="String" />
                <asp:Parameter DefaultValue="PE" Name="tipo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjEventos" runat="server" SelectMethod="ObtieneDatosEventos"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="Object" />
                <asp:Parameter DefaultValue="PR" Name="tipo" Type="String" />
                <asp:Parameter DefaultValue="&quot;&quot;" Name="param2" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjTitulos" runat="server" SelectMethod="ObtieneDatosTitulos"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="String" />
                <asp:Parameter Name="tipo" Type="String" DefaultValue="TI" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjExperiencia" runat="server" SelectMethod="ObtieneDatosExperiencia"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="Object" />
                 <asp:Parameter Name="tipo" DefaultValue="PE" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjIdiomas" runat="server" SelectMethod="ObtieneDatosIdiomas"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="idPersonal" QueryStringField="codigo_per" Type="String" />
                <asp:Parameter DefaultValue="DO" Name="tipo" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjDistinciones" runat="server" SelectMethod="ObtieneDistinciones"
                                                TypeName="Personal">
                                                <SelectParameters>
                                                    <asp:QueryStringParameter Name="idpersonal" QueryStringField="codigo_per" Type="Int32" />
                                                    <asp:Parameter DefaultValue="DP" Name="tipo" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjInvestigaciones" runat="server" SelectMethod="ObtieneInvestigaciones"
            TypeName="Personal">
            <SelectParameters>
                <asp:QueryStringParameter Name="codigo_per" QueryStringField="codigo_per" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </form>
</body>
</html>
