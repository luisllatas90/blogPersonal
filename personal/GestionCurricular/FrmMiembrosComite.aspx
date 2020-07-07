<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMiembrosComite.aspx.vb"
    Inherits="GestionCurricular_FrmMiembrosComite" %>

<!DOCTYPE html>
<html lang="es">
<head id="Head1" runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Comité Curricular</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#' + "<%=fuArchivo.ClientID%>").attr('accept', 'application/pdf');
            $('#txtIniAprobacion').datepicker({ autoclose: true });
            $('#txtFinAprobacion').datepicker({ autoclose: true });

            /*
            $('#btnAceptar').click(function() {
                
            if ($('#txtNombre').val() == '') {
            alert("Ingrese el nombre del comité");
            $('#txtNombre').focus();
            return false;
            }

            if ($('#txtIniAprobacion').val() == '') {
            alert("Seleccione fecha de inicio de aprobación");
            $('#txtIniAprobacion').focus();
            return false;
            }

            if ($('#txtFinAprobacion').val() == '') {
            alert("Seleccione fecha de término de aprobación");
            $('#txtFinAprobacion').focus();
            return false;
            }

            if ($('#ddlSemestre').val() == '-1') {
            alert("Seleccione el semestre de aprobación");
            $('#ddlSemestre').focus();
            return false;
            }

            if ($('#txtNroDecreto').val() == '') {
            alert("Ingrese el Nro de Resolución");
            $('#txtNroDecreto').focus();
            return false;
            }

            var txtIni = ($('#txtIniAprobacion').val()).split('/');
            var txtFin = ($('#txtFinAprobacion').val()).split('/');

            var ini = new Date(txtIni[2], txtIni[1], txtIni[0]);
            var fin = new Date(txtFin[2], txtFin[1], txtFin[0]);
            if (ini > fin) {
            alert("La fecha de término de la aprobación no puede ser menor a la fecha de inicio");
            $('#txtFinAprobacion').focus();
            return false;
            }
                
            return true;
            });
            */
        });

        function openModal(acc, des) {
            $('#modalComite').modal('show');

            if (acc == 'nuevo') {
                $('#txtNombre').val(des);

                $("#spnFile").empty();
                $("#spnFile").text("No se eligió resolución");
            }
        }

        function closeModal(confirm) {
            if (confirm) {
                $('#modalComite').modal('hide');
                $("#modalComite").remove();

                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $(".modal-dialog").remove();
                $(".modal").remove();
            }
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

        function FileSelected(fu) {
            var fn = fu.value;
            $("#spnFile").empty();

            if (fn !== "") {
                var idx = fn.lastIndexOf("\\") + 1;
                fn = fn.substr(idx, fn.lenght);

                $("#hf").val("1");
                $("#spnFile").text(fn);
            } else {
                $("#hf").val("0");
                $("#spnFile").text("No se eligió resolución");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:HiddenField ID="hf" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registrar Comité Curricular</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-10">
                        <div class="form-group">
                            <label class="col-md-4" for="ddlCarreraProf">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList name="ddlCarreraProf" ID="ddlCarreraProf" runat="server" CssClass="form-control"
                                    AutoPostBack="true">
                                    <asp:ListItem Value=""></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <asp:LinkButton ID="btnCrear" runat="server" Text='<i class="fa fa-plus"></i> Crear Comité'
                            CssClass="btn btn-success" OnClick="btnCrear_Click"></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="panel panel-body">
        <div class="table-responsive">
            <asp:GridView ID="grwResultado" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_com, nombre_com, codigo_cpf, nombre_cpf, bloqueado, idArchivo"
                CssClass="table table-sm table-bordered table-hover" GridLines="None">
                <Columns>
                    <asp:BoundField DataField="nombre_com" HeaderText="NOMBRE DEL COMITÉ" HeaderStyle-Width="60%" />
                    <asp:BoundField DataField="ciclo_crea" HeaderText="SEMESTRE CREACIÓN" HeaderStyle-Width="15%" />
                    <asp:BoundField DataField="total_miembros" HeaderText="TOTAL MIEMBROS" HeaderStyle-Width="15%"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="EDITAR" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="RESOLUCIÓN" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <EmptyDataTemplate>
                    No se registró ningún comité
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="10px" />
                <EditRowStyle BackColor="#FFFFCC" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
            </asp:GridView>
        </div>
    </div>
    <!-- Modal -->
    <div id="modalComite" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true"
        data-backdrop="static" data-keyboard="false" aria-hidden="true" runat="server">
        <div class="modal-dialog modal-lg" role="document">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <span class="modal-title">Miembros de Comité</span>
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
                    <fieldset class="border p-1">
                        <legend class="w-auto">Aprobación</legend>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <label class="col-sm-3" for="txtNombre">
                                        Nombre del comité:</label>
                                    <div class="col-sm-9">
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm" Style="text-transform: uppercase;">
                                        </asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <label class="col-sm-3" for="txtIniAprobacion">
                                        Fecha inicio:</label>
                                    <div class="col-sm-3">
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtIniAprobacion" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                        </div>
                                    </div>
                                    <label class="col-sm-3" for="txtFinAprobacion">
                                        Fecha termino:</label>
                                    <div class="col-sm-3">
                                        <div class="input-group date">
                                            <asp:TextBox ID="txtFinAprobacion" runat="server" CssClass="form-control input-sm"
                                                data-provide="datepicker"></asp:TextBox>
                                            <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <label class="col-sm-3" for="ddlSemestre">
                                        Semestre:</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-sm-3" for="txtNroDecreto">
                                        Nro Resolución:</label>
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtNroDecreto" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <asp:UpdatePanel ID="updFileUpload" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <label class="col-sm-3" for="fuArchivo">
                                                Adjuntar Resolución:</label>
                                            <div class="col-sm-9">
                                                <label id="lblFuArchivo" runat="server" style="font-style: normal; font-size: small;
                                                    font-weight: normal">
                                                    <input id="btnFuArchivo" type="button" value="Seleccionar archivo" runat="server" />
                                                    <span id="spnFile" runat="server">No se eligió resolución</span>
                                                </label>
                                                <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" AllowMultiple="true"
                                                    Accept=".pdf" Style="display: none;" onChange="FileSelected(this);" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                    ErrorMessage="* Seleccione un archivo" ControlToValidate="fuArchivo" ValidationGroup="subirArchivo">
                                                </asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red"
                                                    runat="server" ValidationGroup="subirArchivo" ControlToValidate="fuArchivo" ValidationExpression="^([a-zA-Z].*|[1-9].*)\.(((p|P)(d|D)(f|F)))$">Solo archivos *.pdf</asp:RegularExpressionValidator>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    <fieldset class="border p-1">
                        <legend class="w-auto">Miembros</legend>
                        <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="table-responsive">
                                    <asp:UpdatePanel ID="udpComite" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                        <ContentTemplate>
                                            <asp:GridView ID="grwComite" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_mie, rol_mie, codigo_Per, codigo_com"
                                                CssClass="table table-sm table-bordered table-hover" GridLines="None" ShowHeadersWhenNoRecords="true"
                                                ShowFooter="True">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="MIEMBROS DEL COMITÉ" HeaderStyle-Width="70%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocente" runat="server" Text='<%#Eval("nombre_per")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlDocente" runat="server" CssClass="form-control form-control-sm"
                                                                data-size="10">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlNewDocente" runat="server" CssClass="form-control form-control-sm"
                                                                data-size="10">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                        <ItemStyle Wrap="True" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ROL" HeaderStyle-Width="18%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRol" runat="server" Text='<%#Eval("rol_mie")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="">[-- Seleccione --]</asp:ListItem>
                                                                <asp:ListItem Value="PRESIDENTE">PRESIDENTE</asp:ListItem>
                                                                <asp:ListItem Value="SECRETARIO">SECRETARIO</asp:ListItem>
                                                                <asp:ListItem Value="MIEMBRO">MIEMBRO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddlNewRol" runat="server" CssClass="form-control form-control-sm">
                                                                <asp:ListItem Value="">[-- Seleccione --]</asp:ListItem>
                                                                <asp:ListItem Value="PRESIDENTE">PRESIDENTE</asp:ListItem>
                                                                <asp:ListItem Value="SECRETARIO">SECRETARIO</asp:ListItem>
                                                                <asp:ListItem Value="MIEMBRO">MIEMBRO</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ACCIÓN" ShowHeader="False" HeaderStyle-Width="12%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Editar" CssClass="btn btn-primary btn-sm"
                                                                CausesValidation="False" CommandName="Edit" Text='<i class="fa fa-pen"></i>'></asp:LinkButton>
                                                            <span onclick="return confirm('¿Está seguro de eliminar al participante?')">
                                                                <asp:LinkButton ID="LinkButton2" runat="Server" ToolTip="Eliminar" CssClass="btn btn-danger btn-sm"
                                                                    OnClick="OnDelete" Text='<i class="fa fa-trash"></i>'></asp:LinkButton>
                                                            </span>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Actualizar" CssClass="btn btn-success btn-sm"
                                                                OnClick="OnUpdate" Text='<i class="fa fa-save"></i>'></asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton4" runat="server" ToolTip="Cancelar" CssClass="btn btn-info btn-sm"
                                                                OnClick="OnCancel" Text='<i class="fa fa-times"></i>'></asp:LinkButton>
                                                        </EditItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="LinkButton5" runat="server" ToolTip="Agregar" CssClass="btn btn-success btn-sm"
                                                                OnClick="OnNew" Text='<i class="fa fa-plus"></i>'></asp:LinkButton>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se registró ningún miembro
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="10px" />
                                                <EditRowStyle BackColor="#FFFFCC" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAceptar" runat="server" CssClass="d-none" Text="">
                    </asp:LinkButton>
                    <asp:UpdatePanel ID="updAccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnLimpiar" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar"
                                OnClientClick="closeModal(true);">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnLimpiar" runat="server" Text="">
                            </asp:LinkButton>
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
                    __doPostBack('btnAceptar', '');
                }
            }
        });
    </script>

</body>
</html>
