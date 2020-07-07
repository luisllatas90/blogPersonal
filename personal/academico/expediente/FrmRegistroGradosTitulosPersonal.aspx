<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRegistroGradosTitulosPersonal.aspx.vb"
    Inherits="FrmRegistroGradosTitulosPersonal" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Registro de Grados y titulos acádemicos del Personal USAT</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <link rel='stylesheet' href='../../assets/css/bootstrap.min.css?x=1' />
    <%-- ======================= Fecha y Hora =============================================--%>
    <link href="../../assets/css/font-awesome-datetimepicker.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Fecha y Hora =============================================--%>

    <script src="../../assets/js/moment-with-locales.js?x=1" type="text/javascript"></script>

    <script src="../../assets/js/bootstrap-datetimepicker.js" type="text/javascript"></script>

    <%-- ======================= Lista desplegable con busqueda =============================================--%>

    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            Calendario();
        })
        function MostrarModal(div) {
            $("#" + div).modal('show');
        }
        function Calendario() {
            $('#datetimepicker1').datetimepicker({
                locale: 'es',
                format: 'L'
                
            });           
        }
        function initCombo(id) {
            var options = {
                noneSelectedText: '-- Seleccione --',
            };

            $('#' + id).selectpicker(options);
            $("#ddlPrograma").selectpicker();
        }


    </script>

    <style type="text/css">
        table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }
        .dropdown-menu open
        {
            min-width: 0px;
            max-width: 500px;
        }
    </style>
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading">
                <b>Grados y Titulos académicos </b>
            </div>
            <div class="panel-body">
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" CssClass="col-sm-2 col-md-1 control-label">Tipo</asp:Label>
                        <div class="col-sm-2 col-md-2">
                            <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control" AutoPostBack="true">
                                <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                <asp:ListItem Value="1">DOCENTE</asp:ListItem>
                                <asp:ListItem Value="2">ADMINISTRATIVO</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:UpdatePanel runat="server" ID="updDepartamentoArea">
                            <ContentTemplate>
                                <asp:Label ID="lblDepartamentoArea" runat="server" CssClass="col-sm-2 col-md-2 control-label">Área/Departamento</asp:Label>
                                <div class="col-sm-4 col-md-5">
                                    <asp:DropDownList runat="server" ID="ddlDepartamentoArea" CssClass="form-control"
                                        AutoPostBack="true">
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="selectedindexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel runat="server" ID="updListaPersonal">
                            <ContentTemplate>
                                <asp:Label ID="lblPersonal" runat="server" CssClass="col-sm-2 col-md-1 control-label">Colaborador</asp:Label>
                                <div class="col-sm-4 col-md-6">
                                    <asp:DropDownList runat="server" ID="ddlPersonal" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="">[-- SELECCIONE --]</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoArea" EventName="selectedindexchanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="selectedindexchanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <br />
                <asp:UpdatePanel runat="server" ID="updListaGradosTitulosPërsonal" UpdateMode="Conditional"
                    ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-sm-8 col-md-4">
                                    <asp:CheckBox runat="server" ID="chkEducacion" Text="&nbsp;Educación completa en el Perú"
                                        AutoPostBack="true" />
                                </div>
                                <div class="col-sm-4 col-md-2">
                                    <asp:Button runat="server" ID="btnNuevo" CssClass="btn btn-sm btn-success" Text="Nuevo" />
                                </div>
                            </div>
                        </div>
                        <asp:UpdatePanel runat="server" ID="updListaGrados" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="form-group">
                                    <asp:GridView runat="server" ID="gvGradosTitulosPersonal" CssClass="table table-condensed"
                                        DataKeyNames="codigo,tipoGradoTitulo,codigo_per,codigogra_fad,validado" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="TipoGradoTitulo" HeaderText="TIPO" HeaderStyle-Width="11%" />
                                            <asp:BoundField DataField="GradoTitulo" HeaderText="GRADO/TÍTULO" HeaderStyle-Width="30%" />
                                            <asp:BoundField DataField="Mencion" HeaderText="MENCIÓN" HeaderStyle-Width="25%" />
                                            <asp:BoundField DataField="descripcion_sit" HeaderText="SITUACIÓN" HeaderStyle-Width="5%" />
                                            <asp:BoundField DataField="nombre_Ins" HeaderText="INSTITUCIÓN" HeaderStyle-Width="20%" />
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEditar" runat="server" Text='<span class="fa fa-pencil-square-o"></span>'
                                                        CssClass="btn btn-info btn-sm" ToolTip="Editar" CommandName="Editar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ShowHeader="false" HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnValidar" runat="server" Text='<span class="fa fa-check-circle"></span>'
                                                        CommandName="Validar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-success btn-sm" ToolTip="Validar" OnClientClick="return confirm('¿Esta seguro que desea validar el registro?')">
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ItemStyle-VerticalAlign="Middle"
                                                ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnVincular" runat="server" Text='<span class="fa fa-link"></span>'
                                                        CssClass="btn btn-primary btn-sm" ToolTip="Vincular" CommandName="Vincular" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ItemStyle-VerticalAlign="Middle"
                                                ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEliminar" runat="server" Text='<span class="fa fa-trash"></span>'
                                                        CssClass="btn btn-danger btn-sm" ToolTip="Eliminar" CommandName="Eliminar" OnClientClick="return confirm('¿Esta seguro que desea eliminar el registro?')"
                                                        CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataTemplate>
                                            <b>No cuenta con Grados y titulos Registrados en el Sistema</b>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnGuardarRegistro" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div class="modal fade" id="mdVincular" role="dialog" aria-labelledby="myModalLabel"
                            style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-lg" id="Div19">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H6">
                                            Vincular Formación académica docente</h4>
                                    </div>
                                    <div class="modal-body">
                                        <asp:GridView runat="server" ID="gvFormacionAcademica" CssClass="table table-condensed"
                                            DataKeyNames="codigo_gra,vinculado" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnSeleccionarVinculo" CssClass="btn btn-warning btn-sm"
                                                            ToolTip="Seleccionar Vínculo" CommandName="Seleccionar" CommandArgument="<%#Convert.ToString(Container.DataItemIndex)%>"
                                                            Text='<span class="fa fa-hand-o-right"></span>' OnClientClick="return confirm('¿Esta seguro que desea seleccionar vinculo?')"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="descripcion_test" HeaderText="NIVEL" HeaderStyle-Width="4%" />
                                                <asp:BoundField DataField="nombre_pro" HeaderText="PROGRAMA" HeaderStyle-Width="24%" />
                                                <asp:BoundField DataField="nombreExtPro_gra" HeaderText="DETALLE PROGRAMA" HeaderStyle-Width="24%" />
                                                <asp:BoundField DataField="nombre_uni" HeaderText="INSTITUCIÓN" HeaderStyle-Width="24%" />
                                                <asp:BoundField DataField="nombreExtUni_gra" HeaderText="DETALLE INSTITUCIÓN" HeaderStyle-Width="24%" />
                                            </Columns>
                                            <HeaderStyle Font-Size="12px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                            <RowStyle Font-Size="12px" />
                                            <EmptyDataTemplate>
                                                <b>No cuenta registros de formación académica docente</b>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <button type="button" class="btn btn-danger" id="btnCancelar" data-dismiss="modal"
                                                aria-label="Close">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal fade" id="mdEditar" role="dialog" aria-labelledby="myModalLabel"
                            style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                            <div class="modal-dialog modal-lg" id="Div2">
                                <div class="modal-content">
                                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                        </button>
                                        <h4 class="modal-title" id="H1">
                                            Mantenimiento de Grado/Título</h4>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <asp:HiddenField runat="server" ID="hdt" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdfa" Value="0" />
                                                <asp:HiddenField runat="server" ID="hdc" Value="0" />
                                                <asp:Label ID="Label12" runat="server" CssClass="col-sm-2 col-md-3 control-label">Tipo de Institución</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <asp:DropDownList runat="server" ID="ddlTipoInstitucion" CssClass="form-control"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="Label13" runat="server" CssClass="col-sm-2 col-md-2 control-label">Modalidad</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <asp:DropDownList runat="server" ID="ddlModalidad" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="updPrograma" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" CssClass="col-sm-2 col-md-3 control-label">Nivel Programa</asp:Label>
                                                        <div class="col-sm-4 col-md-4">
                                                            <asp:DropDownList runat="server" ID="ddlTipoPrograma" CssClass="form-control" AutoPostBack="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" id="TipoProgramaPregrado" visible="false">
                                                        <asp:Label ID="Label10" runat="server" CssClass="col-sm-2 col-md-3 control-label">Tipo</asp:Label>
                                                        <div class="col-sm-4 col-md-4">
                                                            <asp:DropDownList runat="server" ID="ddlTipoProgramaPregrado" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 col-md-3 control-label">Programa</asp:Label>
                                                        <div class="col-sm-8 col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlPrograma" CssClass="form-control selectpicker"
                                                                AutoPostBack="true" data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" runat="server" visible="False" id="divProgramaEx">
                                                        <asp:Label ID="Label4" runat="server" CssClass="col-sm-2 col-md-3 control-label">Nombre de Programa</asp:Label>
                                                        <div class="col-sm-8 col-md-8">
                                                            <asp:TextBox runat="server" ID="txtNombreProgramaEx" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" CssClass="col-sm-2 col-md-3 control-label">Institución</asp:Label>
                                                        <div class="col-sm-8 col-md-8">
                                                            <asp:DropDownList runat="server" ID="ddlUniversidad" CssClass="form-control selectpicker"
                                                                AutoPostBack="true" data-live-search="true">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoPrograma" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlPrograma" EventName="SelectedIndexChanged" />
                                                    <asp:AsyncPostBackTrigger ControlID="ddlTipoInstitucion" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="conditional">
                                                <ContentTemplate>
                                                    <div class="form-group" runat="server" visible="False" id="divUniversidadEx">
                                                        <asp:Label ID="Label6" runat="server" CssClass="col-sm-2 col-md-3 control-label">Nombre de Institución</asp:Label>
                                                        <div class="col-sm-8 col-md-8">
                                                            <asp:TextBox runat="server" ID="txtNombreUniversidadEx" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlUniversidad" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <asp:Label ID="Label9" runat="server" CssClass="col-sm-2 col-md-3 control-label">Situación de Grado/Título</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <asp:DropDownList runat="server" ID="ddlSituacion" CssClass="form-control" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel runat="server" ID="updSituacion" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" CssClass="col-sm-2 col-md-3 control-label">Año Ingreso</asp:Label>
                                                        <div class="col-sm-2 col-md-2">
                                                            <asp:TextBox runat="server" ID="txtAnioIngreso" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                        <asp:Label ID="Label8" runat="server" CssClass="col-sm-2 col-md-2 control-label">Año Egreso</asp:Label>
                                                        <div class="col-sm-2 col-md-2">
                                                            <asp:TextBox runat="server" ID="txtAnioEgreso" CssClass="form-control" MaxLength="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <asp:Label ID="Label11" runat="server" CssClass="col-sm-2 col-md-3 control-label">Fecha de Emisión</asp:Label>
                                                        <div class="col-sm-3 col-md-3">
                                                            <div class="input-group date" id="datetimepicker1">
                                                                <asp:TextBox runat="server" ID="txtFechaEmision" CssClass="form-control"></asp:TextBox>
                                                                <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlSituacion" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <center>
                                            <asp:Button runat="server" ID="btnGuardarRegistro" CssClass="btn btn-success" Text="Guardar" />
                                            <button type="button" class="btn btn-danger" id="Button1" data-dismiss="modal" aria-label="Close">
                                                Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipo" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlDepartamentoArea" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPersonal" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="gvGradosTitulosPersonal" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="gvFormacionAcademica" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnNuevo" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
