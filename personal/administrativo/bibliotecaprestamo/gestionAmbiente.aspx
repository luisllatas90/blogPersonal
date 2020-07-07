<%@ Page Language="VB" AutoEventWireup="false" CodeFile="gestionAmbiente.aspx.vb" Inherits="gestionAmbiente" %>

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
            fnListAmbiente();
        });
        
        function clearForm() {
            $('#txtNombre').val('');
            $('#txtIdAmbiente').val(0);
            $('#cboEstado').val(1);
            $('.div_class_estado').css('display', 'none');
        }
        
        function fnGuardar(){
            var idAmbiente = $('#txtIdAmbiente').val();
            var parametro;
            if (idAmbiente === 0 || idAmbiente === '0') {
                parametro = 'addAmbiente';
            } else {
                parametro = 'updateAmbiente';
            }
            var form = {
                'nombreAmbiente' : $('#txtNombre').val(),
                'param0' : parametro,
                'ambiente_id': idAmbiente,
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
                    fnListAmbiente();
                    clearForm();
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        function fnListAmbiente(){
            var form = {
                "param0" : 'listAmbiente'
            };
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=utf-8",
                url: "gestionProcess.aspx",
                data: form,
                dataType: "json",
                async: false,
                success: function(data) {
                    $('#tbodyAmbiente').empty();
                    for(var i = 0; i < data.length; i++) {
                        var estado;
                        if (data[i].estado === 1) {
                             estado = 'ACTIVO';
                        } else {
                            estado = 'INACTIVO';
                        }
                        $('#tbodyAmbiente').append('<tr>'+
                            '<td>'+data[i].nombre_ambiente+'</td>'+
                            '<td>'+estado+'</td>'+
                            '<td>'+
                                '<button class="btn btn-info btn-sm" onclick="editarAmbiente(\''+data[i].nombre_ambiente+'\',\'' + data[i].ambiente_id + '\',\'' + data[i].estado + '\')">Editar</button>'+
                            '</td></tr>');
                    }
                },
                error: function(result) {
                    console.log(result);
                }
            });
        }
        function editarAmbiente(nombre, ambiente_id, estado){
            $('.div_class_estado').css('display','block');
            $('#txtIdAmbiente').val(ambiente_id);
            $('#txtNombre').val(nombre);
            $('#cboEstado').val(estado);
        }
    </script>
</head>
<body>
    <br />
    <div class="row">
        <div class="col-md-12">
            <div class="row">
                <div class="col-md-12">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Registrar Ambiente
                        </div>
                        <div class="panel-body">
                            <form id="Form1" runat="server">
                                <input type="hidden" value="0" id="txtIdAmbiente"/>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label">
                                            <font>Nombre del Ambiente:</font>
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
                            <th colspan="3"> Listado de Ambientes </th>
                            </tr>
                            <tr>
                                <th>Ambiente</th>
                                <th>Estado</th>
                                <th>Acción</th>
                            </tr>
                        </thead>
                        <tbody id="tbodyAmbiente">
                        
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
