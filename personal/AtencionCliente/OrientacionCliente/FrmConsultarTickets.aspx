<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConsultarTickets.aspx.vb"
    Inherits="OrientacionCliente_FrmConsultarTickets" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<meta http-equiv="X-UA-Compatible" content="IE=edge" />--%>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <title>Tipo de orientación</title>
    <link href="../../assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../../assets/js/jquery.js'></script>

    <script src="../../assets/js/bootstrap.min.js" type="text/javascript"></script>

    <%-- ======================= Notificaciones =============================================--%>

    <script type="text/javascript" src="../../assets/js/noty/jquery.noty.js"></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/top.js'></script>

    <script type="text/javascript" src='../../assets/js/noty/layouts/default.js'></script>

    <script type="text/javascript" src="../../assets/js/noty/notifications-custom.js"></script>

    <%-- ======================= Sweet Alert =============================================--%>

    <script src="../../assets/js/sweetalert2.all.min.js" type="text/javascript"></script>

    <script src="../../assets/js/promise.min.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function() {
            fnLoading(false);

        });
        function fnLoading(sw) {
            console.log(sw);
            if (sw) {
                $('.piluku-preloader').removeClass('hidden');
            } else {
                $('.piluku-preloader').addClass('hidden');
            }
            //console.log(sw);
        }
        function fnMensaje(typ, msje) {
            var n = noty({
                text: msje,
                type: typ,
                timeout: 5000,
                modal: false,
                dismissQueue: true,
                theme: 'defaultTheme'

            });
        }
        function fnConfirmacion(ctrl, texto, adicional) {
            var defaultAction = $(ctrl).prop("href");
            Swal.fire({
                title: texto,
                text: adicional,
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Si',
                cancelButtonText: 'No'
            }).then(function(result) {
                if (result.value == true) {
                    fnLoading(true);
                    eval(defaultAction);
                }
            })
        }

        function Validar(ctrl, texto, adicional) {
            if ($("#txtEstudiante").val() == "") {
                fnMensaje('error', 'Realice la búsqueda y seleccione un estudiante')
                return false
            }
            if ($("#txtAsunto").val().trim() == "") {
                fnMensaje('error', 'Ingrese el asunto del ticket')
                return false
            }
            if ($("#txtDescripcion").val().trim() == "") {
                fnMensaje('error', 'Ingrese la descripción del ticket')
                return false
            }
            if ($("#archivo").val() != '') {
                if ($("#archivo")[0].files[0].size >= 20971520) {
                    fnMensaje("error", "solo se pueden adjuntar archivos de máximo 20MB");
                    return false;
                }

                archivo = $("#archivo").val();
                extensiones_permitidas = new Array(".jpg", ".jpeg", ".png", ".doc", ".docx", ".rar", ".pdf");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (Ubicación de la Extensión)
                archivo = archivo.substring(archivo.length - 5, archivo.length);
                //despues del punto de nombre recortado
                extension = (archivo.substring(archivo.lastIndexOf("."))).toLowerCase();
                //compruebo si la extensión está entre las permitidas 
                permitida = false;
                for (var i = 0; i < extensiones_permitidas.length; i++) {
                    if (extensiones_permitidas[i].toLowerCase() == extension.toLowerCase()) {
                        permitida = true;
                        break;
                    }
                }
                if (permitida == false) {
                    fnMensaje("error", "Solo puede adjuntar archivos de formato jpg, jpeg, png, doc, docx, pdf y rar");
                    return false;
                }
            }
            if ($("#ddlOrigen").val() == "0") {
                fnMensaje('error', 'Seleccione el canal de recepción')
                return false
            }
            if ($("#ddlTipo").val() == "0") {
                fnMensaje('error', 'Seleccione la clasificación')
                return false
            }
            if ($("#ddlServicio").val() == "0") {
                fnMensaje('error', 'Seleccione el servicio')
                return false
            }
            fnConfirmacion(ctrl, texto, adicional);

        }
        function fnAbrirModal(md) {
            $("#" + md).modal("show");
        }
        function fnDescargar(url) {
            var d = new Date();
            window.open(url + "&h=" + d.getHours().toString() + d.getMinutes().toString() + d.getSeconds().toString());
        }
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
            padding: 3px;
        }
        /* 
        .content .main-content
        {
            padding-right: 18px;
        }
        .content
        {
            margin-left: 0px;
        }*/.form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 30px;
            color: black;
        }
        input[type="checkbox"]
        {
            display: inline-block;
        }
        input[type="checkbox"] + label
        {
            color: White;
            font-weight: bold;
            font-size: 13px;
        }
        /*
        .i-am-new
        {
            z-index: 100;
        }
        .form-group
        {
            margin: 3px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
            text-align: left;
        }
        .modal
        {
            overflow: auto !important;
        }
        .input-group .form-control
        {
            z-index: 0;
        }*/hr
        {
            margin-bottom: 2px;
            margin-top: 2px;
        }
        .modal
        {
            z-index: 99999;
            border-radius: 0px;
            overflow: auto !important;
        }
        input[type="radio"]
        {
            display: inline;
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
        .control-label
        {
            padding-top: 5px;
        }
        .btnSeguimiento
        {
            padding: 0px;
        }
    </style>
</head>
<body>
    <form id="frmRegistro" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="updLoading" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="piluku-preloader text-center">
                <div class="loader">
                    Loading...</div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" />
            <asp:AsyncPostBackTrigger ControlID="btnBusquedaEstudiante" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarAnular" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarDerivar" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="updLista" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div runat="server" class="col-12" id="Lista">
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="TituloPanel">
                            Listado de Tickets</h3>
                    </div>
                    <div class="panel-body">
                        <input type="hidden" id="reguser" name="reguser" value="" runat="server" />
                        <div class='panel-body' style="padding: 8px;">
                            <div class="col-12 text-center">
                                <asp:LinkButton runat="server" ID="btnNuevo" class="btn btn-sm btn-success btn-radius"
                                    Text="<i class='ion-plus-round'></i>&nbsp;Nuevo Ticket" OnClientClick="fnLoading(true);"></asp:LinkButton>
                            </div>
                            <br />
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-sm-1 col-md-1 control-label">
                                        Estado</label>
                                    <div class="col-sm-6 col-md-6">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" Text=" Pendiente" ForeColor="White"
                                            Font-Bold="true" BackColor="Red" />
                                        &nbsp;
                                        <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" Text=" En Proceso" ForeColor="White"
                                            Font-Bold="true" BackColor="orange" />
                                        &nbsp;
                                        <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" Text="Observado" ForeColor="White"
                                            Font-Bold="true" BackColor="CadetBlue" />
                                        &nbsp;
                                        <asp:CheckBox ID="CheckBox4" runat="server" Text=" Resuelto" ForeColor="White" Font-Bold="true"
                                            BackColor="green" />
                                        &nbsp;
                                        <asp:CheckBox ID="CheckBox5" runat="server" Text=" Anulado" ForeColor="Black" Font-Bold="true"
                                            BackColor="Silver" />
                                    </div>
                                    <div class="col-sm-4 col-md-2">
                                        <asp:CheckBox ID="CheckBox6" runat="server" Text="&nbsp; <i class='ion ion-flag text-danger' style='font-size: 13px;'> En Seguimiento</i> "
                                            ForeColor="Black" Font-Bold="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-xs-12 col-sm-1 col-md-1 control-label">
                                        Servicio</div>
                                    <div class="col-xs-12 col-sm-3 col-md-3">
                                        <asp:DropDownList runat="server" ID="ddlServicioAtencion" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-xs-12 col-sm-3 col-md-3 control-label">
                                        Ticket/Estudiante/DNI/Código:</label>
                                    <div class="col-xs-6 col-sm-3 col-md-3">
                                        <asp:TextBox runat="server" CssClass="form-control" placeholder="####-#" ID="txtNroTicket"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 col-md-1">
                                        <asp:LinkButton runat="server" ID="btnBuscar" class="btn btn-sm btn-primary btn-radius"
                                            OnClientClick="fnLoading(true);"> <i class="ion-search"></i>&nbsp;Buscar</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="col-12">
                                <asp:GridView runat="server" ID="gvLista" CssClass="table table-condensed" DataKeyNames="codigo_toc,codigo_soc,orden_cso"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <%--  <asp:TemplateField HeaderText="#" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex + 1%>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="2%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSeguimiento" runat="server" BorderStyle="None" Text='<span class="ion ion-flag"></span>'
                                                    CssClass="btn btn-white btn-sm btn-radius btnSeguimiento" ToolTip="Seguimiento"
                                                    CommandName="Seguimiento" OnClientClick="fnAbrirModal('mdSeguimiento');return false;"
                                                    CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="nroticket_toc" HeaderText="TICKET" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="fecha_reg" HeaderText="FECHA REGISTRO" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="codigoUniver_Alu" HeaderText="CÓDIGO UNIVERSITARIO" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="alumno" HeaderText="ESTUDIANTE" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="abreviatura_Cpf" HeaderText="PROG. DE ESTUDIOS" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="asunto_toc" HeaderText="ASUNTO" HeaderStyle-Width="20%" />
                                        <asp:BoundField DataField="nombre_soc" HeaderText="SERVICIO" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="nombre_ioc" HeaderText="UBICACIÓN" HeaderStyle-Width="10%" />
                                        <asp:BoundField DataField="nombre_eoc" HeaderText="ESTADO" HeaderStyle-Width="5%" />
                                        <asp:BoundField DataField="diaspendientes" HeaderText="DIAS PARA RESOLVER" HeaderStyle-Width="8%" />
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAtender" runat="server" Text='<span class="fa fa-pencil-square-o "></span>'
                                                    CssClass="btn btn-info btn-sm btn-radius" ToolTip="Atender" CommandName="Atender"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRetornar" runat="server" Text='<span class="fa fa-mail-reply"></span>'
                                                    CssClass="btn btn-primary btn-sm btn-radius" ToolTip="Retornar" CommandName="Retornar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDerivar" runat="server" Text='<span class="fa fa-mail-forward"></span>'
                                                    CssClass="btn btn-warning btn-sm btn-radius" ToolTip="Derivar" CommandName="Derivar"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnAnular" runat="server" Text='<span class="fa fa-close"></span>'
                                                    CssClass="btn btn-danger btn-sm btn-radius" ToolTip="Anular" CommandName="Anular"
                                                    OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataTemplate>
                                        <b>No se encontraron registros</b>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscar" />
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" />
            <asp:PostBackTrigger ControlID="btnGuardar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarAnular" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarDerivar" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" />
            
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" class="col-12" id="Mantenimiento" visible="false">
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="H6">
                            Listado de Tickets</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" id="divVolver" style="display: none;">
                            <div class="form-group text-center">
                                <button type="button" id="btnVolver" class="btn btn-sm btn-danger btn-radius">
                                    <i class="ion-android-arrow-back"></i>&nbsp;Volver
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="col-xs-12 col-md-12 badge bg-danger"
                                    Font-Size="16px">Datos de Contacto</asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-2 col-md-2 control-label" style="font-weight: bold;
                                    padding-top: 0px;">
                                    ESTUDIANTE:</label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <div class="input-group demo-group">
                                        <asp:TextBox runat="server" ID="txtEstudiante" class="form-control" ReadOnly="true"></asp:TextBox>
                                        <span class="input-group-btn">
                                            <asp:LinkButton runat="server" ID="btnBuscarEstudiante" CssClass="btn btn-sm btn-primary btn-radius"
                                                Text="<i class='fa fa-search'></i>">
                                            </asp:LinkButton>
                                        </span>
                                    </div>
                                </div>
                                <label class="col-xs-12 col-sm-2 col-md-2 control-label" style="font-weight: bold;
                                    padding-top: 0px;">
                                    CORREO USAT</label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <asp:TextBox runat="server" ID="txtcorreousat" class="form-control" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-2 col-md-2 control-label" style="font-weight: bold;
                                    padding-top: 0px;">
                                    CELULAR:</label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <div class="input-group" style="z-index: 0">
                                        <asp:TextBox runat="server" ID="txtcelular" class="form-control"></asp:TextBox>
                                        <span class="input-group-addon" style="padding: 4px 8px; background-color: Orange;
                                            color: White;" id="spConfTf"><i class="ion-edit"></i></span>
                                    </div>
                                </div>
                                <label class="col-xs-12 col-sm-2 col-md-2 control-label" style="font-weight: bold;
                                    padding-top: 0px;">
                                    CORREO PERSONAL</label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <div class="input-group" style="z-index: 0">
                                        <asp:TextBox runat="server" ID="txtemail" class="form-control"></asp:TextBox>
                                        <span class="input-group-addon" style="padding: 4px 8px; background-color: Orange;
                                            color: White;" id="spConfEmail"><i class="ion-edit"></i></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-12">
                            &nbsp;
                        </div>
                        <div class="col-xs-12 col-md-12" style="text-align: center">
                            <a href="javascript:void(0);" class="btn btn-sm btn-success btn-radius" id="btnConfirmarDatos"
                                runat="server">Confirmar datos <i class="ion-android-phone-portrait"></i>&nbsp;<i
                                    class="ion-android-mail"> </i></a>
                        </div>
                        <div class="col-xs-12 col-md-12">
                            &nbsp;
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="col-xs-12 col-md-12 badge bg-danger"
                                    Font-Size="16px">Datos de Ticket</asp:Label>
                            </div>
                        </div>
                        <div runat="server" id="lblMensajeValidación" style="font-size: 14px;">
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:HiddenField runat="server" ID="hda" Value="0" />
                                <asp:HiddenField runat="server" ID="hdc" Value="0" />
                                <asp:Label ID="Label14" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="txtAsunto">Asunto:</asp:Label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <asp:UpdatePanel runat="server" ID="updAsunto" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtAsunto" OnTextChanged="txtAsunto_TextChanged"
                                                AutoPostBack="True" CssClass="form-control" MaxLength="400" placeholder="Ingrese asunto"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblContadorAsunto" CssClass="control-label" Font-Bold="true">0</asp:Label>
                                            <asp:Label runat="server" ID="label10" CssClass="control-label" Font-Bold="true"> de 400 caracteres permitidos</asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtAsunto" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="txtDescripcion">Descripción:</asp:Label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <asp:UpdatePanel runat="server" ID="UpdatePanel6" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" AutoPostBack="true"
                                                placeholder="Ingrese descripción" TextMode="MultiLine" Rows="6" MaxLength="4000"></asp:TextBox>
                                            <asp:Label runat="server" ID="lblcontador" CssClass="control-label" Font-Bold="true">0</asp:Label>
                                            <asp:Label runat="server" ID="label8" CssClass="control-label" Font-Bold="true"> de 4000 caracteres permitidos</asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtDescripcion" EventName="TextChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="txtAsunto">Adjuntar(Opcional):</asp:Label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <asp:FileUpload runat="server" ID="archivo" CssClass="form-control" />
                                    <ul>
                                        <li>Archivos permitidos: <span style="color: Red">.jpg, .jpeg, .png, .doc, .docx, .pdf,
                                            .rar</span></li>
                                        <li>Tamaño Máximo: <span style="color: Red">20 MB</span></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="txtDescripcion">Vincular trámites(Opcional): <br />Si la solicitud se refiere a un trámite selecciónalo</asp:Label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <asp:LinkButton runat="server" ID="btnAsociar" OnClientClick="fnAbrirModal('mdTramites'); return false;"
                                        CssClass="btn btn-sm btn-warning btn-radius">
                                     <i class="ion-search"></i>&nbsp; Seleccionar el trámite
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="ddlOrigen">Canal de recepción:</asp:Label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <asp:DropDownList runat="server" ID="ddlOrigen" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label ID="Label9" runat="server" CssClass="col-xs-12 col-sm-2 col-md-2 control-label"
                                    For="ddlTipo">Clasificación:</asp:Label>
                                <div class="col-xs-12 col-sm-3 col-md-3">
                                    <asp:DropDownList runat="server" ID="ddlTipo" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                    For="ddlServicio">Servicio:</asp:Label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <asp:DropDownList runat="server" ID="ddlServicio" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div runat="server" id="DivEstado">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label19" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                        For="ddlOrigen">Estado:</asp:Label>
                                    <div class="col-xs-12 col-sm-4 col-md-4">
                                        <asp:TextBox runat="server" ID="txtEstado" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                    <asp:Label ID="Label21" runat="server" CssClass="col-xs-12 col-sm-2 col-md-2 control-label"
                                        For="ddlOrigen">Nivel de Atención:</asp:Label>
                                    <div class="col-xs-12 col-sm-3 col-md-3">
                                        <asp:TextBox runat="server" ID="txtNivelAtencion" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label20" runat="server" CssClass="col-xs-12 col-sm-3 col-md-3 control-label"
                                        For="ddlOrigen">Rol Responsable:</asp:Label>
                                    <div class="col-xs-12 col-sm-8 col-md-8">
                                        <asp:TextBox runat="server" ID="txtRol" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" runat="server" id="DivGuardar">
                            <div class="form-group text-center">
                                <asp:LinkButton runat="server" ID="btnGuardar" OnClientClick="Validar(this,'¿Está seguro que desea guardar los datos del ticket?','Luego no podrá actualizarlos'); return false;"
                                    CssClass="btn btn-sm btn-success btn-radius">
                                 <i class="ion-android-done"></i>&nbsp;Guardar
                                </asp:LinkButton>
                                <asp:LinkButton runat="server" ID="btnCancelar" CssClass="btn btn-sm btn-danger btn-radius"
                                    Text="<i class='ion-close'></i>&nbsp;Cancelar" OnClientClick="fnLoading(true);"></asp:LinkButton>
                            </div>
                        </div>
                        <div runat="server" id="DivSeguimiento">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label ID="Label17" runat="server" CssClass="col-xs-12 col-md-12 badge bg-danger"
                                        Font-Size="16px">Seguimiento de Ticket</asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:LinkButton runat="server" ID="btnSolucionar" class="btn btn-sm btn-primary btn-radius"> <i class="fa fa-pencil-square-o "></i>&nbsp;Solucionar </asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="btnDerivar" class="btn btn-sm btn-orange btn-radius"> <i class="fa fa-mail-forward"></i>&nbsp;Derivar</asp:LinkButton>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <div class="col-12">
                                        <asp:GridView runat="server" ID="gvSeguimiento" CssClass="table table-condensed"
                                            DataKeyNames="codigo_toc,IdArchivosCompartidos" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField DataField="fecha" HeaderText="FECHA" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="hora" HeaderText="HORA" HeaderStyle-Width="10%" />
                                                <asp:BoundField DataField="Rol" HeaderText="ROL" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="usuario" HeaderText="USUARIO" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="accion_dto" HeaderText="ACCIÓN" HeaderStyle-Width="15%" />
                                                <asp:BoundField DataField="descripcion_dto" HeaderText="DETALLE" HeaderStyle-Width="30%" />
                                                <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDescargar" runat="server" Text='<span class="fa fa-download"></span>'
                                                            CssClass="btn btn-info btn-sm btn-radius" ToolTip="Descargar" CommandName="Descargar"
                                                            OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'> 
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                            <RowStyle Font-Size="11px" />
                                            <EmptyDataTemplate>
                                                <b>No se encontraron registros</b>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnNuevo" />
            <asp:PostBackTrigger ControlID="btnGuardar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelar" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvEstudiantes" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnDerivar" />
            <asp:AsyncPostBackTrigger ControlID="btnCancelarDerivar" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" runat="server" id="DivDerivar" visible="false">
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="H2">
                            Derivar Ticket</h3>
                    </div>
                    <div class="panel-body">
                        <asp:HiddenField runat="server" ID="hdprocedencia" Value="0" />
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-3 col-md-3 control-label" for="txtServicioD" id="Label3"
                                    style="padding-top: 0px;">
                                    Servicio</label>
                                <div class="col-xs-12 col-sm-8 col-md-9">
                                    <asp:TextBox runat="server" ID="txtServicioD" CssClass="form-control" MaxLength="400"
                                        Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-3 col-md-3 control-label" for="txtNivelD" id="Label12"
                                    style="padding-top: 0px;">
                                    Nivel</label>
                                <div class="col-xs-12 col-sm-3 col-md-3">
                                    <asp:TextBox runat="server" ID="txtNivelD" CssClass="form-control" MaxLength="400"
                                        Enabled="false"></asp:TextBox>
                                </div>
                                <label class="col-xs-12 col-sm-1 col-md-1 control-label" for="txtRolD" id="Label13"
                                    style="padding-top: 0px;">
                                    Rol</label>
                                <div class="col-xs-12 col-sm-4 col-md-4">
                                    <asp:TextBox runat="server" ID="txtRolD" CssClass="form-control" MaxLength="400"
                                        Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-3 col-md-3 control-label" for="txtMotivoAnulacion"
                                    id="Label15" style="padding-top: 0px;">
                                    Motivo de derivación:</label>
                                <div class="col-xs-12 col-sm-8 col-md-9">
                                    <asp:TextBox runat="server" ID="txtMotivoDerivar" CssClass="form-control" placeholder="Ingrese el motivo"
                                        TextMode="MultiLine" Rows="6"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-xs-12 col-md-12">
                                    <center>
                                        <asp:LinkButton runat="server" ID="btnGuardarDerivar" CssClass="btn btn-sm btn-success btn-radius"
                                            Text="<i class='ion-android-done'></i>&nbsp;Derivar"></asp:LinkButton>
                                        <asp:LinkButton runat="server" ID="btnCancelarDerivar" CssClass="btn btn-sm btn-danger btn-radius"
                                            Text="<i class='ion-close'></i>&nbsp;Cancelar"></asp:LinkButton>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancelarDerivar" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="btnDerivar" />
            <asp:AsyncPostBackTrigger ControlID="btnGuardarDerivar" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel3" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="row" runat="server" id="DivAnular" visible="false">
                <div class="panel panel-piluku">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="H1">
                            Anular Ticket</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-3 col-md-3 control-label" for="txtMotivoAnulacion"
                                    id="lbletiqueta" style="padding-top: 0px;">
                                    Motivo de anulación:</label>
                                <div class="col-xs-12 col-sm-8 col-md-9">
                                    <textarea id="txtMotivoAnulacion" name="txtMotivoAnulacion" class="form-control"
                                        placeholder="Escribe el motivo" maxlength="500" rows="6"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-xs-12 col-md-12">
                                    <center>
                                        <button type="button" runat="server" id="btnAnular" class="btn btn-sm btn-success btn-radius">
                                            <i class="ion-android-done"></i>&nbsp;Anular
                                        </button>
                                        <asp:LinkButton runat="server" ID="btnCancelarAnular" CssClass="btn btn-sm btn-danger btn-radius"
                                            Text="<i class='ion-close'></i>&nbsp;Cancelar"></asp:LinkButton>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCancelarAnular" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <div class="row">
        <div class="modal fade" id="mdSeguimiento" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <h4 class="modal-title" id="H3">
                            Asignar Seguimiento a ticket
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-xs-12 col-sm-3 col-md-3 control-label" for="txtMotivoAnulacion"
                                    id="Label16" style="padding-top: 0px;">
                                    Motivo de Seguimiento:</label>
                                <div class="col-xs-12 col-sm-9 col-md-9">
                                    <textarea id="Textarea1" name="txtMotivoAnulacion" class="form-control" placeholder="Escribe el motivo"
                                        maxlength="500" rows="6"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-xs-12 col-md-12">
                                    <center>
                                        <button type="button" runat="server" id="Button2" class="btn btn-sm btn-success btn-radius">
                                            <i class="ion-android-done"></i>&nbsp;Guardar
                                        </button>
                                        <button type="button" class="btn btn-sm btn-danger btn-radius" data-dismiss="modal">
                                            <i class="ion-close"></i>&nbsp;Cancelar
                                        </button>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel4" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row">
                <div class="modal fade" id="mdBusquedaEstudiante" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
                    aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <h4 class="modal-title" id="H4">
                                    Buscar estudiante
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="row">
                                    <div class="form-group">
                                        <label class="col-xs-12 col-sm-4 col-md-4 control-label" for="txtMotivoAnulacion"
                                            id="Label18" style="padding-top: 0px;">
                                            Apellidos y nombres/ DNI /Código universitario:</label>
                                        <div class="col-xs-12 col-sm-8 col-md-8">
                                            <asp:TextBox runat="server" ID="txtBusqueda" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-xs-12 col-md-12">
                                            <center>
                                                <asp:LinkButton runat="server" ID="btnBusquedaEstudiante" class="btn btn-sm btn-primary btn-radius"
                                                    OnClientClick="fnLoading(true);">
                                                        <i class="ion-search" ></i>&nbsp;Buscar
                                                </asp:LinkButton>
                                                <button type="button" class="btn btn-sm btn-danger btn-radius" data-dismiss="modal">
                                                    <i class="ion-close"></i>&nbsp;Cancelar
                                                </button>
                                            </center>
                                        </div>
                                    </div>
                                </div>
                                <asp:UpdatePanel runat="server" ID="UpdatePanel5" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="max-height: 300px; overflow: auto;">
                                            <asp:GridView runat="server" ID="gvEstudiantes" CssClass="table table-condensed"
                                                DataKeyNames="codigo_alu,correo_usat,correo_personal,celular" AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIVERSITARIO" HeaderStyle-Width="10%" />
                                                    <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" HeaderStyle-Width="40%" />
                                                    <asp:BoundField DataField="nombre_Cpf" HeaderText="PROGRAMA DE ESTUDIOS" HeaderStyle-Width="20%" />
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSeleccionar" runat="server" Text='<span class="fa fa-pencil-square-o "></span>'
                                                                CssClass="btn btn-info btn-sm btn-radius" ToolTip="Seleccionar" CommandName="Seleccionar"
                                                                OnClientClick="fnLoading(true);" CommandArgument='<%#Convert.ToString(Container.DataItemIndex)%>'>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                                <RowStyle Font-Size="12px" />
                                                <EmptyDataTemplate>
                                                    <b>No se encontraron registros</b>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnBusquedaEstudiante" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnBuscarEstudiante" />
            <asp:AsyncPostBackTrigger ControlID="gvEstudiantes" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel runat="server" ID="UpdatePanel7" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <div class="row col-xs-12 col-sm-12 col-lg-12">
                <div class="modal fade  bd-example-modal-lg" id="mdTramites" tabindex="-1" role="dialog"
                    aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="float: right;">
                                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                                </button>
                                <h4 class="modal-title" id="H5">
                                    Vincular trámites
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div style="max-height: 650px; overflow: auto;">
                                    <asp:GridView runat="server" ID="gvTramites" CssClass="table table-condensed" DataKeyNames="codigo_trl,vinculado"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" HeaderStyle-Width="3%" ShowHeader="false">
                                                <ItemTemplate>
                                                    <asp:CheckBox runat="server" ID="chk" Checked="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="glosaCorrelativo_trl" HeaderText="N° TRÁMITE" HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="descripcion_ctr" HeaderText="TRÁMITE" HeaderStyle-Width="30%" />
                                            <asp:BoundField DataField="observacion_trl" HeaderText="DESCRIPCIÓN" HeaderStyle-Width="30%" />
                                            <asp:BoundField DataField="fechaReg_trl" HeaderText="FECHA REG." HeaderStyle-Width="10%" />
                                            <asp:BoundField DataField="estado_ctr" HeaderText="ESTADO" HeaderStyle-Width="10%" />
                                        </Columns>
                                        <HeaderStyle Font-Size="11px" Font-Bold="true" BackColor="#e33439" ForeColor="white" />
                                        <RowStyle Font-Size="12px" />
                                        <EmptyDataTemplate>
                                            <b>No se encontraron registros</b>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvEstudiantes" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="gvLista" EventName="RowCommand" />
        </Triggers>
    </asp:UpdatePanel>
    </div> </div>
    </form>
</body>
</html>
