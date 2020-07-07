<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consultarnoadeudo.aspx.vb" Inherits="noadeudo_consultarnoadeudo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../private/estilo.css"  rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../../../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table align="center" style="width: 100%;">
        <tr>
            <td align="center" class="usatTitulo" colspan="2">
                Consultar y Evaluar solicitud de constancia de no adeudos</td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="cmdNuevo" runat="server" Text="Nuevo" CssClass="guardar" 
                    Height="29px" Width="110px" />
            &nbsp;<asp:Button ID="cmdFinalizar" runat="server" Text="Finalizar" 
                    CssClass="cerrar" Height="29px" Width="110px" Enabled="False" />
            </td>
            <td align="right">
                Consultar por estado:&nbsp;&nbsp;
                <asp:DropDownList ID="ddlEstadoDirAcad" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="P">Pendiente</asp:ListItem>
                    <asp:ListItem Value="F">Finalizada</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlEstadoRevisor" runat="server" AutoPostBack="True">
                    <asp:ListItem Value="P">Pendiente</asp:ListItem>
                    <asp:ListItem Value="O">Observado</asp:ListItem>
                    <asp:ListItem Value="C">Conforme</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr class="aprobar" noshade="noshade" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:GridView ID="gvdetalle" runat="server" CellPadding="4" ForeColor="#333333" 
                    GridLines="None" 
                    EmptyDataText="No se encontraron solicitudes de no adeudos en el estado seleccionado">
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="2">
&nbsp;&nbsp;
                &nbsp;&nbsp;&nbsp;
                <asp:Panel ID="pnlEvaluar" runat="server">
                    <hr class="aprobar" noshade="noshade" />
                    <br />
                    <asp:Label ID="Label3" runat="server" CssClass="usattitulousuario" 
                    Text="Evaluar Solicidud "></asp:Label>
                    <asp:Label ID="lblNroSol" runat="server" CssClass="usattitulousuario" 
                        Font-Size="Medium"></asp:Label>
&nbsp;
                    <asp:RadioButton ID="rbConforme" runat="server" AutoPostBack="True" 
                        GroupName="evaluacion" Text="Conforme" ValidationGroup="evaluacion" 
                        Enabled="False" />
                    <asp:RadioButton ID="rbObservado" runat="server" AutoPostBack="True" 
                        GroupName="evaluacion" Text="Observado" ValidationGroup="evaluacion" 
                        Enabled="False" />
                    &nbsp;&nbsp;&nbsp; Obs.
                    <asp:TextBox ID="txtObservacion" runat="server" Width="272px"></asp:TextBox>
                    &nbsp;&nbsp;
                    <asp:Button ID="cmdEvaluar" runat="server" Text="Evaluar" />
                    &nbsp;<asp:Label ID="lblValidaEvaluacion" runat="server" Font-Bold="False" 
                        Font-Size="Small" ForeColor="Red" Text="** Ingrese una observación" 
                        Visible="False"></asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr class="aprobar" noshade="noshade" />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <asp:GridView ID="gvRevision" runat="server" CellPadding="4" ForeColor="#990000" 
                    GridLines="None" EmptyDataText="No se cuenta con revisiones" 
                    Caption="Revisiones" Font-Bold="False" Font-Size="Medium">
                    <RowStyle BackColor="#EFF3FB" HorizontalAlign="Left" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br />
                <asp:Button ID="cmdImprimir" runat="server" Text="Imprimir" 
                    CssClass="imprimir2" Height="29px" Width="110px" Enabled="False" />
            </td>
        </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
