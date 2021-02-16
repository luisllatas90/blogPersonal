<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCanalRecepcion.aspx.vb"
    Inherits="OrientacionCliente_FrmCanalRecepcion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title>Canales de Recepción</title>
    <link href="../../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Sweet Alert =============================================--%>

    <script src="../../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            fnLoading(false);
        });
        function fnLoading(sw) {
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }
        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 5000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
        function fnConfirmacion(ctrl, texto, adicional) {
            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                title: texto,
                text: adicional,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si',
                cancelButtonText: 'No'
            }).then(function(result) {
                if (result.value == true) {
                    fnLoading(true);
                    eval(defaultAction);
                }
            })
        }
        function Validar(ctrl, texto, adicional) {
            if ($("#txtDescripcion").val().trim() == "") {
                fnMensaje('error', 'Debe ingresar el canal de recepción')
                return false
            }
            else {
                fnConfirmacion(ctrl, texto, adicional);
            }
        }
    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
        }
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="piluku-preloader text-center">
                    <div class="loader">
                        Loading...</div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 16px;">
                <b>Canales de recepción de solicitud</b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <asp:LinkButton runat="server" ID="lbNuevo" CssClass="btn btn-sm btn-primary btn-radius"
                                Text='<span class="fa fa-plus-circle"></span>&nbsp;Nuevo' OnClientClick="fnLoading(true);"></asp:LinkButton>
                            <div class="form-group">
                                <asp:GridView runat="server" ID="gvLista" CssClass="table table-condensed" DataKeyNames="codigo_ooc,nombre_ooc"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nombre_ooc" HeaderText="CANAL DE RECEPCIÓN" HeaderStyle-Width="50%" />
                                        <asp:BoundField DataField="cod_encuesta" HeaderText="ENCUESTA" HeaderStyle-Width="20%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-edit"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Editar" CommandName="Editar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminar" runat="server" Text='<span class="fa fa-close"></span>'
                                                    CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Eliminar" CommandName="Eliminar"
                                                    OnClientClick="fnConfirmacion(this,'¿Está seguro que desea eliminar el canal de recepción?','Luego no podrá visualizar el registro'); return false;"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron registros</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                        <asp:AsyncPostBackTrigger ControlID="lbGuardar" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group" id="DivMantenimiento" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hdc" Value="0" Visible="false" />
                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtDescripcion">Canal de recepción</asp:Label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" Text=""></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:HiddenField runat="server" ID="hdcv" Value="0" Visible="false" />
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-1 control-label"
                                        For="txtEncuesta">Vincular encuesta</asp:Label>
                                    <div class="col-xs-12 col-sm-6 col-md-6">
                                        <div class="input-group demo-group">
                                            <asp:TextBox runat="server" ID="txtEncuesta" CssClass="form-control" Text=""></asp:TextBox>
                                            <span class="input-group-btn">
                                                <asp:LinkButton runat="server" ID="btnBuscarEstudiante" CssClass="btn btn-sm btn-primary btn-radius"
                                                    Text="<i class='fa fa-search'></i>">
                                                </asp:LinkButton>
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group text-center">
                                    <asp:LinkButton runat="server" ID="lbGuardar" CssClass="btn btn-sm btn-success btn-radius"
                                        OnClientClick="Validar(this,'¿Está seguro que desea guardar los datos?',''); return false;"
                                        Text='<span class="fa fa-save"></span>&nbsp;Guardar'></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lbCancelar" CssClass="btn btn-sm btn-danger btn-radius"
                                        OnClientClick="fnLoading(true);" Text='<span class="fa fa-close"></span>&nbsp;Cancelar'></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="lbNuevo" />
                        <asp:AsyncPostBackTrigger ControlID="lbCancelar" />
                        <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
