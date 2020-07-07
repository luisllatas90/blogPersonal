<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmConfiguracionCicloAcademico.aspx.vb"
    Inherits="GestionCurricular_frmConfiguracionCicloAcademico" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Configuración Ciclo Académico</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var cboTE = document.getElementById('<%=cboTipEst.ClientID%>');
                if (cboTE.selectedIndex == -1) {
                    alert("¡ Seleccione Tipo Estudio !");
                    cboTE.focus();
                    return false;
                }
                var cboSe = document.getElementById('<%=cboSemestre.ClientID%>');
                if (cboSe.selectedIndex == -1) {
                    alert("¡ Seleccione Semestre !");
                    cboSe.focus();
                    return false;
                }
                var nombre = $('#txtNombre').val();
                if (nombre == '') {
                    alert("¡ Ingrese Nombre !");
                    $('#txtNombre').focus();
                    return false;
                }
                var valor = $('#txtValor').val();
                if (valor == '') {
                    alert("¡ Ingrese Valor !");
                    $('#txtValor').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            if (accion == 'Agregar') {
                $('#hdcodigo_conf').val('');
                var cboTE = document.getElementById('<%=cboTipEst.ClientID%>');
                cboTE.selectedIndex = -1;
                var cboSe = document.getElementById('<%=cboSemestre.ClientID%>');
                cboSe.selectedIndex = -1;
                $('#txtNombre').val('');
                $('#txtValor').val('');
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
                    Configurar Ciclo Académico</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">
                                Tipo Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboTipoEstudio" runat="server" CssClass="form-control input-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">
                                Nombre:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control  input-sm"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info" />
                        <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvConfiguracion" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_conf, codigo_cac, codigo_test, nombre_conf, valor_conf"
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_conf" HeaderText="Código" Visible="false" />
                                <asp:BoundField DataField="descripcion_test" HeaderText="Tipo Estudio" />
                                <asp:BoundField DataField="descripcion_Cac" HeaderText="Semestre" />
                                <asp:BoundField DataField="nombre_conf" HeaderText="Nombre" />
                                <asp:BoundField DataField="valor_conf" HeaderText="Cantidad Semanas" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click"
                                            CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-warning btn-sm" OnClientClick="return confirm('¿Desea editar esta Configuración de Semestre?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontró ningún ciclo académico
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
                        Registro de Configuración de Semestre</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Tipo Estudio:
                                </label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboTipEst" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">
                                    Semestre:
                                </label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
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
                                    Nro. Semanas:
                                </label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtValor" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
    <asp:HiddenField ID="hdcodigo_conf" runat="server" />
    </form>
</body>
</html>
