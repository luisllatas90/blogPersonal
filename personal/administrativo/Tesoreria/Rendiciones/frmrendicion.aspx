<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmrendicion.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script src="funciones.js"></script>
<link  rel ="stylesheet" href="estilo.css"/> 
<title>Tesoreria  USAT</title> 
    <script>
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
            
    function MostrarDetalleRendicion(codigo_rend)
        {
                if (confirm("¿Desea editar el registro?"))
                    {
                
                        window.open("frmdetallerendicion.aspx?codigo_rend=" + codigo_rend,'ta',"toolbar=no,width=1280,height=750,scrollbars=true");
                        //window.location.href=  "frmdetallerendicion.aspx?codigo_rend=" + codigo_rend;//,'ta',"toolbar=no,width=1200,height=750,scrollbars=true");
                     }
        }
    </script>
<script language="javascript" type="text/javascript">

function MostrarDetalleRendicion(codigo_rend)
    {
        window.open("frmdetallerendicion.aspx?codigo_rend=" + codigo_rend,'ta',"toolbar=no,width=1200,height=750,scrollbars=true");
    }
</script>
</head>
<body style="color: #000000"  bgcolor="#EEEEEE">
<script language="javascript"  > 
   
</script>
    <form id="form1" runat="server" >    
        
        <table >
            <tr>
                <td  colspan="2" style="height: 15px"  bgcolor="darkolivegreen">
                    <span style="color: #ffffff"><strong>
                    
                    Rendiciones</strong></span></td>
            </tr>
            <tr class="etabla">
                <td style="height: 21px; width: 238px; text-align: left;" bgcolor="darkolivegreen">
                    <span style="color: white">
                    Estado de Rendición :</span></td>
                <td style="width: 438px; height: 21px;"  align=left  bgcolor="darkolivegreen">
                    <asp:DropDownList ID="cboestado" runat="server" Width="320px" Font-Names="Courier New" Font-Size="10pt">
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="F">Finalizadas</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="cmdbuscar" runat="server" BackColor="LemonChiffon" BorderStyle="Groove"
                        Font-Names="Courier New" Font-Size="8pt" Height="24px" Text="Buscar" Width="104px" /></td>
            </tr>
            
        <tr>
        </tr>
        </table>
        <br />
        <table width="100%">
            <%
                'Dim dtscliente As New System.Data.DataSet, k As Integer
                'If Me.IsPostBack = False Then
                Dim k As Integer
                'Dim cn As New clsaccesodatos
                'cn.abrirconexion()
                'dtscliente = cn.consultar("spDocumentosEgresoRendir", "3", "", "", "", "", "")
                'cn.cerrarconexion()
                cn.abrirconexion()
                For k = 1 To dtscliente.Tables("consulta").Rows.Count
            %>
            <tr onmouseenter="resaltar(this,1)" onmouseleave="resaltar(this,0)">
                <td bgcolor="#ffffff" style="width: 2%">
                    <img align="absMiddle" height="9" onclick="Mostrar(this,'cli<%=dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl")%>')" src="iconos/mas.gif" /></td>
                <td bgcolor="#ffffff" style="width: 30%">
                    <%=dtscliente.Tables("consulta").Rows(k - 1).Item("nombres")%>
                </td>
                <td bgcolor="#ffffff" style="width: 10%">
                    <%=dtscliente.Tables("consulta").Rows(k - 1).Item("tipo")%>
                </td>
                <td bgcolor="#ffffff" style="width: 10%">
                    <%=dtscliente.Tables("consulta").Rows(k - 1).Item("identificador")%>
                </td>
                <td bgcolor="#ffffff" style="width: 10%">
                </td>
                <td bgcolor="#ffffff" style="width: 10%">
                </td>
            </tr>
            <tr id='cli<%=dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl")%>' style="display: none">
                <td>
                </td>
                <td colspan="5">
                    <table bgcolor="white" border="1" bordercolor="gray" cellpadding="3" cellspacing="0"
                        height="12" style="border-collapse: collapse" width="100%">
                        <tr class="etabla">
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px" width="2%">
                            </td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 25%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Cliente</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 10%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Fecha</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 10%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Documento</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 8%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Nº</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 8%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Moneda</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 8%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Monto</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 8%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Estado</td>
                            <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; width: 10%; color: #ffffff; border-bottom: #cccccc 1px solid;
                                height: 14px">
                                Observación</td>
                        </tr>
                        <%
                Dim dts As New System.Data.DataSet
                Dim i As Integer
                
                            dts = cn.consultar("spDocumentosEgresoRendir", "1", dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl"), dtscliente.Tables("consulta").Rows(k - 1).Item("Estado"), "", "", "")
                'cn.cerrarconexion()
                For i = 1 To dts.Tables("consulta").Rows.Count
                        %>
                        <tr id='F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr") %>' onmouseenter="resaltar(this,1);resaltar(document.getElementById('obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),1)"
                            onmouseleave="resaltar(this,0);resaltar(document.getElementById('obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),0)">
                            <td align="center" bgcolor="#ffffff" rowspan="2" width="5%">
                                <img align="absMiddle" height="9" onclick="Mostrar(this,'S<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>')"
                                    src="iconos/mas.gif" />
                            </td>
                            <td>
                                <%=dtscliente.Tables("consulta").Rows(k - 1).Item("nombres")%>
                            </td>
                            <td>
                                <%=Format(dts.Tables("consulta").Rows(i - 1).Item("fechagen_egr"), "dd/MM/yyyy")%>
                            </td>
                            <td>
                                <%=dts.Tables("consulta").Rows(i - 1).Item("descripcion_tdo")%>
                            </td>
                            <td>
                                <%=dts.Tables("consulta").Rows(i - 1).Item("seriedoc_egr") & "-" & dts.Tables("consulta").Rows(i - 1).Item("numerodoc_egr")%>
                            </td>
                            <td>
                                <%=dts.Tables("consulta").Rows(i - 1).Item("descripcion_tip")%>
                            </td>
                            <td style="width: 59px">
                                <%=Format(dts.Tables("consulta").Rows(i - 1).Item("importe_egr"), "###,##0.00")%>
                            </td>
                            <td>
                            </td>
                            <td>
                                <%=dts.Tables("consulta").Rows(i - 1).Item("usuarioreg_egr")%>
                            </td>
                        </tr>
                        
                        <tr id='obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr") %>' onmouseenter="resaltar(this,1);resaltar(document.getElementById('F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),1)"
                            onmouseleave="resaltar(this,0);resaltar(document.getElementById('F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),0)">
                            <td colspan="3">
                            </td>
                            <td colspan="5" style="font-size: 9px; color: blue; font-style: italic">
                                <%="OBS :" & dts.Tables("consulta").Rows(i - 1).Item("observacion_egr")%>
                            </td>
                        </tr>
                        <tr id='S<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>' style="display: none">
                            <td style="border-right: #ffffff 1px solid; border-top: #cccccc 1px solid; border-left: #ffffff 1px solid;
                                border-bottom: #cccccc 1px solid; font-family: 'Courier New'" width="5%">
                            </td>
                            <td colspan="8" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                                border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; font-family: 'Courier New'">
                                <table border="1" cellpadding="3" cellspacing="0" height="100%" style="border-collapse: collapse"
                                    width="100%">
                                    <tr>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 2%; color: #ffffff; font-style: normal">
                                        </td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 30%; color: #ffffff; font-style: normal">
                                            Rubro / Servicio</td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 10%; color: #ffffff; font-style: normal">
                                            Importe&nbsp;</td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 30%; color: #ffffff; font-style: normal">
                                            Centro de Costos</td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 8%; color: #ffffff; font-style: normal">
                                            Estado</td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 28%; color: #ffffff; font-style: normal">
                                            Observación</td>
                                        <td bgcolor="#006633" style="font-size: 10px; width: 178px; color: #ffffff; font-style: normal">
                                        </td>
                                    </tr>
                                    <%
                                            Dim dtsDetalledocumento As New System.Data.DataSet, j As Integer
                                            'cn.abrirconexion()
                                            dtsDetalledocumento = cn.consultar("spDocumentosEgresoRendir", "2", dts.Tables("consulta").Rows(i - 1).Item("codigo_egr"), "", "", "", "")
                                            'cn.cerrarconexion()
                                            For j = 1 To dtsDetalledocumento.Tables("consulta").Rows.Count
                                    %>
                                    <tr onmouseenter="resaltar(this,1)"  onmouseleave ="resaltar(this,0)" >
                                        <td style="width: 178px"></td>
                                        <td ><%=dtsDetalledocumento.Tables("consulta").Rows(j - 1).item("descripcion_rub")%>
                                        </td>
                                        <td style="text-align: right;"><%=Format(dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("importe_deg"), "###,##0.00")%></td>
                                        <td ><%=dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("centrocostos")%></td>
                                        <%  
                                            If dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("exigirrendicion_deg") = True Then
                                        %>
                                        <td><%=IIf(dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("EstadoRendicion") = "P", "Pendiente", "Finalizada")%>
                                        </td>
                                        <%
                                        Else
                                            %>
                                            <td></td>
                                            <%
                                        End If
                                         %>
                                        <td ><%=dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("Observacion_deg")%></td>
                                        <%  
                                            If dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("exigirrendicion_deg") = True Then
                                        %>
                                        <td ><img src="iconos/corregir.gif"  alt ="Editar Registro" onclick ="MostrarDetalleRendicion(<%=dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("codigo_rend")%>)"  />  </td>
                                        <% 
                                            Else
                                        %>                                                                
                                        <td></td>
                                        <%
                                            End If
                                         %>
                                    </tr>
                                    <% 
                                        next
                                    %>
                                </table>
                            </td>
                        </tr>
                        <%
           
                        Next
                        dts = Nothing
                        %>
                    </table>
                </td>
            </tr>
            <%
            Next
            cn.cerrarconexion()
            
            %>
        </table>
        
    
    </form>
</body>
</html>
