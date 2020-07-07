<%@ Page Language="VB" AutoEventWireup="false" CodeFile="registropec.aspx.vb" Inherits="registropec" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programas de Educación Contínua: PEC</title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../../private/funciones.js"></script>
    <script src="../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }

        $(document).ready(function() {
            jQuery(function($) {
                $("#txtInicio").mask("99/99/9999");
                $("#txtFin").mask("99/99/9999");
                $("#txtInicio0").mask("99/99/9999");
                $("#txtFin0").mask("99/99/9999");
            });
        })
    </script>
    </head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Programas de Educación Contínua</p>
                                    <table runat="server" id="fraDetallePEC" cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%" visible="false">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="3">
                                                Datos del Programa</td>
                                            <td align="right">
                                                <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" 
                                                    ValidationGroup="EdicionPEC" CssClass="guardar2" />
                                                &nbsp;<asp:Button ID="cmdCancelar" runat="server" Text="Cancelar" 
                                                    ValidationGroup="Cancelar" CssClass="regresar2" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Tipo</td>
                                            <td>
                                                <asp:DropDownList ID="dpCodigo_Tpec" runat="server">
                                                </asp:DropDownList>
                                                    <a target="_blank" href="doc/DefinicionesEAUSAT.pdf" style="color: #FF0000">[Ver definiciones]</a>
                                            </td>
                                            <td align="right">
                                                Nro Edición:</td>
                                            <td>
                                                <asp:Label ID="lbledicion_pet" runat="server" Font-Bold="True" Font-Size="10pt" 
                                                    ForeColor="Red" Font-Names="Verdana"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Denominación</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtDescripcion_pes" runat="server" Width="95%" 
                                                    CssClass="cajas"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqDenominacion" runat="server" 
                                                    ControlToValidate="txtdescripcion_pes" 
                                                    ErrorMessage="Debe especificar la denominación del Programa" 
                                                    ValidationGroup="EdicionPEC">*</asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Resol. Aprob</td>
                                            <td>
                                                <asp:TextBox ID="txtResolucion_pec" runat="server" CssClass="cajas" 
                                                    MaxLength="20"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqResolucion" runat="server" 
                                                    ControlToValidate="txtResolucion_pec" 
                                                    
                                                    
                                                    ErrorMessage="Debe especificar el Número de Resolución de Aprobación de Secretaría General" 
                                                    ValidationGroup="EdicionPEC">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right">
                                                Estado:</td>
                                            <td>
                                                <asp:DropDownList ID="dpCodigo_epec" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Fecha Inicio</td>
                                            <td>
                                                <asp:TextBox ID="txtInicio" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RqFechaInicio" runat="server" 
                                                    ControlToValidate="txtInicio" 
                                                    ErrorMessage="Debe especificar la fecha de inicio" 
                                                    ValidationGroup="EdicionPEC">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td align="right">
                                                Fecha Fin:</td>
                                            <td>
                                                <asp:TextBox ID="txtFin" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                                &nbsp;<asp:RequiredFieldValidator ID="RqFechaFin" runat="server" 
                                                    ControlToValidate="txtFin" 
                                                    ErrorMessage="Debe especificar la fecha de término" 
                                                    ValidationGroup="EdicionPEC">*</asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CRqFechaFin" runat="server" 
                                                    ControlToCompare="txtFin" ControlToValidate="txtInicio" 
                                                    ErrorMessage="Fecha de Termino menor o igual a fecha de inicio." 
                                                    Operator="LessThan" Type="Date" ValidationGroup="EdicionPEC">*</asp:CompareValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Horarios</td>
                                            <td colspan="3">
                                                <asp:TextBox ID="txthorarios" runat="server" CssClass="cajas" 
                                                    MaxLength="255" TextMode="MultiLine" Width="50%" Rows="3"></asp:TextBox></td>
                                        </tr>                                        
                                        <tr>
                                            <td style="width: 15%">
                                                Centro Costos</td>
                                            <td colspan="3">
                <asp:DropDownList ID="dpCodigo_cco" runat="server" DataTextField="nombre" 
                                                    DataValueField="codigo_cco">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                &nbsp;</td>
                                            <td colspan="3">
                                                Debe consignar el Centro de Costos Actual para el cruce con la información de 
                                                Presupuestos. Para crear Centros de Costos coordinar con
                                                <a href="mailto:ccama@usat.edu.pe">ccama@usat.edu.pe</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Responsable                            Responsable</td>
                                            <td colspan="3">
                <asp:DropDownList ID="dpCodigo_per" runat="server" DataTextField="personal" DataValueField="codigo_per">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                Registado</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblOperador" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 15%">
                                                &nbsp;</td>
                                            <td colspan="3">
                                                <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Size="10pt" 
                                                    EnableViewState="False"></asp:Label>
                                                <asp:ValidationSummary ID="ValidationPrograma" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="EdicionPEC" />
                                            </td>
                                        </tr>
                                        <tr style="background-color: #E8EEF7; font-weight: bold; height: 30px">
                                            <td colspan="2" >Módulos del Programa</td>
                                            <td colspan="2" align="right" >
                                                <asp:Button ID="cmdNuevoModulo" runat="server" Text="    Nuevo" 
                                                    ValidationGroup="AgregarModulos" CssClass="usatNuevo" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <asp:GridView ID="grwModulosPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_cup" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" 
                                                        HorizontalAlign="Center" />
                                                    <EditRowStyle BackColor="#FFFF66" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Nro">
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="dpciclo_cur" runat="server" 
                                                                    SelectedValue='<%# eval("ciclo_cur") %>'>
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
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="dpciclo_cur" runat="server">
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
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblciclo_cur" runat="server" Text='<%# eval("ciclo_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Denominación">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtnombre_cur" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("nombre_cur") %>' Width="98%" MaxLength="500"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDenominacionF" runat="server" 
                                                                    ControlToValidate="txtnombre_cur" 
                                                                    ErrorMessage="Debe especificar la denominación del Módulo" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RequiredFieldValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtnombre_cur" runat="server" CssClass="cajas" 
                                                                    Width="98%" MaxLength="500"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RqDenominacionF" runat="server" 
                                                                    ControlToValidate="txtnombre_cur" 
                                                                    ErrorMessage="Debe especificar la denominación del Módulo" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RequiredFieldValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblnombre_cur" runat="server" Text='<%# eval("nombre_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Left" Width="55%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Crd.">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtcreditos_cur" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("creditos_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqCrd" runat="server" 
                                                                    ControlToValidate="txtcreditos_cur" 
                                                                    ErrorMessage="Debe especificar los créditos del módulo (entre 0 y 100)" 
                                                                    MaximumValue="100" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtcreditos_cur" runat="server" CssClass="cajas" 
                                                                    Width="25px" MaxLength="2">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqCrdF" runat="server" 
                                                                    ControlToValidate="txtcreditos_cur" 
                                                                    ErrorMessage="Debe especificar los créditos del módulo (entre 0 y 100)" 
                                                                    MaximumValue="100" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcreditos_cur" runat="server" 
                                                                    Text='<%# eval("creditos_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HT">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtht" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horasteo_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHT" runat="server" ControlToValidate="txtht" 
                                                                    ErrorMessage="Debe especificar el Nro de Horas Teóricas (1-300)" 
                                                                    MaximumValue="300" MinimumValue="1" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtht" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqHTF" runat="server" ControlToValidate="txtht" 
                                                                    ErrorMessage="Debe especificar el Nro de Horas Teóricas (1-300)" 
                                                                    MaximumValue="300" MinimumValue="1" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblht" runat="server" Text='<%# eval("horasteo_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HI">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txthi" runat="server" Text='<%# eval("horaspra_cur") %>' 
                                                                    CssClass="cajas" MaxLength="3" Width="27px"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHI" runat="server" ControlToValidate="txthi" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Investigación (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txthi" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3" Text="0"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHIF" runat="server" ControlToValidate="txthi" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Investigación (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhi" runat="server" Text='<%# eval("horaspra_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HA">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtha" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horasAse_cur") %>' Width="25px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHA" runat="server" ControlToValidate="txtha" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Asesoría (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtha" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3">0</asp:TextBox>
                                                                <asp:RangeValidator ID="RqHAF" runat="server" ControlToValidate="txtha" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Asesoría (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblha" runat="server" Text='<%# eval("horasAse_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HEP">
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txthep" runat="server" CssClass="cajas" 
                                                                    Text='<%# eval("horaslab_cur") %>' Width="27px" MaxLength="3"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHEP" runat="server" ControlToValidate="txthep" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Estudio Personal (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarEditarModulo">*</asp:RangeValidator>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txthep" runat="server" CssClass="cajas" 
                                                                    Width="27px" MaxLength="3" Text="0"></asp:TextBox>
                                                                <asp:RangeValidator ID="RqHEPF" runat="server" ControlToValidate="txthep" 
                                                                    ErrorMessage="Debe especificar correctamente el Nro de Horas de Estudio Personal (0-300)" 
                                                                    MaximumValue="300" MinimumValue="0" Type="Integer" 
                                                                    ValidationGroup="ValidarAgregarModulo">*</asp:RangeValidator>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblhep" runat="server" Text='<%# eval("horaslab_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TH">
                                                            <FooterTemplate>
                                                                <asp:ImageButton ID="imgGuardar" runat="server" 
                                                                    ImageUrl="../../images/guardar.gif" onclick="imgGuardar_Click" 
                                                                    ValidationGroup="ValidarAgregarModulo" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblth" runat="server" Text='<%# eval("totalhoras_cur") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="5%" />
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowEditButton="True" ButtonType="Image" 
                                                            CancelImageUrl="../../images/salir.gif" EditImageUrl="../../images/editar.gif" 
                                                            UpdateImageUrl="../../images/guardar.gif" 
                                                            ValidationGroup="ValidarEditarModulo" >
                                                        <ItemStyle Width="5%" />
                                                        </asp:CommandField>
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar" ButtonType="Image" 
                                                            DeleteImageUrl="../../images/eliminar.gif">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" Width="5%" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" HorizontalAlign="Center" />
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lbltotales" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                                    Font-Size="8pt" ForeColor="#3366DB"></asp:Label>
                                                <asp:ValidationSummary ID="ValidarEditarModulo" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" 
                                                    ValidationGroup="ValidarEditarModulo" />
                                                <asp:ValidationSummary ID="ValidarAgregarModulo" runat="server" 
                                                    ShowMessageBox="True" ShowSummary="False" 
                                                    ValidationGroup="ValidarAgregarModulo" />
                                                </td>
                                        </tr>
                                    </table>
                                    <table cellpadding="3" 
                                        cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td>
                                                <asp:Label ID="lblProgramas" runat="server" 
                                                    Text="Listado de Programas de Educación Contínua"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Button ID="cmdAgregar" runat="server" Text="    Nuevo" 
                                                    CssClass="usatnuevo" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:GridView ID="grwPEC" runat="server" 
                                                    AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Top" 
                                                    DataKeyNames="codigo_pec" Width="100%" BorderColor="Silver" 
                                                    EnableModelValidation="True" CellPadding="2">
                                                    <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                                                    <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" 
                                                        ForeColor="Red" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="#">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_tpec" HeaderText="Tipo">
                                                        <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_pes" HeaderText="Denominación">
                                                        <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechainicio_pec" DataFormatString="{0:d}" 
                                                            HeaderText="Inicio">
                                                            <ItemStyle HorizontalAlign="Center" Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fechafin_pec" DataFormatString="{0:d}" 
                                                            HeaderText="Fin">
                                                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="version_pec" HeaderText="Edición">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        <ItemStyle Font-Size="7pt" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="nroresolucion_pec" HeaderText="Nro. Resolución" />
                                                        <asp:BoundField DataField="total_mat" HeaderText="Matriculados">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="total_cur" HeaderText="Módulos" >
                                                        <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="descripcion_epec" HeaderText="Estado">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:ButtonField CommandName="editar" Text="Editar" >
                                                            <ControlStyle Font-Underline="True" />
                                                        <ItemStyle HorizontalAlign="Center" Font-Underline="True" ForeColor="#3333FF" />
                                                        </asp:ButtonField>
                                                        <asp:CommandField ShowDeleteButton="True" 
                                                            DeleteText="Eliminar">
                                                            <ControlStyle Font-Underline="True" />
                                                            <ItemStyle Font-Underline="True" ForeColor="Blue" 
                                                                HorizontalAlign="Center" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No han encontrado Programas registrados asociados a los Centros de Costos que 
                                                        tiene configurado en Presupuestos.
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" 
                                                        BorderWidth="1px" ForeColor="#3366CC" />
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdcodigo_pec" runat="server" Value="0" />
                                            </td>
                                        </tr>
                                    </table>
                                    <table runat="server" id="fraPregunta" cellpadding="5" 
                                        cellspacing="0" 
        style="border: 1px solid #C2CFF1; width:100%" visible="false" width="80%">
                                        
                                        <tr style="background-color: #E8EEF7; font-weight: bold;">
                                            <td colspan="7" 
                                                style="font-size: 14px; font-family: Arial, Helvetica, sans-serif; font-weight: bold; width: 100%;">
                                                Advertencia</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" colspan="7">
                                                <strong>Elija la acción que desea realizar:</strong>&nbsp;&nbsp; </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" colspan="7">
                                                <asp:RadioButton ID="Opt1" runat="server" Checked="True" GroupName="Accion" 
                                                    Text="Registrar un &quot;Nuevo Programa&quot; que no existe en la base de datos." />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 100%" colspan="7">
                                                <asp:RadioButton ID="Opt2" runat="server" GroupName="Accion" 
                                                    
                                                    Text="Registrar una &quot;Nueva Edición&quot; de un  &amp;quot;Programa&amp;quot; existente en la base de datos" />
                                                .</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%" align="right">
                                                &nbsp;</td>
                                            <td style="width: 15%">
                                                Programa:</td>
                                            <td colspan="5" style="width: 100%">
                <asp:DropDownList ID="dpPrograma" runat="server" Width="98%">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%" align="right">
                                                &nbsp;</td>
                                            <td style="width: 15%">
                                                Resolución:</td>
                                            <td style="width: 23%">
                                                <asp:TextBox ID="txtResolucion_pec0" runat="server" CssClass="cajas" 
                                                    MaxLength="20"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Inicio:</td>
                                            <td style="width: 10%">
                                                <asp:TextBox ID="txtInicio0" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                Fin:</td>
                                            <td style="width: 30%">
                                                <asp:TextBox ID="txtFin0" runat="server" BackColor="#CCCCCC" Font-Size="8pt" 
                                                    ForeColor="Navy" MaxLength="12" style="text-align: right" Columns="12"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%" align="right">
                                                &nbsp;</td>
                                            <td style="width: 15%">
                                                Centro Costos</td>
                                            <td colspan="5" style="width: 100%">
                <asp:DropDownList ID="dpCodigo_cco0" runat="server" DataTextField="nombre" 
                                                    DataValueField="codigo_cco" Width="98%">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%" align="right">
                                                &nbsp;</td>
                                            <td style="width: 15%">
                                                Coordinador</td>
                                            <td colspan="5" style="width: 100%">
                <asp:DropDownList ID="dpCodigo_per0" runat="server" DataTextField="personal" DataValueField="codigo_per">
                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%" align="right">
                                                &nbsp;</td>
                                            <td style="width: 15%">
                                                &nbsp;</td>
                                            <td colspan="5" style="width: 100%">
                                                <asp:Label ID="lblmensaje0" runat="server" CssClass="rojo" Font-Size="10pt" 
                                                    EnableViewState="False"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="7">
                                                <asp:Button ID="cmdContinuar" runat="server" Text="  Aceptar" 
                                                    CssClass="agregar2" />
                                                &nbsp;<asp:Button ID="cmdRegresar" runat="server" Text="Cancelar" 
                                                    CssClass="regresar2" />
                                            </td>
                                        </tr>
                                        </table>
    </form>
</body>
</html>

