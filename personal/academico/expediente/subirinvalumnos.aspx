<%@ Page EnableEventValidation="false" Language="VB" AutoEventWireup="false" CodeFile="subirinvalumnos.aspx.vb" Inherits="subirinvalumnos" %>
<html>
<head runat="server">
    <title>Página sin título</title>
    <script src="../../../private/funciones.js"> </script>
    <script language="javascript" type="text/javascript">
        function validarlista(source, arguments)
            {
                if (document.frmListaCorreos.ListaPara.length == 0 )
                    arguments.IsValid = false
                else
                    arguments.IsValid = true
            }
    </script>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css">
    
    <STYLE type="text/css">
BODY {
scrollbar-face-color:#FFFFFF;
scrollbar-highlight-color:#FFFFFF;
scrollbar-3dlight-color:#FFFFFF;
scrollbar-darkshadow-color:#FFFFFF;
scrollbar-shadow-color:#EEEEEE;
scrollbar-arrow-color:#000000;
scrollbar-track-color:#FFFFFF;
}
a:link {text-decoration: none; color: #00080; }
a:visited {text-decoration: none; color: #000080; }
a:hover {text-decoration: none; black; }
a:hover{color: black; text-decoration: none; }
</STYLE>
    
</head>
<body  style="background:#F0F0F0; margin-top:0px; margin-left:0px">
    <form id="frmListaCorreos" runat="server" >
  
        <table width="669" cellpadding="0" cellspacing="0" style="width: 59%">
          <!--DWLayoutTable-->
            <tr>
                <td colspan="2" align="right" valign="top">
                    <table width="84%" height="136" cellpadding="0" cellspacing="0" style="width: 112%">
                        <tr>
                            <td width="109" style="color: maroon;">
                                &nbsp;Profesor</td>
                            <td style="width: 448px">
                                :
                          <asp:Label ID="LblDocente" runat="server" ForeColor="Navy"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="color: maroon;">
                                &nbsp;Asignatura</td>
                            <td style="width: 448px">
                                :
                                <asp:Label ID="LblAsignatura" runat="server" ForeColor="Navy"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2">&nbsp;                          </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                    <asp:Panel ID="PanelInv" runat="server" Height="200px" ScrollBars="Vertical" Width="410px">
                        <asp:GridView ID="GridInv" runat="server" Width="418px" AutoGenerateColumns="False" DataKeyNames="codigo" DataSourceID="Investigaciones" GridLines="Horizontal" CellPadding="2" BorderColor="Transparent">
                            <Columns>
                                <asp:BoundField DataField="codigo" HeaderText="codigo" InsertVisible="False" ReadOnly="True"
                                    SortExpression="codigo" Visible="False" />
                                <asp:BoundField DataField="Titulo" HeaderText="Titulo" ReadOnly="True" SortExpression="Titulo" />
                                <asp:BoundField DataField="Fecharegistro_inv" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Registro"
                                    HtmlEncode="False" SortExpression="Fecharegistro_inv" Visible="False" >
                                    <ItemStyle Width="80px" />                                </asp:BoundField>
                                <asp:BoundField DataField="Asignado" HeaderText="Asignado" ReadOnly="True" SortExpression="Asignado">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />                                </asp:BoundField>
                                <asp:CheckBoxField DataField="vigencia" HeaderText="Activo" SortExpression="vigencia" ReadOnly="True" Visible="False">
                                    <ItemStyle HorizontalAlign="Center" Width="40px" />                                </asp:CheckBoxField>
                                <asp:BoundField DataField="ruta" SortExpression="ruta" Visible="False">
                                    <ItemStyle HorizontalAlign="Center" Width="20px" />                                </asp:BoundField>
                                <asp:BoundField DataField="ruta" HeaderText="ruta" SortExpression="ruta" Visible="False" />
                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="../../../images/ext/pdf.gif" >
                                    <ItemStyle Width="20px" />                                </asp:CommandField>
                            </Columns>
                            <RowStyle Font-Names="Verdana" Font-Size="7pt" BackColor="Transparent" ForeColor="#330099" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle Font-Names="Verdana" Font-Size="8pt" BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" Height="20px" />
                            <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="LightGoldenrodYellow" Font-Bold="True" ForeColor="#663399" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <span style="vertical-align: middle; width: 100%; color: red; height: 100%; text-align: center">
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    No se han registrado investigaciones para publicar. 
                                    <br />
                                    Haga click en Subir Archivos
                                    para registrar una.<br />
                                    Recuerde que solo puede subir archivos con extensión *.pdf.<br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </span>                            </EmptyDataTemplate>
                        </asp:GridView>
                        <asp:SqlDataSource ID="Investigaciones" runat="server" ConnectionString="<%$ ConnectionStrings:CNXBDUSAT%>"
                            SelectCommand="INVALU_ConsultarInvestigaciones" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="codigo_cup" QueryStringField="codigo_cup" Type="Int32" />
                                <asp:QueryStringParameter Name="codigo_cac" QueryStringField="codigo_cac" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </asp:Panel>                            </td>
                        </tr>
                  </table>
                  <tr><td align="center">
                      &nbsp; &nbsp; &nbsp;<asp:HyperLink ID="Linkprevio" runat="server" NavigateUrl="sadasdsada"
                          Target="rightFrame" ForeColor="MediumBlue">Vista Previa de Documento</asp:HyperLink></td>
                    <td align="center"><asp:Button ID="CmdSubir" runat="server" Text="     Subir Archivos" CssClass="agregar3" Width="104px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" Height="19px" /></td>
                  </tr>
            <tr>
                <td colspan="2" valign="top">
                    <table style="width: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="font-size: 8pt; width: 61px; color: black; font-family: verdana; height: 6px">                            </td>
                            <td>&nbsp;                            </td>
                        </tr>
                        
                        <tr>
                            <td style="width: 61px; font-size: 8pt; color: black; font-family: verdana; height: 6px;">
                                &nbsp;Titulo<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="TxtTitulo" ErrorMessage="El título de la investigación es necesario."
                                    ValidationGroup="guardar">*</asp:RequiredFieldValidator></td>
                            <td>
                                <asp:TextBox ID="TxtTitulo" runat="server" Width="363px" style="font-size: 8pt; color: navy; font-family: verdana" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="font-size: 8pt; width: 61px; color: black; font-family: verdana; height: 6px">
                                &nbsp;Breve<br />
                                &nbsp;Resumen &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="TxtDescripcion" ErrorMessage="Ingrese una breve descripcion al trabajo de investigacion para mostrar."
                                    SetFocusOnError="True" ValidationGroup="guardar">*</asp:RequiredFieldValidator><br /><span id="LblMuestra"></span>
                                &nbsp;</td> 
                            <td>
                                <asp:TextBox ID="TxtDescripcion" runat="server" Height="83px" MaxLength="800" Style="font-size: 8pt;
                                    color: navy; font-family: verdana" TextMode="MultiLine" Width="363px" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right">
                                <table style="width: 100%">
                                    <tr>
                                        <td style="font-size: 8pt; color: black; font-family: verdana; height: 6px" align="center">
                                            Estudiantes no asignados</td>
                                        <td style="font-size: 8pt; color: black; font-family: verdana; height: 6px" align="center">
                                            &nbsp;Estudiantes asignados
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validarlista"
                                                ErrorMessage="Debe tener estudiantes asignados a la investigacion." ValidationGroup="guardar">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td rowspan="2" valign="top">
                                            <asp:ListBox ID="ListaDe" runat="server" Height="190px" Width="204px" style="font-size: 7pt; color: navy; font-family: verdana" SelectionMode="Multiple"></asp:ListBox></td>
                                        <td rowspan="2" valign="top">
                                            <asp:ListBox ID="ListaPara" runat="server" Height="190px" Width="212px" style="font-size: 7pt; color: navy; font-family: verdana" SelectionMode="Multiple"></asp:ListBox></td>
                                    </tr>
                                    <tr>                                    </tr>
                                    <tr>
                                        <td align="right" rowspan="1" style="height: 24px">
                                            <asp:Button ID="CmdAgregar" runat="server" Text="      Agregar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="attach_prp" Font-Bold="False" Height="22px" Width="69px" />
&nbsp;                                        </td>
                                        <td rowspan="1" style="height: 24px">
                                            &nbsp;
                                            <asp:Button ID="CmdRetirar" runat="server" Text="     Retirar" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" CssClass="remove_prp" Font-Bold="False" Height="22px" Width="69px" /></td>
                                    </tr>
                                </table>
                                <asp:Button ID="CmdGuardar" runat="server" Text="Guardar" CssClass="guardar" ValidationGroup="guardar" /></td>
                        </tr>
                    </table>                </td>
            </tr>
      </table>
    
  
        <asp:HiddenField ID="HddCodigoInv" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="guardar" />
    </form>
</body>
</html>
