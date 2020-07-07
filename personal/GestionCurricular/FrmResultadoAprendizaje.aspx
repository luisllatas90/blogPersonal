<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmResultadoAprendizaje.aspx.vb"
    Inherits="GestionCurricular_FrmResultadoAprendizaje" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Resultado de Aprendizaje de la Unidad</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript" src="js/bootbox.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnAceptar').click(function() {
                if ($('#ddlSemestre').val() == '' || $('#ddlSemestre').val() == '-1') {
                    alert("Seleccione el Semestre");
                    $('#ddlSemestre').focus();
                    return false;
                }

                if ($('#ddlCarreraProf').val() == '' || $('#ddlCarreraProf').val() == '-1') {
                    alert("Seleccione la carrera profesional");
                    $('#ddlCarreraProf').focus();
                    return false;
                }

                if ($('#ddlPlanEstudio').val() == '' || $('#ddlPlanEstudio').val() == '-1') {
                    alert("Seleccione el plan de estudios");
                    $('#ddlPlanEstudio').focus();
                    return false;
                }

                if ($('#ddlAsignatura').val() == '' || $('#ddlAsignatura').val() == '-1') {
                    alert("Seleccione la asignatura");
                    $('#ddlAsignatura').focus();
                    return false;
                }

                if ($('#txtUnidad').val() == '') {
                    alert("Ingrese descripción de la unidad");
                    $('#txtUnidad').focus();
                    return false;
                }

                var $grid1 = $('#<%=gvDetalle.ClientID%> tr a[id$="LinkButton1"]');
                if ($grid1.length <= 0) {
                    alert("Ingrese descripción del resultado de la unidad");
                    $grid1 = $('#<%=gvDetalle.ClientID%> tr');
                    $grid1.find("td").first().find("input[type='text']").focus();
                    //$('#gvDetalle').focus();
                    return false;
                }
            });
        });

        function openModal(acc) {
            $('#myModal').modal('show');

            if (acc == "nuevo") {
                $('#hdCodigoUnidad').val('');
                $('#txtUnidad').val('');
            }
        }

        function closeModal() {
            $('#hdCodigoUnidad').val('');
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Resultado de Aprendizaje -->
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
                    <div class="col-md-10">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Registrar Diseño de Asignatura</label></h4>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnSeguir" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-right"></i></span> Seguir
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-xs-3">
                        <div class="form-group form-group-sm">
                            <label class="col-xs-4 control-label" for="ddlSemestre">
                                Semestre:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-5">
                        <div class="form-group form-group-sm">
                            <label class="col-xs-4 control-label" for="ddlCarreraProf">
                                Carrera Profesional:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlCarreraProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group form-group-sm">
                            <label class="col-xs-4 control-label" for="ddlPlanEstudio">
                                Plan Estudio:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlPlanEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-6">
                        <div class="form-group">
                            <label id="lblAsignatura" runat="server" class="col-xs-4" for="ddlAsignatura">
                                Asignatura:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlAsignatura" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <asp:LinkButton ID="btnAgregar" runat="server" Text='<i class="fa fa-plus"></i> Agregar Unidad'
                            CssClass="btn btn-success"></asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvResultado" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                            DataKeyNames="codigo_uni, codigo_res, descripcion_uni, codigo_dis, numero_uni, orden_uni"
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_uni" HeaderText="#" Visible="false" HeaderStyle-Width="0%" />
                                <asp:BoundField DataField="numero_uni" HeaderText="Unidad" HeaderStyle-Width="5%" />
                                <asp:BoundField DataField="descripcion_uni" HeaderText="Descripción" HeaderStyle-Width="15%" />
                                <asp:BoundField DataField="descripcion_res" HeaderText="Resultado de aprendizaje de la unidad"
                                    HeaderStyle-Width="70%" />
                                <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <span onclick="return confirm('¿Está seguro de editar la unidad?')">
                                            <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-edit"></i>' ToolTip="Editar"
                                                runat="server" OnClick="OnEditUnidad" CssClass="btn btn-info btn-sm" />
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Eliminar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <span onclick="return confirm('¿Está seguro de eliminar la unidad?')">
                                            <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                runat="server" OnClick="OnDeleteUnidad" CssClass="btn btn-danger btn-sm" />
                                        </span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron datos
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
    <!-- Modal Resultado de Aprendizaje -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Resultados de la Unidad</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="udpDetalle" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-3" for="ddlUnidad">
                                            Unidad:</label>
                                        <div class="col-xs-9">
                                            <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-3" for="txtUnidad">
                                            Descripción de la Unidad:</label>
                                        <div class="col-xs-9">
                                            <asp:TextBox ID="txtUnidad" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" CssClass="table table-sm table-bordered table-hover"
                                            GridLines="None" OnRowEditing="gvDetalle_RowEditing" ShowHeadersWhenNoRecords="True"
                                            DataKeyNames="codigo_res, descripcion_res, peso_res, tipo_prom" ShowFooter="True">
                                            <Columns>
                                                <asp:BoundField DataField="codigo_res" HeaderText="ID" ReadOnly="true" Visible="false"
                                                    HeaderStyle-Width="0%" />
                                                <asp:TemplateField HeaderText="Resultado de la Unidad" HeaderStyle-Width="85%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResultado" runat="server" Text='<%#Eval("descripcion_res")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtResultado" runat="server" Text='<%#Eval("descripcion_res")%>'
                                                            CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtNewResultado" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                            runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                        <span onclick="return confirm('¿Está seguro de eliminar el resultado de la unidad?')">
                                                            <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                                runat="server" OnClick="OnDelete" CssClass="btn btn-danger btn-sm" />
                                                        </span>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                            runat="server" OnClick="OnUpdate" CssClass="btn btn-success btn-sm" />
                                                        <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                            runat="server" OnClick="OnCancel" CssClass="btn btn-info btn-sm" />
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                            runat="server" CommandName="New" OnClick="OnNew" CssClass="btn btn-success btn-sm" />
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                No se registró ningún resultado de la unidad
                                            </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                Font-Size="13px" />
                                            <EditRowStyle BackColor="#FFFFCC" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" Text="Guardar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdCodigoUnidad" runat="server" />
    </form>
</body>
</html>
