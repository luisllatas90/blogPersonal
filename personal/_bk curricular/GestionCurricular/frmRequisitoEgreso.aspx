<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRequisitoEgreso.aspx.vb"
    Inherits="GestionCurricular_frmRequisitoEgreso" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Configurar Requisitos de Egreso</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../scripts/js/jquery-1.12.3.min.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../scripts/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            $('#btnListar').click(function() {
                return fc_validarCombos();
            });

            $('#btnEditar').click(function() {
                return fc_validarCombos();
            });

            $('#btnGrabar').click(function() {
                return fc_validarCombos();
            });
            $('#btnGuardar').click(function() {
                var txt = document.getElementById('<%=txtNombre.ClientID%>');
                var nombre = $('#txtNombre').val();
                if (txt.style.visibility == 'visible') {
                    if (nombre.trim() == '') {
                        alert("¡ Ingrese Nombre !");
                        txt.focus();
                        return false;
                    }
                }
                else {
                    var cboTR = document.getElementById('<%=cboTipoRequisito.ClientID%>');
                    if (cboTR.selectedIndex < 0) {
                        alert('¡ Seleccione Requisito de Egreso !');
                        cboTR.focus();
                        return false;
                    }
                }
                var cant = $('#txtCantidad').val();
                if (cant.trim() == "") {
                    alert('¡ Ingrese Cantidad !');
                    $('#txtCantidad').focus();
                    return false;
                }
                var num = parseInt(cant.trim())
                if (num < 1 || num > 5) {
                    alert('¡ La cantidad debe ser como minimo 1 y maximo 5 !');
                    $('#txtCantidad').focus();
                    return false;
                }
            });

            $('#chkTipoRequisito').click(function() {
                var txt = document.getElementById('<%=txtNombre.ClientID%>');
                var cbo = document.getElementById('<%=cboTipoRequisito.ClientID%>');
                if ($(this).is(':checked')) {
                    cbo.style.visibility = 'hidden';
                    txt.style.visibility = 'visible';
                    txt.focus();
                } else {
                    cbo.style.visibility = 'visible';
                    txt.style.visibility = 'hidden';
                    cbo.focus();
                }
                console.log(txt.style.visibility);
            });

        });

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
            var cboCP = document.getElementById('<%=cboCarProf.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                //alert('¡ Seleccione Carrera Profesional !');
                ShowMessage('¡ Seleccione Carrera Profesional !', 'Warning');
                cboCP.focus();
                return false;
            }

            var cboPC = document.getElementById('<%=cboPlanCurr.ClientID%>');
            if (cboPC.selectedIndex < 0) {
                //alert('¡ Seleccione Plan Curricular !');
                ShowMessage('¡ Seleccione Plan Curricular !', 'Warning');
                cboPC.focus();
                return false;
            }
        }

        function openModal(accion) {
            var txt = document.getElementById('<%=txtNombre.ClientID%>');
            txt.style.visibility = 'hidden';
            if (accion == 'Agregar') {
                //$('#hdcodigo_tip').val('');
                $('#txtNombre').val('');
                $('#cboTipoRequisito').val('');
                $('#txtCantidad').val('1');
                $("#cboEstado").attr('disabled', true);
            }
            $('#myModal').modal('show');
            //$('#txtNombre').focus();
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
        
    </script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Requisito Egreso -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Configurar Requisitos de Egreso</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-3">
                                Plan Curricular:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboPlanCurr" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <%-- <div class="col-md-6">
                        <div class="btn-group">
                            <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info" />
                            <asp:Button ID="btnEditar" runat="server" Text="Editar | Agregar" CssClass="btn btn-warning" />
                            <asp:Button ID="btnGrabar" runat="server" Text="Grabar" CssClass="btn btn-success" />
                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-danger"
                                OnClientClick="return confirm('¿Desea cancelar la operación ?');" />
                        </div>
                    </div>--%>
                    <div class="col-md-2">
                        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-success">
                            <span><i class="fa fa-plus"></i></span> Agregar
                        </asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvRequisitoEgreso" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_req, codigo_tip, codigo_pcur, cantidad, nombre"
                            CssClass="table table-bordered table-hover">
                            <Columns>
                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="codigo_tip" HeaderText="Código" Visible="false" />
                                <asp:BoundField DataField="cantidad" HeaderText="Cantidad" />
                                <asp:BoundField DataField="nombre" HeaderText="Requisito de Egreso" HeaderStyle-Width="50%" />
                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Cantidad">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-sm" Text='<%# Eval("cantidad") %>' Enabled="false"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea Editar el Requisito de Egreso?');">
                                            <span><i class="fa fa-pen"></i></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Desea Eliminar el Requisito de Egreso?');">
                                            <span><i class="fa fa-trash"></i></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron Requisitos de Egreso
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
    <!-- Modal Registro de Tipo Requisito de Egreso -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registrar Requisito de Egreso</h4>
                </div>
                <div class="modal-body">
                    <%--<asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>--%>
                    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="messagealert" id="divAlertModal" runat="server" visible="false">
                                <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                    class="alert alert-warning">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                        id="lblMensaje" runat="server"></span>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Tipo Requisito:
                                </label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboTipoRequisito" runat="server" CssClass="form-control input-sm"
                                        Style="position: absolute">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                </label>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="chkTipoRequisito" runat="server" CssClass="form-control input-sm no-border"
                                        Text=" Agregar" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Cantidad:
                                </label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control input-sm" onKeyPress="return soloNumeros(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Estado:
                                </label>
                                <div class="col-md-6">
                                    <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                    <%--</ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chkTipoRequisito" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:LinkButton ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
