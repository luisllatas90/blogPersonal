<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script type="text/javascript" src='assets/js/jquery-1.10.2.min.js'></script>
    <link rel='stylesheet' href='assets/css/bootstrap.min.css' />
    <script type="text/javascript" src='assets/js/bootstrap.min.js'></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.1.0/css/all.css" integrity="sha384-lKuwvrZot6UHsBSfcMvOkWwlCMgc0TaWr+30HWe3a4ltaBwTZhyTEggF5tJv8tbt" crossorigin="anonymous">
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
  
    <script>
        $(document).ready(function() {
            $('#btnConfirmarAnular').click(fnAnular);
            $("#alertSpan").css("display", "none");
            $("#btnConfirmar").click(fnGuardar);
            $('#txtFechaBsq').datepicker({ dateFormat: 'yy-mm-dd' });
            $('#textFchaColaborador').datepicker({ dateFormat: 'yy-mm-dd' });
            
        });
        function validateConsultaReserva(value) {
            if (value !== '') {
                $('#btnListarReservaColaborador').prop('disabled',false);
            } else {
                $('#btnListarReservaColaborador').prop('disabled',true);
            }
        }
        function fnLstReservaColaborador(){
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processreserva.aspx",
                data: { "param0": "conslColaborador", "fecha": $('#textFchaColaborador').val(), "colaborador_id": 1 },
                dataType: "json",
                async: false,
                success: function(data) {
                    $('#tblReserva').empty();
                    $('#DropDownList1').val('0');
                    $('#txtFechaBsq').val('');
                    $('#tblConsultaColaborador').empty();
                    $('#btnBuscarReserva').prop('disabled', true);
                    $('#tblConsultaColaborador').append('<thead>' +
                    '<tr>' +
                        '<th>Fecha</th>' +
                        '<th>Equipo</th>' +
                        '<th>Ambiente</th>' +
                        '<th>Hora Inicio</th>' +
                        '<th>Hora Fin</th>' +
                        '<th>Estado</th>' +
                        '<th>Acción</th>' +
                    '</tr>' +
                '</thead>');
                    $('#tblConsultaColaborador').append('<tbody id="bodyConsultaColaborador"></tbody>');
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].estado === 1) {
                            estado = 'Pendiente';
                            icono = '<i class="fa fa-trash-alt" style="cursor: pointer;" onclick="anularReserva(' + data[i].reserva_id + ')"></i>'
                        } else if (data[i].estado === 2) {
                            estado = 'Finalizado';
                            icono = '';
                        } else if (data[i].estado === 3) {
                            estado = 'Anulado';
                            icono = '';
                        } else if (data[i].estado === 4) {
                            estado = 'Prestado';
                            icono = '';
                        }
                        
                        $('#bodyConsultaColaborador').append('<tr>' +
                        '<td>' + $('#textFchaColaborador').val() + '</td>' +
                        '<td>' + data[i].nombre_equipo + '</td>' +
                        '<td>' + data[i].nombre_ambiente + '</td>' +
                        '<td>' + data[i].hora_inicio + '</td>' +
                        '<td>' + data[i].hora_fin + '</td>' +
                        '<td>' + estado + '</td>' +
                        '<td>' + icono + '</td>' +
                    '</tr>');
                    }
                },
                error: function(result) {

                }
            });
        }
        function fnLstReserva() {
            var param = {
                'param0' : 'lstReserva',
                'fecha' : $('#txtFechaBsq').val(),
                'ambiente' : $('#DropDownList1').val()
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processreserva.aspx",
                data: param,
                dataType: "json",
                async: false,
                success: function(data) {
                    console.log(data[0].codigo);
                    if (data[0].codigo === '0') {
                        $("#alertSpan").css("display", "block");
                        $("#spanId").html("USTED SE ENCUENTRA SUSPENDIDO(A). No puede realizar ninguna reserva de equipos. Ante cualquier duda por favor comunicarse con el área del biblioteca al correo: biblioservicios@usat.edu.pe");
                    } else {
                        $("#alertSpan").css("display", "none");
                    }
                    $('#txtbsqFechaAlumno').val('');
                    $('#btnListarReservaAlumno').prop('disabled', true);
                    $('#tblReserva').empty();
                    $('#tblConsultaAlumno').empty();
                    var arrayObjetos = [];
                    for (var i = 6; i < 21; i++) {
                        var objeto = {};
                        var index = 0;
                        for (var obj in data[1]) {
                            index++;
                            if (i === 6) {
                                objeto[0] = 'Hora / Equipos';
                                objeto[index] = data[1][obj].nombre_equipo
                            } else {
                                objeto[0] = i + ':00'
                                objeto[index] = {
                                    estado: 0,
                                    equipo: data[1][obj].equipo_id,
                                    hora: i
                                };
                            }
                        }
                        arrayObjetos.push(objeto);
                    }
                    for (var i = 1; i < arrayObjetos.length; i++) {
                        for (var j = 0; j < data[0].length; j++) {
                            if (arrayObjetos[i][0] === data[0][j].hora_inicio) {
                                for (var objx in arrayObjetos[i]) {
                                    if (objx !== '0') {
                                        if (arrayObjetos[i][objx].equipo === data[0][j].equipo_id) {
                                            arrayObjetos[i][objx].estado = data[0][j].estado;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    $('#tblReserva').append('<tbody id="tblBodyReserva"></tbody>');
                    for (var i = 0; i < arrayObjetos.length; i++) {
                        $('#tblBodyReserva').append('<tr id="trReserva' + i + '"></tr>');
                        for (var obj in arrayObjetos[i]) {
                            if (i === 0) {
                                $('#trReserva' + i).append('<th>' + arrayObjetos[i][obj] + '</th>');
                            } else {
                                if (obj === '0') {
                                    $('#trReserva' + i).append('<th>' + arrayObjetos[i][obj] + '</th>');
                                } else {
                                    $('#trReserva' + i).append('<th><i id="icon' + i + obj + '" class="fa fa-desktop" style="cursor: pointer" onclick="addReserva(' + arrayObjetos[i][obj].estado + ',' + arrayObjetos[i][obj].equipo + ',' + arrayObjetos[i][obj].hora + ')"></i></th>');
                                    if (arrayObjetos[i][obj].estado == 1 || arrayObjetos[i][obj].estado == 2 || arrayObjetos[i][obj].estado == 4) {
                                        $('#icon' + i + obj).css('cursor', 'default');
                                        $('#icon' + i + obj).css('color', 'red');
                                    } else {
                                        $('#icon' + i + obj).css('cursor', 'pointer');
                                        $('#icon' + i + obj).css('color', 'green');
                                    }
                                }
                            }
                        }
                    }
                },
                error: function(result) {

                }
            });
        }
        function addReserva(estado, equipo_id, hora) {
            if (estado === 1) {
                return;
            }
            $('#mdRegistro').modal('show');
            $('#labelHora').html(hora);
            $('#inputHora').val(hora);
            $('#inputEquipo').val(equipo_id);
        }
        function fnGuardar() {
            var horafin = parseInt($('#inputHora').val()) + 1
            var form = {
                'idAlumno' : null,
                'idColaborador' : 1,
                'estado' : 1,
                'fecha_reserva' : $('#txtFechaBsq').val(),
                'hora_inicio' : $('#inputHora').val()+':00',
                'hora_fin' : horafin + ':00',
                'equipo_id' : $('#inputEquipo').val(),
                'param0' : 'addReserva'
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processreserva.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {                    
                    if (data[0].r == '2') {
                        $('#mdRegistro').modal('hide');
                        $('#pMensaje').html(data[0].msje);
                        $('#mdMensaje').modal('show');
                    } else {
                        $('#mdRegistro').modal('hide');
                    }
                    fnLstReserva();
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        function anularReserva(reserva_id) {
            $('#mdAnular').modal('show');
            $('#inputReserva').val(reserva_id);
        }
        function fnAnular() {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "processreserva.aspx",
                data: { "param0": "anularReserva", "reserva_id": $('#inputReserva').val(), "estado": 3 },
                dataType: "json",
                async: false,
                success: function(data) {
                    fnLstReservaColaborador();
                    $('#mdAnular').modal('hide');
                },
                error: function(result) {

                }
            });
        }
        function validateCboSearchReserva(value) {
            if (value !== '0' && $('#txtbsqFecha').val() !== '') {
                $('#btnBuscarReserva').prop('disabled', false);
            } else {
                $('#btnBuscarReserva').prop('disabled', true);
            }
        }
        
        function validateTextSearchReserva(value) {
            if ($('#DropDownList1').val() !== '0' && value !== '') {
                $('#btnBuscarReserva').prop('disabled', false);
            } else {
                $('#btnBuscarReserva').prop('disabled', true);
            }
        }
    </script>
</head>
<body>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        Registrar mi Reserva de Equipo
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <form id="Form1" runat="server">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                <font>Ambiente:</font>
                                            </label>
                                            <div class="col-md-8">
                                                <asp:DropDownList ID="DropDownList1" runat="server" class="form-control" onchange="validateCboSearchReserva(this.value);">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">
                                                <font>Fecha:</font>
                                            </label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtFechaBsq" runat="server" class="form-control" onchange="validateTextSearchReserva(this.value);"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </form>
                                <div class="col-md-1">
                                    <button class="btn btn-primary" onclick="fnLstReserva();" id="btnBuscarReserva" disabled="true">Buscar</button>
                                </div>
                                <div class="col-md-3">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <label>Leyenda</label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <i class="fa fa-desktop" style="color: green;"></i>
                                        </div>
                                        <div class="col-md-8">
                                            <span>Equipo Disponible</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <i class="fa fa-desktop" style="color: red;"></i>
                                        </div>
                                        <div class="col-md-8">
                                            <span>Equipo Reservado</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-1">
                                            <i class="fa fa-ban" style="color: red;"></i>
                                        </div>
                                        <div class="col-md-8">
                                            <span>Máximo 2 reservas por día</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row" id="alertSpan">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="alert alert-danger" role="alert">
                                      <strong>Atención!</strong> <span id="spanId"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">                                        
                                        <table class="table table-bordered" id="tblReserva">
                                        
                                        </table>
                                        (*) Las reservas no utilizadas se anularán 10 minutos después de la hora de inicio
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="col-md-12">
                <div class="panel panel-danger">
                    <div class="panel-heading">
                        Consultar mis Reservas
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label class="col-md-4 control-label">
                                        <font>Fecha:</font>
                                    </label>
                                    <div class="col-md-8">
                                        <input id="textFchaColaborador" runat="server" type="text" class="form-control" onchange="validateConsultaReserva(this.value);">
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <button class="btn btn-primary" onclick="fnLstReservaColaborador();" id="btnListarReservaColaborador" disabled="true">Buscar</button>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="alert alert-warning" role="alert">
                                      <strong>Atención!</strong> <span>Si usted no puede llegar en la fecha y hora indicada, deberá anular su reserva y así evitará ser sancionado.</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <table class="table table-bordered" id="tblConsultaColaborador">
                                    
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <br />
    <br />
    <div class="modal fade" id="mdRegistro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true">
        <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color: #E33439;">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                </button>
                <h4 class="modal-title" style="color: White">
                    Registro de Reserva
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <input type="hidden" id="inputHora" name="inputHora" />
                    <input type="hidden" id="inputEquipo" name="inputEquipo" />
                    <div class="col-md-12">
                        <p>¿Está seguro de reservar para las <label id="labelHora"></label> horas?</p>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-primary" id="btnConfirmar">
                            <i class="ion-android-done"></i>&nbsp;SI
                        </button>
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="ion-android-cancel"></i>&nbsp;NO
                        </button>
                    </div>
                </center>
            </div>
        </div>
    </div>
</div>
    <div class="modal fade" id="mdMensaje" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
    aria-hidden="true">
        <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="background-color: #E33439;">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                    <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                </button>
                <h4 class="modal-title" style="color: White">
                    Registro de Reserva
                </h4>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-md-12">
                        <p id="pMensaje"></p>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <center>
                    <div class="btn-group">
                        <button type="button" class="btn btn-danger" data-dismiss="modal">
                            <i class="ion-android-cancel"></i>&nbsp;Aceptar
                        </button>
                    </div>
                </center>
            </div>
        </div>
    </div>
</div>
    <div class="modal fade" id="mdAnular" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
            aria-hidden="true">
                <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" style="color: White">
                            Anular Reserva
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <input type="hidden" id="inputReserva" name="inputReserva"/>
                            <div class="col-md-12">
                                <p>¿Está seguro de anular la reserva?</p>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary" id="btnConfirmarAnular">
                                    <i class="ion-android-done"></i>&nbsp;SI
                                </button>
                                <button type="button" class="btn btn-danger" data-dismiss="modal">
                                    <i class="ion-android-cancel"></i>&nbsp;NO
                                </button>
                            </div>
                        </center>
                    </div>
                </div>
            </div>
    </div>
</body>
</html>
