<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frminscripcioneventoGo.aspx.vb" Inherits="frminscripcionevento" Theme="Acero" %>
<html>
<head runat="server">
    <title>Eventos</title>
    <link rel="stylesheet" type="text/css" href="../../../private/estilo.css">
    <script language="JavaScript" src="../../../private/funciones.js"></script>    
    <style type="text/css">
        .0
        {
            background-color: #E6E6FA;
        }
        .1
        {
            background-color: #FFFCBF;
        }
        .2
        {
            background-color: #D9ECFF;
        }
        .3
        {
            background-color: #C7E0CE;
        }
        
        .5
        {
            background-color: #FFCC00;
        }
        .6
        {
            background-color: #F8C076;
        }
        .4
        {
            background-color: #CCFF66;
        }
        .style4
        {
            border-left: 1px solid #808080;
            border-right: 1px solid #808080;
            border-top: 1px solid #808080;
            color: #0000FF;
            background-color: #EEEEEE;
            font-weight: bold;
            cursor: hand;
            height: 25px;
            width: 18%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
    <tr>
        <td class="<%=request.querystring("mod")%>" colspan="2" 
            style="width: 100%; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #0099FF;" height="40px">
            <asp:Label ID="lblTitulo" runat="server" Text="Eventos registrados" 
                Font-Bold="True" Font-Size="11pt"></asp:Label>
        </td>
    </tr>
    <tr>
    <td width="15%">Centro de costo
                            <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                ControlToValidate="cboCecos" ErrorMessage="Seleccione el Centro de Costos" 
                                Operator="NotEqual" ValueToCompare="-1">*</asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
            ControlToValidate="txtBuscaCecos" 
            ErrorMessage="Debe ingresar el nombre del centro de costos." 
            ValidationGroup="BusquedaTexto">*</asp:RequiredFieldValidator>
                                    </td>
                <td width="85%">
                                        <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True" 
                                            SkinID="ComboObligatorio" Width="500px">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtBuscaCecos" runat="server" ValidationGroup="BuscarCecos" 
                                            style="width: 128px" Visible="False" SkinID="CajaTextoObligatorio"></asp:TextBox>
                                        &nbsp;<asp:Button ID="cmdBuscar" runat="server" Text="Buscar" 
                                            SkinID="BotonBuscar" ValidationGroup="BusquedaTexto" Visible="False" />
                                        &nbsp;<asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" 
                                            Font-Underline="True" ForeColor="Blue" ValidationGroup="Avanzada">Busqueda Avanzada</asp:LinkButton>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                            ShowMessageBox="True" ShowSummary="False" />
                                        <asp:UpdateProgress ID="upProcesando" runat="server">
                                            <ProgressTemplate>
                                                <font style="color:Blue">Procesando. Espere un momento...</font>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                </td>
    </tr>
    <tr runat="server">
    <td width="15%">&nbsp;</td>
                <td width="85%">
                    <asp:Panel ID="trResultados" runat="server" Visible="false" Height="300px" ScrollBars="Vertical">
                    <asp:GridView ID="gvCecos" runat="server" 
                        AutoGenerateColumns="False" BorderColor="#628BD7" BorderStyle="Solid" 
                        BorderWidth="1px" CellPadding="3" DataKeyNames="codigo_cco" ForeColor="#333333" 
                        ShowHeader="False" Width="98%">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" 
                            ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="codigo_cco" 
                                HeaderText="Código" />
                            <asp:BoundField DataField="nombre" 
                                HeaderText="Centro de costos" />
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" 
                            HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <b>No se encontraron items con el término de búsqueda</b>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" 
                            ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" 
                            ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                     </asp:Panel>                  
                </td>
    </tr>
    </table>
    <br />
    <div id="tabs" runat="server" visible="false">
	<table cellspacing="0" cellpadding="0" style="border-collapse: collapse;bordercolor: #111111;width:100%">
		<tr>
			<td class="pestanabloqueada" id="tab" align="center" style="height:25px;width:12%" onclick="ResaltarPestana2('0','','');">
                <asp:LinkButton ID="lnkDatosEvento" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Datos del <br /> evento</asp:LinkButton>
            </td>
			<td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkPreInscripcion" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">PreInscritos</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%;display:none;">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:12%;display:none;" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkInscripciones" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Inscripciones</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%;display:none;">&nbsp;</td>
			<td class="pestanaresaltada" id="tab" align="center" style="height:25px;width:12%;display:none;" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkRegisMateriales" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Registrar<br>Materiales</asp:LinkButton>
            </td>
            <td class="bordeinf" style="height:25px;width:1%;display:none;">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td1" align="center" style="height:25px;width:12%;display:none;" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkprogActividades" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Programar<br>Actividades</asp:LinkButton>
            </td>                        
              <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td2" align="center" style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');"> 
               <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" 
                 Font-Underline="True" ForeColor="Blue" ToolTip="Inscripción&lt;br/&gt;Directa"> Inscripción<br/>Directa</asp:LinkButton> 
             </td> 
            <!-- por mvillavicencio 18/07/12 -->
            <td class="bordeinf" style="height:25px;width:1%">&nbsp;</td>
			    <td class="pestanaresaltada" id="Td3" align="center" style="height:25px;width:12%" onclick="ResaltarPestana2('1','','');">
                <asp:LinkButton ID="lnkInscripcionCompleta" runat="server" Font-Bold="True" 
                    Font-Underline="True" ForeColor="Blue">Inscripción<br/>Completa</asp:LinkButton>
            </td>
		</tr>
		<tr>
		<!-- por mvillavicencio 18/07/12 colspan='12' por "14" -->
    	<td style="height:600px;width:100%" valign="top" colspan="14" class="pestanarevez">
			<iframe id="fradetalle" height="100%" width="100%" border="0" frameborder="0" runat="server">
			</iframe>
		</td>
	  </tr>
	</table>
    </div>
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="13pt" 
        ForeColor="Red"></asp:Label>
</form>
</body>
</html>