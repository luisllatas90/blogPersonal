<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmTipoReferencia.aspx.vb"
    Inherits="GestionCurricular_frmTipoReferencia" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Tipo de Referencia</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript" src="../scripts/js/jquery-1.12.3.min.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../scripts/js/bootstrap.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var nombre = $('#txtNombre').val();
                if (nombre == '') {
                    alert("¡ Ingrese Nombre !");
                    $('#txtNombre').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            if (accion == 'Agregar') {
                $('#hdcodigo_tip').val('');
                $('#txtNombre').val('');
                $("#cboEstado").attr('disabled', true);
            }
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
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Tipo Requisito Egreso -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registrar Tipo de Referencia</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Nombre:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control  input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info" />
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvTipoReferencia" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_tip, nombre, estado" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_tip" HeaderText="Código" />
                                <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Estado">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("estado") %>' Enabled="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click"
                                            CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-warning btn-sm" OnClientClick="return confirm('¿Desea editar este Tipo Referencia?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron tipo de referencias
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
    <!-- Modal Registro de Ubicacion Activo Fijo -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Tipo de Referencia</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Nombre:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Estado:
                                </label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdcodigo_tip" runat="server" />
    </form>
</body>
</html>
