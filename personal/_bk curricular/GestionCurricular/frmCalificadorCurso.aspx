<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCalificadorCurso.aspx.vb"
    Inherits="GestionCurricular_frmCalificadorCurso" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Calificador Asignatura</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src='../assets/js/jquery-ui-1.10.3.custom.min.js' type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function StartCount() {
            var t = 120 * 1000;
            var x = setInterval(function() {
                t -= 1000;
                $("#contador").html((t / 1000) + " seg");

                if (t <= 0) {
                    clearInterval(x);
                    $("#contador").html("Expiró");
                }
            }, 1000);
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

        function soloNumeros(e, txt) {
            var key = window.Event ? e.which : e.keyCode
            var artxt = txt.id.split("_")
            var nro = "";
            var nrox = 0;
            if (key == 13) {
                nro = artxt[1].substr(3)
                nrox = parseInt(nro) + 1;
                nro = nrox.toString();
                if (nro.length == 1) {
                    nro = "0" + nro;
                }
                $('#' + artxt[0] + "_" + "ctl" + nro + "_" + artxt[2]).focus();
                console.log(nro);
                console.log('#' + artxt[0] + "_" + "ctl" + nro + "_" + artxt[2]);
                return true;
            }
            return (key >= 48 && key <= 57)
        }

        function soloCalificacion(txt) {
            var nota = parseInt(txt.value)
            if (nota > 20) {
                $('#' + txt.id).val(0);
            }
            if (nota < 14) {
                txt.style.color = 'red';
            }
            else {
                txt.style.color = 'blue';
            }
        }

        function openModal() {
            $('#txtToken').val("");
            $('#txtToken').focus();
            $('#myModalSMS').modal('show');
        }

        function closeModal() {
            $('#txtToken').val("");
            $('#myModalSMS').modal('hide');
        }

        function showDivs(acc) {
            if ($('#divTexto').is(':hidden')) {
                $('#divTexto').show('slide', { direction: 'left' }, 1000);
                $('#divEnviar').show('slide', { direction: 'left' }, 1000);
            } else {
                $('#divTexto').hide('slide', { direction: 'left' }, 1000);
                $('#divEnviar').hide('slide', { direction: 'left' }, 1000);
            }

            if (acc == "hide") {
                $('#divTexto').hide();
                $('#divEnviar').hide();
            }
        }

        function onChangeToken() {
            $('#divAlertModal').hide();
            $('#lblMensaje').val('');
        }
    </script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-left"></i></span> Volver
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-11">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Asignatura:</label></h4>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-1">
                        &nbsp;
                    </div>
                    <div class="col-md-4" id="divTexto" runat="server" style="color: red; font-size: smaller;">
                        *Para proceder, un código será enviado por SMS al(los) número(s):
                    </div>
                    <div class="col-md-7">
                        &nbsp;
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-1" style="text-align: left">
                        <asp:LinkButton ID="btnEnviar" runat="server" CssClass="btn btn-success" Text='<i class="fa fa-check"></i> Aceptar'
                            OnClientClick="showDivs(''); return false;" autopostback="false" />
                        <asp:LinkButton ID="btnGuardar" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('¿Desea guardar la información?');">
                            <span><i class="fa fa-save"></i></span> Guardar
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-3" id="divEnviar" runat="server">
                        <div class="input-group">
                            <asp:Label ID="lblTelefono" runat="server" CssClass="form-control form-control-sm"
                                Style="color: #a76f80;"></asp:Label>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btnSend" runat="server" CssClass="btn btn-info input-group-text"
                                    Text='<i class="fa fa-paper-plane"></i> Enviar' Visible="true" Enabled="false">
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnGenerar" runat="server" CssClass="btn btn-info input-group-text"
                                    Text='<i class="fa fa-paper-plane"></i> Enviar' Visible="false" Enabled="true">
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8" style="text-align: right; float: right">
                        <div class="form-group" id="divLeyenda" runat="server" style="float: right;">
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                DataKeyNames="codigo_Dma, nombre_alu" OnRowDataBound="gvNotas_OnRowDataBound"
                                OnRowCreated="gvNotas_OnRowCreated" CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" HeaderStyle-Width="5%" />
                                    <asp:BoundField DataField="nombre_alu" HeaderText="Apellidos y Nombres" HeaderStyle-Width="25%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" Font-Bold="true" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <%--<PagerStyle ForeColor="#003399" HorizontalAlign="Center" />--%>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal SMS -->
    <div id="myModalSMS" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-md">
            <div class="modal-content" id="Div1">
                <div class="modal-header" style="background-color: #D9534F; color: White; font-weight: bold;
                    font-size: 16px;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <span class="modal-title">Confirmar Token de seguridad enviado por SMS</span>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:HiddenField ID="validar" runat="server" />
                            <div class="messagealert" id="divAlertModal" runat="server" visible="false">
                                <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                    class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                        id="lblMensaje" runat="server"></span>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                            <label for="txtToken" class="form-control input-sm no-border">
                                Ingrese el Token:
                            </label>
                        </div>
                        <div class="col-sm-8">
                            <div class="input-group">
                                <asp:TextBox ID="txtToken" runat="server" CssClass="form-control" Style="text-transform: uppercase"
                                    onKeyPress="javascript:onChangeToken();"></asp:TextBox>
                                <div class="input-group-addon">
                                    <span id="contador" class="input-group-text">120 seg</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div style="float: left">
                        <label style="font-size: smaller; color: Red">
                            (*) ¿No le llegó ningún código?<br />
                            <asp:LinkButton ID="lnkReenviar" runat="server" OnClick="btnGenerar_Click" Text="Clic aquí"></asp:LinkButton>
                            para volver a intentar
                        </label>
                    </div>
                    <asp:LinkButton ID="btnAceptar" runat="server" Text="" CssClass="d-none" />
                    <asp:UpdatePanel ID="updAccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal"
                                onclick="closeModal();">
                                Cancelar</button>
                            <button type="button" id="btnValidar" runat="server" class="btn btn-success">
                                Confirmar
                            </button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal SMS -->
    </form>

    <script type="text/javascript">
        var controlId = ''
        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(function(sender, args) {
            var elem = args.get_postBackElement();
            controlId = elem.id
        });

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function(sender, args) {
            var isOk = $("#validar").val();
            var error = args.get_error();

            if (error) {
                // Manejar el error
            }

            if (controlId == 'btnValidar') {
                if (isOk == "1") {
                    __doPostBack('btnAceptar', '');
                    showDivs('hide');
                }
            }
        });
    </script>

</body>
</html>
