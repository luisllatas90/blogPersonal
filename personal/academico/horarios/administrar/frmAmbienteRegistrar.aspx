<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAmbienteRegistrar.aspx.vb" Inherits="academico_horarios_administrar_frmAmbienteRegistrar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
            body
        { font-family:Trebuchet MS;
          font-size:11px;
          cursor:hand;
          background-color:#F0F0F0;	
        }
        
        .celda1
        {           
            background:white;
            padding:5px;
            border:1px solid #808080;           
            color:#2F4F4F;
            font-weight:bold;            
        }
       .titulo
       { 
           font-weight:bold; font-size: 13px; padding-bottom:10px;
       }
       .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:5px; 
       }
       .sinTop { border-top:0px; }
       .sinleft{ border-left:0px; }
       .sinRight{ border-right:0px; }
       .sinBottom{ border-bottom:0px; }
       
       .divContent
       {
           float:left; border:1px solid #808080;
       }
       .divItem
       {
           float:left;  margin:5px;
           padding:5px;
       }
       
    </style>
<script language="javascript" type="text/javascript">
    function regresar() {
        history.back(1);
    }
    
</script>
</head>
<body>
    <form id="form1" runat="server">
    <h3>Registro de Ambientes</h3>
    <table width="75%" cellpadding="0" cellspacing="0" class="celda1">
        <tr>
            <td>
                Nombre Real</td>
            <td>
                <asp:TextBox ID="txtNombreReal" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ControlToValidate="txtNombreReal" Display="Dynamic" ErrorMessage="*" 
                    SetFocusOnError="True" ToolTip="Falta Dato"></asp:RequiredFieldValidator>
            </td>
            <td>
                Nombre Ficticio</td>
            <td>
                <asp:TextBox ID="txtNombreFicticio" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                    ControlToValidate="txtNombreFicticio" Display="Dynamic" ErrorMessage="*" 
                    SetFocusOnError="True" ToolTip="Falta Dato"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Abreviatura Real</td>
            <td>
                <asp:TextBox ID="txtAbrevReal" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ControlToValidate="txtAbrevReal" Display="Dynamic" ErrorMessage="*" 
                    SetFocusOnError="True" ToolTip="Falta Dato"></asp:RequiredFieldValidator>
            </td>
            <td>
                Abreviatura Ficticia</td>
            <td>
                <asp:TextBox ID="txtAbrevFicticia" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                    ControlToValidate="txtCapacidad" Display="Dynamic" ErrorMessage="*" 
                    SetFocusOnError="True" ToolTip="Falta Dato"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Tipo de Ambiente</td>
            <td>
                <asp:DropDownList ID="ddlTipoAmbiente" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Ubicación </td>
            <td>
                <asp:DropDownList ID="ddlUbicacion" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Capacidad Máxima</td>
            <td>
                <asp:TextBox ID="txtCapacidad" runat="server" Width="46px">0</asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                    ControlToValidate="txtNombreReal" Display="Dynamic" ErrorMessage="*" 
                    SetFocusOnError="True" ToolTip="Falta Dato"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ControlToValidate="txtCapacidad" ErrorMessage="!" SetFocusOnError="True" 
                    ToolTip="Debe ser un número" ValidationExpression="^\d+$">!</asp:RegularExpressionValidator>
            </td>
            <td>
                Estado</td>
            <td>
                <asp:CheckBox ID="chckActivo" runat="server" Text="Activo" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                Preferencial</td>
            <td>
                <asp:CheckBox ID="chkPreferencial" runat="server" Text="Sí" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                Disponible para Solicitar</td>
            <td>
                <asp:CheckBox ID="chkDisponible" runat="server" Text="Sí" /></td>
        </tr>
        <tr>
            <td>
                Características</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" style="float:left;">
            <div class="divContent" >
            <div class="divItem" >
                <asp:GridView ID="gridAudio" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none" DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirAudio" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Audio" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                <div class="divItem" >
                <asp:GridView ID="gridVideo" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none" DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirVideo" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Video" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                <div class="divItem" >
                <asp:GridView ID="gridSillas" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none" DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirSillas" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Sillas" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                <div class="divItem" >
                <asp:GridView ID="gridDistribucion" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none" DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirDistribucion" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Distribución" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                <div class="divItem" >
                <asp:GridView ID="gridVenti" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none"  DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirVenti" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Ventilación" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                <div class="divItem" >
                <asp:GridView ID="gridOtros" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="none"  DataKeyNames="codigo_camb">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:TemplateField>                            
                            <ItemTemplate>
                                <asp:CheckBox ID="chkElegirOtros" runat="server" />
                            </ItemTemplate>                            
                        </asp:TemplateField>
                        <asp:BoundField DataField="codigo_camb" HeaderText="codigo_camb" 
                            Visible="False" />
                        <asp:BoundField DataField="descripcion_cam" HeaderText="Otros" />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
                </div>
                </div>
            </td>
        </tr> 
        <tr>
           
            <td colspan="4" align="right">
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn" />
                
                &nbsp;<input id="btnCancelar" runat="server" class="btn" onclick="javascript:history.back(1);"
                    type="button" value="Cancelar" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;</td>
           
           
        </tr>  
        <tr>
           
            <td colspan="4">
                <asp:Label ID="lblMensaje" runat="server" ForeColor="#990000"></asp:Label>
                </td>
           
           
        </tr>            
    </table>
    <br />

 

    </form>
    
    </body>
</html>
