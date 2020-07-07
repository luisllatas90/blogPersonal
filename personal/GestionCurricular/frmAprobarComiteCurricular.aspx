<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAprobarComiteCurricular.aspx.vb"
    Inherits="GestionCurricular_frmAprobarComiteCurricular" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Aprobar Comité Curricular</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap-datepicker3.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src='../assets/js/jquery-ui-1.10.3.custom.min.js'></script>

    <script type="text/javascript" src="../assets/js/bootstrap-datepicker.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#txtFechaAprobacion').on('change', function(ev) {
                $(this).datepicker('hide');
            });
        
            $('#btnGuardar').click(function() {
                var fecha = $('#txtFechaAprobacion').val();
                if (fecha == '') {
                    alert("¡ Ingrese Fecha de Aprobación !");
                    $('#txtFechaAprobacion').focus();
                    return false;
                }
                var nrodecreto = $('#txtNroDecreto').val();
                if (nrodecreto == '') {
                    alert("¡ Ingrese Nro de Resolución !");
                    $('#txtNroDecreto').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
            $('#txtFechaAprobacion').val('');
            $('#txtNroDecreto').val('');
        }

        function closeModal() {
            $('#hdcodigo_com').val('');
            $('#myModal').modal('hide');
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

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Comité -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Aprobar Comité Curricular</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-8">
                        <div class="form-group">
                            <label class="col-md-3">
                                Carrera Profesional:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboCarProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="col-md-2">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info"/>
                        <asp:LinkButton ID="btnListar2" runat="server" CssClass="btn btn-info">
                            <span aria-hidden="true"><i class="fa fa-search"></i> Listar</span>
                        </asp:LinkButton>
                    </div>--%>
                    <%--<div class="col-md-2">
                        <asp:Button ID="btnAdjuntar" runat="server" Text="Adjuntar Resolución" CssClass="btn btn-success"/>
                    </div>--%>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvComiteCurricular" runat="server" Width="99%" AutoGenerateColumns="false"
                            ShowHeader="true" DataKeyNames="codigo_com, habilitar, vigente_com" CssClass="table table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="codigo_com" HeaderText="Código" Visible="false" />
                                <asp:BoundField DataField="nombre_com" HeaderText="Comité Curricular" />
                                <asp:BoundField DataField="nrodecreto_com" HeaderText="Nro Resolución" />
                                <asp:BoundField DataField="fechaAprob_com" HeaderText="Fecha Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:TemplateField HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnHabilitar" runat="server" CommandName="Habilitar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-primary btn-sm" Visible='<%# IIF(Eval("vigente_com"),IIF(Eval("habilitar"),"false","true"),"false") %>'
                                            OnClientClick="return confirm('¿Desea habilitar este comité?');">
                                            <span><i class="fa fa-lock-open"></i></span> Habilitar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnDeshabilitar" runat="server" CommandName="Deshabilitar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-info btn-sm" Visible='<%# IIF(Eval("vigente_com"),IIF(Eval("habilitar"),"true","false"),"false") %>'
                                            OnClientClick="return confirm('¿Desea deshabilitar este comité?');">
                                            <span><i class="fa fa-lock"></i></span> Deshabilitar
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="vigencia" HeaderText="Vigente" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAlta" runat="server" CommandName="Alta" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-success btn-sm" Visible='<%# IIF(Eval("vigente_com"),"false","true") %>'
                                            OnClientClick="return confirm('¿Desea dar de alta a este comité?');">
                                            <span><i class="fa fa-thumbs-up"></i></span> Dar de Alta
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnBaja" runat="server" CommandName="Baja" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                            CssClass="btn btn-danger btn-sm" Visible='<%# IIF(Eval("vigente_com"),"true","false") %>'
                                            OnClientClick="return confirm('¿Desea dar de baja a este comité?');">
                                            <span><i class="fa fa-thumbs-down"></i></span> Dar de baja
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron comités
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                Font-Size="12px" />
                            <RowStyle Font-Size="11px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Aprobacion Comité -->
    <div id="myModal" class="modal fade" role="dialog" role="dialog" aria-labelledby="myModalLabel"
        style="z-index: 0;" aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Aprobar Comité Curricular</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form_group">
                                <label class="col-md-3">
                                    Fecha Aprobación:</label>
                                <div class="col-md-3">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtFechaAprobacion" runat="server" CssClass="form-control  input-sm"
                                            data-provide="datepicker"></asp:TextBox>
                                        <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                    </div>
                                </div>
                                <label class="col-md-2">
                                    Nro Resolución:</label>
                                <div class="col-md-3">
                                    <asp:TextBox ID="txtNroDecreto" runat="server" CssClass="form-control  input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">
                                    Resolución:</label>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control input-sm" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnGuardar" runat="server" Text="Grabar" CssClass="btn btn-info" />
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdcodigo_com" runat="server" />
    </form>
</body>
</html>
