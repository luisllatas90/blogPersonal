<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmContenidosAsignatura.aspx.vb"
    Inherits="GestionCurricular_FrmContenidosAsignatura" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Configurar contenidos de la Asignatura</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/smart-tab/styles/smart_tab.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/smart-tab/js/jquery.smartTab.min.js"></script>

    <script type="text/javascript" src="../scripts/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#tabs').smartTab({ selected: 0, saveState: true, autoProgress: false, stopOnFocus: true, transitionEffect: 'slide', keyNavigation: false, autoHeight: true });

            $('#btnGrabar').click(function() {
                if ($('#ddlSemestre').val() == '' || $('#ddlSemestre').val() == '-1') {
                    alert("Seleccione el semestre");
                    $('#ddlSemestre').focus();
                    return false;
                }

                if ($('#ddlDiseñoAprendizaje').val() == '' || $('#ddlDiseñoAprendizaje').val() == '-1') {
                    alert("Seleccione el diseño de aprendizaje");
                    $('#ddlDiseñoAprendizaje').focus();
                    return false;
                }

                if ($('#ddlUnidad').val() == '' || $('#ddlUnidad').val() == '-1') {
                    alert("Seleccione la unidad");
                    $('#ddlUnidad').focus();
                    return false;
                }

                var $grid1 = $('#<%=gvContenido.ClientID%> tr a[id$="LinkButton1"]');
                if ($grid1.length <= 0) {
                    alert("Ingrese descripción del Contenido");
                    $grid1 = $('#<%=gvContenido.ClientID%> tr');
                    $grid1.find("td").first().find("input[type='text']").focus();
                    return false;
                }

                var $grid2 = $('#<%=gvActividad.ClientID%> tr a[id$="LinkButton1"]');
                if ($grid2.length <= 0) {
                    alert("Ingrese descripción de la Actividad");
                    $grid2 = $('#<%=gvActividad.ClientID%> tr');
                    $grid2.find("td").first().find("input[type='text']").focus();
                    return false;
                }
            });
        });

        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
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
                    <div class="col-md-10">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Configurar contenidos de la Asignatura</label></h4>
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
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-4">
                                Semestre:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <div class="form-group">
                            <label id="lblAsignatura" runat="server" class="col-md-4">
                                Diseño Aprendizaje:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlDiseñoAprendizaje" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-4">
                                Unidad:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <label class="col-md-4">
                            &nbsp;</label>
                        <div class="col-md-8">
                            <asp:LinkButton ID="btnAgregarGrupo" runat="server" Text='<i class="fa fa-plus"></i> Crear Contenido'
                                CssClass="btn btn-success"></asp:LinkButton>
                            <asp:Button ID="btnListar" runat="server" Text="Listar Contenidos" CssClass="btn btn-info"
                                Visible="false" />
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_dis, codigo_uni, unidad, codigo_gru, total_ses"
                            CssClass="table table-sm table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="codigo_gru" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                    HeaderText="#" HeaderStyle-Width="0%" Visible="false" />
                                <asp:BoundField HtmlEncode="false" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                    DataField="sesion" HeaderText="Sesión" HeaderStyle-Width="4%" />
                                <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenido" HeaderStyle-Width="43%" />
                                <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividades"
                                    HeaderStyle-Width="43%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Editar" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditarGrupo" runat="server" ToolTip="Editar" Text='<i class="fa fa-edit"></i>'
                                            CommandName="EditarGrupo" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea editar el grupo de contenidos?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Eliminar" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEliminarGrupo" runat="server" ToolTip="Eliminar" Text='<i class="fa fa-trash"></i>'
                                            CommandName="EliminarGrupo" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Seguro de eliminar el grupo de contenidos?');"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron contenido de asignaturas
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
    <!-- Modal Registro Contenidos de Asignatura -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Contenidos de la Asignatura</h4>
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
                    <!-- Tabs -->
                    <div id="tabs">
                        <ul>
                            <li id="liContenido" runat="server"><a href="#tabs-1"><small><b>Contenido</b></small>
                            </a></li>
                            <li id="liActividad" runat="server"><a href="#tabs-2"><small><b>Actividad</b></small>
                            </a></li>
                            <li id="liSesiones" runat="server"><a href="#tabs-3"><small><b>Sesiones</b></small>
                            </a></li>
                        </ul>
                        <div id="tabs-1" style="display: block; top: 0px; width: 100%;">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="udpContenido" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvContenido" runat="server" AutoGenerateColumns="false" CssClass="table table-sm table-bordered table-hover"
                                                    GridLines="None" OnRowEditing="gvContenido_RowEditing" ShowHeadersWhenNoRecords="True"
                                                    DataKeyNames="codigo_con,codigo_gru,contenido" ShowFooter="True" ShowHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Contenido" HeaderStyle-Width="88%" ItemStyle-Width="88%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContenido" runat="server" Text='<%#Eval("contenido")%>' TextMode="MultiLine"
                                                                    Columns="120" Rows="2"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtContenido" runat="server" Text='<%#Eval("contenido")%>' TextMode="MultiLine"
                                                                    Columns="120" Rows="2" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtNewContenido" runat="server" TextMode="MultiLine" Columns="120"
                                                                    Rows="2" CssClass="form-control form-control-sm"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemStyle Wrap="True" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                                            FooterStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                                    runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                                <span onclick="return confirm('¿Está seguro de eliminar el contenido?')">
                                                                    <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                                        runat="server" OnClick="OnDeleteContenido" CssClass="btn btn-danger btn-sm" />
                                                                </span>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                                    runat="server" OnClick="OnUpdateContenido" CssClass="btn btn-success btn-sm" />
                                                                <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                                    runat="server" OnClick="OnCancelContenido" CssClass="btn btn-info btn-sm" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                                    runat="server" CommandName="New" OnClick="OnNewContenido" CssClass="btn btn-success btn-sm" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No se registró ningún contenido
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                        Font-Size="13px" />
                                                    <EditRowStyle BackColor="#FFFFCC" />
                                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div id="tabs-2" style="display: block; top: 0px; width: 100%;">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="udpActividad" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvActividad" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered table-hover"
                                                    GridLines="None" OnRowEditing="gvActividad_RowEditing" ShowHeadersWhenNoRecords="True"
                                                    DataKeyNames="codigo_act,codigo_gru,actividad" ShowFooter="True" ShowHeader="False">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Actividad" HeaderStyle-Width="88%" ItemStyle-Width="88%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActividad" runat="server" Text='<%# Eval("actividad") %>' TextMode="MultiLine"
                                                                    Columns="120" Rows="2"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtActividad" runat="server" Text='<%# Eval("actividad") %>' TextMode="MultiLine"
                                                                    Columns="120" Rows="2" CssClass="form-control input-sm" Style="width: 100%"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtNewActividad" runat="server" TextMode="MultiLine" Columns="120"
                                                                    Rows="2" CssClass="form-control input-sm" Style="width: 100%"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemStyle Wrap="True" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="12%" ItemStyle-Width="12%"
                                                            FooterStyle-Width="12%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                                    runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" />
                                                                <span onclick="return confirm('¿Está seguro de eliminar la actividad?')">
                                                                    <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-trash"></i>' ToolTip="Eliminar"
                                                                        runat="server" OnClick="OnDeleteActividad" CssClass="btn btn-danger btn-sm" />
                                                                </span>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                                    runat="server" OnClick="OnUpdateActividad" CssClass="btn btn-success btn-sm" />
                                                                <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                                    runat="server" OnClick="OnCancelActividad" CssClass="btn btn-info btn-sm" />
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="LinkButton5" Text='<i class="fa fa-plus"></i>' ToolTip="Agregar"
                                                                    runat="server" CommandName="New" OnClick="OnNewActividad" CssClass="btn btn-success btn-sm" />
                                                            </FooterTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div id="tabs-3" style="display: block; top: 0px; width: 100%;">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:UpdatePanel ID="udpSesion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvSesion" runat="server" AutoGenerateColumns="False" CssClass="table table-sm table-bordered table-hover"
                                                    GridLines="None" OnRowEditing="gvSesion_RowEditing" ShowHeadersWhenNoRecords="True"
                                                    DataKeyNames="codigo,sesion,codigo_ses" ShowFooter="False" ShowHeader="True">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="#" HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSeleccionado" runat="server" Checked='<%# Eval("seleccion") %>'
                                                                    Enabled='<%# Eval("habilitado") %>' OnCheckedChanged="OnCheck" AutoPostBack="true" />
                                                            </ItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="sesion" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                                            HeaderText="Sesión" HeaderStyle-Width="4%" ReadOnly="true" />
                                                        <asp:TemplateField HeaderText="Descripción de la Sesión" HeaderStyle-Width="85%"
                                                            ItemStyle-Width="85%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescripcion" runat="server" Text='<%# Eval("descripcion") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtDescripcion" runat="server" Text='<%# Eval("descripcion") %>'
                                                                    CssClass="form-control input-sm" Style="width: 100%"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txtNewDescripcion" runat="server" CssClass="form-control input-sm"
                                                                    Style="width: 100%;"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemStyle Wrap="True" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="unidad" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center"
                                                            HeaderText="Unidad" HeaderStyle-Width="4%" ReadOnly="true" />
                                                        <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                                            FooterStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton1" Text='<i class="fa fa-pen"></i>' ToolTip="Editar"
                                                                    runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" Enabled='<%# Eval("habilitado") %>' />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton2" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                                    runat="server" OnClick="OnUpdateSesion" CssClass="btn btn-success btn-sm" />
                                                                <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                                    runat="server" OnClick="OnCancelSesion" CssClass="btn btn-info btn-sm" />
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnGrabar" runat="server" Text="" CssClass="d-none"></asp:LinkButton>
                    <asp:UpdatePanel ID="updAccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">
                                Cancelar</button>
                            <button type="button" id="btnValidar" runat="server" class="btn btn-success">
                                Guardar
                            </button>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
                    __doPostBack('btnGrabar', '');
                }
            } else {
                if (controlId.indexOf("LinkButton2") > -1 || controlId.indexOf("LinkButton5") > -1) {
                    $('#tabs').smartTab({ autoHeight: true });
                }
            }
        });
    </script>

</body>
</html>
