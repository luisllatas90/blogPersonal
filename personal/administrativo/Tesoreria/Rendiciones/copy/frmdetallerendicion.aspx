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
        codigo_rend2 = Me.Request.QueryString("codigo_rend")

     %>
    <script language="javascript">
    
        function RegistrarDetallerendicion()
        {      
        
        window.open('frmregistrardetallerendicion.aspx?codigo_rend=<%=codigo_rend2%>','frmadjuntararchivo','toolbar=no,width=1200,height=410')         
        }
        function FinalizarRendicion()
        {
            var respuesta ;// <%=codigo_rend2%>
            respuesta=confirm("Desea dar por finalizada la rendición de gastos, esta operación bloqueara la edición de la misma y debe acercarse a la oficina de Contabilidad con la documentación física de su rendición");
            if (respuesta )
                {
                    window.location.href="frmprocesar.aspx?codigo_rend=<%=codigo_rend2%>"  ;
                }
                
            
        }
        
    </script> 
    
</head>
<body bgcolor="white" style="background-color: whitesmoke" >
    <form id="form1" runat="server" >
    
        <table  height="100%"  border="0" cellspacing="0" cellpadding="3" style="border-collapse: collapse; height: 704px; border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid; width: 98%;" bordercolor="#000000">
               <tr bordercolor="#000000"> 
                    <td class="usatCeldaTitulo" style="height: 16pt; width: 100%;">                       
                        Detalle de documento</td>
                </tr>
            <tr bordercolor="#000000">
                <td class="usatTitulo" colspan="4" valign="top" width="100%" style="font-weight: bold; height: 110px;">
                    <table style="width: 100%; border-top-width: thin; border-left-width: thin; border-left-color: black; border-bottom-width: thin; border-bottom-color: black; border-top-color: black; border-right-width: thin; border-right-color: black;">
                        <tr>
                            <td style="font-size: 12px; width: 642px; font-family: 'Courier New'; height: 26px;
                                background-color: white; font-variant: normal;" >
                                <span style="font-size: 9pt; font-family: Courier New">Documento:</span></td>
                            <td style="font-size: 12px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;" colspan="3">
                                <asp:Label ID="lbldocumento" runat="server" Text="Label" Width="456px" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                            <td style="font-size: 12px; width: 529px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                Nº :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                <em>
                                    <asp:Label ID="lblnumero" runat="server" Text="Label" Width="160px" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></em></td>
                            <td style="font-size: 12px; width: 100px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                Fecha :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblfecha" runat="server" Text="Label" Width="104px" style="text-align: right" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                            <td style="font-size: 12px; width: 144px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                Usuario :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 26px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblusuario" runat="server" Text="Label" Width="104px" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 642px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                Rubro/ concepto:</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblrubro" runat="server" Text="Label" Width="264px" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 22px;
                                background-color: white; font-variant: normal">
                                Moneda :</td>
                            <td style="font-size: 12px; width: 174px; font-family: 'Courier New'; height: 22px;
                                background-color: white; font-variant: normal">
                                <asp:Label ID="lblmoneda" runat="server" Text="Label" Width="128px"></asp:Label></td>
                            <td style="font-size: 12px; width: 529px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                Importe entregado&nbsp; :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblimporteentregado" runat="server" Text="Label" Width="152px" style="text-align: right" BackColor="YellowGreen" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                            <td style="font-size: 12px; width: 100px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                Importe Rendido :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblimporterendido" runat="server" Text="Label" Width="104px" style="text-align: right" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                            <td style="font-size: 12px; width: 144px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                Importe devuelto :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 22px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblimportedevuelto" runat="server" Text="Label" Width="104px" style="text-align: right" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; width: 642px; font-family: 'Courier New'; height: 21px; background-color: white; font-variant: normal;">
                                Saldo &nbsp; :</td>
                            <td style="font-size: 12px; width: 96px; font-family: 'Courier New'; height: 21px; background-color: white; font-variant: normal;">
                                <asp:Label ID="lblsaldo" runat="server" Text="Label" Width="256px" Font-Bold="False" Font-Italic="False" Font-Names="Courier New" Font-Size="10pt" ForeColor="Black" BackColor="YellowGreen"></asp:Label></td>
                            <td style="font-size: 12px; width: 73px; font-family: 'Courier New'; height: 21px;
                                background-color: white; font-variant: normal">
                            </td>
                            <td style="font-size: 12px; width: 174px; font-family: 'Courier New'; height: 21px;
                                background-color: white; font-variant: normal">
                            </td>
                            <td style="font-size: 12px; width: 529px; font-family: 'Courier New'; height: 21px; background-color: white; font-variant: normal;">
                                Observación :</td>
                            <td style="font-size: 12px; font-family: 'Courier New'; height: 21px; background-color: white; font-variant: normal;" colspan="5">
                                <asp:Label ID="lblobservacion" runat="server" Font-Bold="False" Font-Names="Courier New"
                                    Height="24px" Text="Label" Width="576px"></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="10" style="font-size: 12px; font-family: 'Courier New'; height: 21px;
                                background-color: white; font-variant: normal">
                                <asp:Label ID="lblindicaciones" runat="server" Font-Bold="False" Font-Names="Courier New"
                                    Font-Size="9pt" Height="24px" Style="text-align: right" Text="Contraste la información con el documento físico que le fue entregado"
                                    Width="828px"></asp:Label></td>
                        </tr>
                    </table>
                
                
                
                </td>
            </tr>
            <tr height="2%">
                <td valign="top" style="height: 1%; width: 1232px;"  class="usatTitulo">
                <% If mostrarfinalizar = True Then%>
                    &nbsp;<input id="cmdagregar" style="width: 112px; background-repeat: no-repeat; background-color: lemonchiffon"
                        type="button" value="Agregar"    language="javascript" onclick="RegistrarDetallerendicion()" BorderStyle="Groove"/>                        
                    <input id="Button1" style="width: 144px; background-repeat: no-repeat; background-color: lemonchiffon"                        
                        type="button" value="Finalizar operación"    language="javascript" onclick="FinalizarRendicion()" BorderStyle="Groove"/>
                        <% end if %>
                    <asp:Button ID="cmdcancelar" runat="server" BackColor="LemonChiffon" Font-Bold="False"
                        Font-Names="Courier New" Font-Size="8pt" Height="24px" Style="background-image: url(iconos/salir.gif);
                        background-repeat: no-repeat; background-position: left center;" Text=" Cerrar" Width="96px" BorderStyle="Groove" /></td>
            </tr >
            <tr height ="5%" bordercolor="#000000">
                <td style="border-collapse: collapse; height: 30%; width: 98%;" bordercolor="#000000" id="tbllista" valign =top>
                        <div id="xxx"  style="overflow-y : scroll;   height:100%;" bordercolor=#000000>
                                    <asp:GridView ID="lstinformacion" runat="server" AutoGenerateColumns="False" Width="98%" Height="20%" Font-Names="Arial Narrow" Font-Size="8pt" style="background-image: url(eliminar.gif); background-repeat: no-repeat" BackColor="White" UseAccessibleHeader="False" BorderColor="Black">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_dren">
                                            <HeaderStyle CssClass="usatCeldaTitulo" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Tipo Documento" DataField="descripcion_tdo">
                                                <HeaderStyle Width="20%" CssClass="usatCeldaTitulo"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Serie / N&#250;mero" DataField="serienumero_dren">
                                                <HeaderStyle Width="10%" CssClass="usatCeldaTitulo"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_dren" HeaderText="Fecha">                                
                                                <HeaderStyle CssClass="usatCeldaTitulo" Width="20%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Instituci&#243;n / Empresa" DataField="institucion_dren">
                                                <HeaderStyle Width="20%" CssClass="usatCeldaTitulo"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="importe_dren" HeaderText="Importe">                                
                                                <HeaderStyle Width="10%" CssClass="usatCeldaTitulo"/>
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcion_dren" HeaderText="Descripci&#243;n">
                                                <HeaderStyle CssClass="usatCeldaTitulo" Width ="25%" />
                                                <ItemStyle Font-Names="Courier New" Font-Size="10pt" HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowDeleteButton="True" DeleteImageUrl="~/eliminar.gif" ButtonType="Image" DeleteText=""  />
                                            
                                        </Columns>
                                        <RowStyle BorderStyle="Solid" />
                                        <EmptyDataRowStyle CssClass="usatCeldaTitulo" />
                                    </asp:GridView>                           
                                    </div>                                     
                    </td>
                  </tr>
                 <tr valign=top bordercolor="#000000">
                    <td style="height :43px; width: 1232px;" bordercolor="#000000">
                                    <asp:HiddenField ID="hd" runat="server" />
                        <asp:HiddenField ID="hdpagina" runat="server" />
                        &nbsp;&nbsp;
                        <asp:HiddenField ID="hdestado" runat="server" />
                        <asp:Label ID="lblmensaje" runat="server" ForeColor="Maroon" Width="100%"></asp:Label>
                    </td>
                  </tr>                    

            <tr height="auto" bordercolor="#000000">
                <td colspan="2" style="height: auto" width="98%"   class ="usatCeldaTitulo">
                    
                    Datos adjuntos 
                </td>
                              
                
            </tr>
            <tr height="10%">
                <!-- <td class="pestanaresaltada" colspan="3" style="height: 10%; background : white; width: 110%;" bgcolor="white" align =left valign="top"> -->
                <td width="100%" style="height: 10%" >
                <div id="listadiv" style="height:365%; left:0px; top: 0px; width :100%; background : white ;   overflow-y :  scroll    ; border : none">               

                <iframe id="documentodetalle"    src="frmadjuntararchivo.aspx" style="width: 98%; background-color: lemonchiffon; height:  90%; vertical-align: top; top=0; border=none; left:0px" frameborder="no"></iframe>
                </div>
                    </td>
            </tr>
            
            </table>
       
       
    </form>
</body>
</html>
