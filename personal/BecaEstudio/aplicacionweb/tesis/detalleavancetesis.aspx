<%@ Page Language="VB" AutoEventWireup="false" CodeFile="detalleavancetesis.aspx.vb" Inherits="detalleavancetesis" %>

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
    A:Active
    {
    	color:Blue;
    	text-decoration:underline;
    }
    a:Link 
    {
    	color:Blue;
        text-decoration: underline;
        }
    a:Visited 
    {
    	color:Maroon;
        text-decoration: underline;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td class="usattitulo" width="90%">
                <asp:Label ID="lbltitulo" runat="server"></asp:Label>
            </td>
            <td width="10%">
                <asp:Button ID="CmdCancelar" runat="server" CssClass="usatSalir" 
                    Text="    Regresar" Height="25px" Width="80px" />
            </td>
        </tr>
    </table>
    <asp:Button ID="cmdNuevo" runat="server" CssClass="usatNuevo" Height="25px" 
        Text="  Nueva asesoría" Width="120px" UseSubmitBehavior="False" />
    &nbsp;<input id="cmdImprimir" class="usatimprimir" type="button" 
        value="Imprimir" onclick="window.print()" />
    <asp:Label ID="Label4" runat="server" ForeColor="Red" 
        Text="Sólo puede eliminar asesorías registradas el día de hoy"></asp:Label>
    <br />
    <br />
    <asp:DataList ID="DataList1" runat="server" Width="100%" 
        DataKeyField="codigo_Ates">
        <ItemTemplate>       
            <table class="contornotabla" style="width:100%;" cellpadding="3" 
                cellspacing="0">
                <tr>
                    <td align="right" style="background-color: #f4f2ed" width="95%" colspan="2">
                        Publicado:
                        <asp:Label ID="lblFecha" runat="server" Font-Bold="True" 
                            Text='<%# eval("fechareg_ates") %>'></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td class="bordeinf" style="background-color: #f4f2ed" width="95%" colspan="2">
                        <asp:Label ID="Label2" runat="server" Text='<%# eval("asesor") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td width="95%" colspan="2">
                        <asp:Label ID="Label3" runat="server" Text='<%# eval("obs_ates") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!--
                        'Fecha: 29.10.2012
                        'Usuario: dguevara
                        'Modificacion: Se modifico el http://www.usat.edu.pe por http://intranet.usat.edu.pe
                        '---------------------------------------------------------------------------------------------------------------
                    -->
                    <td width="90%">
                        &nbsp;<asp:Image ID="Image1" runat="server" ImageUrl='<%# eval("icono") %>' 
                            Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>' />
                        <asp:HyperLink ID="lnkRuta" runat="server" 
                            NavigateUrl='<%# cstr("//intranet.usat.edu.pe/campusvirtual/archivoscv/tesis/") + eval("ruta_Ates") %>' 
                            Text='<%# StrConv(eval("descripcion_TATes"),3) %>' Target='<%# cstr("_blank") %>' 
                            Visible='<%# iif(IsDBNull(eval("ruta_Ates"))=true,false,true) %>'></asp:HyperLink>
                    </td>
                    <td align="right" width="5%">
                        <asp:ImageButton ID="imgEliminar" runat="server" 
                            ImageUrl='<%# cstr("../../../images/Eliminar.gif") %>' 
                            
                            onclientclick="return confirm('¿Está seguro que desea Eliminar el registro de asesoría de tesis?');" 
                            
                            Visible='<%# iif(eval("codigo_per")=cstr(session("codigo_usu2")),true,false) %>' 
                            ToolTip="Eliminar asesoría de tesis" />
                    </td>
                </tr>
            </table>
            <br />
        </ItemTemplate>
    </asp:DataList>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" ForeColor="#333333" GridLines="None" Visible="False">
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="fechareg_Ates" HeaderText="Fecha de Registro" />
            <asp:BoundField DataField="asesor" HeaderText="Registrado por" />
            <asp:BoundField DataField="descripcion_Tates" HeaderText="Tipo" />
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </form>
</body>
</html>
