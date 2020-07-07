<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaProgramarReunion_POA.aspx.vb" Inherits="administrativo_propuestas2_proponente_ListaProgramarReunion_POA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../estilo.css" rel="stylesheet" type="text/css" />
    <title></title>
        <style type="text/css">
         .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px;
                height: 25px;
            }
            </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Lista de Reuniones</h2>
        </div>

        <div>
            <table style="width: 100%; margin-bottom: 229px;" align="left">
                <tr>
                    <td class="contornotabla" valign="top" width="100%" colspan="6">
                        <table  width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                               <td  bgcolor="#F0F0F0" class="bordeinf" width="20%">
                                    <asp:Panel ID="PanelConsulta" runat="server">
                                        <table style="width:100%;">
                                           
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdConsultar" runat="server" CssClass="nuevo1" Height="47px" Text="        Nuevo" Width="120px" />                          
                                                    <asp:HiddenField ID="HD_Usuario" runat="server" />
                                                    &nbsp;<asp:HiddenField ID="txtelegido" runat="server" />
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                   <asp:GridView ID="dgvResolucion" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15" CellPadding="2" 
                                                        ForeColor="#333333" Font-Size="Smaller"  DataKeyNames="id_Rec">
                                                        <RowStyle Height="20px" />
                                                        <Columns>
                                                            
                                                            <asp:BoundField DataField="fecha_rec" HeaderText="Fecha" >
                                                            <ItemStyle Width="8%" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="agenda_Rec" HeaderText="Agenda" >
                                                            <ItemStyle Width="40%" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="lugar_Rec" HeaderText="Lugar" >                               
                                                            <ItemStyle Width="40%" />
                                                            </asp:BoundField>
                                                        
                                                            <asp:CommandField HeaderText="Modificar" SelectText="Ver" ShowSelectButton="True" ButtonType="Image" SelectImageUrl="../../../images/previo.gif" >                                
                                                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            
                                                        </Columns>
                                                        <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                                                    </asp:GridView>                                                
                                                </td>
                                            </tr>
                                            
                                        </table>
                                    </asp:Panel>
                                </td>       
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
