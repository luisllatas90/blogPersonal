<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGestionInvestigaciondnt.aspx.vb" Inherits="frmGestionInvestigaciondnt" EnableEventValidation ="true" %>
<%@ Register
    Assembly="AjaxControlToolkit"
    Namespace="AjaxControlToolkit"
    TagPrefix="ajaxToolkit" %>
   
<link href="../css/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.4.4.min.js"></script>
    <script src="../aprise/apprise-1.5.full.js" type="text/javascript"></script>
    <link href="../aprise/apprise.css" rel="stylesheet" type="text/css" />
    <link href="../css/MyStyles.css" rel="stylesheet" type="text/css" />
    <script src="private/funciones.js" type="text/javascript"></script>
    
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Gestión Comité</title>    
</head>
<body>
    <script type="text/javascript">

        function uploadComplete(sender, args) {
            //document.getElementById('<%=lblDocumento.ClientID%>').innerText = args.get_path();
            //$('#lblDocumento').html(args.get_path());
            $("input:hidden[id$=hfDocumento]").val(args.get_path());
        } 
    </script>
    <form id="form1" runat="server" enctype="multipart/form-data" method="post">

    <div>
     
        <ajaxtoolkit:toolkitscriptmanager runat="server" ></ajaxtoolkit:toolkitscriptmanager>
        <asp:UpdatePanel ID="upContenido" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td colspan="3" 
                                style ="background-color:#FFA500; font-weight: bold;" height="30px">
                                <asp:Label ID="lblTit" runat="server" Text="Investigaciones"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                </td>
                            <td align="right">
                                <asp:Button ID="btnNuevo" runat="server" Height="21px" Text="Nuevo" 
                                    Width="96px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvInvestigacion" runat="server" CellPadding="4" ForeColor="#333333" Width="100%" 
                                        BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" 
                                AllowPaging="True" PageSize="5" GridLines="Horizontal" AutoGenerateColumns="False">
                        
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="titulo" HeaderText="Título"></asp:BoundField>
                            <asp:BoundField DataField="linea" HeaderText="Linea"></asp:BoundField>
                            <asp:BoundField DataField="fechaRegistro" HeaderText="Registro"></asp:BoundField>
                            <asp:BoundField DataField="fechaInicio" HeaderText="Inicio"></asp:BoundField>
                            <asp:BoundField DataField="fechaFin" HeaderText="Fin"></asp:BoundField>
                            <asp:BoundField DataField="presupuesto" HeaderText="Presupuesto">
                            
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td>
                                                <asp:LinkButton 
                                                    ID="lbModificar" 
                                                    runat="server"  
                                                    Font-Underline="True" 
                                                    Visible='<%# iif(Eval("etapa") = "Borrador", "True", "False") %>' 
                                                    onclick="lbModificar_Click">Modificar
                                                </asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:LinkButton 
                                                    ID="hlEnviar" 
                                                    runat="server" 
                                                    Font-Underline="True" 
                                                    Visible='<%# iif(Eval("etapa") = "Borrador", "True", "False") %>'
                                                    OnClick="hlEnviar_Click"
                                                    >Enviar
                                                </asp:LinkButton>
                                            </td>
                                            <td><asp:LinkButton 
                                                    ID="hlVersion" 
                                                    runat="server" 
                                                    Font-Underline="True" 
                                                    Visible='<%# iif(Eval("etapa") = "Formulación de Proyecto", "True", "False") %>'
                                                    >Versión
                                                </asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                           <asp:CommandField SelectText="" ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                        <HeaderStyle BackColor="#e8eef7" ForeColor="#3366CC" BorderColor="#99BAE2" 
                                        BorderStyle="Solid" BorderWidth="1px" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlDetalle" runat="server">
                        <asp:Menu
                                ID="Menu1"
                                Width="50%"
                                runat="server"
                                Orientation="Horizontal"
                                StaticEnableDefaultPopOutImage="False"
                                OnMenuItemClick="Menu1_MenuItemClick">
                            <Items>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvDGSel.png" Text=" " Value="0"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvBitacora.png" Text=" " Value="1"></asp:MenuItem>
                                <asp:MenuItem ImageUrl="~/Images/TagButtons/btnInvResumen.png" Text=" " Value="2"></asp:MenuItem>
                            </Items>
                        </asp:Menu>

                        <asp:MultiView 
                            ID="MultiView1"
                            runat="server"
                            ActiveViewIndex="0"  >
                           <asp:View ID="Tab1" runat="server"  >
                                
                                <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            <asp:HiddenField ID="hfUsuReg" runat="server" />
                                        </td>
                                        <td style="width:30%">
                                            <asp:HiddenField ID="hfCodInvestigacion" runat="server" />
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                        </td>
                                        <td style="width:30%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Título</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblTitulo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Fecha de Reg</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFechaRegistro" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Fecha Inicio</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFecIni" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Fecha Fin</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFecFin" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Presupuesto</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblPresupuesto" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Financiamiento</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblFinanci" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Ámbito</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblAmbito" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Línea</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblLinea" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Etapa</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblEtapa" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Tipo</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblTipo" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            Instancia</td>
                                        <td style="width:30%">
                                            <asp:Label ID="lblInstancia" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%" valign="top">
                                            Beneficiarios</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblBeneficiarios" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%" valign="top">
                                            Resumen</td>
                                        <td colspan="3">
                                            <asp:Label ID="lblResumen" runat="server"></asp:Label>
                                        </td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                        <td style="width:5%">
                                            &nbsp;</td>
                                        <td style="width:15%">
                                            &nbsp;</td>
                                        <td style="width:30%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:GridView ID="gvDocumentos" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                                PageSize="5" Width="70%">
                                                <Columns>
                                                    <asp:ImageField ConvertEmptyStringToNull="False" DataImageUrlField="extension" 
                                                        DataImageUrlFormatString="~/images/ext/{0}.gif" HeaderText=""><HeaderStyle Width="1%" /><ItemStyle HorizontalAlign="Center" Width="20px" /></asp:ImageField>
                                                    <asp:HyperLinkField DataNavigateUrlFields="documento" 
                                                        DataNavigateUrlFormatString="{0}" DataTextField="nombre" HeaderText="Documento" 
                                                        Target="_blank" ><ItemStyle HorizontalAlign="Center" /></asp:HyperLinkField>
                                                    <asp:TemplateField><ItemTemplate><asp:HiddenField ID="hfRuta" runat="server" value='<%# Eval("ruta") %>' /></ItemTemplate><ItemStyle Width="1%" /></asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                La investigación no tiene ningún Documento.</td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                                <EditRowStyle BackColor="#999999" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="6">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                           </asp:View>
                           <asp:View ID="Tab2" runat="server"  >
                                
                                <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr style="width:100%" valign="top">
                                        <td  style="width: 100%">&nbsp;
                                        </td>
                                    </tr>
                                    <tr style="width:100%" valign="top">
                                        <td align="center" style="width: 100%">
                                            <asp:GridView ID="gvBitacora" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" 
                                                GridLines="Horizontal" Width="70%">
                                                <Columns>
                                                    <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                                    <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                No hay registros.</td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr style="width:100%" valign="top">
                                        <td style="width: 100%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                             </asp:View>
                           <asp:View ID="Tab3" runat="server"  >
                                <table style="border: 1px solid #99BAE2; width:100%; border-collapse: collapse;" width="100%" height="100%" cellpadding=0 cellspacing=0>
                                    <tr valign="top">
                                        <td style="width: 65%">
                                            &nbsp;</td>
                                        <td style="width: 35%">
                                            &nbsp;</td>
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 65%" align="center">
                                            <asp:GridView ID="gvResumen" runat="server" AllowPaging="True" 
                                                AutoGenerateColumns="False" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                BorderWidth="1px" CellPadding="4" ForeColor="#333333" GridLines="Horizontal" 
                                                PageSize="5" Width="98%">
                                                <Columns>
                                                    <asp:BoundField DataField="hito" HeaderText="Hito" Visible="False" ><ItemStyle Width="30%" /></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Hito">
                                                        <ItemTemplate>
                                                            <table style="width: 100%;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblFechaHito" runat="server" Text='<%# Eval("fechaHito") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDesHito" runat="server" Text='<%# Eval("hitoDes") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="hlNuevoHito" runat="server" Font-Underline="True" 
                                                                            ForeColor="#003399" onclick="hlNuevoHito_Click">Nuevo Hito</asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="42%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avance"><ItemTemplate>
                                                    <table style="width:100%;"><tr><td><asp:Label ID="lblFecha" runat="server" Text= '<%# Eval("fechaAvance") %>'></asp:Label><asp:HiddenField ID="hfAvance_id" runat="server" value='<%# Eval("avance_id") %>' /></td></tr><tr><td><asp:HyperLink ID="hlAvance" runat="server" Target ="_blank" NavigateUrl='<%# Eval("rutaAvance") %>' Text='<%# Eval("documento") %>' /></td></tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="hlNuevoAvance" runat="server" Font-Underline="True">Nuevo Avance</asp:LinkButton>
                                                                    </td>
                                                                </tr></table>
                                                    </ItemTemplate><ItemStyle Width="42%" /></asp:TemplateField>
                                                    <asp:BoundField HeaderText="Observaciones" DataField="observaciones" >
                                                        <ItemStyle Width="16%" /></asp:BoundField>
                                                    <asp:CommandField SelectText="" ShowSelectButton="True" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <table style="width:100%;">
                                                        <tr>
                                                            <td style="width:90%">
                                                                No hay registros.</td>
                                                            <td style="width:10%">
                                                                <asp:Image ID="imgNingun0" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                    BorderWidth="1px" ForeColor="#3366CC" />
                                                <SelectedRowStyle BackColor="#D1DDF1" ForeColor="#333333" />
                                            </asp:GridView>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Panel ID="pnlObservaciones" runat="server" Height="250px" 
                                                ScrollBars="Vertical" BorderWidth="1px" BorderColor="#99BAE2" Width="98%" HorizontalAlign="Center" >
                                                <br />
                                            <asp:DataList ID="dlObservaciones" runat="server" Width="90%">
                                                <ItemTemplate>
                                                    <table class="contornotabla"  Width="100%">
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" align="center">
                                                                <asp:Label ID="lblNro" runat="server" Text='<%# Eval("nro") %>' Font-Bold="True" ForeColor="#3366CC"></asp:Label>
                                                                </td>
                                                            <td width="90%">
                                                                Fecha:
                                                                <asp:Label ID="lblFecha" runat="server" ForeColor="Black" 
                                                                    Text='<%# Eval("fecha") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%" rowspan="3">
                                                                <img alt="" src="../Images/img7.gif" /></td>
                                                            <td width="90%">
                                                                Revisor:
                                                                <asp:Label ID="revisor" runat="server" Font-Bold="True" 
                                                                    ForeColor="Black" Text='<%# Eval("revisor") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%">
                                                                <asp:Label ID="descripcion" runat="server" ForeColor="#003399" 
                                                                    Text='<%# Eval("descripcion") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="90%">
                                                                Documento:
                                                                <asp:HyperLink ID="hlDocumento" runat="server" Target ="_blank" NavigateUrl='<%# Eval("archivo") %>' Text='<%# Eval("documento") %>' />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td width="10%">
                                                                &nbsp;</td>
                                                            <td width="90%">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:DataList>
                                            
                                            </asp:Panel>
                                            <br />
                                            <asp:Panel ID="pnlInsertarObs" runat="server" Height="80px" Width="98%" BorderWidth="1px" BorderColor="#99BAE2" >
                                                <table style="width: 90%;" align="center">
                                                    <tr>
                                                        <td style="width:10%" valign="top">
                                                            &nbsp;</td>
                                                        <td style="width:90%">
                                                            <asp:Label ID="lblDocumento" runat="server" Text=""></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10%" valign="top">
                                                            Observación:
                                                        </td>
                                                        <td style="width:90%">
                                                            <asp:TextBox ID="txtObs" runat="server" Height="45px" Width="100%" 
                                                                TextMode="MultiLine"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width:10%">
                                                            Documento:</td>
                                                        <td style="width:90%">
                                                            <ajaxtoolkit:asyncfileupload runat="server" id="afuObservacion" Font-Size="Smaller" Width="100%"  UploadedComplete="afuObservacion_UploadedComplete"></ajaxtoolkit:asyncfileupload>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" style="width:100%">
                                                            <asp:Button ID="cmdGuardar" runat="server" Text="            Guardar" 
                                                                CssClass="guardar_prp" Height="35px" Width="100px" ValidationGroup="Guardar" 
                                                                ToolTip="Guardar Observación" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr valign="top">
                                        <td style="width: 65%">
                                            &nbsp;</td>
                                        <td style="width: 35%">
                                            &nbsp;</td>
                                    </tr>
                                </table>
                             </asp:View>
                           </asp:MultiView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="btnMostrarPopup" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlPopContenedor" runat="server" Style="display: none" CssClass="modalPopup">
                        <asp:Panel ID="pnlPopCabecera" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:30px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <p>Nuevo Hito</p>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ibtnCerrar" runat="server" 
                                        ImageUrl="~/Images/cerrar.gif" />
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White;height:200px">
                            <tr>
                                <td style="width: 20%" valign="top">
                                    
                                </td>
                                <td style="width: 70%" valign="top">
                                    
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" valign="top">
                                    Descripción:
                                </td>
                                <td style="width: 70%" valign="top">
                                    <asp:TextBox ID="txtDesHito" runat="server" TextMode="MultiLine" 
                                        Height="50px" Width="98%"></asp:TextBox>
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%">
                                    
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%">
                                    &nbsp;
                                </td>
                                <td style="width: 70%" align="center">
                                    <asp:ImageButton ID="ibtnGuardarObs" runat="server" />
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender ID="mpeDescripcion" runat="server" 
                    TargetControlID="btnMostrarPopup"
                    PopupControlID="pnlPopContenedor"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlPopCabecera" 
                    />
                </td>
            </tr>
        </table>
        
        </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnNuevo"/>
                <asp:PostBackTrigger ControlID="gvInvestigacion"/>
            </Triggers>
        </asp:UpdatePanel>

 
    </div>
    </form>
</body>
</html>
