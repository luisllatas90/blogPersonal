<%@ Page Language="VB" AutoEventWireup="false" CodeFile="matriculafc.aspx.vb" Inherits="matriculafc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel='stylesheet' href='assets/css/fullcalendar.css' />
    <link rel='stylesheet' href='assets/css/fullcalendar-custom.css' />

    <script type="text/javascript" src='assets/js/jquery.dataTables.min.js'></script>

    <link rel='stylesheet' href='assets/css/jquery.dataTables.min.css' />

    <script type="text/javascript" src='assets/js/moment.min.js'></script>

    <script type="text/javascript" src='assets/js/fullcalendar.js?x=1'></script>

    <script src='assets/js/noty/jquery.noty.js'></script>

    <script src='assets/js/noty/layouts/top.js'></script>

    <script src='assets/js/noty/layouts/default.js'></script>

    <script src='assets/js/noty/jquery.desknoty.js'></script>

    <script src='assets/js/noty/notifications-custom.js?x=1'></script>

    <!--<script type="text/javascript" src='assets/js/fullcalendar-custom.js'></script>-->
    <style type="text/css">
        td.details-control
        {
            background: url('assets/img/details_open.png') no-repeat center center;
            width: 10%;
            cursor: pointer;
            font-size: small;
        }
        /*td.delete {
	background: url('assets/img/details_delete.png') no-repeat center center;
	width:10%;
	cursor: pointer; 
	font-size:small;
}*/tr.shown td.details-control
        {
            background: url('assets/img/details_close.png') no-repeat center center;
        }
        .info
        {
            border: 1px solid;
            margin: 10px 0px;
            padding: 10px 5px 10px 30px;
            background-repeat: no-repeat;
            background-position: 10px center;
            max-width: 380px;
            color: #9F6000;
            background-color: #FEEFB3;
        }
        .style1
        {
            height: 26px;
        }
    </style>

    <script type="text/javascript">

        var PI = {

            onReady: function() {
                $('#btnInscripcionA').click(fnGeneraInscripcion);
                $('#btnInscripcionC').click(fnCancelarInscripcion);
                $('#btnCartaA').click(fnGenerarC);
                $('#btnCartaC').click(fnCancelarC);
                $('#calendar').on(function(event) { event.preventDefault(); });
                $("#mdForoReg").css({ 'top': _H });
                //fnVerificarDeuda();


                $("#flArchivo").change(function() {
                    var maxSize = 500000;
                    var val = $(this).val();
                    var file = $("#flArchivo")[0].files[0];
                    var fileName = file.name;
                    var fileExtension = fileName.substring(fileName.lastIndexOf('.') + 1);
                    if (!isImage(fileExtension)) {
                        fnMensaje('error', 'Solo se permiten subir im&aacute;genes');
                        $(this).val('');
                        $(this).focus();
                        return false;
                    }
                    var fileSize = ($(flArchivo)[0].files[0].size);
                    if (fileSize > maxSize) {
                        fnMensaje('error', 'Solo se permiten archivos con tamaño maximo de 500Kb. Ingrese otro archivo');
                        $(this).val('');
                        $(this).focus();
                    }

                });
                $('.panel-minimize').on('click', function(e) {
                    e.preventDefault();
                    var $target = $(this).parent().parent().parent().next('.panel-body');
                    if ($target.is(':visible')) {
                        $('i', $(this)).removeClass('ti-angle-up').addClass('ti-angle-down');
                    } else {
                        $('i', $(this)).removeClass('ti-angle-down').addClass('ti-angle-up');
                    }
                    $target.slideToggle();
                });

            }
        }

        $(document).ready(PI.onReady);
        function isImage(extension) {
            switch (extension.toLowerCase()) {
                case 'jpg': case 'gif': case 'png': case 'jpeg': case 'xlsx': case 'docx': case 'pdf':
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
        function fnCalendar() {
            var date = new Date();
            var d = date.getDate();
            var m = date.getMonth();
            var y = date.getFullYear();
            $('#calendario').empty();
            var calendar = $('#calendar').fullCalendar({
                height: 900,
                contentHeight: 400,
                aspectRatio: 64,
                theme: true,
                /*header: {
                left: '',
                center: '',
                right: ''
                },*/
                header: false,
                defaultView: 'agendaWeek',
                defaultEventMinutes: 60,
                //selectable: true, //Deshabilita la seleccion en los dias

                selectable: false,
                selectHelper: true,
                disableDragging: true,
                firstDay: 1,
                monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
                monthNameShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
                dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sabado'],
                dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sab'],
                editable: false, //Deshabilita que se modifique el evento
                allDaySlot: false,
                allDayText: 'Todo el dia',
                axisFormat: 'HH:mm',
                timeFormat: 'HH:mm',
                eventDrop: function(event, delta) {
                    return false;
                    //alert(event.title + ' was moved ' + delta + ' days\n' +'(should probably update your database)');
                }, eventRender: function(event, element) {
                    element.attr('title', event.tip);
                }

            });


        }

        function CalendarAgregaEvento(cup, titulo, inicio, fin, allday, cr) {
            fnCalendar();
            var bg = '#337ab7';
            var filabg = '#2196f3';
            //console.log(cup + ' - ' + titulo + ' - ' + inicio + ' - ' + fin + ' - ' + allday + ' - ' + cr);
            if (cr) {
                bg = '#f00';
                $("#btnRegistrar").removeClass("btn btn-primary");
                $("#btnRegistrar").addClass("btn btn-primary disabled");
                //fnMensaje('error', 'se ha generado cruce de horarios con el curso: ' + titulo)
            } else {
                $("#btnRegistrar").removeClass("btn btn-primary disabled");
                $("#btnRegistrar").addClass("btn btn-primary");

            };

            $('#calendar').fullCalendar('addEventSource', [{ id: cup, title: titulo, start: inicio, end: fin, allDay: allday, color: bg, tip: titulo}]);
            $('#calendar').fullCalendar('refetchEvents');
            $('#calendar').fullCalendar('rerenderEvents');
        }
        function CalendarEliminarEvento() {
            // console.log('eliminar');
            $('#calendar').fullCalendar('removeEvents', function(event) {
                return true;
            });
        }
        function fnGeneraInscripcion() {
            $('.piluku-preloader').removeClass('hidden');
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processventa.aspx",
                data: { "param0": "gInscVera" },
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    $('.piluku-preloader').addClass('hidden');

                    if (data.sw) {
                        // f_Menu('matricula.aspx');
                        fnCargoConfirmado();

                    } else {
                        fnMensaje(data.alert, data.msje);
                    }
                },
                error: function(result) {
                    console.log('erro');
                    console.log(result);
                    $('.piluku-preloader').addClass('hidden');
                }
            });
        }

        function fnGenerarC() {
            $('.piluku-preloader').removeClass('hidden');
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "procesar.aspx",
                data: { "param0": "gCC" },
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    $('.piluku-preloader').addClass('hidden');

                    if (data.r) {
                        f_Menu('matricula.aspx');
                    }
                    fnMensaje(data.alert, data.msj);
                },
                error: function(result) {
                    console.log('erro');
                    console.log(result);
                    $('.piluku-preloader').addClass('hidden');
                }
            });
        }

        function fnCancelarInscripcion() {
            $('#divInfo').html('');
        }
        function fnCancelarC() {
            $('#divCarta').html('');
        }
        function fnVerificarDeuda() {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processventa.aspx",
                data: { "param0": "dv" },
                dataType: "json",
                success: function(data) {
                    console.log(data);
                    if (data.r) {
                        fnMostrarDeuda();
                    }
                },
                error: function(result) {
                    sOut = '';
                }
            });
            $('#mdPageDeuda').modal('show');
        }
        function fnMostrarDeuda() {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "detalledeuda.aspx",
                data: { "param1": 5 },
                dataType: "json",
                success: function(data) {
                    //console.log(data);
                    var sOut = '';
                    jQuery.each(data, function(i, val) {
                        sOut += '<tr>';
                        sOut += '<td>' + val.FECHA + ' ' + '</td>';
                        sOut += '<td>' + val.SERVICIO + ' ' + '</td>';
                        sOut += '<td>' + val.estado_deu + ' ' + '</td>';
                        sOut += '<td align="right">' + val.CARGO + ' ' + '</td>';
                        sOut += '<td align="right">' + val.PAGOS + ' ' + '</td>';
                        sOut += '<td align="right">' + val.SALDO + ' ' + '</td>';
                        sOut += '<td align="right">' + val.MORA_DEU + ' ' + '</td>';
                        sOut += '<td align="right">' + val.TOTAL + ' ' + '</td>';
                        sOut += '<td>' + val.OBSERVACION + ' ' + '</td>';
                        sOut += '</tr>';
                    });
                    $("#tbodyPago").html(sOut);
                },
                error: function(result) {
                    sOut = '';
                }
            });
            $('#mdPageDeuda').modal('show');
        }

        function fnCargoConfirmado() {
            var t = '';
            t += "<div class='row'>";
            t += "<div class='col-md-12'>"
            t += "<div class='panel panel-piluku'>";

            t += "<div class='panel-heading'>";
            t += "<h3 class='panel-title'>Inscripci&oacute;n de Cursos de Verano</h3>";
            t += "</div>";

            t += "<div class='table-responsive'>";

            t += "<div class='panel-body'>";

            t += "<div class='row'>"

            t += "<div class='form-group'>";

            t += "<div class='col-sm-12'  style='text-align:justify'>";
            t += "<center>";
            t += "<img src='assets/images/AVISO_ACTIVACION_VERANO.png?a=1' style='width:50%;height:50%;' />";
            t += "</center>";
            t += "</div>";

            t += "</div>";

            t += "</div>";

            t += "</div>";

            t += "</div>";

            t += "</div>";
            t += "</div>";
            t += "</div>";
            $("#divInfo").hide();
            $('#divInfo').html(t);
            $("#divInfo").fadeIn(3000);
        }
    </script>

    <script src='assets/js/datatables/foroalumno.js'></script>

