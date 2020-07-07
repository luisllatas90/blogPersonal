<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultarEstudiante_Acreditacion.aspx.vb" Inherits="Encuesta_ConsultarEstudiante_Acreditacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <script src="../../../private/funciones.js" type ="text/javascript" language="javascript"></script>
    <script type="text/javascript" language="javascript">
    function pintarcelda(celda)
    {
        celda.style.backgroundColor = '#FFFFC1'//'#FFFF99'//'#DFEFFF'
    }

    function despintarcelda(celda)  
    {
        celda.style.backgroundColor = ''
    }
    </script>
</head>
<body style="margin-top:0">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td colspan="4" class="usatTitulo">
                    Lista de estudiantes que fantan completar la encuesta</td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="RbtVer" runat="server" AutoPostBack="True" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">Faltantes: por llenar encuesta</asp:ListItem>
                        <asp:ListItem Value="1">Por estudiante</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td align="left" colspan="2">
                    <asp:DropDownList ID="CboTipoBusqueda" runat="server" AutoPostBack="True" 
                        Visible="False">
                        <asp:ListItem Value="0">Por apellidos y nombres</asp:ListItem>
                        <asp:ListItem Value="1">Por código universitario</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;<asp:TextBox ID="TxtTextoBusqueda" runat="server" Visible="False" Width="250px"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:Button ID="CmdBuscar" runat="server" CssClass="Buscar" Text="Buscar" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="4">
                    <hr />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="GvDatos" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            <asp:BoundField DataField="tipo" HeaderText="Estado Encuesta">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Codigo universitario">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombres" HeaderText="Apellidos y nombres">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera">
                                <ItemStyle Width="25%" />
                            </asp:BoundField>
                            <asp:ButtonField HeaderImageUrl="~/images/prop.gif" Text="Ir a ..." 
                                CausesValidation="True" DataTextField="codigo_alu" 
                                DataTextFormatString="&lt;a href='AcreditacionUniversitaria_generales.aspx?sesion={0}' &gt; Ir a ...&lt;/a&gt;"   />
                        </Columns>
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
