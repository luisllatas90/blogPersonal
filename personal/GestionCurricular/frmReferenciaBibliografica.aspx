<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmReferenciaBibliografica.aspx.vb"
    Inherits="GestionCurricular_frmReferenciaBibliografica" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Referencias Bibliográficas</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var nombre = $('#txtNombre').val();
                if (nombre.trim() == '') {
                    alert("¡ Ingrese Nombre !");
                    $('#txtNombre').focus();
                    return false;
                }
            });
            $('#btnListar').click(function() {
                return fc_validarCombos();
            });
            $('#chkTipo').click(function() {
                if ($(this).is(':checked')) {
                    $("#txtObservacion").removeAttr("disabled");
                    //$("#txtObservacion").focus();
                    document.getElementById('txtObservacion').select();
                } else {
                    $('#txtObservacion').attr("disabled", "disabled");
                }
                console.log(txt.style.visibility);
            });
        });

        function openModal(accion) {
            if (accion == 'Agregar') {
                $('#hdcodigo_ref').val('');
                $('#txtNombre').val(' ');
                $('#txtObservacion').val('');
                $('#txtObservacion').attr("disabled", "disabled");
                $('#chkTipo').prop('checked', false);
            }
            $('#myModal').modal('show');
            document.getElementById('txtNombre').select();
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

        function fc_validarCombos() {
            var cboCA = document.getElementById('<%=cboSemestre.ClientID%>');
            if (cboCA.selectedIndex < 1) {
                //alert('¡ Seleccione Semestre !');
                ShowMessage('¡ Seleccione Semestre !', 'Warning');
                cboCA.focus();
                return false;
            }
            var cboCP = document.getElementById('<%=cboCarrProf.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                //alert('¡ Seleccione Carrera Profesional !');
                ShowMessage('¡ Seleccione Carrera Profesional !', 'Warning');
                cboCP.focus();
                return false;
            }
            var cboPE = document.getElementById('<%=cboPlanEstudio.ClientID%>');
            if (cboPE.selectedIndex < 1) {
                //alert('¡ Seleccione Plan Estudio !');
                ShowMessage('¡ Seleccione Plan Estudio !', 'Warning');
                cboPE.focus();
                return false;
            }
            var cboDA = document.getElementById('<%=cboDisApr.ClientID%>');
            if (cboDA.selectedIndex < 1) {
                //alert('¡ Seleccione Diseño Asignatura !');
                ShowMessage('¡ Seleccione Diseño Asignatura !', 'Warning');
                cboDA.focus();
                return false;
            }
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
                <div class="row">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-left"></i></span> Volver
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-10">
                        <h4>
                            <label id="lblCurso" runat="server">
                                Registrar Referencias Bibliográficas</label></h4>
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
                            <label class="col-xs-4 control-label">
                                Semestre:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-5">
                        <div class="form-group form-group-sm">
                            <label class="col-xs-4 control-label">
                                Carrera Profesional:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-4">
                        <div class="form-group form-group-sm">
                            <label class="col-md-4 control-label">
                                Plan Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboPlanEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-xs-2">
                        <%--<asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info"/>--%>
                        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-success">
                            <span><i class="fa fa-plus"></i></span> Agregar
                        </asp:LinkButton>
                    </div>
                    <div class="col-xs-7">
                        <div class="form-group">
                            <label id="lblAsignatura" runat="server" class="col-md-4">
                                Diseño Aprendizaje:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboDisApr" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-xs-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvReferencia" runat="server" Width="99%" AutoGenerateColumns="false"
                                ShowHeader="true" DataKeyNames="codigo_ref, descripcion_ref, codigo_tip, nombre, tipo_ref, observacion_ref"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="codigo_ref" HeaderText="Código" Visible="false" />
                                    <asp:BoundField DataField="nombre" HeaderText="Tipo de Referencia" HeaderStyle-Width="15%" />
                                    <asp:BoundField DataField="descripcion_ref" HeaderText="Referencia Bibliográfica"
                                        HeaderStyle-Width="30%" />
                                    <asp:BoundField DataField="observacion_ref" HeaderText="Enlace de Internet" HeaderStyle-Width="30%"
                                        ItemStyle-Wrap="true" ItemStyle-Width="50" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acciones" HeaderStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click"
                                                CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea editar esta Referencia Bibliográfica?');">
                                            <span><i class="fa fa-pen"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Desea eliminar esta Referencia Bibliográfica?');">
                                            <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos!
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
            <div class="panel panel-body">
                <div class="row">

                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro de Referencia Bibliografica -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Referencia Bibliográficas</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Tipo:
                                </label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="cboTipo" runat="server" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Referencia:
                                </label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"
                                        Rows="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <div class="col-md-3">
                                    <asp:CheckBox ID="chkTipo" runat="server" Text="Enlace Net:" />
                                </div>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm"
                                        TextMode="MultiLine" Rows="5"></asp:TextBox>
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
    </form>
</body>
</html>
