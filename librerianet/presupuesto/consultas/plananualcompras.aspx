<%@ Page Language="VB" AutoEventWireup="false" CodeFile="plananualcompras.aspx.vb" Inherits="presupuesto_consultas_plananualcompras" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title>Plan Anual de Compras</title>
    <link href="../../../private/estilo.css" rel="stylesheet" type="text/css" />
    <%--<link href="../../private/estilo.css" rel="stylesheet" type="text/css" />--%>
    <link href="../../private/estiloweb.css" rel="stylesheet" type="text/css" />  
       
    <script type="text/javascript" src="../../../private/jq/jquery.js"></script>
    <script type="text/javascript" language="JavaScript" src="../../../private/funciones.js"></script>
    <link href="../../../private/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
        .style1
        {
            width: 11px;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            line-height :12pt;
            padding-top: 20px;
            padding-left: 10px;
            width: 520px;
            height: 190px;

          }
        td
        {   padding: 1px;
            
        }
        th
        {   text-align :center;  
        }
        #hlnkVerDetalle
        {   color:#E33439;  
        }
    </style>
  <script type ="text/javascript" >

      function abrir() {
          var bcgDiv = document.getElementById('<%=divbackground.ClientID%>');
                   if (bcgDiv != null) {
                            if (document.body.clientHeight > document.body.scrollHeight) {
                                bcgDiv.style.height = (document.body.clientHeight +20) & "px";
                            }
                           else {
                               bcgDiv.style.height = (document.body.scrollHeight+20) + "px";
                           }
                           if (document.body.clientWidth > document.body.scrollWidth) {
                               bcgDiv.style.width = (document.body.clientWidth+20) & "px";
                           }
                          else {
                              bcgDiv.style.width = (document.body.scrollWidth+20) + "px";
                          }
                         
          }

          var modal = document.getElementById('<%=pnlVerDetalle1.ClientID%>');
          if (modal != null) {
              modal.style.top = "30%";
              modal.style.left = "20%";
          }
      };

      function ResetScrollPosition() {
          setTimeout("window.scrollTo(0,0)", 0);
      }
      
  </script>
</head>
<body style ="margin :10px;">

 <form class="form-horizontal" role="form"  id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
 <p class="usattitulo"></p>
      
     Plan Anual de Compras<div class="form-group">
    <label for="ejemplo_email_3" class="col-lg-2 control-label">Proceso</label>
    <div class="col-lg-5">
     <asp:DropDownList ID="dpCodigo_pct" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                </asp:DropDownList>
    </div>
  </div>
  <div class="form-group" id="divs" runat ="server" >
    <label for="ejemplo_password_3" class="col-lg-2 control-label"> Centro de Costo</label>
    <div class="col-lg-5">
      <asp:DropDownList ID="dpCodigo_cco" runat="server" CssClass="form-control input-s">
                </asp:DropDownList>
    </div>
  </div>
  <div class="form-group">
    <label for="ejemplo_password_3" class="col-lg-2 control-label"> Clase</label>
    <div class="col-lg-5">
       <asp:DropDownList ID="dpCodigo_cla" runat="server" CssClass="form-control input-sm">
                </asp:DropDownList>
    </div>
  </div>
