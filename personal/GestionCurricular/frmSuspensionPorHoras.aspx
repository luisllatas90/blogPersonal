<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmSuspensionPorHoras.aspx.vb" 
Inherits="GestionCurricular_frmSuspensionPorHoras" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Suspensión por Horas</title>
    
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">
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

        jQuery(document).ready(function() {
            $('#btnGuardar').click(function() {
                var des = $('#txtDescripcion').val();
                if (des.trim() == '') {
                    alert('¡ Ingrese Descripción !');
                    $('#txtDescripcion').focus();
                    return false;
                }
                var fec = $('#fecRegistro').val();
                if (fec.trim() == '') {
                    alert('¡ Ingrese Fecha !');
                    $('#fecRegistro').focus();
                    return false;
                }
                if (!validarFormatoFecha(fec)) {
                    alert('¡ Ingrese Fecha [dd/MM/yyyy]!');
                    $('#fecRegistro').focus();
                    return false;
                }
                if (!existeFecha2(fec)) {
                    alert('¡ Ingrese Fecha [dd/MM/yyyy]!');
                    $('#fecRegistro').focus();
                    return false;
                }
                var ini = $('#txtHoraIni').val();
                if (ini.trim() == '') {
                    alert('¡ Ingrese Hora Inicio !');
                    $('#txtHoraIni').focus();
                    return false;
                }
                if (!validarHora(ini)){
                    alert('¡ Ingrese Hora Inicio - Formato 24h [HH:MM] !');
                    $('#txtHoraIni').focus();
                    return false;
                }
                var fin = $('#txtHoraFin').val();
                if (fin.trim() == '') {
                    alert('¡ Ingrese Hora Fin !');
                    $('#txtHoraFin').focus();
                    return false;
                }
                if (!validarHora(fin)) {
                    alert('¡ Ingrese Hora Fin - Formato 24h [HH:MM] !');
                    $('#txtHoraFin').focus();
                    return false;
                }
                if (!validarHora2(ini, fin)) {
                    alert('¡ La hora de Inicio no puede ser mayor a la Hora de Fin !');
                    $('#txtHoraIni').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            if (accion == 'Agregar') {
                $('#txtDescripcion').val(' ');
                $('#fecRegistro').val('');
                $('#txtHoraIni').val('');
                $('#txtHoraFin').val('');
            }
            $('#myModal').modal('show');
            $('#txtDescripcion').focus();
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function validarFormatoFecha(campo) {
            var RegExPattern = /^\d{1,2}\/\d{1,2}\/\d{2,4}$/;
            if ((campo.match(RegExPattern)) && (campo != '')) {
                return true;
            } else {
                return false;
            }
        }

        function existeFecha2(fecha) {
            var fechaf = fecha.split("/");
            var d = fechaf[0];
            var m = fechaf[1];
            var y = fechaf[2];
            return m > 0 && m < 13 && y > 0 && y < 32768 && d > 0 && d <= (new Date(y, m, 0)).getDate();
        }

        function validarHora(hora) {
            var xhora = hora.split(":");
            console.log(xhora.length);
            if (!(xhora.length == 2)) {
                return false;
            } else {
                var hor = xhora[0];
                var min = xhora[1];
                if (isNaN(hor) || hor.trim() == '') {
                    return false;
                }
                if (hor > 24) {
                    return false;
                }
                if (isNaN(min) || min.trim() == '') {
                    return false;
                }
                if (min > 59) {
                    return false;
                }
            }
            return true;
        }

        function validarHora2(horaini,horafin) {
            var xhora = horaini.split(":");
            var yhora = horafin.split(":");
            var xhor = xhora[0];
            var xmin = xhora[1];
            var yhor = yhora[0];
            var ymin = yhora[1];
            if (xhor==yhor) {
                if (xmin > ymin) {
                    return false;
                }
            } else {
                if (xhor > yhor) {
                    return false;
                }
            }
            return true;
        }
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>Suspensión Por Horas</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="col-md-1">Año:</label>
                            <div class="col-md-3">
                                <asp:DropDownList ID="cboAnio" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnAgregar" runat="server" Text='<i class="fa fa-plus"></i> Agregar'
                                    CssClass="btn btn-sm btn-success" OnClick="btnAgregar_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvSPH" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo_sph, descripcion_sph, fecha_sph, horaInicio_sph, horaFin_sph"
                    CssClass="table table-sm table-bordered table-hover" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="codigo_sph" HeaderText="ID"/>
                            <asp:BoundField DataField="descripcion_sph" HeaderText="Descripcion"/>
                            <asp:BoundField DataField="fecha_sph" HeaderText="Fecha" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="horaInicio_sph" HeaderText="Hora Inicio"/>
                            <asp:BoundField DataField="horaFin_sph" HeaderText="Hora Fin"/>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea Editar la Suspensión?');">
                                        <span><i class="fa fa-pen"></i></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminar" runat="server" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('¿Desea Eliminar la Suspensión?');">
                                        <span><i class="fa fa-trash"></i></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate> No se encontró ningun registro </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                        <EditRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registrar Suspensión por Horas</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Descripción:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Fecha:</label>
                                <div class="col-md-6">
                                    <asp:TextBox ID="fecRegistro" runat="server" CssClass="form-control input-sm" data-provide="datepicker" placeholder="__/__/____"></asp:TextBox>
                                    <%--<span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Hora Inicio:</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtHoraIni" runat="server" CssClass="form-control input-sm" MaxLength="5" placeholder="__:__"></asp:TextBox>
                                </div>
                                <label class="col-md-6">Formato 24h. [HH:MM]</label>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Hora Fin:</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtHoraFin" runat="server" CssClass="form-control input-sm" MaxLength="5" placeholder="__:__"></asp:TextBox>
                                </div>
                                <label class="col-md-6">Formato 24h. [HH:MM]</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:LinkButton ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
