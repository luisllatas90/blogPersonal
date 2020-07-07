<%@ Page Language="VB" AutoEventWireup="false" CodeFile="experiencia.aspx.vb" Inherits="experiencia" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Hoja de Vida :: Experiencia Laboral</title>
    <script language="JavaScript" src="../../../private/funciones.js"></script>
    <script language="JavaScript" src="../../../private/tooltip.js"></script>
      <link  href="private/expediente.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="private/expediente.js"></script>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    
    <style>
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
       
    body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
	}  
    </style>
</head>
<body>
<div id="divmensaje"></div>
<script type="text/javascript" src="div.js"></script>
<center>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    
        <table cellpadding="0" cellspacing="0" class="style1">
            <tr>
                <td align="left" valign="top" width="75%">
   
        <table cellpadding="0" cellspacing="0" class="tabla_personal" style="width: 100%;
            height: 100%">
            <tr>
                <td align="left" class="titulo_tabla" style="height: 29px">
                    &nbsp;Registro de Experiencia Laboral y Asistencia a Eventos</td>
                <td align="right" class="titulo_tabla" style="height: 29px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 04 de 06"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                    font-family: verdana; height: 27px" colspan="2">
                    &nbsp;Experiencia Laboral</td>
            </tr>
            <tr>
                <td align="center" style="padding-top: 5px; height: 147px" valign="top" 
                    colspan="2">
                    <table cellpadding="0" cellspacing="0" style="width: 98%">
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkVistaExperiencia" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CausesValidation="False" CssClass="tab_seleccionado" Height="27px"
                                    Width="126px">Datos Exp. Laboral</asp:LinkButton><asp:LinkButton ID="LinkAgregaExperiencia"
                                        runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
                                        CssClass="tab_normal" Height="27px" Style="text-align: center" Width="130px">Agregar Experiencia</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td align="center" class="borde_tab" style="height: 230px" valign="top">
                                <asp:Panel ID="Panel3" runat="server" Visible="False" Width="100%">
                                    <table id="tabla" class="borde_tab">
                                        <tr>
                                            <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                                <asp:Label ID="LblExperiencia" runat="server" Font-Bold="True" Font-Names="Verdana"
                                                    ForeColor="Navy"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items">
                                                &nbsp;Institución/Empresa</td>
                                            <td class="titulo_items">
                                                <asp:TextBox ID="TxtEmpresa" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="datos_combo" Width="415px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtEmpresa" ErrorMessage="Ingrese institucion o empresa que laboró"
                                                        SetFocusOnError="True" ValidationGroup="experiencia">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items">
                                                &nbsp;Ciudad</td>
                                            <td class="titulo_items" style="width: 415px">
                                                <asp:TextBox ID="TxtCiudad" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="datos_combo" Width="166px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtCiudad" ErrorMessage="Ingrese ciudad que laboró"
                                                        SetFocusOnError="True" ValidationGroup="experiencia">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items">
                                                &nbsp;Tipo de Contrato</td>
                                            <td class="titulo_items">
                                                <asp:DropDownList ID="DDLContrato" runat="server" CssClass="datos_combo" Width="174px">
                                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="DDLContrato"
                                                    ErrorMessage="Seleccione tipo de contrato" Operator="NotEqual" SetFocusOnError="True"
                                                    ValueToCompare="0" Width="7px" ValidationGroup="experiencia">*</asp:CompareValidator>
                                                Cargo &nbsp;<asp:DropDownList ID="DDLCargo" runat="server" CssClass="datos_combo"
                                                    Width="184px">
                                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="DDLCargo"
                                                    ErrorMessage="Seleccione cargo" Operator="NotEqual" SetFocusOnError="True" ValueToCompare="0" ValidationGroup="experiencia">*</asp:CompareValidator></td>
                                        </tr>
                                        <tr style="color: #808000">
                                            <td class="titulo_items">
                                                &nbsp;Función Desempeño</td>
                                            <td class="titulo_items" style="font-size: 10pt; color: olive; font-family: Verdana">
                                                <asp:TextBox ID="TxtFuncion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="datos_combo" Width="414px"></asp:TextBox></td>
                                        </tr>
                                        <tr style="color: #808000">
                                            <td class="titulo_items">
                                                &nbsp;Fechas</td>
                                            <td class="titulo_items">
                                                Inicio
                                                <asp:DropDownList ID="DDLMesIni" runat="server" CssClass="datos_combo" Width="68px">
                                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                    <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DDLAnioIni" runat="server" CssClass="datos_combo"
                                                    Width="65px">
                                                </asp:DropDownList>
                                                Fin
                                                <asp:DropDownList ID="DDLMesFin" runat="server" CssClass="datos_combo" Width="68px">
                                                    <asp:ListItem Value="0">En curso</asp:ListItem>
                                                    <asp:ListItem Value="1">Enero</asp:ListItem>
                                                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                    <asp:ListItem Value="4">Abril</asp:ListItem>
                                                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                    <asp:ListItem Value="6">Junio</asp:ListItem>
                                                    <asp:ListItem Value="7">Julio</asp:ListItem>
                                                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                    <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                </asp:DropDownList><asp:DropDownList ID="DDLAnioFin" runat="server" CssClass="datos_combo"
                                                    Width="68px">
                                                </asp:DropDownList><asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="validaexperiencia"
                                                    ErrorMessage="Ambas fecha de fin deben estar en curso o seleccione una fecha correcta" ValidationGroup="experiencia">*</asp:CustomValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items">
                                                &nbsp;Motivo de Cese</td>
                                            <td class="titulo_items" style="width: 415px">
                                                <asp:DropDownList ID="DDLCese" runat="server" CssClass="datos_combo" Enabled="False"
                                                    Width="170px">
                                                    <asp:ListItem Value="Laborando">-- Seleccione Motivo Cese --</asp:ListItem>
                                                    <asp:ListItem>Termino Contrato</asp:ListItem>
                                                    <asp:ListItem>Termino de Proyecto</asp:ListItem>
                                                    <asp:ListItem>Renuncia</asp:ListItem>
                                                    <asp:ListItem>Despido</asp:ListItem>
                                                    <asp:ListItem>Cambio de Puesto</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="valida2experiencia"
                                                    ErrorMessage="Seleccione motivo de cese" ValidationGroup="experiencia">*</asp:CustomValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items">
                                                &nbsp;Breve Descripción del &nbsp;<br />
                                                &nbsp;cargo desempeñado</td>
                                            <td id="mensaje" class="titulo_items" style="font-size: 10pt; color: olive; font-family: Verdana">
                                                <asp:TextBox ID="TxtDescripcion" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="datos_combo" Height="53px" MaxLength="400" TextMode="MultiLine"
                                                    Width="418px"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                                        runat="server" ControlToValidate="TxtDescripcion" ErrorMessage="Ingrese breve descripcion de labor desempeñada"
                                                        SetFocusOnError="True" ValidationGroup="experiencia">*</asp:RequiredFieldValidator></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="CmdGuardar" runat="server" CssClass="tab_normal" Height="24px" Text="Guardar"
                                                    ValidationGroup="experiencia" Width="85px" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" Width="100%">
                                            <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ObjExperiencia"
                                                Width="100%" DataKeyNames="codigo_exp" AllowPaging="True" PageSize="6" GridLines="Horizontal">
                                                <FooterStyle Font-Names="Arial" Font-Size="Small" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo_exp" HeaderText="Codigo" Visible="False"/>
                                                    <asp:BoundField DataField="descripcion_Car" HeaderText="Cargo" SortExpression="descripcion_Car" />
                                                    <asp:BoundField DataField="Funcion_Exp" HeaderText="Funcion" SortExpression="Funcion_Exp" />
                                                    <asp:BoundField DataField="empresa" HeaderText="Institucion" SortExpression="empresa" />
                                                    <asp:BoundField DataField="ciudad" HeaderText="Ciudad" SortExpression="ciudad" />
                                                    <asp:BoundField DataField="descripcion_Tco" HeaderText="Contrato" SortExpression="descripcion_Tco" />
                                                    <asp:BoundField DataField="inicio" HeaderText="F. Inicio" ReadOnly="True" SortExpression="inicio" />
                                                    <asp:BoundField DataField="fin" HeaderText="F. Fin" ReadOnly="True" SortExpression="fin" />
                                                    <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" ShowEditButton="True">
                                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                    </asp:CommandField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif"
                                                        ShowDeleteButton="True" DeleteText="Eliminar Registro" >
                                                        <ItemStyle Width="25px" />
                                                    </asp:CommandField>
                                                </Columns>
                                                <RowStyle HorizontalAlign="Center" CssClass="fila_datos" Height="23px" />
                                                <EmptyDataTemplate>
                                                    <table width="100%">
                                                        <tr>
                                                            <td align="center">
                                                                <span style="font-size: 10pt; color: #800000; font-family: Verdana">Usted no Tiene registrada
                                                        ninguna información sobre experiencia laboral.<br />
                                                        Haga click en agregar para registrar uno</span>.
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <PagerStyle Font-Names="Arial" Font-Size="X-Small" />
                                                <HeaderStyle CssClass="tab_normal" ForeColor="Black" Height="22px" />
                                            </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                    font-family: verdana; height: 27px" colspan="2">
                    &nbsp;Participación en Eventos - Seminarios - Talleres</td>
            </tr>
            <tr>
                <td align="right" style="padding-top: 5px" valign="top" colspan="2">
                    <table cellpadding="0" cellspacing="0" style="width: 98%">
                        <tr>
                            <td align="left">
                                <asp:LinkButton ID="LinkVistaEventos" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                    BorderWidth="1px" CausesValidation="False" CssClass="tab_seleccionado" Height="27px"
                                    Width="104px">Datos Eventos</asp:LinkButton><asp:LinkButton ID="LinkAgregaEventos"
                                        runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
                                        CssClass="tab_normal" Height="27px" Style="text-align: center" Width="106px">Agregar Eventos</asp:LinkButton></td>
                        </tr>
                        <tr>
                            <td align="center" class="borde_tab" style="height: 230px" valign="top">
                                <asp:Panel ID="Panel2" runat="server" Width="100%">
                                    &nbsp;&nbsp;<asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataSourceID="ObjEventos" DataKeyNames="codigo,Tipoevento" AllowPaging="True" PageSize="6" GridLines="Horizontal">
                                                <FooterStyle Font-Names="Arial" Font-Size="X-Small" />
                                                <RowStyle CssClass="fila_datos" Height="23px" />
                                                <EmptyDataTemplate>
                                                    <strong style="font-weight: normal; font-size: 10pt; color: maroon; font-family: verdana;
                                                        text-align: center">
                                                        <table style="font-size: 10pt" width="100%">
                                                            <tr>
                                                                <td align="center">
                                                                    <span style="color: #800000">Usted no Tiene registrada información de asistencia a eventos.<br />
                                                        Haga click en agregar para registrar uno. </span>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </strong>
                                                </EmptyDataTemplate>
                                                <PagerStyle Font-Names="Arial" Font-Size="X-Small" />
                                                <HeaderStyle HorizontalAlign="Center" CssClass="tab_normal" ForeColor="Black" Height="22px" />
                                                <Columns>
                                                    <asp:BoundField DataField="codigo" HeaderText="codigo" SortExpression="codigo" Visible="False" />
                                                    <asp:BoundField DataField="Tipoevento" HeaderText="Evento" ReadOnly="True" SortExpression="Tipoevento" >
                                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcion_tev" HeaderText="Tipo Evento" SortExpression="descripcion_tev" >
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" SortExpression="descripcion" >
                                                        <ItemStyle HorizontalAlign="Center" Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="organizado" HeaderText="Organizado por" SortExpression="organizado" />
                                                    <asp:BoundField DataField="descripcion_tpa" HeaderText="Participacion" SortExpression="descripcion_tpa" />
                                                    <asp:BoundField DataField="inicio" DataFormatString="{0:dd-MM-yyyy}" HeaderText="F. Inicio"
                                                        SortExpression="inicio" HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="fin" DataFormatString="{0:dd-MM-yyyy}" HeaderText="F. Termino"
                                                        SortExpression="fin" HtmlEncode="False" >
                                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/editar.gif" Text="Modificar registro" Visible="False" >
                                                        <ItemStyle HorizontalAlign="Center" Width="4%" />
                                                    </asp:ButtonField>
                                                    <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif" DeleteText="Eliminar registro" ShowDeleteButton="True" >
                                                        <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                    </asp:CommandField>
                                                </Columns>
                                            </asp:GridView>
                                            </asp:Panel>
                                <asp:Panel ID="Panel4" runat="server" Visible="False" Width="100%">
                                    <table id="Table1" class="borde_tab">
                                        <tr>
                                            <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                                <asp:Label ID="LblEventos" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Navy"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items" colspan="2">
                                                Buscar Eventos por: &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items" colspan="2">
                                                &nbsp;<asp:RadioButton ID="RbAcademico" runat="server" AutoPostBack="True" Checked="True"
                                                    GroupName="tipoeven" Text="Academico" />
                                                <asp:RadioButton ID="RbSocial" runat="server" AutoPostBack="True" GroupName="tipoeven"
                                                    Text="Social" Width="60px" />
                                                de tipo:
                                                <asp:DropDownList ID="DDLTIpo" runat="server" CssClass="datos_combo" Width="94px">
                                                </asp:DropDownList><asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif"
                                                    Style="cursor: hand" />
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DDLTIpo"
                                                    ErrorMessage="Seleccione tipo de evento" Operator="NotEqual" ValidationGroup="buscar"
                                                    ValueToCompare="0" Width="1px">*</asp:CompareValidator><asp:CompareValidator ID="CompareValidator4"
                                                        runat="server" ControlToValidate="DDLTIpo" ErrorMessage="Seleccione tipo de evento"
                                                        Operator="NotEqual" ValidationGroup="eventos" ValueToCompare="0" Width="1px">*</asp:CompareValidator><asp:DropDownList
                                                            ID="DDLClaseEven" runat="server" CssClass="datos_combo" Width="106px">
                                                        </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtBuscar" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="datos_combo" Width="437px"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtBuscar" ErrorMessage="Ingrese Texto de busqueda"
                                                        ValidationGroup="buscar">*</asp:RequiredFieldValidator>
                                                <asp:Button ID="CmdBuscar" runat="server" CssClass="tab_normal" Text="Buscar" ValidationGroup="buscar"
                                                    Width="78px" /></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items" colspan="2" valign="top">
                                                &nbsp;Eventos Registrados:
                                                <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="validalista"
                                                    ErrorMessage="Seleccione un elemento de la lista" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" valign="top">
                                                <asp:ListBox ID="LstEventos" runat="server" CssClass="datos_combo" Height="90px"
                                                    Style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                                    border-bottom: black 1px solid" Width="100%"></asp:ListBox></td>
                                        </tr>
                                        <tr>
                                            <td class="titulo_items" colspan="2">
                                                Ingrese el (los) tipo(s) de participacion que tuvo en el evento.
                                                <asp:Button ID="CmdDetalle" runat="server" CssClass="tab_normal" OnClientClick="javascript: abrir(); return false;"
                                                    Text="Detalle Ev." Width="70px" CausesValidation="False" />
                                                <asp:CustomValidator ID="CustomValidator6" runat="server" ClientValidationFunction="validacheck"
                                                    ErrorMessage="Debe marcar por lo menos 1 tipo de participacion" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:CheckBoxList ID="ChkParticipa" runat="server" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="titulo_items" RepeatColumns="4" Width="100%">
                                                </asp:CheckBoxList>
                                                </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" colspan="2" id="filaocultar" align="right">
                                                <table id="otros" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td class="titulo_items" colspan="2">
                                                            &nbsp;Ingresar datos si el evento no se encuentra en lista.</td>
                                                    </tr>
                                                    <tr>
                                                        <td class="titulo_items">
                                                            &nbsp;Nombre</td>
                                                        <td class="titulo_items">
                                                            <asp:TextBox ID="TxtOtro" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" CssClass="datos_combo" Width="392px"></asp:TextBox>
                                                            <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validaevento"
                                                                ErrorMessage="Ingrese Nombre de Evento" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="titulo_items">
                                                            &nbsp;Organizado por</td>
                                                        <td class="titulo_items">
                                                            <asp:TextBox ID="TxtOrganizado" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" CssClass="datos_combo" Width="392px"></asp:TextBox>
                                                            <asp:CustomValidator ID="CustomValidator4" runat="server" ClientValidationFunction="validaorganiza"
                                                                ErrorMessage="Nombre de organizacion requerido" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="titulo_items">
                                                            &nbsp;Fecha Inicio</td>
                                                        <td class="titulo_items">
                                                            <asp:DropDownList ID="DDLIniDia" runat="server" CssClass="datos_combo">
                                                                <asp:ListItem Value="0">Dia</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>7</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                                <asp:ListItem>9</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLIniMes" runat="server" CssClass="datos_combo"
                                                                Width="72px">
                                                                <asp:ListItem Value="0">Mes</asp:ListItem>
                                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                                <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLIniAño" runat="server" CssClass="datos_combo">
                                                            </asp:DropDownList>
                                                            <asp:CustomValidator ID="CustomValidator8" runat="server" ClientValidationFunction="validafecha1"
                                                                ErrorMessage="Fecha de Inicio Incorrecta" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="titulo_items">
                                                            &nbsp;Fecha Termino</td>
                                                        <td class="titulo_items">
                                                            <asp:DropDownList ID="DDLFinDIa" runat="server" CssClass="datos_combo">
                                                                <asp:ListItem Value="0">Dia</asp:ListItem>
                                                                <asp:ListItem>1</asp:ListItem>
                                                                <asp:ListItem>2</asp:ListItem>
                                                                <asp:ListItem>3</asp:ListItem>
                                                                <asp:ListItem>4</asp:ListItem>
                                                                <asp:ListItem>5</asp:ListItem>
                                                                <asp:ListItem>6</asp:ListItem>
                                                                <asp:ListItem>7</asp:ListItem>
                                                                <asp:ListItem>8</asp:ListItem>
                                                                <asp:ListItem>9</asp:ListItem>
                                                                <asp:ListItem>10</asp:ListItem>
                                                                <asp:ListItem>11</asp:ListItem>
                                                                <asp:ListItem>12</asp:ListItem>
                                                                <asp:ListItem>13</asp:ListItem>
                                                                <asp:ListItem>14</asp:ListItem>
                                                                <asp:ListItem>15</asp:ListItem>
                                                                <asp:ListItem>16</asp:ListItem>
                                                                <asp:ListItem>17</asp:ListItem>
                                                                <asp:ListItem>18</asp:ListItem>
                                                                <asp:ListItem>19</asp:ListItem>
                                                                <asp:ListItem>20</asp:ListItem>
                                                                <asp:ListItem>21</asp:ListItem>
                                                                <asp:ListItem>22</asp:ListItem>
                                                                <asp:ListItem>23</asp:ListItem>
                                                                <asp:ListItem>24</asp:ListItem>
                                                                <asp:ListItem>25</asp:ListItem>
                                                                <asp:ListItem>26</asp:ListItem>
                                                                <asp:ListItem>27</asp:ListItem>
                                                                <asp:ListItem>28</asp:ListItem>
                                                                <asp:ListItem>29</asp:ListItem>
                                                                <asp:ListItem>30</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLFinMes" runat="server" CssClass="datos_combo"
                                                                Width="73px">
                                                                <asp:ListItem Value="0">Mes</asp:ListItem>
                                                                <asp:ListItem Value="1">Enero</asp:ListItem>
                                                                <asp:ListItem Value="2">Febrero</asp:ListItem>
                                                                <asp:ListItem Value="3">Marzo</asp:ListItem>
                                                                <asp:ListItem Value="4">Abril</asp:ListItem>
                                                                <asp:ListItem Value="5">Mayo</asp:ListItem>
                                                                <asp:ListItem Value="6">Junio</asp:ListItem>
                                                                <asp:ListItem Value="7">Julio</asp:ListItem>
                                                                <asp:ListItem Value="8">Agosto</asp:ListItem>
                                                                <asp:ListItem Value="9">Setiembre</asp:ListItem>
                                                                <asp:ListItem Value="10">Octubre</asp:ListItem>
                                                                <asp:ListItem Value="11">Noviembre</asp:ListItem>
                                                                <asp:ListItem Value="12">Diciembre</asp:ListItem>
                                                            </asp:DropDownList>&nbsp;<asp:DropDownList ID="DDLFinAño" runat="server" CssClass="datos_combo">
                                                            </asp:DropDownList>
                                                            <asp:CustomValidator ID="CustomValidator9" runat="server" ClientValidationFunction="validafecha2"
                                                                ErrorMessage="Fecha de Termino Incorrecta" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td class="titulo_items">
                                                            &nbsp;Duración</td>
                                                        <td class="titulo_items">
                                                            <asp:TextBox ID="TxtHoras" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                                BorderWidth="1px" CssClass="datos_combo" Style="text-align: right" Width="36px"></asp:TextBox>&nbsp;<asp:DropDownList
                                                                    ID="DDLDuracion" runat="server" Width="54px" CssClass="datos_combo">
                                                                    <asp:ListItem Value="1">Hora(s)</asp:ListItem>
                                                                    <asp:ListItem Value="2">Dia(s)</asp:ListItem>
                                                                    <asp:ListItem Value="3">Mes(es)</asp:ListItem>
                                                                    <asp:ListItem Value="4">A&#241;o(s)</asp:ListItem>
                                                                </asp:DropDownList><asp:CustomValidator ID="CustomValidator7" runat="server" ClientValidationFunction="duracion"
                                                                ErrorMessage="Ingrese Duracion" ValidationGroup="eventos">*</asp:CustomValidator></td>
                                                    </tr>
                                                </table>
                                                <asp:Button ID="CmdGuardarEvento" runat="server" CssClass="tab_normal" Height="24px"
                                                    Text="Guardar" ValidationGroup="eventos" Width="85px" /></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <br />
                    &nbsp;&nbsp;<asp:Button ID="CmdGuardar0" runat="server" CssClass="tab_normal"
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
                                    <asp:Label ID="Label21" runat="server" Text="Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                </td>
                            </tr>
                            </table>
                        </asp:Panel>
                        <table style="width: 100%;background-color:White; height:30px">
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hfTipo" runat="server" />
                                </td>
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
                    <table cellpadding="0" cellspacing="0" class="style1">
                        <tr>
                            <td width="20%">
                                <table align="left">
                                    <tr>
                                        <td>
                                        <a href="personales.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="1. Datos personales" src="images/hojavida/datospersonales.gif" /></td>
                                        </a>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="perfil.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>"> 
                                            <img border=0 alt="2. Perfil profesional" src="images/hojavida/perfilprofesional.gif" />
                                        </a>
                                        </td>
                                        
                                    </tr>
                                    <tr align="left">
                                        <td align="left" class="style2">
                                        <a href="educacionuniversitaria.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="3. Formación Académica" src="images/hojavida/formacionacademica.gif" />
                                        </a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="experiencia.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">                                         
                                            <img border=0 alt="4. Experiencia laboral y asistencia a eventos" 
                                                src="images/hojavida/experiencialaboral_r.gif" />
                                        </a>    
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        <a href="otros.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="5. Otros datos" src="images/hojavida/otrosdatos.gif" />
                                        </a>
                                            </td>
                                    </tr>
									<tr>
                                        <td>
                                        <a href="futuroempleo.aspx?menu= <%=Request.querystring("menu")%>&id=<%=Request.querystring("id")%>&ctf=<%=Request.querystring("ctf")%>">
                                            <img border=0 alt="6. Futuro Empleo" src="images/hojavida/futuroempleo.gif" />
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
                                            <asp:Label ID="Label23" runat="server" Text="Los datos que consigno a continuación son  de carácter de DECLARACION JURADA y por tanto asumo plena responsabilidad por la veracidad de los mismos."></asp:Label>
                                        </td>
                                     </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
        <asp:ObjectDataSource ID="ObjEventos" runat="server" DeleteMethod="QuitarEventos"
            SelectMethod="ObtieneDatosEventos" TypeName="Personal">
            <DeleteParameters>
                <asp:Parameter Name="CodEvenPro" Type="Int32" />
                <asp:Parameter Name="tipoeven" Type="Int32" />
            </DeleteParameters>
            <SelectParameters>
                <asp:Parameter DefaultValue="PR" Name="tipo" Type="String" />
                <asp:SessionParameter DefaultValue="" Name="idpersonal" SessionField="id" Type="String" />
                <asp:Parameter DefaultValue="&quot;&quot;" Name="param2" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
                                            <asp:ObjectDataSource ID="ObjExperiencia" runat="server" DeleteMethod="QuitarExperiencia"
                                                SelectMethod="ObtieneDatosExperiencia" TypeName="Personal">
                                                <DeleteParameters>
                                                    <asp:Parameter Name="codexperiencia" Type="Int32" />
                                                </DeleteParameters>
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="idpersonal" SessionField="id" Type="Object" />
                                                    <asp:Parameter Name="tipo" DefaultValue="PE" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
        <asp:HiddenField ID="HddExperiencia" runat="server" />
        <asp:HiddenField ID="HddEvento" runat="server" />
        <asp:HiddenField ID="HddLista" runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="experiencia" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="eventos" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" DisplayMode="List"
            ShowMessageBox="True" ShowSummary="False" ValidationGroup="buscar" />
    
        </ContentTemplate>
        <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                <asp:PostBackTrigger ControlID="CmdGuardarEvento"/>
                <asp:PostBackTrigger ControlID="CmdGuardar"/>
                <asp:PostBackTrigger ControlID="btnCancelar"/>
                <asp:PostBackTrigger ControlID="LinkVistaExperiencia"/>
                <asp:PostBackTrigger ControlID="LinkAgregaExperiencia"/>
                <asp:PostBackTrigger ControlID="LinkVistaEventos"/>
                <asp:PostBackTrigger ControlID="LinkAgregaEventos"/>
         </Triggers>
        
</asp:UpdatePanel>
    
    </form>
    </center>
</body>
</html>
