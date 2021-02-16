<%@ Page Language="VB" AutoEventWireup="false" CodeFile="solicitarBajaActivoFijo.aspx.vb" 
Inherits="administrativo_activofijo_solicitarBajaActivoFijo" EnableEventValidation="false" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
    <meta name="viewport", content="width=device-width, inicial-scale=1" />
    <title>Solicitud de Baja Activo Fijo</title>
    
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
        var codigo_per_para;
        var codigo_per_de;
        var cargo_para;
        var cargo_de;
        var cco_de;
        var email_para;
        var email_de;

        jQuery(document).ready(function() {

            $('#btnGenerar').click(function() {
                var DeNombre = $('#hdUserNom').val();
                var DeCodigo = $('#hdUserCod').val();
                var DeCargo = $('#hdUserCgo').val();
                var DeCco = $('#hdUserCco').val();
                $('#txtDe').val(DeNombre);
                $('#hdCodPerDe').val(DeCodigo);
                $('#hdCargoDe').val(DeCargo);
                $('#hdCcoDe').val(DeCco);
            });

            $('#btnGrabar').click(function() {
                if ($('#txtPara').val() == '' || $('#hdCodPerPara').val() == '') {
                    alert("Ingrese Para quien va la solicitud");
                    $('#txtPara').focus();
                    return false;
                }
                if ($('#txtDe').val() == '' || $('#hdCodPerDe').val() == '') {
                    alert("Ingrese De quien envia la solicitud");
                    $('#txtDe').focus();
                    return false;
                }
                if ($('#txtFecha').val() == '') {
                    alert("Ingrese Fecha de Solicitud");
                    $('#txtFecha').focus();
                    return false;
                }
                if ($('#txtAsunto').val() == '') {
                    alert("Ingrese Asunto de Solicitud");
                    $('#txtAsunto').focus();
                    return false;
                }
            });

            lstPersonal = fnCargarLista("lstPersonal");
            var jsonString = JSON.parse(lstPersonal);

            $('#txtPara').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.d_des;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonString.filter(function(value) {
                        return value.d_des == ui.item.value;
                    });
                    codigo_per_para = selectedItem[0].d_id;
                    cargo_para = selectedItem[0].x_des;
                    email_para = selectedItem[0].u_des;
                    $('#hdCodPerPara').val(codigo_per_para);
                    $('#hdCargoPara').val(cargo_para);
                    $('#hdEmailPara').val(email_para + '@usat.edu.pe');
                },
                minLength: 3,
                delay: 100
            });

            $('#txtPara').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    codigo_per_para = "";
                    cargo_para = "";
                    email_para = "";
                }
            });

            $('#txtDe').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.d_des;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonString.filter(function(value) {
                        return value.d_des == ui.item.value;
                    });
                    codigo_per_de = selectedItem[0].d_id;
                    cargo_de = selectedItem[0].x_des;
                    cco_de = selectedItem[0].c_id;
                    email_de = selectedItem[0].u_des;
                    $('#hdCodPerDe').val(codigo_per_de );
                    $('#hdCargoDe').val(cargo_de);
                    $('#hdCcoDe').val(cco_de);
                    $('#hdUserEml').val(email_de + '@usat.edu.pe;');
                },
                minLength: 3,
                delay: 100
            });

            $('#txtDe').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    codigo_per_de = "";
                    cargo_de = "";
                    cco_de = "";
                    email_de = "";
                }
            });
        });

        function openModal() {
            $('#myModal').modal('show');
            $('#txtPara').val('');
            //$('#txtFecha').val('');
            $('#txtAsunto').val('');
        }

        function closeModal() {
            $('#hdCodPerPara').val('');
            $('#myModal').modal('hide');
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
    <asp:HiddenField ID="hdCodPerPara" runat="server" />
    <asp:HiddenField ID="hdCodPerDe" runat="server" />
    <asp:HiddenField ID="hdUserCod" runat="server" />
    <asp:HiddenField ID="hdUserNom"  runat="server" />
    <asp:HiddenField ID="hdCargoPara" runat="server" />
    <asp:HiddenField ID="hdCargoDe" runat="server" />
    <asp:HiddenField ID="hdUserCgo" runat="server" />
    <asp:HiddenField ID="hdUserCco" runat="server" />
    <asp:HiddenField ID="hdCcoDe" runat="server" />
    <asp:HiddenField ID="hdUserEml" runat="server" />
    <asp:HiddenField ID="hdEmailPara" runat="server" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                Solicitud de Baja de Activo Fijo
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">N� Etiqueta:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtEtiqueta" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnGenerar" runat="server" Text="Generar Solicitud" CssClass="btn btn-success" 
                        OnClientClick="return confirm('�Desea generar solicitud de baja?');" />
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvActivoFijo" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_af,etiqueta_af,descripcionArt,Marca,Modelo,Serie" CssClass="table table-bordered">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selecionar">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelec" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_af" HeaderText="Codigo" />
                                <asp:BoundField DataField="etiqueta_af" HeaderText="Etiqueta" />
                                <asp:BoundField DataField="descripcionArt" HeaderText="Descripcion" />
                                <asp:BoundField DataField="Marca" HeaderText="Marca"/>
                                <asp:BoundField DataField="Modelo" HeaderText="Modelo" />
                                <asp:BoundField DataField="Serie" HeaderText="Serie"/>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron Datos!
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px"/>
                            <RowStyle Font-Size="11px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro de Solicitud de Baja -->
    <div id="myModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel"
                        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalSolicitud">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registro de Solicitud</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group"> 
                                <label class="col-md-1">Fecha:</label>
                                <div class="col-md-3">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                        <span class="input-group-addon bg">
                                            <i class="ion ion-ios-calendar-outline"></i>
                                        </span>
                                    </div>
                                </div>   
                                <label class="col-md-1">Asunto:</label>
                                <div class="col-md-7">
                                    <asp:TextBox ID="txtAsunto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-1">Para:</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtPara" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                                <label class="col-md-1">De:</label>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtDe" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvSolicitud" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                                DataKeyNames="codigo_af,etiqueta,descripcion" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="codigo_af" Visible="false"/>
                                    <asp:BoundField DataField="etiqueta" HeaderText="Codigo" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion" HeaderStyle-Width="40%"/>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Recomendaciones">
                                        <ItemTemplate>
                                            <asp:RadioButtonList ID="rbRecomendacion" runat="server">
                                                <asp:ListItem class="radio-inline" Value="L" Text="Licitaci�n"></asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="D" Text="Donaci�n"></asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="E" Text="Eliminaci�n"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="rfvRecomencacion" runat="server" ControlToValidate="rbRecomendacion">
                                            * Seleccione una Recomendaci&oacute;n
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Observaciones">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvObservacion" runat="server" ControlToValidate="txtObservacion">
                                            * Ingrese Observaci&oacute;n
                                            </asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="11px"/>
                                <RowStyle Font-Size="10px" />
                            </asp:GridView>
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
    </form>
</body>
</html>
