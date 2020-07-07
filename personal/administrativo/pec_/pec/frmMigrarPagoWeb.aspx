<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMigrarPagoWeb.aspx.vb" Inherits="administrativo_pec_frmMigrarPagoWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../css/estilo.css" rel="stylesheet" type="text/css" />
    <script src="../../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>
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
        <script type="text/javascript" language="javascript">
            function MarcarCursos(obj) {
                //asignar todos los controles en array
                var arrChk = document.getElementsByTagName('input');
                for (var i = 0; i < arrChk.length; i++) {
                    var chk = arrChk[i];
                    //verificar si es Check
                    if (chk.type == "checkbox") {
                        chk.checked = obj.checked;
                        if (chk.id != obj.id) {
                            PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                        }
                    }
                }
            }

            function PintarFilaMarcada(obj,value) {
                if (value == true) {
                    obj.style.backgroundColor = "#FFE7B3"
                } else {
                    obj.style.backgroundColor = "white"
                }
            }        
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <% Response.Write(ClsFunciones.CargaCalendario)%>
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
                <asp:Label ID="lblTitulo" runat="server" Text="Migración de Pago" 
                    Font-Bold="True" Font-Size="11pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width:12%">
                <asp:Label ID="Label1" runat="server" Text="Centro de Costos:"></asp:Label>
            </td>
            <td style="width:48%">
                <asp:DropDownList ID="ddpCentroCostos" runat="server" Width="99%" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td style="width:40%">
                <asp:Panel ID="tbBuscar" runat="server" Visible ="false" >
                    <table width="100%">
                        <tr>            
                            <td style="width:25%">
                                <asp:Label ID="Label2" runat="server" Text="Fecha:"></asp:Label>
                            </td>
                            <td style="width:45%">
                                <asp:TextBox ID="txtFecha" runat="server" Height="14px" Width="80px" 
                                    CssClass="Cajas2"></asp:TextBox>
                                <input 
                                id="btnFechaInicio" 
                                onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.txtFecha,'dd/mm/yyyy')" class="cunia" type="button" />
                            </td>
                            <td style="width:30%" align="center">
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
                <asp:Panel ID="pnlListaPagos" runat="server" Height="520px" 
                    ScrollBars="Vertical" Width="100%">
                <asp:GridView ID="grwListaPagos" runat="server"
                    AutoGenerateColumns="False" DataKeyNames="codigo_pso" CellPadding="3" 
                    SkinID="skinGridViewLineas" Width="98%">
                    <Columns>
                        <asp:BoundField HeaderText="Participante" DataField="participante" >
                            <ItemStyle Width="34%"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Tipo Doc." DataField="tipoDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center" Width="10%"/>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Nro. Doc." DataField="numeroDocIdent_Pso">
                            <ItemStyle HorizontalAlign="Center" Width="10%"/>
                        </asp:BoundField>            
                        <asp:BoundField DataField="fecha_Pago" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                            HeaderText="Fecha Pago" >
                            <ItemStyle HorizontalAlign="Center" Width="14%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="descripcion_Tdo" HeaderText="Documento" >
                            <ItemStyle HorizontalAlign="Center" Width="14%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="montoTotal_Deu" HeaderText="Monto" >
                            <ItemStyle HorizontalAlign="Right" Width="6%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="pago_Deu" 
                            HeaderText="Pago" >
                            <ItemStyle HorizontalAlign="Right" Width="6%"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="saldo_Deu" 
                            HeaderText="Saldo" >
                            <ItemStyle HorizontalAlign="Right" Width="6%"/>
                        </asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkTodos" runat="server" onclick="MarcarCursos(this)" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="hf_ID_pagoweb" runat="server" Value='<%# Eval("id_pagoweb") %>' />
                                <asp:CheckBox ID="chkSeleccionar" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="1%" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:90%">
                                    No se encontraron pagos realizados en el Campus Virtual.</td>
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
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Button ID="btnMigrar" runat="server" Text="           MIGRAR" 
                    CssClass="respondercomentario" Height="40px" Visible="False" />
                <asp:HiddenField ID="hfUsuReg" runat="server" />
            </td>
        </tr>
    </table>
            
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnMigrar" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
