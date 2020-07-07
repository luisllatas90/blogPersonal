<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Informes.aspx.vb" Inherits="SisSolicitudes_Informes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    </head>
<body style="margin-top:0px">
    <form id="form1" runat="server">
                       <table  align="center" width="100%">
                            <tr>
                                <td>
                                    
                        <table style="width:100%;" align="center">
                            <tr>
                                <td align="center">
                                    <b style="text-align: center">SECCIÓN DE INFORMES</b></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <b>
                                    <div id="DivAnulada" align="center" 
                                        style="position: absolute; width: 100%; height: 522px; top: 32px; left: 2px;">
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <asp:Image ID="Image1" runat="server" ImageAlign="Middle" 
                                            ImageUrl="~/images/Anulada.gif" />
                                    </div>
                                    Número de solicitud:</b>
                                    <asp:Label ID="LblNroSolicitud" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" id="tblDE">
                                        <tr>
                                            <td colspan="2">
                                                <b>
                                                <asp:Label ID="lblNivel1" runat="server" Text="Director de Escuela:"></asp:Label>
&nbsp;<asp:Label ID="LblDE" runat="server"></asp:Label>
                                                </b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Resultado:
                                                <asp:Label ID="LblResultadoDE" runat="server"></asp:Label>
                                            </td>
                                            <td width="200">
                                                Fecha :
                                    <asp:Label ID="LblFechaDE" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Observacion:</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtObservacionDE" runat="server" ReadOnly="True" Rows="3"
                                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" id="tblDA">
                                        <tr>
                                            <td colspan="2">
                                                <b>
                                                <asp:Label ID="lblNivel2" runat="server" Text="Director Académico:"></asp:Label>
&nbsp;<asp:Label ID="LblDA" runat="server"></asp:Label>
                                                </b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Resultado:
                                                <asp:Label ID="LblResultadoDA" runat="server"></asp:Label>
                                            </td>
                                            <td width="200">
                                                Fecha :
                                    <asp:Label ID="LblFechaDA" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Observacion:</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtObservacionDA" runat="server" ReadOnly="True" Rows="3" 
                                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="tblAG" width="100%" style="visibility:hidden">
                                        <tr>
                                            <td colspan="2">
                                                <b>Administrador General:
                                                <asp:Label ID="LblAG" runat="server"></asp:Label>
                                                </b></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Resultado:
                                                <asp:Label ID="LblResultadoAG" runat="server"></asp:Label>
                                            </td>
                                            <td width="200px">
                                                Fecha :
                                    <asp:Label ID="LblFechaAG" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                Observacion:</td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:TextBox ID="TxtObservacionAG" runat="server" ReadOnly="True" Rows="3" 
                                                    TextMode="MultiLine" Width="100%"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                                    
                                </td>
                            </tr>
                            </table>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width:100%;" align="center">
                            <tr>
                                <td>
                                    <b>Estado de Cuenta:</b></td>
                            </tr>
                            <tr>
                                <td>
                                    <table style="width:100%;">
                                       <tr > 
                                          <th>
                                              <asp:GridView ID="GvEstadoCue" runat="server" AutoGenerateColumns="False" 
                                                  DataKeyNames="codigo_Deu" Width="100%">
                                                  <Columns>
                                                      <asp:BoundField DataField="fechaVencimiento_Sco" HeaderText="Fecha de Venc." 
                                                          SortExpression="fechaVencimiento_Sco" DataFormatString="{0:dd-MM-yyyy}" >
                                                          <ItemStyle Width="120px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="SERVICIO" HeaderText="Servicio" ReadOnly="True" 
                                                          SortExpression="SERVICIO" >
                                                          <ItemStyle Width="250px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="estado_Deu" HeaderText="Estado" 
                                                          SortExpression="estado_Deu" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="CARGO" HeaderText="Cargo" ReadOnly="True" 
                                                          SortExpression="CARGO" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="PAGOS" HeaderText="Pago" SortExpression="PAGOS" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="SALDO" HeaderText="Saldo" SortExpression="SALDO" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="Mora_deu" HeaderText="Mora" ReadOnly="True" 
                                                          SortExpression="Mora_deu" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                      <asp:BoundField DataField="DOCUMENTO" HeaderText="DOCUMENTO" 
                                                          SortExpression="DOCUMENTO" Visible="False" />
                                                      <asp:BoundField DataField="FECHA" HeaderText="FECHA" SortExpression="FECHA" 
                                                          Visible="False" />
                                                      <asp:BoundField DataField="CODIGO_RESP" HeaderText="CODIGO_RESP" 
                                                          ReadOnly="True" SortExpression="CODIGO_RESP" Visible="False" />
                                                      <asp:BoundField DataField="RESPONSABLE" HeaderText="RESPONSABLE" 
                                                          ReadOnly="True" SortExpression="RESPONSABLE" Visible="False" />
                                                      <asp:BoundField DataField="OBSERVACIÓN" HeaderText="OBSERVACIÓN" 
                                                          SortExpression="OBSERVACIÓN" Visible="False" />
                                                      <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" 
                                                          Visible="False" />
                                                      <asp:CheckBoxField DataField="generaMora_Sco" HeaderText="generaMora_Sco" 
                                                          SortExpression="generaMora_Sco" Visible="False" />
                                                      <asp:BoundField DataField="codigo_Deu" HeaderText="codigo_Deu" 
                                                          InsertVisible="False" ReadOnly="True" SortExpression="codigo_Deu" 
                                                          Visible="False" />
                                                      <asp:BoundField DataField="codigo_Sco" HeaderText="codigo_Sco" 
                                                          SortExpression="codigo_Sco" Visible="False" />
                                                      <asp:BoundField DataField="Est" HeaderText="Est" SortExpression="Est" 
                                                          Visible="False" />
                                                      <asp:BoundField HeaderText="Sub total" >
                                                          <ItemStyle Width="80px" />
                                                      </asp:BoundField>
                                                  </Columns>
                                                  <EmptyDataTemplate>
                                                      <asp:Label ID="Label1" runat="server" ForeColor="Red" 
                                                          Text="El estudiante no tiene deuda"></asp:Label>
                                                  </EmptyDataTemplate>
                                                  <HeaderStyle CssClass="usatCeldaHeader" />
                                              </asp:GridView>
                                           </th>
                                        </tr>
                                       <tr > 
                                          <th align="right">Total&nbsp;&nbsp;&nbsp;
                                              <asp:Label ID="LblTotal" runat="server" Width="80px" style="text-align:center"></asp:Label>
                                                            </th>
                                        </tr>
                                       <tr > 
                                          <th align="right" height="30">
                                        <input runat="server" id="CmdImprimir" type="button" value="imprimir"  
                                            onclick="javascript:print();" class="boton" /></th>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                                    </td>
                                </tr>
                                </table>
    </form>
</body>
</html>
