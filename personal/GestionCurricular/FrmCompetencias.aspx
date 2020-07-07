<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCompetencias.aspx.vb"
    Inherits="GestionCurricular_FrmCompetencias" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Competencias de Aprendizaje</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnAceptar').click(function() {
                if ($('#ddlTipoCompetencia').val() == '' || $('#ddlTipoCompetencia').val() == '-1') {
                    alert("Seleccione el tipo de Competencia");
                    $('#ddlTipoCompetencia').focus();
                    return false;
                }

                if ($('#ddlCategoria').val() == '' || $('#ddlCategoria').val() == '-1') {
                    alert("Seleccione la Categoría para el Competencias");
                    $('#ddlCategoria').focus();
                    return false;
                }

                if ($('#txtNombre').val() == '') {
                    alert("Ingrese un Nombre de la Competencia");
                    $('#txtNombre').focus();
                    return false;
                }
            });
        });

        function openModal(acc, tip, cat) {
            $('#myModal').modal('show');

            if (acc == "nuevo") {
                $('#hdCodigoCompetencia').val('');
                $('#txtNombre').val('');
            } else {
                $('#ddlTipoCompetencia').val(tip);
                $('#ddlCategoria').val(cat);
            }
        }

        function closeModal() {
            $('#hdCodigoCompetencia').val('');
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
            $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert ' + cssclss + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Competencias -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registrar Competencias de Aprendizaje</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-2">
                        <asp:Button ID="btnListar" runat="server" Text="Refrescar" CssClass="btn btn-info" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar Competencia" CssClass="btn btn-success" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvCompetencia" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_tcom, codigo_cat, nombre_com" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_com" HeaderText="CÓDIGO" />
                                <asp:BoundField DataField="tipo" HeaderText="TIPO" />
                                <asp:BoundField DataField="categoria" HeaderText="CATEGORÍA" />
                                <asp:BoundField DataField="nombre_com" HeaderText="NOMBRE" />
                                <asp:BoundField HeaderText="ACCIÓN" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron competencias
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
                            <RowStyle Font-Size="11px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Competencias -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Competencias</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form_group">
                                <label class="col-xs-3">
                                    Tipo de Competencia:</label>
                                <div class="col-xs-9">
                                    <asp:DropDownList ID="ddlTipoCompetencia" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form_group">
                                <label class="col-xs-3">
                                    Categoría:</label>
                                <div class="col-xs-9">
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12">
                            <div class="form-group">
                                <label class="col-xs-3">
                                    Nombre:</label>
                                <div class="col-xs-9">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Style="text-transform: uppercase"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" Text="Guardar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdCodigoCompetencia" runat="server" />
    </form>
</body>
</html>
