<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCalificadorEstudiante.aspx.vb"
    Inherits="GestionCurricular_FrmCalificadorEstudiante" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Calificación del Estudiante</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/bootstrap-treeview/bootstrap-treeview.min.css" rel="stylesheet"
        type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../assets/jquery/jquery-3.3.1.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript" src="../assets/bootstrap-treeview/bootstrap-treeview.min.js"></script>

    <script type="text/javascript">
        function LoadTree(json) {
            var $tree = $('#tree').treeview({
                expandIcon: 'glyphicon glyphicon-chevron-right',
                collapseIcon: 'glyphicon glyphicon-chevron-down',
                selectedColor: "#FFFFFF",
                onhoverColor: "#FFF0EF",
                showTags: true,
                data: json
            });

            $('#tree li .badge').each(function() {
                var text = $(this).html().trim();
                if ($.isNumeric(text)) {
                    if (text > 13.49) {
                        $(this).css({ 'background-color': "#0C8710" });
                    } else {
                        $(this).css({ 'background-color': "#D9534F" });
                    }
                }
            });
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
    </script>

    <style type="text/css">
        .no-border
        {
            border: 0;
            box-shadow: none;
        }
        .panel
        {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div id="Div1" class="panel panel-default" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                    <div class="col-sm-1">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-left"></i></span> Volver
                        </asp:LinkButton>
                    </div>
                    <div class="col-sm-11">
                        <h4>
                            <label id="lblAlumno" runat="server">
                                Calificación por Alumno</label></h4>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row" id="divFiltro1" runat="server">
                    <div class="col-xs-12">
                        <div class="form-group">
                            <label class="col-xs-2">
                                Semestre:</label>
                            <div class="col-xs-3">
                                <asp:DropDownList ID="ddlSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <label class="col-xs-2">
                                Curso:</label>
                            <div class="col-xs-5">
                                <asp:DropDownList ID="ddlCurso" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div class="row" id="divFiltro2" runat="server">
                    <div class="col-xs-12">
                        <div class="form-group">
                            <label class="col-xs-2">
                                Alumno:</label>
                            <div class="col-xs-10">
                                <asp:DropDownList ID="ddlAlumno" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br />
                </div>
                <div id="tree" class="treeview">
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
