<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConsultarIngresosEgresosPersonal.aspx.vb" Inherits="administrativo_frmConsultarIngresosEgresosPersonal" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <style type="text/css">
        .btnGenerar
        {
            border:1px;
            border-style:solid;
            width:100px;
            height:22px;
            background-image: url('img/paper16');
        } 
     body
        { font-family:Tahoma;
          font-size:11px;
          cursor:hand;
          background-color:white;	
        }
     .btn
       {
            border:1px solid #5D7B9D; 
            background:#F7F6F3 ; 
            font-family:Tahoma; 
            font-size:8pt; 
            font-weight:bold;  padding:3px; 
       }
       .success, .warning
       {
            border : 1px solid;
  margin: 10px 0px;
  padding:10px 5px 10px 30px;
  background-repeat: no-repeat;
  background-position: 10px center;
  max-width:380px;
  font-weight:bold;
           }
 .success {
  color: #4F8A10;
  background-color: #DFF2BF;
  background-image: url('../images/check.png');
}
.warning {
  color: #9F6000;
  background-color: #FEEFB3;
  background-image: url('../images/warning.png');
}
.tbl{border: 1px solid #C2CFF1;}
 .tbl th{ background-color:#e8eef7; color:#3366CC; font-weight:bold;"}
    
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="border: 1px solid #C2CFF1;">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
     <table  cellpadding="3" cellspacing="0" width="100%">
        <tr style="background-color: #E8EEF7; font-weight: bold; font-size:12px" height="30px">
            <td><b>Consultar Ingresos/Egresos en Planilla</b></td>            
        </tr>
     </table>
     <table  cellpadding="3" cellspacing="0">
      <tr style="font-weight: normal; height: 30px;">
            <td style="font-weight: bold; padding-top:10px;" valign="top">Referencia:&nbsp; <asp:Label ID="lblPlanilla" runat="server" Text="" style="color: #993300"></asp:Label>
                <br />
                <br />
                                Personal USAT<br />
                <asp:DropDownList ID="ddlPersonal" runat="server" BackColor="#E8EEF7" Font-Bold="False" AutoPostBack="True"></asp:DropDownList>
                <br />
                                Descuentos<br />
                <asp:DropDownList ID="ddlMes" runat="server" BackColor="#E8EEF7" 
                    Font-Bold="False" AutoPostBack="True">
                    <asp:ListItem Value="1">ENERO</asp:ListItem>
                    <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                    <asp:ListItem Value="3">MARZO</asp:ListItem>
                    <asp:ListItem Value="4">ABRIL</asp:ListItem>
                    <asp:ListItem Value="5">MAYO</asp:ListItem>
                    <asp:ListItem Value="6">JUNIO</asp:ListItem>
                    <asp:ListItem Value="7">JULIO</asp:ListItem>
                    <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                    <asp:ListItem Value="9">SETIEMBRE</asp:ListItem>
                    <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                    <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                    <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAño" runat="server" BackColor="#E8EEF7" 
                    Font-Bold="False" AutoPostBack="True"></asp:DropDownList>
                    <br />
                <br />
                <div id="mensaje" runat="server"></div>
                <div id="GeneraPrestamo" runat="server">
                    <asp:Button ID="btnRedireccionar" runat="server" Text="Generar" CssClass="btnGenerar" />
                </div>
                </td>            
          
            <td rowspan="3" valign="top" style="padding-left:50px; padding-top:15px;">
            
                <asp:Panel ID="pnlResumen" runat="server" Visible="false">
                    <table cellpadding="4" cellspacing="0" class="tbl" style="margin:3px;">
                 
                        <tr>
                            <th>
                                RESUMEN</th>
                            <th style="border-right: 1px solid #99BAE2;">
                                (+)</th>
                            <th style="border-right: 1px solid #99BAE2;">
                                (-)</th>
                            <th>
                                (=)</th>
                        </tr>
                        <tr>
                            <td style="border-top: 1px solid #99BAE2;">
                                Total Ingresos</td>
                            <td style="border-top: 1px solid #99BAE2; border-right: 1px solid #99BAE2; text-align:right;">
                                <asp:Label ID="lblIngresos" runat="server" 
                                    style="color:blue; text-align: right;" Text="0.00"></asp:Label>
                            </td>
                            <td style="border-top: 1px solid #99BAE2;border-right: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td style="border-top: 1px solid #99BAE2;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Total Ingresos Extraord.</td>
                            <td style="border-right: 1px solid #99BAE2; text-align:right;">
                                <asp:Label ID="lblIngresosExt" runat="server" 
                                    style="color:purple; text-align: right;" Text="0.00"></asp:Label>
                            </td>
                            <td style="border-right: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                Total Egresos</td>
                            <td style="border-right: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td style="border-right: 1px solid #99BAE2; text-align:right;">
                                <asp:Label ID="lblEgresos" runat="server" style="color:red; text-align: right;" 
                                    Text="0.00"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total Descuentos</td>
                            <td style="border-right: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td style="border-right: 1px solid #99BAE2; text-align:right;">
                                <asp:Label ID="lblDescuentos" runat="server" style="color:green" Text="0.00"></asp:Label>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td style="border-top: 1px solid #99BAE2; color:#3366CC; font-weight:bold;">
                                <b>Neto Remunerativo</b></td>
                            <td style="border-top: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td style="border-top: 1px solid #99BAE2;">
                                &nbsp;</td>
                            <td style="border-top: 1px solid #99BAE2;color:#3366CC; font-weight:bold; text-align:right;">
                                <asp:Label ID="lblNeto" runat="server" 
                                    style="font-weight: 700; text-align:center;" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="color:Purple">
                                <i><b>Neto + Ingreso Extraord.</b></i></td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                            <td style="text-align:center;">
                                <asp:Label ID="lblNetoyExt" runat="server" 
                                    style="font-weight: 700; font-style: italic; text-align: right; color:Purple" Text="0.00"></asp:Label>
                            </td>
                        </tr>                       
                    </table>
                </asp:Panel>            
            </td>
        </tr>               
        </table>        
        <!--<table>
        <tr>
        <td>
       
        </td>
        </tr>
        </table>-->
        <table>
        <tr>
        <td valign="top">
            <asp:HiddenField ID="HdEstado" runat="server" />
            <asp:GridView ID="gridIngresosEgresos" runat="server" 
                AutoGenerateColumns="False" CaptionAlign="Top" 
                DataKeyNames="codigo_dpc,codigo_ddc" BorderStyle="None" CellPadding="4" 
                AlternatingRowStyle-BackColor="#F7F6F4">
            <RowStyle BorderColor="#C2CFF1" Font-Size="10px"/>
             <Columns>
                 <asp:BoundField DataField="TIPO" HeaderText="TIPO" Visible="TRUE">
                 <ItemStyle Font-Underline="false" ForeColor="black" />
                 </asp:BoundField>
                  <asp:BoundField DataField="DESCRIPCIÓN" HeaderText="DESCRIPCIÓN" Visible="true">
                 <ItemStyle Font-Underline="false" ForeColor="black" Width="350px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="MONTO_I" HeaderText="(+) MONTO S./" Visible="true" ItemStyle-HorizontalAlign="right">
                 <ItemStyle Font-Underline="false" ForeColor="blue" Font-Size="11px" />
                 </asp:BoundField>
                 <asp:BoundField DataField="MONTO_E" HeaderText="(-) MONTO S./" Visible="true" ItemStyle-HorizontalAlign="right">
                 <ItemStyle Font-Underline="false" ForeColor="red" Font-Size="11px" />
                 </asp:BoundField>                    
                 <asp:TemplateField HeaderText="Ver">
                 <ItemTemplate>
                         <asp:ImageButton ID="imgVer" runat="server" 
                             ImageUrl="~/administrativo/ver.gif"
                             style="height: 20px" CommandName="VerDetalle" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"  />
                     </ItemTemplate>
                     
                 </asp:TemplateField>
                    
                 <asp:BoundField DataField="codigo_dpc" HeaderText="codigo_dpc" 
                     Visible="False" />
                    
                 <asp:BoundField DataField="codigo_ddc" HeaderText="codigo_ddc" 
                     Visible="False" />
                    
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#0066CC; background-color:#F7F6F4; padding:5px; font-style:italic;">
                    No se encontró registros
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
            </asp:GridView>           
            </td> 
            <td valign="top" style="padding-left:20px; padding-top:0px;">
            <div id="panelDetalle" runat="server" visible="False" style="border: 1px solid #9D9DA1;">
            <table width="100%" cellpadding="3" cellspacing=0>
            <tr><td colspan="2" style="background-color:#e8eef7; color:#3366CC; padding:5px; font-weight:bold;">DETALLE DE DEUDA</td></tr>
            <tr><td style="color:#3366CC;">Fecha de Registro</td><td><asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label></td></tr>
            <tr><td style="color:#3366CC;">Rubro</td><td><asp:Label ID="lblRubro" runat="server" Text="Label"></asp:Label></td></tr>
            
            <tr><td style="color:#3366CC;">Importe Total S./</td>
                <td style="text-align: right"><asp:Label ID="lblImporte" runat="server" 
                        Text="Label" style="font-weight: 700"></asp:Label></td></tr>
            <tr><td style="color:#3366CC;">Importe Pagado S./</td>
                <td style="text-align: right"><asp:Label ID="lblPagado" runat="server" Text="Label" 
                        style="font-weight: 700"></asp:Label></td></tr>
            <tr><td style="color:#3366CC;"><b>Importe Pendiente S./</b></td>
                <td style="text-align: right; border-top:1px solid #99BAE2;"><asp:Label ID="lblPendiente" runat="server" 
                        Text="Label" style="font-weight: 700"></asp:Label></td></tr>
            </table>
            <table>
            <tr>
            <td>
            <div style="border:0px solid; max-height:350px; overflow:scroll; padding:10px;">
            <asp:GridView ID="gridDetalle" runat="server" AutoGenerateColumns="False" CaptionAlign="Top"  BorderStyle="None" CellPadding="4" AlternatingRowStyle-BackColor="#F7F6F4" DataKeyNames="codigo_ddc">
            <RowStyle BorderColor="#C2CFF1" />
             <Columns>
                 <asp:BoundField DataField="fechvenc_ddc" HeaderText="Fecha Venc." 
                     Visible="true" ItemStyle-HorizontalAlign="Center">                 
                 <ItemStyle HorizontalAlign="Center" />   </asp:BoundField>
                <asp:BoundField DataField="importe_ddc" HeaderText="Importe" Visible="TRUE">
                    
                 </asp:BoundField>
                 <asp:BoundField DataField="importecancelado_ddc" HeaderText="Saldo">    
                 <ItemStyle Font-Underline="false" HorizontalAlign="Right" />
               </asp:BoundField>
                                                               
                 <asp:BoundField DataField="codigo_ddc" HeaderText="codigo_ddc" 
                     Visible="False" />                                                               
             </Columns>
             <EmptyDataTemplate>
                 <div style="color:#0066CC; background-color:#F7F6F4; padding:5px; font-style:italic;">
                    No se encontró registros
                 </div>
             </EmptyDataTemplate>
             <HeaderStyle BackColor="#e8eef7" BorderColor="#99BAE2" BorderStyle="Solid" BorderWidth="1px" 
                 ForeColor="#3366CC" />
             <AlternatingRowStyle BackColor="#F7F6F4" />
            </asp:GridView>   
            </div>            
            </td>
            
            </tr>
            </table>
            </div>
            </td>           
        </tr>
        </table>
        <br /><!--aqui-->
       
     </div>
      
    </form>
</body>
</html>
