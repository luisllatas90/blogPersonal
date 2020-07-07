<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detallenotasmodulo.aspx.vb" Inherits="detallenotasmodulo" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Acta de Registro de Notas por Módulo</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Acta de Registro de Notas</p>   
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:70%">                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Datos del Módulo</td>
                                            <td align="right">
                                                <input id="cmdImprimir" class="imprimir2" title="Imprimir" type="button" 
                                                    value="Imprimir" onclick="window.print()" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Programa</td>
                                            <td colspan="3" style="width: 85%">
                                                :
                                                <asp:Label ID="lblDescripcion_pes" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Denominación</td>
                                            <td colspan="3" style="width: 85%">
                                                :
                                                <asp:Label ID="lblnombre_cur" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Profesor</td>
                                            <td colspan="3" style="width: 85%">
                                                :
                                                <asp:Label ID="lblProfesor" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Fecha Inicio</td>
                                            <td style="width: 17%">
                                                :
                                                <asp:Label ID="lblFechaInicio" runat="server" Font-Bold="True" 
                                                    ForeColor="Green"></asp:Label>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Fecha Fin:</td>
                                            <td style="width: 62%">
                                                <asp:Label ID="lblFechaFin" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Estado</td>
                                            <td colspan="3">
                                                :
                                                <asp:Label ID="lblEstadoNota" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                                            </td>
                                        </tr>
                                        </table>
    <br />

                                                <asp:GridView ID="grwParticipantes" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_alu,codigo_dma" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2" 
                                                    Width="70%">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" 
                                                        HorizontalAlign="Center" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nro.">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" />
                                                        <asp:BoundField DataField="participante" HeaderText="Apellidos y Nombres" ItemStyle-HorizontalAlign="Left">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="notafinal_dma" HeaderText="Nota Final" >
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="estado_dma" HeaderText="Estado" >
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="condicion_dma" HeaderText="Condición">
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se encontraron participantes matriculados en el curso
                                                    </EmptyDataTemplate>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>

                                        <br />
    <asp:Label ID="lblA" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="9pt" ForeColor="Blue"></asp:Label>
&nbsp;|
    <asp:Label ID="lblD" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="9pt" ForeColor="Red"></asp:Label>
&nbsp;|
    <asp:Label ID="lblP" runat="server" Font-Bold="True" Font-Names="Verdana" 
        Font-Size="9pt" ForeColor="Green"></asp:Label>
    
    
    </form>
</body>
</html>
