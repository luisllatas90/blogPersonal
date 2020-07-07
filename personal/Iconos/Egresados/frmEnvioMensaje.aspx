<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEnvioMensaje.aspx.vb" Inherits="Egresado_frmEnvioMensaje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
        <tr>
            <td style="width:15%; height:32px">Para:</td>
            <td>                           
                <asp:Label ID="lblDestino" runat="server" Text="" Width="98%" Height="20px" ></asp:Label><br />
                <asp:CheckBox ID="chkEnvio" runat="server" Text="Enviar a Correo" />                
                <asp:CheckBox ID="chkObservado" runat="server" 
                    Text="Ficha de Egresado observada" AutoPostBack="True" />  
            </td>
        </tr>
        <tr>
            <td style="width:15%">Asunto:</td>
            <td><asp:TextBox ID="txtTitulo" runat="server" Width="98%"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:15%">Mensaje:</td>
            <td><asp:TextBox ID="txtDescripcion" runat="server" Width="98%" 
                    TextMode="MultiLine" Height="70px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width:15%"></td>
            <td style="height:22px">
                <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnAceptar" runat="server" Text="Guardar" Width="100px" Height="22px" CssClass="guardar" 
                     onclientclick="self.parent.tb_remove();" />
                <asp:Button ID="btnCancelar" runat="server" Text="Regresar" Width="100px" Height="22px" CssClass="salir" 
                    onclientclick="self.parent.tb_remove();" UseSubmitBehavior="False" />
            </td>
        </tr>
    </table>
    </div>
    <asp:HiddenField ID="HdCodigo_pso" runat="server" />
    </form>
</body>
</html>
