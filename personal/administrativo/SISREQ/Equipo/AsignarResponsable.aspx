<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AsignarResponsable.aspx.vb" Inherits="AsignarResponsable" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Asignar Responsable</title>
    <link href ="../private/estilo.css" rel="stylesheet" type ="text/css" />
    <link href ="../private/estiloweb.css" rel="stylesheet" type ="text/css" />
    <script language="javascript" type ="text/javascript" src="../private/funcion.js"></script>
    <script language ="javascript" type="text/javascript">
        function ValidaDatos(source, arguments)
        {
            if (frmAsigResponsable.CboCampo.value > 0)
                if (frmAsigResponsable.CboValor.value > -1)
                    arguments.IsValid= true
                else
                    arguments.IsValid= false
            
        }
    </script>
</head>
<body style="text-align: center">
    <form id="frmAsigResponsable" runat="server">
    <div style="text-align: center">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="3" rowspan="1"
                    valign="top">
                                &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="3" rowspan="1" style="width: 90%; height: 10px;
                    text-align: center" valign="middle">
                    <table width="100%">
                        <tr>
                            <td>
                                <strong>&nbsp; BUSCAR SOLICITUD POR:
                                <asp:DropDownList ID="CboCampo" runat="server" AutoPostBack="True" Width="153px">
                                    <asp:ListItem Value="-1">-- Seleccione campo --</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">Todos los campos</asp:ListItem>
                                    <asp:ListItem Value="1">Prioridad</asp:ListItem>
                                    <asp:ListItem Value="2">Tipo de solicitud</asp:ListItem>
                                    <asp:ListItem Value="3">&#193;rea</asp:ListItem>
                                    <asp:ListItem Value="4">Aplicaci&#243;n</asp:ListItem>
                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="CboCampo"
                                    ErrorMessage="Seleccione campo de busqueda" Operator="GreaterThanEqual" ValueToCompare="0" ValidationGroup="Consultar">*</asp:CompareValidator>
                                    </strong></td>
                            <td rowspan="3">
                                <strong><asp:Button ID="CmdConsultar" runat="server" Text="Buscar" ValidationGroup="Consultar" CssClass="buscar" Height="28px" Width="90px" /></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>&nbsp;
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:DropDownList ID="CboValor" runat="server" Width="446px">
                                    <asp:ListItem Value="-1">-- Selecione Valor --</asp:ListItem>
                                </asp:DropDownList><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Seleccione valor de busqueda" ClientValidationFunction="ValidaDatos" ValidationGroup="Consultar">*</asp:CustomValidator>&nbsp;</strong></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                  </td>
            </tr>
            <tr>
            <td style="height: 1px; background-color:#004182">
            </td>
            </tr>
            <tr>
                <td colspan="3" style="width: 90%; text-align: center; height: 176px;" 
                    valign="top">
                    <table id="TABLE1" width="98%" border="0" cellpadding="0" cellspacing="0" >
                        <tr>
                            <td style="height: 25px; text-align: right">
                                <asp:Label ID="LblTotal" runat="server" Font-Bold="False" 
                                    ForeColor="#0000CC"></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right" height="15px">
                                Para asignar el equipo dar clic en la imagen
                                <img alt="" src="../images/asignar.gif" /> </td>
                        </tr>
                        <tr>
                            <td style="height: 16px; margin-left: 40px;" valign="top">
                                <asp:GridView ID="GvSolicitud" runat="server" AutoGenerateColumns="False" 
                                    DataKeyNames="id_sol" EnableTheming="True" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" Width="100%">
                                    <Columns>
                                        <asp:BoundField DataField="id_sol" HeaderText="Nro" InsertVisible="False" ReadOnly="True"
                                            SortExpression="id_sol" Visible="False" />
                                        <asp:BoundField DataField="descripcion_sol" HeaderText="Solicitud" SortExpression="descripcion_sol">
                                            <ItemStyle Width="600px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="fecha_sol" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Fecha"
                                            HtmlEncode="False" SortExpression="fecha_sol">
                                            <ItemStyle Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_tsol" HeaderText="Id_Tipo" SortExpression="id_tsol"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_tsol" HeaderText="Tipo" SortExpression="descripcion_tsol" >
                                            <ItemStyle Width="300px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="prioridad" HeaderText="Prioridad" SortExpression="prioridad" >
                                            <ItemStyle Width="150px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" SortExpression="codigo_cco"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_cco" HeaderText="&#193;rea" SortExpression="descripcion_cco">
                                            <ItemStyle Width="800px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="codigo_apl" HeaderText="codigo_apl" SortExpression="codigo_apl"
                                            Visible="False" />
                                        <asp:BoundField DataField="descripcion_apl" HeaderText="M&#243;dulo" SortExpression="descripcion_apl">
                                            <ItemStyle Width="600px" Wrap="False" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="id_est" HeaderText="Estado" SortExpression="id_est" Visible="False" />
                                        <asp:BoundField DataField="vigente_solest" HeaderText="Vigencia" SortExpression="vigente_solest" Visible="False" />
                                        <asp:HyperLinkField DataNavigateUrlFields="id_sol" DataNavigateUrlFormatString="Responsable.aspx?field={0}"
                                            Text="Asignar" HeaderText="Asignar" >
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:HyperLinkField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle Height="40px" ForeColor="#333333" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle CssClass="TituloReq" Height="20px" 
                                        Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="Consultar" />
                </td>
            </tr>
            </table>
    
    </div>
    </form>
</body>
</html>
