<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmRevisionTesisAsesor.aspx.vb"
    Inherits="FrmRevisionTesisAsesor" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Revisión de Tesis para sustentación </title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/material.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />
    <link href="../../assets/font-awesome-4.7.0/css/font-awesome.css" rel="stylesheet"
        type="text/css" />
    <%-- ======================= Lista desplegable con busqueda =============================================--%>
    <link href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.css" rel="stylesheet"
        type="text/css" />
    <link href="../../assets/css/sweetalert2.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Inicio Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <script src="../../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        //        function MostrarModal(div) {
        //            $("#" + div).modal('show');
        //        }
        //        function MostrarAsesoria() {
        //            $("#DivAsesorias").show();
        //            $("#Lista").hide();
        //        }
        //        function CerrarModal(div) {
        //            $("#" + div).modal('hide');
        //        }
        //        function OcultarAsesoria() {
        //            $("#Lista").show();
        //            $("#DivAsesorias").hide();
        //        }

        $(document).ready(function() {
            $('#mdResponder').on('show.bs.modal', function(e) {
                var button = e.relatedTarget;
                if (button != null) {
                    $("#hdc").val($("#" + button.id).attr("attr"))
                }
                $("#txtRespuesta").val("");
                $("#ArchivoRespuesta").val("");

            });
            $(".swal2-confirm").click(function() {
                console.log("click");
                fnLoading(true);
            })
        });

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

                    $("#lblMensajeRespuesta").text("Solo puede adjuntar archivos en formato .doc,.docx,.jpg,.png,.pdf,.zip,.rar,.xls,.xlsx");
                    $("#lblMensajeRespuesta").attr("class", "alert alert-danger");
                    return false;
                }
            }
            if (!confirm('¿Está seguro que desea guardar respuesta?')) {
                return false;
            }
            return true;
        }

        //        function fnMensaje(typ, msje) {
        //            var n = noty({
        //                text: msje,
        //                type: typ,
        //                timeout: 3000,
        //                modal: false,
        //                dismissQueue: true,
        //                theme: 'defaultTheme'

        //            });
        //        }
        function fnLoading(sw) {
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }

        function fnDescargar(url) {
            var d = new Date();
            window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
        
        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 3000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }

        function fnConfirmacion(ctrl) {

            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                title: '¿Está seguro que desea dar conformidad a la tesis?',
                text: "Luego no podrá revertir la conformidad",
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

        function MensajeValidacion(texto) {
            Swal.fire({
                icon: 'warning',
                title: 'Validación ORCID',
                html: texto,
                footer: '<span>Para registrar su ORCID debe ingresar al módulo de <span style="color:red;font-weight:bold;">GESTIÓN DE INVESTIGACIÓN</span> en la Opción: Movimientos > Colaboradores y Grupos de investigación > Registro Colaborador con actividad investigadora</span>'
            })
        }

    </script>

    <style type="text/css">
        body
        {
            padding-right: 0 !important;
        }
        .modal-open
        {
            overflow: inherit;
        }
        .form table th
        {
            text-align: center;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-control
        {
            color: Black;
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
        .table > thead > tr > th
        {
            color: White;
            font-size: 12px;
            font-weight: bold;
            text-align: center;
        }
        .table > tbody > tr > td
        {
            color: black;
            vertical-align: middle;
        }
        .table tbody tr th
        {
            color: White;
            font-size: 11px;
            font-weight: bold;
            text-align: center;
        }
    </style>
</head>
<body class="">
    <div class="container-fluid">
        <form enctype="multipart/form-data" id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: #E33439; color: White; font-weight: bold;
                font-size: 14px;">
                <b>Revisión de Tesis para Sustentación</b>
            </div>
            <div class="panel-body">
                <asp:UpdatePanel runat="server" ID="updConformidad" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div runat="server" id="Lista">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="col-sm-1 col-md-1 control-label">Estado</asp:Label>
                                    <div class="col-sm-5 col-md-5">
                                        <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-control" AutoPostBack="true">
                                            <asp:ListItem Value="P">PENDIENTES</asp:ListItem>
                                            <asp:ListItem Value="N">NO REVISADOS</asp:ListItem>
                                            <asp:ListItem Value="O">OBSERVADOS</asp:ListItem>
                                            <asp:ListItem Value="A">APROBADOS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <%--      <div class="col-sm-1 col-md-1">
                            <asp:LinkButton ID="btnConsultar" runat="server" Text='<span class="fa fa-search"></span>'
                                CssClass="btn btn-primary" ToolTip="Buscar"></asp:LinkButton>
                        </div>--%>
                                </div>
                                <div class="form-group">
                                    <div class="col-sm-4 col-md-4">
                                        <table>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="btnDescargar1" runat="server" Text='<span class="fa fa-download"></span>'
                                                        CssClass="btn btn-info btn-sm btn-radius" Font-Size="11px" ToolTip="Descargar"
                                                        OnClientClick="return false;">
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label7" runat="server" CssClass="control-label">Descargar Archivo de Tesis</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="btnBloquear1" runat="server" Text='<span class="fa fa-lock"></span>'
                                                        CssClass="btn btn-sm btn-danger btn-radius" Font-Size="11px" ToolTip="Bloquear"
                                                        OnClientClick="return false;">
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label8" runat="server" CssClass="control-label">Bloquear actualización de archivo de Tesis a egresado</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <table>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<span class="fa fa-unlock"></span>'
                                                        CssClass="btn btn-sm btn-primary btn-radius" Font-Size="11px" ToolTip="Desbloquear"
                                                        OnClientClick="return false;">
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label18" runat="server" CssClass="control-label">Desloquear actualización de archivo de Tesis a egresado</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="LinkButton5" runat="server" Text='<span class="fa fa-comment"></span>'
                                                        CssClass="btn btn-warning btn-sm btn-radius" Font-Size="11px" ToolTip="Observaciones"
                                                        OnClientClick="return false;">
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label25" runat="server" CssClass="control-label">Registro de observaciones</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-4 col-md-4">
                                        <table>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<span class="fa fa-check"></span>'
                                                        CssClass="btn btn-success btn-sm btn-radius" Font-Size="11px" ToolTip="Conformidad"
                                                        OnClientClick="return false;"> 
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label14" runat="server" CssClass="control-label">Dar conformidad a tesis</asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 15%; padding: 4px;">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<span class="fa fa-download"></span>'
                                                        CssClass="btn btn-danger btn-sm btn-radius" Font-Size="11px" ToolTip="Descargar"
                                                        OnClientClick="return false;">
                                                    </asp:LinkButton>
                                                </td>
                                                <td style="width: 85%;">
                                                    <asp:Label ID="Label9" runat="server" CssClass="control-label">Descargar informe de asesor</asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <%--<asp:UpdatePanel runat="server" ID="updGeneral" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <asp:UpdatePanel runat="server" ID="updGrilla" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>--%>
                            <%--  <div class="form-group">
                        <table id="tbTest" class="table table-condensed" style="width: 100%; font-size: 12px;"
                            cellspacing="0" border="1">
                            <thead style="background: #E33439; color: White; font-weight: bold;">
                                <tr>
                                    <th style="width: 5%;">
                                        #
                                    </th>
                                    <th style="width: 40%;">
                                        Título
                                    </th>
                                    <th style="width: 40%;">
                                        Bachiller(es)
                                    </th>
                                    <th style="width: 15%;">
                                        Opciones
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        1
                                    </td>
                                    <td>
                                        LA POSIBILIDAD DE ABRIR NUEVOS PROCEDIMIENTOS SANCIONADORES CONCLUIDOS POR CADUCIDAD
                                        Y LA POSIBLE LESIÓN AL PRINCIPIO DE INTERDICCIÓN DE LA ARBITRARIEDAD.
                                    </td>
                                    <td>
                                        DEL VALLE MERINO RONNY JEANPIERRE
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                            CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnAsesorias" runat="server" Text='<span class="fa fa-comment"></span>'
                                            CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Asesorias" OnClientClick="MostrarAsesoria();return false;">
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnConforme" runat="server" Text='<span class="fa fa-check"></span>'
                                            CssClass="btn btn-success btn-sm btn-radius" ToolTip="Conformidad">
                                        </asp:LinkButton>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>--%>
                            <div class="form-group">
                                <div runat="server" id="lblmensaje">
                                </div>
                                <asp:GridView runat="server" ID="gvAlumnos" CssClass="table table-condensed" DataKeyNames="codigo_Tes,codigo_RTes,codigo_per,archivofinal,BloqueadoxAsesor,fechaconformidad,informeasesor,codigo_test"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1%>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                        <asp:BoundField DataField="titulo_tes" HeaderText="TÍTULO" HeaderStyle-Width="48%" />
                                        <asp:BoundField DataField="alumno" HeaderText="BACHILLER(ES)" HeaderStyle-Width="40%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar" CommandName="Descargar"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnBloquear" runat="server" Text='<span class="fa fa-lock"></span>'
                                                    CssClass="btn btn-sm btn-danger btn-radius" ToolTip="Bloquear" CommandName="Bloquear"
                                                    OnClientClick="return confirm('¿Está seguro que desea bloquear la tesis?')" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAsesorias" runat="server" Text='<span class="fa fa-comment"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Observaciones" CommandName="Asesorias"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnConforme" runat="server" Text='<span class="fa fa-check"></span>'
                                                    CssClass="btn btn-success btn-sm btn-radius" ToolTip="Conformidad" CommandName="Conformidad"
                                                    OnClientClick="fnConfirmacion(this); return false;" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#D9534F" ForeColor="white" />
                                    <RowStyle Font-Size="12px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron tesis</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="ddlEstado" EventName="selectedindexchanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                    </Triggers>
                </asp:UpdatePanel>
                <%--   </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnConsultar" EventName="click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="form-group" id="DivAsesorias" runat="server" visible="false">
                            <div class="row">
                                <div class="form-group text-center">
                                    <asp:Button runat="server" ID="btnAtras" CssClass="btn btn-sm btn-danger" Text="Atrás" />
                                </div>
                            </div>
                            <div role="tabpanel">
                                <!-- Nav tabs -->
                                <ul class="nav nav-tabs nav-justified piluku-tabs piluku-noborder" role="tablist">
                                    <li role="presentation" id="Li1" runat="server"><a href="#Datos" aria-controls="datos"
                                        role="tab" data-toggle="tab" id="A1">Datos de Tesis</a></li>
                                    <li role="presentation" class="active" id="TabAsesorias" runat="server"><a href="#Asesorias"
                                        aria-controls="asesorias" role="tab" data-toggle="tab" id="tab4">Revisiones</a></li>
                                </ul>
                                <!-- Tab panes -->
                                <div class="tab-content piluku-tab-content">
                                    <div role="tabpanel" class="tab-pane" id="Datos" runat="server">
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtAutores">Bachiller(es)</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txtAutor" CssClass="form-control" TextMode="MultiLine"
                                                        Rows="3" ReadOnly="true" Text=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtFacultad">Facultad</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <asp:TextBox runat="server" ID="txtFacultad" CssClass="form-control" ReadOnly="true"
                                                        Text=""></asp:TextBox>
                                                </div>
                                                <asp:Label ID="Label5" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                    For="txtCarrera">Carrera Profesional</asp:Label>
                                                <div class="col-sm-4 col-md-4">
                                                    <asp:TextBox runat="server" ID="txtcarrera" CssClass="form-control" ReadOnly="true"
                                                        Text=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtLinea">Linea de Investigación USAT:</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txtlinea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label11" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtArea">Área OCDE</asp:Label>
                                                <div class="col-sm-3 col-md-3">
                                                    <asp:TextBox runat="server" ID="txtarea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                                <asp:Label ID="Label12" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                    For="txtSubArea">Sub Área OCDE</asp:Label>
                                                <div class="col-sm-4 col-md-4">
                                                    <asp:TextBox runat="server" ID="txtsubarea" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label19" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtDisciplina">Disciplina OCDE:</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txtdisciplina" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label20" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtPresupuesto">Presupuesto</asp:Label>
                                                <div class="col-sm-2 col-md-3">
                                                    <asp:TextBox runat="server" ID="txtPresupuesto" CssClass="form-control" ReadOnly="true"
                                                        Text=""></asp:TextBox>
                                                </div>
                                                <asp:Label ID="Label21" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                    For="txtFinanciamiento">Financiamiento</asp:Label>
                                                <div class="col-sm-5 col-md-4">
                                                    <asp:TextBox runat="server" ID="txtFinanciamiento" CssClass="form-control" ReadOnly="true"
                                                        Text=""></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label22" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtTitulo">Título</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txttitulo" CssClass="form-control" TextMode="MultiLine"
                                                        Text="" Rows="3" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label23" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtObjetivoG">Objetivo General</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txtObjetivoG" CssClass="form-control" TextMode="MultiLine"
                                                        Text="" Rows="3" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label ID="Label24" runat="server" CssClass="col-sm-3 col-md-3 control-label"
                                                    For="txtObjetivoE">Objetivos Específicos</asp:Label>
                                                <div class="col-sm-9 col-md-9">
                                                    <asp:TextBox runat="server" ID="txtObjetivoE" CssClass="form-control" TextMode="MultiLine"
                                                        Text="" Rows="5" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div role="tabpanel" class="tab-pane active" id="Asesorias">
                                        <div class="panel panel-piluku">
                                            <asp:UpdatePanel runat="server" ID="updGuardarObservacion" ChildrenAsTriggers="false"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div runat="server" id="lblMensajeObservación">
                                                    </div>
                                                    <div class="row">
                                                        <div class="form-group">
                                                            <asp:HiddenField runat="server" ID="hdrtes" Value="0" />
                                                            <asp:HiddenField runat="server" ID="hdtes" Value="0" />
                                                            <asp:Label ID="Label16" runat="server" CssClass="col-sm-2 col-md-2 control-label"
                                                                For="txtObservacion">Observaciones</asp:Label>
                                                            <div class="col-sm-7 col-md-8">
                                                                <asp:TextBox runat="server" ID="txtObservacion" CssClass="form-control" TextMode="MultiLine"
                                                                    MaxLength="1000" Rows="4"></asp:TextBox>
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
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnGuardarObservacion" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="row">
                                                <div class="panel-body timeline-block">
                                                    <!--Timeline-->
                                                    <div id="LineaDeTiempo" runat="server">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="gvAlumnos" EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="btnAtras" />
                    </Triggers>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="modal fade" id="mdResponder" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                        aria-hidden="true" data-backdrop="static" data-keyboard="false">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                    </button>
                                    <h4 class="modal-title" id="H1">
                                        Responder consulta
                                    </h4>
                                </div>
                                <div class="modal-body">
                                    <div runat="server" id="lblMensajeRespuesta">
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12 col-md-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:HiddenField runat="server" ID="hdc" Value="0" />
                                                        <label id="lbl1" class="col-xs-4 col-sm-4 col-md-4 control-label">
                                                            Descripción</label>
                                                        <div class="col-xs-8 col-sm-8 col-md-8">
                                                            <asp:TextBox runat="server" ID="txtRespuesta" CssClass="form-control" TextMode="MultiLine"
                                                                Rows="4"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <label id="Label4" class="col-xs-4 col-sm-4 col-md-4 control-label">
                                                            Adjuntar archivo</label>
                                                        <div class="col-xs-8 col-sm-8 col-md-8">
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
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</body>
</html>
