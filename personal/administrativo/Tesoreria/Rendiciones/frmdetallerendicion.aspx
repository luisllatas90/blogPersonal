<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmdetallerendicion.aspx.vb" Inherits="frmdetallerendicion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script src="funciones.js"></script>
<script  src="calendario.js"></script>
<link  rel ="stylesheet" href="estilo.css"/> 

    <title>Tesoreria  USAT</title>
     <%
        Dim codigo_rend2 As Integer
        Dim idPersonal As Integer
        codigo_rend2 = Me.Request.QueryString("codigo_rend")
         ID = Me.Request.QueryString("id")
         'Response.Write(ID)
     %>
    
    <script language="javascript">
    
        function RegistrarDetallerendicion()
        {      
           window.open('frmregistrardetallerendicion.aspx?codigo_rend=<%=codigo_rend2%>','frmadjuntararchivo','toolbar=no,width=1200,height=410')         
        }
        function FinalizarRendicion()
        {
//            var respuesta ;// <%=codigo_rend2%>
//            respuesta=confirm("Desea dar por finalizada la rendición de gastos, esta operación bloqueará la edición de la misma y debe acercarse a la oficina de Tesorería con la documentación física de su rendición");
//            if (respuesta )
//                {
                    window.location.href="frmprocesar.aspx?operacion=&codigo_rend=<%=codigo_rend2%>&ID=<%=id%>";
                    //&id=<%=id%>"  ;
//                }                        
        }
          function ReactivarRendicion()
        {
            var respuesta ;// <%=codigo_rend2%>
            respuesta=confirm("Desea REACTIVAR la rendición de gastos");
            if (respuesta )
                {
                    window.location.href="frmprocesar.aspx?operacion=reactivar&codigo_rend=<%=codigo_rend2%>"  ;
                }                        
        }
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
function IMG1_onclick() {

}

function cmdagregar_onclick() {

}

function cmdReactivar_onclick() {

}

function cmdReactivar_onclick() {

}

function cmdReactivar_onclick() {

}

function cmdagregar_onclick() {

}

    </script> 
    
