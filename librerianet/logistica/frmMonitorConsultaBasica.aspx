<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMonitorConsultaBasica.aspx.vb" Inherits="logistica_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />

    <title>Monitor de Pedidos de Logística</title>
    <link href="../private/estilo.css"rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloctrles.css" rel="stylesheet" type="text/css" /> 
    <style type="text/css">
        .style1
        {
            width: 979px;
        }

        .style2
        {
            height: 30px;
        }

    </style>
    <style type="text/css">
        .style1
        {
            width: 11px;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 20px;
            padding-left: 10px;
            width: 520px;
            height: 140px;
            vertical-align:middle;

          }
    </style>
    <script language="javascript" type="text/javascript">   
        function validarObservacion(source, arguments)
            {   if ((document.form1.rbOpciones_0.checked != true )&(document.form1.txtObservacion.value==""))
                    arguments.IsValid = false
                else 
                    arguments.IsValid = true
            }
    </script>
</head>
<body>
    <form id="form1" runat="server">
<table id="tblMarcos" style="height: 100%; width: 100%; margin-right: 0px" 
        align="right" class="contornotabla">
<tr  ><td class="style2">
    Consulta básica de pedidos&nbsp;<asp:DropDownList ID="cboInstancia" 
        runat="server" AutoPostBack="True" Height="16px" Visible="False">
    </asp:DropDownList>
            <asp:DropDownList ID="cboVeredicto" runat="server" AutoPostBack="True" 
        Visible="False">
                <asp:ListItem Selected="True" Value="P">Pendientes</asp:ListItem>
                <asp:ListItem Value="O">Observados</asp:ListItem>
                <asp:ListItem Value="C">Conformes</asp:ListItem>
                <asp:ListItem Value="R">Rechazados</asp:ListItem>
                <asp:ListItem Value="D">Derivadas</asp:ListItem>
    </asp:DropDownList>
            </td>
            <td align="right" class="style2">
    <asp:Button ID="cmdEnviar" runat="server" Text="  Enviar" 
                                    BorderStyle="Outset" CssClass="salir" Width="87px" 
                        Height="26px" Visible="False" />
            </td>
</tr>

<tr  ><td class="style2" colspan="2">
    Pedido
    <asp:TextBox ID="txtIdPedido" runat="server" Width="56px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Trabajador&nbsp;
    <asp:TextBox ID="txtTrabajador" runat="server" Width="342px"></asp:TextBox>
    <asp:Button ID="cmdConsultar" runat="server" Text="      Consultar" 
        CssClass="Buscar" Height="26px" Width="83px" />
            &nbsp;
            </td>
</tr>

<tr  ><td class="style2" colspan="2">
    Centro de Costos    <asp:DropDownList ID="cboCentroCostos" runat="server" Width="100%">
    </asp:DropDownList>
    &nbsp;</td>
</tr>

<tr height="280"><td valign="top" class="contornotabla" colspan="2" >
    Lista de pedidos:<asp:Panel ID="Panel5" runat="server" Height="267px" 
        ScrollBars="Vertical">
        <asp:GridView ID="gvPedidos" runat="server" Width="100%" 
    AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="Num" ForeColor="#333333" 
        GridLines="None" BorderColor="White" BorderWidth="1px">
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="num" HeaderText="Num" InsertVisible="False" 
                ReadOnly="True" />
                <asp:BoundField DataField="Trabajador" HeaderText="Persona" ReadOnly="True" />
                <asp:BoundField DataField="descripcion_Cco" HeaderText="CeCo" />
                <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                <asp:BoundField DataField="importe_Ped" HeaderText="Importe (S/.)" />
                <asp:BoundField DataField="nombreInstancia_Ipl" HeaderText="Instancia" />
                <asp:BoundField DataField="descripcionEstado_Eped" HeaderText="Estado" />
                <asp:CommandField SelectText="" ShowSelectButton="True" />
            </Columns>
            <FooterStyle Font-Bold="True" ForeColor="White" />
            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    </asp:Panel>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="calificar" />
    </td></tr>
<tr><td colspan="2">
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
    
                </td></tr>
