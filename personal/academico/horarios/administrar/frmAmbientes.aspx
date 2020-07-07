<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAmbientes.aspx.vb" Inherits="academico_horarios_administrar_frmAmbientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 70%;
        }
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
       .celdaCa{ padding:10px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">   
    <center>
    <table class="style1" cellpadding="0" cellspacing="0" style="text-align:justify">
        <tr>
            <td colspan="4" class="titulo">
                Gestión de Ambientes</td>
        </tr>
        <tr class="celda1">
            <td>
                Tipo de Ambiente</td>
            <td>
                <asp:DropDownList ID="ddlTipoAmbiente" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Estado</td>
            <td>
                <asp:CheckBox ID="chkActivo" runat="server" Checked="True" Text="Activo" />
            </td>
        </tr>
        <tr class="celda1">
            <td>
                Ubicación</td>
            <td>
                <asp:DropDownList ID="ddlUbicacion" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                                Capacidad &gt;=</td>
            <td>
                <asp:TextBox ID="txtCapacidad" runat="server" Width="50px">0</asp:TextBox>
            </td>
        </tr>
        <tr class="celda1">
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                    Text="Buscar solo por nombre real" />
            </td>
            <td>
                <asp:TextBox ID="txtNombre" runat="server" Enabled="False"></asp:TextBox>
            </td>
            <td>
                                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr class="celda1">
            <td>
                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="btn" PostBackUrl="frmAmbienteRegistrar.aspx"/>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="Buscar" CssClass="btn" />
            </td>
            <td>
                <asp:Label ID="lblContador" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
        </center>
   <table>
   <tr>
            <td colspan="4" class="celda1">
                <asp:GridView ID="gridLista" runat="server" CellPadding="4" ForeColor="#333333" 
                    DataKeyNames="Editar" HorizontalAlign="Center" ShowFooter="True">
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Center" />
                    <Columns>
                    <asp:CommandField ShowDeleteButton="True" ButtonType="Image" 
                            DeleteImageUrl="~/academico/horarios/administrar/images/delete.png" 
                            DeleteText="Inactivar" HeaderText="X"    />
                    </Columns>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#999999" />
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                </asp:GridView>
            </td>
        </tr>
   </table>
    </form>
</body>
</html>
