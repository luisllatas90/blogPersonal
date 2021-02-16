<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmImportarComunicaciones.aspx.vb"
    Inherits="Crm_FrmImportarComunicaciones" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta http-equiv="Pragma" content="no-cache" />
    <link rel='stylesheet' href='../assets/css/bootstrap.min.css?x=1' />
    <link rel='stylesheet' href='../assets/css/material.css' />
    <link rel='stylesheet' href='../assets/css/style.css?x=1' />

    <script type="text/javascript" src='../assets/js/jquery.js'></script>

    <title>CRM - Importar Comunicaciones</title>

    <script type="text/javascript">
        function fnValidar() {
            $("#Mensaje").html("");
            $("#Mensaje").removeAttr("class");
            if ($("#ddlTipoEstudio").val() == "") {
                $("#Mensaje").html("Seleccione un Tipo de Estudio");
                $("#Mensaje").attr("class", "alert alert-danger");
                return false
            }
            if ($("#ddlConvocatoria").val() == "") {
                $("#Mensaje").html("Seleccione una Convocatoria");
                $("#Mensaje").attr("class", "alert alert-danger");
                return false
            }
            if ($("#ddlEvento").val() == "") {
                $("#Mensaje").html("Seleccione un Evento");
                $("#Mensaje").attr("class", "alert alert-danger");
                return false
            }
            if ($("#ArchivoASubir").val() == "") {
                $("#Mensaje").html("Adjuntar Archivo a Importar");
                $("#Mensaje").attr("class", "alert alert-danger");
                return false
            }

            if ($("#ArchivoASubir").val() != '') {
                archivo = $("#ArchivoASubir").val();
                //Extensiones Permitidas
                //        extensiones_permitidas = new Array(".pdf", ".doc", ".docx");
                //extensiones_permitidas = new Array(".xls", ".xlsx");
                extensiones_permitidas = new Array(".csv");
                //recupero la extensión de este nombre de archivo
                // recorto el nombre desde la derecha 4 posiciones atras (por lo general Ubicación de la Extensión)
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
                    //$("#Mensaje").html("Solo puede Adjuntar Archivos de Informe en Formato de Excel (.xls o .xlsx)");

                    $("#Mensaje").html("Solo puede Adjuntar Archivos con el formato de la plantilla en (.csv)");
                    $("#Mensaje").attr("class", "alert alert-danger");
                    return false
                }
            }
            if (confirm("¿Seguro que Deseas Importar los Datos?")) {
                return true;
            } else {
                return false;
            }
        }
    
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
        #UpdateProgress1
        {
            width: 400px;
            background-color: #FFC080;
            bottom: 0%;
            left: 0px;
            position: absolute;
        }
    </style>
</head>
<body>
    <div class="piluku-preloader text-center hidden">
        <div class="loader">
            Loading...</div>
    </div>
    <div class="wrapper">
        <div class="content">
            <div class="page_header">
                <div class="pull-left">
                    <i class="icon ti-import page_header_icon"></i><span class="main-text">Importar Comunicaciones</span>
                    <p class="text">
                        Importar Comunicaciones desde Excel.
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
                        Importar Comunicaciones
                        <%--                <span class="panel-options"><a class="panel-refresh"
                    href="#"> <i class="icon ti-reload" onclick="">
                    </i></a><a class="panel-minimize" href="#"><i class="icon ti-angle-up"></i></a>
                </span>--%>
                    </h3>
                </div>
                <div class="panel-body">
                    <form id="frmImportar" runat="server" enctype="multipart/form-data">
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <div class="row">
                        <div class="form-group">
                            <label class="col-md-2 control-label ">
                                Archivo</label>
                            <div class="col col-md-4">
                                <asp:FileUpload runat="server" ID="ArchivoASubir" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div id="Mensaje" runat="server">
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group">
                            <div class="col col-md-12">
                                <center>
                                    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                        <ProgressTemplate>
                                            Procesando
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>--%>
                                    <asp:Button runat="server" ID="btnImportar" CssClass="btn btn-success" Text="Importar"
                                        OnClientClick="javascript:return fnValidar();" />
                                </center>
                            </div>
                        </div>
                    </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
