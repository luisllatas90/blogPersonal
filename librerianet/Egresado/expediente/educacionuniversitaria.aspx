<%@ Page Language="VB" AutoEventWireup="false" CodeFile="educacionuniversitaria.aspx.vb" Inherits="educacionuniversitaria" title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Campus Virtual : Hoja de Vida</title>
     <link  href="private/expediente.css" rel="stylesheet" type="text/css"  />
      
      <script type="text/javascript" src="private/expediente.js"></script>
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
<center>
    <table cellpadding="0" cellspacing="0" class="style1">
        <tr>
            <td align="left" valign="top" width="75%">
    <table cellpadding="0" cellspacing="0" class="tabla_personal" 
                    style="height: 100%; width: 100%;" width="100%">
        <tr>
            <td align="left" style="height: 29px" class="titulo_tabla">
                &nbsp;Registro de Títulos Profesionales y Grados Académicos</td>
            <td align="right" style="height: 29px" class="titulo_tabla">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Verdana" 
                    Font-Size="10pt" ForeColor="White" Text="Paso 03 de 07"></asp:Label>
                &nbsp;&nbsp;&nbsp; </td>
        </tr>
        <tr>
            <td align="left" style="font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid;
                font-family: verdana; height: 27px" colspan="2">
                &nbsp;Titulos Profesionales</td>
        </tr>
        <tr>
            <td align="center" style="height: 147px; padding-top: 5px;" valign="top" 
                colspan="2">
                <table style="width: 98%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:LinkButton ID="LinkVistaTitulo" runat="server" CssClass="tab_seleccionado" Height="27px" Width="110px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False" >Datos 
                            Título</asp:LinkButton><asp:LinkButton
                                    ID="LinkAgregaTitulo" runat="server" CssClass="tab_normal" Height="27px" Style="text-align: center;"
                                    Width="110px" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px" CausesValidation="False">Agregar 
                            Títulos</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 230px" align="center" class="borde_tab" valign="top">
                            <asp:Panel ID="Panel3" runat="server" Visible="False" Width="100%">
                                <table id="tabla" class="borde_tab">
                                    <tr>
                                        <td class="titulo_items" colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                            <asp:Label ID="LblTitulo" runat="server" Font-Bold="True" Font-Names="Verdana" ForeColor="Navy"></asp:Label>
                                            <asp:Label ID="lblCodigo" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Título Profesional</td>
                                        <td class="titulo_items">
                                            <asp:DropDownList ID="DDLTitulo" runat="server"
                                                Width="429px" CssClass="datos_combo">
                                            </asp:DropDownList><asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="DDLTitulo"
                                                ErrorMessage="Seleccion un Titulo Profesional" MaximumValue="80000" MinimumValue="1"
                                                SetFocusOnError="True" Type="Integer" ValidationGroup="titulo">*</asp:RangeValidator><asp:Image ID="Img" runat="server" ImageUrl="~/images/menus/prioridad_.gif" Style="cursor: hand"
                                                ToolTip="Si el titulo no se encuentra en la lista anterior, seleccione OTRO y escriba el nombre en la caja de texto" /></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                        </td>
                                        <td class="titulo_items">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtrosTitulo" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px"
                                                Width="316px" CssClass="datos_combo"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="titulo"
                                                ErrorMessage="Ingrese un Titulo en Otros" ValidationGroup="titulo">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr style="color: #808000">
                                        <td class="titulo_items">
                                            &nbsp;Año</td>
                                        <td class="titulo_items">
                                            Ingreso
                                            <asp:DropDownList ID="DDlAIngreso" runat="server"
                                                Width="58px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            Egreso &nbsp;<asp:DropDownList ID="DDLAEgreso" runat="server" Width="63px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            Titulación &nbsp;<asp:DropDownList ID="DDLATitulo" runat="server" Width="64px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            <asp:CompareValidator 
                                                ID="CompareValidator1" 
                                                runat="server" 
                                                ControlToCompare="DDLAEgreso"
                                                ControlToValidate="DDlAIngreso" 
                                                ErrorMessage="Año de ingreso debe ser menor que de el egreso"
                                                Operator="LessThanEqual" 
                                                SetFocusOnError="True" 
                                                Type="Integer" ValidationGroup="titulo">*</asp:CompareValidator>
                                         </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Tipo Institución</td>
                                        <td class="titulo_items">
                                            <asp:DropDownList ID="DDLInstitucion" runat="server" AutoPostBack="True" Width="199px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="DDLInstitucion"
                                                ErrorMessage="Seleccione tipo de institucion" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True" ValidationGroup="titulo">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Procedencia</td>
                                        <td class="titulo_items">
                                            <asp:DropDownList ID="DDLProcedencia" runat="server" AutoPostBack="True" Width="111px" CssClass="datos_combo">
                                                <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                            </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Centro de Estudios</td>
                                        <td class="titulo_items">
                                            <asp:DropDownList ID="DDLCentro" runat="server"
                                                Width="432px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            <asp:RangeValidator 
                                                ID="RangeValidator3" 
                                                runat="server" 
                                                ControlToValidate="DDLCentro"
                                                ErrorMessage="Seleccione centro de estudios" 
                                                MaximumValue="8000" 
                                                MinimumValue="1"
                                                SetFocusOnError="True" 
                                                Type="Integer" 
                                                ValidationGroup="titulo">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                        </td>
                                        <td id="mensaje" class="titulo_items">
                                            Otros Especifique :
                                            <asp:TextBox ID="TxtOtros" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" Width="318px" CssClass="datos_combo"></asp:TextBox>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="estudios"
                                                ErrorMessage="Ingrese Centro de Estudios en otros" ValidationGroup="titulo">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Situación</td>
                                        <td class="titulo_items">
                                            <asp:DropDownList ID="DDLSituacion" runat="server"
                                                Width="116px" CssClass="datos_combo">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="DDLSituacion"
                                                ErrorMessage="Seleccione Situacion de Titulo" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer" ValidationGroup="titulo">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items" style="height: 35px">
                                        </td>
                                        <td align="right" style="height: 35px">
                                            <asp:Button ID="CmdGuardar" 
                                            runat="server" 
                                            CssClass="tab_normal" 
                                            Text="Guardar" 
                                            Width="85px" 
                                            Height="24px" 
                                            ValidationGroup="titulo" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:Panel ID="Panel1" runat="server"  Width="100%">
                                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="codigo_TPr" DataSourceID="ObjTitulos" Width="100%" AllowPaging="True" PageSize="6" GridLines="Horizontal">
                                            <RowStyle CssClass="fila_datos" Height="25px" />
                                            <HeaderStyle CssClass="tab_normal" Font-Names="Tahoma" ForeColor="Black" Height="22px" />
                                            <Columns>
                                                <asp:BoundField DataField="descripcion_Tpf" HeaderText="Titulo" SortExpression="descripcion_Tpf">
                                                    <ItemStyle   HorizontalAlign="Left" Width="300px" Wrap="False" />
                                                    <HeaderStyle HorizontalAlign="Center" Width="300px" Wrap="False" CssClass="tab_normal" />
                                                    <FooterStyle Width="300px" Wrap="False" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="nombre_Ins" HeaderText="Institucion" SortExpression="nombre_Ins">
                                                    <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="descripcion_Sit" HeaderText="Situacion" SortExpression="descripcion_Sit" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anioIngreso_TPr" HeaderText="Ingreso" SortExpression="anioIngreso_TPr" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anioEgreso_TPr" HeaderText="Egreso" SortExpression="anioEgreso_TPr" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anioGrad_TPr" HeaderText="Titulacion" SortExpression="anioGrad_TPr" >
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="codigo_TPr" HeaderText="codigo_TPr" InsertVisible="False"
                                                    SortExpression="codigo_TPr" Visible="False" />
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" InsertVisible="False"
                                                    ShowCancelButton="False" ShowEditButton="True" />
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif" DeleteText="Eliminar Registro" ShowDeleteButton="True" ShowHeader="True" >
                                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <span style="color: #800000; font-family: Verdana; vertical-align: middle; text-align: center;">
                                                    <table style="font-size: 8pt; width: 100%; height: 100%">
                                                        <tr>
                                                            <td style="font-size: 8pt; color: maroon; font-family: verdana; text-align: center">
                                                                Lo sentimos Usted no tiene registrado ningún título profesional.
                                                    <br />
                                                                Haga click en agregar para registrar uno nuevo.</td>
                                                        </tr>
                                                    </table>
                                                </span> 
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="fila_datos" />
                                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjTitulos" runat="server" DeleteMethod="QuitarTitulos"
                                            SelectMethod="ObtieneDatosTitulos" TypeName="Personal">
                                            <DeleteParameters>
                                                <asp:Parameter Name="CodTitPro" Type="Int32" />
                                            </DeleteParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="idpersonal" SessionField="id" Type="String" />
                                                <asp:Parameter DefaultValue="TI" Name="tipo" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" 
                style="height: 27px; font-weight: bold; font-size: 9pt; color: darkblue; border-bottom: gold 1px solid; font-family: verdana;" 
                colspan="2">
                &nbsp;Grados Académicos</td>
        </tr>
        <tr>
            <td align="right" style="padding-top: 5px" valign="top" colspan="2">
                <table style="width: 98%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left">
                            <asp:LinkButton ID="LinkVistaGrado" runat="server" BorderColor="Silver" BorderStyle="Solid"
                                BorderWidth="1px" CssClass="tab_seleccionado" Height="27px" Width="110px" CausesValidation="False">Datos 
                            Grados</asp:LinkButton><asp:LinkButton
                                    ID="LinkAgregaGrado" runat="server" BorderColor="Silver" BorderStyle="Solid" BorderWidth="1px"
                                    CssClass="tab_normal" Height="27px" Style="text-align: center" Width="110px" CausesValidation="False">Agregar 
                            Grados</asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td style="height: 230px" align="center" class="borde_tab" valign="top">
                            <asp:Panel ID="Panel2" runat="server" Width="100%">
                                        <asp:GridView ID="GridView2" runat="server" AllowSorting="True" AutoGenerateColumns="False" Width="100%" DataKeyNames="codigo_gpr" DataSourceID="ObjGrados" GridLines="Horizontal" AllowPaging="True" PageSize="6" >
                                            <Columns>
                                                <asp:BoundField DataField="descripcion_TGr" HeaderText="Grado" SortExpression="descripcion_TGr" />
                                                <asp:BoundField DataField="nombre_Gra" HeaderText="Descripci&#243;n" SortExpression="nombre_Gra" />
                                                <asp:BoundField DataField="mencion_GPr" HeaderText="Menci&#243;n" SortExpression="mencion_GPr" />
                                                <asp:BoundField DataField="nombre_Ins" HeaderText="Instituci&#243;n" SortExpression="nombre_Ins" />
                                                <asp:BoundField DataField="descripcion_Sit" HeaderText="Situaci&#243;n" SortExpression="descripcion_Sit" />
                                                <asp:BoundField DataField="anioIngreso_GPr" HeaderText="A Ing." SortExpression="anioIngreso_GPr">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anioEgreso_GPr" HeaderText="A Egr." SortExpression="anioEgreso_GPr">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="anioGrad_GPr" HeaderText="A Gra." SortExpression="anioGrad_GPr">
                                                    <ItemStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:CommandField ButtonType="Image" EditImageUrl="~/images/editar.gif" ShowCancelButton="False"
                                                    ShowEditButton="True">
                                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                                </asp:CommandField>
                                                <asp:CommandField ButtonType="Image" DeleteImageUrl="~/images/eliminar.gif"
                                                    ShowDeleteButton="True" ShowHeader="True" DeleteText="Eliminar registro" >
                                                    <ItemStyle HorizontalAlign="Center" Width="25px" />
                                                </asp:CommandField>
                                            </Columns>
                                            <RowStyle CssClass="fila_datos" Height="25px" />
                                            <EmptyDataTemplate>
                                                <em style="font-size: 8pt; color: maroon; font-style: normal; font-family: verdana">
                                                    <table style="font-size: 8pt; width: 100%; height: 100%">
                                                        <tr>
                                                            <td align="center">
                                                                <span style="color: #800000; text-align: center">
                                                                Lo sentimos Usted no tiene registrado ningún grado Académico.
                                                    <br />
                                                                Haga click en agregar para registrar uno. </span>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </em>
                                            </EmptyDataTemplate>
                                            <HeaderStyle
                                                ForeColor="Black" HorizontalAlign="Center" CssClass="tab_normal" Height="23px" />
                                        </asp:GridView>
                            </asp:Panel>
                            <asp:Panel ID="Panel4" runat="server" Visible="False" Width="100%">
                                <table id="Table1" class="borde_tab">
                                    <tr>
                                        <td colspan="2" style="border-bottom: gold 1px solid; height: 22px">
                                            <asp:Label ID="LblGrado" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small"
                                                ForeColor="Navy"></asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Tipo de Estudios</td>
                                        <td>
                                            <asp:DropDownList ID="DDLTipoGrado" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                Width="97px">
                                                <asp:ListItem Value="3">Bachillerato</asp:ListItem>
                                                <asp:ListItem Value="4">Maestria</asp:ListItem>
                                                <asp:ListItem Value="5">Doctorado</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblCod" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Grado Académico</td>
                                        <td>
                                            <asp:DropDownList ID="DDLGrado" runat="server" CssClass="datos_combo" Width="425px">
                                            </asp:DropDownList>
                                                <asp:RangeValidator 
                                                ID="RangeValidator5" 
                                                runat="server" 
                                                ControlToValidate="DDLGrado"
                                                ErrorMessage="Seleccione Grado Academico" 
                                                MaximumValue="8000" 
                                                MinimumValue="1"
                                                SetFocusOnError="True" 
                                                Type="Integer" 
                                                ValidationGroup="grado">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                        </td>
                                        <td class="titulo_items">
                                            Otros Especifique
                                            <asp:TextBox ID="TxtOtrosGrados" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" CssClass="datos_combo" Width="318px"></asp:TextBox><asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validagrado"
                                                ErrorMessage="Ingrese Grado Academico en otros" ValidationGroup="grado">*</asp:CustomValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Mención</td>
                                        <td>
                                            <asp:TextBox ID="TxtMención" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" CssClass="datos_combo" Width="420px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Año</td>
                                        <td class="titulo_items">
                                            Ingreso
                                            <asp:DropDownList ID="DDLAIngGrado" runat="server" CssClass="datos_combo" Width="58px">
                                            </asp:DropDownList>
                                            <asp:CompareValidator   
                                                    ID="CompareValidator6" 
                                                    runat="server" 
                                                    ControlToValidate="DDLAIngGrado"
                                                    ErrorMessage="Seleccione año de Ingreso" 
                                                    Operator="NotEqual"
                                                    SetFocusOnError="True"  
                                                    ValidationGroup="grado"
                                                    ValueToCompare="3000">*</asp:CompareValidator>
                                            Egreso &nbsp;<asp:DropDownList ID="DDLAEgrGrado" runat="server" CssClass="datos_combo"
                                                Width="63px">
                                            </asp:DropDownList>
                                            Titulación &nbsp;<asp:DropDownList ID="DDLATitGrado" runat="server" CssClass="datos_combo"
                                                Width="64px">
                                            </asp:DropDownList><asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="DDLAEgrGrado"
                                                ControlToValidate="DDLAIngGrado" ErrorMessage="Año de ingreso debe ser menor que de el egreso"
                                                Operator="LessThanEqual" SetFocusOnError="True" Type="Integer" ValidationGroup="grado">*</asp:CompareValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Tipo Institución</td>
                                        <td valign="middle">
                                            <asp:DropDownList ID="DDLTipoInsGrado" runat="server" AutoPostBack="True" CssClass="datos_combo"
                                                Width="179px" Height="16px">
                                            </asp:DropDownList>
                                            <asp:RangeValidator ID="RangeValidator6" runat="server" ControlToValidate="DDLTipoInsGrado"
                                                ErrorMessage="Seleccione tipo de institucion" MaximumValue="8000" MinimumValue="1"
                                                SetFocusOnError="True">*</asp:RangeValidator>&nbsp;
                                            <asp:Label ID="Label4" runat="server" CssClass="titulo_items" 
                                                Text="Procedencia"></asp:Label>
                                            &nbsp;
                                            <asp:DropDownList ID="DDLProcedienciaGrado" runat="server" AutoPostBack="True" 
                                                CssClass="datos_combo" Width="111px">
                                                <asp:ListItem Value="1">Nacional</asp:ListItem>
                                                <asp:ListItem Value="2">Extranjera</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Centro de Estudios</td>
                                        <td>
                                            <asp:DropDownList ID="DDLCentroGrado" runat="server" CssClass="datos_combo" Width="422px">
                                            </asp:DropDownList>
                                                <asp:RangeValidator 
                                                ID="RangeValidator9" 
                                                runat="server" 
                                                ControlToValidate="DDLCentroGrado"
                                                ErrorMessage="Seleccione centro de estudios" 
                                                MaximumValue="8000" 
                                                MinimumValue="1"
                                                SetFocusOnError="True" 
                                                Type="Integer" 
                                                ValidationGroup="grado">*</asp:RangeValidator>
                                                                                                </td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                        </td>
                                        <td id="Td1" class="titulo_items">
                                            Otros Especifique
                                            <asp:TextBox ID="TxtOtrosCentroGrados" runat="server" BorderColor="Black" BorderStyle="Solid"
                                                BorderWidth="1px" CssClass="datos_combo" Width="316px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items">
                                            &nbsp;Situación</td>
                                        <td>
                                            <asp:DropDownList ID="DDLSitGrado" runat="server" CssClass="datos_combo" Width="116px">
                                            </asp:DropDownList><asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="DDLSitGrado"
                                                ErrorMessage="Seleccione Situacion de grado academico" MaximumValue="8000" MinimumValue="1"
                                                Type="Integer" ValidationGroup="grado">*</asp:RangeValidator></td>
                                    </tr>
                                    <tr>
                                        <td class="titulo_items" style="height: 32px">
                                        </td>
                                        <td align="right" style="height: 32px">
                                            <asp:Button ID="CmdGrabarGrado" runat="server" CssClass="tab_normal" Text="Guardar" Width="85px" Height="24px" ValidationGroup="grado" /></td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <br />
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
                                            <img border=0 alt="Títulos y grados académicos" 
                                                src="images/hojavida/titulosygrados_r.gif" />
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
                                <table cellpadding="3" cellspacing="3" class="tabla_personal" 
                                    style="background-color: #FFFFCC; border: 1px solid #000080" width="100%">
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
                            <td width="80%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
        </tr>
    </table>
    <br />
    <asp:ValidationSummary ID="ListaErrores" runat="server" DisplayMode="List" ShowMessageBox="True" ShowSummary="False" ValidationGroup="grado" />
    &nbsp;
                                        <asp:ObjectDataSource ID="ObjGrados" runat="server" DeleteMethod="QuitarGrados" SelectMethod="ObtieneDatosGrados"
                                            TypeName="Personal">
                                            <DeleteParameters>
                                                <asp:Parameter Name="CodGrado" Type="Int32" />
                                            </DeleteParameters>
                                            <SelectParameters>
                                                <asp:SessionParameter Name="idpersonal" SessionField="id" Type="String" />
                                                <asp:Parameter DefaultValue="GR" Name="tipo" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
    <asp:HiddenField ID="HddTitulo" runat="server" />
    <asp:HiddenField ID="HddGrado" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" DisplayMode="List"
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="titulo" />
    </center>
    <p>
    </p>
</ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="CmdGrabarGrado"/>
                <asp:PostBackTrigger ControlID="CmdGuardar"/>
                <asp:PostBackTrigger ControlID="btnGuardarInforme"/>
                <asp:PostBackTrigger ControlID="btnCancelar"/>
                <asp:PostBackTrigger ControlID="LinkVistaTitulo"/>
                <asp:PostBackTrigger ControlID="LinkAgregaTitulo"/>
                <asp:PostBackTrigger ControlID="LinkVistaGrado"/>
                <asp:PostBackTrigger ControlID="LinkAgregaGrado"/>
            </Triggers>
</asp:UpdatePanel>    
    </form>
    </body>
</html> 


