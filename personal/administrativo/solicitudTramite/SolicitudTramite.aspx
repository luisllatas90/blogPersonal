<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SolicitudTramite.aspx.vb"
    Inherits="_SolicitudTramite" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link href="../../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel='stylesheet' href='../../assets/css/material.css' />
    <link rel='stylesheet' href='../../assets/css/style.css?x=1' />

    <script src="../../assets/js/jquery.js" type="text/javascript"></script>

    <script src="../../assets/js/jquery.loadmask.min.js" type="text/javascript"></script>

    <style type="text/css">
        #txtDesde, #txtHasta, #txtFechaEstado
        {
            background-color: #C9DDF5;
        }
    </style>

    <script type="text/javascript" language="javascript">


        function DeshabilitarGuardaEnviaSI() {
            document.GetElementByTagId("btnConfirmarGuardarEnviarSI").disabled = true;
            return true;
        }

        $(document).ready(function() {

            $("#btnGuardarEnviar").click(function() {
                $("#divFormulario").attr("style", "display:none");
                $("#divConfirmaGuardarEnviar").attr("style", "display:block");
            })
            $("#btnGuardar").click(function() {
                $("#divFormulario").attr("style", "display:none");
                $("#divConfirmaGuardar").attr("style", "display:block");
            })
            $("#btnConfirmarGuardarEnviarNO").click(function() {
                $("#divFormulario").attr("style", "display:block");
                $("#divConfirmaGuardarEnviar").attr("style", "display:none");
            })

            $("#btnConfirmarGuardarNO").click(function() {
                $("#divFormulario").attr("style", "display:block");
                $("#divConfirmaGuardar").attr("style", "display:none");
            })
        })

        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        //if (top.location == self.location)
        //location.href='../../tiempofinalizado.asp'} //El ../ depende de la ruta de la página
          
    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-right: 15px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 28px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 4px;
        }
        .page_header
        {
            background-color: #FAFCFF;
        }
        .table-responsive th
        {
            text-align: center;
            padding: 6px 3px;
            font-size: small;
        }
        .aviso
        {
            text-align: left;
            font-size: small;
        }
    </style>
    <link rel="stylesheet" href="../js/jquery-ui.css" />

    <script src="../js/jquery-1.9.1.js" type="text/javascript"></script>

    <script type="text/javascript" src="../js/jquery-ui.js?x=1"></script>

    <%--<script src="../../javascript/DateTime/Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>--%>
    <%--<script src="../../javascript/DateTime/Scripts/calendar-en.min.js" type="text/javascript"></script>--%>
    <%--<link href="../../javascript/DateTime/Styles/calendar-blue.css" rel="stylesheet"
        type="text/css" />--%>

    <script src="../js/jquery.ui.datepicker-es.js?xq=3" type="text/javascript"></script>

    <script type="text/javascript">
        $(function() {
            $.datepicker.setDefaults($.datepicker.regional["es"]);
            $("#txtDesde").datepicker({
                firstDay: 1
            });
            $("#txtHasta").datepicker({
                firstDay: 1
            });
        });

        function DescargarArchivo(IdArchivo, tk) {
            window.open("DescargarArchivo.aspx?Id=" + IdArchivo + "&tk=" + tk, 'ta', "");
        }
        
    </script>

    <%--   <script type="text/javascript">

        $(document).ready(function() {
            //alert('hola');
            $("#<%=txtHoraIni.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
            $("#<%=txtHoraFin.ClientID %>").dynDateTime({
                showsTime: true,
                ifFormat: "%d/%m/%Y %H:%M",
                daFormat: "%l;%M %p, %e %m,  %Y",
                align: "BR",
                electric: false,
                singleClick: false,
                displayArea: ".siblings('.dtcDisplayArea')",
                button: ".next()"
            });
        });
    </script>  --%>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="Form1" runat="server" name="frm">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">
                        <asp:Label ID="nombre_titulo" runat="server" Text="Registro de "></asp:Label>Solicitud
                        de Trámite N°
                        <asp:Label ID="lblNumero_Tramite" runat="server" Text="Nuevo" Style="color: #1313CC"></asp:Label></span>
                    <p class="text">
                        Solicitud&nbsp;de&nbsp;Trámite N°
                        <asp:Label ID="lblNumero_Tramite1" runat="server" Text="Nuevo" Style="color: #1313CC"></asp:Label>
                    </p>
                </div>
                <div class="right pull-right">
                    <%-- <ul class="right_bar">
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Headings</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;Inline
                            Text Elements</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;alignment
                            Classes</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;List
                            Types &amp; Groups</li>
                        <li class="list-unstyled"><i class="icon ion-checkmark text-primary"></i>&nbsp;and more...</li>
                    </ul>--%>
                </div>
            </div>
            <div class="panel panel-piluku" id="PanelLista">
                <div class="panel-heading">
                    <h3 class="panel-title">
                        <%--<span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <div id="divFormulario" runat="server">
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Estado :</label>
                                <asp:Label ID="lblEstado" class="col-md-2 control-label" Style="color: #CC0000" Font-Bold="True"
                                    runat="server" Text="Nuevo"></asp:Label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtFechaEstado" runat="server" CssClass="form-control" AutoPostBack="true"
                                        Style="color: #CC0000; text-align: center" Enabled="false"></asp:TextBox>
                                </div>
                                <label class="col-md-1 control-label">
                                    Prioridad :</label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlPrioridad" runat="server" AutoPostBack="True" CssClass="form-control">
                                        <asp:ListItem Value="N" Selected="True">Normal</asp:ListItem>
                                        <asp:ListItem Value="U">Urgente</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label ">
                                    Tipo de Solicitud :</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTipoSolicitud" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:CheckBox ID="chkCumple" runat="server" AutoPostBack="True" Text="" TabIndex="1">
                                    </asp:CheckBox>
                                    <font style="float: left" id="ftchkCumple" runat="server">Cumpleaños</font>
                                    <asp:Label ID="lblchkCump" runat="server" class="control-label" Visible="false">
                                    </asp:Label>
                                    <asp:Label ID="lblCorreo" runat="server" class="control-label" Visible="false">
                                    </asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblAvisoTipoSol" class="aviso" Style="color: #CC0000" runat="server"></asp:Label>
                                </div>
                                <div class="col-md-7">
                                    <asp:Label ID="lblVacaciones" class="aviso" Style="color: #CC0000; font-style: oblique"
                                        runat="server" Text="*Si se trata de Vacaciones no debe sobrepasar el límite de días pendientes"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divTipoLicencia" runat="server">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblTipoLicencia" runat="server" class="control-label" style="margin-left">
                                        Tipo de Licencia C/Goce:</label>
                                </div>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlTipoLicencia" runat="server" AutoPostBack="True" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblMotivoLicencia" runat="server" Text="*Motivo Licencia:"
                                    Style="color: #1313CC">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlMotivoLicencia" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <%--<div class="col-md-2">
                                    <asp:DropDownList ID="ddlDetalleLicencia" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>--%>
                                <asp:Label class="col-md-1 control-label" ID="lblPapel" runat="server" Text="*Papel:"
                                    Style="color: #1313CC">
                                </asp:Label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divActividad" runat="server">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblActividad" runat="server" class="control-label" style="margin-left: 0;
                                        color: #1313CC">
                                        *Nombre de Actividad:</label>
                                </div>
                                <div class="col-md-5">
                                    <asp:TextBox ID="txtActividad" runat="server" AutoPostBack="true" CssClass="form-control"
                                        MaxLength="500"></asp:TextBox>
                                </div>
                                <asp:Label class="col-md-1 control-label" ID="lblInstitucion" runat="server" Text="*Institución:"
                                    Style="color: #1313CC">
                                </asp:Label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtInstitucion" runat="server" AutoPostBack="true" CssClass="form-control"
                                        MaxLength="600"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divPais" runat="server">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblPais" runat="server" class="control-label" style="margin-left: 0; color: #1313CC">
                                        *País:</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <asp:Label class="col-md-1 control-label" ID="lblCiudad" runat="server" Text="*Ciudad:"
                                    Style="color: #1313CC"></asp:Label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCiudad" runat="server" AutoPostBack="true" CssClass="form-control"
                                        MaxLength="150"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Fecha de Inicio :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDesde" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblDesde" runat="server" class="col-md-1 control-label" Visible="false"></asp:Label>
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblHoraInicio" runat="server" Text="Hora Inicio:">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlHoraInicio" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Fecha de Fin :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtHasta" runat="server" AutoPostBack="true" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblHasta" runat="server" class="col-md-1 control-label" Visible="false"></asp:Label>
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblHoraFin" runat="server" Text="Hora Final:">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlHoraFin" runat="server" AutoPostBack="true" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <asp:Label ID="lblNumDias" runat="server" class="control-label" Text="Días a Solicitar :">
                                    </asp:Label>
                                    <%--<label id="lblNumDias" runat="server" class="control-label" style="margin-left">
                                        Días Solicitados:</label>--%>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblNum_dias" runat="server" class="control-label" Style="color: #1313CC"
                                        Font-Bold="True" Text ="--">
                                    </asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblHoras" runat="server" CssClass="control-label" Text="Total Tiempo :"
                                        Style="color: #CC0000">
                                    </asp:Label>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="lblTotalHoras" runat="server" class="control-label" Style="color: #1313CC"
                                        Font-Bold="True">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-4">
                                    <label id="lblCumple" runat="server" class="control-label" style="color: #CC0000">
                                        *Se otorga el mismo día del Cumpleaños
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row" id="divSaldo" runat="server">
                            <div class="form-group">
                                <label id="lblSaldoDias" class="col-md-2 control-label" runat="server">
                                    Saldo de Días :</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSPend" runat="server" class="control-label" Font-Bold="True">
                                    </asp:Label>
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="lblDiasPend" runat="server" class="control-label" Text="*Usted Dispone de :"
                                        Style="color: #CC0000">
                                    </asp:Label>
                                    <asp:Label ID="lblDPendi" runat="server" class="control-label">
                                    </asp:Label>
                                    <asp:Label ID="lblpendientes" runat="server" class="control-label" Text=" días pendientes"
                                        Style="color: #CC0000">
                                    </asp:Label>
                                    <asp:Label ID="lblFechaIni" runat="server" class="control-label">
                                    </asp:Label>
                                    <asp:Label ID="lblAuxPend" runat="server" class="control-label">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblMotivo" class="col-md-2 control-label" runat="server">
                                    Motivo :</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtmotivo" runat="server" Visible="true" MaxLength="1000" CssClass="form-control"
                                        Rows="3" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <asp:Label ID="lblmotivo_text" runat="server" class="control-label" Visible="false">
                                    </asp:Label>
                                </div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblMotivoDescansoMedico" class="aviso" Style="color: #CC0000; font-style: oblique"
                                        runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblObservaDirector" runat="server" class="control-label" style="margin-left">
                                        Observación Director :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacion" runat="server" AutoPostBack="True" MaxLength="1000"
                                        CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblObservaPersonal" runat="server" class="control-label" style="margin-left">
                                        Observación Personal :</label>
                                </div>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacionPersonal" runat="server" AutoPostBack="True" MaxLength="1000"
                                        CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblAdjuntos" class="col-md-2 control-label" runat="server">
                                    Subir Adjuntos:</label>
                                <div class="col-md-4">
                                    <asp:FileUpload ID="files" runat="server" CssClass="form-control" multiple="multiple" />
                                </div>
                                <asp:Button ID="btnSubirAdjunto" runat="server" Text="Subir" ToolTip="*Sube archivos.."
                                    CssClass="btn btn-success" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-5">
                                    <asp:Label ID="lblAvisoSubida" class="aviso" Style="color: #CC0000" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-5">
                                    <asp:Label ID="lbladjunto1" class="aviso" Style="color: #1313CC" runat="server" Text="*Puedes adjuntar varios archivos manteniendo presionada la tecla CONTROL "></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="lbladjunto2" class="aviso" Style="color: #1313CC" runat="server" Text="*Tener en cuenta que cada archivo debe pesar hasta 2MB como máximo (Extensión: .rar,  .pdf, .jpg)"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row ">
                            <div class="form-group">
                                <center>
                                    <asp:Label ID="lblMensaje0" runat="server" class="col-md-10 control-label" Style="color: #FF6833"
                                        Font-Bold="true"></asp:Label>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <p>
                            </p>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblAdjuntos1" class="col-md-2 control-label" runat="server">
                                    Archivos Adjuntos:</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-8">
                                <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ast,ID,token"
                                    CssClass="table-responsive">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField HeaderText="#">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre del Archivo" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha de Registro" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Descargar" />
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                                    ImageUrl="../../../images/eliminar.gif" Text="Eliminar" OnClientClick="return confirm('¿Desea Eliminar Registro?.')" />
                                            </ItemTemplate>
                                            <ControlStyle Font-Underline="True" ForeColor="Blue" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="codigo_ast" HeaderText="ID" Visible="false" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#E33439" ForeColor="White" Font-Size="Small" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#999999" />
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-2">
                                <asp:HiddenField ID="hddEliminar" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <p runat="server" id="celdaGrid" visible="false" style="font-weight: bold">
                                        &nbsp;</p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div id="divMensaje" runat="server">
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <center>
                                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" ToolTip="*Guarda la solicitud de trámite.."
                                        CssClass="btn btn-success" OnClientClick="return false;" />
                                    <asp:Button ID="btnGuardarEnviar" runat="server" Text="Guardar / Enviar" ToolTip="*Guarda y Envía la solicitud de trámite.."
                                        CssClass="btn btn-success" OnClientClick="return false;" />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" ToolTip="*Cancela la solicitud de trámite.."
                                        CssClass="btn btn-success" />
                                </center>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaGuardar" runat="server" style="display: none">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO SEGURO QUE DESEA GUARDAR LA SOLICITUD DE TRÁMITE?</label>
                        <asp:Button ID="btnConfirmarGuardarSI" runat="server" Text="SI" CssClass="btn btn-success" />
                        <asp:Button ID="btnConfirmarGuardarNO" runat="server" Text="NO" CssClass="btn btn-success"
                            OnClientClick="return false;" />
                    </div>
                    <div id="divConfirmaGuardarEnviar" runat="server" style="display: none">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO SEGURO QUE DESEA GUARDAR Y ENVIAR LA SOLICITUD DE TRÁMITE?</label>
                        <asp:Button ID="btnConfirmarGuardarEnviarSI" runat="server" Text="SI" CssClass="btn btn-success"
                            OnClientClick="return DeshabilitarGuardaEnviaSI();" />
                        <asp:Button ID="btnConfirmarGuardarEnviarNO" runat="server" Text="NO" CssClass="btn btn-success"
                            OnClientClick="return false;" />
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