<%--  </div>--%>
 <asp:UpdatePanel runat="server" ID="up" UpdateMode ="Conditional" >
            <ContentTemplate >
  <div class="form-group">
    <div class="col-lg-offset-2 col-lg-2">
         <asp:Button ID="cmdDetalle" runat="server" Text="Detalle" OnClientClick="MostrarBox(document.getElementById('PanelDetalle'))" style="display:none" />
        
       
            <asp:Button ID="cmdConsultar" runat="server" Text="   Consultar" CssClass="btn btn-primary" />
     
         <asp:Button ID="cmdExportar" runat="server" Text="   Exportar" 
                    CssClass="btn btn-success" OnClick ="cmdExportar_Click" />
                   <div class="col-lg-1">         
     
    </div>
  </div>
   </div>
    <table cellpadding="2" cellspacing="0" style="width:100%">
        
        <tr>
            <td style="width: 10%">
                </td>
            <td style="width: 90%">
                
            &nbsp;
            </td>
        </tr>
    </table>
            
    <asp:GridView ID="grwPlanAnualCompras" runat="server"  UpdateMode="Conditional" ChildrenAsTriggers="false" 
        AutoGenerateColumns="False" CellPadding="3" ShowFooter="True">
         
         <Columns>
      
            <asp:BoundField HeaderText="Centro de Costos" DataField="descripcion_cco" >
            </asp:BoundField>
            <asp:BoundField DataField="CodItem" HeaderText="Código" />
            <asp:TemplateField HeaderText="Descripción Item">
                <ItemTemplate>
                    <asp:Label ID="lblDesEstandar" runat="server" Text='<%# eval("DesEstandar") %>'></asp:Label>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle" />
            <asp:BoundField DataField="numeracion_cta" HeaderText="Cuenta" />
            <asp:BoundField DataField="nombre_cta" HeaderText="Denominación" />
            <asp:BoundField HeaderText="Unidad" DataField="unidad" />
            <asp:BoundField DataField="preUnitario" HeaderText="Precio Unit." DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Cantidad" HeaderText="Cant." DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="subTotal" HeaderText="SubTotal" DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
          <%--  <asp:BoundField HeaderText="IndCant" DataField="indicoCantidades" >  
            <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>--%>
            
            <asp:TemplateField HeaderText ="Disponible" >
                <ItemTemplate>                                                                                                    
                <%--<asp:LinkButton ID="hlnkVerDetalle" runat="server" NavigateUrl="#" Text=<%# Eval("Disponible") %> ></asp:LinkButton>--%>
                <asp:LinkButton ToolTip="El detalle se visualizará seleccionando un Centro de Costos específico." STYLE="text-decoration:underline;COLOR:#265a88;" ID="hlnkVerDetalle" runat="server" NavigateUrl="#" Text=<%# Eval("Disponible") %> OnCommand ="CommandBtn_Click" CommandArgument= '<%#Eval("DesEstandar")&","& Eval("PresAcum")&","& Eval("SalEjeAcum")&","& Eval("Pedido")&","& Eval("SalPreEje")&","& Eval("MesIni")&","& Eval("MesFin")&","& Eval("PresAnt")&","& Eval("transferido_dpr")&","& Eval("Disponible")%>' ></asp:LinkButton>             
                      <%-- <cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="pnlVerDetalle1" TargetControlID="hlnkVerDetalle"
                        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
                </cc1:ModalPopupExtender>
         
           <asp:Panel ID="pnlVerDetalle1" runat="server"  CssClass="modalPopup" align="center" style = "display:none">
    <table cellspacing="0" cellpadding="2" border="1" style="color:Black;background-color:White;width:90%;border-collapse:collapse;">
            <tr style="color:White;background-color:#FF3300;font-weight:bold;"><th colspan = 2 style="text-align:center;" ><%# Eval("DesEstandar") %></th></tr>
            <tr><td>Presupuestado Acumulado</td>
                <td> <%# Eval("PresAcum") %></td></tr>
             <tr>
                <td>Saldo Ejecutado Acumulado</td>
                <td> <%# Eval("SalEjeAcum") %> (- Pedido: <%# Eval("Pedido") %>)</td>
            </tr>
            <tr>
                <td>Saldo Presupuestado Ejecutable</td>
                <td> <%# Eval("SalPreEje") %> (- Fechas:  <%# Eval("MesIni")%> - <%# Eval("MesFin") %>: <%# Eval("PresAnt") %>)</td>
            </tr>
            <tr>
                <td>Transferido</td>
                <td> <%# Eval("transferido_dpr") %></td>
            </tr>
            <tr>
                <td>Disponible</td>
                <td style ="font-weight:bold"> <%# Eval("Disponible") %></td>
            </tr>
    </table>
    <br />
    <asp:Button ID="btnClose" runat="server" Text="Salir" />
    
