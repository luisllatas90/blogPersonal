<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmcambiarasesor.aspx.vb" Inherits="frmcambiarasesor" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asesores de tesis</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form2" runat="server">
    
    <table width="100%">
        <tr>
            <td style="width:60%" class="usatTitulo">Actualización de asesores de tesis</td>
            <td style="width:40%">
            <table width="100%" cellpadding="3" cellspacing="0" style="border: 1px solid #507CD1; background-color: #eff3fb;">
             <tr>
                <td style=" width:20%"><b>Código</b></td>
                <td style=" width:65%">
                    <asp:TextBox ID="txtTermino" runat="server" 
                        CssClass="cajas2" MaxLength="20"></asp:TextBox></td>
                <td style=" width:10%"><asp:Button ID="cmdBuscar" runat="server" Text="    Buscar" CssClass="buscar_prp_small" /></td>
            </tr>
            </table>
            </td>
        </tr>
    </table>
        <br/>
        <fieldset style="padding: 2; width:80%">
        <legend style="font-size: 13px; font-weight: bold; color: #003399; font-family: Verdana;">Datos de la Tesis</legend>
                <table cellpadding="3" cellspacing="0" width="100%">
                    <tr>
                        <td width="15%">
                            Código</td>
                        <td width="75%">
                &nbsp;<asp:Label ID="lblcodigo" runat="server" Font-Bold="True" Font-Names="Tahoma" 
                    Font-Size="14px" ForeColor="Maroon" Height="20px"></asp:Label>
                            <asp:HiddenField ID="hdCodigo_Tes" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            Fase</td>
                        <td width="75%">
                            <asp:Label ID="lblFase" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td width="15%">
                            Tesis</td>
                        <td width="75%">
                            <asp:Label ID="lblTitulo" runat="server" Font-Bold="True"></asp:Label>
                            <br />
                            <asp:Label ID="lblProblema" runat="server" 
                        Font-Italic="True"></asp:Label>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td width="15%" valign="top">
                            Autor (es)</td>
                        <td width="75%">
                            <asp:BulletedList ID="lstAutores" runat="server" DataTextField="alumno" 
                                DataValueField="codigo_Rtes">
                            </asp:BulletedList>
                        </td>
                    </tr>
                </table>
        </fieldset>
        <p class="usatTitulousuario">Registro de asesores de tesis</p>
                <asp:DataList ID="dlAsesores" runat="server" RepeatColumns="1" 
                    RepeatDirection="Horizontal" Width="100%" 
                    DataKeyField="codigo_Rtes" 
        OnDeleteCommand="dlAsesores_DeleteCommand">
                    <ItemTemplate>
                        <table width="100%" cellpadding="2" cellspacing="0" class="contornotabla">
                            <tr style="background-color: #f4f2ed">
                                <td width="100%" class="bordeinf" colspan="2">Registrado:
                                    <asp:Label ID="lblFecha" runat="server" Text='<%# eval("fechareg_Rtes") %>'></asp:Label>
                                    &nbsp;Operador:
                                    <asp:Label ID="lblOpRegistro" runat="server" 
                                        Text='<%#StrConv(eval("opRegistro"),3) %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" rowspan="6" width="13%">
                                    <asp:Image ID="FotoAlumno" runat="server" Height="104px" 
                                        ImageUrl='<%# eval("foto_per") %>' Width="90px" />
                                    <br />
                                </td>
                                <td width="87%">
                                    <asp:Label ID="lblasesor" runat="server" Font-Bold="True" ForeColor="#CC6600" 
                                        Text='<%# eval("docente") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblcategoria" runat="server" 
                                        Text='<%# eval("descripcion_tpe") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblDedicacion" runat="server" 
                                        Text='<%# eval("descripcion_ded") %>'></asp:Label>
                                    <asp:HiddenField ID="hdCodigoR_tes" runat="server" 
                                        Value='<%# eval("codigo_Rtes") %>' />
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblTipo" runat="server" Text='<%# eval("descripcion_tpi") %>'></asp:Label>
                                    &nbsp;<b>- Estado actual</b>: <asp:Label ID="lblEstado" runat="server" 
                                        Text='<%# eval("estado_Rtes") %>' Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblobs_Btes" runat="server" Text='<%# eval("obs_Btes") %>' 
                                        Visible='<%# iif(IsDBNull(eval("obs_Btes"))=true,false,true) %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td width="87%">
                                    <asp:Label ID="lblMotivo" runat="server" Text="Especifique el motivo:"></asp:Label>
                                    &nbsp;<asp:TextBox ID="txtMotivo" runat="server" MaxLength="100" Width="300px"></asp:TextBox>
                                    &nbsp;<asp:Button ID="cmdQuitar" runat="server" CausesValidation="False" 
                                        CommandName="delete" CssClass="eliminar2" Text="Quitar" Width="70px" />
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:DataList>
    </form>
</body>
</html>
