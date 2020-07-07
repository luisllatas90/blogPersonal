<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmverdeudacobrar.aspx.vb" Inherits="frmverdeudacobrar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <style type="text/css">
        .style1
        {
            width: 98%;
        }
        .style3
        {
            font-family: Arial;
            font-size: x-small;
        }
        .style4
        {
            color: #0000FF;
            font-family: Arial;
            font-size: x-small;
            background-color: #F0F0F0;
        }
        .style5
        {
            font-family: Arial;
            font-size: x-small;
            height: 14px;
        }
        .style6
        {
            height: 68px;
        }
        .style7
        {
            color: #993300;
            font-family: Arial;
            font-size: x-small;
            background-color: #FFFFFF;
            font-weight: bold;
        }
            .style8
            {
                color: #0000FF;
                font-family: Arial;
                font-size: x-small;
                background-color: #F0F0F0;
                width: 99px;
            }
            .style9
            {
                font-family: Arial;
                font-size: x-small;
                width: 99px;
            }
            .style10
            {
                width: 99px;
            }
            .style11
            {
                color: #0000FF;
                font-family: Arial;
                font-size: x-small;
                background-color: #F0F0F0;
                width: 363px;
            }
            .style12
            {
                font-family: Arial;
                font-size: x-small;
                width: 363px;
            }
            .style13
            {
                width: 363px;
            }
    </style>
    <script type="text/javascript">
        function Mostrar(y,codigo_egr)
        {
            
            var x, y;
            x=   document.getElementById(codigo_egr);            
            if (x.style.display=='none')
                {                
                    x.style.display='';                    
                    y.src="iconos/menos.gif";                    
                }
            else
                {
                    x.style.display='none';                    
                    y.src="iconos/mas.gif";
                }            
        }
    </script> 
    <title>Página sin título</title>
</head>
<body>
    <form id="form1" runat="server">
    <%
        Dim cn As New clsaccesodatos
        ' tipo=2 busca por el codigo_rco
        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String, tipo As String, Modo As String = ""
        If Me.IsPostBack = False Then
            
            Envcodigo = Mid(clsEncr.Decodifica(Me.Request.QueryString("id")), 4)
            tipo = Mid(clsEncr.Decodifica(Me.Request.QueryString("tipo")), 4)
            Modo = Me.Request.QueryString("modo")
        End If
    
    %>
    <div style=" overflow :scroll; width :1233px; height : 301px">
    <table class="style1" style="border: 1px solid #000000" width="900px" >
        <tr>
            <td class="style7" colspan="9">
                Cargos o deudas a pagar USAT</td>
        </tr>
        <tr>
            <td class="style4">
                &nbsp;</td>
            <td class="style4">
                Id</td>
            <td class="style4">
                Nombres</td>
            <td class="style4">
                Tipo</td>
            <td class="style4">
                Moneda</td>
            <td class="style4">
                Importe</td>
            <td class="style8">
                Importe cancelado</td>
            <td class="style11">
                Observación</td>
            <td class="style4">
                img</td>
        </tr>
        <%
            Dim dts As New System.Data.DataSet, j As Integer
            cn.abrirconexion()
            If Modo = "AX" Then ' anexo del detalle de la planilla
                dts = cn.consultar("dbo.spConsultarAnexoDetalleplanilla", tipo, Envcodigo, "", "", "", "", "", "", "", "", "", "", "")
            End If
            cn.cerrarconexion()
            For j = 1 To dts.Tables("consulta").Rows.Count
                    
        %>
        <tr>
            <td class="style3"><img src ="iconos/mas.gif"  onclick="Mostrar(this, '<%=dts.tables("consulta").rows(j-1).item("codigo_ddc")%>')" /></td>
            <td class="style3"><%=dts.Tables("consulta").Rows(j - 1).Item("codigo_ddc")%></td>
            <td class="style3"><%=dts.Tables("consulta").Rows(j - 1).Item("nombres")%></td>
            <td class="style3"><%=dts.Tables("consulta").Rows(j - 1).Item("tipo")%></td>
            <td class="style3"><%=dts.Tables("consulta").Rows(j - 1).Item("descripcion_tip")%></td>
            <td class="style3"><%=dts.Tables("consulta").Rows(j - 1).Item("importe_ddc")%></td>
            <td class="style9"><%=dts.Tables("consulta").Rows(j - 1).Item("importecancelado_ddc")%></td>
            <td class="style12"><%=dts.Tables("consulta").Rows(j - 1).Item("observacion_ddc")%></td>
            <td class="style3">img2</td>
        </tr>
        <tr id ="<%=dts.tables("consulta").rows(j-1).item("codigo_ddc")%>" style="display:none">
            <td class="style6">
                &nbsp;</td>
            <td colspan="8" class="style6">
                <%
                    Dim i As Integer
                    Dim dtsdetalleingreso As New System.Data.DataSet

                    cn.abrirconexion()
                    dtsdetalleingreso = cn.consultar("dbo.sp_verdocumentoemitidos", "DIDPC", dts.Tables("consulta").Rows(j - 1).Item("codigo_ddc"), "", "")
                    cn.cerrarconexion()
                 %>
                <table class="style1" style="border: 1px solid #000000">
                    <tr>
                        <td class="style4">
                            Id</td>
                        <td class="style4">
                            Documento</td>
                        <td class="style4">
                            Rubro</td>
                        <td class="style4">
                            Importe Unitario</td>
                        <td class="style4">
                            Cantidad</td>
                        <td class="style4">
                            Importe cancelado</td>
                        <td class="style4">
                            Centro de costos</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                        <td class="style4">
                            &nbsp;</td>
                    </tr>
                    <% 
                        For i = 1 To dtsdetalleingreso.Tables("consulta").Rows.Count
                        
                            
                    %>
                    <tr>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("codigo_din")%></td>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("descripcion_tdo")%></td>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("descripcion_rub")%></td>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("codigo_ing")%></td>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("cantidad_din")%></td>
                        <td class="style3"><%=dtsdetalleingreso.Tables("consulta").Rows(i - 1).Item("importe_din")%></td>
                        <td class="style3"></td>
                        <td><img src ="iconos/buscar.gif" onclick="window.open('frmdocumentoingreso.aspx?codigo_ing=<%=clsEncr.Codifica("069" & dtsdetalleingreso.tables("consulta").rows(i-1).item("codigo_ing"))%>','','toolbar=no, width=1200, height=750')"/></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <%
                        next %>
                    <tr>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                        <td class="style5">
                            </td>
                    </tr>
                    <% 
                    Next
                        %>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td class="style10">
                &nbsp;</td>
            <td class="style13">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
