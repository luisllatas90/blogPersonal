<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstavancetesis.aspx.vb" Inherits="vstavancetesis" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista de asesorías</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js">

</script>
<style type="text/css">
A:hover
{
	color: red;
	text-decoration:underline;
}
a:Link
{
    color: #0000FF;
    text-decoration: underline;
}
</style>
</head>
<body bgcolor="#eeeeee">
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td class="usattitulo" width="70%">
                <asp:Label ID="lbltitulo" runat="server"></asp:Label>
            </td>
            <td width="30%" align="right">
             <input id="Button1" class="imprimir3" type="button" value="  Imprimir" onclick="window.print()" />
             <asp:Button ID="CmdCancelar" runat="server" CssClass="cerrar3" Text="        Regresar" Height="25px" onclientclick="return(history.back(-1))" UseSubmitBehavior="False" />
            </td>
        </tr>
        </table>
    <br />
    <asp:DataList ID="DataList1" runat="server" Width="100%" 
        DataKeyField="codigo_Ates" CellPadding="2" CellSpacing="1">
        <ItemTemplate>       
            <table class="contornotabla" style="width:100%;" cellpadding="3" 
                cellspacing="0">
                <tr style="background-color: #DDEEFF;">
                    <td width="5%" align="center" rowspan="2">
                        <asp:Label ID="lblNro" runat="server" Font-Bold="True" Font-Names="Tahoma"></asp:Label>
                        
                    </td>
                    <td width="80%">
                        PUBLICADO:
                        <asp:Label ID="lblFecha" runat="server" Font-Bold="False" 
                            Text='<%# eval("fechareg_ates") %>'></asp:Label>
                        &nbsp;por
                        <asp:Label ID="lblAsesor" runat="server" Font-Italic="True" 
                            Text='<%# Strconv(eval("asesor"),3) %>'></asp:Label>
                    </td>
                    <td align="right" width="15%">
                        <!--
                            '---------------------------------------------------------------------------------------------------------------
                            'Fecha: 29.10.2012
                            'Usuario: dguevara
                            'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                            '---------------------------------------------------------------------------------------------------------------
                        -->
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# eval("icono") %>' 
                            Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>' />
                        <asp:HyperLink ID="lnkRuta" runat="server" 
                            NavigateUrl='<%# cstr("//intranet.usat.edu.pe/campusvirtual/archivoscv/tesis/") + eval("ruta_Ates") %>' 
                            Target='<%# cstr("_blank") %>' Text='<%# Cstr("Descargar") %>' 
                            ToolTip='<%# eval("descripcion_TATes") %>' 
                            Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" width="95%">
                        <asp:Label ID="Label3" runat="server" Text='<%# eval("obs_ates") %>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    </form>
</body>
</html>
