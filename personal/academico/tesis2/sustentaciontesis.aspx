<%@ Page Language="VB" AutoEventWireup="false" CodeFile="sustentaciontesis.aspx.vb" Inherits="sustentaciontesis" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Evaluación de tesis</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Sustentaciones de tesis<p/>
    <table width="100%" cellpadding="3">
        <tr>
            <td style="width:20%" align="right">
                Estado:</td>
            <td style="width:80%">
                <asp:DropDownList ID="DropDownList1" runat="server">
                    <asp:ListItem Value="0">Por sustentar</asp:ListItem>
                    <asp:ListItem Value="1">En revisión de Jurado</asp:ListItem>
                    <asp:ListItem>En proceso de correcciones finales</asp:ListItem>
                    <asp:ListItem>Sustentadas</asp:ListItem>
                </asp:DropDownList>
                    </td>
        </tr>
        <tr>
            <td colspan="2" class="contornotabla_azul" bgcolor="#FFCC66">
                Listado de investigaciones</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundField DataField="nro" HeaderText="#" />
                        <asp:BoundField DataField="fechareg" HeaderText="Fecha Registro" />
                        <asp:BoundField DataField="codigo" HeaderText="Código Inv." />
                        <asp:BoundField DataField="titulo" HeaderText="Titulo de investigación" />
                        <asp:BoundField DataField="autor" HeaderText="Autor" />
                        <asp:BoundField DataField="asesor" HeaderText="Asesor (es)" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="fase" HeaderText="Fase" />
                    </Columns>
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                <table style="width:100%; border-collapse:collapse" cellpadding="3" cellspacing="0" border="1">
                    <tr style="background-color:#5D7B9D;color:White">
                        <th >#</th>
                        <th >
                            Fecha de registro</th>
                        <th >
                            Código Inv</th>
                        <th >
                            Título de Investigación</th>
                        <th >
                            Autor</th>
                        <th >
                            Asesor</th>
                        <th >
                            Jurado</th>
                        <th >
                            Calificativo</th>
                    </tr>
                    <tr class="selected">
                        <td>
                           1</td>
                        <td>
                            <%=Date.Today%></td>
                        <td>
                            <%=Session("codigo")%>&nbsp;</td>
                        <td>
                            <%=Session("titulo")%>&nbsp;</td>
                        <td>
                            <%=Session("autor")%>&nbsp;</td>
                        <td>
                            <%=Session("asesor")%>&nbsp;</td>
                        <td>
                            <%=Session("estado")%>&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>      
        </table>
        <fieldset style="padding: 2">
<legend><b>Registrar acciones</b></legend>
                <asp:Button ID="cmdNotaProyecto" runat="server" 
                    onclientclick="AbrirPopUp('frmjurado.aspx','350','550');return(false)" 
                    Text="Proponer Jurado" CausesValidation="False" 
                    UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cmdNotaEjecucion" runat="server" 
                    onclientclick="AbrirPopUp('frmregsustentacion.aspx','350','550');return(false)" 
                    Text="Tramitar Sustentación" CausesValidation="False" 
                    UseSubmitBehavior="False" />
                    &nbsp;<asp:Button ID="cmdNotaInforme" runat="server" 
                    onclientclick="AbrirPopUp('frmcalificarsustentacion.aspx','350','550');return(false)" 
                    Text="Calificar Sustentación" CausesValidation="False" 
                    UseSubmitBehavior="False" />
&nbsp;<asp:Button ID="cmdNotaInforme0" runat="server" 
                    Text="Imprimir documento de sustentación" CausesValidation="False" 
                    UseSubmitBehavior="False" />
</fieldset>
        
    </form>
</body>
</html>
