<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmpersona.aspx.vb" Inherits="librerianet_frmpersona" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registrar Persona</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="JavaScript" src="../private/funciones.js"></script>
    <script type="text/javascript" language="JavaScript" src="../private/jq/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" language="javascript" src="../private/jq/jquery.mascara.js"></script>
	<script type="text/javascript" language="javascript">
	    $(document).ready(function() {
            jQuery(function($) {
                $("#txtFechaNac").mask("99/99/9999");
            });
        })
	</script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Registro de Datos Personales</p>
    <table style="width: 80%" class="contornotabla" runat="server" id="tblPersona">
        <tr>
            <td colspan="2">
        <asp:Label ID="lblmensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Doc. de Identidad<b><asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                    ControlToValidate="txtdni" 
                    ErrorMessage="Debe ingresar el número de documento de identidad">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:DropDownList ID="dpTipo" runat="server">
                    <asp:ListItem>DNI</asp:ListItem>
                    <asp:ListItem>CARNÉ DE EXTRANJERÍA</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txtdni" runat="server" CssClass="cajas" MaxLength="15"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Apellido: Paterno
                <b> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtAPaterno" 
                    ErrorMessage="Debe ingresar el Apellido Paterno">*</asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtAMaterno" 
                    ErrorMessage="Debe ingresar el Apellido Materno">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txtAPaterno" runat="server" CssClass="cajas" MaxLength="100" 
                    Width="250px"></asp:TextBox>
            &nbsp;Materno
                <asp:TextBox ID="txtAMaterno" runat="server" CssClass="cajas" MaxLength="100" 
                    Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Nombres
                <b> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtNombres" 
                    ErrorMessage="Debe ingresar los Nombres">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txtNombres" runat="server" CssClass="cajas" MaxLength="80" 
                    Width="250px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Fecha Nac.
                <b> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtFechaNac" 
                    ErrorMessage="Debe ingresar la Fecha de Nac.">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txtFechaNac" runat="server" CssClass="cajas" MaxLength="11" 
                    Width="128px"></asp:TextBox>
            &nbsp;&nbsp; Sexo:<asp:DropDownList ID="dpSexo" runat="server" CssClass="cajas">
                    <asp:ListItem Value="M">Masculino</asp:ListItem>
                    <asp:ListItem Value="F">Femenino</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Email Principal
                <b> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                    ControlToValidate="txtemail1" 
                    ErrorMessage="Debe ingresar el email">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txtemail1" runat="server" CssClass="cajas" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Email Alternativo</td>
            <td>
                <asp:TextBox ID="txtemail2" runat="server" CssClass="cajas" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Dirección
                <b> 
                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                    ControlToValidate="txtdireccion" 
                    ErrorMessage="Debe ingresar la dirección de la persona">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txtdireccion" runat="server" CssClass="cajas" Width="80%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Provincia</td>
            <td>
                <asp:DropDownList ID="dpprovincia" runat="server" CssClass="cajas" 
                    AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="dpdistrito" runat="server" CssClass="cajas">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                Teléfono Fijo<b><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                    ControlToValidate="txtcelular" 
                    ErrorMessage="Debe ingresar el número de celular">*</asp:RequiredFieldValidator>
                            </b></td>
            <td>
                <asp:TextBox ID="txttelefono" runat="server" CssClass="cajas"></asp:TextBox>
            &nbsp;
                Celular:<asp:TextBox ID="txtcelular" runat="server" CssClass="cajas"></asp:TextBox>
                <asp:HiddenField ID="hcodigo_cac" runat="server" Value="0" />
                <asp:HiddenField ID="hdlogin_per" runat="server" Value="0" />
                <asp:HiddenField ID="hdcodigo_cco" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td style="width:20%">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <p class="usatTitulo">Servicios asociados a la persona y al centro de costos         <asp:Label ID="lblCentroCosto" runat="server"></asp:Label>
    </p>
    <asp:GridView ID="grwServicios" runat="server" AutoGenerateColumns="False" 
        CellPadding="3" DataKeyNames="codigo_sco">
        <EmptyDataRowStyle CssClass="rojo" Font-Bold="True" />
        <Columns>
            <asp:TemplateField HeaderText="Marcar">
                <ItemTemplate>
                    <asp:CheckBox ID="chkElegir" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="descripcion_sco" HeaderText="Servicio" />
            <asp:BoundField DataField="precio_sco" HeaderText="Monto" />
            <asp:TemplateField HeaderText="Fecha Venc.">
                <ItemTemplate>
                    <asp:TextBox ID="txtfechavencimiento" runat="server" CssClass="cajas" 
                        Enabled="False" MaxLength="10" 
                        Text='<%# cdate(eval("fechavencimiento_sco")).toshortdatestring() %>' 
                        Width="80px"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="F. Inicio Cobro">
                <ItemTemplate>
                    <asp:TextBox ID="txtfechainiciocobro" runat="server" CssClass="cajas" 
                        Enabled="False" MaxLength="10" 
                        Text='<%# cdate(eval("fechainicio_sco")).toshortdatestring() %>' Width="80px"></asp:TextBox>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="nropartes_sco" HeaderText="Nro Partes" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="generamora_sco" HeaderText="Mora" Visible="False" />
            <asp:BoundField DataField="fecha_deu" DataFormatString="{0:d}" 
                HeaderText="Fecha Deuda">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="montototal_deu" HeaderText="Cargo" />
            <asp:BoundField HeaderText="Abono (*)" />
            <asp:BoundField DataField="saldo_deu" HeaderText="Saldo" />
            <asp:BoundField DataField="estado_deu" HeaderText="Estado">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
        </Columns>
        <EmptyDataTemplate>
            No se encontraron items asociados al Centro de Costos. Por favor consultar con 
            el Sr. Miguel Rentería dicha información.
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#3366CC" ForeColor="White" />
    </asp:GridView>

    <br />(*). Incluye notas de abono + pago en efectivo. 
    <br /> Para anular deudas debe coordinar con Caja y Pensiones
    <p style="text-align:center">
    <asp:Button ID="cmdGuardar" runat="server" Text="Guardar" CssClass="guardar" 
            Enabled="False" />
    &nbsp;<input id="cmdCancelar" type="button" value="Cerrar" 
                onclick="self.parent.tb_remove()" class="salir" /></p>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
            ShowMessageBox="True" ShowSummary="False" />
    </form>
</body>
</html>
