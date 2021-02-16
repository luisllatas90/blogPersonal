<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioInteres.aspx.vb" Inherits="CambioInteres" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Cambio de interés</title>
    <link href="libs/bootstrap-4.1/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" onsubmit="return false;">
    <div class="container">
        <div class="panel panel-default" style="margin-top: 15px; background-color: #FBFBFB;">
            <div class="panel panel-body">
                <div class="panel panel-body">
                    <fieldset>
                        <legend>Datos del Interesado</legend>
                        <div class="row">
                            <label>
                                <i class="fas fa-check"></i>Nombre:</label><br />
                            <span style="padding-left: 5em">María Fernández Ortíz</span>
                        </div>
                        <div class="row">
                            <label>
                                <i class="fas fa-check"></i>E-mail:</label><br />
                            <span style="padding-left: 5em"><a style="cursor: pointer">mfernandez@gmail.com</a></span>
                        </div>
                        <div class="row">
                            <label>
                                <i class="fas fa-check"></i>Número Telf.:</label><br />
                            <span style="padding-left: 5em">979797979</span>
                        </div>
                        <div class="row">
                            <label>
                                <i class="fas fa-check"></i>Interés:</label><br />
                            <span style="padding-left: 5em">Diplomado de Arquitectura de Software</span>
                        </div>
                        <div class="row">
                            <label>
                                <i class="fas fa-check"></i>Fecha Inicio:</label><br />
                            <span style="padding-left: 5em">21 Septiembre 2018</span>
                        </div>
                        <div class="row">
                            &nbsp;
                        </div>
                    </fieldset>
                    <div class="row">
                        <fieldset>
                            <legend>Lista de interés vigentes</legend>
                            <input type="checkbox" id="chk1" name="chk1" checked="checked" />
                            <label for="chk1">
                                Diplomado de Arquitectura de Software <i>(21/09/2018)</i></label><br />
                            <input type="checkbox" id="chk2" name="chk2" />
                            <label for="chk2">
                                Diplomado de Procesos con BPM <i>(28/10/2018)</i></label><br />
                            <input type="checkbox" id="chk3" name="chk3" />
                            <label for="chk3">
                                Diploma en Gestión de Calidad y Procesos <i>(01/11/2018)</i></label><br />
                            <input type="checkbox" id="chk4" name="chk4" />
                            <label for="chk4">
                                Diploma en Habilidades directivas <i>(25/11/2018)</i></label><br />
                            <input type="checkbox" id="chk5" name="chk5" />
                            <label for="chk5">
                                Diploma en Gestión del talento humano <i>(08/10/2018)</i></label><br />
                            <input type="checkbox" id="chk6" name="chk6" />
                            <label for="chk6">
                                Diploma en Marketing Digital <i>(01/09/2018)</i></label><br />
                            <input type="checkbox" id="chk7" name="chk7" />
                            <label for="chk7">
                                Diploma Finanzas corporativas <i>(15/11/2018)</i></label><br />
                            <input type="checkbox" id="chk8" name="chk8" />
                            <label for="chk8">
                                Diploma internacional en ventas <i>(01/10/2018)</i></label><br />
                            <input type="checkbox" id="chk9" name="chk9" />
                            <label for="chk9">
                                Diploma en Tributación Empresarial <i>(25/09/2018)</i></label><br />
                        </fieldset>
                    </div>
                    <div class="panel-body" style="border-top: 1px solid #e5e5e5; margin-top: 10px;">
                        <div class="row">
                            <button id="btnActualizar" name="btnActualizar" class="btn btn-success">
                                Actualizar
                            </button>
                            <button id="btnCancelar" name="btnCancelar" class="btn btn-danger">
                                Cancelar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>
    
    <script src="libs/bootstrap-4.1/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/fontawesome.min.js" type="text/javascript"></script>

    <script src="js/programacion.js" type="text/javascript"></script>

</body>
</html>
