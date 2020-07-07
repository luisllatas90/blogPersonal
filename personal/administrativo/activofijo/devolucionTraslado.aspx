<%@ Page Language="VB" AutoEventWireup="false" CodeFile="devolucionTraslado.aspx.vb" 
    Inherits="administrativo_activofijo_devolucionTraslado" EnableEventValidation="false" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=ISO-8859-1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Devolucion de Traslado de Activo Fijo</title>
    
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
	<meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0"/> 
	
	<script type="text/javascript" src="../../assets/js/jquery.js"></script>
	
	<script type="text/javascript" src="../../assets/js/bootstrap.min.js"></script>	
	
    <script type="text/javascript" src='../../assets/js/jquery-ui-1.10.3.custom.min.js'></script>    
          
    <link rel="stylesheet" type="text/css" href="../../assets/css/bootstrap.min.css"/>
    
    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        var lstPersonal;
        var codigo_per;
        var nombre_per;
        var email_per;

        var lstUbicacion;
        var codigo_uba;
        var nombre_uba;

        var lstCentroCosto;
        var codigo_cco;
        var nombre_cco;

        jQuery(document).ready(function() {
            $('#btnGrabar').click(function() {
                if ($('#txtFecha').val() == '') {
                    alert("Ingrese Fecha de Solicitud");
                    $('#txtFecha').focus();
                    return false;
                }
                if ($('#txtAsignado').val() == '' || $('#hdCodPer').val() == '') {
                    alert("Ingrese Persona Asignada");
                    $('#txtAsignado').focus();
                    return false;
                }
                if ($('#txtCentroCosto').val() == '' || $('#hdCcoPer').val() == '') {
                    alert("Ingrese Centro Costo");
                    $('#txtCentroCosto').focus();
                    return false;
                }
                if ($('#txtUbicacion').val() == '' || $('#hdCodUba').val() == '') {
                    alert("Ingrese Ubicacion");
                    $('#txtUbicacion').focus();
                    return false;
                }
                if ($('#txtObservacion').val() == '') {
                    alert("Ingrese Observacion");
                    $('#txtObservacion').focus();
                    return false;
                }
            });

            lstPersonal = fnCargarLista("lstPersonal");
            var jsonString = JSON.parse(lstPersonal);

            $('#txtAsignado').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.d_des;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonString.filter(function(value) {
                        return value.d_des == ui.item.value;
                    });
                    codigo_per = selectedItem[0].d_id;
                    nombre_per = selectedItem[0].d_des;
                    email_per = selectedItem[0].u_des;
                    codigo_cco = selectedItem[0].c_id;
                    nombre_cco = selectedItem[0].c_des;
                    $('#hdCodPer').val(codigo_per);
                    $('#txtCentroCosto').val(nombre_cco);
                    $('#hdCcoPer').val(codigo_cco);
                    $('#hdEmaPer').val(email_per + '@usat.edu.pe;');
                },
                minLength: 3,
                delay: 100
            });

            $('#txtAsignado').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    codigo_per = "";
                    nombre_per = "";
                    email_per = "";
                    codigo_cco = "";
                    nombre_cco = "";
                }
            });

            lstUbicacion = fnCargarLista("lstUbicacion");
            var jsonStringU = JSON.parse(lstUbicacion);

            $('#txtUbicacion').autocomplete({
                source: $.map(jsonStringU, function(item) {
                    return item.d_des;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonStringU.filter(function(value) {
                        return value.d_des == ui.item.value;
                    });
                    codigo_uba = selectedItem[0].d_id;
                    nombre_uba = selectedItem[0].d_des;
                    $('#hdCodUba').val(codigo_uba);
                },
                minLength: 3,
                delay: 100
            });

            $('#txtUbicacion').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    codigo_uba = "";
                    nombre_uba = "";
                }
            });

            lstCentroCosto = fnCargarLista("LstCentroCo");
            var jsonStringC = JSON.parse(lstCentroCosto);

            $('#txtCentroCosto').autocomplete({
                source: $.map(jsonStringC, function(item) {
                    return item.d_des;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonString.filter(function(value) {
                        return value.d_des == ui.item.value;
                    });
                    codigo_cco = selectedItem[0].d_id;
                    nombre_cco = selectedItem[0].d_des;
                    $('#hdCcoPer').val(codigo_cco);

                },
                minLength: 3,
                delay: 100
            });

            $('#txtCentroCosto').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    codigo_cco = "";
                    nombre_cco = "";
                }
            });

        });

        function openModal() {
            $('#myModal').modal('show');
            //$('#txtFecha').val('');
            $('#txtObservacion').val('');
        }

        function closeModal() {
            $('#hdCodPer').val('');
            $('#hdCcoPer').val('');
            $('#hdCodUba').val('');
            $('#myModal').modal('hide');
        }

        function openModal2() {
            $('#myModalDet').modal('show');
        }

        function ShowMessage(message, messagetype) {
            var cssclss;
            switch (messagetype) {
                case 'Success':
                    cssclss = 'alert-success'
                    break;
                case 'Error':
                    cssclss = 'alert-danger'
                    break;
                case 'Warning':
                    cssclss = 'alert-warning'
                    break;
                default:
                    cssclss = 'alert-info'
            }
            if (cssclss != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }

        function fnCargarLista(param) {
            try {
                var arr;
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "../../DataJson/activofijo/processactivofijo.aspx",
                    data: { "param0": param },
                    async: false,
                    cache: false,
                    success: function(data) {
                        arr = data;
                    },
                    error: function(result) {
                        arr = null;
                    }
                })
                return arr;
            }
            catch (err) {
                console.log('error');
            }
        }
    
    </script>
    
    <style type="text/css">
    
        .ui-widget-content {
            border: 1px solid #dddddd;
            background: #eeeeee url(images/ui-bg_highlight-soft_100_eeeeee_1x100.png) 50% top repeat-x;
            color: #333333;
        }
        .ui-widget {
            font-family: Trebuchet MS,Tahoma,Verdana,Arial,sans-serif;
            font-size: 1.1em;
        }
        .ui-menu {
            list-style: none;
            padding: 2px;
            margin: 0;
            display: block;
            outline: none;
        }
        .ui-autocomplete {
            position: absolute;
            top: 0;
            left: 0;
            cursor: default;
        }
        .ui-front {
            z-index: 100;
        }
        .ui-menu .ui-menu-item {
            margin: 0;
            padding: 0;
            width: 100%;
            list-style-image: url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7);
        }
        .ui-menu .ui-menu-item a {
            text-decoration: none;
            display: block;
            padding: 2px .4em;
            line-height: 1.5;
            min-height: 0;
            font-weight: normal;
        }
        .ui-widget-content a {
            color: #333333;
        }
        .ui-helper-hidden-accessible {
            border: 0;
            clip: rect(0 0 0 0);
            height: 1px;
            margin: -1px;
            overflow: hidden;
            padding: 0;
            position: absolute;
            width: 1px;
        }
        
    </style>
       
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <asp:HiddenField ID="hdCodUser" runat="server"/>
    <asp:HiddenField ID="hdNomUser" runat="server" />
    <asp:HiddenField ID="hdEmaUser" runat="server" />
    <asp:HiddenField ID="hdCcoUser" runat="server" />
    <asp:HiddenField ID="hdCodPer" runat="server" />
    <asp:HiddenField ID="hdCcoPer" runat="server" />
    <asp:HiddenField ID="hdEmaPer" runat="server" />
    <asp:HiddenField ID="hdCodUba" runat="server" />
    <asp:HiddenField ID="hdCodTraslado" runat="server" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel-heading">
                Devoluci&oacute;n de Traslado de Activo Fijo
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Nro Traslado:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtNroTraslado" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info"/>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvTraslado" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_tld,codigo_per,responsable,codigo_Cco,descripcion_Cco,cod_estado,observacion_tld" 
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="VER">
                                    <ItemTemplate>
                                        <asp:Button ID="btnVer" runat="server" Text="Ver" OnClick="btnVer_Click" CommandName="Ver" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-info btn-sm" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="nro_tld" HeaderText="Codigo"/>
                                <asp:BoundField DataField="fecha_tld" HeaderText="Fecha"/>
                                <asp:BoundField DataField="tipomov_tld" HeaderText="Tipo"/>
                                <asp:BoundField DataField="codigo_per" Visible="false"/>
                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="codigo_Cco" Visible="false" />
                                <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costo" />
                                <asp:BoundField DataField="observacion_tld" HeaderText="Observacion" />
                                <asp:BoundField DataField="estado_tld" HeaderText="Estado" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACION">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDevolver" runat="server" Text="Devolver" OnClick="btnDevolver_Click" CommandName="Devolver" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-success btn-sm" 
                                            OnClientClick="return confirm('¿Desea devolver este traslado?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros!
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                            <RowStyle Font-Size="11px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
     <!-- Modal Registro de Traslado -->
    <div id="myModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel" style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalTraslado">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registro de Traslado</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group"> 
                                <label class="col-md-3">Fecha:</label>
                                <div class="col-md-4">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                        <span class="input-group-addon bg">
                                            <i class="ion ion-ios-calendar-outline"></i>
                                        </span>
                                    </div>
                                </div>   
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Asignada a:</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtAsignado" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Centro Costo:</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtCentroCosto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Ubicaci&oacute;n de Destino:</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtUbicacion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Observaci&oacute;n:</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">Salir</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" CssClass="btn btn-info" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Detalle de Traslado -->
    <div id="myModalDet" class="modal fade" role="dialog" aria-labelledby="myModalDetLabel" style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalDetalle">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Detalle de Traslado</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvDetalle" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                                DataKeyNames="codigo_af" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="etiqueta_af" HeaderText="N° Etiqueta"/>
                                    <asp:BoundField DataField="descripcionArt" HeaderText="Descripcion" />
                                    <asp:BoundField DataField="Serie" HeaderText="Serie" />
                                    <asp:BoundField DataField="Marca" HeaderText="Marca" />
                                    <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                    <asp:BoundField DataField="Procedencia" HeaderText="Procedencia" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Registros!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="11px"/>
                                <RowStyle Font-Size="10px"/>
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"/>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalir2" class="btn btn-danger" data-dismiss="modal">Salir</button>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
