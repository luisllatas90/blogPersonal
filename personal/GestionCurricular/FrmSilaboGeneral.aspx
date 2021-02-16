<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmSilaboGeneral.aspx.vb"
    Inherits="GestionCurricular_FrmSilaboGeneral" EnableEventValidation="false" Culture="auto"
    UICulture="auto" %>

<%--<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Gestión del Sílabo</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <!-- <link rel="stylesheet" type="text/css" href="../assets/bootstrap-select-1.12.2/bootstrap-select.css" />  -->
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <!-- <script src="../assets/bootstrap-select-1.12.2/bootstrap-select.js" type="text/javascript"></script> -->

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript" src="js/bootbox.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnBuscar').click(function() {
                if ($('#ddlSemestre').val() == '' || $('#ddlSemestre').val() == '-1') {
                    alert("Seleccione el Semestre");
                    $('#ddlSemestre').focus();
                    return false;
                }
            });
        });

        function openModal(acc, tip, title) {
            if (tip == "subirActa") {
                $('#myModalActa').modal('show');
                $('#lblTituloActa').text(title)
            } else {
                $('#myModalSesion').modal('show');

                if (acc == "nuevo") {
                    $('#hdCodigoDis').val('');
                    $('#hdCodigoCur').val('');
                    $('#hdCodigoCup').val('');
                }
            }
        }

        function closeModal() {
            $('#hdCodigoDis').val('');
            $('#hdCodigoCur').val('');
            $('#hdCodigoCup').val('');
            $('#myModalSesion').modal('hide');
            $('#myModalActa').modal('hide');
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

        function txtBuscar_onKeyPress(obj, e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE, Chrome
            else
                key = e.which; //firefox

            if (key == 13) {
                var btn = document.getElementById(obj);
                if (btn != null) {
                    //$('#btnBuscar').focus();
                    //$('#btnBuscar').click();
                    btn.click();
                    event.keyCode = 0
                }
            }
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
                $("#spnFile").text("No se eligió acta de exposición");
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:HiddenField ID="hdCodigoDis" runat="server" />
    <asp:HiddenField ID="hdCodigoCur" runat="server" />
    <asp:HiddenField ID="hdCodigoCup" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <%-- <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" AnchorControl=""
        /> --%>
    <!-- Listado de -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Gestión del Sílabo</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label class="col-xs-4">
                                Semestre:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-3">
                        <div class="form-group">
                            <label class="col-xs-3">
                                Estado:</label>
                            <div class="col-xs-9">
                                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True">
                                    <asp:ListItem Value="%" Selected="True" Text="[ --- Mostrar todos --- ]"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Con fecha de publicación"></asp:ListItem>
                                    <asp:ListItem Value="0" Text="Sin fecha de publicación"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-6" id="divCarrera" runat="server">
                        <div class="form-group">
                            <label class="col-xs-4">
                                Carrera Profesional:</label>
                            <div class="col-xs-8">
                                <asp:DropDownList ID="ddlCarreraProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <%-- <div class="row" id="divCarrera" runat="server">
                   
                </div>--%>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-7">
                        <asp:Panel ID="panBuscar" runat="server" DefaultButton="btnBuscar" Width="100%">
                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                        </asp:Panel>
                    </div>
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info">
                            <span><i class="fa fa-search"></i></span>
                        </asp:LinkButton>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvResultados" runat="server" Width="99%" AutoGenerateColumns="False"
                                ShowHeader="true" AllowPaging="True" PageSize="20" DataKeyNames="codigo_cur,nombre_cur,codigo_dis,fecha_apr,codigo_cup,descripcion_Cac,nombre_Cpf,codigo_Pes,codigo_Cac,codigo_Cpf,grupoHor_Cup,modular_pcu,estado_sil,instr_total,instr_asign,instr_pend,sesion_total,sesion_asign,sesion_pend,fechas_total,fechas_asign,fechas_pend,idcurso_mdl"
                                OnRowCreated="gvResultados_OnRowCreated" OnRowCommand="gvResultados_RowCommand"
                                CellPadding="0" ForeColor="#333333" CssClass="table table-bordered">
                                <Columns>
                                    <asp:BoundField DataField="nombre_Cpf" HeaderText="Carrera Profesional" HeaderStyle-Width="10%" />
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" HeaderStyle-Width="3%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" HeaderStyle-Width="14%" />
                                    <asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" HeaderStyle-Width="0%"
                                        Visible="false" />
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" HeaderStyle-Width="3%"
                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="estado" HeaderText="Estado" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HtmlEncode="false" DataField="instr_total" HeaderText="Tot.|Asig.|Pend"
                                        HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HtmlEncode="false" DataField="sesion_total" HeaderText="Tot.|Asig.|Pend"
                                        HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HtmlEncode="false" DataField="fechas_total" HeaderText="Tot.|Asig.|Pend"
                                        HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Registra Fechas" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnRegFechas" runat="server" CausesValidation="False" CssClass="btn btn-success btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="RegistrarFechas"
                                                Text="<i class='fa fa-calendar-alt'></i>" ToolTip="Registrar Fechas de Sesiones"
                                                Enabled='<%# IIF(Eval("aprobado") = "0" AND Request.QueryString("ctf") <> "249", "true", "false") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Agregar Instr." HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnInstrumentos" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Instrumentos"
                                                Text="<i class='fa fa-tasks'></i>" ToolTip="Agregar Instrumentos de Evaluación"
                                                Enabled='<%# IIF(Eval("aprobado") = "0" AND Request.QueryString("ctf") <> "249", "true", "false") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Enviar" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnConfirmar" runat="server" CausesValidation="False" CssClass="btn btn-danger btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="Confirmar"
                                                Text="<i class='fa fa-check-circle'></i>" ToolTip="Confirmar el envio del Sílabo para su publicación"
                                                Enabled='<%# IIF(Eval("estado_sil") = "P" AND Request.QueryString("ctf") <> "249", IIF(Len(Eval("instr_pend")) = 0 and Len(Eval("sesion_pend")) = 0 and Len(Eval("fechas_pend")) = 0, "true", "false"), "false") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Desc. Sílabo" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnBajarSilabo" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="DescargarSilabo"
                                                Text="<i class='fa fa-file-signature'></i>" ToolTip="Descargar Sílabo" Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                        HeaderText="Acta de sílabo" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnBajarActa" runat="server" CausesValidation="False" CssClass="btn btn-info btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="BajarActa"
                                                Text="<i class='fa fa-download'></i>" ToolTip="Descargar Formato de Acta" Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'></asp:LinkButton>
                                            <asp:Label ID="lblPorcentaje" runat="server" Text='<%#Eval("porcentaje")%>' Visible='<%# IIF(Request.QueryString("ctf") <> "249", "false", "true") %>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Aula Virtual">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnIrAula" runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="AulaVirtual"
                                                Text="<i class='fa fa-share-square'></i>" ToolTip="Ir a Aula Virtual" OnClientClick="return confirm('¿Desea ir al aula virtual?');"
                                                Visible='<%# IIF(Request.QueryString("ctf") <> "249", "false", "true") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--
                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left"
                                        HeaderText="Acta Firmada" HeaderStyle-Width="9%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSubirActa" runat="server" CausesValidation="False" CssClass='<%# IIF(Eval("idActa") = "0", "btn btn-warning btn-sm", "btn btn-success btn-sm") %>'
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="SubirActa"
                                                Text='<%# IIF(Eval("idActa") = "0", "<i class=&#39;fa fa-upload&#39;></i>", "<i class=&#39;fa fa-download&#39;></i>") %>'
                                                ToolTip='<%# IIF(Eval("idActa") = "0", "Subir acta firmada", "Bajar acta firmada") %>'
                                                Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>'></asp:LinkButton>
                                            &nbsp;
                                            <asp:LinkButton ID="btnQuitarActa" runat="server" CausesValidation="False" CssClass="btn btn-danger btn-sm"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="QuitarActa"
                                                Text="<i class='fa fa-trash-alt'></i>" ToolTip="Eliminar Acta Adjunta" Visible='<%# IIF(Eval("idActa") = "0", "False", "True") %>'
                                                Enabled='<%# IIF(Eval("aprobado") = "0", "false", "true") %>' OnClientClick="return confirm('¿Seguro de eliminar el acta adjuntada?');"></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                                    </asp:TemplateField>
                                    --%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron asignaturas
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal fecha de sesiones -->
    <div id="myModalSesion" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div2">
                <div class="modal-header">
                    <button type="button" id="btnSalir2" runat="server" class="close">
                        &times;</button>
                    <h4 class="modal-title">
                        Asignar fechas:
                        <label id="lblCursoA" runat="server" style="font-weight: 100">
                        </label>
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="messagealert" id="divAlertModal" runat="server" visible="false" style="margin-bottom: 10px">
                                <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                    class="alert alert-info">
                                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                        id="lblMensaje" runat="server"></span>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpSesion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-8">
                                            <label style="font-weight: 100; text-align: right; float: right; margin-top: 5px;
                                                margin-bottom: 0px; font-size: smaller; color: #8B0000;">
                                                (*)Mantenga presionado la tecla Ctrl para seleccionar más de una fecha</label>
                                        </div>
                                    </div>
                                    <asp:GridView ID="gvSesion" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ses,nombre_ses,dia_fec,codigo_fec,codigo_gru,numero_uni,descripcion_uni"
                                        CellPadding="0" ForeColor="#333333" CssClass="table table-sm table-striped table-bordered table-condensed">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_ses" HeaderText="Código" Visible="false" HeaderStyle-Width="0%"
                                                ItemStyle-Width="0%" FooterStyle-Width="0%"></asp:BoundField>
                                            <asp:BoundField DataField="numero_uni" HeaderText="#" HeaderStyle-Width="1%" ReadOnly="True"
                                                ItemStyle-Width="1%" FooterStyle-Width="1%"></asp:BoundField>
                                            <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenido" ReadOnly="True"
                                                HeaderStyle-Width="27%" ItemStyle-Width="27%" FooterStyle-Width="27%">
                                                <ItemStyle Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividad" ReadOnly="True"
                                                HeaderStyle-Width="28%" ItemStyle-Width="28%" FooterStyle-Width="28%">
                                                <ItemStyle Wrap="true" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="sesion" HeaderText="Sesión" ReadOnly="True" HeaderStyle-Width="5%"
                                                Visible="false">
                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="nombre_ses" HeaderText="Descripción" ReadOnly="True" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%" FooterStyle-Width="10%">
                                                <ItemStyle Wrap="True" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="Fecha(s)*" HeaderStyle-Width="19%" ItemStyle-Width="19%"
                                                FooterStyle-Width="19%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("nombre_fec") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ListBox ID="ddlFecha" runat="server" SelectionMode="Multiple" CssClass="form-control form-control-sm selectpicker"
                                                        size="6" Style="font-size: 12px; width: 180px; height: 120px;" ToolTip="Presione Ctrl para seleccionar más de una fecha">
                                                    </asp:ListBox>
                                                </EditItemTemplate>
                                                <ItemStyle Wrap="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Editar" ShowHeader="False" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%" FooterStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Editar fecha(s)" CssClass="btn btn-primary btn-sm"
                                                        CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-pen"></i>'></asp:LinkButton>
                                                    <span onclick="return confirm('¿Está seguro de eliminar la fecha asignada?')">
                                                        <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Eliminar fecha(s)" CssClass="btn btn-danger btn-sm"
                                                            CausesValidation="True" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'
                                                            Text='<i class="fa fa-backspace"></i>' OnClick="OnDeleteFecha" />
                                                    </span>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Actualizar fecha(s)" CssClass="btn btn-success btn-sm"
                                                        CausesValidation="True" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-save"></i>'></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" ToolTip="Cancelar edición" CssClass="btn btn-info btn-sm"
                                                        CausesValidation="False" CommandName="Cancel" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-times"></i>'></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="12px" />
                                        <RowStyle Font-Size="12px" />
                                        <EditRowStyle Font-Size="12px" />
                                        <FooterStyle Font-Size="12px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnSalir" runat="server" CssClass="btn btn-danger" Text='<i class="fa fa-sign-out-alt"></i> Salir'></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal subir acta -->
    <div id="myModalActa" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 1;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="Div3">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title" id="lblTituloActa">
                        Subir Acta de Exposición
                    </h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="updFileUpload" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <label>
                                        Adjuntar Acta:</label>
                                </div>
                                <div class="col-md-10 col-xs-10 form-group">
                                    <label id="lblFuArchivo" runat="server" style="font-style: normal; font-size: small;
                                        font-weight: normal">
                                        <input id="btnFuArchivo" type="button" value="Seleccionar archivo" runat="server" />
                                        <span id="spnFile" runat="server">No se eligió acta de exposición</span>
                                    </label>
                                    <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" AllowMultiple="true"
                                        Style="display: none;" onChange="FileSelected(this);" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                        ErrorMessage="* Seleccione un archivo" ControlToValidate="fuArchivo" ValidationGroup="subirArchivo">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalirActa" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGuardarActa" runat="server" Text="Guardar" CssClass="btn btn-success"
                        ValidationGroup="subirArchivo" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