<tr height="430px" class="" valign="top"><td class="contornotabla" colspan="2">
    <table width="100%">
    <tr>
    <td style="color: #0000FF">
    <TABLE width="100%">
    <tr>
    <TD style="width:45%">
    <table width="100%">
    <tr><td>Pedido N°
    °
    <asp:TextBox ID="txtPedido" runat="server" Enabled="False"></asp:TextBox>
                                                </td><td>Ver:         <asp:LinkButton ID="lnkDatos" runat="server" ForeColor="#0000CC">Datos Generales</asp:LinkButton>
        |<asp:LinkButton ID="lnkRevisiones" runat="server" ForeColor="#0000CC">Revisiones</asp:LinkButton>
                                                    <br />
    </td></tr>
    <tr><td>Proceso&nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="cboPeriodoPresu" runat="server" Enabled="False">
            </asp:DropDownList>
            &nbsp;</td><td>Estado&nbsp;&nbsp;
            <asp:DropDownList ID="cboEstado" runat="server" Enabled="False">
            </asp:DropDownList>
    </td></tr>
    </table>
    </TD>
    <td><table width="100%">
    <tr>
    <td>
        <asp:RadioButtonList ID="rbOpciones" runat="server" 
            RepeatDirection="Horizontal" Width="256px" AutoPostBack="True" 
            Visible="False">
            <asp:ListItem Value="C">Conforme</asp:ListItem>
            <asp:ListItem Value="O">Observado</asp:ListItem>
            <asp:ListItem Value="R">Rechazado</asp:ListItem>
            <asp:ListItem Value="D">Derivar</asp:ListItem>
        </asp:RadioButtonList>
        </td>
    <td>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
            ControlToValidate="rbOpciones" 
            ErrorMessage="Seleccione una opción de evaluación del pedido" 
            ValidationGroup="calificar">*</asp:RequiredFieldValidator>
        </td>
    <td align="right">
        <asp:Button ID="cmdCalificar" runat="server" CssClass="guardar" 
            Text="Calificar" Width="95px" Height="23px" ValidationGroup="calificar" 
            Enabled="False" Visible="False" />
        </td>
    </tr>
    <tr>
    <td colsObs:
        <asp:TextBox ID="txtObservacion" runat="server" Width="90%"></asp:TextBox>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Ingrese una observación" 
            ValidationGroup="calificar" ClientValidationFunction="validarObservacion">*</asp:CustomValidator>
                                                </td>
    </tr>
    <tr>
    <td colspan=3 >
        <asp:Panel ID="pnlDerivar" runat="server" Visible="False">
            Derivar a:<asp:DropDownList ID="cboPersonalDerivar" runat="server">
            </asp:DropDownList>
        </asp:Panel>
                                                </td>
    </tr>
    </table></td>
    </tr>
    </TABLE>
    </td>
    </tr>
    <tr>
    <td>
            <hr />
        <asp:Panel ID="pnlDatos" runat="server" Visible="False">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" style="width:100%;">
                        <tr>
                            <td align="left" class="style1">
                                Centro de costo<asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                    runat="server" ControlToValidate="cboCecos" 
                                    ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Presupuesto" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCecos" runat="server" BackColor="#F3F3F3" Visible="False" 
                                    Width="90px"></asp:TextBox>
                                <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                &nbsp;</td>
                            <td>
                                <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue" 
                                    Visible="False">Busqueda Avanzada</asp:LinkButton>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                    AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <font style="color:Blue">Procesando. Espere un momento...</font></ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                    Width="100%">
                                    <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        DataKeyNames="codigo_cco" ForeColor="#333333" ShowHeader="False" Width="98%">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                            <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron items con el término de búsqueda</b>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="pnlPresupuesto" runat="server" ForeColor="Red" 
                                    HorizontalAlign="Left" Visible="False">
                                    Seleccione el ítem presupuestado que desea pedir:<asp:TextBox ID="txtDetPresup" 
                                        runat="server" Visible="False"></asp:TextBox>
                                    <asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" DataKeyNames="codigo_dpr,codigo_Ppr,codigo_Art" 
                                        ForeColor="#333333" GridLines="Horizontal" Width="90%">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Ppr" HeaderText="Prog. Presupuestal" />
                                            <asp:BoundField DataField="DesEstandar" HeaderText="Item" />
                                            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle Item" />
                                            <asp:BoundField DataField="PreUnitario" HeaderText="Precio Unit." />
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                        <tr bgcolor="#F5F9FC">
                            <td>
                                Prog. presupuestal<asp:CompareValidator ID="CompareValidator6" runat="server" 
                                    ControlToValidate="cboProgramaPresu" 
                                    ErrorMessage="Seleccione programa presupuestal" Operator="GreaterThan" 
                                    ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboProgramaPresu" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:RadioButtonList ID="rblMovimiento" runat="server" AutoPostBack="True" 
                                    RepeatDirection="Horizontal" ValidationGroup="BuscaItem" Visible="false">
                                    <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="E">Egreso</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server">
                                    Item<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtCodItem" 
                                        ErrorMessage="Busque el item que desea registrar" ValidationGroup="BuscaItem">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtConcepto" ErrorMessage="Selecione item a registrar" 
                                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <font style="color:Blue">Procesando. Espere un momento...</font></ProgressTemplate>
                                    </asp:UpdateProgress>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConcepto" runat="server" ValidationGroup="BuscaItem" 
                                    Width="500px"></asp:TextBox>
                                <asp:ImageButton ID="ImgBuscarItems" runat="server" 
                                    ImageUrl="~/images/busca.gif" ValidationGroup="BuscaItem" />
                                (clic aquí o presione enter)</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" 
                                    Width="100%">
                                    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        DataKeyNames="codigocon,tipo,iduni,especificaCantidad" ForeColor="#333333" 
                                        ShowHeader="False" Width="98%">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                            <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                            <asp:BoundField DataField="precio" HeaderText="Precio">
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron items con el término de búsqueda</b>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#D1DDF1" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td width="15%">
                                Detalle/Justificación
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentarioReq" runat="server" MaxLength="100" 
                                    TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="lblUnidad" runat="server" BackColor="#F3F3F3" ReadOnly="True" 
                                    Visible="false" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblTexto" runat="server" Text="Precio Unitario (S/.)"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                    ControlToValidate="txtPrecioUnit" ErrorMessage="Ingrese un precio válido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrecioUnit" runat="server" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                <asp:Label ID="lblValores" runat="server" Font-Bold="True" Text="Cantidad"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCantidad" runat="server" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Fecha</td>
                            <td>
                                <asp:TextBox ID="TxtFechaEsperada" runat="server" Width="80px"></asp:TextBox>
                                <input ID="Button2" class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaEsperada,'dd/mm/yyyy')" 
                                    style="height: 22px" type="button" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator11" runat="server" 
                                    ControlToValidate="TxtFechaEsperada" ErrorMessage="Ingrese fecha de nacimiento" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;</td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                Distribución</td>
                            <td>
                                <asp:RadioButtonList ID="rblModoDistribucion" runat="server" 
                                    AutoPostBack="True" RepeatDirection="Horizontal" ValidationGroup="BuscaItem">
                                    <asp:ListItem Selected="True" Value="C">Cantidad</asp:ListItem>
                                    <asp:ListItem Value="P">Porcentaje</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="cmdGuardar" runat="server" BorderStyle="Outset" 
                CssClass="guardar" Height="26px" Text="   Guardar detalle" 
                ValidationGroup="Guardar" Width="148px" />
            <br />
            <asp:GridView ID="gvDetallePedido" runat="server" 
                AutoGenerateColumns="False" CellPadding="4" 
                DataKeyNames="codigo_dpe,modoDistribucion_Dpe" ForeColor="#333333" 
                GridLines="None" Width="100%" Caption="Lista de ítems pedidos" 
                CaptionAlign="Left">
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:BoundField DataField="descripcionart" HeaderText="Artículo" 
                        SortExpression="descripcionart" />
                    <asp:BoundField DataField="descripcion_cco" HeaderText="CeCo" 
                        SortExpression="descripcion_cco" />
                    <asp:BoundField DataField="precioreferencial_dpe" HeaderText="Precio" 
                        SortExpression="precioreferencial_dpe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="cantidad_dpe" HeaderText="Cantidad" 
                        SortExpression="cantidad_dpe">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="subtotal" HeaderText="Subtotal" ReadOnly="True" 
                        SortExpression="subtotal">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Observacion_dpe" HeaderText="Observación" 
                        SortExpression="Observacion_dpe" />
                        
                          <asp:TemplateField HeaderText ="Espec. Téc." >
                            <ItemTemplate>
                                <asp:LinkButton ID="hlnkVerDetalle" runat="server" NavigateUrl="#" Text=<%#Iif(Eval("especificacion").ToString() <>"" or Eval("archivo_especificacion").toString()<>"" ,"Ver", "Ninguna")%> Enabled=<%#Iif(Eval("especificacion").ToString() <>"" or Eval("archivo_especificacion").toString()<>"", "True","False")%> ToolTip ="Especificaiones Técnicas"></asp:LinkButton>
                                <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlVerDetalle1" TargetControlID="hlnkVerDetalle"
                                    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlVerDetalle1" runat="server"  CssClass="modalPopup" align="center" style = "display:none">
                                    <table cellspacing="0" cellpadding="2" border="1" style="color:Black;background-color:White;width:90%;border-collapse:collapse;">
                                        <tr style="color:White;background-color:#FF3300;font-weight:bold;"><th colspan = 2 style="text-align:center;" id="titPanel" runat="server"  ><%#Eval("descripcionArt")%></th></tr>
                                        
                                        <tr >
                                            <td>Especificación Técnicas</td>
                                            <td><%#IIf(Eval("especificacion").ToString() = "", "Ninguna", Eval("especificacion"))%></td>
                                        </tr>
                                         <tr>
                                            <td>Archivo adjunto</td>
                                            <td> <img src="../images/attachment.png" /><asp:HyperLink  ID ="hlAdjunto" runat ="server" NavigateUrl ='<%#Iif( Eval("archivo_especificacion").toString()="","#",Eval("archivo_especificacion","archivos/{0}")) %>' Text ='<%#Iif(Eval("archivo_especificacion").toString() <>"",Eval("archivo_especificacion"),"Ninguno") %>' target="_blank" ></asp:HyperLink></td>
                                        
                                        </tr>
                                    </table>
                                    <br />
                                    <asp:Button ID="btnClose" runat="server" Text="Salir" />
                                </asp:Panel>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField >
                    <asp:CheckBoxField DataField="presupuestado" HeaderText="Presup.">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:CheckBoxField>
                    <asp:BoundField DataField="descripcionEstado_Eped" HeaderText="Estado" />
                    <asp:BoundField DataField="observacion_ecc" HeaderText="Motivo Eliminado" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Denegar" 
                        EditText="Denegar" Visible="False">
                        <ItemStyle HorizontalAlign="Center" ForeColor="#CC3300" />
                    </asp:CommandField>
                    <asp:CommandField SelectText="Distribuir" ShowSelectButton="True" 
                        Visible="False">
                        <ItemStyle HorizontalAlign="Center" ForeColor="#0000CC" />
                    </asp:CommandField>
                </Columns>
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
            <asp:Label ID="lblTitTotal" runat="server" ForeColor="Red" 
                Text="Total del pedido (S/.):"></asp:Label>
            <asp:Label ID="lblTotalDetalle" runat="server" Font-Bold="True" 
                Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC" Text="0.00"></asp:Label>
            <br />
            <br />
            <asp:Panel ID="pnlDistribuir" runat="server" ForeColor="#FF3300" 
                Visible="False">
                Distribución de:
                <asp:Label ID="lblNombreItem" runat="server" Font-Size="10pt" 
                    ForeColor="#0033CC"></asp:Label>
                <br />
                <asp:Label ID="lblCeCoDist" runat="server" Text="Centro Costos"></asp:Label>
                <asp:CompareValidator ID="CompareValidator7" runat="server" 
                    ControlToValidate="cboCecosEjecucion" 
                    ErrorMessage="Seleccione el centro de costos a distribuir" 
                    Operator="GreaterThan" ValidationGroup="Distribuir" ValueToCompare="0">*</asp:CompareValidator>
                &nbsp;<asp:DropDownList ID="cboCecosEjecucion" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblModoDistribucion" runat="server" Font-Size="12px" 
                    ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="txtCantidadDistribucion" 
                    ErrorMessage="Ingrese la distribución" ValidationGroup="Distribuir">*</asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox ID="txtCantidadDistribucion" runat="server" Width="52px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="cmdDistribuir" runat="server" BorderStyle="Outset" 
                    Text="Distribuir" ValidationGroup="Distribuir" />
                <asp:TextBox ID="txtCodigo_Ecc" runat="server" Visible="False" Width="26px"></asp:TextBox>
                <asp:TextBox ID="txtCodigoDetalle" runat="server" Visible="False" Width="26px"></asp:TextBox>
                <asp:GridView ID="gvDistribucion" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataKeyNames="codigo_ecc,codigo_Cco" ForeColor="#333333" 
                    GridLines="None" Width="90%">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#E3EAEB" />
                    <Columns>
                        <asp:BoundField DataField="Descripcion_cco" HeaderText="CeCo" />
                        <asp:BoundField DataField="cantidad" HeaderText="Distribución">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="modoDistribucion_Dpe" HeaderText="Modo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Precio" HeaderText="Distrib. Precio">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                        <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                    </Columns>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                Total:
                <asp:Label ID="lblTotalItem" runat="server" Font-Bold="True" Font-Size="10pt" 
                    Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Distribuido:
                <asp:Label ID="lblDistribuidoItem" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Por Distribuir:
                <asp:Label ID="lblPorDistribuir" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True"></asp:Label>
                <br />
                <div>
                </div>
                <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
                <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
            </asp:Panel>
        </asp:Panel>
        <asp:Panel ID="pnlRevision" runat="server" Visible="False">
        <table width="100%">
        <tr>
        <td width="50%" valign="top">
            <asp:GridView ID="gvRevisiones" runat="server" Caption="Revisiones del pedido" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        <td width="50%" valign="top">
            <asp:GridView ID="gvEstados" runat="server" Caption="Evolución del estado" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        </table>
            <br />
            <asp:GridView ID="gvObservaciones" runat="server" 
                Caption="Observaciones al pedido" CaptionAlign="Left" CellPadding="4" 
                ForeColor="#333333" GridLines="None" Width="97.5%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
        </asp:Panel>
    </td>
    </tr>
    </table>
    </td></tr>
</table>
    </form>
</body>
</html>