</head>
<body bgcolor="white" style="background-color: whitesmoke" scroll="auto">

    <form id="form1" runat="server" >
    
        
        <%
            Dim codigo_rend2 As Integer, dtsdetalleegreso As New System.Data.DataSet
            Dim dtsdetallerendicion As New System.Data.DataSet, i As Integer
                           
            codigo_rend2 = Me.Request.QueryString("codigo_rend")
            cn.abrirconexion()
            'dtsdetalleegreso = cn.consultar("dbo.sp_verdocumentoemitidos", "VDREND", codigo_rend2, "", "")
            dtsdetalleegreso = cn.consultar("TES_BuscaRendicion", codigo_rend2)
            cn.cerrarconexion()
                
            ' detalle de la rendición                                     
            'Dim codigo_rend2 As Integer
            'codigo_rend2 = Me.Request.QueryString("codigo_rend")
            cn.abrirconexion()
            dtsdetallerendicion = cn.consultar("sp_verdetallerendicion", "PR", codigo_rend2, "", "", "")
            cn.cerrarconexion()
            
     %>
                    <table cellpadding="3" cellspacing="0" bordercolor="gray" bgcolor="white" style="width: 98%; border-collapse: collapse" >
                        <tr>
                            <td colspan="5" 
                                style="font-size: 12px; color: #ffffff; font-family: 'Courier New';
                                height: 3px; background-color: darkolivegreen; font-variant: normal; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-left: darkolivegreen 1px solid; width: 73px; border-bottom: darkolivegreen 1px solid; text-align: center;">
                                Detalle</td>r
                            <td colspan="5" 
                                style="font-size: 12px; color: #ffffff; font-family: 'Courier New';
                                height: 3px; background-color: darkolivegreen; font-variant: normal; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-left: darkolivegreen 1px solid; width: 183px; border-bottom: darkolivegreen 1px solid; text-align: center;">
                                Número de Rendición :  <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("numeracionanual_rend").ToString%> </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px;
                                background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-left: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;" >
                                <span style="font-size: 9pt; font-family: Courier New">Documento:</span></td>
                            <td style="font-size: 12px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-left: darkolivegreen 1px solid; width: 73px; color: #ffffff; border-bottom: darkolivegreen 1px solid;" colspan="3">
                                <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("descripcion_tdo")%></td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Nº :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("seriedoc_egr") & "-" & dtsdetalleegreso.Tables("consulta").Rows(0).Item("numerodoc_egr")%></td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Fecha :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=format(dtsdetalleegreso.Tables("consulta").Rows(0).Item("fechagen_egr"),"dd/MM/yyyy")%>
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Usuario :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("usuarioreg_egr")%>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Rubro/ concepto:</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("descripcion_rub")%>                                
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px;
                                background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Moneda :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px;
                                background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;"><%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("Descripcion_tip")%>                            
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Importe entregado:</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                 <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("importe_deg")%>
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Importe Rendido :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                               <%=Format(dtsdetalleegreso.Tables("consulta").Rows(0).Item("montorendido_deg"), "###,##0.00")%>
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Importe devuelto :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=Format(dtsdetalleegreso.Tables("consulta").Rows(0).Item("montodevuelto_deg"), "###,##0.00")%>
                                </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Saldo &nbsp; :</td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal; border-left: darkolivegreen 1px solid; color: #ffffff; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                <%=dtsdetalleegreso.Tables("consulta").Rows(0).Item("saldorendir_deg")%>
                                </td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 3px;
                                background-color: darkolivegreen; font-variant: normal; color: #ffffff; border-left: darkolivegreen 1px solid; border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid; border-bottom: darkolivegreen 1px solid;">
                                Cliente:</td>
                            <td colspan="7" 
                                style="border-right: darkolivegreen 1px solid; border-top: darkolivegreen 1px solid;
                                font-size: 12px; border-left: darkolivegreen 1px solid; color: #ffffff; border-bottom: darkolivegreen 1px solid;
                                font-family: 'Courier New'; height: 3px; background-color: darkolivegreen; font-variant: normal">
                                <% =dtsdetalleegreso.Tables("consulta").Rows(0).Item("nombres")%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10" style="font-size: 12px; font-family: 'Courier New';
                                background-color: white; font-variant: normal; height: 30px;" 
                                valign="middle">
                    <asp:Button ID="cmdcancelar" runat="server" BackColor="LemonChiffon" Font-Bold="False"
                        Font-Names="Courier New" Font-Size="8pt" Height="24px" Style="background-image: url(iconos/salir.gif);
                        background-repeat: no-repeat; background-position: left center;" Text=" Cerrar" Width="96px" BorderStyle="Groove" />
                        <input id="Button1" style="width: 144px; background-repeat: no-repeat; background-color: lemonchiffon"                        
                        type="button" value="Finalizar operación"    language="javascript" onclick="FinalizarRendicion()" />
                        <input id="cmdagregar" style="width: 112px; background-repeat: no-repeat; background-color: lemonchiffon"
                        type="button" value="Agregar"    language="javascript" onclick="RegistrarDetallerendicion()" onclick="return cmdReactivar_onclick()" onclick="return cmdagregar_onclick()" />
                        
                        <% If Me.Request.QueryString("id") = 344 Then%>
                        
                        <input id="cmdReactivar" style="width: 112px; background-repeat: no-repeat; background-color: lemonchiffon;"
                        type="button" value="Reactivar"    language="javascript" 
                                    onclick="ReactivarRendicion()" onclick="return cmdReactivar_onclick()" 
                                    onclick="return cmdReactivar_onclick()" 
                                    onclick="return cmdReactivar_onclick()" />&nbsp;&nbsp;
                        <% end if %>

                                </td>
                        </tr>
                    </table>              
        <br />
                        
                            <table  border="1" cellpadding="3" cellspacing="0" style="width: 98%; border-collapse: collapse" bordercolor="gray" bgcolor="white">
                                <tr >
                                    <td bgcolor="darkolivegreen" style="width: 2%; color: #ffffff; font-size: 10pt;">
                                    </td>
                                    <td bgcolor="darkolivegreen" style="width: 20%; color: #ffffff; font-size: 8pt;">
                                        Tipo Documento</td>
                                    <td bgcolor="darkolivegreen" style="width: 10%; color: #ffffff; font-size: 8pt;">
                                        Serie - Número</td>
                                    <td bgcolor="darkolivegreen" style="width: 10%; color: #ffffff; font-size: 8pt;">
                                        Fecha</td>
                                    <td bgcolor="darkolivegreen" style="width: 30%; color: #ffffff; font-size: 8pt;">
                                        Institución/Empresa</td>
                                    <td bgcolor="darkolivegreen" style="width: 12%; color: #ffffff; font-size: 8pt;">
                                        Importe</td>
                                    <td bgcolor="darkolivegreen" style="font-size: 8pt; width: 30%; color: #ffffff">
                                        Observación</td>
                                        <td></td>
                                </tr>
                                <%
                                    For i = 1 To dtsdetallerendicion.Tables("consulta").Rows.Count
                                        
                                    %>
                                <tr onmouseenter="resaltar(this,1)" onmouseleave ="resaltar(this,0)" style="font-size: 8pt">
                                    <td style="height: 10px"><img src="iconos/mas.gif"     onclick ="Mostrar(this,'S<%=dtsdetallerendicion.tables("consulta").rows(i-1).item("codigo_dren")%>')"/> </td>
                                    <td style="height: 10px" ><%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("descripcion_tdo")%>
                                        </td>
                                    <td style="height: 10px" ><%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("serienumero_dren")%></td>
                                    <td style="height: 10px" ><%=Format(dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("fecha_dren"), "dd/MM/yyyy")%></td>
                                    <td style="height: 10px; width: 471px;" ><%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("institucion_dren")%></td>
                                    <td style="height: 10px" ><%=Format(dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("importe_dren"), "###,##0.00")%>
                                        </td>
                                    <td style="height: 10px">
                                        </td>
                                    <td><a href="frmprocesar.aspx?operacion=quitardetallerendicion&codigo_dren=<%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("codigo_dren")%>">Eliminar</a>
                                        &nbsp;</td>
                                    
                                </tr>
                                <tr onmouseenter="resaltar(this,1)" onmouseleave="resaltar(this,0)" style="font-size: 8pt">
                                    <td style="height: 9px; background-color: whitesmoke">
                                    </td>
                                    <td style="height: 9px; background-color: whitesmoke" colspan="2">
                                    </td>
                                    <td colspan="4" style="height: 9px; font-size: 8pt; color: blue; font-style: italic;">
                                        <span>Obs :<%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("descripcion_dren")%></span></td>
                                </tr>
                                <tr style=" display :none; font-size: 8pt;" id="S<%=dtsdetallerendicion.tables("consulta").rows(i-1).item("codigo_dren")%>" >
                                    <td style="width: 100px; height: 1px; background-color: whitesmoke;">
                                    </td>
                                    <td colspan="6" style="height: 1px; background-color: whitesmoke;">
                                        <table height ="100%" cellspacing="0" style="width: 30%; border-collapse: collapse" bordercolor="gray" bgcolor="white">
                                            <tr>
                                                <td bgcolor="darkolivegreen" style="width: 2%; color: #ffffff; height: 15px">
                                                    Tipo</td>
                                                <td bgcolor="darkolivegreen" style="width: 363px; color: #ffffff; height: 15px">
                                                    Descripción <img src="iconos/anadir.gif"  alt ="Añadir Archivo" id="IMG1" language="javascript" onclick="window.open('frmsubirarchivo.aspx?codigo_dren=<%=dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("codigo_dren")%>','frmsubirarchivo','toolbar=no, width=600, height=200');" /> </td>
                                                 
                                            </tr> 
                                            <%
                                                ' Mostrar la información de los archivos adjuntos
                                                Dim dtsdocumentosadjuntos As New System.Data.DataSet, j As Integer
                                                cn.abrirconexion()
                                                dtsdocumentosadjuntos = cn.consultar("sp_verdocumentodetalle", "DD", dtsdetallerendicion.Tables("consulta").Rows(i - 1).Item("codigo_dren"), "", "", "")
                                                cn.cerrarconexion()
                                                For j = 1 To dtsdocumentosadjuntos.Tables("consulta").Rows.Count
                                                %>                                           
                                            <tr height ="8px">
                                                <td style="width: 8px; height: 1px;"><a TARGET='_blank' href='frmdescargar.aspx?ruta=A<%=dtsdocumentosadjuntos.Tables("consulta").Rows(j - 1).Item("codigo_ddr") & dtsdocumentosadjuntos.Tables("consulta").Rows(j - 1).Item("tipo_ddr")%>'> <img border=0   src="iconos/<%=dtsdocumentosadjuntos.Tables("consulta").Rows(j - 1).item("icono_ddr")%>" /></a></td>
                                                
                                                                                               
                                                 <%
                                                     If dtsdocumentosadjuntos.Tables("consulta").Rows(j - 1).Item("descripcion_ddr") = "" Then
                                                 %>
                                                    <td style="width: 363px; height: 1px; background-color: #ffffff;">Ninguno</td> 
                                                <%
                                                Else
                                                %>
                                                <td style="width: 363px; height: 1px; background-color: #ffffff;"><%=dtsdocumentosadjuntos.Tables("consulta").Rows(j - 1).Item("descripcion_ddr")%></td> 
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
                                %>
                            </table>
        <br />
        <br />
        <table>
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    ----------------------------------------------------------</td>
            </tr>
            <tr>
                <td colspan="3" style="font-family: 'Courier New'; text-align: center"><% =dtsdetalleegreso.Tables("consulta").Rows(0).Item("nombres")%>
                </td>
            </tr>
        </table>
       
    </form>
</body>
</html>
