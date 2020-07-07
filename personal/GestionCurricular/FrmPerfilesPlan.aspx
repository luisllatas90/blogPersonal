<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPerfilesPlan.aspx.vb"
    Inherits="GestionCurricular_FrmPerfilesPlan" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registro Perfil de Ingreso y Egreso</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/smart-tab/styles/smart_tab.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/smart-tab/js/jquery.smartTab.js?x=1"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#tabs').smartTab({ selected: 0, saveState: true, autoProgress: false, stopOnFocus: true, transitionEffect: 'slide', keyNavigation: false, autoHeight: true });
            
            var $grid1 = $('#<%=gvIngreso.ClientID%> tr');
            var $grid2 = $('#<%=gvEgreso.ClientID%> tr');

            if ($grid1.length > 0) {
                $('#tabs0').show();
                $('#tabs1').hide();
            } else {
                if ($grid2.length > 0) {
                    $('#tabs0').hide();
                    $('#tabs1').show();
                }
            }

            $('#btnAceptarIng').click(function() {
                if ($('#ddlCompetenciaIng').val() == '' || $('#ddlCompetenciaIng').val() == '-1') {
                    alert("Seleccione el tipo de Competencia");
                    $('#ddlCompetenciaIng').focus();
                    return false;
                }

                if ($('#txtDescripcionIng').val() == '') {
                    alert("Ingrese una descripción del Perfil de Ingreso");
                    $('#txtDescripcionIng').focus();
                    return false;
                }

                return fc_validarCombos();
            });

            $('#btnAceptarEgr').click(function() {
                if ($('#ddlCompetenciaEgr').val() == '' || $('#ddlCompetenciaEgr').val() == '-1') {
                    alert("Seleccione el tipo de Competencia");
                    $('#ddlCompetenciaEgr').focus();
                    return false;
                }

                if ($('#txtDescripcionEgr').val() == '') {
                    alert("Ingrese una descripción del Perfil de Egreso");
                    $('#txtDescripcionEgr').focus();
                    return false;
                }

                return fc_validarCombos();
            });

            $('#chkCompetenciaIng').click(function() {
                $('#txtCompetenciaIng').focus();
            });

            $('#chkCompetenciaEgr').click(function() {
                $('#txtCompetenciaEgr').focus();
            });
        });

        function openModal(acc, com, modal, cat) {
            if (modal == "ingreso") {
                $('#myModalIngreso').modal('show');

                if (acc == "nuevo") {
                    $('#hdCodigoIng').val('');
                    $('#txtDescripcionIng').val('');
                } else {
                    $('#ddlCompetenciaIng').val(com);
                    $('#ddlCategoriaIng').val(cat);
                }
            } else if (modal == "egreso") {
                $('#myModalEgreso').modal('show');

                if (acc == "nuevo") {
                    $('#hdCodigoEgr').val('');
                    $('#txtDescripcionEgr').val('');
                } else {
                    $('#ddlCompetenciaEgr').val(com);
                    $('#ddlCategoriaEgr').val(cat);
                }
            }
        }

        function closeModal() {
            $('#hdCodigoIng').val('');
            $('#hdCodigoEgr').val('');

            $('#myModalIngreso').modal('hide');
            $('#myModalEgreso').modal('hide');
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

        function fc_validarCombos() {
            if ($('#ddlCarreraProf').val() == '' || $('#ddlCarreraProf').val() == '-1') {
                alert('Seleccione una Carrera Profesional');
                $('#ddlCarreraProf').focus();
                return false;
            }

            if ($('#ddlPlanCurricular').val() == '' || $('#ddlPlanCurricular').val() == '-1') {
                alert('Seleccione un Plan Curricular');
                $('#ddlPlanCurricular').focus();
                return false;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:busybox id="BusyBox1" runat="server" showbusybox="OnLeavingPage" image="Clock"
        text="Su solicitud está siendo procesada..." title="Por favor espere" />
    <!-- Listado General -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registro Perfil de Ingreso y Egreso</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="col-md-3" for="ddlCarreraProf">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlCarreraProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="col-md-3" for="ddlPlanCurricular">
                                Plan Curricular:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlPlanCurricular" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <!-- Tabs -->
                <div id="tabs" runat="server">
                    <ul>
                        <li id="liPerfilIngreso"><a href="#tabs0"><small><b>PERFIL DE INGRESO</b></small> </a>
                        </li>
                        <li id="liPerfilEgreso"><a href="#tabs1"><small><b>PERFIL DE EGRESO</b></small> </a>
                        </li>
                    </ul>
                    <div id="tabs0" style="display: block; top: 0px; width: 100%;">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:LinkButton ID="btnAgregarIng" runat="server" Text='<i class="fa fa-plus"></i> Agregar Perfil'
                                    CssClass="btn btn-success"></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvIngreso" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                    DataKeyNames="codigo_pIng, codigo_com, codigo_pcur, descripcion_pIng, estado_pIng, codigo_cat"
                                    CssClass="table table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Activo" HeaderStyle-Width="0%"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("estado_pIng") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo_pIng" HeaderText="Código" HeaderStyle-Width="0%"
                                            Visible="false" />
                                        <asp:BoundField DataField="categoria" HeaderText="Categoría" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="nombre_com" HeaderText="Competencia" HeaderStyle-Width="0%"
                                            Visible="false" />
                                        <asp:BoundField DataField="descripcion_pIng" HeaderText="Competencia" HeaderStyle-Width="75%" />
                                        <asp:TemplateField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditarIng" runat="server" ToolTip="Editar Perfil" CommandName="Editar"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-primary btn-sm"
                                                    Text='<i class="fa fa-pen"></i>' OnClientClick="return confirm('¿Desea editar éste Perfil de Ingreso?');">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminarIng" runat="server" ToolTip="Eliminar Perfil" CommandName="Eliminar"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm"
                                                    Text='<i class="fa fa-trash"></i>' OnClientClick="return confirm('¿Está seguro de eliminar éste Perfil de Ingreso?');">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron perfiles de ingreso
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="tabs1" style="display: block; top: 0px; width: 100%;">
                        <div class="row">
                            <div class="col-md-4">
                                <asp:LinkButton ID="btnAgregarEgr" runat="server" Text='<i class="fa fa-plus"></i> Agregar Perfil'
                                    CssClass="btn btn-success"></asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-lg-12">
                                <asp:GridView ID="gvEgreso" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                    DataKeyNames="codigo_pEgr, codigo_com, codigo_pcur, descripcion_pEgr, estado_pEgr, codigo_cat"
                                    CssClass="table table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Activo" HeaderStyle-Width="0%"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Enabled="false" Checked='<%# Eval("estado_pEgr") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo_pEgr" HeaderText="Código" HeaderStyle-Width="0%"
                                            Visible="false" />
                                        <asp:BoundField DataField="categoria" HeaderText="Categoría" HeaderStyle-Width="15%" />
                                        <asp:BoundField DataField="nombre_com" HeaderText="Competencia" HeaderStyle-Width="0%"
                                            Visible="false" />
                                        <asp:BoundField DataField="descripcion_pEgr" HeaderText="Competencia" HeaderStyle-Width="75%" />
                                        <asp:TemplateField HeaderText="Editar" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEditarEgr" runat="server" ToolTip="Editar Perfil" CommandName="Editar"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-primary btn-sm"
                                                    Text='<i class="fa fa-pen"></i>' OnClientClick="return confirm('¿Desea editar éste Perfil de Egreso?');">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Eliminar" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEliminarEgr" runat="server" ToolTip="Eliminar Perfil" CommandName="Eliminar"
                                                    CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm"
                                                    Text='<i class="fa fa-trash"></i>' OnClientClick="return confirm('¿Está seguro de eliminar éste Perfil de Egreso?');">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron perfiles de egreso
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
        </div>
    </div>
    <!-- Modal Perfil de Ingreso -->
    <div id="myModalIngreso" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Perfil de Ingreso</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModalIng" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form_group">
                                        <label for="ddlCategoriaIng" class="col-xs-2">
                                            Categoría:</label>
                                        <div class="col-xs-10">
                                            <asp:DropDownList ID="ddlCategoriaIng" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form_group">
                                        <label id="lblCompetenciaIng" runat="server" class="col-xs-2">
                                            Competencia:</label>
                                        <div class="col-xs-10">
                                            <asp:DropDownList ID="ddlCompetenciaIng" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCompetenciaIng" runat="server" CssClass="form-control input-sm"
                                                Visible="false"></asp:TextBox>
                                            <asp:CheckBox ID="chkCompetenciaIng" runat="server" CssClass="form-control input-sm no-border"
                                                Text=" Crear nueva competencia" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-2" for="txtDescripcionIng" runat="server" id="lblDescripcionIng"
                                            visible="false">
                                            Competencia:</label>
                                        <div class="col-xs-10">
                                            <asp:TextBox ID="txtDescripcionIng" runat="server" CssClass="form-control input-sm"
                                                TextMode="MultiLine" Rows="5" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelarIng" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptarIng" runat="server" Text="Guardar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Perfil de Egreso -->
    <div id="myModalEgreso" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div2">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Perfil de Egreso</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModalEgr" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form_group">
                                        <label for="ddlCategoriaEgr" class="col-xs-2">
                                            Categoría:</label>
                                        <div class="col-xs-10">
                                            <asp:DropDownList ID="ddlCategoriaEgr" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form_group">
                                        <label id="lblCompetenciaEgr" runat="server" class="col-xs-2">
                                            Competencia:</label>
                                        <div class="col-xs-10">
                                            <asp:DropDownList ID="ddlCompetenciaEgr" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtCompetenciaEgr" runat="server" CssClass="form-control input-sm"
                                                Visible="false"></asp:TextBox>
                                            <asp:CheckBox ID="chkCompetenciaEgr" runat="server" CssClass="form-control input-sm no-border"
                                                Text=" Crear nueva competencia" AutoPostBack="true" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-2" for="txtDescripcionEgr" runat="server" id="lblDescripcionEgr"
                                            visible="false">
                                            Competencia:</label>
                                        <div class="col-xs-10">
                                            <asp:TextBox ID="txtDescripcionEgr" runat="server" CssClass="form-control input-sm"
                                                TextMode="MultiLine" Rows="5" Visible="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelarEgr" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptarEgr" runat="server" Text="Guardar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdCodigoIng" runat="server" />
    <asp:HiddenField ID="hdCodigoEgr" runat="server" />
    </form>
</body>
</html>
