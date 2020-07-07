<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleprogramaec.aspx.vb" Inherits="librerianet_pec_detalleprogramaec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Detalle de PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Datos Informativos del Programa</p>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                &nbsp;</td>
                                            <td align="right">
                                                <input id="cmdRegresar" class="salir" title="Regresar" type="button" 
                                                    value="Regresar" onclick="history.back(-1)" />
                                                <input id="cmdImprimir" class="usatimprimir" title="Imprimir" type="button" 
                                                    value="Imprimir" onclick="window.print()" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Tipo</td>
                                            <td>
                                                <asp:Label ID="lbldescripcion_tpec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Edición:</td>
                                            <td>
                                                <asp:Label ID="lblversion_pec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Denominación</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblDescripcion_pes" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Resol. Aprob</td>
                                            <td>
                                                <asp:Label ID="lblnroresolucion_pec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Estado:</td>
                                            <td>
                                                <asp:Label ID="lbldescripcion_epec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Fecha Inicio</td>
                                            <td>
                                                <asp:Label ID="lblfechainicio_pec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                            <td align="right">
                                                Fecha Fin:</td>
                                            <td>
                                                <asp:Label ID="lblfechafin_pec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Horarios:</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblhorarios" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Horas totales:</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblhorastotales" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Centro Costos</td>
                                            <td colspan="3">
                                                <asp:Label ID="lbldescripcion_cco" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Responsable</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblcoordinador" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Registado</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblOperador" runat="server" Font-Bold="True" ForeColor="#006600"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                &nbsp;</td>
                                            <td colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td colspan="2" >Módulos del Programa</td>
                                            <td colspan="2">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="grwModulosPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2" ShowFooter="True">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Nro" />
                                                        <asp:BoundField DataField="nombre_cur" HeaderText="Denominación" />
                                                        <asp:BoundField DataField="creditos_cur" HeaderText="Crd." />
                                                        <asp:BoundField DataField="horasteo_cur" HeaderText="H. Teo." />
                                                        <asp:BoundField DataField="horaspra_cur" HeaderText="H. Inv." />
                                                        <asp:BoundField DataField="horasase_cur" HeaderText="H. Ases." />
                                                        <asp:BoundField DataField="horaslab_cur" HeaderText="H. Est. Per." />
                                                        <asp:BoundField DataField="totalhoras_cur" HeaderText="Total Hrs." />
                                                        <asp:BoundField DataField="aprobados" HeaderText="Aprob." />
                                                        <asp:BoundField DataField="desaprobados" HeaderText="Desaprob." />
                                                        <asp:BoundField HeaderText="Inscritos" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" Font-Bold="True" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                </td>
                                        </tr>
                                    </table>
    </form>
</body>
</html>
