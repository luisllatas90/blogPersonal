<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMantenimiento_POA.aspx.vb" Inherits="indicadores_POA_PROTOTIPOS_Registrar_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <%--<link href="../../css/estilo.css" rel="stylesheet" type="text/css" media="screen" />--%>
        <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
        
</head>
<body>
    <form id="form1" runat="server">
    <asp:HiddenField runat="server" ID="foco" />
<%--    <asp:ScriptManager ID="ScriptManager1" runat="server"> 
    </asp:ScriptManager>--%>  <%--PROBLEMAS CON EL UPDATEPANEL, EJECUTABA 2 VECES EL SELECTINDEX DEL ARBOL--%>
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server" Text="Registro de Plan Operativo Anual"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%">
            <tr>
                <td>
                    <asp:HiddenField ID="hdcodigopoa" runat="server" Value="0" />
                    Ejercicio Presupuestal</td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="202px" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToValidate="ddlEjercicio" 
                    Operator="NotEqual"
                    ValueToCompare="0" ValidationGroup="Grupo1"
                    ErrorMessage="Debe Seleccionar un Ejercicio">&nbsp;
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    Plan Estratégico</td>
                <td>
                    <asp:DropDownList ID="ddlPlan" runat="server" Width="550px" AutoPostBack="true">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                    ControlToValidate="ddlplan" 
                    Operator="NotEqual"
                    ValueToCompare="0" ValidationGroup="Grupo1"
                    ErrorMessage="Debe Seleccionar un Plan">&nbsp;
                    </asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td rowspan="2" valign="middle">
                    Centro de Costos - Dirección</td>
                <td>
                 <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlDireccion" runat="server" Width="400px" 
                            Enabled="False">
                        </asp:DropDownList>
<%--                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlPlan" 
                            EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
             <tr>
                <td>
<%--            <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>--%>
                    <div>
                        <asp:TreeView ID="treePrueba" runat="server" ExpandDepth="0" 
                            Font-Size="XX-Small" MaxDataBindDepth="4">
                            <Nodes>
                            </Nodes>
                            <HoverNodeStyle CssClass="menuporelegir" />
                        </asp:TreeView>
                    </div>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtarea"
                    ErrorMessage="Debe Seleccionar un Area" ValidationGroup="Grupo1">&nbsp;</asp:RequiredFieldValidator>
<%--             </ContentTemplate>
             <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlPlan"  EventName="SelectedIndexChanged" />
             </Triggers>
             </asp:UpdatePanel>--%>
                
                </td>
            </tr>
            <tr>
                <td>
                    Centro de Costos - Área</td>
                <td> 
<%--                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <ContentTemplate>--%>
                        <asp:HiddenField id="hdcodarea" runat="server" Value="0"/>
                        <asp:TextBox ID="txtarea" runat="server" Width='550px' CssClass="caja_poa" Enabled="false"></asp:TextBox>
<%--                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="treePrueba"  EventName="SelectedNodeChanged" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
            
            <tr>
                <td>
                    Nombre de Plan</td>
                <td> 
                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" >
                    <ContentTemplate>--%>
                    <asp:TextBox ID="txtNombrePoa" runat="server" Width="550px" MaxLength="200" CssClass="caja_poa" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtarea"
                    ErrorMessage="Ingrese Nombre del Plan" ValidationGroup="Grupo1">&nbsp;</asp:RequiredFieldValidator>
<%--                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="treePrueba"  EventName="SelectedNodeChanged" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
            <tr>
                <td>
                    Responsable del Plan</td>
                <td>
<%--                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                    <ContentTemplate>--%>
                        <asp:DropDownList ID="ddlResponsable" runat="server" Width="400px">
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="ddlResponsable" 
                        Operator="NotEqual"
                        ValueToCompare="0" ValidationGroup="Grupo1"
                        ErrorMessage="Debe Seleccionar un Responsable">&nbsp;
                        </asp:CompareValidator>
<%--                    </ContentTemplate>
                    <Triggers>
                       <asp:AsyncPostBackTrigger ControlID="treePrueba"  EventName="SelectedNodeChanged" />
                    </Triggers>
                    </asp:UpdatePanel> --%>
                </td>
            </tr>
            <tr>
                <td>
                    Vigencia</td>
                <td>
                    <asp:CheckBox runat="server" ID="chkVigencia" Checked="true" />
                </td>
            </tr>
            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>
            <tr>
            <td colspan="2">
             <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="False" HeaderText="Errores"  ShowSummary="True" ValidationGroup="Grupo1" />
            </td>
            </tr>
            <tr>
            <td colspan="2">
            <div runat="server" id="aviso">
            <asp:Label ID="lblmensaje" runat="server" Font-Bold="true"></asp:Label>
            </div>
            </td>
            </tr>
            <tr>
            <td align="right" colspan="2">
                <asp:Button ID="cmdGuardarPoa" runat="server" CssClass="btnGuardar"  Text="   Guardar" ValidationGroup="Grupo1" />
                &nbsp;<asp:Button ID="cmdCancelar" runat="server" CssClass="btnCancelar" Text="  Cancelar"/>
            </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
