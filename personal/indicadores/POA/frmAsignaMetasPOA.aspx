<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsignaMetasPOA.aspx.vb"
    Inherits="indicadores_POA_frmAsignaMetasPOA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        table
        {
            font-family: Trebuchet MS;
            font-size: 8pt;
        }
        tbody tr
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-size: 8pt;
            color: #2F4F4F;
        }
        select
        {
            font-family: Verdana;
            font-size: 8.5pt;
            margin-left: 0px;
        }
        .btnBuscar
        {
            border: 1px solid #bfac4c;
            background: #eee9cf url('../../../Images/buscar_poa.png') no-repeat 0% center;
            color: #685d25;
            font-weight: bold;
            height: 25px;
        }
        TBODY
        {
            display: table-row-group;
        }
        .celda_combinada
        {
            border-color: rgb(169,169,169);
            border-style: solid;
            border-width: 1px;
        }
        .titulo_poa
        {
            position: absolute;
            top: 15px;
            left: 15px;
            font-size: 14px;
            font-weight: bold;
            font-family: "Helvetica Neue" ,Helvetica,Arial,sans-serif;
            color: #337ab7;
            background-color: White;
            padding-bottom: 10px;
            padding-left: 5px;
            padding-right: 5px;
            z-index: 1;
        }
        .contorno_poa
        {
            position: relative;
            top: 10px;
            border: 1px solid #C0C0C0;
            padding-left: 4px;
            padding-top: 20px;
            padding-right: 4px;
        }
        tr
        {
            font-family: Verdana, Geneva, Arial, Helvetica, sans-serif;
            font-size: 8pt;
            color: #2F4F4F;
            cursor: pointer;
        }
        .buscar2
        {
            border: 1px solid #666666;
            background: #FEFFE1 url('../../images/previo.gif') no-repeat 0% center;
            width: 80;
            font-family: Tahoma;
            font-size: 8pt;
            cursor: hand;
            margin-left: 0px;
        }
        .agregar2
        {
            border: 1px solid #666666;
            background: #FEFFE1 url('../../images/anadir.gif') no-repeat 0% center;
            width: 80;
            font-family: Tahoma;
            font-size: 8pt;
            cursor: hand;
        }
        .mensajeError
        {
            background-color: #f2dede;
            border: 1px solid #E9ABAB;
            font-weight: bold;
            color: #a94442;
            height: 25px;
            font-size: 11px;
            padding-top: 3px;
            padding-bottom: 3px;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label2" runat="server" Text="Asignar Metas"></asp:Label>
    </div>
    <div class="contorno_poa">
        <table width="100%" id="tabla" runat="server">
            <tr style="height: 30px;">
                <td width="140px">
                    Plan Estratégico
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlplan" runat="server" Width="100%" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td width="50px">
                </td>
                <td width="140px">
                    Ejercicio Presupuestal
                </td>
                <td>
                    <asp:DropDownList ID="ddlEjercicio" runat="server" Width="140" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnBuscar" runat="server" Text="   Buscar" CssClass="btnBuscar" />
                </td>
            </tr>
            <tr>
                <td>
                    Plan Operativo Anual
                </td>
                <td width="510px">
                    <asp:DropDownList ID="ddlPoa" runat="server" Width="100%">
                        <asp:ListItem Value="0">--SELECCIONE--</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td width="50px">
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div runat="server" id="aviso0">
                        <asp:Label ID="lblrpta" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" width="50%">
                </td>
                <td colspan="3" width="50%">
                    Estado de Proyectos: &nbsp;
                    <asp:DropDownList ID="ddlEstado" runat="server" Width="140" AutoPostBack="true">
                        <asp:ListItem Value="I">INICIADA</asp:ListItem>
                        <asp:ListItem Value="F">FINALIZADA</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <div id="aviso1" runat="server" visible="false">
                        <asp:Label ID="lblmensaje" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3" width="50%" valign="top">
                    <asp:GridView ID="dgv_iniciar" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False"
                        DataKeyNames="codigo_dap">
                        <Columns>
                            <asp:BoundField DataField="proyecto" HeaderText="PROYECTO" />
                            <asp:BoundField DataField="actividad" HeaderText="ACTIVIDAD" />
                            <asp:BoundField DataField="ene" HeaderText="ENE">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="feb" HeaderText="FEB">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mar" HeaderText="MAR">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abr" HeaderText="ABR">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="may" HeaderText="MAY">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jun" HeaderText="JUN">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jul" HeaderText="JUL">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ago" HeaderText="AGO">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="set" HeaderText="SET">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oct" HeaderText="OCT">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nov" HeaderText="NOV">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dic" HeaderText="DIC">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="INICIAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Enviar"
                                        CommandName="Edit" ImageUrl="../../images/inv_paso.png" Text="Editar" />
                                </ItemTemplate>
                                <ControlStyle Height="17px" Width="17px" />
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:GridView>
                </td>
                <td colspan="3" width="50%" valign="top">
                    <asp:GridView ID="dgv_termino" runat="server" Width="100%" CellPadding="3" AutoGenerateColumns="False"
                        DataKeyNames="codigo_dap,codigo_iac">
                        <Columns>
                            <asp:BoundField DataField="proyecto" HeaderText="PROYECTO" />
                            <asp:BoundField DataField="actividad" HeaderText="ACTIVIDAD" />
                            <asp:BoundField DataField="ene" HeaderText="ENE">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="feb" HeaderText="FEB">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mar" HeaderText="MAR">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="abr" HeaderText="ABR">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="may" HeaderText="MAY">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jun" HeaderText="JUN">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="jul" HeaderText="JUL">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ago" HeaderText="AGO">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="set" HeaderText="SET">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="oct" HeaderText="OCT">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nov" HeaderText="NOV">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dic" HeaderText="DIC">
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="TERMINAR" ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" AlternateText="Enviar"
                                        CommandName="Edit" ImageUrl="../../images/inv_paso.png" Text="Editar" />
                                </ItemTemplate>
                                <ControlStyle Height="17px" Width="17px" />
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White" Height="25px" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Label ID="lblMensajeFormulario" runat="server"></asp:Label>
        <table width="95%">
            <tr>
                <td runat="server" id="aviso">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="HdCodigo_poa" runat="server" />
    </form>
</body>
</html>
