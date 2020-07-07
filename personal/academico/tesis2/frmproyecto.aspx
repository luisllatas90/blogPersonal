<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmproyecto.aspx.vb" Inherits="frmproyecto" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro de Proyecto</title>
    <script type="text/javascript" language="JavaScript" src="private/PopCalendar.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript">
        function ElegirAutor(id)
        {
            form1.txtAutor.value=id
        }
        
        function ElegirAsesor(id)
        {
            form1.txtAsesor.value=form1.txtAsesor.value + ' / ' + id
        }
        
    </script>
    <style type="text/css">
        .lineatitulo
        {
            border-bottom-style: solid;
            border-width: 1px;
            border-color: #FFCC00;
            font-family: Tahoma;
            font-size: 12px;
            font-weight: bold;
            color: #333333;
        }
    </style>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <%Response.Write(ClsFunciones.CargaCalendario)%>
            <fieldset style="padding: 2;">
<legend class="usatTitulo"><b>Registrar investigación</b></legend>
                    <table cellpadding="3" cellspacing="0" width="100%">
        <tr>
            <td colspan="5" class="LineaTitulo">
                <b>Datos informativos</b></td>
        </tr>
        <tr>
            <td>
                Código</td>
            <td>
                <asp:TextBox ID="txtcodigo" runat="server"></asp:TextBox>
            </td>
            <td>
                Fase</td>
            <td>
                <asp:DropDownList ID="dpFase" runat="server">
                    <asp:ListItem Value="0">Tema de investigación</asp:ListItem>
                    <asp:ListItem Value="1">Proyecto de investigación</asp:ListItem>
                    <asp:ListItem Value="2">Ejecución de investigación</asp:ListItem>
                    <asp:ListItem Value="3">Informe de tesis</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                ESTADO:
                <asp:Label ID="lblestado" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel1" runat="server" Visible="False" >
                    <table width="100%">
                        <tr>
                            <td>
                                <b>Título</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtTitulo" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Resumen</b></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtResumen" runat="server" CssClass="cajas2" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td>
                Fecha inicio</td>
            <td>
                <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
                </asp:TextBox>
                <asp:Button ID="Button2" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaInicio,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
            </td>
            <td>
                Fecha Fin</td>
            <td colspan="2">
                <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
                <asp:Button ID="Button3" runat="server" 
                    onclientclick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFechaFin,'dd/mm/yyyy');return(false)" 
                    Text="..." CausesValidation="False" UseSubmitBehavior="False" />
                    </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" class="lineatitulo">
                <b>Autor</b></td>
            <td align="right" class="lineatitulo" colspan="2">
                <asp:Button ID="Button4" runat="server" 
                    onclientclick="AbrirPopUp('buscarestudiante.aspx','400','550');return(false)" 
                    Text="Agregar" CausesValidation="False" UseSubmitBehavior="False" />
                    </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:BoundField HeaderText="Código" />
                        <asp:BoundField HeaderText="Apellidos y Nombres" />
                        <asp:BoundField HeaderText="Ciclo de Ingreso" />
                        <asp:BoundField HeaderText="Escuela Profesional" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:TextBox ID="txtAutor" runat="server" CssClass="cajas2"></asp:TextBox>
                <br />
                </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3" class="lineatitulo">
                <b>Asesor (es)</b></td>
            <td align="right" class="lineatitulo" colspan="2">
                <asp:Button ID="Button5" runat="server" 
                    onclientclick="AbrirPopUp('buscarasesor.aspx','400','550');return(false)" 
                    Text="Agregar" CausesValidation="False" UseSubmitBehavior="False" />
                    </td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField HeaderText="#" />
                        <asp:BoundField HeaderText="Apellidos y Nombres" />
                        <asp:BoundField HeaderText="Función" />
                        <asp:BoundField HeaderText="Institución" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <asp:TextBox ID="txtAsesor" runat="server" CssClass="cajas2" 
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="5">
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <table width="100%">
                        <tr>
                            <td class="lineatitulo">
                                <b>Temática</b></td>
                            <td align="right" class="lineatitulo">
                                <asp:Button ID="Button6" runat="server" CausesValidation="False" 
                                    onclientclick="AbrirPopUp('buscarasesor.aspx','400','550');return(false)" 
                                    Text="Agregar" UseSubmitBehavior="False" Visible="False" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
                                    CellPadding="4" ForeColor="#333333">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField HeaderText="#" />
                                        <asp:BoundField HeaderText="Descripción de temática" />
                                    </Columns>
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:TextBox ID="txtTematica" runat="server" CssClass="cajas2" Height="50px" 
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
        </table>
</fieldset>         
        <p style="text-align:center">
            <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" Font-Bold="True" />&nbsp;
            <asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" Font-Bold="True" />
        </p>
    <p style="text-align:center">
            <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" Font-Size="12pt" 
                ForeColor="Red" Text="Se ha guardado correctamente" Visible="False"></asp:Label>
        </p>
    </form>
</body>
</html>
