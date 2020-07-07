<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSilaboGeneral.aspx.vb"
    Inherits="GestionCurricular_FrmSilaboGeneral" EnableEventValidation="false" Culture="auto"
    meta:resourcekey="PageResource1" UICulture="auto" %>

<%--<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Asignar fechas a la sesión</title>
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

    <script type="text/javascript" src="js/bootbox.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnBuscar').click(function() {
                if ($('#ddlSemestre').val() == '' || $('#ddlSemestre').val() == '-1') {
                    alert("Seleccione el Semestre");
                    $('#ddlSemestre').focus();
                    return false;
                }

                if ($('#ddlCarreraProf').val() == '' || $('#ddlCarreraProf').val() == '-1') {
                    alert("Seleccione la Carrera Profesional");
                    $('#ddlCarreraProf').focus();
                    return false;
                }
            });
        });

        function openModal(acc, tip) {
            if (tip == "subirActa") {
                $('#myModalActa').modal('show');

            } else {
                $('#myModalSesion').modal('show');

                if (acc == "nuevo") {
                    $('#hdCodigoDis').val('');
                    $('#hdCodigoCur').val('');
                    $('#hdCodigoCup').val('');
                }
            }
        }

        function closeModal() {
            $('#hdCodigoDis').val('');
            $('#hdCodigoCur').val('');
            $('#hdCodigoCup').val('');
            $('#myModalSesion').modal('hide');
            $('#myModalActa').modal('hide');
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

        function mostrarMensaje(mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box = bootbox.alert({ message: mensaje, backdrop: true });
            box.find('.modal-body').css({ 'background-color': backcolor });
            box.find('.bootbox-body').css({ 'color': forecolor });
            box.find(".btn-primary").removeClass("btn-primary").addClass("btn-" + tipo);
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:HiddenField ID="hdCodigoDis" runat="server" />
    <asp:HiddenField ID="hdCodigoCur" runat="server" />
    <asp:HiddenField ID="hdCodigoCup" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <%-- <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" AnchorControl=""
        meta:resourcekey="BusyBox1Resource1" /> --%>
    <!-- Listado de -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Asignar fechas a la sesión</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Semestre:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True" meta:resourcekey="ddlSemestreResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Estado:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True" meta:resourcekey="ddlEstadoResource1">
                                    <asp:ListItem Value="%" Selected="True" Text="[ --- Mostrar todos --- ]" meta:resourcekey="ListItemResource1"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Con fecha de publicación" meta:resourcekey="ListItemResource2"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Sin fecha de publicación" meta:resourcekey="ListItemResource3"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row" visible="false" style="visibility: hidden">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Plan Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlPlanEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlCarreraProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True" meta:resourcekey="ddlCarreraProfResource1">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:Button ID="btnListar" runat="server" Text="Listar Sílabos" CssClass="btn btn-info"
                            meta:resourcekey="btnListarResource1" Visible="false" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="False"
                            DataKeyNames="codigo_cur,nombre_cur,codigo_dis,fecha_apr,codigo_cup,descripcion_Cac,nombre_Cur,codigo_Pes,codigo_Cac,codigo_Cpf"
                            OnRowCommand="gvResultados_RowCommand" CellPadding="0" ForeColor="#333333" CssClass="table table-bordered"
                            meta:resourcekey="gvResultadosResource1">
                            <Columns>
                                <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" HeaderStyle-Width="4%" />
                                <asp:BoundField DataField="identificador_Cur" HeaderText="Código" HeaderStyle-Width="8%" />
                                <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" HeaderStyle-Width="20%" />
                                <asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" HeaderStyle-Width="5%" />
                                <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" HeaderStyle-Width="5%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acciones" HeaderStyle-Width="58%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRegFechas" runat="server" CausesValidation="False" CssClass="btn btn-success btn-sm"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="RegistrarFechas"
                                            Text="Reg. Fechas" Enabled='<%# IIF(Eval("aprobado") = "0", "true", "false") %>'
                                            meta:resourcekey="btnFechasResource1"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="btnBajarSilabo" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="DescargarSilabo"
                                            Text="Desc. Sílabo" Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'
                                            meta:resourcekey="btnFechasResource1"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="btnBajarActa" runat="server" CausesValidation="False" CssClass="btn btn-info btn-sm"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="BajarActa"
                                            Text="Desc. Acta" Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'
                                            meta:resourcekey="btnFechasResource1"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="btnSubirActa" runat="server" CausesValidation="False" CssClass="btn btn-warning btn-sm"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="SubirActa"
                                            Text="Subir acta" Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'
                                            meta:resourcekey="btnFechasResource1"></asp:LinkButton>
                                        &nbsp;
                                        <asp:LinkButton ID="btnInstrumentos" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Instrumentos"
                                            Text="Agregar Instrumentos" Enabled='<%# IIF(Eval("aprobado") = "0", "true", "false") %>'
                                            meta:resourcekey="btnFechasResource1"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron asignaturas
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
    <!-- Modal fecha de sesiones -->
    <div id="myModalSesion" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Asignar fecha a las sesiones</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
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
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpSesion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <asp:GridView ID="gvSesion" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ses,nombre_ses,dia_fec,codigo_fec,codigo_gru"
                                        CellPadding="0" ForeColor="#333333" CssClass="table table-sm table-striped table-bordered table-condensed"
                                        meta:resourcekey="gvSesionResource1">
                                        <Columns>
                                            <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenido" ReadOnly="True"
                                                meta:resourcekey="BoundFieldResource6" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                                FooterStyle-Width="30%">
                                                <ItemStyle Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividad" ReadOnly="True"
                                                meta:resourcekey="BoundFieldResource6" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                                FooterStyle-Width="30%">
                                                <ItemStyle Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sesion" HeaderText="Sesión" ReadOnly="True" meta:resourcekey="BoundFieldResource5"
                                                HeaderStyle-Width="5%">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre_ses" HeaderText="Descripción" ReadOnly="True" meta:resourcekey="BoundFieldResource6"
                                                HeaderStyle-Width="10%" ItemStyle-Width="10%" FooterStyle-Width="10%">
                                                <ItemStyle Wrap="True" Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Fecha de Sesión" meta:resourcekey="TemplateFieldResource2"
                                                HeaderStyle-Width="13%" ItemStyle-Width="13%" FooterStyle-Width="13%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("nombre_fec") %>' meta:resourcekey="Label1Resource1"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlFecha" CssClass="form-control form-control-sm"
                                                        Style="width: 100%" />
                                                </EditItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlNewFecha" CssClass="form-control form-control-sm"
                                                        Style="width: 100%" />
                                                </FooterTemplate>
                                                <ItemStyle Wrap="True" HorizontalAlign="Center" VerticalAlign="Middle" Width="13%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Editar" ShowHeader="False" meta:resourcekey="TemplateFieldResource3"
                                                HeaderStyle-Width="12%" ItemStyle-Width="12%" FooterStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Editar" CssClass="btn btn-primary btn-sm"
                                                        CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-pen"></i>' meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
                                                    <span onclick="return confirm('¿Está seguro de eliminar la fecha asignada?')">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Eliminar" CssClass="btn btn-danger btn-sm"
                                                            CausesValidation="True" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'
                                                            Text='<i class="fa fa-backspace"></i>' meta:resourcekey="LinkButton2Resource1"
                                                            OnClick="OnDeleteFecha" />
                                                    </span>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Actualizar" CssClass="btn btn-success btn-sm"
                                                        CausesValidation="True" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-save"></i>' meta:resourcekey="LinkButton3Resource1"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" ToolTip="Cancelar" CssClass="btn btn-info btn-sm"
                                                        CausesValidation="False" CommandName="Cancel" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-times"></i>' meta:resourcekey="LinkButton4Resource1"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="Button2" runat="server" ToolTip="Salir" CssClass="btn btn-danger"
                        Text='<i class="fa fa-sign-out-alt"></i> Salir' data-dismiss="modal"></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal subir acta -->
    <div id="myModalActa" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div3">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Subir Acta de Exposición</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-12">
                                <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                    ErrorMessage="* Seleccione un archivo" ControlToValidate="fuArchivo" ValidationGroup="subirArchivo">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalirActa" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGuardarActa" runat="server" Text="Guardar" CssClass="btn btn-success"
                        ValidationGroup="subirArchivo" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