</asp:Panel>--%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Right" />
            </asp:TemplateField >
           
            <asp:BoundField HeaderText="Ene." DataField="Ene" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Feb." DataField="Feb" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Mar." DataField="Mar" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Abr." DataField="Abr" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="May." DataField="May" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Jun." DataField="Jun" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Jul." DataField="Jul" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Ago." DataField="Ago" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Sep." DataField="Sep" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Oct." DataField="Oct" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Nov." DataField="Nov" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Dic." DataField="Dic" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Total Unid" DataFormatString="{0:F2}" 
                DataField="totalUnd" />
        </Columns>
        <EmptyDataTemplate>
                    <p class="rojo"><b>No se han encontrado Plan Anual de Compras según los criterios seleccionados</b></p>
          </EmptyDataTemplate>
          <FooterStyle HorizontalAlign="Center" CssClass="TituloTabla"  BackColor= "#E33439" Height="20px" />
         <HeaderStyle CssClass="TituloTabla" BackColor= "#E33439" HorizontalAlign="Center"  Height="20px" />
    </asp:GridView>     
                    
                    
         <asp:Panel runat="server" ID="pnlVerDetalle1" CssClass="modalPopup" align="center" style = "display:none;position: absolute;z-index:1000; width: 520px; height: 190px; top: 0; left: 0;">
            <table  id="tbl" cellspacing="0" cellpadding="2" border="1" style="color:Black;background-color:White;width:90%;border-collapse:collapse;">
                    <tr style="color:White;background-color:#FF3300;font-weight:bold;"><th colspan = 2 style="text-align:center;" id="thDesEstandar" runat="server"></th></tr>
                    <tr><td>Presupuestado Acumulado</td>
                        <td id="tdPresAcum" runat ="server" > </td></tr>
                     <tr>
                        <td>Saldo Ejecutado Acumulado</td>
                        <td id="tdSalEjeAcum" runat ="server"> </td>
                    </tr>
                    <tr>
                        <td>Saldo Presupuestado Ejecutable</td>
                        <td id="tdSalPresEje" runat ="server"> </td>
                    </tr>
                    <tr>
                        <td>Transferido</td>
                        <td id="tdTransferido" runat ="server"> </td>
                    </tr>
                    <tr>
                        <td>Disponible</td>
                        <td style="font-weight:bold" id="tdDisponible" runat ="server"> </td>
                    </tr>
            </table>
            <br />
            <asp:Button ID="btnClose" runat ="server"  Text="Salir" OnClick="btnClose_Click" />
            
        </asp:Panel>
        
        <div runat="server" id = "divBackground" style=" position:absolute; top:0px; left:0px;background-color:black; z-index:100;opacity: 0.8;filter:alpha(opacity=90); -moz-opacity: 0.8; overflow:hidden; display:none;">
        </div>
        
            </ContentTemplate>
            
             <Triggers>
                <asp:PostBackTrigger ControlID="cmdExportar" />
                <asp:PostBackTrigger ControlID="btnClose" />
                <asp:PostBackTrigger ControlID="grwPlanAnualCompras" />
            </Triggers>
        </asp:UpdatePanel>
                
     
    
  <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
        AssociatedUpdatePanelID="up">
        <ProgressTemplate>
            <font style="color:#265a88;font-size :small ;">Procesando. Espere un momento...</font>
        </ProgressTemplate>
    </asp:UpdateProgress>
           
    <%--<asp:GridView ID="GridView1" runat="server" 
        AutoGenerateColumns="False" CellPadding="3" ShowFooter="True" style="display:none ;">
         
         <Columns>
      
            <asp:BoundField HeaderText="Centro de Costos" DataField="descripcion_cco" >
            </asp:BoundField>
            <asp:BoundField DataField="CodItem" HeaderText="Código" />
            <asp:TemplateField HeaderText="Descripción Item">
                <ItemTemplate>
                    <asp:Label ID="lblDesEstandar" runat="server" Text='<%# eval("DesEstandar") %>'></asp:Label>
                    <br />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle" />
            <asp:BoundField DataField="numeracion_cta" HeaderText="Cuenta" />
            <asp:BoundField DataField="nombre_cta" HeaderText="Denominación" />
            <asp:BoundField HeaderText="Unidad" DataField="unidad" />
            <asp:BoundField DataField="preUnitario" HeaderText="Precio Unit." DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Cantidad" HeaderText="Cant." DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="subTotal" HeaderText="SubTotal" DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="Disponible" HeaderText="Disponible" DataFormatString="{0:F2}">
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
                               
            <asp:BoundField HeaderText="Ene." DataField="Ene" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Feb." DataField="Feb" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Mar." DataField="Mar" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Abr." DataField="Abr" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="May." DataField="May" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Jun." DataField="Jun" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Jul." DataField="Jul" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Ago." DataField="Ago" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Sep." DataField="Sep" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Oct." DataField="Oct" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Nov." DataField="Nov" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Dic." DataField="Dic" DataFormatString="{0:F2}" />
            <asp:BoundField HeaderText="Total Unid" DataFormatString="{0:F2}" 
                DataField="totalUnd" />
        </Columns>
        <EmptyDataTemplate>
                    <p class="rojo"><b>No se han encontrado Plan Anual de Compras según los criterios seleccionados</b></p>
          </EmptyDataTemplate>
          <FooterStyle HorizontalAlign="Center" CssClass="TituloTabla"  BackColor= "#E33439" Height="20px" />
         <HeaderStyle CssClass="TituloTabla" BackColor= "#E33439" HorizontalAlign="Center"  Height="20px" />
    </asp:GridView>--%>
</form>

</body>
</html>
