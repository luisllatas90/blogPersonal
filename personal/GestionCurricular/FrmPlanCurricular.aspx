<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmPlanCurricular.aspx.vb"
    Inherits="GestionCurricular_FrmPlanCurricular" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registro de aprobación del Plan Curricular</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

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
            $('#' + "<%=fuArchivoDecreto.ClientID%>").attr('accept', 'application/pdf');
            $('#' + "<%=fuArchivoPlan.ClientID%>").attr('accept', 'application/pdf');

            $('#btnAceptar').click(function() {
                if ($('#txtNombre').val() == '') {
                    alert("Ingrese un Nombre del Plan Curricular");
                    $('#txtNombre').focus();
                    return false;
                }

                if ($('#txtNroDecreto').val() == '') {
                    alert("Ingrese Número Decreto del Plan Curricular");
                    $('#txtNroDecreto').focus();
                    return false;
                }

                var file1 = $.trim($('#spnFileDecreto').html());
                if (file1 == 'No se eligió decreto' || file1 == '' || file1 == undefined) {
                    alert("Seleccione el archivo de decreto");
                    $('#btnFuArchivoDecreto').focus();
                    return false;
                }

                var file2 = $.trim($('#spnFilePlan').html());
                if (file2 == 'No se eligió el plan' || file2 == '' || file2 == undefined) {
                    alert("Seleccione el archivo del plan curricular");
                    $('#btnFuArchivoPlan').focus();
                    return false;
                }
            });
        });

        function openModal(acc, cod, vig) {
            $('#myModal').modal('show');
            $('#chkVigente').prop('checked', false);

            if (acc == "editar") {
                var check = (vig == 'True' || vig == 'true');
                $('#chkVigente').prop('checked', check);
            } else {
                $('#hdCodigoPlan').val('');
                $('#txtNombre').focus();
                $('#txtNroDecreto').val('');
                $('#chkVigente').prop('checked', true);
            }
        }

        function closeModal() {
            $('#hdCodigoPlan').val('');
            $('#hdCodigoCpf').val('');
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

        function FileSelected(fu, ctrl) {
            var fn = fu.value;
            $("#spnFile").empty();

            if (fn !== "") {
                var idx = fn.lastIndexOf("\\") + 1;
                fn = fn.substr(idx, fn.lenght);

                if (ctrl == "plan") {
                    $("#spnFilePlan").text(fn);
                } else {
                    $("#spnFileDecreto").text(fn);
                }
            } else {
                if (ctrl == "plan") {
                    $("#spnFilePlan").text("No se eligió plan");
                } else {
                    $("#spnFileDecreto").text("No se eligió decreto");
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <%--    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />--%>
    <!-- Listado Plan Curricular -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registro de aprobación del Plan Curricular</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
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
                    <div class="col-md-6">
                        <asp:LinkButton ID="btnAgregar" runat="server" Text='<i class="fa fa-plus"></i> Agregar'
                            CssClass="btn btn-success" Visible="false"></asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvPlanCurricular" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_cpf, nombre_cpf, nombre_pcur, vigente_pcur, codigo_pcur, archivoPlan, archivoDecreto"
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="nombre_pcur" HeaderText="Nombre del Plan Curricular" HeaderStyle-Width="60%" />
                                <asp:BoundField DataField="decreto" HeaderText="Decreto" HeaderStyle-Width="10%" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Vigente" HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkVigente" runat="server" Checked='<%# Eval("vigente_pcur") %>'
                                            Enabled="false" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%" />
                                <asp:BoundField HeaderText="Ver Decreto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                                <asp:BoundField HeaderText="Ver Plan" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="10%" />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron planes curriculares
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
    <!-- Modal Plan Curricular -->
    <div id="myModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro del Plan Curricular</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form_group row">
                                <label class="col-xs-2" for="lblCarrera">
                                    Carrera Profesional:</label>
                                <div class="col-xs-10">
                                    <asp:Label ID="lblCarrera" runat="server" Text="" CssClass="form-control input-sm"
                                        Enabled="false">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group row">
                                <label class="col-xs-2" for="txtNombre">
                                    Nombre:</label>
                                <div class="col-xs-10">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form_group row">
                                <label class="col-xs-2" for="txtNroDecreto">
                                    Nro Decreto:</label>
                                <div class="col-xs-10">
                                    <asp:TextBox ID="txtNroDecreto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group row">
                                <asp:UpdatePanel ID="updFileDecreto" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <label class="col-sm-2" for="fuArchivoDecreto">
                                            Adjuntar Decreto:</label>
                                        <div class="col-sm-10">
                                            <label id="lblFuArchivoDecreto" runat="server" style="font-style: normal; font-size: small;
                                                font-weight: normal">
                                                <input id="btnFuArchivoDecreto" type="button" value="Seleccionar archivo" runat="server" />
                                                <span id="spnFileDecreto" runat="server">No se eligió decreto</span>
                                            </label>
                                            <asp:FileUpload ID="fuArchivoDecreto" runat="server" CssClass="form-control input-sm"
                                                AllowMultiple="true" Accept=".pdf" Style="display: none;" onChange="FileSelected(this, 'decreto');" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                ErrorMessage="* Seleccione el decreto" ControlToValidate="fuArchivoDecreto" ValidationGroup="subirArchivo"
                                                Enabled="false">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                                                runat="server" ValidationGroup="subirArchivo" ControlToValidate="fuArchivoDecreto"
                                                ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">Solo archivos *.pdf</asp:RegularExpressionValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group row">
                                <asp:UpdatePanel ID="updFilePlan" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                    <ContentTemplate>
                                        <label class="col-sm-2" for="fuArchivoPlan">
                                            Adjuntar Plan:</label>
                                        <div class="col-sm-10">
                                            <label id="lblFuArchivoPlan" runat="server" style="font-style: normal; font-size: small;
                                                font-weight: normal">
                                                <input id="btnFuArchivoPlan" type="button" value="Seleccionar archivo" runat="server" />
                                                <span id="spnFilePlan" runat="server">No se eligió el plan</span>
                                            </label>
                                            <asp:FileUpload ID="fuArchivoPlan" runat="server" CssClass="form-control input-sm"
                                                AllowMultiple="true" Accept=".pdf" Style="display: none;" onChange="FileSelected(this, 'plan');" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                                                ErrorMessage="* Seleccione el plan curricular" ControlToValidate="fuArchivoPlan"
                                                ValidationGroup="subirArchivo" Enabled="false">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ForeColor="Red"
                                                runat="server" ValidationGroup="subirArchivo" ControlToValidate="fuArchivoPlan"
                                                ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">Solo archivos *.pdf</asp:RegularExpressionValidator>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12">
                            <div class="form-group row">
                                <div class="col-xs-2">
                                    &nbsp;
                                </div>
                                <div class="col-xs-10">
                                    <asp:CheckBox ID="chkVigente" runat="server" Text="Plan curricular vigente" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" Text="Guardar" CssClass="btn btn-success"
                        ValidationGroup="subirArchivo" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdCodigoPlan" runat="server" />
    <asp:HiddenField ID="hdCodigoCpf" runat="server" />
    </form>
</body>
</html>
