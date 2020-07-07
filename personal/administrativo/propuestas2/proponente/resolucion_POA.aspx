<%@ Page Language="VB" AutoEventWireup="false" CodeFile="resolucion_POA.aspx.vb" Inherits="administrativo_propuestas2_proponente_resolucion_POA" %>

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
            <h2>Número de Resolución</h2>
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
                                                    <asp:HiddenField ID="txtelegido" runat="server" />
                                                </td>
                                            </tr>
                                            <%--<tr>
                                                <td>Instancia:</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlInstanciaPropuesta" runat="server" AutoPostBack="True">
                                                        <asp:ListItem Value="F">Consejo de Facultad</asp:ListItem>
                                                        <asp:ListItem Value="K">Rectorado</asp:ListItem>
                                                        <asp:ListItem Value="A">Consejo Administrativo</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="cmdConsultar" runat="server" CssClass="nuevo1" Height="47px" Text="        Consultar" Width="120px" />                          
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td>
                                                   <asp:GridView ID="dgvResolucion" runat="server" Width="100%" AutoGenerateColumns="False" PageSize="15" CellPadding="2" 
                                                        ForeColor="#333333" Font-Size="Smaller"  DataKeyNames="codigo_prp">
                                                        <RowStyle Height="20px" />
                                                        <Columns>
                                                            
                                                            <asp:BoundField DataField="nombre_prp" HeaderText="Propuesta" >
                                                            <ItemStyle Width="60%" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="fechainicio_Ipr" HeaderText="F. Inicio" >
                                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                            </asp:BoundField>
                                                            
                                                            <asp:BoundField DataField="resolucion" HeaderText="N° de Resolución" >                               
                                                            <ItemStyle Width="20%" />
                                                            </asp:BoundField>
                                                        
                                                            <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectText="Nuevo" SelectImageUrl="../images/nuevo.gif" HeaderText="Nuevo">
                                                                <ItemStyle HorizontalAlign="Center" />
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
                
                <tr>
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
                                   
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
        </div>
    </form>
</body>
</html>
