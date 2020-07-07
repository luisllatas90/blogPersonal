<%@ Page Language="VB" AutoEventWireup="false" CodeFile="distinciones.aspx.vb" Inherits="distinciones" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <script language="JavaScript" src="../../../private/funciones.js"></script>
    <link rel="STYLESHEET" href="private/estilo.css"/>
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="private/expediente.js"></script>
    <script src="private/calendario.js"></script>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    
    <style type="text/css">
        .tab_pasar
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_sobre.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_seleccionado
        {
            font-weight: bold;
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_seleccion.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .tab_normal
        {
            font-size: 8pt;
            background: white;
            background-image: url(images/boton_normal.gif);
            background-repeat: repeat-x;
            font-family: Tahoma;
            color: black;
            vertical-align: middle;
            text-align: center;
        }
        .style1
        {
            width: 100%;
        }
       
        .style2
        {
            height: 22px;
        }
       
    </style>
</head>
<body >
<center>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager> 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
           
           <table cellpadding="0" cellspacing="0" class="style1">
               <tr>
                   <td align="left" valign="top" width="75%">
           <table cellpadding="0" cellspacing="0" class="tabla_personal" style="width: 100%;">
            <tr>
                <td align="left" class="titulo_tabla" style="height: 29px">
                    &nbsp;Registro de Distinciones y Publicaciones</td>
                <td align="right" class="titulo_tabla" style="height: 29px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 06 de 07"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                    font-family: verdana; height: 27px" colspan="2">
                    &nbsp;Registrar Distinciones y Publicaciones</td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 5px; height: 147px" valign="top" colspan="2">
                    <table cellpadding="0" cellspacing="0" style="width: 98%">
                            <tr>
                                <td align="left">
                                    <asp:LinkButton ID="LinkVistaDistinciones" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                        BorderWidth="1px" CausesValidation="False" CssClass="tab_seleccionado" Height="27px"
                                        Width="126px">Datos Registrados</asp:LinkButton><asp:LinkButton ID="LinkAgregaDistinciones"
                                            runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
                                            CssClass="tab_normal" Height="27px" Style="text-align: center" Width="130px">Agregar Datos</asp:LinkButton></td>
                            </tr>
                            <!--*** REGISTROS -->
                                <asp:Panel ID="Panel3" runat="server" Visible="False" Width="100%">
                                                <tr>
                                                    <!-- aqui vamos a ponder la parte del registro -->
                                                    <td align="center" class="borde_tab" style="width:100%" valign="top">
                                                        <!-- Tabla para las distinciones -->
                                                                <table id="tabla" class="borde_tab">
                                                                <tr>
                                                                    <td colspan="2" style="border-bottom: gold 1px solid; height: 22px" class="titulo_items">
                                                                        <asp:Label ID="LblExperiencia" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                                            ForeColor="Navy"></asp:Label></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Nombre de Distinción</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtDistincion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="1px" ToolTip="Ingrese nombre de la distincion que recibió."
                                                                            Width="420px" CssClass="datos_combo"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtDistincion"
                                                                            ErrorMessage="Nombre de Distincion Requerida" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Otorgado por</td>
                                                                    <td>
                                                                        <asp:TextBox ID="TxtOtorgado" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="1px" ToolTip="Ingrese un nombre de la institucion o persona que hizo entrega de la distincion."
                                                                            Width="420px" CssClass="datos_combo"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtOtorgado"
                                                                            ErrorMessage="Ingrese Institución o persona que hizo entrega la distinción."
                                                                            SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Ciudad</td>
                                                                    <td style="width: 415px" align="left">
                                                                        <asp:TextBox ID="TxtCiudad" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="1px" ToolTip="Ingrese nombre de la ciudad donde se le hizo entrega de la distincion"
                                                                            Width="166px" CssClass="datos_combo"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCiudad"
                                                                            ErrorMessage="Ingrese ciudad de entrega de distincion" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Tipo de Distinción</td>
                                                                    <td style="width: 415px" align="left">
                                                                        <asp:DropDownList ID="DDLDistinciones" runat="server" Width="171px" CssClass="datos_combo">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Fecha de Entrega</td>
                                                                    <td align="left">
                                                                        <asp:TextBox ID="TxtFecha" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="1px" Width="68px" CssClass="datos_combo"></asp:TextBox><input
                                                                                id="Button2" class="cunia" onclick="MostrarCalendario('TxtFecha')" type="button" />
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtFecha"
                                                                            ErrorMessage="Fecha de Entrega Requerida">*</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="titulo_items">
                                                                        &nbsp;Breve Descripción de 
                                                                        <br />
                                                                        &nbsp;motivo de entrega de &nbsp; &nbsp;<br />
                                                                        &nbsp;la distincion.</td>
                                                                    <td id="mensaje" style="font-size: 10pt; color: olive; font-family: Verdana">
                                                                        <asp:TextBox ID="TxtDescripcion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                            BorderWidth="1px" Height="53px"
                                                                            MaxLength="400" TextMode="MultiLine" Width="420px" CssClass="datos_combo"></asp:TextBox><asp:RequiredFieldValidator
                                                                                ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtDescripcion"
                                                                                ErrorMessage="Ingrese breve descripcion de motivo de entrega de distincion" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="2" style="font-size: 10pt; color: olive; font-family: Verdana">
                                                                        <asp:Button ID="CmdGuardar" runat="server" CssClass="tab_normal" Height="24px" Text="Guardar"
                                                                            Width="85px"  />       </td>
                                                                </tr>
                                                            </table>
                                                        <!--end distinciones -->
                                                    </td>
                                                    <!-- fin de los formularios de registro -->
                                                </tr>
                                                <tr>
                                                    <td align="center" class="borde_tab" style="width:100%" valign="top">
                                                        <!-- Publicaciones -->
                                                        <table ID="Table2" class="borde_tab">
                                                                                <tr>
                                                                                    <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                                                                        <asp:Label ID="LblExperiencia4" runat="server" Font-Bold="True" 
                                                                                            Font-Names="Verdana" ForeColor="Navy"></asp:Label>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">
                                                                                        Nombre de Publicación</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtnombre" runat="server" BorderColor="Black" 
                                                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="datos_combo" 
                                                                                            ToolTip="Ingrese nombre de la distincion que recibió." Width="420px"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                                                                                            ControlToValidate="txtnombre" ErrorMessage="Nombre de publicación Requerida" 
                                                                                            SetFocusOnError="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">
                                                                                        &nbsp;Editorial</td>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txteditorial" runat="server" BorderColor="Black" 
                                                                                            BorderStyle="Solid" BorderWidth="1px" CssClass="datos_combo" 
                                                                                            ToolTip="Ingrese un nombre de la institucion o persona que hizo entrega de la distincion." 
                                                                                            ValidationGroup="1" Width="420px"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                                                                                            ControlToValidate="txteditorial" ErrorMessage="Nombre de editorial requerida" 
                                                                                            SetFocusOnError="True" ValidationGroup="1">*</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">&nbsp;Procedencia</td>
                                                                                    <td align="left" style="width: 415px">
                                                                                        <asp:DropDownList ID="combo_procedencia" runat="server" CssClass="datos_combo" 
                                                                                            Width="171px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">&nbsp;Autoría</td>
                                                                                    <td align="left" style="width: 415px">
                                                                                        <asp:DropDownList ID="combo_autoria" runat="server" CssClass="datos_combo" 
                                                                                            Width="171px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">&nbsp;Tipo</td>
                                                                                    <td align="left" style="width: 415px">
                                                                                        <asp:DropDownList ID="combo_tipo" runat="server" AutoPostBack="true" 
                                                                                            CssClass="datos_combo" Width="171px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td class="titulo_items">&nbsp;<asp:Label ID="lblTexto" runat="server" Text="Indexado"></asp:Label></td>
                                                                                    <td align="left" style="width: 415px">
                                                                                        <asp:DropDownList ID="combo_sino" runat="server" CssClass="datos_combo" 
                                                                                            Width="171px">
                                                                                        </asp:DropDownList>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right" colspan="2" style="font-size: 10pt; color: olive; font-family: Verdana">
                                                                                        <asp:Button ID="cmd_GuardarPublicacion" runat="server" CssClass="tab_normal" Height="24px" Text="Guardar" ValidationGroup="1" Width="85px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                        <!-- fin de registro de publicaciones -->
                                                    </td>
                                                </tr>
                                </asp:Panel>  
                            <!--*** REGISTROS -->
                                <asp:Panel ID="Panel2" runat="server" Visible="False" Width="100%">
                                </asp:Panel>
                                <tr>
                            <td align="center" >
                                
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                            &nbsp;
                            </td>
                        </tr>
                     </table>
                     
                       <table cellpadding="0" cellspacing="0" style="width: 98%">
                          <tr>
                            <td align="left"  class="borde_tab" valign="top">
                                 <table>
                                    <tr>
                                            <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                                                    font-family: verdana; height: 27px; text-align:left;">
                                                    &nbsp;Registros de Distinciones
                                            </td>
                                    </tr>
                                 </table>
                                    
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" DataKeyNames="codigo_dis" DataSourceID="ObjDistinciones"
                                                PageSize="6" Width="100%" GridLines="Horizontal" 
                                     EmptyDataText="Usted no Tiene registrada ninguna información sobre distinciones ">
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_dis" HeaderText="Codigo" InsertVisible="False"
                                                        ReadOnly="True" SortExpression="codigo_dis" Visible="False">
                                                        <ItemStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcion_tdis" HeaderText="Tipo Distincion" SortExpression="descripcion_tdis">
                                                        <ItemStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="nombre_dis" HeaderText="Distincion" SortExpression="nombre_dis" />
                                                    <asp:BoundField DataField="otorgado_dis" HeaderText="Otorgado por" SortExpression="otorgado_dis" />
                                                    <asp:BoundField DataField="ciudad_dis" HeaderText="Ciudad" SortExpression="ciudad_dis" />
                                                    <asp:BoundField DataField="motivo_dis" HeaderText="Motivo" SortExpression="motivo_dis" />
                                                    <asp:BoundField DataField="fechaentrega_dis" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Fecha Entrega"
                                                        HtmlEncode="False" SortExpression="fechaentrega_dis">
                                                        <ItemStyle Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" ShowEditButton="True">
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:CommandField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif"
                                                        ShowDeleteButton="True" DeleteText="Eliminar Registro">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <RowStyle CssClass="fila_datos" Height="23px" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tab_normal" ForeColor="Black" Height="25px" />
                                                <EmptyDataTemplate>
                                                    <span style="font-family: Verdana"><span style="color: #800000">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <span style="font-size: 10pt; color: #800000; font-family: Verdana">Usted no
                                                        Tiene registrada ninguna información sobre distinciones <br />
                                                                        Haga click en agregar para registrar uno</span>.
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </span><span style="color: #800000"> </span>
                                                    </span>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                  
                                  <table>
                                    <tr>
                                            <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                                                    font-family: verdana; height: 27px; text-align:left;">
                                                    &nbsp;Registros de Publicaciones
                                            </td>
                                    </tr>
                                 </table>
                                    
                                            
                                       
                                    <!--<asp:Panel ID="Panel4" runat="server" Width="100%"> -->
                                        <asp:GridView ID="GridView2" runat="server" AllowPaging="True" 
                                                    AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="codigo_pub" 
                                                    GridLines="Horizontal" PageSize="6" Width="100%" 
                                     
                                     EmptyDataText="Usted no tiene registrada ninguna información sobre publicaciones.  Haga click en agregar para registrar">
                                                    <Columns>
                                                        <asp:BoundField DataField="codigo_pub" HeaderText="Codigo" 
                                                            InsertVisible="False" ReadOnly="True" SortExpression="codigo_pub" 
                                                            Visible="False">
                                                            <ItemStyle Width="20px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nombre" HeaderText="Nombre de Publicación" 
                                                            SortExpression="nombre">
                                                            <ItemStyle Width="100px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="editorial" HeaderText="Editorial" 
                                                            SortExpression="editorial" />
                                                        <asp:BoundField DataField="procedencia" HeaderText="Procedencia" 
                                                            SortExpression="procedencia" />
                                                        <asp:BoundField DataField="autoria" HeaderText="Autoría" 
                                                            SortExpression="autoria" />
                                                        <asp:BoundField DataField="tipo" HeaderText="Tipo de Publicación" 
                                                            SortExpression="tipo" />
                                                        <asp:BoundField DataField="info" HeaderText="Información" 
                                                            SortExpression="info" />
                                                        <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" 
                                                            ShowEditButton="True">
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif" 
                                                            DeleteText="Eliminar Registro" ShowDeleteButton="True">
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <RowStyle CssClass="fila_datos" Height="23px" />
                                                    <HeaderStyle CssClass="tab_normal" ForeColor="Black" Height="25px" 
                                                        HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <span style="font-family: Verdana"><span style="color: #800000">
                                                        <table width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <span style="font-size: 10pt; color: #800000; font-family: Verdana">
                                                                    Usted no 
                                                                    Tiene registrada ninguna información sobre publicaciones.<br />
                                                                    Haga click en agregar para registrar</span>.
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        </span><span style="color: #800000"></span></span>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                    &nbsp;
                                    <!--</asp:Panel>-->
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;&nbsp;&nbsp;<asp:Button ID="CmdGuardar0" runat="server" CssClass="tab_normal"
                    Height="26px" Text="&lt;&lt; Anterior" Width="86px" />&nbsp;&nbsp;
                    <asp:Button ID="CmdGuardar1" runat="server" CssClass="tab_normal"
                    Height="26px" Text="Siguiente &gt;&gt;" Width="86px" />&nbsp;&nbsp;&nbsp;
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="ibtnMostrarPopUpInforme" runat="server" Height="5px" CssClass="hidden" />
                    <asp:Panel ID="pnlContedorInforme" runat="server" Style="display: none; Width:400px"  CssClass="modalPopup">
                        <asp:Panel ID="pnlCabeceraInforme" runat="server" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;height:300px">
                            
                            <table style="width: 100%;background-color:White;">
                            <tr>
                                <td style="width: 98%">
                                    <div style="background-color: #e8eef7; color: #3366CC; font-weight: bold;" >
                                        <asp:Label ID="lblTitPopUpInforme" runat="server" Text="DECLARACIÓN JURADA"></asp:Label>
                                    </div>
                                </td>
                                <td style="width: 2%">
                                    <asp:ImageButton ID="ImageButton3" runat="server" 
                                        ImageUrl="~/images/cerrar.gif" />
                                </td>
                            </tr>
                             <tr>
                                <td style="text-align:justify">
                                    <asp:Label ID="lblDeclarante" runat="server" Text=""></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label21" runat="server" Text="1.- Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label19" runat="server" Text="2.- La información consignada deberá ser utilizada por la Universidad para informar a SUNAT en forma obligatoria, conforme  a las normas legales vigentes."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label22" runat="server" Text="3.- Dicha información deberá ser actualizada obligatoriamente por mi persona, previa coordinación con la Direcciónde Personal cuando haya un cambio en los datos, o cuando la USAT lo solicite."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label24" runat="server" Text="4.- Luego del presente registro me comprometo a entregar en un plazo de 5 días hábiles, las copias de los documentos sustentatorios de los datos consignados (DNI, actas de nacimiento y de matrimonio, etc) las cuales serán fiel copia de los originales que obran en mi poder."></asp:Label>
                               </td>
                            </tr>
                            <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label25" runat="server" Text="5.- Reconozco que la información que no sustente documentalmente no estará validada por la Dirección de Personal, y por tanto no será tomada en cuenta."></asp:Label>
                               </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White; height:30px">
                            <tr>
                                <td style="width: 50%" align="center">
                                    <asp:Button ID="btnGuardarInforme" 
                                    runat="server" Text="            Acepto" 
                                    CssClass="conforme1" 
                                    Height="35px" Width="100px" 
                                    ValidationGroup="btnGuardarInforme" 
                                    ToolTip="Guardar" />
                                </td>
                                 <td style="width: 50%" align="center">
                                    <asp:Button ID="btnCancelar" 
                                    runat="server" Text="           No Acepto" 
                                    CssClass="rechazar_inv" 
                                    Height="35px" Width="100px" 
                                    ToolTip="Cancelar" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <ajaxtoolkit:modalpopupextender 
                    ID="mpeInforme" runat="server" 
                    TargetControlID="ibtnMostrarPopUpInforme"
                    PopupControlID="pnlContedorInforme"
                    BackgroundCssClass="modalBackground"
                    DropShadow="true"
                    PopupDragHandleControlID="pnlCabeceraInforme" 
                    />
                </td>
            </tr>
        </table>
                                        </td>
                   <td align="left" valign="top" width="25%">
                    <!-- Tabla de tabs y lo declaración jurada  -->
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td width="20%">
                            <!-- Tabs de controles : -->
                                <table align="left">
                                    <tr>
                                        <td>
                                        <a href="personales.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Datos personales" 
                                                src="images/hojavida/datospersonales.gif" /></td>
                                        </a>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="perfil.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="Perfil profesional" src="images/hojavida/perfilprofesional.gif" />
                                        </a>
                                        </td>
                                        
                                    </tr>
                                    <tr align="left">
                                        <td align="left" class="style2">
                                        <a href="educacionuniversitaria.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Títulos y grados académicos" src="images/hojavida/titulosygrados.gif" />
                                        </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="idiomas.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Idiomas y otros cursos" src="images/hojavida/idiomasyotros.gif" />
                                        </a>    
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="experiencia.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Experiencia laboral y asistencia a eventos" src="images/hojavida/exepriencia.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="distinciones.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="Distinciones y Publicaciones" 
                                                src="images/hojavida/distinciones_r.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="otros.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="Otros datos" src="images/hojavida/otrosdatos.gif" />
                                        </a>
                                            </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="80%" valign="top">
                                <table cellpadding="3" cellspacing="3" class="tabla_personal" 
                                    style="background-color: #FFFFCC; border: 1px solid #000080">
                                    <!--<tr>
                                        <td align="left">
                                            <font face="Arial" size="2"><span style="FONT-SIZE: 10pt; FONT-FAMILY: Arial">
                                            <b>Estimado trabajador:</b><br />
                                            <br />
                                            Sírvase completar esta información para actualizar su Ficha Personal.<br />
                                            <br />
                                            Esta información será verificada y utilizada por la Universidad, para fines 
                                            académicos y administrativos.<br />
                                            <br />
                                            Muchas Gracias</span></font></td>
                                    </tr>
                                     <tr>
                                        <td>
                                            <hr />
                                        </td>
                                    </tr>-->
                                    <tr>
                                       <td style="text-align:justify">
                                           <asp:Label ID="Label29" runat="server" Text="DECLARACIÓN JURADA" 
                                               Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                     </tr>
                                    <tr>
                                       <td style="text-align:justify">
                                            <asp:Label ID="Label23" runat="server" Text="1.- Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                        </td>
                                     </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label26" runat="server" Text="2.- La información consignada deberá ser utilizada por la Universidad para informar a SUNAT en forma obligatoria, conforme  a las normas legales vigentes."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label27" runat="server" Text="3.- Dicha información deberá ser actualizada obligatoriamente por mi persona, previa coordinación con la Direcciónde Personal cuando haya un cambio en los datos, o cuando la USAT lo solicite."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                               <td style="text-align:justify">
                                    <asp:Label ID="Label28" runat="server" Text="4.- Luego del presente registro me comprometo a entregar en un plazo de 5 días hábiles, las copias de los documentos sustentatorios de los datos consignados (DNI, actas de nacimiento y de matrimonio, etc) las cuales serán fiel copia de los originales que obran en mi poder."></asp:Label>
                               </td>
                            </tr>
                                    <tr>
                                         <td style="text-align:justify">
                                                <asp:Label ID="Label20" runat="server" Text="5.- Reconozco que la información que no sustente documentalmente no estará validada por la Dirección de Personal, y por tanto no será tomada en cuenta."></asp:Label>
                                         </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                   </td>
               </tr>
           </table>
           <br />
                                            <asp:ObjectDataSource ID="ObjDistinciones" runat="server" DeleteMethod="QuitarDistinciones"
                                                SelectMethod="ObtieneDistinciones" TypeName="Personal">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="codigo_dis" Type="Int32" />
                                                </DeleteParameters>
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="idpersonal" SessionField="id" Type="Int32" />
                                                    <asp:Parameter DefaultValue="DP" Name="tipo" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
        <asp:HiddenField ID="HddDistinciones" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" />
            
         </ContentTemplate>
             <Triggers>
                    <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                    <asp:PostBackTrigger ControlID="CmdGuardar"/>
                    <asp:PostBackTrigger ControlID="btnCancelar"/>
                    <asp:PostBackTrigger ControlID="LinkVistaDistinciones"/>
                    <asp:PostBackTrigger ControlID="LinkAgregaDistinciones"/>
            </Triggers>   
    </asp:UpdatePanel> 
    </form>
    </center>
</body>
</html>
