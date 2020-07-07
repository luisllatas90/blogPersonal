<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDeudaPagoWeb.aspx.vb" Inherits="administrativo_pec_frmDeudaPagoWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" media="screen" />
    <script type="text/javascript" src="../../../private/funciones.js"></script>        
    <style type="text/css">
        .0
        {
            background-color: #E6E6FA;
        }
        .1
        {
            background-color: #FFFCBF;
        }
        .2
        {
            background-color: #D9ECFF;
        }
        .3
        {
            background-color: #C7E0CE;
        }
        
        .5
        {
            background-color: #FFCC00;
        }
        .6
        {
            background-color: #F8C076;
        }
        .4
        {
            background-color: #CCFF66;
        }
        </style>        
        <script type="text/javascript">
            function seleccionaFoco() {
                if (event.keyCode == 13) {
                    event.keyCode = 9;
                    //document.form1.btnEntregar.click;
                }
            } 
	        function PintarFilaElegida(obj) {
	            if (obj.style.backgroundColor == "white") {
	                obj.style.backgroundColor = "#E6E6FA"//#395ACC
	            }
	            else {
	                obj.style.backgroundColor = "white"
	            }
	        }

        </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
    <table cellpadding="3" cellspacing="0" style="border: 1px solid #C2CFF1; width:100%">
        <tr>
            <td colspan="5" 
                style="border-bottom: 1px solid #0099FF; background: #E6E6FA;" 
                height="40px">
                <asp:Label ID="lblTitulo" runat="server" Text="Deuda - Pago Web" 
                    Font-Bold="True" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:14%">
                <asp:Label ID="Label1" runat="server" Text="Centro de Costos:"></asp:Label>
            </td>
            <td style="width:44%">
                <asp:DropDownList ID="ddpCentroCostos" runat="server" Width="99%" 
                    AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="width:42%">
                <asp:Panel ID="tbBuscar" runat="server" Visible ="false" >
                    <table width="100%">
                        <tr>
                            <td style="width:26%">
                                <asp:Label ID="Label2" runat="server" Text="Nombre/Doc.Ind.:"></asp:Label>
                            </td>
                            <td style="width:54%">
                                <asp:TextBox ID="txtNombre" runat="server" Height="14px" Width="95%" 
                                    CssClass="Cajas2"></asp:TextBox>
                            </td>
                            <td style="width:20%" align="center">
                                <asp:Button ID="btnBuscar" runat="server" CssClass="buscar1" Height="30px" 
                                    Text="       Buscar" Width="80px" />
                            </td>
                        </tr>
                    </table>                
                </asp:Panel>
                <asp:Panel ID="tbMensaje" runat="server" Visible ="false">
                    <table width="100%">
                        <tr>
                            <td style="width:100%">
                                <asp:Label ID="lblMensaje" runat="server" Text="No se ha habilitado el Centro de Costos para Pagos Web." ForeColor="Red" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </table>                
                </asp:Panel>
            </td>            
        </tr>
    </table>
    
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="grwListaDeudas" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="codigo_pso" CellPadding="3" 
                    SkinID="skinGridViewLineas" Width="100%">
                    <Columns>
                        <asp:BoundField HeaderText="Participante" DataField="participante" >
                        <ItemStyle Width="32%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo Doc." DataField="tipoDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center"  Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nro. Doc." DataField="numeroDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center"  Width="10%" />
                        </asp:BoundField>            
                        <asp:BoundField DataField="fechaRegistro_Deu" DataFormatString="{0:dd/MM/yyyy}" 
                            HeaderText="Fecha Reg." >
                            <ItemStyle HorizontalAlign="Center"  Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="montoTotal_Deu" HeaderText="Monto" >
                            <ItemStyle HorizontalAlign="Right" Width="8%"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_Deu" 
                            HeaderText="Saldo" Visible="False" >
                            <ItemStyle HorizontalAlign="Right"  Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_Deu_Pago" 
                            HeaderText="Saldo" >
                            <ItemStyle HorizontalAlign="Right"  Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Pagar">
                            <ItemTemplate>
                                <asp:HiddenField ID="hfCodigo_Deu" runat="server" Value='<%# Eval("codigo_Deu") %>' />
                                <asp:TextBox ID="txtSaldo" runat="server" Visible="False" Width="70px" Text='<%# Eval("saldo_Deu_Pago") %>'
                                    CssClass="monto" Height="14px"></asp:TextBox>
                                <asp:Label ID="lblSaldo" runat="server" Text='<%# Eval("saldo_Deu_Pago") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right"  Width="8%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Tipo Doc.">
                            <ItemTemplate>
                                <asp:DropDownList ID="ddlTipoDoc" runat="server" Width="98%" Visible="False">
                                </asp:DropDownList>
                                <asp:Label ID="lblTipoDoc" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"  Width="14%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Guardar">
                            <ItemTemplate>
                                <asp:ImageButton ID="ibtnEditar" runat="server" ImageUrl="~/Images/editar.gif" 
                                    onclick="ibtnEditar_Click" ToolTip="Editar" />
                                <asp:ImageButton ID="ibtnGuardar" runat="server" 
                                    ImageUrl="~/Images/guardar.gif" Visible="False" 
                                    onclick="ibtnGuardar_Click" ToolTip="Guardar" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="1%" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:90%">
                                    No se encontraron Deudas con los criterios ingresados.</td>
                                <td style="width:10%">
                                    <asp:Image ID="imgNingun" runat="server" ImageUrl="~/Images/cerrar.gif" />
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <PagerStyle BackColor="Silver" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#0B3861" ForeColor="White" Height="25px" />                
                    <RowStyle Height="22px" />
                </asp:GridView>
            </td>
        </tr>
        </table>
            
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
