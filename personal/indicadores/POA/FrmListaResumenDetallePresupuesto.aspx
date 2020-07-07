<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaResumenDetallePresupuesto.aspx.vb" Inherits="indicadores_POA_FrmListaEvaluarPresupuesto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  
    <title></title>
     <link href="css/estilo_poa.css" rel="stylesheet" type="text/css" media="screen" />
    <script src="Jquery/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {

        });
    </script>
     <style type="text/css">
         
        .nombre_prog
        {
            color:#aa6708;
            font-weight:bold;
            padding-top:3px;
            padding-bottom:3px;
            padding-left:4px;
            font-size:16px;
        }
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="titulo_poa">
        <asp:Label ID="Label1" runat="server"
            Text="Detalle de Presupuesto Registrado de Programa/Proyecto"></asp:Label>
    </div>
    <div class="contorno_poa">
    <table width="100%">
    <tr>
    <td>
    <asp:Label ID="nombre_progproy" runat="server" CssClass="nombre_prog" Text=""></asp:Label>
    </td>
    </tr>
    <tr>
    <td>
        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="False" 
                        DataKeyNames="codigo_dap,codigo_acp,responsable_dap" ShowFooter="True" 
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="descripcion_dap" HeaderText="DESCRIPCION" 
                                HtmlEncode="false">
                                <ItemStyle CssClass="celda_combinada" Width="30%" Height="18px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecini_dap" HeaderText="F. INICIO">
                                <ItemStyle CssClass="celda_combinada" HorizontalAlign="Center" Width="90px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="fecfin_dap" HeaderText="F. FIN">
                                <ItemStyle CssClass="celda_combinada" HorizontalAlign="Center" Width="90px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="nombreresponsable_dap" HeaderText="RESPONSABLE">
                                <ItemStyle CssClass="celda_combinada"  Width="30%" Height="18px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="requiere_pto" HeaderText="PRESUPUESTO REQUERIDO">
                                <ItemStyle CssClass="celda_combinada" HorizontalAlign="Center"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="ingresos" DataFormatString="{0:C2}" 
                                HeaderText="INGRESOS (S/.)">
                                <ItemStyle HorizontalAlign="right" Width="200px"  />
                            </asp:BoundField>
                            <asp:BoundField DataField="egresos" DataFormatString="{0:C2}" 
                                HeaderText="EGRESOS (S/.)">
                                <ItemStyle HorizontalAlign="right" Width="200px" />
                            </asp:BoundField>
                        </Columns>
                        <HeaderStyle BackColor="#3871b0" ForeColor="White"  />
        </asp:GridView>
    </td>
    </tr>
    <tr>
    <td align="right">
    <asp:Button runat="server" ID="btnRegresar" CssClass="btnRegresar" Text="  Regresar" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>
