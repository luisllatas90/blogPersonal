<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarPromedio_Modular.aspx.vb"
    Inherits="GestionCurricular_frmGenerarPromedio_Modular" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="google" value="notranslate" />
    <title>Generar Promedio Final</title>
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

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function StartCount() {
            var t = 300 * 1000;
            var x = setInterval(function() {
                t -= 1000;
                $("#contador").html((t / 1000) + " seg");

                if (t <= 0) {
                    clearInterval(x);
                    $("#contador").html("Expiró");
                }
            }, 1000);
        }

        function mostrarMensaje(yesNo, mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box;
            if (yesNo !== "") {
                if (yesNo == "no") {
                    box = bootbox.alert({ message: mensaje
                        , onEscape: false
                        , backdrop: true
                        , closeButton: false
                        , buttons: {
                            ok: {
                                label: "Aceptar",
                                className: "btn-success"
                            }
                        }
                    });
                } else {
                    box = bootbox.confirm({ message: mensaje
                        , onEscape: false
                        , backdrop: true
                        , closeButton: false
                        , buttons: {
                            cancel: {
                                label: "Cancelar",
                                className: "btn-danger"
                            }, confirm: {
                                label: "Continuar",
                                className: "btn-success"
                            }
                        }
                    , callback: function(result) {
                        //debugger;
                        var info = '<%= panInfo.ClientID %>';

                        if (result) {
                            $("#navigate").val("1");

                            if (info != null)
                                __doPostBack(info, '');

                            __doPostBack("<%= btnPublicar.ClientID %>", "");
                        }
                        else {
                            $("#navigate").val("2");
                        }
                    }
                    });
                };
            };

            box.find('.modal-body').css({ 'background-color': backcolor });
            box.find('.bootbox-body').css({ 'color': forecolor });
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
            if ($('#divEnviar').is(':hidden')) {
                $('#divEnviar').show('slide', { direction: 'left' }, 1000);
            } else {
                $('#divEnviar').hide('slide', { direction: 'left' }, 1000);
            }

            if (acc == "hide") {
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
        .panel
        {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
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
                        <div class="row">
                            <div class="col-md-6" style="border-right: 1px solid #d6d2d2;">
                                <label id="lblCorte" runat="server">
                                    Corte:</label>
                            </div>
                            <div class="col-md-6">
                                <i class="fa fa-user-tie" style="color: Black"></i>
                                <label id="lblDocente" runat="server">
                                    [Nombre docente]</label>
                            </div>
                        </div>
                        <div class="row" id="infoDocente" runat="server">
                            <asp:UpdatePanel ID="panInfo" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:HiddenField ID="navigate" runat="server" />
                                    <div class="col-md-6" style="border-right: 1px solid #d6d2d2;">
                                        <label id="lblCurso" runat="server">
                                            Asignatura:</label>
                                    </div>
                                    <div class="col-md-6">
                                        <b><i class="fab fa-whatsapp" style="color: #228B22"></i>
                                            <asp:HyperLink ID="lblCelular" runat="server" NavigateUrl="" Text="" Target="_blank"
                                                autopostback="false">
                                            </asp:HyperLink>
                                        </b>&nbsp; <b><i class="far fa-envelope" style="color: #1212B9"></i>
                                            <asp:HyperLink ID="lblEmail" runat="server" NavigateUrl="mailto:soporte@usat.edu.pe"
                                                Text="" Target="_self" autopostback="false">
                                            </asp:HyperLink>
                                        </b>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-5" style="text-align: left;">
                        <div class="row">
                            <div class="col-md-12" style="text-align: left;" runat="server" id="divPromedio">
                                <asp:CheckBox ID="chkConfirmarPublicacion" runat="server" Text="Confirmo generación de Actas de Notas" 
                                OnCheckedChanged="chkConfirmarPublicacion_ChekedChanged" AutoPostBack="true"/>
                                <asp:LinkButton ID="btnPublicar" runat="server" CssClass="btn btn-primary" Text='<i class="fa fa-save"></i> Publicación de promedios finales' />
                                <label ID="lblAviso" runat="server" style="background-color: Yellow; color: Blue;"></label>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1" id="divEnviar" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-md-12" style="color: red; font-size: smaller;">
                                *A continuación, un código se enviará por mensaje de texto(SMS) al:
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
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
                        </div>
                    </div>
                    <div class="col-md-6" style="text-align: right; float: right">
                        <asp:LinkButton ID="btnDescargarActa" runat="server" CssClass="btn btn-warning" Text='<i class="fa fa-download""></i> Descargar Acta de Notas' />
                        <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success" Text='<i class="fa fa-file-excel"></i> Exportar' />
                        <div class="form-group" id="divLeyenda" runat="server" style="float: right;">
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                DataKeyNames="codigo_Dma, codigoUniver_Alu, codigo_mat, nombre_alu, codigo_pso, inhabilitado_dma"
                                OnRowDataBound="gvNotas_OnRowDataBound" OnRowCreated="gvNotas_OnRowCreated" CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:TemplateField HeaderText="Código" HeaderStyle-Width="7%" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<%# Eval("codigoUniver_Alu") %>'
                                            CausesValidation="False" CssClass="btn btn-link btn-sm" OnClick="OnClick" ToolTip="Ver de manera resumida"></asp:LinkButton>
                                        --%>
                                            <asp:Label ID="lblCodUniv" runat="server" Text='<%# Eval("codigoUniver_Alu") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="nombre_alu" HeaderText="Apellidos y Nombres" HeaderStyle-Width="30%" ItemStyle-Width="30%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="10.5px" Font-Bold="true" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
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
                    <span class="modal-title">Confirmar Token de seguridad enviado por mensaje de texto</span>
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
                        <div class="col-sm-3">
                            <label for="txtToken" class="form-control input-sm no-border">
                                Ingrese el Token:
                            </label>
                        </div>
                        <div class="col-sm-9">
                            <div class="input-group">
                                <asp:TextBox ID="txtToken" runat="server" CssClass="form-control" Style="text-transform: uppercase"
                                    onKeyPress="javascript:onChangeToken();"></asp:TextBox>
                                <div class="input-group-addon">
                                    <span id="contador" class="input-group-text">300 seg</span>
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
                                <span><i class="fa fa-times"></i></span>&nbsp;Cancelar</button>
                            <button type="button" id="btnValidar" runat="server" class="btn btn-success">
                                <span><i class="fa fa-save"></i></span>&nbsp;Confirmar
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
