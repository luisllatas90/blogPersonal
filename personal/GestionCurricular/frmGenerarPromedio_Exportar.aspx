<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGenerarPromedio_Exportar.aspx.vb"
    Inherits="GestionCurricular_frmGenerarPromedio_Exportar" EnableEventValidation="false" %>

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
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvNotas" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                DataKeyNames="codigo_Dma, codigoUniver_Alu, codigo_mat, nombre_alu, codigo_pso, inhabilitado_dma, codigo_alu"
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
