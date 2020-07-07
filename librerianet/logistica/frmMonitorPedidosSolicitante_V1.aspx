<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmMonitorPedidosSolicitante_V1.aspx.vb"
    Inherits="logistica_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Monitor de Pedidos de Logística</title>
    <link href="../private/estilo.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloweb.css" rel="stylesheet" type="text/css" />
    <link href="../private/estiloctrles.css" rel="stylesheet" type="text/css" />

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

    <script type="text/javascript" src='../assets/js/bootstrap-colorpicker.js'></script>

    <link rel='stylesheet' href='../assets/css/jquery.dataTables.min.css' />

    <script src="../../private/funciones.js" type="text/javascript" language="javascript"></script>

    <script src="../../private/PopCalendar.js" language="javascript" type="text/javascript"></script>

    <script type="text/javascript">
        var ffile;

        $(document).ready(function() {
            jQuery(function($) {
                $("#TxtFechaEsperada").mask("99/99/9999");
            });
            Sys.Browser.WebKit = {};
            if (navigator.userAgent.indexOf('WebKit/') > -1) {
                Sys.Browser.agent = Sys.Browser.WebKit;
                Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
                Sys.Browser.name = 'WebKit';
            }
        });
        function DisableEnviar() {
            //alert('d');
            document.getElementById("cmdEnviar").disabled = true;  
        };
      function ClearFile(){
        var fu = $('#File1')[0];  
        fu.value="";

        var fu2= fu.cloneNode(false);
        fu2.onchange= fu.onchange;
        fu.parentNode.replaceChild(fu2, fu);

    };
    function guardarFile() {

        ffile = $("#File1").get(0).files;
        if (ffile.length > 0) {
            if (ffile[0].size > 4194304) {
                alert('El archivo adjunto supera el peso máximo de 4MB');
                ClearFile();
            }
        }
        //console.log(ffile);
    };
    function ModalAdjuntar2(cod_dpe, cod_ped, tipo) {

        $("#div1").empty();
        $("#div2").empty();
        if (tipo === "Registrado") {
            var tb = '';
            var tb2 = '';
            tb += '<div class="form-group" id="divAdjunto">';
            tb += '<label class="col-sm-3 control-label" id="lblAdjunto"> Adjunto:</label>';
            tb += '<div class="col-sm-8">';
            tb += '<input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />';
            tb += '<label class="control-label" style="font-size: 12px;padding-top: 0px;">Tam. máx 5MB</label>';
            tb += '</div>';
            tb += '<div class="col-sm-10">';
            tb += '</div>';
            tb += '<div style="float: left;" id="divLoading" class="hidden">';
            tb += '<img id="imgload" src="../assets/images/loading.GIF"></div></div>';
            
            tb2 += '<div class="modal-footer" id="divfooter">';
            tb2 += '<center>';
            tb2 += '<button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">Guardar</button>';
            tb2 += '<button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">Cancelar</button>';
            tb2 += ' </center></div>';

            $('#div1').html(tb);
            $('#div2').html(tb2);
        }
        
        $("#cod_dpe").val(cod_dpe)
        $("#cod_ped").val(cod_ped)
        $("#txtfile").val("");
        fnVer(cod_dpe);
        ////console.log(tieneArchivos);
        $('.AdjuntoProy').attr("data-toggle", "modal");
        $('.AdjuntoProy').attr("data-target", "#mdRegistro");
        //alert('a');
    }

    function fnVer(c) {
        $.ajax({
            type: "POST",
            url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
            data: { "action": "Ver", "cod1": c, "idTabla": 2 },
            dataType: "json",
            cache: false,
            success: function(data) {
                //console.log(data);
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
                tieneArchivos = filas;
                //console.log(tieneArchivos);
                ////console.log(filas);
                if (filas > 0) {
                    // //console.log("aqui");
                    $('#mdFiles').modal('toggle');
                    //$('.AdjuntoProy').attr("data-toggle", "modal");
                    //$('.AdjuntoProy').attr("data-target", "#mdRegistro");
                } //else {
                //$('#mdRegistro').modal('hide');
                //}
                $('#tbFiles').html(tb);
            },
            error: function(result) {
            }
        });
    }
    function fnDownload(id_ar) {
        var flag = false;
        var form = new FormData();
        form.append("action", "Download");
        form.append("cod_Arch", id_ar);
        // alert();
        //console.log(form);
        $.ajax({
            type: "POST",
            url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
            data: form,
            dataType: "json",
            cache: false,
            contentType: false,
            processData: false,
            async: false,
            success: function(data) {
                flag = true;
                //console.log(data);
                var file = 'data:application/octet-stream;base64,' + data[0].File;
                var link = document.createElement("a");
                link.download = data[0].Nombre;
                link.href = file;
                link.click();
                if (navigator.userAgent.indexOf("NET") > -1) {

                    var param = { 'Id': id_ar };
                    // OpenWindowWithPost("DataJson/DescargarArchivo.aspx", "", "NewFile", param);
                    window.open("../DataJson/Logistica/DescargarArchivo.aspx?Id=" + id_ar, 'ta', "");

                }
                fnMensaje(data[1].tipo, data[1].msje);
            },
            error: function(result) {
                //console.log(result);
                fnMensaje(data[1].tipo, data[1].msje);
                flag = false;
            }
        });
        return flag;

    }
    function fnGuardar() {
        if ($("#txtfile").val() == "") {
            $("#divMessage").html("<p>Debe Selecionar un Archivo.</p>")
        } else {
            $("#btnGuardar").attr("disabled", true);
            fnLoadingDiv("divLoading", true);
            SubirArchivo($("#cod_dpe").val(), $("#cod_dpe").val());
            fnLoadingDiv("divLoading", false);
            //            $('#mdRegistro').modal('hide');
            //            $('#mdRegistro').modal('hide');
            //            fnMensaje('success', data[0].msje);
            //            fnConsultar();

            // fnMensaje('warning', data.msje);
            //            if ($("#" + data[0].obj)) {
            //                $("#" + data[0].obj).focus();
            //                $("#msje").addClass("alert alert-warning");
            //            } else {
            //                $("#msje").addClass("alert alert-danger");
            //            }
            $("#btnGuardar").removeAttr("disabled");
            fnVer($("#cod_dpe").val())
        } 
    }
    function SubirArchivo(c, n) {
            var flag = false;
            var form = new FormData();
            var files = $("#txtfile").get(0).files;
            //console.log(files);
            // Add the uploaded image content to the form data collection
            if (files.length > 0) {
                form.append("action", "Upload")
                form.append("cod1", $("#cod_dpe").val())
                form.append("cod2", $("#cod_ped").val())
                form.append("idTabla", 2)
                form.append("UploadedImage", files[0]);
            }
            //console.log(form);
            $.ajax({
                type: "POST",
                url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                data: form,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function(data) {
                    flag = true;
                    //console.log(data);
                    $("#txtfile").val("");
                    //		              fnMensaje('warning', 'Subiendo Archivo');
                    //		              $('#divMessage').addClass('alert alert-success alert-dismissable');
                    //		              $fileupload = $('#fileData');
                    //		              $fileupload.replaceWith($fileupload.clone(true));

                },
                error: function(result) {
                    //console.log(result);
                    $("#divMessage").html("<p>" & data[0].msje & "</p>");
                    flag = false;
                }
            });
            return flag;
        }
        function SubirArchivo2(c, n) {

            var flag = false;
            var form = new FormData();
            //console.log(ffile);
            var files = ffile;
            if (files.length > 0) {
                form.append("action", "Upload")
                form.append("cod1", c)
                form.append("cod2", n)
                form.append("idTabla", 2)
                form.append("UploadedImage", files[0]);

                $.ajax({
                    type: "POST",
                    url: "../DataJson/Logistica/Movimientos_Logistica.aspx",
                    data: form,
                    dataType: "json",
                    cache: false,
                    contentType: false,
                    processData: false,
                    success: function(data) {
                        flag = true;
                        //console.log(data);
                        ClearFile();
                        //		              fnMensaje('warning', 'Subiendo Archivo');
                        //		              $('#divMessage').addClass('alert alert-success alert-dismissable');
                        //		              $fileupload = $('#fileData');
                        //		              $fileupload.replaceWith($fileupload.clone(true));

                    },
                    error: function(result) {
                        ////console.log(result);
                        $("#divMessage").html("<p>" & data[0].msje & "</p>");
                        flag = false;
                    }
                });
                return flag;
                alert('ok');
            }
            ////console.log(form);

        }
    </script>

    <style type="text/css">
    
        table tr th
        {
            border-color: rgb(169,169,169);
            
            border-width: 1px;
            padding: 4px;
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
    </style>
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
            padding-top: 20px;
            padding-left: 10px;
            width: 520px;
            height: 160px;

          }
           table tr th
        {
            border-color: rgb(169,169,169);
            
            border-width: 1px;
            padding: 6px;
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
        body
        {
            background-color :White;
            font-size :8pt;    
        }
        input[type="radio"],input[type="checkbox"]
        {
            display:inline;     
        }
        input[type="checkbox"] + label {
            font-weight :bold;
            color :#2F4F4F;
        }
        span label
        {
            font-weight :bold;
        }
        #gvPresupuesto td
        {
            padding: 2px;
        }
        #imgEliminar
        {
            width:auto ;
            height :auto;
        }
        #panel1 td
        {
            padding :4px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <br />
    <%  response.write(clsfunciones.cargacalendario)%>
    &nbsp;
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <contenttemplate>
<table id="tblMarcos" style="height: 100%; width: 100%; margin-right: 0px" 
        align="right" class="contornotabla">
<tr  ><td>
    Ver mis pedidos en:&nbsp;&nbsp; <asp:DropDownList ID="cboInstancia" runat="server" AutoPostBack="True">
    </asp:DropDownList>
    
            </td>
            <td align="right">
    <asp:Button ID="cmdEnviar" runat="server" Text="  Enviar" 
                                    BorderStyle="Outset" CssClass="salir" Width="87px" 
                        Height="26px" Visible="False" />
    <asp:Button ID="cmdEliminar" runat="server" Text="Eliminar" 
                                    BorderStyle="Outset" CssClass="eliminar" Width="87px" 
                        Height="26px" Visible="False" />
    <%--treyes 05/07/2016--%>
    <%--<asp:Button ID="cmdClonar" runat="server" Text="Duplicar" 
                                    BorderStyle="Outset" CssClass="guardar" Width="95px" 
                        Height="26px" />--%>
            </td>
</tr>

<tr height="180"><td valign="top" class="contornotabla" colspan="2" >
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:CNXBDUSAT %>" 
        SelectCommand="LOG_ConsultarPedidos" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="SO" Name="tipo" Type="String" />
            <asp:QueryStringParameter DefaultValue="0" Name="codigo_per" 
                QueryStringField="ID" Type="Int32" />
            <asp:ControlParameter ControlID="cboInstancia" DefaultValue="1" 
                Name="instancia" PropertyName="SelectedValue" Type="Int32" />
            <asp:Parameter DefaultValue="P" Name="veredicto" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    Lista de pedidos:<asp:GridView ID="gvPedidos" runat="server" Width="100%" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" 
        DataKeyNames="Num,codigo_cco" DataSourceID="SqlDataSource1" ForeColor="#333333" 
        GridLines="None" BorderColor="White" BorderWidth="1px">
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:BoundField DataField="Num" HeaderText="Num" InsertVisible="False" 
                ReadOnly="True" SortExpression="Num" />
            <asp:BoundField DataField="Persona" HeaderText="Persona" ReadOnly="True" 
                SortExpression="Persona" />
            <asp:BoundField DataField="Fecha" HeaderText="Fecha" SortExpression="Fecha" />
            <asp:BoundField DataField="CeCo" HeaderText="CeCo" SortExpression="CeCo" />
            <asp:BoundField DataField="Importe (S/.)" HeaderText="Importe (S/.)" 
                SortExpression="Importe (S/.)" />
            <asp:BoundField DataField="Estado" HeaderText="Estado" 
                SortExpression="Estado" />
            <asp:CommandField SelectText="" ShowSelectButton="True" />
            <asp:BoundField DataField="codigo_cco" HeaderText="codigo_cco" 
                Visible="False" />
        </Columns>
        <FooterStyle Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#003399" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <AlternatingRowStyle BackColor="White" />
    </asp:GridView>
    </td></tr>
<tr><td colspan="2">
    
             <asp:GridView ID="gvDetallePedido" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" DataKeyNames="codigo_dpe,modoDistribucion_Dpe,codigo_dap,presupuestado" 
                        ForeColor="#333333" GridLines="None" Width="100%">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="descripcionart" HeaderText="Artículo" 
                                SortExpression="descripcionart" />
                            <asp:BoundField DataField="descripcion_cco" HeaderText="CeCo" 
                                SortExpression="descripcion_cco" />
                            <asp:BoundField DataField="precioreferencial_dpe" HeaderText="Precio" 
                                SortExpression="precioreferencial_dpe">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="cantidad_dpe" HeaderText="Cantidad" 
                                SortExpression="cantidad_dpe">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="subtotal" HeaderText="Subtotal" ReadOnly="True" 
                                SortExpression="subtotal">
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Observacion_dpe" HeaderText="Observación" 
                                SortExpression="Observacion_dpe" />
                            <asp:CheckBoxField DataField="presupuestado" HeaderText="Presup.">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:CheckBoxField>
                            <asp:BoundField DataField="descripcionEstado_Eped" HeaderText="Estado" />
                            <asp:CommandField ShowDeleteButton="True">
                                <ItemStyle ForeColor="#CC3300" HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField SelectText="Distribuir" ShowSelectButton="false">
                                <ItemStyle ForeColor="#0000CC" HorizontalAlign="Center" />
                            </asp:CommandField>
                            <asp:CommandField ShowEditButton="True">
                                <ItemStyle ForeColor="#009900" />
                            </asp:CommandField>
                             <asp:TemplateField HeaderText="Adjuntos">
                                    <ItemTemplate>
                                        <img id="AdjuntoProy" name="AdjuntoProy" src="../images/adjuntar.png" runat="server"
                                            style="width: 20px; height: 20px" alt="Adjuntar" title="Adjuntar Archivo" class="AdjuntoProy" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
    
                </td></tr>
<tr height="550px" class="" valign="top"><td class="contornotabla" colspan="2">
    <table width="100%">
    <tr>
    <td style="color: #0000FF">
        &nbsp;<tr>
    <td style="color: #0000FF">
        Pedido N°
      <asp:TextBox ID="txtPedido" runat="server" Enabled="False"></asp:TextBox>
                        &nbsp;Ver:
        <asp:LinkButton ID="lnkDatos" runat="server" ForeColor="#0000CC">Datos Generales</asp:LinkButton>
        |<asp:LinkButton ID="lnkRevisiones" runat="server" ForeColor="#0000CC">Revisiones</asp:LinkButton>
    </td>
    <td style="color: #0000FF" align="right">
        <asp:TextBox ID="txtDetPresup" 
                                        runat="server" Width="103px" Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtCodigoDpe" runat="server" Width="116px" Visible="False"></asp:TextBox>
        Proceso:
                    <asp:DropDownList ID="cboPeriodoPresu" runat="server" 
            Enabled="False">
                    </asp:DropDownList>
                &nbsp;Estado:<asp:DropDownList ID="cboEstado" runat="server" Enabled="False">
                    </asp:DropDownList>
    </td>
    </tr>
    <tr>
        <td style="color: #0000FF" colspan="2">
            Almacen&nbsp;&nbsp;
          <asp:TextBox ID="txtAlmacen" runat="server" Enabled="False" Width="210px"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td style="color: #0000FF" colspan="2">
                    <hr />
    </td>
    </tr>
    <tr>
    <td colspan="2">
        <asp:Panel ID="pnlDatos" name="pnlDatos"  runat="server" Visible="False">
            <asp:Label ID="lblTitTotal" runat="server" ForeColor="Red" 
                Text="Total del pedido (S/.):"></asp:Label>
            <asp:Label ID="lblTotalDetalle" runat="server" Font-Bold="True" 
                Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC" Text="0.00"></asp:Label>
            <br />
            <asp:Panel ID="pnlDistribuir" runat="server" ForeColor="#FF3300" 
                Visible="False">
                <br />
                Distribución de:
                <asp:Label ID="lblNombreItem" runat="server" Font-Size="10pt" 
                    ForeColor="#0033CC"></asp:Label>
                <br />
                <asp:Label ID="lblCeCoDist" runat="server" Text="Centro Costos"></asp:Label>
                <asp:CompareValidator ID="CompareValidator7" runat="server" 
                    ControlToValidate="cboCecosEjecucion" 
                    ErrorMessage="Seleccione el centro de costos a distribuir" 
                    Operator="GreaterThan" ValidationGroup="Distribuir" ValueToCompare="0">*</asp:CompareValidator>
                &nbsp;<asp:DropDownList ID="cboCecosEjecucion" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblModoDistribucion" runat="server" Font-Size="12px" 
                    ForeColor="Red"></asp:Label>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" 
                    ControlToValidate="txtCantidadDistribucion" 
                    ErrorMessage="Ingrese la distribución" ValidationGroup="Distribuir">*</asp:RequiredFieldValidator>
                &nbsp;<asp:TextBox ID="txtCantidadDistribucion" runat="server" Width="52px"></asp:TextBox>
                &nbsp;
                <asp:Button ID="cmdDistribuir" runat="server" BorderStyle="Outset" 
                    Text="Distribuir" ValidationGroup="Distribuir" />
                <asp:TextBox ID="txtCodigo_Ecc" runat="server" Visible="False" Width="26px"></asp:TextBox>
                <asp:TextBox ID="txtCodigoDetalle" runat="server" Visible="False" Width="26px"></asp:TextBox>
                <asp:GridView ID="gvDistribucion" runat="server" AutoGenerateColumns="False" 
                    CellPadding="4" DataKeyNames="codigo_ecc,codigo_Cco" ForeColor="#333333" 
                    GridLines="None" Width="90%">
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#E3EAEB" />
                    <Columns>
                        <asp:BoundField DataField="Descripcion_cco" HeaderText="CeCo" />
                        <asp:BoundField DataField="cantidad" HeaderText="Distribución">
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="modoDistribucion_Dpe" HeaderText="Modo">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Precio" HeaderText="Precio">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:CommandField>
                        <asp:CommandField SelectText="Editar" ShowSelectButton="True" />
                    </Columns>
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                Total:
                <asp:Label ID="lblTotalItem" runat="server" Font-Bold="True" Font-Size="10pt" 
                    Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Distribuido:
                <asp:Label ID="lblDistribuidoItem" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True" ForeColor="#0033CC">0.00</asp:Label>
                &nbsp;Por Distribuir:
                <asp:Label ID="lblPorDistribuir" runat="server" Font-Bold="True" 
                    Font-Size="10pt" Font-Underline="True"></asp:Label>
                <br />
                <div>
                </div>
                <asp:TextBox ID="txtCodItem" runat="server" Visible="False"></asp:TextBox>
                <asp:HiddenField ID="hddForzar" runat="server" Value="0" />
            </asp:Panel>
            <hr />
                    <table border="0" cellpadding="2" cellspacing="0" style="width:100%;"  >
                        <tr>
                            <td align="left">
                                Centro de costo <asp:RequiredFieldValidator ID="RequiredFieldValidator4" 
                                    runat="server" ControlToValidate="cboCecos" 
                                    ErrorMessage="Seleccione centro de costos" ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                    ControlToValidate="cboCecos" ErrorMessage="Seleccione centro de costos" 
                                    Operator="GreaterThan" ValidationGroup="Presupuesto" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCecos" runat="server" BackColor="#F3F3F3" Visible="False" 
                                    Width="90px"></asp:TextBox>
                                <asp:DropDownList ID="cboCecos" runat="server" AutoPostBack="True" Enabled="false" style="width:100%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td> &nbsp;</td>
                            <td>
                                <asp:LinkButton ID="lnkBusquedaAvanzada" runat="server" ForeColor="Blue" 
                                    Visible="False">Busqueda Avanzada</asp:LinkButton>
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" 
                                    AssociatedUpdatePanelID="UpdatePanel2">
                                    <ProgressTemplate>
                                        <font style="color:Blue">Procesando. Espere un momento...</font>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="Panel3" runat="server" Height="150px" ScrollBars="Vertical" 
                                    Width="100%">
                                    <asp:GridView ID="gvCecos" runat="server" AutoGenerateColumns="False" 
                                        BorderColor="#628BD7" BorderStyle="Solid" BorderWidth="1px" CellPadding="4" 
                                        DataKeyNames="codigo_cco" ForeColor="#333333" ShowHeader="False" Width="98%">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo_cco" HeaderText="Código" />
                                            <asp:BoundField DataField="nombre" HeaderText="Centro de costos" />
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron items con el término de búsqueda</b>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2">
                                <asp:Panel ID="pnlPresupuesto" runat="server" ForeColor="Red" 
                                    HorizontalAlign="Left" Visible="False">
                                    Seleccione el ítem presupuestado que desea pedir:<asp:GridView ID="gvPresupuesto" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" DataKeyNames="codigo_dpr,codigo_Ppr,codigo_Art" 
                                        ForeColor="#333333" GridLines="Horizontal" Width="90%">
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_Ppr" HeaderText="Prog. Presupuestal" />
                                            <asp:BoundField DataField="DesEstandar" HeaderText="Item" />
                                            <asp:BoundField DataField="DetDescripcion" HeaderText="Detalle Item" />
                                            <asp:BoundField DataField="PreUnitario" HeaderText="Precio Unit.">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Cantidad" HeaderText="Cantidad">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Disponible" HeaderText="Disponible">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
            <br />

                    <table border="0" cellpadding="2" cellspacing="0" width="100%">
                      <tr bgcolor="#F5F9FC">
                            <td>
                                <%--Prog. presupuestal--%><asp:CompareValidator ID="CompareValidator6" runat="server" 
                                    ControlToValidate="cboProgramaPresu" ErrorMessage="Seleccione programa presupuestal" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboProgramaPresu" runat="server" Visible="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                      <tr>
                        <td colspan="2"><asp:CheckBox runat="server"  ID="chkNoPresupuestado" Text="Item No Presupuestado" Font-Bold="true" AutoPostBack="true" /></td>
                      </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                Actividad<asp:CompareValidator ID="CompareValidator1" runat="server" 
                                    ControlToValidate="cboDetalleActividadPOA" ErrorMessage="Seleccione Actividad" 
                                    Operator="GreaterThan" ValidationGroup="Guardar" ValueToCompare="0">*</asp:CompareValidator>
                            </td>
                            <td>
                                <asp:DropDownList ID="cboDetalleActividadPOA" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:RadioButtonList ID="rblMovimiento" runat="server" AutoPostBack="True" 
                                    RepeatDirection="Horizontal" ValidationGroup="BuscaItem" Visible="false">
                                    <asp:ListItem Value="I">Ingreso</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="E">Egreso</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="Panel4" runat="server">
                                    Item<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                        ControlToValidate="txtCodItem" 
                                        ErrorMessage="Busque el item que desea registrar" ValidationGroup="BuscaItem">*</asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" 
                                        ControlToValidate="txtConcepto" ErrorMessage="Selecione item a registrar" 
                                        ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" 
                                        AssociatedUpdatePanelID="UpdatePanel2">
                                        <ProgressTemplate>
                                            <font style="color:Blue">Procesando. Espere un momento...</font>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConcepto" runat="server" ValidationGroup="BuscaItem" 
                                    Width="500px"></asp:TextBox>
                                <%--<asp:ImageButton ID="ImgBuscarItems" runat="server" 
                                    ImageUrl="~/images/busca.gif" ValidationGroup="BuscaItem" />--%>
                                    
                                            <asp:LinkButton ID="LinkButton3" runat="server" 
                        ForeColor="Blue" ValidationGroup="BuscaItem" ><img style="height:auto;width:auto;" src="../images/busca.gif"alt="Buscar" /></asp:LinkButton>
                                (clic aquí)</td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Panel ID="Panel1" runat="server" Height="150px" ScrollBars="Vertical" 
                                    Width="100%">
                                    <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" 
                                        CellPadding="4" DataKeyNames="codigocon,tipo,iduni,especificaCantidad" 
                                        ForeColor="Black" Width="98%" BorderColor="#CCCCCC" BorderStyle="None" 
                                        BorderWidth="1px" ShowHeader="False" BackColor="White" 
                                        GridLines="Horizontal">
                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                        <Columns>
                                            <asp:BoundField DataField="codigo" HeaderText="Código" />
                                            <asp:BoundField DataField="concepto" HeaderText="Concepto" />
                                            <asp:BoundField DataField="unidad" HeaderText="Unidad" />
                                            <asp:BoundField DataField="precio" HeaderText="Precio" >
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:CommandField ShowSelectButton="True" />
                                        </Columns>
                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron items con el término de búsqueda</b>
                                        </EmptyDataTemplate>
                                        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td width="15%">
                                Detalle/Justificación
                            </td>
                            <td>
                                <asp:TextBox ID="txtComentarioReq" runat="server" MaxLength="100" 
                                    TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                                    <td width="15%">
                                        Especificaciones técnicas</td>
                                    <td>
                                        <asp:TextBox ID="txtEspecificaciones" runat="server" MaxLength="100" 
                                            TextMode="MultiLine" Width="90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                    <span runat="server" id="spAdjunto">
                                        <input type="file" id="File1" name="txtfile" class="form-control" runat="server" style=" width:50%; display :inline-block;" onchange ="guardarFile()"  />
                                        <a href="#" onclick="ClearFile();return false;">
                                        <img id="imgEliminar" alt="Link to this page" border="0" src="../images/delete.png" style=" display :inline-block;" ></img></a> 
                                    </span>
                                       <%--<img src="../images/attachment.png" /><asp:HyperLink  ID ="hlAdjunto" runat ="server" Text="" Enabled ="false" ></asp:HyperLink></span>--%>
                                        <%--<asp:FileUpload ID="fuCargarArchivo" runat="server" size="50" ContentEditable="False" ToolTip ="Se admiten archivos .doc,.xls,.docx,.xlsx,.jpg,.png,.pdf,.zip  Tam. máx 4MB" />
                                        <a href="#" onclick="ClearFile();return false;" ><img src="../images/delete.png" border="0" alt="Link to this page"></a> --%>
                                        &nbsp;<asp:RegularExpressionValidator ID="revUpload" runat="server" ErrorMessage="Formato de archivo incorrecto" ControlToValidate="File1" ValidationExpression= "^.+(.zip|.ZIP|.doc|.docx|.xls|.xlsx|.jpg|.png|.pdf)$" ValidationGroup ="Guardar" />
                                     
                                    </td>
                                </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                            </td>
                            <td>
                                <asp:TextBox ID="lblUnidad" runat="server" BackColor="#F3F3F3" ReadOnly="True" 
                                    Visible="false" Width="90px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                <asp:Label ID="lblTexto" runat="server" Text="Precio Unitario (S/.)"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                                    ControlToValidate="txtPrecioUnit" ErrorMessage="Ingrese un precio válido" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPrecioUnit" runat="server" Width="90px"></asp:TextBox>
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:Label ID="lblValores" runat="server" Font-Bold="True" Text="Cantidad"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;
                                &nbsp;
                                &nbsp;
                                <asp:TextBox ID="txtCantidad" runat="server" Width="90px"></asp:TextBox>
                                
                            </td>
                        </tr>
                       <%-- <tr>
                            <td>
                                <asp:Label ID="lblValores" runat="server" Font-Bold="True" Text="Cantidad"></asp:Label>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                                    ControlToValidate="txtCantidad" ErrorMessage="Ingrese una cantidad válida" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCantidad" runat="server" Width="90px"></asp:TextBox>
                                
                            </td>
                        </tr>--%>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                Fecha</td>
                            <td>
                                <asp:TextBox ID="TxtFechaEsperada" runat="server" Width="80px"></asp:TextBox>
                                <input ID="Button2" class="cunia" 
                                    onClick="PopCalendar.selectWeekendHoliday(1,1);PopCalendar.show(document.form1.TxtFechaEsperada,'dd/mm/yyyy')" 
                                    style="height: 22px" type="button" /><asp:RequiredFieldValidator 
                                    ID="RequiredFieldValidator11" runat="server" 
                                    ControlToValidate="TxtFechaEsperada" ErrorMessage="Ingrese fecha a Despachar" 
                                    ValidationGroup="Guardar">*</asp:RequiredFieldValidator>
                                &nbsp;</td>
                        </tr>
                        <tr bgcolor="#F5F9FC">
                            <td>
                                &nbsp;</td>
                            <td>
                                <asp:RadioButtonList ID="rblModoDistribucion" runat="server" 
                                    AutoPostBack="True" RepeatDirection="Horizontal" 
                                    ValidationGroup="BuscaItem" Visible="False">
                                    <asp:ListItem Selected="True" Value="C">Cantidad</asp:ListItem>
                                    <asp:ListItem Value="P">Porcentaje</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
            <%--<asp:Button ID="cmdGuardar" runat="server" BorderStyle="Outset" 
                CssClass="guardar" Height="26px" Text="   Guardar detalle"
                ValidationGroup="Guardar" Width="148px" CausesValidation ="true" OnClientClick =" if (Page_ClientValidate()) {if(document.getElementById('hlAdjunto').innerHTML !='' && document.getElementById('fuCargarArchivo').value!='' ){ return confirm('Se reemplazará el archivo adjunto existente. ¿Desea continuar?');}}"/>--%>
            <asp:Button ID="cmdGuardar" runat="server" BorderStyle="Outset" 
                CssClass="guardar" Height="26px" Text="   Guardar detalle"
                ValidationGroup="Guardar" Width="148px" CausesValidation ="true" UseSubmitBehavior ="false" />
            <br />
            <br />
        </asp:Panel>
        <asp:Panel ID="pnlRevision" runat="server" Visible="False">
        <table width="100%">
        <tr>
        <td width="50%" valign="top">
            <asp:GridView ID="gvRevisiones" runat="server" Caption="Revisiones del pedido" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        <td width="50%" valign="top">
            <asp:GridView ID="gvEstados" runat="server" Caption="Evolución del estado" 
                CaptionAlign="Left" CellPadding="4" ForeColor="#333333" GridLines="None" 
                Width="95%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            </td>
        </table>
            <br />
            <asp:GridView ID="gvObservaciones" runat="server" 
                Caption="Observaciones al pedido" CaptionAlign="Left" CellPadding="4" 
                ForeColor="#333333" GridLines="None" Width="97.5%">
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#E3EAEB" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF3300" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
            <br />
        </asp:Panel>
    </td>
    </tr>
    </table>
    </td></tr>
    
</table>

                </contenttemplate>
    </asp:UpdatePanel>
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
                            Subir Archivos</h4>
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
                            <table style="width: 100%;" class="display dataTable">
                                <thead>
                                    <tr>
                                        <th style="width: 10%">
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
                        <div class="row" id="div1">
                            <%--<div class="form-group" id="divAdjunto">
                                <label class="col-sm-3 control-label" id="lblAdjunto">
                                    Adjunto:</label>
                                <div class="col-sm-8">
                                    <input type="file" id="txtfile" name="txtfile" class="form-control" runat="server" />
                                </div>
                                <div style="float: left;" id="divLoading" class="hidden">
                                    <img id="imgload" src="../assets/images/loading.GIF"></div>
                            </div>--%>
                        </div>
                        </form>
                    </div>
                    <div class="row" id="div2">
                        <%--<div class="modal-footer" id="divfooter">
                            <center>
                                <button type="button" id="btnGuardar" class="btn btn-primary" onclick="fnGuardar();">
                                    Guardar</button>
                                <button type="button" class="btn btn-danger" id="btnCancelarReg" data-dismiss="modal">
                                    Cancelar</button>
                            </center>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