</head>
<body>
    <div id="divFrameMat" runat="server">
        <form id="Form1" runat="server" name="frm">
        <div id="divReglamento">
            <div class="row">
                <div class="col-md-12">
                    <!-- *** Responsive Tables *** -->
                    <!-- panel -->
                    <div class='panel panel-piluku'>
                        <div class='panel-heading'>
                            <h3 class='panel-title'>
                                Matrícula y Retiro de Estudiantes en Talleres de Formación Complementaria</h3>
                        </div>
                        <div class='table-responsive'>
                            <div class='panel-body'>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-12">
                                            <center>
                                                &nbsp;</center>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="usatTitulo">
                                                        Búsqueda del alumno:
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" id="Table1" runat="server" cellpadding="0" border="0">
                                                            <tr>
                                                                <td align="center">
                                                                    Código Universitario:
                                                                    <asp:TextBox ID="txtCodigoUni" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                        MaxLength="600"></asp:TextBox>
                                                                    <asp:Button ID="Button1" runat="server" Text="Buscar" />
                                                                    <br>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <center>
                                                                        <img src="img/ima_02-02.png" style="width: 690px; height: 220px; border: 1px;" />
                                                                    </center>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br/>
                                                        <center>
                                                            Talleres Disponibles en el Semestre Actual: 2021-0
                                                        </center>
                                                        <br>
                                                        <table style="width: 100%;" id="tbincluye" runat="server" cellpadding="0" border="1">
                                                            <tr>
                                                                <td style="background-color: red; color: white;" class="style1" align="center">
                                                                    Nombre del Taller
                                                                </td>
                                                                <td style="background-color: red; color: white;" class="style1" align="center">
                                                                    RSU
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Cré
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Grupo
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Estado
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Vacantes
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Motivo
                                                                    de Agregado</td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Acción
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller RSU: Lenguaje de Señas
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    Sí
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1
                                                                </td>
                                                                <td align="center" class="style1">
                                                                    A
                                                                </td>
                                                                <td align="center" style="color: red;" class="style1">
                                                                    Cerrado
                                                                </td>
                                                                <td class="style1" style="color: red;" align="center">
                                                                    &nbsp;0
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList6" runat="server" AutoPostBack="false" DataTextFormatString="ambos"
                                                                        Height="16px" Width="72px">
                                                                        <asp:ListItem Value="A">Otro motivo</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td class="style1" align="center" style="color: red;">
                                                                    Habilitar
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller RSU: Mimo Ambiental
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    Sí
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    A
                                                                </td>
                                                                <td align="center" style="color: red;">
                                                                    Cerrado
                                                                </td>
                                                                <td class="style1" align="center" style="color: red;">
                                                                    0
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" style="color: red;">
                                                                    Habilitar
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller de Guitarra Clásica
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    Sí
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    B
                                                                </td>
                                                                <td align="center" style="color: Blue;">
                                                                    Abierto
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    7
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: Blue;" align="center">
                                                                    Inscribir
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller de ajedréz
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    No
                                                                </td>
                                                                <td align="center">
                                                                    1
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    B
                                                                </td>
                                                                <td align="center" style="color: Blue;">
                                                                    Abierto
                                                                </td>
                                                                <td align="center">
                                                                    5
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: Blue;" align="center">
                                                                    Inscribir
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller de Dibujo y Pintura
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    No
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1&nbsp;
                                                                </td>
                                                                <td align="center">
                                                                    B
                                                                </td>
                                                                <td align="center" style="color: Blue;">
                                                                    &nbsp; Abierto</td>
                                                                <td align="center">
                                                                    4
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: Blue;" align="center">
                                                                    Inscribir
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller de Canto Coral
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    No
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    C
                                                                </td>
                                                                <td align="center" style="color: Blue;">
                                                                    &nbsp;Abierto
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    1
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList5" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: Blue;" align="center">
                                                                    Inscribir
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br>
                                                        <center>
                                                            Talleres Inscritos:
                                                        </center>
                                                        <br>
                                                        <table style="width: 100%;" id="tbnoincluye" runat="server" cellpadding="0" border="1">
                                                            <tr>
                                                                <td style="background-color: red; color: white;" class="style1" align="center">
                                                                    Nombre del Taller
                                                                </td>
                                                                <td style="background-color: red; color: white;" class="style1" align="center">
                                                                    RSU
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Cré
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Grupo
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Estado
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Acreditación
                                                                </td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Motivo
                                                                    de Retiro</td>
                                                                <td style="background-color: red; color: white;" align="center">
                                                                    Acción
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="style1" align="center">
                                                                    Taller de ajedréz
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    No
                                                                </td>
                                                                <td align="center">
                                                                    1
                                                                </td>
                                                                <td class="style1" align="center">
                                                                    B
                                                                </td>
                                                                <td align="center" style="color: Blue;">
                                                                    Matriculado
                                                                </td>
                                                                <td align="center">
                                                                    1 Taller Acreditado en 2020-1 (Ver)
                                                                </td>
                                                                <td align="center">
                                                                    <asp:DropDownList ID="DropDownList7" runat="server" AutoPostBack="false" DataTextFormatString="ambos">
                                                                        <asp:ListItem Value="A">ambos</asp:ListItem>
                                                                        <asp:ListItem Value="U">RSU</asp:ListItem>
                                                                        <asp:ListItem Value="NU">No RSU</asp:ListItem>
                                                                        <asp:ListItem Value="N" Selected="True">---</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="color: Blue;" align="center">
                                                                    Retirar
                                                                </td>
                                                            </tr>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="divMatricula">
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-3 col-sm-6 col-xs-12 nopad-left">
                        <div class="dashboard-stats">
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 nopad-left">
                        <div class="dashboard-stats">
                            <div class="left">
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3 col-sm-6 col-xs-12 nopad-right">
                        <div class="dashboard-stats">
                            <div class="center">
                                <center>
                                    <p style="margin-top: 10px;">
                                        <button class="btn btn-primary" id="btnGuardar" onclick="fnGuardar()">
                                            <i class="ion ion-checkmark"></i><span id="spGuardar" runat="server">Guardar Matrícula</span>
                                        </button>
                                    </p>
                                </center>
                            </div>
                            <%--<div class="right flatOrange">
						    <h6>Matr&iacute;cula</h6>
					    </div>--%>
                        </div>
                    </div>
                </div>
            </div>            
        </div>
        <!-- panel -->
        <!-- /divmatricula -->
        <input type="hidden" id="txtponderado" runat="server" value="" />
        <input type="hidden" id="txtnotaMinima" runat="server" value="" />
        <input type="hidden" id="txtcredMaxMat" runat="server" value="" />
        <input type="hidden" id="txttotalCredAprob" runat="server" value="" />
        <input type="hidden" id="txtprecioCredito" runat="server" value="" />
        <input type="hidden" id="txttal" runat="server" value="" />
        <input type="hidden" id="txtveces1" runat="server" value="" />
        <input type="hidden" id="txtvecesn" runat="server" value="" />
        <input type="hidden" id="txtmaxCred" runat="server" value="" />
        <input type="hidden" id="txtmontoCredPen" runat="server" value="" />
        <input type="hidden" id="txtcuotas" runat="server" value="" />
        <input type="hidden" id="txtmat" runat="server" value="" />
        <input type="hidden" id="txtcond" runat="server" value="" />
        <input type="hidden" id="txtcpf" runat="server" value="" />
    </div>
    <div class="row">
        <div class="col-md-12">
        </div>
    </div>
    <div class="modal fade" id="mdForoReg" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" runat="server">
    </div>
    <div class="modal fade modal-full-pad" id="mdPageDeuda" tabindex="-1" role="dialog"
        aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-full">
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439;">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                        <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                    </button>
                    <h4 class="modal-title" style="color: White">
                        Alerta de Estado de Cuenta - Deudas Vencidas</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <font style="color: Red;"><b>(*) El alumno tiene las siguientes deudas vencidas</b></font>
                                <table class="display dataTable cell-border" id="tbPagos">
                                    <thead>
                                        <tr role="row">
                                            <th>
                                                Fecha Venc.
                                            </th>
                                            <th>
                                                Concepto
                                            </th>
                                            <th>
                                                Estado
                                            </th>
                                            <th style="text-align: right;">
                                                Cargo (S/.)
                                            </th>
                                            <th style="text-align: right;">
                                                Pago (S/.)
                                            </th>
                                            <th style="text-align: right;">
                                                Saldo (S/.)
                                            </th>
                                            <th style="text-align: right;">
                                                Mora (S/.)
                                            </th>
                                            <th style="text-align: right;">
                                                SubTotal (S/.)
                                            </th>
                                            <th>
                                                Observaci&oacute;n
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody id="tbodyPago" runat="server">
                                    </tbody>
                                    <tfoot>
                                        <tr>
                                            <th colspan="9">
                                            </th>
                                        </tr>
                                    </tfoot>
                                </table>
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
