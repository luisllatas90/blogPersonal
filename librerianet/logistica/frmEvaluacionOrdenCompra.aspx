<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmEvaluacionOrdenCompra.aspx.vb" Inherits="logistica_frmEvaluacionOrdenCompra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1"/>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" /> 
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" /> 
<script type="text/javascript" src='../assets/js/jquery.js'></script>
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css' />
    <script type="text/javascript" src='../assets/js/app.js'></script>
    <%--<script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>--%>
    <script type="text/javascript" src='../assets/js/bootstrap.min.js'></script>
    <script type="text/javascript" src='../assets/js/jquery.nicescroll.min.js'></script>
    <script type="text/javascript" src='../assets/js/wow.min.js'></script>
    <script type="text/javascript" src="../assets/js/jquery.nicescroll.min.js"></script>
    <script type="text/javascript" src='../assets/js/jquery.loadmask.min.js'></script>
    <%--    <script type="text/javascript" src='../../assets/js/jquery.accordion.js'></script>

    <script type="text/javascript" src='../../assets/js/materialize.js'></script>

    <script type="text/javascript" src='../../assets/js/bic_calendar.js'></script>

    <script type="text/javascript" src='../../assets/js/core.js'></script>--%>
    <script type="text/javascript" src='../assets/js/jquery.countTo.js'></script>
    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>
    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>
    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>
    <script type="text/javascript" src='../assets/js/jquery.dataTables.min.js'></script>
    <script type="text/javascript" src='../assets/js/funciones.js'></script>

    <%--    <script type="text/javascript" src="../../assets/js/DataJson/jsselect.js?x=10"></script>

    <script type="text/javascript" src='../../assets/js/form-elements.js'></script>

    <script type="text/javascript" src='../../assets/js/select2.js'></script>

    <script type="text/javascript" src='../../assets/js/jquery.multi-select.js'></script>--%>

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>
    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />
       <%-- <script src="../Egresado/jquery/jquery-1.9.1.js" type="text/javascript"></script>--%>
    <%--<script src="../private/jquery-ui-1.12.0.custom/jquery-ui.min.js" type="text/javascript"></script>--%>

    <%--<script src="../Egresado/jquery/jquery-ui.js" type="text/javascript"></script>--%>
        
    <%--<link href="../Egresado/jquery/jquery-ui.css" rel="stylesheet" type="text/css" />--%>
    <script src="../../private/funciones.js" type ="text/javascript" language ="javascript"></script>
    

    <script type ="text/javascript">
       
                  function openWin(url, w, h) {
                      var left = (screen.width / 2) - (w / 2);
                      var top = (screen.height / 2) - (h / 2);
                      var strWindowFeatures = "width=" + w + ",height=" + h + ",top="+top+", left= "+left + ",menubar=no,location=no,resizable=no,scrollbars=no";
                      w = window.open(url, "CNN_WindowName", strWindowFeatures);
                     
                      w.focus();
                  };
  </script>
  <script type ="text/javascript" >

      function ModalAdjuntar2(cod_rco) {
          $("#cod_rco").val(cod_rco)
          $("#txtfile").val("");
          fnVer(cod_rco)
          $('.AdjuntoProy').attr("data-toggle", "modal");
          $('.AdjuntoProy').attr("data-target", "#mdRegistro");
          //alert('a');
      }

      function fnVer(c) {
          console.log(c);
          try {
              $.ajax({
                  type: "POST",
                  url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                  data: { "action": "Ver", "cod1": c, "cod2": "", "idTabla": 6 },
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      console.log(data);
                      var tb = '';
                      var i = 0;
                      var filas = data.length;
                      for (i = 0; i < filas; i++) {
                          tb += '<tr>';
                          tb += '<td>' + (i + 1) + "" + '</td>';
                          tb += '<td><i  class="' + data[i].nExtension + '"></i> ' + data[i].nArchivo + '</td>';
                          tb += '<td>';
                          tb += '<button onclick="fnDownload(\'' + data[i].cCod + '\');" class="btn btn-primary"><i  class=" ion-android-download"><span></span></i></button>';
                          tb += '</td>';
                          tb += '</tr>';
                      }
                      if (filas > 0) $('#mdFiles').modal('toggle');
                      $('#tbFiles').html(tb);
                  },
                  error: function(result) {
                      console.log(result);
                  }
              });
          }
          catch (err) {
              console.log(err.message);
          }  
      }
      function fnDownload(id_ar) {
          var flag = false;
          try {
              $.ajax({
                  type: "POST",
                  url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                  data: { "action": "Download", "cod1": "", "cod2": "", "idTabla": 6, "cod_Arch": id_ar },
                  dataType: "json",
                  cache: false,
                  success: function(data) {
                      flag = true;
                      var file = 'data:application/octet-stream;base64,' + data[0].File;
                      var link = document.createElement("a");
                      link.download = data[0].Nombre;
                      link.href = file;
                      link.click();
                      if (navigator.userAgent.indexOf("NET") > -1) {
                          var param = { 'Id': id_ar };
                          window.open("../DataJson/Logistica/DescargarArchivo.aspx?Id=" + id_ar, 'ta', "");
                      }
                      fnMensaje(data[1].tipo, data[1].msje);
                  },
                  error: function(result) {
                      console.log(result);
                      fnMensaje(data[1].tipo, data[1].msje);
                      flag = false;
                  }
              }); 
              return flag;
          }
          catch (err) {
              console.log(err.message);
          }
      }

    </script>
    <style type="text/css">
        .style2
        {
            height: 116px;
        }
        .style3
        {
            height: 178px;
        }
        .style4
        {
            height: 23px;
        }
        .style5
        {
            height: 9px;
        }
        .style6
        {
            height: 12px;
        }
        .style7
        {
            height: 17px;
        }
        input[type="radio"],input[type="checkbox"]
        {
            display:inline;     
        }
        body
        {
            background-color: white;
        }    
        table tr th
        {
            border-color: white;
            border-width: 1px;
            padding: 2px;
            text-align: center;
            font-weight: bold;
        }
        table tbody tr td
        {
            padding: 4px;
        }
        table tbody tr td input
        {
            cursor: pointer;
        }
        td,th
        {
            border:none;
        }
        input[type="radio"] + label {
            color: black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
      <busyboxdotnet:busybox id="BusyBox1" runat="server" showbusybox="OnLeavingPage" image="Clock"
                text="Su solicitud está siendo procesada..." title="Por favor espere" />
    <div>
    
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
        <table style="width:100%;" cellpadding="0" cellspacing="2" >
            <tr>
                <td>
                    Ver ordenes [O/S - O/C]:
                    <asp:DropDownList ID="cboEstado" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="P">Pendiente</asp:ListItem>
                        <asp:ListItem Value="A">Conforme</asp:ListItem>
                        <asp:ListItem Value="D">Rechazado</asp:ListItem>
                        <asp:ListItem Value="X">Derivado</asp:ListItem>
                        <asp:ListItem Value="O">Observado</asp:ListItem>
                    </asp:DropDownList>
                    <br />
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarOrdenesParaEvaluacion" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="codigo_per" QueryStringField="id" 
                                    Type="Int32" />
                                <asp:ControlParameter ControlID="cboEstado" Name="estado_Rcom" 
                                    PropertyName="SelectedValue" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarEstadosCompras" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                </td>
            </tr>
           <%-- <tr>
                <td>
                    &nbsp;</td>
            </tr>--%>
            <tr>
                <td >
                    <asp:Panel ID="Panel1" runat="server"  >
                        &nbsp;<asp:GridView ID="gvCabOrden" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" 
                            DataKeyNames="codigo_Rco,codigo_Rcom,Tipo Documento" 
                            DataSourceID="SqlDataSource4" PageSize="5" Width="100%" BorderColor ="White" >
                            <Columns>
                                <asp:BoundField DataField="codigo_Rco" HeaderText="codigo_Rco" 
                                    InsertVisible="False" ReadOnly="True" SortExpression="codigo_Rco" 
                                    Visible="False" />
                                <asp:BoundField DataField="fechaReg_Rco" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha Reg." SortExpression="fechaReg_Rco">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="fechaDoc_Rco" DataFormatString="{0:dd/MM/yyyy}" 
                                    HeaderText="Fecha Doc." SortExpression="fechaDoc_Rco">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_Tdo" HeaderText="Tipo Documento" 
                                    SortExpression="descripcion_Tdo" Visible="False" />
                                <asp:BoundField DataField="Tipo Documento" HeaderText="Tipo Orden" 
                                    ReadOnly="True" SortExpression="Tipo Documento">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="numeroDoc_Rco" HeaderText="Número" 
                                    SortExpression="numeroDoc_Rco">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="nombrePro" HeaderText="Proveedor" 
                                    SortExpression="nombrePro" />
                                <asp:BoundField DataField="moneda_Rco" HeaderText="Moneda" 
                                    SortExpression="moneda_Rco">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="porcentajeIGV_Rco" 
                                    DataFormatString="{0:#,###,##0.00}" HeaderText="IGV" 
                                    SortExpression="porcentajeIGV_Rco">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:CheckBoxField DataField="precioIncluyeIGV_Rco" HeaderText="Exonerado IGV" 
                                    SortExpression="precioIncluyeIGV_Rco">
                                <ItemStyle HorizontalAlign="Center" />
                                </asp:CheckBoxField>
                                <asp:BoundField DataField="totalCompra_Rco" DataFormatString="{0:#,###,##0.00}" 
                                    HeaderText="Total (S/.)" SortExpression="totalCompra_Rco">
                                <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="descripcion_Fpc" 
                                    HeaderText="Condición de Pago" SortExpression="descripcion_Fpc">
                                <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText ="Observación">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkObservacion" runat="server" CausesValidation="false" CommandName ="Obs" 
                                            CommandArgument='<%# Eval("codigo_Rco") %>' Text=<%#Iif( Eval("referencia_Rco").ToString()<>"","Ver","Ninguna") %> Enabled =<%#Iif( Eval("referencia_Rco").ToString()<>"","True","False") %>  ForeColor="Red" 
                                            OnClientClick  ='<%#Iif( Eval("referencia_Rco").ToString()<>"",Eval("codigo_Rco","openWin(""abrirVentana.aspx?numero={0}"", 400, 200); return false;"),"#")  %>'>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjuntos">
                                    <ItemTemplate>
                                        <img id="AdjuntoProy" name="AdjuntoProy" src="../images/adjuntar.png" runat="server"
                                            style="width: 20px; height: 20px; cursor:pointer;" alt="Adjuntar" title="Adjuntar Archivo" class="AdjuntoProy" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="nombre_Alm" HeaderText="Almacén"
                                    SortExpression="nombre_Alm" />
                                <asp:BoundField DataField="usuario" HeaderText="Registrado por" 
                                    SortExpression="usuario" />
                                <asp:CommandField SelectText="" ShowSelectButton="True" />
                            </Columns>
                            <FooterStyle Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#FF3300" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <table  style="width:100%;">
                        <tr>
                <td>
                    Evaluación:</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="rbtVeredicto" 
                        ErrorMessage="Seleccione el veredicto de la evaluación" 
                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:RadioButtonList ID="rbtVeredicto" runat="server" 
                        RepeatDirection="Horizontal" AutoPostBack="True">
                        <asp:ListItem Value="A">Aprobar</asp:ListItem>
                        <asp:ListItem Value="D">Rechazar</asp:ListItem>
                        <asp:ListItem Value="O" Enabled="False">Observar</asp:ListItem>
                        <asp:ListItem Value="X" Enabled="False">Derivar</asp:ListItem>
                    </asp:RadioButtonList>
                &nbsp;</td>
                <td>
        <asp:Panel ID="pnlDerivar" runat="server" Visible="False">
            Derivar a:&nbsp; <asp:DropDownList ID="cboPersonalDerivar" runat="server" 
                AutoPostBack="True">
            </asp:DropDownList>
        </asp:Panel>
                </td>
                <td>
                    <asp:LinkButton ID="lnkRevisiones" runat="server" AutoPostBack="false" OnClientClick=""
                        ForeColor="Blue">Ver 
                    Revisiones</asp:LinkButton>
&nbsp;|<asp:LinkButton ID="lnkDatosGenerales" runat="server" ForeColor="Blue">Datos Generales</asp:LinkButton>
                                </td>
                        </tr>
                        <tr>
                <td valign="top" class="style4">
                    Observación:</td>
                <td valign="top" class="style4">
                            </td>
                <td valign="top" colspan="2" class="style4">
                    <asp:TextBox ID="txtObservacion" runat="server" Rows="3" 
                        Width="100%"></asp:TextBox>
                </td>
                <td align="right" valign="top" class="style4">
                    <asp:Button ID="cmdGuardar" runat="server" Text="Calificar" 
                        ValidationGroup="Guardar" Height="23px" Width="95px" />
&nbsp;
                </td>
                        </tr>
                        <tr>
                <td valign="top" colspan="5">
                                    <asp:GridView ID="gvDetalleCompra" runat="server" AutoGenerateColumns="False" 
                                        DataKeyNames="idArt" DataSourceID="SqlDataSource2" 
                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_rco" HeaderText="codigo_rco" ReadOnly="True" 
                                                SortExpression="codigo_rco" Visible="False" />
                                            <asp:BoundField DataField="idArt" HeaderText="idArt" ReadOnly="True" 
                                                SortExpression="idArt" Visible="False" />
                                            <asp:BoundField DataField="DetalleArticulo_Dco" HeaderText="Artículo" 
                                                SortExpression="DetalleArticulo_Dco" >
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" 
                                                SortExpression="descripcionuni" />
                                            <asp:BoundField DataField="Precio_Dco" DataFormatString="{0:#,###,##0.00}" 
                                                HeaderText="Precio" SortExpression="Precio_Dco">
                                                <ItemStyle HorizontalAlign="Right" BackColor="#FFDF9D" Font-Bold="True" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad_Dco" HeaderText="Cantidad" 
                                                SortExpression="cantidad_Dco">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="subtotal_Dco" DataFormatString="{0:#,###,##0.00}" 
                                                HeaderText="Sub total" SortExpression="subtotal_Dco">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField HeaderText="Pedidos Relacionados" SelectText="Ver" 
                                                ShowSelectButton="True">
                                                <ItemStyle ForeColor="Red" HorizontalAlign="Center" Width="100px" />
                                            </asp:CommandField>
                                        </Columns>
                                         <FooterStyle Font-Bold="True" ForeColor="White" />
                                            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                    <hr />
                            </td>
                        </tr>
                        <tr>
                <td colspan="5">
                    <asp:Panel ID="pnlDatosGenerales" runat="server" Visible="true">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarPedidosRelacionadosOrden" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                            <asp:ControlParameter ControlID="gvDetalleCompra" Name="id_art" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:GridView ID="gvDetalleOrdenPedido" runat="server" 
                                        AutoGenerateColumns="False" DataKeyNames="codigo_Dpe" Visible="False" 
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_rco" ReadOnly="True" 
                                                SortExpression="codigo_cco" />
                                            <asp:BoundField DataField="codigo_Ped" HeaderText="idArt" ReadOnly="True" 
                                                SortExpression="codigo_Ped" />
                                            <asp:BoundField DataField="codigo_Rco" HeaderText="Artículo" 
                                                SortExpression="codigo_Rco">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="codigo_Dpe" HeaderText="Unidad" 
                                                SortExpression="codigo_Dpe" />
                                        </Columns>
                                         <FooterStyle Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td class="style7">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td >
                                    » Pedidos Relacionados<asp:GridView ID="gvPedidos" runat="server" 
                                        AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" 
                                                SortExpression="codigo_cco" Visible="False" />
                                            <asp:BoundField DataField="AreaPresupuestal" HeaderText="Centro de costos" 
                                                SortExpression="AreaPresupuestal">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="codigo_Ped" HeaderText="Nro Pedido" 
                                                InsertVisible="False" ReadOnly="True" SortExpression="codigo_Ped">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="fecha_Ped" HeaderText="Fecha Pedido" 
                                                SortExpression="fecha_Ped" />
                                            <asp:BoundField DataField="descripcionArt" HeaderText="Artículo" 
                                                SortExpression="descripcionArt" />
                                            <asp:BoundField DataField="observacion_Dpe" HeaderText="Observación" 
                                                SortExpression="observacion_Dpe" />
                                            <asp:BoundField DataField="descripcionuni" HeaderText="Unidad" 
                                                SortExpression="descripcionuni" />
                                            <asp:BoundField DataField="precioReferencial_Dpe" 
                                                HeaderText="Precio referencial" SortExpression="precioReferencial_Dpe">
                                                <ItemStyle BackColor="#FFFFD2" Font-Bold="True" HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="cantidad_Dpe" HeaderText="Cantidad Pedida" 
                                                SortExpression="cantidad_Dpe">
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="usuario_per" HeaderText="Solicitante" 
                                                SortExpression="usuario_per" />
                                        </Columns>
                                         <FooterStyle ForeColor="White" />
                                        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                                        SelectCommand="LOG_ConsultarDetalleOrden" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                        </tr>
                        <tr>
                <td class="style2" colspan="5">
                    <asp:Panel ID="pnlObservaciones" runat="server">
                        <table style="width:100%;">
                            <tr>
                                <td>
                                    Revisiones</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    Observaciones resueltas</td>
                            </tr>
                            <tr>
                                <td width="48%">
                                    <asp:GridView ID="gvRevisiones" runat="server" DataSourceID="SqlDataSource6" 
                                        Width="100%" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="Tipo Orden" HeaderText="Tipo Orden" />
                                            <asp:BoundField DataField="login_Per" HeaderText="Usuario" />
                                            <asp:BoundField DataField="Evaluación" HeaderText="Evaluación" />
                                            <asp:BoundField DataField="Observacion" HeaderText="Observación" />
                                            <asp:BoundField DataField="fecha_Rcom" HeaderText="Fecha" />
                                        </Columns>
                                        <FooterStyle Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td valign="top" width="48%">
                                    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource5" 
                                        Width="100%">
                                         <FooterStyle Font-Bold="True" ForeColor="White" />
                                        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                        </tr>
                        <tr>
                <td class="style5">
                    &nbsp;</td>
                <td class="style5">
                            </td>
                <td colspan="2" class="style5">
                                    </td>
                <td class="style5">
                            </td>
                        </tr>
                        <tr>
                <td class="style6" colspan="5">
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                            SelectCommand="LOG_ConsultarObservacionesOrdenes" 
                            SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                    PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                            </td>
                        </tr>
                        <tr>
                <td class="style4">
                        <asp:SqlDataSource ID="SqlDataSource6" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
                        SelectCommand="LOG_ConsultarRevisionesOrdenes" 
                            SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="gvCabOrden" Name="codigo_rco" 
                                PropertyName="SelectedValue" Type="Int32" />
                        </SelectParameters>
                        </asp:SqlDataSource>
                            </td>
                <td class="style4">
                            </td>
                <td colspan="2" class="style4">
                                <asp:DropDownList ID="cboDerivar" runat="server" Visible="False"></asp:DropDownList>
                            </td>
                <td class="style4">
                            </td>
                        </tr>
                        </table>
                </td>
            </tr>
            </table>
            
                </ContentTemplate>
            </asp:UpdatePanel>
    
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Guardar" 
        style="margin-bottom: 2px" />
    <asp:HiddenField ID="hddPersonal" runat="server" />
    </form>
    <div class="row">
        <div class="modal fade" id="mdRegistro" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" role="dialog" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #3871B0; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel3">
                            Descargar Archivos</h4>
                    </div>
                    <div class="modal-body">
                        <div id="divMessage">
                        </div>
                        <form id="frmRegistro" name="frmRegistro" enctype="multipart/form-data" class="form-horizontal"
                        method="post" onsubmit="return false;" action="#">
                        <div class="row">
                            <div id="msje">
                            </div>
                        </div>
                        <div class="row">
                            <input type="hidden" id="cod_dpe" value="" runat="server" />
                            <input type="hidden" id="cod_ped" value="" runat="server" />
                            <input type="hidden" id="action" value="" runat="server" />
                        </div>
                        <div class="row">
                            <table style="width: 70%;" class="display dataTable">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">N°
                                        </th>
                                        <th style="width: 80%">
                                            Archivo
                                        </th>
                                        <th style="width: 10%">
                                            Opci&oacute;n
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="tbFiles">
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <th colspan="3">
                                        </th>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>
                        <%--<div class="row">
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Adjunto:</label>
                                <div class="col-sm-8">
                                    <input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />
                                </div>
                                <div style="float: left;" id="divLoading" class="hidden">
                                    <img id="imgload" src="../assets/images/loading.GIF"></div>
                            </div>
                        </div>--%>
                        </form>
                    </div>
                    <div class="modal-footer">
                        <center>
                           <%-- <button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">
                                Guardar</button>
                            <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                Cancelar</button>--%>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
