<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmboleta.aspx.vb" Inherits="frmboleta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            color: #0000FF;
            font-family: Arial;
            font-size: x-small;
            background-color: #F0F0F0;
        }
        .style2
        {
            color: #0000FF;
            font-family: Arial;
            font-size: x-small;
            background-color: #F0F0F0;
        }
        .style3
        {
            font-family: Arial;
            font-size: x-small;
        }
        .style4
        {
            width: 100px;
            height: 24px;
        }
        .style5
        {
            width: 257px;
            height: 24px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style7
        {
            font-family: Arial;
            font-size: x-small;
            width: 257px;
        }
        .style11
        {
            height: 20px;
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style12
        {
            height: 20px;
            font-family: Arial;
            font-size: x-small;
        }
        .style14
        {
            height: 23px;
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style15
        {
            width: 100px;
            height: 23px;
        }
        .style20
        {
            width: 257px;
            height: 21px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style21
        {
            height: 21px;
            font-family: Arial;
            font-size: x-small;
        }
        .style22
        {
            height: 22px;
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style23
        {
            height: 22px;
            font-family: Arial;
            font-size: x-small;
        }
        .style25
        {
            height: 26px;
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style26
        {
            width: 100px;
            height: 26px;
        }
        .style31
        {
            width: 100px;
            height: 23px;
            font-family: Arial;
            font-size: x-small;
        }
        .style32
        {
            font-family: Arial;
            font-size: small;
            text-decoration: underline;
            color: #0000FF;
        }
        .style35
        {
            height: 25px;
            font-family: Arial;
            font-size: x-small;
        }
        .style36
        {
            height: 23px;
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
            font-weight: bold;
        }
        .style40
        {
            width: 129px;
            height: 24px;
        }
        .style42
        {
            width: 129px;
            height: 23px;
        }
        .style44
        {
            width: 129px;
            height: 21px;
        }
        .style45
        {
            width: 129px;
            height: 26px;
        }
        .style49
        {
            width: 83px;
            height: 24px;
        }
        .style52
        {
            width: 83px;
            height: 23px;
        }
        .style54
        {
            width: 83px;
            height: 21px;
        }
        .style55
        {
            width: 83px;
            height: 26px;
        }
        .style57
        {
            font-family: Arial;
            font-size: x-small;
            width: 83px;
        }
        .style58
        {
            width: 83px;
            background-color: #F0F0F0;
            text-align: right;
            font-size: small;
            font-family: Arial;
        }
        .style59
        {
            width: 443px;
            height: 24px;
        }
        .style61
        {
            width: 443px;
            height: 23px;
        }
        .style63
        {
            width: 443px;
            height: 21px;
        }
        .style64
        {
            width: 443px;
            height: 26px;
        }
        .style66
        {
            font-family: Arial;
            font-size: x-small;
            width: 443px;
        }
        .style68
        {
            font-family: Arial;
            font-size: x-small;
            width: 129px;
        }
        .style69
        {
            width: 129px;
            background-color: #F0F0F0;
            font-size: small;
            font-family: Arial;
            text-align: right;
        }
        .style70
        {
            width: 372px;
            height: 24px;
        }
        .style73
        {
            width: 372px;
            height: 23px;
        }
        .style75
        {
            width: 372px;
            height: 21px;
        }
        .style76
        {
            width: 372px;
            height: 26px;
        }
        .style78
        {
            font-family: Arial;
            font-size: x-small;
            width: 372px;
        }
        .style80
        {
            font-family: Arial;
            font-size: small;
            width: 372px;
            color: #0000FF;
            height: 14px;
            background-color: #F0F0F0;
        }
        .style81
        {
            width: 83px;
            height: 14px;
            background-color: #F0F0F0;
            font-family: Arial;
            font-size: small;
            text-align: right;
        }
        .style82
        {
            height: 14px;
            color: #993300;
            font-family: Arial;
            font-size: x-small;
            font-weight: bold;
        }
        .style83
        {
            font-family: Arial;
            font-size: x-small;
            font-weight: bold;
            color: #0000FF;
            background-color: #F0F0F0;
            width: 257px;
        }
        .style84
        {
            font-family: Arial;
            font-size: x-small;
            width: 372px;
            font-weight: bold;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style85
        {
            font-family: Arial;
            font-size: x-small;
            width: 443px;
            font-weight: bold;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style87
        {
            width: 129px;
        }
        .style88
        {
            width: 372px;
        }
        .style89
        {
            width: 83px;
        }
        .style90
        {
            width: 257px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style91
        {
            height: 22px;
            font-family: Arial;
            font-size: x-small;
        }
        .style99
        {
            width: 443px;
        }
        .style103
        {
            width: 100px;
        }
        .style104
        {
            width: 257px;
            height: 25px;
            font-family: Arial;
            font-size: x-small;
            color: #0000FF;
            background-color: #F0F0F0;
        }
        .style105
        {
            font-family: Arial;
            font-size: small;
            text-decoration: underline;
            text-align: right;
            color: #993300;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%
        Dim cn As New clsaccesodatos
        ' tipo=2 busca por el codigo_rco
        Dim clsEncr As New EncriptaCodigos.clsEncripta, Envcodigo As String, tipo As String, codigo_per As String, codigo_dplla As String, codigo_plla As String, dtspersonal As New System.Data.DataSet
        Dim dts As New System.Data.DataSet, dtsdat As New System.Data.DataSet
        If Me.IsPostBack = False Then
            codigo_dplla = Mid(clsEncr.Decodifica(Me.Request.QueryString("codigo_dplla")), 4)
            
            cn.abrirconexion()
            dtsdat = cn.consultar("dbo.spConsultarPlanilla", "2", codigo_dplla, "", "", "", "")
            codigo_per = dtsdat.Tables("consulta").Rows(0).Item("codigo_per")
            codigo_plla = dtsdat.Tables("consulta").Rows(0).Item("codigo_plla")
            dts = cn.consultar("dbo.spGenerarBoleta", codigo_per, codigo_plla)
            dtspersonal = cn.consultar("dbo.spPla_ConsultarDatosPersonal", "BP", codigo_per, "", "", "", "", "")
            cn.cerrarconexion()
        
        %>
        <table style="border: 1px solid #000000" cellpadding="4" cellspacing="0">
            <tr>
                <td colspan="10" 
                    style="height: 23px; text-align: center; background-color: #F0F0F0;" 
                    class="style32">
                    BOLETA DE PAGO</td>
            </tr>
            <tr>
                <td class="style5">
                    DATOS DEL EMPLEADOR</td>
                <td class="style4" colspan="2">
                </td>
                <td class="style59" colspan="2">
                </td>
                <td class="style40" colspan="2">
                </td>
                <td class="style70" colspan="2">
                </td>
                <td class="style49">
                </td>
            </tr>
            <tr>
                <td class="style11">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Nombre o Razón social :</p>
                </td>
                <td class="style12" colspan="9">
                    Universidad Católica Santo Toribio de Mogrovejo</td>
            </tr>
            <tr>
                <td class="style22">
                    <p>
&nbsp;&nbsp;&nbsp; &gt; RUC</p>
                </td>
                <td class="style91" colspan="9">
                    20395492129</td>
            </tr>
            <tr>
                <td class="style36">
                    DATOS DEL TRABAJADOR</td>
                <td class="style15" colspan="2">
                </td>
                <td class="style61" colspan="2">
                </td>
                <td class="style42" colspan="2">
                </td>
                <td class="style73" colspan="2">
                </td>
                <td class="style52">
                </td>
            </tr>
            <tr>
                <td class="style104">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Nombre del trabajador</p>
                </td>
                <td class="style35" colspan="9">
                    <%=dtsPersonal.Tables("consulta").Rows(0).Item("apellidopat_per") & " " & dtsPersonal.Tables("consulta").Rows(0).Item("apellidomat_per") & " " & dtsPersonal.Tables("consulta").Rows(0).Item("nombres_per")%></td>
            </tr>
            <tr>
                <td class="style90">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Centro Costo</p>
                </td>
                <td class="style103" colspan="2"></td>
                <td class="style99" colspan="2"></td>
                <td class="style87" colspan="2"></td>
                <td class="style88" colspan="2">
                </td>
                <td class="style89">
                </td>
            </tr>
            <tr>
                <td class="style22">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Documento de Identidad</p>
                </td>
                <td class="style23"><%=dtsPersonal.Tables("consulta").Rows(0).Item("tipodocidentidad_per") & "         Número : " & dtsPersonal.Tables("consulta").Rows(0).Item("nrodocidentidad_per")%></td>
                <td class="style23" colspan="2">&nbsp;</td>
                <td class="style23" colspan="2">&nbsp;</td>
                <td class="style23" colspan="2">&nbsp;</td>
                <td class="style23" colspan="2">&nbsp;</td>
            </tr>
            <tr>
                <td class="style20">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Inicio Actividad Laboral :</p>
                </td>
                <td class="style21" colspan="2">
                </td>
                <td class="style63" colspan="2">
                </td>
                <td class="style44" colspan="2">
                </td>
                <td class="style75" colspan="2">
                </td>
                <td class="style54">
                </td>
            </tr>
            <tr>
                <td class="style14">
                    <p>
&nbsp;&nbsp;&nbsp;&nbsp; &gt; Categoría del Trabajador</p>
                </td>
                <td class="style31" colspan="2">
                    Empleado</td>
                <td class="style61" colspan="2">
                </td>
                <td class="style42" colspan="2">
                </td>
                <td class="style73" colspan="2">
                </td>
                <td class="style52">
                </td>
            </tr>
            <tr>
                <td class="style20">
                    <p>
&nbsp;&nbsp;&nbsp; &gt; Código SPP CUSSP</p>
                </td>
                <td class="style21" colspan="9"><%=dtsPersonal.Tables("consulta").Rows(0).Item("codigoseguro_per")%></td>
            </tr>
            <tr>
                <td class="style25">
                    <p>
&nbsp;&nbsp;&nbsp; &gt; Régimen Pensionario</p>
                </td>
                <td class="style26" colspan="2">
                </td>
                <td class="style64" colspan="2">
                </td>
                <td class="style45" colspan="2">
                </td>
                <td class="style76" colspan="2">
                </td>
                <td class="style55">
                </td>
            </tr>
            <tr>
                <td class="style90">
                    <p>
&nbsp;&nbsp;&nbsp; &gt; N° días laborados</p>
                </td>
                <td colspan="4">
                </td>
                <td class="style87" colspan="2">
                </td>
                <td class="style88" colspan="2">
                </td>
                <td class="style89">
                </td>
            </tr>
            <tr style="border-style: solid; border-width: 1px; border-color: #000000">
                <td colspan="3" style="border: 1px solid #CCCCFF; height: 21px" class="style1">
                    Ingresos</td>
                <td colspan="4" 
                    style="border: 1px solid #CCCCFF; height: 21px; text-align: center;" 
                    class="style2" rowspan="0">
                    Egresos</td>
                <td colspan="3" 
                    style="border: 1px solid #CCCCFF; height: 21px; text-align: center;" 
                    class="style2">
                    Aportes</td>
            </tr>
            <%
                Dim i As Integer
                Dim totalingreso As Double = 0, totalegreso As Double = 0, totalaporte As Double = 0
                Dim tipoconsulta As String
                tipoconsulta = clsEncr.Codifica("0691")
                For i = 1 To dts.Tables("consulta").Rows.Count
             %>
	<div style="overflow : auto">
            <tr>
		
                <td class="style7"><%=dts.Tables("consulta").Rows(i - 1).Item("Ingresosdescripcion_rub")%></td>
                <%
                    Dim importeIngreso As String, importeegreso As String, importeaporte As String
                    If IsNumeric(dts.Tables("consulta").Rows(i - 1).Item("Ingresosimporte")) = False Then
                        importeIngreso = ""
                    Else
                        importeIngreso = Format(dts.Tables("consulta").Rows(i - 1).Item("Ingresosimporte"), "###,###,##0.00")
                        totalingreso = totalingreso + dts.Tables("consulta").Rows(i - 1).Item("Ingresosimporte")
                    End If
                    If IsNumeric(dts.Tables("consulta").Rows(i - 1).Item("Egresosimporte")) = False Then
                        importeegreso = ""
                    Else
                        importeegreso = Format(dts.Tables("consulta").Rows(i - 1).Item("Egresosimporte"), "###,###,##0.00")
                        totalegreso = totalegreso + dts.Tables("consulta").Rows(i - 1).Item("egresosimporte")
                    End If
                    If IsNumeric(dts.Tables("consulta").Rows(i - 1).Item("Aporteimporte")) = False Then
                        importeaporte = ""
                    Else
                        importeaporte = Format(dts.Tables("consulta").Rows(i - 1).Item("Aporteimporte"), "###,###,##0.00")
                        totalaporte = totalaporte + dts.Tables("consulta").Rows(i - 1).Item("aporteimporte")
                    End If
                    
                    
                    %>
                <td style="width: 100px; text-align: right;" class="style3" colspan="2"><%=importeIngreso%></td>
                <td class="style66" colspan="2"> <img src="iconos/btn_arrow_right.gif" onclick="window.open('frmverdeudacobrar.aspx?modo=AX&id=<%=clsEncr.codifica("069" & dts.tables("consulta").rows(i-1).item("egresoscodigo_dplla"))%>&tipo=<%=tipoconsulta%>','','height=400px, width=1200px')" /><%=dts.Tables("consulta").Rows(i - 1).Item("Egresosdescripcion_rub").ToString%></td>
                <td style="text-align: right;" class="style68" colspan="2"><%=importeegreso%></td>
                <td class="style78" colspan="2"><%=dts.Tables("consulta").Rows(i - 1).Item("Aportedescripcion_rub")%></td>
                <td style="text-align: right;" class="style57"><%=importeaporte%></td>
            </tr>
	</div>
            <%
                next
             %>
            <tr>
                <td class="style83">
                    Total Ingresos :</td>
                <td style="width: 100px; background-color: #F0F0F0; font-family: Arial; font-size: small; text-align: right;" 
                    colspan="2"><%=Format(totalingreso, "###,###,##0.00")%></td>
                <td class="style85" colspan="2">Total Egresos :</td>
                <td class="style69" colspan="2"><%=Format(totalegreso, "###,###,##0.00")%></td>
                <td class="style84" colspan="2">Total Aportes :</td>
                <td class="style58"><%=Format(totalaporte, "###,###,##0.00")%></td>
            </tr>
            <tr>
                <td class="style82" colspan="7">
                    *Importes en Nuevos soles</td>
                <td class="style80" colspan="2">
                    Total Pagar :</td>
                <td class="style81">
                    <%=Format(totalingreso - totalegreso, "###,###,##0.00")%></td>
            </tr>
            <tr>
                <td colspan="10" class="style105">
                    Sistema de Tesorería V1.0</td>
            </tr>
        </table>
    
    </div>
    <%
    End If
     %>
    <p>
        <asp:Button ID="cmdcerrar" runat="server" Text="Cerrar" />
    </p>
    </form>
</body>
</html>
