<%@ Page Language="VB" AutoEventWireup="false" CodeFile="gestionEquipo.aspx.vb" Inherits="gestionEquipo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <link rel='stylesheet' href='assets/css/bootstrap.min.css' />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script type="text/javascript" src='assets/js/jquery-1.10.2.min.js'></script>
    <script type="text/javascript" src='assets/js/bootstrap.min.js'></script>
    <script>
        $(document).ready(function(){
            $('#cboEstado').val(1);
            $('.div_class_estado').css('display', 'none');
            $('#btnGuardar').click(fnGuardar);
            fnListEquipo(); 
        });
        
        function fnListEquipo(){
            var form = {
                "param0" : 'listEquipo'
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {
                    $('#tbodyEquipo').empty();
                    for(var i = 0; i < data.length; i++) {
                        var estado;
                        if (data[i].estado_equipo === 1) {
                            estado = 'ACTIVO';
                        } else if (data[i].estado_equipo === 0) {
                            estado = 'INACTIVO';
                        } else {
                            estado = 'MANTENIMIENTO';
                        }
                        $('#tbodyEquipo').append('<tr>'+
                            '<td>'+data[i].nombre_equipo+'</td>'+
                            '<td>'+data[i].nombre_ambiente+'</td>'+
                            '<td>'+estado+'</td>'+
                            '<td>'+
                                '<button class="btn btn-info btn-sm" onclick="editarEquipo(\''+data[i].nombre_equipo+'\',\'' + data[i].ambiente_id + '\',\'' + data[i].estado_equipo + '\',\'' + data[i].equipo_id + '\')">Editar</button>'+
                            '</td></tr>');
                    }
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        
        function clearForm() {
            $('#txtNombre').val('');
            $('#txtIdEquipo').val(0);
            $('#cboEstado').val(1);
            $('#DropDownList1').val(1);
            $('.div_class_estado').css('display', 'none');
        }
        
        function fnGuardar(){
            var idEquipo = $('#txtIdEquipo').val();
            var parametro;
            if (idEquipo === 0 || idEquipo === '0') {
                parametro = 'addEquipo';
            } else {
                parametro = 'updateEquipo';
            }
            var form = {
                'nombreEquipo' : $('#txtNombre').val(),
                'param0' : parametro,
                'ambiente_id': $('#DropDownList1').val(),
                'equipo_id' : $('#txtIdEquipo').val(),
                'estado' : $('#cboEstado').val()
            };
            
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {
                    fnListEquipo();
                    clearForm();
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        
        function editarEquipo(nombre, ambiente_id, estado, equipo_id) {
            $('.div_class_estado').css('display','block');
            $('#DropDownList1').val(ambiente_id);
            $('#txtNombre').val(nombre);
            $('#txtIdEquipo').val(equipo_id);
            $('#cboEstado').val(estado);
        }
    </script>
</head>
<body>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-8">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Registrar Equipo
                        </div>
                        <div class="panel-body">
                            <form id="Form1" runat="server">
                                <input type="hidden" value="0" id="txtIdEquipo"/>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
                                            <font>Ambiente:</font>
                                        </label>
                                        <div class="col-md-8">
                                            <asp:DropDownList ID="DropDownList1" runat="server" class="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
                                            <font>Nombre del Equipo:</font>
                                        </label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group div_class_estado">
                                        <label class="col-md-4 control-label">
                                            <font>Estado:</font>
                                        </label>
                                        <div class="col-md-8">
                                            <select name="cboEstado" id="cboEstado" class="form-control">
                                                <option value="0">Inactivo</option>
                                                <option value="1">Activo</option>
                                                <option value="2">Mantenimiento</option>
                                            </select>
                                        </div>
                                    </div>
                                </div>
                            </form>
                            <div class="col-lg-12">
                                <button type="button" class="btn btn-primary" id="btnGuardar">Guardar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th colspan="3"> Listado de Equipos</th>
                            </tr>
                            <tr>
                                <th>Equipo</th>
                                <th>Ambiente</th>
                                <th>Estado</th>
                                <th>Acción</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyEquipo">
                        
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
