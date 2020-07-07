<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AprobacionSolicitudTramite.aspx.vb"
    Inherits="_AprobacionSolicitudTramite" %>

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

    <script type="text/javascript" language="JavaScript" src="../../private/jq/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="javascript" src="../../private/jq/jquery.mascara.js"></script>

    <style type="text/css">
        #txtDesde, #txtHasta, #txtFechaEstado
        {
            background-color: #C9DDF5;
        }
    </style>

    <script type="text/javascript" language="javascript">
        $(document).ready(function() {
            jQuery(function($) {
                $("#TxtFechaNac").mask("99/99/9999"); //.mask("(999)-999999");
                //   $("#txttelefono").mask("(999)-9999999");
                //   $("#txtcelular").mask("(999)-9999999");  
            });
        })
        function MarcarEvaluadores(obj) {
            //asignar todos los controles en array
            var arrChk = document.getElementsByTagName('input');
            for (var i = 0; i < arrChk.length; i++) {
                var chk = arrChk[i];
                //verificar si es Check
                if (chk.type == "checkbox") {
                    chk.checked = obj.checked;
                    if (chk.id != obj.id) {
                        PintarFilaMarcada(chk.parentNode.parentNode, obj.checked)
                    }
                }
            }
        }
        function PintarFilaMarcada(obj, estado) {
            if (estado == true) {
                obj.style.backgroundColor = "#FFE7B3"
            }
            else {
                obj.style.backgroundColor = "white"
            }
        }
        function DescargarArchivo(IdArchivo, tk) {
            window.open("DescargarArchivo.aspx?Id=" + IdArchivo + "&tk=" + tk, 'ta', "");
        }

        //if(top.location==self.location)
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
        /*
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }*/.page_header
        {
            background-color: #FAFCFF;
        }
        .table-responsive th
        {
            text-align: center;
            padding: 6px 3px;
            font-size: small;
        }
    </style>
