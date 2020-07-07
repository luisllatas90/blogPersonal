<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPrestamoPersonal.aspx.vb" Inherits="administrativo_FrmPrestamoPersonal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="js/jquery-1.4.4.min.js" type="text/javascript"></script>   
    <script src="js/jquery.numeric.js" type="text/javascript"></script> 
    <script type="text/javascript">
        $(document).ready(function() {
            /* Para esto debemos usar el jquery.numeric */
            $('#txtCuotas').numeric();
            $('#txtImporte').numeric(",");             
        });
    </script>
    <style type="text/css">
        .col1
        {
            width:8%;
        }
        .col2
        {
            width:5%;
        }
        body
        {
            font-size:smaller;
            font-family:Arial;
        }      
        .btnGenerar
        {
            border:1px;
            border-style:solid;
            width:100px;
            height:22px;
            background-image: url('img/paper16');
        }     
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="Label12" runat="server" Text="Registro de prestamos personales y adelantos" 
        Font-Bold="True" Font-Size="Medium" ></asp:Label>
    <br /><br />
    <table width="100%">
        <tr>
            <td class="col1">        
        <asp:Label ID="Label4" runat="server" Text="Fecha:"></asp:Label>
            </td>
            <td>
        <asp:TextBox ID="txtFecha" runat="server" Enabled="False" Width="80px"></asp:TextBox>
            </td>
            <td class="col1">
        <asp:Label ID="Label2" runat="server" Text="Cliente:"></asp:Label>
            </td>
            <td colspan="3">
        <asp:TextBox ID="txtTrabajador" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="col1">
        <asp:Label ID="Label3" runat="server" Text="Moneda"></asp:Label>
            </td>
            <td>
        <asp:TextBox ID="txtMoneda" runat="server" Enabled="False" Width="80px">SOLES</asp:TextBox>
            </td>
            <td class="col1">
        <asp:Label ID="Label1" runat="server" Text="Rubro:"></asp:Label>
            </td>
            <td>
        <asp:DropDownList ID="cboRubro" runat="server" Width="100%">
        </asp:DropDownList>
            </td>
            <td class="col1" align="right">
        <asp:Label ID="Label5" runat="server" Text="Total:" Font-Bold="True" ForeColor="Blue" Font-Size="Medium"></asp:Label>
            </td>
            <td>
        <asp:Label ID="txtImporteTotal" runat="server" Width="99%" ForeColor="Blue" 
                    Font-Bold="True" Font-Size="Medium"></asp:Label>
            </td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td colspan="10">&nbsp;</td>            
        </tr>
         <tr>
            <td colspan="10"><b>Detalle</b></td>            
        </tr>
         <tr>
            <td class="col1">
        <asp:Label ID="Label9" runat="server" Text="Importe:"></asp:Label>
             </td>
            <td>
        <asp:TextBox ID="txtImporte" runat="server" Width="80px"></asp:TextBox>
             </td>
            <td class="col1">
                <asp:Label ID="Label6" runat="server" Text="Planilla:"></asp:Label>        
             </td>
            <td>        
        <asp:DropDownList ID="cboTipoPlanilla" runat="server">
        </asp:DropDownList>        
             </td>
            <td class="col2">        
        <asp:Label ID="Label7" runat="server" Text="Año:"></asp:Label>
             </td>
            <td>
        <asp:DropDownList ID="cboAnio" runat="server">
        </asp:DropDownList>
             </td>
             <td class="col2">
        
        <asp:Label ID="Label8" runat="server" Text="Mes:"></asp:Label>
             </td>
             <td>
        <asp:DropDownList ID="cboMes" runat="server">
        </asp:DropDownList>
             </td>
             <td class="col2">
        <asp:Label ID="Label10" runat="server" Text="Cuotas:"></asp:Label>
             </td>
             <td>
        <asp:TextBox ID="txtCuotas" runat="server" Width="40px">1</asp:TextBox>
             </td>
        </tr>
         <tr>            
            <td colspan="10" align="right">        
                <asp:Button ID="btnAgregar" runat="server" Text="Generar" CssClass="btnGenerar"/>                                
             </td>            
        </tr>
    </table>
    <div>        
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
        <asp:Label ID="Label11" runat="server" Text="Numero:" Visible="False"></asp:Label>
        <asp:TextBox ID="txtSerie" runat="server" Visible="False">0</asp:TextBox>
        <asp:TextBox ID="txtNumeracion" runat="server" Visible="False">0</asp:TextBox>
        <br />
        
    </div>
    <asp:GridView ID="gvCuotas" runat="server" Width="100%"    
        AutoGenerateColumns="False">
    <Columns>          
          <asp:BoundField DataField="importe" HeaderText="Importe" >
          <ItemStyle HorizontalAlign="Right" />
          </asp:BoundField>
          <asp:BoundField DataField="moneda" HeaderText="Moneda" />
          <asp:BoundField DataField="idtipoplanilla" HeaderText="idtipoplanilla" Visible="false" />
          <asp:BoundField DataField="tipoplanilla" HeaderText="Tipo Planilla" />
          <asp:BoundField DataField="anio" HeaderText="Año" >
          <ItemStyle HorizontalAlign="Center" />
          </asp:BoundField>
          <asp:BoundField DataField="mes" HeaderText="Mes" />
          <asp:CommandField ShowDeleteButton="True" />
        </Columns>
        <RowStyle Height="22px" />
        <HeaderStyle BackColor="#e33439" Height="22px" ForeColor="White" />
        
        <EmptyDataTemplate>
            <table width="100%" border="0px">
                <tr style="height:22px">
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">Importe</td>
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">Moneda</td>
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">Tipo Planilla</td>
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">Año</td>
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">Mes</td>
                    <td align="center" style="background:#e33439; color:White; font-weight:bold">&nbsp;</td>
                </tr>
            </table>
        </EmptyDataTemplate>
    </asp:GridView>
    <br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btnGenerar"/>
    <asp:HiddenField ID="HdPersonal" runat="server" />
    &nbsp;<asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btnGenerar"/>
&nbsp;<asp:HiddenField ID="HdAnio" runat="server" />
    <asp:HiddenField ID="HdMes" runat="server" />
    </form>
</body>
</html>
