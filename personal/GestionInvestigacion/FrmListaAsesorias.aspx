<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaAsesorias.aspx.vb"
    Inherits="GestionInvestigacion_FrmListaAsesorias" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Listado de Asesorias</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />
    <link href="../assets/css/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script src="../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../assets/js/app.js" type="text/javascript"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../assets/js/noty/notifications-custom.js"></script>

    <script src="../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function fnDescargar(id_ar) {
            window.open("../Descargar.aspx?Id=" + id_ar);
        }
        function ValidarRespuesta() {
            $("#lblMensajeRespuesta").text("");
            $("#lblMensajeRespuesta").removeAttr("class");
            if ($("#txtRespuesta").val() == "") {
                $("#lblMensajeRespuesta").text("Ingrese Respuesta");
                $("#lblMensajeRespuesta").attr("class", "alert alert-danger");
                $("#txtRespuesta").focus();
                return false;
            }
            if ($("#ArchivoRespuesta").val() != '') {

                if ($("#ArchivoRespuesta")[0].files[0].size >= 20971520) {
                    //fnMensaje("error", "Solo se pueden adjuntar archivos de máximo 20MB");
                    //fnMensaje('error', '');
                    $("#lblMensajeRespuesta").text("Solo se pueden adjuntar archivos de máximo 20MB");
                    $("#lblMensajeRespuesta").attr("class", "alert alert-danger");
                    return false;
                }
                archivo = $("#ArchivoRespuesta").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                extensiones_permitidas = new Array(".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png", ".zip", ".rar", ".pdf");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
                archivo = archivo.substring(archivo.length - 5, archivo.length);
                //despues del punto de nombre recortado
                extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
                //compruebo si la extensión está entre las permitidas 
                permitida = false;
                for (var i = 0; i < extensiones_permitidas.length; i++) {
                    if (extensiones_permitidas[i] == extension) {
                        permitida = true;
                        break;
                    }
                }
                if (permitida == false) {

                    $("#lblMensajeRespuesta").text("Solo puede adjuntar archivos de proyecto en formato .doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx");
                    $("#lblMensajeRespuesta").attr("class", "alert alert-danger");
                    return false;
                }
            }
            if (!confirm('Está segúro que desea Guardar Respuesta?')) {
                return false;
            }
            return true;
        }
        $(document).ready(function() {
            $('#mdResponder').on('show.bs.modal', function(e) {
                var button = e.relatedTarget;
                if (button != null) {
                    $("#hdc").val($("#" + button.id).attr("attr"))
                }
                $("#txtRespuesta").val("");
                $("#ArchivoRespuesta").val("");
            });
        })

        function fnConfirmacion(ctrl) {

            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                width: 600,
                title: '¿Usted se compromete a asesorar este proyecto de tesis, cumpliendo con lo establecido en el Reglamento de elaboración de trabajos de investigación para optar el grado académico de Bachiller y Título profesional?',
                html: "<h4 style='color:red'>Luego no podrá revertir la acción</h4>",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si',
                cancelButtonText: 'No'
            }).then(function(result) {
                if (result.value == true) {
                    eval(defaultAction);
                }
            })
            /*
            if (confirm('¿Está seguro que desea dar conformidad a tesis?')) {
            return true;
            } else {
            return false;
            }*/
        }
       
    </script>

    <style type="text/css">
        .content
        {
            margin-left: 0px;
        }
        .page_header
        {
            background-color: #FAFCFF;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #718FAB;
            height: 28px;
            font-weight: 300;
            color: black;
        }
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 4px;
            vertical-align: middle;
        }
        .table > tfoot > tr > th, .table > tbody > tr > td, .table > tfoot > tr > td
        {
            color: Black;
            border-color: Black;
        }
        .table > tbody > tr > th, .table > thead > tr > th, .table > thead > tr > td
        {
            color: White;
            text-align: center;
            vertical-align: middle; /*font-weight: bold;*/
        }
        .checkbox label
        {
            padding-left: 1px;
        }
        input[type="checkbox"] + label
        {
            color: Black;
        }
        :-ms-input-placeholder.form-control
        {
            line-height: 0px;
        }
        hr
        {
            margin-bottom: 0px;
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">Listado
                        de asesorías</span>
                </div>
            </div>
            <div class="panel panel-piluku">
                <%--<div class="panel-heading">
                    <h3 class="panel-title">
                        Listado de asesorías
                    </h3>
                </div>--%>
                <div class="panel-body">
                    <div class="row">
                        <label class="col-sm-1 col-md-1 control-label">
                            Semestre
                        </label>
                        <div class="col-sm-2 col-md-2">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlSemestre" AutoPostBack="true">
                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-sm-1 col-md-1 control-label">
                            Etapa
                        </label>
                        <div class="col-sm-2 col-md-2">
                            <asp:DropDownList runat="server" class="form-control" ID="ddlEtapa" AutoPostBack="true">
                                <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                <asp:ListItem Value="E">EJECUCIÓN</asp:ListItem>
                                <asp:ListItem Value="I">INFORME</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label class="col-sm-1 col-md-1 control-label ">
                            Docente</label>
                        <div class="col-sm-5 col-md-5">
                            <asp:UpdatePanel runat="server" ID="UpdatePanelAsesor">
                                <ContentTemplate>
                                    <asp:DropDownList runat="server" class="form-control" ID="ddlDocente" AutoPostBack="true">
                                        <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlEtapa" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                        <%--                        <div class="col-md-1">
                            Año</div>
                        <div class="col-md-2">
                           
                                    <asp:DropDownList runat="server" ID="cboAnio" CssClass="form-control" AutoPostBack="true">
                                        <asp:ListItem Value="">-- Seleccione --</asp:ListItem>
                                    </asp:DropDownList>
                              
                        </div>--%>
                        <%-- <div class="col-md-2">
                            <asp:Button runat="server" ID="btnGuardar" CssClass="btn btn-success" Text="Guardar" />
                        </div>--%>
                    </div>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>
                            <div class="row" id="Asesorados" runat="server">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvAsesorias" DataKeyNames="codigo_tes,codigo_cac,codigo_RTes,codigo_Eti,porcentaje,nota,PermiteCalificar,fechaIni_Cro,fechaFin_Cro,compromiso,generacompromiso"
                                    AutoGenerateColumns="False" CssClass="table table-responsive">
                                    <Columns>
                                        <asp:TemplateField HeaderText="N°" HeaderStyle-Width="2%">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="ALUMNO" DataField="alumno" HeaderStyle-Width="17%"></asp:BoundField>
                                        <asp:BoundField HeaderText="TESIS" DataField="titulo_tes" HeaderStyle-Width="36%">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="ETAPA" DataField="nombre_Eti" HeaderStyle-Width="9%">
                                        </asp:BoundField>
                                        <%--<asp:BoundField HeaderText="ASESOR" DataField="asesor" HeaderStyle-Width="15%"></asp:BoundField>--%>
                                        <asp:BoundField HeaderText="ASIGNADO" DataField="FechaAsignacíon" HeaderStyle-Width="7%">
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="AVANCE(%) | NOTA" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <asp:TextBox runat="server" ID="txtPorcentaje" Width="40px" Height="28px" placeholder="%%"
                                                        Text='<%# Eval("Porcentaje") %>'></asp:TextBox>&nbsp;&nbsp;|&nbsp;&nbsp;
                                                    <asp:TextBox runat="server" ID="txtNota" Width="40px" Height="28px" placeholder="Nota"
                                                        Text='<%# Eval("Nota") %>'></asp:TextBox>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OPCIONES" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                                            ItemStyle-VerticalAlign="Middle">
                                            <ItemTemplate>
                                                <div class="row" style="padding-left: 0px;padding-right: 0px;">
                                                    <asp:LinkButton runat="server" ID="Guardar" CommandName="Actualizar" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-sm btn-orange btn-radius" Text="<span class='ion-android-open'></span>"
                                                        ToolTip="Guardar" OnClientClick="return confirm('¿Desea guardar puntaje y nota?, una vez colocada la nota y el porcentaje ya no podrá registrar asesorias, se recomienda registrar al finalizar el curso.')"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbAsesoria" CommandName="Asesorias" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-sm btn-primary btn-radius" Text="<span class='ion-chatbox-working'></span>"
                                                        ToolTip="Asesorias"></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lbCompromiso" CommandName="Compromiso" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'
                                                        CssClass="btn btn-sm btn-info btn-radius" Text="<span class='ion-ios-list'></span>"
                                                        OnClientClick="fnConfirmacion(this); return false;" ToolTip="Aceptar compromiso de asesor"></asp:LinkButton>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle"
                                        Font-Size="11px" />
                                    <RowStyle Font-Size="10px" />
                                    <EmptyDataTemplate>
                                        No se Encontraron Registros
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlDocente" EventName="SelectedIndexChanged" />
                            <asp:PostBackTrigger ControlID="gvAsesorias" />
                            <asp:AsyncPostBackTrigger ControlID="btnRegresar" EventName="click" />
                            <%--           <asp:AsyncPostBackTrigger ControlID="cboAnio" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnGuardar" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel runat="server" ID="updASesorias" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>
                            <%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
                            <div class="row" runat="server" id="DivAsesorias" visible="false">
                                <div class="row">
                                    <div class="col-sm-12 col-md-12">
                                        <center>
                                            <asp:Button runat="server" ID="btnRegresar" CssClass="btn btn-sm btn-danger" Text="Regresar" />
                                        </center>
                                    </div>
                                </div>
                                <div role="tabpanel">
                                    <!-- Nav tabs -->
                                    <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                        <li role="presentation" id="TabProyecto"><a href="#hometabnb" aria-controls="home"
                                            role="tab" data-toggle="tab" id="Tab1">Datos de Tesis</a></li>
                                        <li role="presentation" class="active" id="Asesorias"><a href="#settingstabnb" aria-controls="settings"
                                            role="tab" data-toggle="tab" id="Tab4">Asesorías</a></li>
                                    </ul>
                                    <!-- Tab panes -->
                                    <div class="tab-content piluku-tab-content">
                                        <div role="tabpanel" class="tab-pane" id="hometabnb">
                                            <form enctype="multipart/form-data" id="frmRegistro" name="frmRegistro">
                                            <input type="hidden" id="hdcodA" name="hdcodA" value="0" />
                                            <input type="hidden" id="hdcod" name="hdcod" value="0" />
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtAutores">Autores</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtAutores" CssClass="form-control" TextMode="MultiLine"
                                                            Rows="2" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtFacultad">Facultad</asp:Label>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:TextBox runat="server" ID="txtFacultad" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="Label3" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                        For="txtCarrera">Carrera Profesional</asp:Label>
                                                    <div class="col-sm-4 col-md-4">
                                                        <asp:TextBox runat="server" ID="txtCarrera" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label4" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtLinea">Línea de Investigación USAT:</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtLinea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label5" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtArea">Área OCDE</asp:Label>
                                                    <div class="col-sm-3 col-md-3">
                                                        <asp:TextBox runat="server" ID="txtArea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="Label6" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                        For="txtSubArea">Sub Área OCDE</asp:Label>
                                                    <div class="col-sm-4 col-md-4">
                                                        <asp:TextBox runat="server" ID="txtSubArea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label7" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtDisciplina">Disciplina OCDE:</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtDisciplina" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label8" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtPresupuesto">Presupuesto</asp:Label>
                                                    <div class="col-sm-2 col-md-3">
                                                        <asp:TextBox runat="server" ID="txtPresupuesto" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                    <asp:Label ID="Label9" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                        For="txtFinanciamiento">Financiamiento</asp:Label>
                                                    <div class="col-sm-5 col-md-4">
                                                        <asp:TextBox runat="server" ID="txtFinanciamiento" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label10" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtTitulo">Título</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtTitulo" CssClass="form-control" TextMode="MultiLine"
                                                            Rows="3" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label11" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtObjetivoG">Objetivo General</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtObjetivoG" CssClass="form-control" TextMode="MultiLine"
                                                            Rows="3" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label12" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                        For="txtObjetivoE">Objetivos Específicos</asp:Label>
                                                    <div class="col-sm-9 col-md-9">
                                                        <asp:TextBox runat="server" ID="txtObjetivoE" CssClass="form-control" TextMode="MultiLine"
                                                            Rows="5" ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-4 col-md-4" runat="server" id="divProyecto">
                                                    </div>
                                                    <div class="col-sm-4 col-md-4" runat="server" id="divPreinforme">
                                                    </div>
                                                    <div class="col-sm-4 col-md-4" runat="server" id="divInforme">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="col-sm-4 col-md-4" runat="server" id="div1">
                                                    </div>
                                                    <div class="col-sm-4 col-md-4" runat="server" id="div2">
                                                    </div>
                                                    <div class="col-sm-4 col-md-4" runat="server" id="divEnlaceInforme">
                                                    </div>
                                                </div>
                                            </div>
                                            </form>
                                        </div>
                                        <div role="tabpanel" class="tab-pane active" id="settingstabnb">
                                            <div runat="server" id="lblMensajeObservación">
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:HiddenField runat="server" ID="hdetapa" Value="0" />
                                                    <asp:HiddenField runat="server" ID="hdrtes" Value="0" />
                                                    <asp:HiddenField runat="server" ID="hdtes" Value="0" />
                                                    <asp:Label ID="Label16" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                        For="txtObservacion">Observaciones</asp:Label>
                                                    <div class="col-sm-7 col-md-8">
                                                        <asp:TextBox runat="server" ID="txtObservacion" CssClass="form-control" TextMode="MultiLine"
                                                            Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group">
                                                    <asp:Label ID="Label13" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                        For="txtObservacion">Adjuntar revisión</asp:Label>
                                                    <div class="col-sm-7 col-md-8">
                                                        <asp:FileUpload runat="server" ID="archivo" CssClass="form-control" />
                                                        <ul>
                                                            <li>Archivos permitidos: <span style="color: Red">.doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx</span></li>
                                                            <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                                        </ul>
                                                    </div>
                                                    <div class="col-sm-3 col-md-2">
                                                        <asp:Button runat="server" ID="btnGuardarObservacion" CssClass="btn btn-sm btn-primary"
                                                            OnClientClick="return confirm('¿Está seguro que desea registrar observación?')"
                                                            Text="Guardar" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-12">
                                                    <!--                        *** Timeline ***-->
                                                    <div class="panel panel-piluku">
                                                        <div class="panel-body timeline-block">
                                                            <!--Timeline-->
                                                            <div runat="server" id="LineaDeTiempo">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <!-- *** /Timeline ***-->
                                                    <!-- /panel -->
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="modal fade" id="mdResponder" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-body">
                                                <div runat="server" id="lblMensajeRespuesta">
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12 col-md-12">
                                                        <legend style="font-weight: bold; background-color: #f5f5dc; text-align: center;">Registrar
                                                            Respuesta</legend>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <input type="hidden" runat="server" id="hdc" name="hdc" value="0" />
                                                                    <label class="col-md-4 control-label" for="txtDescripcion" style="font-weight: bold;
                                                                        padding-top: 0px;">
                                                                        Observaciones:</label>
                                                                    <div class="col-md-8">
                                                                        <asp:TextBox runat="server" ID="txtRespuesta" CssClass="form-control" MaxLength="1000"
                                                                            placeholder="Escriba un mensaje" TextMode="MultiLine" Rows="6"> </asp:TextBox>
                                                                        <label id="txtdescsolsize">
                                                                        </label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-group">
                                                                    <label class="col-md-4 control-label" for="txtArchivo" style="font-weight: bold;
                                                                        padding-top: 0px;">
                                                                        Adjuntar revisión:</label>
                                                                    <div class="col-md-8">
                                                                        <asp:FileUpload runat="server" ID="ArchivoRespuesta" CssClass="form-control" />
                                                                        <ul>
                                                                            <li>Archivos permitidos: <span style="color: Red">.doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx</span></li>
                                                                            <li>Tamaño Máximo: <span style="color: Red">20 Mb</span></li>
                                                                        </ul>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <center>
                                                                    <asp:Button runat="server" ID="btnGuardarRespuesta" CssClass="btn btn-sm btn-success"
                                                                        OnClientClick="return ValidarRespuesta();" Text="Guardar" />
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                                                                        Cancelar
                                                                    </button>
                                                                </center>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="gvAsesorias" />
                            <asp:AsyncPostBackTrigger ControlID="btnRegresar" EventName="click" />
                            <asp:PostBackTrigger ControlID="btnGuardarObservacion" />
                            <asp:PostBackTrigger ControlID="btnGuardarRespuesta" />
                            <asp:AsyncPostBackTrigger ControlID="ddlDocente" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlSemestre" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlEtapa" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
