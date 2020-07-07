<%@ Page Language="VB" AutoEventWireup="false" CodeFile="gestionSuspension.aspx.vb" Inherits="gestionSuspension" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link rel='stylesheet' href='assets/css/bootstrap.min.css' />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script type="text/javascript" src='assets/js/jquery-1.10.2.min.js'></script>
    <script type="text/javascript" src='assets/js/bootstrap.min.js'></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
      <script type="text/javascript" src="../../assets/js/jquery.dataTables.min.js?x=1"></script>	
    <link rel='stylesheet' href='../../assets/css/jquery.dataTables.min.css?z=1'/>
    <script type="text/javascript" src='assets/js/bootstrap.min.js'></script>
    <script type="text/javascript" src="../../assets/js/funcionesDataTable.js"></script>

    <script>
        $(document).ready(function() {
            $('#fechaIni').datepicker({ dateFormat: 'yy-mm-dd' });
            $('#fechaFin').datepicker({ dateFormat: 'yy-mm-dd' });
            $("#btnConfirmar").click(fnGuardar);
            $("#BtnConfirmarHabilitar").click(fnGuardarHabilitar);

            //            var oTable = $('#gData').DataTable({
            //                //"sPaginationType": "full_numbers",
            //                "bPaginate": false,
            //                "bFilter": true,
            //                "bLengthChange": false,
            //                "bSort": false,
            //                "bInfo": true,
            //                "bAutoWidth": true
            //            });
            fnCreateDataTable('gData');
        });
        
        function fnGuardar() {
            var form = {
                "param0" : $('#cboTipoUsuario').val() === '1' ? 'registerEstudianteSuspension' : 'registerColaboradorSuspension',
                "codigo" : $('#codigoID').val(),
                "usuario" : $('#usuarioID').val(),
                "fechaInicio" : $('#fechaIni').val(),
                "fechaFin" : $('#fechaFin').val(),
                "flag_equipo" : 1
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {
                    $('#mdRegistro').modal('hide');
                    alert("Registro correcto");
                    searchPorTipo();
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        
        function fnGuardarHabilitar() {
            var form = {
                "param0" : 'habilitarSuspension',
                "usuario" : $('#usuarioID').val(),
                "flag_equipo" : 0
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {
                    $('#mdHabilitar').modal('hide');
                    alert("Registro correcto");
                    searchPorTipo();
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        
        function searchPorTipo(){
            var form = {
                "param0" : 'listTipo',
                "tipo" : $('#cboTipoUsuario').val()
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {                    
                    $('#bodyTableConsulta').empty();
                    fnDestroyDataTable('gData');
                    for (var i = 0; i < data.length; i++) {
                        var estado;
                        var button;
                        var fechaInicio;
                        var fechaFin;
                        if (data[i].suspension_id !== null) {
                            estado = 'Suspendido';
                            button = '<button class="btn btn-success btn-sm" onclick="actionHabilitar(' + data[i].codigo + ',' + data[i].usuario_id + ')">Habilitar</button>';
                            fechaInicio = data[i].fecha_inicio;
                            fechaFin = data[i].fecha_fin;
                        } else {
                            estado = 'Habilitado';
                            button = '<button class="btn btn-danger btn-sm" onclick="actionSuspender(' + data[i].codigo + ',' + data[i].usuario_id + ')">Suspender</button>'
                            fechaInicio = '-';
                            fechaFin = '-';
                        }
                        $('#bodyTableConsulta').append('<tr>' +
                            '<td>' + data[i].nombre + '</td>' +
                            '<td>' + estado + '</td>' +
                            '<td>' + fechaInicio + '</td>' +
                            '<td>' + fechaFin + '</td>' +
                            '<td>' + button + '</td>' +
                        '</tr>');
                    }
                    fnResetDataTable('gData');
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        
        function actionSuspender(codigo, usuario) {
            $('#mdRegistro').modal('show');
            $('#codigoID').val(codigo);
            $('#usuarioID').val(usuario);
        }
        
        function actionHabilitar(codigo, usuario) {
            $('#mdHabilitar').modal('show');
            $('#codigoID').val(codigo);
            $('#usuarioID').val(usuario);
        }
    </script>
</head>
<body>
    <br />
    <div class="col-lg-12">
        <div class="row">
            <div class="col-md-2">
                <label>Tipo de Usuario:</label>
            </div>
            <div class="col-md-4">
                <select class="form-control" name="cboTipoUsuario" id="cboTipoUsuario">
                    <option value="0">Elegir Tipo de Usuario</option>
                    <option value="1">Estudiante</option>
                    <option value="2">Colaborador</option>
                </select>
            </div>
            <div class="col-md-4">
                <button class="btn btn-primary" onclick="searchPorTipo();">Buscar</button>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-lg-12">
                <table class="table table-bordered" id="gData" class="display">
                    <thead>
                        <tr>
                            <th colspan="3"> Listado de Usuarios </th>
                        </tr>
                        <tr>
                            <th>Nombre</th>
                            <th>Estado</th>
                            <th>Fecha Inicio Suspensión</th>
                            <th>Fecha Fin Suspensión</th>
                            <th>Acción</th>
                        </tr>
                    </thead>
                    <tbody id="bodyTableConsulta">
                    </tbody>
                </table>
            </div>
        </div>
        <div class="modal fade" id="mdRegistro" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" style="color: White">
                           Suspensión de Reservas
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <input type="hidden" id="codigoID" name="codigoID" />
                            <input type="hidden" id="usuarioID" name="usuarioID"/>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <p>¿Está seguro de suspender?</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <label>Fecha Inicio:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control" id="fechaIni" name="fechaIni"/>
                                    </div>
                                    <div class="col-md-2">
                                        <label>Fecha Fin:</label>
                                    </div>
                                    <div class="col-md-4">
                                        <input type="text" class="form-control" id="fechaFin" name="fechaFin"/>
                                    </div>
                                </div>
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
        <div class="modal fade" id="mdHabilitar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439;">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: White;">
                            <span aria-hidden="true" class="ti-close" style="color: White;"></span>
                        </button>
                        <h4 class="modal-title" style="color: White">
                           Suspensión de Reservas
                        </h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <input type="hidden" id="Hidden1" name="codigoID" />
                            <input type="hidden" id="Hidden2" name="usuarioID"/>
                            <div class="col-md-12">
                                <div class="row">
                                    <div class="col-md-12">
                                        <p>¿Está seguro de habilitar?</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <div class="btn-group">
                                <button type="button" class="btn btn-primary" id="BtnConfirmarHabilitar">
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
    </div>
</body>
</html>
