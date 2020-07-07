<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptenotaspec.aspx.vb" Inherits="rptenotaspec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reporte de Notas por Programa</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Reporte de Notas por Programa</p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                Programa:
                <asp:DropDownList ID="dpPrograma" runat="server" Width="70%" 
                    AutoPostBack="True">
                </asp:DropDownList>
                                            &nbsp;<input id="cmdImprimir" class="imprimir2" 
                    title="Imprimir" type="button" 
                                                    value="Imprimir" 
                    onclick="window.print()" /></td>
        </tr>
        </table>
                                    <br />
                                                <asp:GridView ID="grwModulosPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2" ShowFooter="True">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" 
                                                        HorizontalAlign="Center" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ciclo_cur" HeaderText="Nro" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:HyperLinkField DataNavigateUrlFields="codigo_pec,codigo_cup" 
                                                            DataNavigateUrlFormatString="detallenotasmodulo.aspx?p={0}&amp;c={1}" 
                                                            DataTextField="nombre_cur" HeaderText="Módulo" Target="_blank">
                                                        <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                                        <ItemStyle Font-Underline="False" HorizontalAlign="Left" Width="45%" />
                                                        </asp:HyperLinkField>
                                                        <asp:BoundField DataField="creditos_cur" HeaderText="Crd." >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="horasteo_cur" HeaderText="HT" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="horaspra_cur" HeaderText="HI" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="horasase_cur" HeaderText="HA" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="horaslab_cur" HeaderText="HEP" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="totalhoras_cur" HeaderText="TH" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="aprobados" HeaderText="Aprob.">
                                                        <ItemStyle Font-Bold="False" ForeColor="Blue" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="desaprobados" HeaderText="Desaprob.">
                                                        <ItemStyle Font-Bold="False" ForeColor="Red" Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pendientes" HeaderText="Pend.">
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Inscritos" HeaderText="Inscritos">
                                                        <ItemStyle Width="5%" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se han encontrado cursos o módulos registrados.
                                                    </EmptyDataTemplate>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" 
                                                        Font-Bold="True" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
    </form>
</body>
</html>

