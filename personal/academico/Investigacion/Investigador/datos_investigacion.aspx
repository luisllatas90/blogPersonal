<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datos_investigacion.aspx.vb" Inherits="Investigador_datos_investigacion" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<STYLE type="text/css">
BODY {
scrollbar-face-color:#AED9F4;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#FFFFFF;
scrollbar-arrow-color:#000000;

scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 75%" valign="top">
                    <asp:DataList ID="DataList1" runat="server" DataKeyField="codigo_Inv" Width="100%">
                        <ItemTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Titulo de Investigación</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="titulo_InvLabel" runat="server" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="X-Small" Font-Underline="True" Style="text-transform: uppercase" Text='<%# Eval("titulo_Inv") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; width: 23%; color: black; font-family: verdana">
                                        Fecha de Inicio</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="fechaInicio_InvLabel" runat="server" Text='<%# format(Eval("fechaInicio_Inv"),"dd-MM-yyyy") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Unidad</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="descripcion_CcoLabel" runat="server" Text='<%# Eval("descripcion_Cco") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Linea</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="nombre_LinLabel" runat="server" Text='<%# Eval("nombre_Lin") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Etapa</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="nombre_EtILabel" runat="server" Text='<%# Eval("nombre_EtI") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Estado</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="descripcion_EinLabel" runat="server" Text='<%# Eval("descripcion_Ein") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Costo</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="Costo_InvLabel" runat="server" Text='<%# Eval("Costo_Inv") %>'></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Duración</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="Costo_InvLabel0" runat="server" 
                                            Text='<%# Eval("Duracion_Inv") %>'></asp:Label>
                                        &nbsp;<asp:Label ID="Costo_InvLabel4" runat="server" 
                                            Text='<%# Eval("TipoDuracion") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Ámbito</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="Costo_InvLabel1" runat="server" Text='<%# Eval("Ambito") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Población</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="Costo_InvLabel2" runat="server" Text='<%# Eval("Poblacion") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        Detalle Zona Impacto</td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        :
                                        <asp:Label ID="Costo_InvLabel3" runat="server" 
                                            Text='<%# Eval("DetalleZona_Inv") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-weight: bold; font-size: 9pt; color: black; font-family: verdana">
                                        <asp:Label ID="Label5" runat="server" Text="Decreto" Visible='<%# iif(Eval("decreto") = "-" ,false,true) %>'></asp:Label></td>
                                    <td style="font-size: 8pt; color: black; font-family: verdana">
                                        <asp:Label ID="Label6" runat="server" Text='<%# ": " & Eval("decreto") %>' Visible='<%# iif(Eval("decreto") = "-",false,true) %>'></asp:Label>
                                        &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" ImageUrl="../../../../images/ext/pdf.gif"
                                            NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("codigo_inv") & "decreto.pdf" %>'
                                            Target="_blank" Visible='<%# iif(Eval("decreto") = "-",false,true) %>'></asp:HyperLink></td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList></td>
                <td style="width: 50%" valign="top">
                    <asp:Panel ID="Panel1" runat="server" Height="233px" ScrollBars="Auto" Width="100%">
                        <asp:DataList ID="DataList2" runat="server" DataKeyField="codigo_Inv" DataSourceID="INvestigacion" Width="100%">
                            <ItemTemplate>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-size: 9pt; color: navy; font-family: verdana; height: 16px; background-color: #e1f1fb; text-align: center" colspan="3">
                                            Archivos</td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 8pt; width: 25%; font-family: verdana" align="center">
                                            Tipo</td>
                                        <td style="font-size: 8pt; width: 70%; font-family: verdana" align="center">
                                            Fecha</td>
                                        <td style="font-size: 8pt; width: 10%; font-family: verdana" align="center">
                                            Ver</td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 8pt; width: 10%; font-family: verdana">
                                            <asp:Label ID="Label1" runat="server" Text="Perfil" Visible='<%# iif(Eval("rutaPerfil_Inv") = "-" ,false,true) %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 70%; font-family: verdana" align="center">
                                            <asp:Label ID="fechaPerfil_InvLabel" runat="server" Text='<%# Eval("fechaPerfil_Inv", "{0:d}") %>' Width="80px" Visible='<%# iif(Eval("rutaPerfil_Inv") = "-" ,false,true) %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 10%; font-family: verdana" align="center">
                                            <asp:HyperLink ID="HyperLink1" runat="server" ImageUrl='<%# "../../../../images/ext/"& Right(Eval("rutaPerfil_Inv"),3) &".gif" %>'
                                                NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("rutaPerfil_Inv") %>'
                                                Target="_blank" Visible='<%# iif(Eval("rutaPerfil_Inv") = "-",false,true) %>'></asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 8pt; width: 10%; font-family: verdana">
                                            <asp:Label ID="Label2" runat="server" Text="Proyecto" Visible='<%# iif(Eval("rutaProyecto_Inv") = "-",false,true) %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 70%; font-family: verdana" align="center">
                                            <asp:Label ID="fechaProyecto_InvLabel" runat="server" Text='<%# Eval("fechaProyecto_Inv", "{0:d}") %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 10%; font-family: verdana" align="center">
                                            <asp:HyperLink ID="HyperLink2" runat="server" ImageUrl='<%# "../../../../images/ext/"& Right(Eval("rutaProyecto_Inv"),3) &".gif" %>'
                                                NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("rutaProyecto_Inv") %>'
                                                Target="_blank" Visible='<%# iif(Eval("rutaProyecto_Inv") = "-",false,true) %>'></asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 8pt; font-family: verdana" colspan="3">
                                            <asp:DataList ID="DataList3" runat="server" DataKeyField="codigo_Ava" DataSourceID="Avances"
                                                Width="100%" CellPadding="0" ShowFooter="False" ShowHeader="False">
                                                <ItemTemplate>
                                                    <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                        <tr>
                                                            <td style="font-size: 8pt; color: black; font-family: verdana; font-weight: bold; width: 10%;">
                                                                Avance 
                                                            </td>
                                                            <td style="font-size: 8pt; color: black; font-family: verdana; width: 70%;" align="center">
                                                                <asp:Label ID="fechaIngreso_AvaLabel" runat="server" Text='<%# Eval("fechaIngreso_Ava", "{0:d}") %>' Width="80px"></asp:Label></td>
                                                            <td style="font-size: 8pt; color: black; font-family: verdana; width: 10%;" align="left">
                                                                <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl='<%# "../../../../images/ext/"& Right(Eval("rutaAvance_Ava"),3) &".gif" %>'
                                                                    NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("rutaAvance_Ava") %>'
                                                                    Target="_blank" Visible='<%# iif(Eval("rutaAvance_Ava") = "-",false,true) %>'></asp:HyperLink>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:DataList></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 8pt; width: 10%; font-family: verdana; height: 18px;">
                                            <asp:Label ID="Label3" runat="server" Text="Informe" Visible='<%# iif(Eval("rutaInforme_Inv") = "-" ,false,true) %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 70%; font-family: verdana; height: 18px;" align="center">
                                            <asp:Label ID="fechaInforme_InvLabel" runat="server" Text='<%# Eval("fechaInforme_Inv", "{0:d}") %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 10%; font-family: verdana; height: 18px;" align="center">
                                            <asp:HyperLink ID="HyperLink3" runat="server" ImageUrl='<%# "../../../../images/ext/"& Right(Eval("rutaInforme_Inv"),3) &".gif" %>'
                                                NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("rutaInforme_Inv") %>'
                                                Target="_blank" 
                                                Visible='<%# iif(Eval("rutaInforme_Inv") = "-",false,true) %>' 
                                                ToolTip="Informe">[HyperLink3]</asp:HyperLink></td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold; font-size: 8pt; width: 10%; font-family: verdana; height: 18px;">
                                            <asp:Label ID="Label4" runat="server" Text="Resumen" Visible='<%# iif(Eval("rutaResumen_Inv")  = "-",false,true) %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 70%; font-family: verdana; height: 18px;" align="center">
                                            <asp:Label ID="fechaResumen_InvLabel" runat="server" Text='<%# Eval("fechaResumen_Inv", "{0:d}") %>'></asp:Label></td>
                                        <td style="font-size: 8pt; width: 10%; font-family: verdana; height: 18px;" align="center">
                                            <asp:HyperLink ID="HyperLink4" runat="server" ImageUrl='<%# "../../../../images/ext/"& Right(Eval("rutaResumen_Inv"),3) &".gif" %>'
                                                NavigateUrl='<%# "../../../../filesInvestigacion/" & Eval("codigo_Inv") &"/" & Eval("rutaResumen_Inv") %>'
                                                Target="_blank" 
                                                Visible='<%# iif(Eval("rutaResumen_Inv") = "-",false,true) %>' 
                                                ToolTip="Resumen">[HyperLink4]</asp:HyperLink></td>
                                    </tr>
                                </table>
                                            <asp:SqlDataSource ID="Avances" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                                                SelectCommand="ConsultarAvances" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="1" Name="tipo" Type="String" />
                                                    <asp:QueryStringParameter DefaultValue="" Name="param1" QueryStringField="codigo_inv"
                                                        Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:SqlDataSource ID="INvestigacion" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>"
                            SelectCommand="ConsultarInvestigaciones2" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="12" Name="tipo" Type="String" />
                                <asp:QueryStringParameter DefaultValue="" Name="param1" QueryStringField="codigo_inv"
                                    Type="Int64" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
