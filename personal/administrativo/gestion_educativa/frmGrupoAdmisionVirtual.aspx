<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGrupoAdmisionVirtual.aspx.vb" 
    Inherits="administrativo_gestion_educativa_frmGrupoAdmisionVirtual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Regitro de Grupo Evaluación</title>
    
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../../assets/tempusdominus-bootstrap-3/css/tempusdominus-bootstrap-3.min.css">
    
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../assets/js/popper.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    <script src="../../assets/moment/moment-with-locales.js"></script>
    <script src="../../assets/tempusdominus-bootstrap-3/js/tempusdominus-bootstrap-3.js"></script>

    <style>
        .dropdown-toggle .filter-option 
        {
            margin-top: -2px;
            position: relative !important;
        }
        .no-gutters {
            margin-right: 0;
            margin-left: 0;
        }

        .no-gutters > [class*="col-"] {
            padding-right: 0;
            padding-left: 0;
        }
        .usat-div
        {
            margin: 0px;	
        }
        .usat-col
        {
            padding: 5px;	
        }
    </style>
    
    <script type="text/javascript">

        jQuery(document).ready(function() {
            $('#cboCentroCosto2').selectpicker({
                liveSearch: true,
                size: 10,
                noneSelectedText: 'Nada seleccionado'
            });

            $('#btnGrabar').click(function() {
                var cod = $('#txtCodigo').val();
                if (cod.trim() == '') {
                    alert("¡ Ingrese Codigo !");
                    $('#txtCodigo').focus();
                    return false;
                }
                var nom = $('#txtNombre').val();
                if (nom.trim() == '') {
                    alert("¡ Ingrese Nombre !");
                    $('#txtNombre').focus();
                    return false;
                }
                var cboTip = document.getElementById('<%=cboTipoGrupoEval.ClientID%>');
                if (cboTip.selectedIndex < 1) {
                    alert('¡ Seleccione un Tipo de Evaluación !');
                    cboTip.focus();
                    return false;
                }
                var cbo = document.getElementById('<%=cboAmbiente.ClientID%>');
                if (cbo.selectedIndex < 1) {
                    alert('¡ Seleccione Ambiente !');
                    //ShowMessage('¡ Seleccione Carrera Profesional !', 'Warning');
                    cbo.focus();
                    return false;
                }
                var cap = $('#txtCapacidad').val();
                if (cap.trim() == '') {
                    alert("¡ Ingrese Capacidad !");
                    $('#txtCapacidad').focus();
                    return false;
                }
            });

            $('#btnFakeGrabar').on('click', function (e) {  
                $('#btnGrabar').trigger('click');
            });

            initDateTimePickers();
        });

        function initDateTimePickers() {  
            $('#pkFecha').datetimepicker({
                format: 'DD/MM/YYYY',
            });

            $('#pkHoraInicio').datetimepicker({
                format: 'HH:mm',
            });

            $('#pkHoraFin').datetimepicker({
                format: 'HH:mm',
            });
        }

        function openModal(id, accion, timeout) {
            switch (id) {
                case "gru":
                    var nombre = $('#txtCodigo').val();
                    if (nombre.trim() == '') {
                        $('#txtCodigo').val(' ');
                        $('#txtFecha').val('');
                        $('#txtHoraInicio').val('');
                        $('#txtHoraFin').val('');
                    }
                    document.getElementById('txtCodigo').select();

                    if (timeout == undefined) {
                        timeout = 0;
                    }
                    setTimeout(function () {  
                        $('#myModal').modal('show');
                    }, timeout);
                    break;
                case "cco":
                    $('#myModalCco').modal('show');
                    break;
                case "res":
                    $('#myModalRes').modal('show');
                    break;
            }
        }

        function closeModal(id) {
            switch (id) {
                case "gru":
                    $('#myModal').modal('hide');
                    break;
                case "cco":
                    $('#myModalCco').modal('hide');
                    break;
                case "res":
                    $('#myModalRes').modal('hide');
                    break;
            }
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

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading usat-div">
                <h4>Registro de Grupo Evaluación</h4>
            </div>
            <div class="panel panel-body usat-div">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="form-group">
                            <label class="col-xs-2 form-control-sm usat-col">Tipo Evaluación: </label>
                            <div class="col-xs-2 usat-col">
                                <asp:DropDownList ID="cboFiltroTipoEva" runat="server" CssClass="form-control input-sm" 
                                    AutoPostBack="true" data-live-search="true">
                                </asp:DropDownList>
                            </div>
                            <label class="col-xs-2 form-control-sm usat-col">Centro de Costo:</label>
                            <div class="col-xs-4 usat-col">
                                <asp:ListBox ID="cboCentroCosto2" runat="server" AutoPostBack="true" SelectionMode="Multiple" 
                                    CssClass="form-control form-control-sm selectpicker">
                                </asp:ListBox>
                            </div>
                            <div class="col-xs-2 usat-col">
                                <asp:LinkButton ID="btnAgregar" runat="server" Text='<i class="fa fa-plus"></i> Nuevo Grupo'
                                    CssClass="btn btn-success btn-sm" OnClick="btnAgregar_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGrupo" runat="server" AutoGenerateColumns="false" 
                            DataKeyNames="codigo_gru, codigo_cco, codigo, nombre, codigo_amb, virtual_amb, capacidad, codigo_tge, fechaHoraInicio_gru, fechaHoraFin_gru"
                            CssClass="table table-sm table-bordered table-hover" GridLines="None">
                                <Columns>
                                    <asp:BoundField DataField="codigo" HeaderText="Código"/>
                                    <asp:BoundField DataField="nombre" HeaderText="Nombre"/>
                                    <asp:BoundField DataField="nombre_tge" HeaderText="Tipo Evaluación"/>
                                    <asp:BoundField DataField="nombre_amb" HeaderText="Ambiente"/>
                                    <asp:TemplateField HeaderText="Virtual" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><asp:CheckBox ID="chkVirtual" runat="server" Checked='<%# Eval("virtual_amb") %>' Enabled ="false" /></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="cap_dis" HeaderText="Asignados / Capacidad" ItemStyle-HorizontalAlign="Center"/>
                                    <asp:TemplateField HeaderText="Aula Activa" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate><asp:CheckBox ID="chkAula" runat="server" Checked='<%# Eval("aula_activa") %>' Enabled ="false"/></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Centro de Costos" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnCentroCosto" runat="server" CommandName="CentroCosto" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CssClass="btn btn-warning btn-sm" ToolTip = "Centro de Costos">
                                                <span><i class="fa fa-bars"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Responsables" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnResponsable" runat="server" CommandName="Responsable" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CssClass="btn btn-info btn-sm" ToolTip = "Responsables">
                                                <span><i class="fa fa-users"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CssClass="btn btn-primary btn-sm" ToolTip = "Editar"
                                                OnClientClick="return confirm('¿ Desea editar el registro ?');">
                                                <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CssClass="btn btn-danger btn-sm" ToolTip = "Eliminar"
                                                OnClientClick="return confirm('¿ Desea eliminar el registro ?');">
                                                <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate> No se encontró ningún registro </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EditRowStyle BackColor="#FFFFCC" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Modal para Registro de Grupos -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Grupos de Evaluación</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Código:</label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Nombre:</label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Tipo:</label>
                                        <div class="col-xs-6">
                                            <asp:RadioButtonList ID="rbTipo" runat="server" OnSelectedIndexChanged="rbTipo_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem class="radio-inline" Value="0" Text="Físico" Selected="True"></asp:ListItem>
                                                <asp:ListItem class="radio-inline" Value="1" Text="Virtual"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Tipo Evaluación:</label>
                                        <div class="col-xs-6">
                                            <asp:DropDownList ID="cboTipoGrupoEval" runat="server" CssClass="form-control input-sm" data-live-search="true"> 
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Fecha:</label>
                                        <div class="col-sm-6">
                                            <div class="input-group date" id="pkFecha" data-target-input="nearest">
                                                <input type="text" id="txtFecha" runat="server" class="form-control input-sm datetimepicker-input" data-target="#pkFecha"/>
                                                <span class="input-group-addon" data-target="#pkFecha" data-toggle="datetimepicker">
                                                    <span class="glyphicon glyphicon-calendar"></span>
                                                </span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Hora:</label>
                                        <div class="col-sm-6">
                                            <div class="row no-gutters">
                                                <div class="col-sm-6">
                                                    <div class="input-group date" id="pkHoraInicio" data-target-input="nearest">
                                                        <input type="text" id="txtHoraInicio" runat="server" class="form-control input-sm datetimepicker-input" data-target="#pkHoraInicio"/>
                                                        <span class="input-group-addon" data-target="#pkHoraInicio" data-toggle="datetimepicker">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6">
                                                    <div class="input-group date" id="pkHoraFin" data-target-input="nearest">
                                                        <input type="text" id="txtHoraFin" runat="server" class="form-control input-sm datetimepicker-input" data-target="#pkHoraFin"/>
                                                        <span class="input-group-addon" data-target="#pkHoraFin" data-toggle="datetimepicker">
                                                            <span class="glyphicon glyphicon-time"></span>
                                                        </span>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Ambiente:</label>
                                        <div class="col-xs-6">
                                            <asp:DropDownList ID="cboAmbiente" runat="server" CssClass="form-control input-sm" AutoPostBack="true" data-live-search="true"> 
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-4">Capacidad:</label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txtCapacidad" runat="server" CssClass="form-control input-sm" onKeyPress="return soloNumeros(event)"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <button type="button" id="btnFakeGrabar" class="btn btn-success">Guardar</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-success hide" />
                </div>
            </div>
        </div>
    </div>
    
    <!-- Modal para Registro de Centro Costo-->
    <div id="myModalCco" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBodyCco">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar de Centro de Costos</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panCentroCosto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <label class="col-sm-3">Centro Costo:</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="cboCentro" runat="server" CssClass="form-control input-sm" data-live-search="true"> 
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnAdd2" runat="server" Text='<i class="fa fa-plus"></i>'
                                            CssClass="btn btn-success btn-sm"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvCentroCosto" runat="server" AutoGenerateColumns="false" 
                                         DataKeyNames="codigo_gcc, codigo_gru, codigo_cco, estado_gcc, descripcion_Cco, fechainiciopropuesta_dev" 
                                         CssClass="table table-sm table-bordered table-hover" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="codigo_cco" HeaderText="Código"/>
                                                <asp:BoundField DataField="descripcion_Cco" HeaderText="Descripción"/>
                                                <asp:BoundField DataField="abreviatura_cco" HeaderText="Abreviatura"/>
                                                <asp:TemplateField HeaderText="Quitar" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete2" runat="server" CommandName="Quitar2" 
                                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                            CssClass="btn btn-danger btn-sm" ToolTip = "Quitar">
                                                            <span><i class="fa fa-trash"></i></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate> No se encontró ningún registro </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
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
                    <button type="button" id="Button1" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGrabar2" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    
    <!-- Modal para Registro de Responsables-->
    <div id="myModalRes" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBodyResp">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar de Responsables</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panResponsable" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-sm-12">
                                    <label class="col-sm-3">Responsable:</label>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="cboResponsable" runat="server" CssClass="form-control input-sm" data-live-search="true"> 
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-1">
                                        <asp:LinkButton ID="btnAdd" runat="server" Text='<i class="fa fa-plus"></i>'
                                            CssClass="btn btn-success btn-sm"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-sm-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvResponsable" runat="server" AutoGenerateColumns="false" 
                                         DataKeyNames="codigo_gre, codigo_gru, codigo_per, estado_gre, Personal" 
                                         CssClass="table table-sm table-bordered table-hover" GridLines="None">
                                            <Columns>
                                                <asp:BoundField DataField="codigo_per" HeaderText="Código" Visible="false"/>
                                                <asp:BoundField DataField="nroDocIdentidad_Per" HeaderText="DNI"/>
                                                <asp:BoundField DataField="Personal" HeaderText="Apellidos y Nombres"/>
                                                <asp:TemplateField HeaderText="Quitar" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Quitar" 
                                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                            CssClass="btn btn-danger btn-sm" ToolTip = "Quitar">
                                                            <span><i class="fa fa-trash"></i></span>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate> No se encontró ningún registro </EmptyDataTemplate>
                                            <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                            <RowStyle Font-Size="11px" />
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
                    <button type="button" id="Button2" class="btn btn-danger" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGrabar3" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    </form>

    <script>
        // Detecto actualizaciones en paneles (UpdatePanel)
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (sender, args) {
            var updatedPanels = args.get_panelsUpdated();

            for (var i = 0; i < updatedPanels.length; i++) {
                var udpPanelId = updatedPanels[i].id;
                switch (udpPanelId) {
                    case 'panModal': 
                        initDateTimePickers();
                        break;
                }
            }
        });
    </script>
</body>
</html>
