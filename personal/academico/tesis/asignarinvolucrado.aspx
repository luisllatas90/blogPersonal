<%@ Page Language="VB" AutoEventWireup="false" CodeFile="asignarinvolucrado.aspx.vb" Inherits="asignarinvolucrado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignación de Asesor / Jurado</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../../private/jq/jquery.js" type="text/javascript"></script>
    <script src="../../../private/jq/jquery.mascara.js" type="text/javascript"></script>
    	
    <script type="text/javascript" language="javascript">
        $(document).ready(function(){
            jQuery(function($){
            $("#txtFechaInicio").mask("99/99/9999");
            });
        })       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <p class="usatTitulo">Asignar Asesor / Jurado</p>
    <asp:GridView ID="grwInvolucrados" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" BorderColor="#999999" BorderStyle="Solid" 
        BorderWidth="1px" DataKeyNames="codigo_Rtes">
        <Columns>
            <asp:BoundField HeaderText="Rol" DataField="descripcion_tpi" />
            <asp:TemplateField HeaderText="Apellidos y Nombres">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("docente") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblnombre" runat="server" Text='<%# Bind("docente") %>'></asp:Label>
                    <br />
                    <asp:Label ID="lbldedicacion" runat="server" Font-Italic="True" 
                        Text='<%# Bind("descripcion_ded") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="Estado actual" DataField="estado_rTes" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField HeaderText="Fecha Reg." DataField="fechareg_Rtes" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="OpRegistro" HeaderText="Operador Reg." />
            <asp:BoundField HeaderText="Obs" DataField="obs_RTes" />
            <asp:CommandField HeaderText="Acción" ShowDeleteButton="True" 
                DeleteText="Desactivar" >
                <ControlStyle Font-Underline="True" ForeColor="#0000CC" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:CommandField>
        </Columns>
        <EmptyDataTemplate>
            <strong style="width: 100%; color: red; text-align: center">
                <br />
                <br />
                Aún no han registrado Asesores de la investigación</strong>
        </EmptyDataTemplate>
        <HeaderStyle BackColor="#E8EEF7" ForeColor="#3366CC" />
    </asp:GridView>
    <p style="text-align:right">
        &nbsp;<input id="cmdCancelar" type="button" value="    Cerrar" 
            onclick="self.parent.tb_remove()" class="eliminar2" />
    </p>
    <table style="width:100%" class="contornotabla" cellpadding="3" cellspacing="0">
        <tr>
            <td style="background-color: #99CCFF; font-weight: bold; height: 25px;" 
                colspan="2">Registrar nuevo Asesor / Jurado</td>
        </tr>
        <tr>
            <td style="width: 10%">Dpto.</td>
            <td>
                <asp:DropDownList ID="cboCodigo_dac" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <asp:DropDownList ID="cbocodigo_per" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">Rol</td>
            <td>
                <asp:DropDownList ID="cboCodigo_tpi" runat="server">
                </asp:DropDownList>
            &nbsp; Fecha de asignación:
                <asp:TextBox ID="txtFechaInicio" runat="server" Width="80px" ForeColor="Navy" 
                    style="text-align: right" BackColor="#CCCCCC" MaxLength="10"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">Obs.</td>
            <td>
                <asp:TextBox ID="txtObs" runat="server" CssClass="cajas" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 10%">&nbsp;</td>
            <td>
                <asp:Button ID="cmdGuardar" runat="server" Text="     Guardar" Visible="False" 
                    CssClass="guardar2" />
                <asp:Label ID="lblMensaje" runat="server" CssClass="rojo" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        </table>
</form>
</body>
</html>
