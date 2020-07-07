<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstpec.aspx.vb" Inherits="lstpec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programas de Educación Contínua: PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Reporte de general de Programas</p>
<table cellpadding="3" cellspacing="0" style="width: 100%; border:1px solid #96ACE7" 
            border="0">
        <tr style="background-color: #6694e3; color:White">
            <td style="height: 30px; ">
                &nbsp;<asp:Button ID="cmdExportar" runat="server" CssClass="excel2" Height="20px" 
                    Text="Exportar" Visible="False" />
            </td>
        </tr>
        </table>
    <br />
                                                <asp:GridView ID="grwPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_pec" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="#">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_tpec" HeaderText="Tipo">
                                                        <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="Denominación">
                                                        <ItemStyle Font-Size="7pt" Font-Underline="True" ForeColor="#3333FF" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_pec" DataFormatString="{0:d}" 
                                                            HeaderText="Inicio">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_pec" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="version_pec" HeaderText="Edición">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="coordinador" HeaderText="Coordinador">
                                                        <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nroresolucion_pec" HeaderText="Nro. Resolución" />
                                                        <asp:HyperLinkField DataNavigateUrlFields="codigo_pec" 
                                                            DataNavigateUrlFormatString="lstmatriculadospec.aspx?id={0}" 
                                                            DataTextField="total_mat" HeaderText="Matriculados" Target="_self">
                                                            <ControlStyle Font-Underline="True" ForeColor="#3333FF" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" HorizontalAlign="Center" />
                                                        </asp:HyperLinkField>
                                                        <asp:BoundField DataField="total_cur" HeaderText="Módulos" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_epec" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No han encontrado Programas registrados asociados a los Centros de Costos que 
                                                        tiene configurado en Presupuestos.
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
    </form>
</body>
</html>

