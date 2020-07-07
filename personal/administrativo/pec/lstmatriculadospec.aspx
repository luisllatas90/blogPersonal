<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstmatriculadospec.aspx.vb" Inherits="pec_lstmatriculadospec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de Matriculados PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Lista de participantes matriculados en el Programa</p>
    
    
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                &nbsp;</td>
                                            <td align="right">
                                                <input id="cmdRegresar" class="regresar2" title="Regresar" type="button" 
                                                    value="Regresar" onclick="history.back(-1)" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">Tipo</td>
                                            <td><asp:Label ID="lbldescripcion_tpec" runat="server" Font-Bold="True" 
                                                    ForeColor="#006600"></asp:Label></td>
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
                                                &nbsp;</td>
                                            <td colspan="3">
                                                &nbsp;</td>
                                        </tr>
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td colspan="2" >Lista de participantes Matriculados</td>
                                            <td colspan="2" align="right">
                <asp:Button ID="cmdExportar" runat="server" CssClass="excel2" Height="20px" 
                    Text="Exportar" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="grwParticipantes" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_alu" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nro" />
                                                        <asp:BoundField DataField="codigouniver_alu" HeaderText="Código" />
                                                        <asp:BoundField DataField="participante" HeaderText="Apellidos y Nombres" />
                                                        <asp:BoundField DataField="sexo_alu" HeaderText="Sexo" />
                                                        <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI" />
                                                        <asp:BoundField DataField="email_alu" HeaderText="Email 1" />
                                                        <asp:BoundField DataField="email2_alu" HeaderText="Email 2" />
                                                        <asp:BoundField DataField="fechaNacimiento_Alu" HeaderText="Fecha Nac." 
                                                            DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="direccion" HeaderText="Dirección" />
                                                        <asp:BoundField DataField="telefonoFam_Dal" HeaderText="Teléfono" />
                                                        <asp:BoundField DataField="telefonoMovil_Dal" HeaderText="Celular" />
                                                        <asp:BoundField DataField="password_alu" HeaderText="Clave" />
                                                    </Columns>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                </td>
                                        </tr>
                                    </table>
    </form>
</body>
</html>
