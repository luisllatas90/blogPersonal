<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatosSolicitud.aspx.vb" Inherits="Equipo_DatosSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <link href ="../private/estilo.css" type ="text/css" rel="stylesheet" /> 
    <link href ="../private/estiloweb.css" type ="text/css" rel="stylesheet" /> 
</head>
<body style="background-color: #E3EBF9;">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="color: #004182; background-color:White" height="20">
                    <strong>DATOS DE LA SOLICITUD</strong></td>
            </tr>
            <tr>
                <td style="background-color: #004182; height: 1px;">
                   </td>
            </tr>
            <tr >
                <td style="height: 20px; width: 873px; background-color: #E3EBF9;">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="id_sol" DataSourceID="ConsultarDatosdeunaSolicitud"
                        RepeatColumns="1" Width="100%" Font-Bold="False">
                        <ItemTemplate>
                            <table style="font-family: Verdana;" border="0" cellpadding="1" 
                                cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 123px">
                                        Solicitud</td>
                                    <td style="width: 3px; height: 18px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="descripcion_solLabel" runat="server" Text='<%# Eval("descripcion_sol") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 123px">
                                        Prioridad</td>
                                    <td style="width: 3px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="prioridadLabel" runat="server" Text='<%# Eval("prioridad") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        Tipo de solicitud</td>
                                    <td class="style2">
                                        :</td>
                                    <td class="style3">
                                        <asp:Label ID="descripcion_tsolLabel" runat="server" Text='<%# Eval("descripcion_tsol") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 123px">
                                        Área que solicitó</td>
                                    <td style="width: 3px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="descripcion_ccoLabel" runat="server" Text='<%# Eval("descripcion_cco") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 123px">
                                        Solicitada por</td>
                                    <td style="width: 3px; height: 18px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="descripcion_ccoLabel0" runat="server" 
                                            Text='<%# Eval("personal") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 123px">
                                        Módulo</td>
                                    <td style="width: 3px; height: 18px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="descripcion_aplLabel" runat="server" 
                                            Text='<%# Eval("descripcion_apl") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 123px">
                                        Fecha solicitada</td>
                                    <td style="width: 3px">
                                        :</td>
                                    <td>
                                        <asp:Label ID="fecha_solLabel" runat="server" Text='<%# Eval("fecha_sol") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 123px;">
                                        Registrada por</td>
                                    <td style="width: 3px; height: 20px;">
                                        :</td>
                                    <td>
                                        <asp:Label ID="registradopor_solLabel" runat="server" 
                                            Text='<%# Eval("registradopor_sol") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 123px;">
                                        Asignada a</td>
                                    <td style="width: 3px; height: 20px;">
                                        :</td>
                                    <td>
                                        <asp:Label ID="personalLabel" runat="server" 
                                            Text='<%# Eval("per_responsable") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 123px;">
                                        Observación</td>
                                    <td style="width: 3px; height: 20px;">
                                        :</td>
                                    <td>
                                        <asp:Label ID="observacion_solLabel" runat="server" Text='<%# Eval("observacion_sol") %>'></asp:Label></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                        <SeparatorStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Width="110px" Font-Size="Small" />
                        <SeparatorTemplate>
                            <br />
                        </SeparatorTemplate>
                        <ItemStyle Font-Size="Small" />
                    </asp:DataList>
                    <asp:SqlDataSource ID="ConsultarDatosdeunaSolicitud" runat="server" ConnectionString="<%$ ConnectionStrings:CnxBDUSAT %>"
                        SelectCommand="paReq_ConsultarSolicitud" 
                        SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="id_sol" QueryStringField="cod_sol" Type="Int32" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>  
    </form>
</body>
</html>
