<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProgramarReunion_POA.aspx.vb" Inherits="administrativo_propuestas2_proponente_ProgramarReunion_POA" %>

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
            .style1
            {
                height: 23px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Registro Reunión</h2>
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
                                                <td width="8%">Fecha:</td>
                                                <td>
                                                    <asp:TextBox ID="txt_fecha" runat="server" Width="100px"></asp:TextBox>
                                                    <asp:Button ID="btnCalendario_ini" runat="server" Text="..." Width="20px" />
                                                </td>
                                                <td width="8%"></td>
                                                <td>
                                                    <asp:HiddenField ID="HD_Usuario" runat="server" />
                                                    <asp:HiddenField ID="HD_id_rec" runat="server" />
                                                    <asp:HiddenField ID="HD_Facultad" runat="server" />
                                                    <asp:HiddenField ID="HD_Tipo" runat="server" />
                                                </td>
                                                <td></td>
                                                <td width="8%">&nbsp;</td>
                                                <td colspan="2"></td>
                                            </tr>
                                             <tr>
                                                <td width="8%"></td>
                                                <%--<td ></td>
                                                <td width="8%">Hora:</td>
                                                <td></td>
                                                <td></td>
                                                <td width="8%">&nbsp;</td>--%>
                                                <%--<td colspan="5"></td>--%>
                                                
                                                <td colspan="5">
                                                    <div id="divCalendario_ini" runat="server">
                                                        <asp:Calendar ID="Calendario_ini" runat="server" EnableTheming="True" >
                                                        <SelectedDayStyle BackColor="#FF6666" />
                                                        <TitleStyle Font-Bold="True" Font-Names="Arial Narrow" 
                                                             />
                                                        </asp:Calendar>
                                                    </div>
                                                </td>
                        
                                            </tr>
                                            <tr>
                                                <td>Agenda:</td>
                                                <td colspan="7">
                                                    <asp:TextBox ID="txt_Agenda" runat="server" MaxLength="100" 
                                                        style="font-size: x-small" Width="57%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style1" >Lugar:</td>
                                                <td colspan="7" class="style1">
                                                    <asp:TextBox ID="txt_lugar" runat="server" MaxLength="100" 
                                                        style="font-size: x-small" Width="57%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr><td colspan="8"></td></tr>
                                            <tr>
                                                <td >Propuesta:</td>
                                                <td colspan="7">
                                                    <asp:DropDownList ID="ddl_Propuesta" runat="server" TabIndex="5" Width="52%">
                                                    </asp:DropDownList>
                                                    <asp:Button ID="btn_Agregar" runat="server" CssClass="agregar2" Text="   Agregar" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="8"></td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td colspan="5">
                                                   <asp:GridView ID="dgvPropuesta" runat="server" Width="70%" 
                                                        AutoGenerateColumns="False" PageSize="15" CellPadding="2" 
                                                        ForeColor="#333333" Font-Size="Smaller"  DataKeyNames="codigo_prp" >
                                                        <RowStyle Height="20px"/>
                                                        <Columns>
                                                            
                                                            <asp:BoundField DataField="nombre_prp" HeaderText="Propuesta" >
                                                            <ItemStyle Width="100%" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:CommandField ButtonType="Image" DeleteImageUrl="../../../Images/eliminar.gif" 
                                                                HeaderText="Eliminar" ShowDeleteButton="True">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            </asp:CommandField>
                                                            
                                                        </Columns>
                                                        <HeaderStyle BackColor="#666666" ForeColor="White" Height="22px" />
                                                    </asp:GridView>                                                
                                                </td>
                                                <td width="8%"></td>
                                                <td width="8%"></td>
                                            </tr>
                                            
                                            <tr>
                                                <td colspan="8" align="center">
                                                    <asp:Button ID="btnRegistrar" runat="server" Text="          Registrar"  CssClass="guardar_prp" Height="35px" Width="90px"  Enabled=true />
                                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="salir_prp" Height="35px" Width="112px" Enabled=true />                           
                                                </td>
                                               
                                            </tr>                                            
                                            
                                        </table>
                                    </asp:Panel>
                                </td>       
                            </tr>
                        </table>
                    </td>
                </tr>
                
                <%--<tr>
                    <td class="contornotabla" colspan="6">
                        <asp:Panel ID="PanelRegistro" runat="server" Height="100%">
                            <table style="width:100%;">
                                <tr>
                                    <td colspan="2" bgcolor="#F0F0F0" class="bordeinf" width="20%">
                                    <h2>Registro de Resoluciones</h2></td>
                                </tr>
                                <tr>
                                    <td>Propuesta: </td>
                                    <td>
                                        <asp:Label ID="lblPropuesta" runat="server" style="color: #0000FF; font-weight: 700" Text="[]"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td >N° de Resolución: </td>
                                    <td>
                                        <asp:TextBox ID="txtResolucion" runat="server" MaxLength="100" style="font-size: x-small" Width="450px"></asp:TextBox>
                                        <asp:TextBox ID="txtCodigo_prp" runat="server" MaxLength="10" style="font-size: x-small" Width="20px" visible=false></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <asp:Button ID="btnRegistrar" runat="server" Text="          Registrar"  CssClass="guardar_prp" Height="35px" Width="90px"  Enabled=true />
                                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="salir_prp" Height="35px" Width="112px" Enabled=true />                           
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>--%>
            </table>        
        
        </div>
    </form>
</body>
</html>
