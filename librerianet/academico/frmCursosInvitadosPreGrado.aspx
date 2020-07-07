<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCursosInvitadosPreGrado.aspx.vb" Inherits="academico_frmCursosInvitadosPreGrado" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../private/estilo.css" rel="stylesheet" type="text/css" />    
</head>
<body>
    <form id="form1" runat="server">    
    <div>    
        <asp:Button ID="btnActualiza" runat="server" Text="Actualizar Ex. Recuperación" />
        <br />
        <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small" 
            ForeColor="Red"></asp:Label><br />
        <asp:HiddenField ID="HdPC" runat="server" />
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #0C0C0B; width:100%">
            <tr>
                <td bgcolor="#DFDBA4" colspan="4" height="30px">
                <b>
                    <asp:Label ID="Label4" runat="server" Text="SOLICITUDES DE EXAMENES DE RECUPERACION"></asp:Label></b>
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
                    DataKeyNames="codigo_Cup,codigo_Dma" 
                    EmptyDataText="No se encontraron registros.">
                        <RowStyle BorderColor="#C2CFF1" BorderStyle="Solid" BorderWidth="1px" />
                        <EmptyDataRowStyle BorderStyle="None" BorderWidth="0px" Font-Bold="True" ForeColor="Red" />
                        <Columns>
                            <asp:BoundField HeaderText="codigo_Cup" DataField="codigo_Cup" Visible="False">
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_Dma" HeaderText="codigo_Dma" Visible="False">
                            </asp:BoundField>
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="GH" >
                            <ItemStyle Width="80px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombre_Cur" HeaderText="Descripción del Curso">
                            <ItemStyle Width="350px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo">
                            <ItemStyle Width="10px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="creditos_Cur" HeaderText="Crd">
                            <ItemStyle Width="10px" />
                            </asp:BoundField>                                                                                                               
                            <asp:TemplateField HeaderText="Confirmar">
                                  <ItemTemplate>
                                    <table style="width:100%;">
                                        <tr>                                            
                                            <td align="center">
                                                <asp:ImageButton    
                                                                    ID="ibtnConfirma" 
                                                                    runat="server" 
                                                                    ToolTip="Confirmar Inscripcion" 
                                                                    ImageUrl="../../images/Confirm.png"                                                                     
                                                                    onclick="ibtnConfirma_Click" 
                                                                    OnClientClick="javascript:return confirm('¿Desea confirmar su inscripción?');"
                                                                    />                                                                    
               
                                             </td>
                                             <td align="center">
                                                <asp:ImageButton 
                                                                    ID="ibtnRechaza" 
                                                                    runat="server" 
                                                                    ToolTip="Rechazar inscripción" 
                                                                    ImageUrl="../../images/noCondirm.png" 
                                                                    onclick="ibtnRechaza_Click"                                                                     
                                                                    OnClientClick="javascript:return confirm('¿Desea rechazar su inscripción?');"
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
        <br />(*) Estarás apto (matriculado) para rendir el examen de recuperación del 
        curso una vez realizado el pago correspondiente.<br />        
        <table cellpadding="3" cellspacing="0" style="border: 1px solid #0C0C0B; width:100%">
            <tr>
                <td bgcolor="#DFDBA4" colspan="4" height="30px">
                <b>
                    <asp:Label ID="Label1" runat="server" Text="LISTA DE EXAMENES DE RECUPERACION"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvDetalle" runat="server" Width="100%" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.">
                        <Columns>                            
                            <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" >                                
                            <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="GRUPO" >
                            <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="ESTADO" />
                        </Columns>
                        <HeaderStyle BackColor="#DFDBA4" BorderColor="#99BAE2" BorderStyle="Solid" 
                                BorderWidth="1px" ForeColor="#030300" />
                    </asp:GridView>
                </td>
            </tr>
         </table>
         <br />
         <table cellpadding="3" cellspacing="0" style="border: 1px solid #0C0C0B; width:100%">
            <tr>
                <td bgcolor="#DFDBA4" colspan="4" height="30px">
                <b>
                    <asp:Label ID="Label2" runat="server" 
                        Text="EXAMENES DE RECUPERACION BLOQUEADOS POR LÍMITE DE INASISTENCIAS"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:GridView ID="gvNoDisponible" runat="server" Width="100%" 
                        AutoGenerateColumns="False" EmptyDataText="No se encontraron registros.">
                        <Columns>
                            <asp:BoundField DataField="codigo_Cup" HeaderText="CODIGO" Visible="False" />
                            <asp:BoundField DataField="nombre_Cur" HeaderText="CURSO" >                                
                            <ItemStyle Width="50%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="grupoHor_Cup" HeaderText="GRUPO" >
                            <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="estado" HeaderText="ESTADO" Visible="False" />
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
