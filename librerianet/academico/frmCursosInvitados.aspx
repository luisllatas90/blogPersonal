<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCursosInvitados.aspx.vb" Inherits="academico_frmCursosInvitados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../../private/funciones.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #0C0C0B; width:100%">
            <tr>
                <td bgcolor="#DFDBA4" colspan="4" height="30px">
                <b>
                    <asp:Label ID="Label4" runat="server" Text=":. LISTA DE CURSOS INVITADOS :."></asp:Label></b>
                </td>
          </tr>
          <tr>
            <td>
            <asp:GridView   ID="gvLista" 
                                    runat="server" 
                                    AutoGenerateColumns="False" 
                                    BorderColor="Silver" 
                                    BorderStyle="Solid" 
                                    CaptionAlign="Top" 
                                    CellPadding="2" 
                                    EnableModelValidation="True" 
                                    Width="100%" 
                                    
                    DataKeyNames="codigo_Cup,codigo_Dma,nombre_Cur,fechainicio_Cup,fechafin_Cup" 
                    EmptyDataText="No se encontraron registros.">
                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" ForeColor="Red" />
                        <Columns>
                            <asp:BoundField HeaderText="codigo_Cup" DataField="codigo_Cup" Visible="False">
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_Dma" HeaderText="codigo_Dma" Visible="False">
                            </asp:BoundField>
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="GH" >
                            <ItemStyle Width="8px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cur" HeaderText="Descripción del Curso">
                            <ItemStyle Width="225px" />
                            </asp:BoundField>
<asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo">
                            <ItemStyle Width="10px" />
</asp:BoundField>
                            <asp:BoundField DataField="creditos_Cur" HeaderText="Crd">
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="docente" HeaderText="Docente">
                            <ItemStyle Font-Size="7pt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechainicio_Cup" HeaderText="Fecha Inicio">
                            <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fechafin_Cup" HeaderText="Fecha Fin" >
                            <ItemStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Costo" HeaderText="S/. Costo" >
                            <ItemStyle Width="15px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Confirmar">
                                  <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>
                                            <td align="center">
                                                    <asp:Label  ID="Label2" 
                                                            runat="server" 
                                                            ForeColor="Red" 
                                                            Visible='<%# iif(Eval("CntDeudasNivelacion") >= "2", "True", "False") %>'
                                                            Text="REGULARIZAR DEUDAS PENDIENTES"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:Label  ID="Label1" 
                                                            runat="server" 
                                                            ForeColor="Red" 
                                                            Visible='<%# iif(Eval("EstadoMuestra") = "N", "True", "False") %>'
                                                            Text="CURSO FINALIZADO"></asp:Label>
                                            </td>
                                            <td align="center">
                                                <asp:ImageButton    
                                                                    ID="ibtnConfirma" 
                                                                    runat="server" 
                                                                    ToolTip="Confirmar Matricula" 
                                                                    ImageUrl="../../images/Confirm.png" 
                                                                    Visible='<%# iif(Eval("EstadoMuestra") = "M", "True", "False") %>'
                                                                    onclick="ibtnConfirma_Click" 
                                                                    OnClientClick="javascript:return confirm('¿Desea confirmar la matricula?');"
                                                                    />
                                                                    
                                
               
                                             </td>
                                             <td align="center">
                                                <asp:ImageButton 
                                                                    ID="ibtnRechaza" 
                                                                    runat="server" 
                                                                    ToolTip="Rechazar 
                                                                    Matricula" 
                                                                    ImageUrl="../../images/noCondirm.png" 
                                                                    onclick="ibtnRechaza_Click" 
                                                                    Visible='<%# iif(Eval("EstadoMuestra") = "M", "True", "False") %>'
                                                                    OnClientClick="javascript:return confirm('¿Desea eliminar la pre-matricula?');"
                                                                    />
                                             </td>
                                         </tr>
                                     </table>
                                   </ItemTemplate>
                                   <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                             </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#DFDBA4" BorderColor="#99BAE2" BorderStyle="Solid" 
                            BorderWidth="1px" ForeColor="#030300" />
                    </asp:GridView>
            </td>
          </tr>
        </table>    
    </div>
    </form>
</body>
</html>
