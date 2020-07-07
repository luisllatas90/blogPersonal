<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListarRendiciones.aspx.vb" Inherits="Rendiciones_frmListarRendiciones" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <script src="funciones.js"></script>
    <link  rel ="stylesheet" href="estilo.css"/>
    <title>Tesoreria  USAT
    <%
        Dim codigo_usu As Integer
        codigo_usu = Request.QueryString("id")
     %>
    </title>
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
            
    function MostrarDetalleRendicion(codigo_rend, idUsu)
        {
                if (confirm("¿Desea editar el registro?"))
                    {
                
                        window.open("frmdetallerendicion.aspx?codigo_rend=" + codigo_rend + "&id="+ idUsu,'ta',"toolbar=no,width=1280,height=750,scrollbars=true");
                        //window.location.href=  "frmdetallerendicion.aspx?codigo_rend=" + codigo_rend;//,'ta',"toolbar=no,width=1200,height=750,scrollbars=true");
                     }
        }
    </script>

</head>
<body bgcolor="white" style="background-color: whitesmoke" scroll="auto">

    <form id="form1" runat="server">
   
        <table width="96%" height="20%" border="0" cellspacing="0" cellpadding="0" style="border-collapse: collapse" bordercolor="#111111" >
            <tr>
                <td  colspan="3" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid;" bgcolor="darkolivegreen" >
                    <strong><span style="font-size: 10pt; color: #ffffff; font-family: Courier New;">
                    
                    Monitoreo de Rendiciones</span></strong></td>
            </tr>
            <tr height="1%" >
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; background-color: darkolivegreen; height: 1%;" bgcolor="darkolivegreen">
                    <span style="color: #ffffff; font-family: Courier New;"><strong>
                    
                    Ver rendiciones de :</strong></span></td>
                <td bgcolor="white" style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; height: 1%;" colspan="2">
                    &nbsp;<asp:TextBox ID="txtCliente" runat="server" Width="401px"></asp:TextBox>
                    <asp:CheckBox ID="chktodos" runat="server" Text="Todos" Font-Names="Courier New" Font-Size="11pt" Width="136px" /></td>
            </tr>
            <tr>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; background-color: darkolivegreen;" bgcolor="darkolivegreen"  >                    
                    <strong><span style="font-size: 9pt; color: #ffffff; font-family: Courier New;">Estado :</span></strong></td>
                <td style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid;"    bgcolor="white" colspan="2">
                    &nbsp;<asp:DropDownList ID="cboestado" runat="server" Height="24px"
                        Width="144px" Font-Names="Courier New" Font-Size="10pt">
                        <asp:ListItem Value="P">Pendientes</asp:ListItem>
                        <asp:ListItem Value="F">Finalizadas</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="cmdbuscar" runat="server" BackColor="LemonChiffon" Font-Names="Courier New"
                        Font-Size="8pt" Height="24px" Text="Buscar" Width="104px" BorderStyle="Groove" />
                    <asp:Button ID="cmdexportar" runat="server" BackColor="LemonChiffon" Font-Names="Verdana"
                        Font-Size="8pt" Height="24px" Text="Exportar" Width="104px" BorderStyle="Groove" />
                    <asp:Button ID="Button2" runat="server" BackColor="LemonChiffon" Font-Names="Verdana"
                        Font-Size="8pt" Height="24px" Text="Enviar Correo" Width="104px" BorderStyle="Groove" />&nbsp;
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width ="100%">
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
            <tr onmouseenter="resaltar(this,1)" onmouseleave ="resaltar(this,0)">
                <td style="width: 2%;" bgcolor="#ffffff"><img onclick="Mostrar(this,'cli<%=dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl")%>')"  src="iconos/mas.gif" align="absMiddle" height="9"/></td>
                <td style="width: 30%;" bgcolor="#ffffff"><%=dtscliente.Tables("consulta").Rows(k - 1).Item("nombres")%></td>
                <td style="width: 10%;" bgcolor="#ffffff"><%=dtscliente.Tables("consulta").Rows(k - 1).Item("tipo")%>
                </td>
                <td style="width: 10%;" bgcolor="#ffffff"><%=dtscliente.Tables("consulta").Rows(k - 1).Item("identificador")%>
                </td>
                <td style="width: 10%;" bgcolor="#ffffff">
                </td>
                <td style="width: 10%;" bgcolor="#ffffff">
                </td>
            </tr>
            <tr ID ="cli<%=dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl")%>" style="display :none" >
                <td>
                </td>
                <td colspan="5">
        <table width="100%" height= "12px" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" bordercolor="gray" bgcolor="white">  
            <tr class="etabla">
                <td style="height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" bgcolor="#336666" width="2%">
                    </td>
                <td   bgcolor="#336666" style="width: 25%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Cliente</td>
                <td bgcolor="#336666" style="width: 10%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Fecha</td>
                <td bgcolor="#336666" style="width: 10%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Documento</td>
                <td bgcolor="#336666" style="width: 8%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Nº</td>
                <td bgcolor="#336666" style="width: 8%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Moneda</td>
                <td bgcolor="#336666" style="width: 8%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Monto</td>
                <td bgcolor="#336666" style="width: 8%; height: 14px; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid; color: #ffffff;" >
                    Estado</td>
                <td bgcolor="#336666" style="border-right: #cccccc 1px solid; border-top: #cccccc 1px solid;
                    border-left: #cccccc 1px solid; width: 10%; color: #ffffff; border-bottom: #cccccc 1px solid;
                    height: 14px">Observación</td>
            </tr>
            <%
                Dim dts As New System.Data.DataSet
                Dim i As Integer
                
                dts = cn.consultar("dbo.spDocumentosEgresoRendir", "1", dtscliente.Tables("consulta").Rows(k - 1).Item("codigo_tcl"), dtscliente.Tables("consulta").Rows(k - 1).Item("Estado"), "", "", "")
                'cn.cerrarconexion()
                For i = 1 To dts.Tables("consulta").Rows.Count
             %>
            <tr id ="F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr") %>" onmouseenter="resaltar(this,1);resaltar(document.getElementById('obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),1)"  onmouseleave ="resaltar(this,0);resaltar(document.getElementById('obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),0)" >
                <td  bgcolor="#ffffff" width="5%" rowspan="2" align =center>
                    <img onclick="Mostrar(this,'S<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>')"  
                        src="iconos/mas.gif" align="absMiddle" height="9" style="height: 9px"/> </td>
                <td ><%=dtscliente.Tables("consulta").Rows(k - 1).Item("nombres")%> </td>
                <td><%=Format(dts.Tables("consulta").Rows(i - 1).Item("fechagen_egr"), "dd/MM/yyyy")%></td>
                <td>
                    <%=dts.Tables("consulta").Rows(i - 1).Item("descripcion_tdo")%></td>
                <td>
                    <%=dts.Tables("consulta").Rows(i - 1).Item("seriedoc_egr") & "-" & dts.Tables("consulta").Rows(i - 1).Item("numerodoc_egr")%></td>
                <td>
                    <%=dts.Tables("consulta").Rows(i - 1).Item("descripcion_tip")%></td>
                <td style="width: 59px">
                    <%=Format(dts.Tables("consulta").Rows(i - 1).Item("importe_egr"), "###,##0.00")%></td>
                <td>
                </td>
                <td> <%=dts.Tables("consulta").Rows(i - 1).Item("usuarioreg_egr")%>
                </td>
            </tr>
            <tr ID="obs<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr") %>" onmouseenter="resaltar(this,1);resaltar(document.getElementById('F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),1)"  onmouseleave ="resaltar(this,0);resaltar(document.getElementById('F<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>'),0)" >
                <td colspan="3">
                </td>
                <td colspan="5" style="font-size: 9px; font-style :italic; color : Blue" ><%="OBS :" & dts.Tables("consulta").Rows(i - 1).Item("observacion_egr")%></td>
            </tr>
            <tr id="S<%=dts.Tables("consulta").Rows(i - 1).Item("codigo_egr")%>" style="display:none">
          
            
            
                <td style="font-family: 'Courier New'; border-right: #ffffff 1px solid; border-top: #cccccc 1px solid; border-bottom: #cccccc 1px solid; border-left: #ffffff 1px solid;" width="5%"></td>
                <td colspan="8" style="font-family: 'Courier New'; border-right: #cccccc 1px solid; border-top: #cccccc 1px solid; border-left: #cccccc 1px solid; border-bottom: #cccccc 1px solid;">
                    <table width="100%" border="1" cellpadding="3" cellspacing="0" style="border-collapse: collapse" height ="100%">
                        <tr >
                            <td bgcolor="#006633" style="font-size: 10px; width: 2%; color: #ffffff; font-style: normal;">
                            </td>
                            <td style="width: 30%; font-size: 10px; color: #ffffff; font-style: normal;" bgcolor="#006633">
                                Rubro / Servicio</td>
                            <td style="width: 10%; font-size: 10px; color: #ffffff; font-style: normal;" bgcolor="#006633">
                                Importe&nbsp;</td>
                            <td style="width: 30%; font-size: 10px; color: #ffffff; font-style: normal;" bgcolor="#006633">
                                Centro de Costos</td>
                            <td bgcolor="#006633" style="font-size: 10px; width: 8%; color: #ffffff; font-style: normal;">
                                Estado</td>
                            <td style="width: 28%; font-size: 10px; color: #ffffff; font-style: normal;" bgcolor="#006633">
                                Observación</td>
                            <td bgcolor="#006633" style="font-size: 10px; width: 178px; color: #ffffff; font-style: normal;">
                            </td>
                        </tr>
                          <%
                              Dim dtsDetalledocumento As New System.Data.DataSet, j As Integer
                              'cn.abrirconexion()
                              dtsDetalledocumento = cn.consultar("spDocumentosEgresoRendir", "2", dts.Tables("consulta").Rows(i - 1).Item("codigo_egr"), "", "", "", "")
                              'cn.cerrarconexion()
                              For j = 1 To dtsDetalledocumento.Tables("consulta").Rows.Count
                                  'If dts.Tables("consulta").Rows(i - 1).Item("codigo_egr") = 66 Then
                            
                                  'End If
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
                            <td ><img src="iconos/corregir.gif"  alt ="Editar Registro" 
                                    onclick ="MostrarDetalleRendicion(<%=dtsDetalledocumento.Tables("consulta").Rows(j - 1).Item("codigo_rend")%>, <%=Me.Request.QueryString("id")%> )" 
                                    style="height: 16px"  />  </td>
                            <% 
                                Else
                            %>                                                                
                            <td></td>
                            <%
                                End If
                             %>
                        </tr>
                        <%
                         Next
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
        <!---</div>-->
    </form>
</body>
</html>