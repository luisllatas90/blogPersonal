<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmimportarcolegiado.aspx.vb" Inherits="frmimportarcolegiado" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Importar usuarios a base de datos</title>
    <link href="../../private/estilos.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body bgcolor="#eeeeee">
    <%Response.Write(ClsFunciones.BarraDeProgreso)%>
    <form id="form1" runat="server">
        <table width="100%" cellpadding="3" cellspacing="0" class="contornotabla">
            <tr>
                <td  width="85%" class="titulofrm">Importar nuevos colegiados</td>
                <td align="center" width="15%" rowspan="2">
                    <img alt="folder" src="../../images/folder.png" 
                        style="width: 32px; height: 32px" /></td>
            </tr>
            <tr>
                <td  width="85%">
                    Permite importar los nuevos colegiados a la base de datos del sitio web. El 
                    formato de archivo debe ser Microsoft Excel XP, 2002 o 2003                 </td>
            </tr>
            </table>
        <br />
        <table width="100%" cellpadding="3" cellspacing="0">
            <tr>
                <td colspan="2" width="100%">
                    Seleccione el archivo<asp:RequiredFieldValidator ID="ValidarSubir" 
                        runat="server" ControlToValidate="FileUpload" 
                        ErrorMessage="Por favor indique la ubicación del archivo de Microsoft Excel (*.xls)">*</asp:RequiredFieldValidator>
                    :
            <asp:FileUpload ID="FileUpload" runat="server" Width="50%" Height="20px" />
                    &nbsp;<asp:Button ID="cmdSubir" runat="server" 
                    Text="   Procesar" CssClass="cmdprocesarXLS" Font-Bold="False" ForeColor="Blue" 
                        ToolTip="Visualiza el archivo de excel a importar." 
                        EnableViewState="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                <div id="listadiv" style="width:100%;height:350px; background-color:white" class="contornotabla">
                <asp:GridView ID="grwUsuarios" runat="server" CellPadding="4" ForeColor="#333333" 
                        GridLines="None" Width="100%" AutoGenerateColumns="False">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="ical" HeaderText="ICAL" />
                        <asp:BoundField DataField="apellidopaterno" HeaderText="ApellidoPaterno" />
                        <asp:BoundField DataField="apellidomaterno" HeaderText="ApellidoMaterno" />
                        <asp:BoundField DataField="nombres" HeaderText="Nombres" />
                        <asp:BoundField DataField="domicilioprocesal" HeaderText="DomicilioProcesal" />
                        <asp:BoundField DataField="telefonodomicilioprocesal" 
                            HeaderText="TelefonoDomicilioProcesal" />
                        <asp:BoundField DataField="celular1" HeaderText="Celular1" />
                        <asp:BoundField DataField="dni" HeaderText="DNI" />
                        <asp:BoundField DataField="sexo" HeaderText="Sexo" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                        Font-Names="Tahoma" Font-Size="8pt" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                </div>
                </td>
            </tr>
            <tr>
                <td width="85%">
                    <asp:Label ID="lblmensaje" runat="server" Font-Bold="True" ForeColor="Red" 
                        EnableViewState="False"></asp:Label>
                    <asp:HyperLink ID="LnkFormato" runat="server" 
                        NavigateUrl="../formatos/tmpcolegiados.xls" Target="_blank" 
                        EnableViewState="False">
                    Descargar Plantilla de Microsoft Excel para importar pagos de colegiados</asp:HyperLink>
                </td>
                <td align="right" width="15%">
                    <asp:Button ID="cmdCopiar" runat="server" 
                    Text="     Importar &amp; Grabar" CssClass="cmdOK" 
                        onclientclick="OcultarTabla()" Visible="False" CausesValidation="False" 
                        EnableViewState="False" />
                </td>
            </tr>
        </table>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
        
    </form>

</body>
</html>
