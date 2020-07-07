<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAdicionarInstrumentosContenido_modular.aspx.vb"
    Inherits="GestionCurricular_FrmAdicionarInstrumentosContenido_modular" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Instrumentos de Evaluación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript" src="js/bootbox.min.js"></script>

    <script type="text/javascript">
        function openModal(accion) {
            $('#myModal').modal('show');

            if (accion == 'Nuevo') {
                $('#hdCodigoGru').val('');
            }
        }

        function closeModal() {
            $('#hdCodigoGru').val('');

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
    <asp:HiddenField ID="hdCodigoGru" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <busyboxdotnet:busybox id="BusyBox1" runat="server" showbusybox="OnLeavingPage" image="Clock"
        text="Su solicitud está siendo procesada..." title="Por favor espere" />
    <!-- Listado Contenidos de Asignatura -->
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
                            <label id="titulo" runat="server">
                                &nbsp;Instrumentos de Evaluación</label></h4>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-xs-4">
                        <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control input-sm"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-12">
                        <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                            DataKeyNames="codigo_dis, codigo_uni, unidad, codigo_gru, total_ses, numero_uni, codigo_sgr"
                            CssClass="table table-sm table-striped table-bordered table-condensed">
                            <Columns>
                                <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenido" HeaderStyle-Width="28%"
                                    ItemStyle-Width="28%" FooterStyle-Width="28%" />
                                <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividad" HeaderStyle-Width="28%"
                                    ItemStyle-Width="28%" FooterStyle-Width="28%" />
                                <asp:BoundField HtmlEncode="false" DataField="sesion" HeaderText="Descripción" ItemStyle-VerticalAlign="Middle"
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="6%" ItemStyle-Width="6%"
                                    FooterStyle-Width="6%" />
                                <asp:BoundField DataField="descripcion_sgr" HeaderText="Subgrupo" ReadOnly="True"
                                    HeaderStyle-Width="6%" ItemStyle-Width="6%" FooterStyle-Width="6%">
                                    <ItemStyle Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField HtmlEncode="false" DataField="instrumento" HeaderText="Instrumentos"
                                    HeaderStyle-Width="27%" ItemStyle-Width="27%" FooterStyle-Width="27%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción" HeaderStyle-Width="5%"
                                    ItemStyle-Width="5%" FooterStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAgregarInstr" runat="server" Text="Editar" CommandName="EditarGrupo"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-warning btn-sm"
                                            OnClientClick="return confirm('¿Desea editar los Intrumentos de Evaluación?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron instrumentos de evaluación
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
    <!-- Modal Registro de Instrumentos de Evaluación -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-md">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" id="btnSalir2" runat="server" class="close">
                        &times;</button>
                    <h4 class="modal-title">
                        Instrumentos de Evaluación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpInstrumento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="gvInstrumento" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_eva,codigo_cup,codigo_ins,codigo_gru,descripcion_ins,descripcion_eva"
                                        ShowFooter="True" ShowHeader="True" OnRowDeleting="gvInstrumento_RowDeleting"
                                        OnRowCommand="gvInstrumento_RowCommand" OnRowDataBound="gvInstrumento_RowDataBound"
                                        CellPadding="0" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Evaluación" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtEvaluacion" runat="server" CssClass="form-control input-sm" Style="width: 100%" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEvaluacion" runat="server" Text='<%# Eval("descripcion_eva") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtNewEvaluacion" runat="server" CssClass="form-control input-sm"
                                                        Style="width: 100%" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Instrumento">
                                                <EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlInstrumento" CssClass="form-control input-sm"
                                                        Style="width: 100%" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoIns" runat="server" Text='<%# Eval("codigo_ins") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblInstrumento" runat="server" Text='<%# Eval("instrumento") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlNewInstrumento" CssClass="form-control input-sm"
                                                        Style="width: 100%" />
                                                </FooterTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Fecha">
                                                <EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlFecEvaluacion" CssClass="form-control input-sm"
                                                        Style="width: 100%" />
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCodigoFec" runat="server" Text='<%# Eval("codigo_fec") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="lblFechaEvaluacion" runat="server" Text='<%# Eval("nombre_fec") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlNewFecEva" CssClass="form-control input-sm"
                                                        Style="width: 100%" />
                                                </FooterTemplate>
                                                <ItemStyle Wrap="True" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acción" ShowHeader="False">
                                                <ItemTemplate>
                                                    <span onclick="return confirm('¿Está seguro de eliminar el Instrumento de Evaluación?')">
                                                        <asp:LinkButton ID="lnkB" runat="server" CssClass="btn btn-danger btn-sm" Text="Eliminar"
                                                            CommandArgument='<%# Container.DataItemIndex %>' CommandName="Delete"></asp:LinkButton>
                                                    </span>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkAddNew" runat="server" CssClass="btn btn-primary btn-sm" Text="Agregar"
                                                        CommandArgument='<%# Container.DataItemIndex %>' CommandName="AddNew"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer" style="text-align: center">
                    <asp:Button ID="btnSalir" runat="server" Text="Salir" class="btn btn-danger" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
