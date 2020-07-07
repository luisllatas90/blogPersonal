<%@ Page Language="VB" AutoEventWireup="false" CodeFile="idiomas.aspx.vb" Inherits="idiomas" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Campus Virtual : Hoja de Vida</title>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/tooltip.js"></script>
    <link  href="private/expediente.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="private/expediente.js"></script>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />

    <script type = "text/javascript">
        function ValidateCheckBox(sender, args) {
            if (document.getElementById("<%=TxtOtroCentroArea.ClientID %>").text== "") {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script> 
    
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
       
    body{ font-family: "Trebuchet MS", "Lucida Console", Arial, san-serif;
	color: Black;font-size:8pt;
	font: normal;
	}
       
    </style>
</head>
<body>
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
                 &nbsp;Registro de Idiomas y Otros Cursos</td>
             <td align="right" class="titulo_tabla" style="height: 29px">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 04 de 07"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
         </tr>
         <tr>
             <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                 font-family: verdana; height: 27px" colspan="2">
                 &nbsp;Idiomas Extranjeros</td>
         </tr>
         <tr>
             <td align="center" style="padding-top: 5px; height: 147px" valign="top" 
                 colspan="2">
                 <table cellpadding="0" cellspacing="0" style="width: 98%">
                     <tr>
                         <td align="left">
                             <asp:LinkButton ID="LinkVistaIdioma" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                 BorderWidth="1px" CausesValidation="False" CssClass="tab_seleccionado" Height="27px"
                                 Width="111px">Datos Idiomas</asp:LinkButton><asp:LinkButton ID="LinkAgregaIdioma"
                                     runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
                                     CssClass="tab_normal" Height="27px" Style="text-align: center" Width="113px">Agregar Idiomas</asp:LinkButton></td>
                     </tr>
                     <tr>
                         <td align="center" class="borde_tab" style="height: 230px" valign="top">
                             <asp:Panel ID="Panel3" runat="server" Visible="False" Width="100%">
                                 <table id="tabla" class="borde_tab">
                                     <tr>
                                         <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                             <asp:Label ID="LblIdioma" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Navy"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Idioma</td>
                                         <td class="titulo_items">
                                             <asp:DropDownList ID="DDLIdioma" runat="server" CssClass="datos_combo">
                                             </asp:DropDownList>
                                             <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLIdioma"
                                                 ErrorMessage="Seleccione idioma" MaximumValue="8000" MinimumValue="1" SetFocusOnError="True"
                                                 Type="Integer" ValidationGroup="Idioma">*</asp:RangeValidator>Año de graduación
                                             <asp:DropDownList ID="DDlAno" runat="server" CssClass="datos_combo" Width="62px">
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Tipo Institución</td>
                                         <td class="titulo_items">
                                             <asp:DropDownList ID="DDLInstitucion" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                 Width="199px">
                                             </asp:DropDownList>
                                             <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DDLInstitucion"
                                                 ErrorMessage="Seleccione tipo de institucion" MaximumValue="8000" MinimumValue="1"
                                                 SetFocusOnError="True" ValidationGroup="Idioma">*</asp:RangeValidator></td>
                                     </tr>
                                     <tr style="color: #000000">
                                         <td class="titulo_items">
                                             &nbsp;Procedencia</td>
                                         <td class="titulo_items">
                                             <asp:DropDownList ID="DDLProcedencia" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                 Width="111px">
                                                 <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                 <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Centro de Estudios</td>
                                         <td class="titulo_items" style="height: 17px">
                                             <asp:DropDownList ID="DDLCentro" runat="server" CssClass="datos_combo" Width="422px">
                                             </asp:DropDownList>
                                             <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="DDLCentro"
                                                 ErrorMessage="Seleccione centro de estudios" MaximumValue="8000" MinimumValue="1"
                                                 SetFocusOnError="True" Type="Integer" ValidationGroup="Idioma">*</asp:RangeValidator></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items" style="font-size: 10pt; width: 131px; color: olive; font-family: Verdana">
                                         </td>
                                         <td id="mensaje" class="titulo_items">
                                             Otros Especifique
                                             <asp:TextBox ID="TxtOtros" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                 BorderWidth="1px" CssClass="datos_combo" Width="316px"></asp:TextBox>
                                             <asp:CustomValidator 
                                                ID="CustomValidator1" 
                                                runat="server" 
                                                ClientValidationFunction="validaidioma"
                                                ErrorMessage="Ingrese centro de estudios en otros" 
                                                ValidationGroup="Idioma">*</asp:CustomValidator></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Situación</td>
                                         <td class="titulo_items" style="height: 21px">
                                             <asp:DropDownList ID="DDLSituacion" runat="server" CssClass="datos_combo" Width="116px">
                                             </asp:DropDownList>
                                             <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                 ErrorMessage="Seleccione Situacion de Estudios de Idioma" MaximumValue="8000"
                                                 MinimumValue="1" Type="Integer" ValidationGroup="Idioma">*</asp:RangeValidator></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Niveles</td>
                                         <td class="titulo_items" style="height: 15px">
                                             Lee&nbsp;<asp:DropDownList ID="DDLLee" runat="server" CssClass="datos_combo">
                                                 <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                 <asp:ListItem Value="1">Medio</asp:ListItem>
                                                 <asp:ListItem Value="2">Alto</asp:ListItem>
                                             </asp:DropDownList>
                                             Escribe
                                             <asp:DropDownList ID="DDLEscribe" runat="server" CssClass="datos_combo">
                                                 <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                 <asp:ListItem Value="1">Medio</asp:ListItem>
                                                 <asp:ListItem Value="2">Alto</asp:ListItem>
                                             </asp:DropDownList>
                                             Habla&nbsp;<asp:DropDownList ID="DDLHabla" runat="server" CssClass="datos_combo">
                                                 <asp:ListItem Value="0">Bajo</asp:ListItem>
                                                 <asp:ListItem Value="1">Medio</asp:ListItem>
                                                 <asp:ListItem Value="2">Alto</asp:ListItem>
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Observaciones</td>
                                         <td style="height: 42px">
                                             <asp:TextBox ID="TXtObservaciones" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                 BorderWidth="1px" CssClass="datos_combo" Height="32px" TextMode="MultiLine" Width="421px"></asp:TextBox></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                         </td>
                                         <td align="right">
                                             <asp:Button ID="CmdGuardar" 
                                                            runat="server" 
                                                            CssClass="tab_normal" 
                                                            Height="24px" Text="Guardar"
                                                            ValidationGroup="Idioma" 
                                                            Width="85px" /></td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                             <asp:Panel ID="Panel1" runat="server" Width="100%">
                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataSourceID="ObjIdioma" Width="100%" Height="1px" DataKeyNames="codigo_ipr" GridLines="Horizontal" AllowPaging="True" PageSize="6">
                                            <Columns>
                                                <asp:BoundField DataField="descripcion_Idi" HeaderText="Idioma" SortExpression="descripcion_Idi" >
                                                    <ItemStyle Font-Size="8pt" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre_Ins" HeaderText="Centro de Estudios" SortExpression="nombre_Ins" >
                                                    <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="aniograduacion" HeaderText="A&#241;o Graduacion" SortExpression="aniograduacion" >
                                                    <ItemStyle Font-Size="8pt" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="lee" HeaderText="Niv. Lectura" ReadOnly="True" SortExpression="lee" >
                                                    <ItemStyle Font-Size="8pt" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="escribe" HeaderText="Niv. Escritura" ReadOnly="True" SortExpression="escribe" >
                                                    <ItemStyle Font-Size="8pt" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="habla" HeaderText="Niv. Habla" ReadOnly="True" SortExpression="habla" >
                                                    <ItemStyle Font-Size="8pt" />
                                                </asp:BoundField>
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" ShowCancelButton="False"
                                                    ShowEditButton="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:CommandField>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif"
                                                    ShowDeleteButton="True" DeleteText="Eliminar Registro" >
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <RowStyle HorizontalAlign="Center" CssClass="fila_datos" Height="23px" />
                                            <EmptyDataTemplate>
                                                <strong style="font-weight: normal; font-size: 10pt; color: maroon;
                                                    font-family: verdana;">
                                                Lo sentimos Usted no tiene registrado ningun estudio en idiomas
                                                    extranjeros.<br />
                                                Haga click en agregar para registrar uno</strong>.
                                            </EmptyDataTemplate>
                                            <HeaderStyle ForeColor="Black" CssClass="tab_normal" Height="23px" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                        &nbsp;</asp:Panel>
                         </td>
                     </tr>
                 </table>
             </td>
         </tr>
         <tr>
             <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                 font-family: verdana; height: 27px" colspan="2">
                 &nbsp;Otros Estudios Realizados</td>
         </tr>
         <tr>
             <td align="right" style="padding-top: 5px" valign="top" colspan="2">
                 <table cellpadding="0" cellspacing="0" style="width: 98%">
                     <tr>
                         <td align="left">
                             <asp:LinkButton ID="LinkVistaOtros" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                 BorderWidth="1px" CausesValidation="False" CssClass="tab_seleccionado" Height="27px"
                                 Width="150px">Datos Otros Estudios</asp:LinkButton><asp:LinkButton ID="LinkAgregaOtros" runat="server"
                                     BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False"
                                     CssClass="tab_normal" Height="27px" Style="text-align: center" Width="159px">Agregar Otros Estudios</asp:LinkButton></td>
                     </tr>
                     <tr>
                         <td align="center" class="borde_tab" style="height: 230px" valign="top">
                             <asp:Panel ID="Panel2" runat="server" Width="100%">
                                        <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataKeyNames="codigo_opr" DataSourceID="ObjOtros" GridLines="Horizontal" Font-Names="Tahoma" ForeColor="Black" AllowPaging="True" PageSize="6">
                                            <RowStyle Font-Names="Arial" Font-Size="8pt" CssClass="fila_datos" Height="23px" />
                                            <EmptyDataTemplate>
                                                <strong style="font-weight: normal; font-size: 10pt; color: maroon; font-family: verdana;
                                                    text-align: center">Lo sentimos Usted no tiene registrado información de otros estudios realizados.<br />
                                                Haga click en agregar para registrar uno. </strong>
                                            </EmptyDataTemplate>
                                            <HeaderStyle HorizontalAlign="Center" CssClass="tab_normal" Height="22px" />
                                            <Columns>
                                                <asp:BoundField DataField="Des_AreaEs" HeaderText="Area" SortExpression="Des_AreaEs">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="estudios" HeaderText="Estudio" SortExpression="estudios">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="centroestudios" HeaderText="Institucion" ReadOnly="True"
                                                    SortExpression="centroestudios">
                                                    <ItemStyle Width="150px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Otrosinicio" HeaderText="Inicio" ReadOnly="True" SortExpression="Otrosinicio">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="OtrosFin" HeaderText="Fin" ReadOnly="True" SortExpression="OtrosFin">
                                                    <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="modalidad" HeaderText="Modalidad" ReadOnly="True" SortExpression="modalidad">
                                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CicloActual" HeaderText="Ciclo" ReadOnly="True" SortExpression="CicloActual">
                                                    <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                </asp:BoundField>
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" ShowCancelButton="False"
                                                    ShowEditButton="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:CommandField>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif"
                                                    ShowDeleteButton="True" DeleteText="Eliminar Registro">
                                                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                        &nbsp;</asp:Panel>
                             <asp:Panel ID="Panel4" runat="server" Visible="False" Width="100%">
                                 <table id="Table1" class="borde_tab">
                                     <tr>
                                         <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                             <asp:Label ID="LblOtros" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Navy"></asp:Label></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Area de Estudio</td>
                                         <td>
                                             <asp:DropDownList ID="DDLArea" runat="server" CssClass="datos_combo" Width="228px">
                                             </asp:DropDownList>
                                             <asp:RangeValidator 
                                                    ID="RangeValidator5" 
                                                    runat="server" 
                                                    ControlToValidate="DDLArea"
                                                    ErrorMessage="Seleccione area de estudio" 
                                                    MaximumValue="8000" 
                                                    MinimumValue="1" 
                                                    SetFocusOnError="True" 
                                                    ValidationGroup="otros">
                                                    *</asp:RangeValidator>
                                                    
                                                    </td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Nombre de Estudio</td>
                                         <td>
                                             <asp:TextBox ID="TxtEstudio" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                 BorderWidth="1px" CssClass="datos_combo" Width="435px"></asp:TextBox>
                                                    
                                                                <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator1" 
                                                                runat="server" 
                                                                ControlToValidate="TxtEstudio" 
                                                                ErrorMessage="Ingrese Nombre de Estudio"
                                                                SetFocusOnError="True" 
                                                                ValidationGroup="otros">*</asp:RequiredFieldValidator>
            
                                                                
                                                                </td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Tipo Institucion</td>
                                         <td>
                                             <asp:DropDownList ID="DDLTipoInsArea" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                 Width="199px">
                                             </asp:DropDownList>
                                                 </td>
                                     </tr>
                                     <tr style="color: #000000">
                                         <td class="titulo_items">
                                             &nbsp;Procedencia</td>
                                         <td style="height: 2px">
                                             <asp:DropDownList ID="DDLProcArea" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                 Width="111px">
                                                 <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                 <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                             </asp:DropDownList>
                                             <asp:Label ID="lblCod" runat="server" Text="Label"></asp:Label>
                                         </td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Centro de Estudios</td>
                                         <td style="height: 2px">
                                             <asp:DropDownList ID="DDLCentroArea" runat="server" CssClass="datos_combo" 
                                                 Width="437px" AutoPostBack="True">
                                             </asp:DropDownList>
                                             <asp:RangeValidator 
                                                    ID="RangeValidator7" 
                                                    runat="server" 
                                                    ControlToValidate="DDLCentroArea"
                                                    ErrorMessage="Seleccione centro de estudios" 
                                                    MaximumValue="8000" 
                                                    MinimumValue="1"
                                                    SetFocusOnError="True" 
                                                    Type="Integer" 
                                                    ValidationGroup="otros">*</asp:RangeValidator>
                                               </td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                         </td>
                                         <td id="Td1" class="titulo_items">
                                             Otros Especifique
                                             <asp:TextBox ID="TxtOtroCentroArea" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                 BorderWidth="1px" CssClass="datos_combo" Width="333px"></asp:TextBox>
                                                    
                                                    <asp:CustomValidator 
                                                    ID="CustomValidator2" 
                                                    runat="server" 
                                                    ClientValidationFunction="validaotros"
                                                    ErrorMessage="Ingrese centro de estudios en otros" 
                                                    ValidationGroup="otros">*</asp:CustomValidator>
                                         </td>
                                                  
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Fecha de Estudios</td>
                                         <td class="titulo_items">
                                             Inicio Mes
                                             <asp:DropDownList ID="DDLMesIni" runat="server" CssClass="datos_combo" Width="68px">
                                                 <asp:ListItem Value="Enero">Enero</asp:ListItem>
                                                 <asp:ListItem Value="Febrero">Febrero</asp:ListItem>
                                                 <asp:ListItem Value="Marzo">Marzo</asp:ListItem>
                                                 <asp:ListItem Value="Abril">Abril</asp:ListItem>
                                                 <asp:ListItem Value="Mayo">Mayo</asp:ListItem>
                                                 <asp:ListItem Value="Junio">Junio</asp:ListItem>
                                                 <asp:ListItem Value="Julio">Julio</asp:ListItem>
                                                 <asp:ListItem Value="Agosto">Agosto</asp:ListItem>
                                                 <asp:ListItem Value="Setiembre">Setiembre</asp:ListItem>
                                                 <asp:ListItem Value="Octubre">Octubre</asp:ListItem>
                                                 <asp:ListItem Value="Noviembre">Noviembre</asp:ListItem>
                                                 <asp:ListItem Value="Diciembre">Diciembre</asp:ListItem>
                                             </asp:DropDownList>
                                             Año&nbsp;<asp:DropDownList ID="DDLAnioIni" runat="server" CssClass="datos_combo"
                                                 Width="65px">
                                             </asp:DropDownList>
                                             Fin Mes&nbsp;<asp:DropDownList ID="DDLMesFin" runat="server" CssClass="datos_combo"
                                                 Width="68px">
                                                 <asp:ListItem Value="En curso">En curso</asp:ListItem>
                                                 <asp:ListItem Value="Enero">Enero</asp:ListItem>
                                                 <asp:ListItem Value="Febrero">Febrero</asp:ListItem>
                                                 <asp:ListItem Value="Marzo">Marzo</asp:ListItem>
                                                 <asp:ListItem Value="Abril">Abril</asp:ListItem>
                                                 <asp:ListItem Value="Mayo">Mayo</asp:ListItem>
                                                 <asp:ListItem Value="Junio">Junio</asp:ListItem>
                                                 <asp:ListItem Value="Julio">Julio</asp:ListItem>
                                                 <asp:ListItem Value="Agosto">Agosto</asp:ListItem>
                                                 <asp:ListItem Value="Setiembre">Setiembre</asp:ListItem>
                                                 <asp:ListItem Value="Octubre">Octubre</asp:ListItem>
                                                 <asp:ListItem Value="Noviembre">Noviembre</asp:ListItem>
                                                 <asp:ListItem Value="Diciembre">Diciembre</asp:ListItem>
                                             </asp:DropDownList>
                                             Año<asp:DropDownList ID="DDLAnioFin" runat="server" CssClass="datos_combo" Width="70px">
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Tipo estudio</td>
                                         <td class="titulo_items">
                                             <asp:DropDownList ID="DDLModalidad" runat="server" CssClass="datos_combo">
                                                 <asp:ListItem Value="1">Mensual</asp:ListItem>
                                                 <asp:ListItem Value="2">Semestral</asp:ListItem>
                                                 <asp:ListItem Value="3">Anual</asp:ListItem>
                                             </asp:DropDownList>
                                             Mes/Semestre/Año que cursa actualmente :
                                             <asp:DropDownList ID="DDLCursa" runat="server" CssClass="datos_combo" Width="55px">
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
                                                 <asp:ListItem Value="999">Culminado</asp:ListItem>
                                             </asp:DropDownList></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Situacion</td>
                                         <td>
                                             <asp:DropDownList ID="DDLSitArea" runat="server" CssClass="datos_combo" Width="116px">
                                             </asp:DropDownList>
                                              
                                                
                                             </td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                             &nbsp;Observaciones</td>
                                         <td>
                                             <asp:TextBox ID="TxtObservacionArea" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                 BorderWidth="1px" CssClass="datos_combo" Height="25px" TextMode="MultiLine" Width="438px"></asp:TextBox></td>
                                     </tr>
                                     <tr>
                                         <td class="titulo_items">
                                         </td>
                                         <td align="right">
                                             <asp:Button ID="CmdGuardarOtros" 
                                                runat="server" 
                                                CssClass="tab_normal" Height="24px"
                                                Text="Guardar" 
                                                ValidationGroup="otros" 
                                                Width="85px" /></td>
                                     </tr>
                                 </table>
                             </asp:Panel>
                         </td>
                     </tr>
                     
                 </table>
                 &nbsp;<br />
                &nbsp;<asp:Button ID="CmdGuardar0" runat="server" CssClass="tab_normal"
                    Height="26px" Text="&lt;&lt; Anterior" Width="86px" />&nbsp;&nbsp;
                 <asp:Button ID="CmdGuardar1" runat="server" CssClass="tab_normal"
                    Height="26px" Text="Siguiente &gt;&gt;" Width="86px" />&nbsp;&nbsp;&nbsp;
                 <br />
&nbsp;</td>
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
                                            <img border=0 alt="Idiomas y otros cursos" 
                                                src="images/hojavida/idiomasyotros_r.gif" />
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
                                            <img border=0 alt="Distinciones y honores" src="images/hojavida/distinciones.gif" />
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
                                        <asp:ObjectDataSource ID="ObjIdioma" runat="server" SelectMethod="ObtieneDatosIdiomas"
                                            TypeName="Personal" DeleteMethod="Quitaridiomas">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="idPersonal" SessionField="id" Type="String" />
                                                <asp:Parameter DefaultValue="DO" Name="tipo" Type="String" />
                                            </SelectParameters>
                                            <DeleteParameters>
                                                <asp:Parameter Name="codidioma" Type="Int32" />
                                            </DeleteParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="ObjOtros" runat="server" DeleteMethod="QuitarOtros" SelectMethod="ObtieneDatosOtros"
                                            TypeName="Personal">
                                            <DeleteParameters>
                                                <asp:Parameter Name="codotros" Type="Int32" />
                                            </DeleteParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="idpersonal" SessionField="id" Type="String" />
                                                <asp:Parameter DefaultValue="PE" Name="tipo" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
             <asp:HiddenField ID="HddIdioma" runat="server" />
             <asp:HiddenField ID="HddOtros" runat="server" />
             <asp:ValidationSummary ID="ValidationSummary1" 
                                    runat="server" 
                                    DisplayMode="List"
                                    ShowMessageBox="True" 
                                    ShowSummary="False" 
                                    ValidationGroup="Idioma" />
             <asp:ValidationSummary ID="ValidationSummary2" 
                                    runat="server" 
                                    DisplayMode="List"
                                    ShowMessageBox="True" 
                                    ShowSummary="False" 
                                    ValidationGroup="otros" />
                 
    </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                
                <asp:PostBackTrigger ControlID="CmdGuardar"/>
                <asp:PostBackTrigger ControlID="btnCancelar"/>
                <asp:PostBackTrigger ControlID="LinkVistaIdioma"/>
                <asp:PostBackTrigger ControlID="LinkAgregaIdioma"/>
                <asp:PostBackTrigger ControlID="LinkVistaOtros"/>
                <asp:PostBackTrigger ControlID="LinkAgregaOtros"/>
                                
                
            </Triggers>
   </asp:UpdatePanel>
   </form>
</body>
</html>