</head>
<body>
    <div class="wrapper">
        <div class="content">
            <form id="Form1" runat="server" name="frm">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-bookmark-alt page_header_icon"></i><span class="main-text">
                        <asp:Label ID="nombre_titulo" runat="server" Text="Registro de "></asp:Label>
                        Solicitud de
                        <asp:Label ID="lblTipo_Solic" runat="server"></asp:Label>
                        N°
                        <asp:Label ID="lblNumero_Tramite" runat="server" Style="color: #1313CC"></asp:Label></span>
                    <p class="text">
                        Solicitud de
                        <asp:Label ID="lblTipo_Solic1" runat="server"></asp:Label>
                        N°
                        <asp:Label ID="lblNumero_Tramite1" runat="server" Style="color: #1313CC"></asp:Label>
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
                                <asp:Label ID="lblprioridad" class="col-md-1 control-label" runat="server" Font-Bold="True">                                                                 
                                </asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label ">
                                    Colaborador :</label>
                                <asp:Label ID="lblColaborador" runat="server" Font-Bold="True" class="col-md-5 control-label"></asp:Label>
                                <asp:Label ID="lblcod_EST" runat="server" class="col-md-1 control-label" Visible="false"></asp:Label>
                                <asp:Label ID="lblPorDirec" runat="server" class="col-md-1 control-label" Visible="false"></asp:Label>
                                <asp:Label ID="lblcodigo_Per" runat="server" class="col-md-1 control-label" Visible="false"></asp:Label>
                                <label class="col-md-1 control-label ">
                                    Evaluador:</label>
                                <asp:Label ID="lblPersonal" runat="server" Font-Bold="True" class="col-md-4 control-label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label ">
                                    Tipo de Solicitud :</label>
                                <div class="col-md-3">
                                    <asp:DropDownList ID="ddlTipoSolicitud" runat="server" AutoPostBack="True" CssClass="form-control"
                                        Enabled="false">
                                    </asp:DropDownList>
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
                                    <asp:DropDownList ID="ddlTipoLicencia" runat="server" AutoPostBack="True" CssClass="form-control" Enabled ="false">
                                    </asp:DropDownList>
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblMotivoLicencia" runat="server" Text="*Motivo Licencia:"
                                    Style="color: #1313CC">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlMotivoLicencia" runat="server" AutoPostBack="true" CssClass="form-control" Enabled ="false">
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
                                    <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="true" CssClass="form-control" Enabled ="false">
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
                                        MaxLength="500" Enabled ="false"></asp:TextBox>
                                </div>
                                <asp:Label class="col-md-1 control-label" ID="lblInstitucion" runat="server" Text="*Institución:"
                                    Style="color: #1313CC">
                                </asp:Label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtInstitucion" runat="server" AutoPostBack="true" CssClass="form-control" Enabled ="false"
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
                                    <asp:DropDownList ID="ddlPais" runat="server" AutoPostBack="true" CssClass="form-control" Enabled ="false">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                </div>
                                <asp:Label class="col-md-1 control-label" ID="lblCiudad" runat="server" Text="*Ciudad:"
                                    Style="color: #1313CC"></asp:Label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtCiudad" runat="server" AutoPostBack="true" CssClass="form-control" Enabled ="false"
                                        MaxLength="150" ></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label ">
                                    Fecha de Inicio:</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtDesde" runat="server" AutoPostBack="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Image ID="Image1" src="../img/calender.png" runat="server" Width="15px" Height="15px" />&nbsp;
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblHoraInicio" runat="server" Text="Hora Inicio:">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlHoraInicio" runat="server" AutoPostBack="true" CssClass="form-control"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Fecha de Fin :</label>
                                <div class="col-md-2">
                                    <asp:TextBox ID="txtHasta" runat="server" AutoPostBack="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:Image ID="Image2" src="../img/calender.png" runat="server" Width="15px" Height="15px" />&nbsp;
                                </div>
                                <asp:Label class="col-md-2 control-label" ID="lblHoraFin" runat="server" Text="Hora Final:">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:DropDownList ID="ddlHoraFin" runat="server" AutoPostBack="true" CssClass="form-control"
                                        Enabled="false">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-2">
                                    <label id="lblNumDias" runat="server" class="control-label" style="margin-left">
                                        Número de Días :</label>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblNum_dias" runat="server" class="control-label" Style="color: #1313CC"
                                        Font-Bold="True">
                                    </asp:Label>
                                </div>
                                <asp:Label ID="lblHoras" runat="server" class="col-md-2 control-label" Text="Total Tiempo:"
                                    Style="color: #CC0000">
                                </asp:Label>
                                <div class="col-md-2">
                                    <asp:Label ID="lblTotalHoras" runat="server" class="control-label" Style="color: #1313CC"
                                        Font-Bold="True">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <%--<label id="lblSaldoPend" class="col-md-2 control-label" runat="server">
                                    Saldo de Días :</label>
                                <div class="col-md-3">
                                    <asp:Label ID="lblSPend" runat="server" class="control-label" Style="color: #1313CC"
                                        Font-Bold="True">
                                    </asp:Label>
                                </div>--%>
                                <div class="col-md-6">
                                </div>
                                <div class="col-md-3">
                                <asp:Label ID="lblDiasPend" runat="server" class="control-label" Text="Días Pendientes:"
                                    Style="color: #CC0000">
                                </asp:Label>
                                
                                    <asp:Label ID="lblDPendi" runat="server" class="control-label" Visible="true">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblMotivo" class="col-md-2 control-label" runat="server">
                                    Motivo :</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtMotivo" runat="server" AutoPostBack="True" CssClass="form-control"
                                        MaxLength="1000" Enabled="false" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2 control-label">
                                    Observación Responsable:</label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtObservacion" runat="server" AutoPostBack="True" MaxLength="1000"
                                        CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
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
                        <div class="row ">
                            <div class="form-group">
                                <center>
                                    <asp:Label ID="lblMensaje0" runat="server" class="col-md-10 control-label" Style="color: #FF6833"
                                        Font-Bold="true"></asp:Label>
                                </center>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label id="lblAdjuntos" class="col-md-2 control-label" runat="server">
                                    Archivos Adjuntos:</label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-10">
                                <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ast,ID,token"
                                    CssClass="table-responsive">
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <Columns>
                                        <asp:BoundField HeaderText="#">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre del Archivo" />
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha de Registro" />
                                        <%--<asp:BoundField DataField="Ruta" HeaderText="Ruta" ReadOnly="true" />--%>
                                        <asp:BoundField DataField="Fecha" HeaderText="Descargar" />
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
                            <div class="col-md-1">
                                <asp:HiddenField runat="server" ID="hdid" Value="0" />
                                <asp:HiddenField runat="server" ID="hdctf" Value="0" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <div class="col-md-8">
                                    <p runat="server" id="celdaGrid" visible="false" style="font-weight: bold">
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <center>
                                    <asp:Button ID="btnAprobar" runat="server" Text="Aprobar" CssClass="btn btn-success"
                                        ToolTip="*Aprueba y Envía la solicitud a Personal." />
                                    <asp:Button ID="btnRechazar" runat="server" Text="Rechazar" CssClass="btn btn-success"
                                        ToolTip="*Rechaza la solicitud de trámite.." />
                                    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-success"
                                        ToolTip="*Cancela la solicitud de trámite.." />
                                </center>
                            </div>
                        </div>
                    </div>
                    <div id="divConfirmaAprobar" runat="server">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO QUE DESEA APROBAR LA SOLICITUD Y ENVIARLA A PERSONAL?
                        </label>
                        <asp:Button ID="btnConfirmarAprobarSI" runat="server" Text="SI" CssClass="btn btn-success"
                            Width="37px" />
                        <asp:Button ID="btnConfirmarAprobarNO" runat="server" Text="NO" CssClass="btn btn-success"
                            Width="41px" />
                    </div>
                    <div id="divConfirmaRechazar" runat="server">
                        <label style="font-weight: bold">
                            ¿ESTÁ SEGURO QUE DESEA RECHAZAR LA SOLICITUD DE TRÁMITE?
                        </label>
                        <asp:Button ID="btnConfirmarRechazarSI" runat="server" Text="SI" CssClass="btn btn-success"
                            Width="37px" />
                        <asp:Button ID="btnConfirmarRechazarNO" runat="server" Text="NO" CssClass="btn btn-success"
                            Width="41px" />
                    </div>
                </div>
            </div>
            </form>
        </div>
    </div>
</body>
</html>
