<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptenotas.aspx.vb" Inherits="librerianet_academico_rptenotas" %>

<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Historial de Notas por ciclo</p>

        <table ID="tblDatos" border="0" bordercolor="#111111" cellpadding="3" 
                        cellspacing="0" class="contornotabla" width="100%">
                        <tr>
                            <td rowspan="6" valign="top" width="10%">
                                <asp:Image ID="FotoAlumno" runat="server" Height="104px" Width="90px" Visible="False" />
                            </td>
                            <td width="15%">
                                Código Universitario</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;
                                <asp:TextBox ID="txtcodigo" runat="server" MaxLength="14"></asp:TextBox>
                                <asp:Button ID="cmdBuscar" runat="server" Text="Buscar" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="txtcodigo" Display="Dynamic" 
                                    ErrorMessage="Debe ingresar el código universitario">*</asp:RequiredFieldValidator>
                                <asp:Button ID="cmdExportar" runat="server" Text="Imprimir" Visible="False" 
                                    onclientclick="imprimir('N','','');return(false)" UseSubmitBehavior="False" />
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Apellidos y Nombres
                            </td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblalumno" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Carrera Profesional</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblescuela" runat="server"></asp:Label>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Semestre de Ingreso</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblcicloingreso" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                Plan de Estudio</td>
                            <td class="usatsubtitulousuario" width="70%">
                                &nbsp;<asp:Label ID="lblPlan" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="width: 85%" width="15%">
                                <asp:Label 
                                                ID="lblMensaje" runat="server" Font-Bold="True" 
                                    Font-Size="10pt" ForeColor="Red"></asp:Label>
                                    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" BackColor="White" 
                                    Overlay="False" Text="Se está procesando su información" 
                                    Title="Por favor espere" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                        ShowMessageBox="True" ShowSummary="False" />
                            </td>
                        </tr>
                    </table>
            
    <br />         
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" BorderColor="#CCCCCC" BorderStyle="Solid" 
        BorderWidth="1px" ShowFooter="True" Width="100%" EnableViewState="False" 
                EmptyDataText="&amp;nbsp;&amp;nbsp;&amp;nbsp;&amp;nbsp;No se han encontrado asignaturas matriculadas aprobadas.">
        <EmptyDataRowStyle CssClass="usatSugerencia" />
        <Columns>
            <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="identificador_cur" HeaderText="Código" />
            <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura" />
            <asp:BoundField DataField="creditoCur_Dma" HeaderText="Créditos" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="notafinal_dma" HeaderText="Promedio" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="descripcion_cac" HeaderText="Semestre" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <FooterStyle 
            HorizontalAlign="Center" BackColor="#e8eef7" Font-Bold="True" ForeColor="#3366CC" />
        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
            BorderStyle="Solid" BorderWidth="1px" />
    </asp:GridView>
    </form>

    </body>
</html>
