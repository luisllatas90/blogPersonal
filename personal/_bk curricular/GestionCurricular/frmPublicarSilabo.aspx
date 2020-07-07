<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmPublicarSilabo.aspx.vb"
    Inherits="GestionCurricular_frmPublicarSilabo" EnableEventValidation="false" %>

<%--<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Publicar Silabo</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            $('#btnGrabar').click(function() {
                var nombre = $('#txtObservacion').val();
                if (nombre == '') {
                    alert("¡ Ingrese Observación !");
                    $('#txtObservacion').focus();
                    return false;
                }
            });

            $('#btnListar').click(function() {
                return fc_validarCombos();
            });
        });

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

        function fc_validarCombos() {
            var cboCA = document.getElementById('<%=cboSemestre.ClientID%>');
            if (cboCA.selectedIndex < 1) {
                alert('¡ Seleccione Semestre !');
                cboCA.focus();
                return false;
            }
            var cboTE = document.getElementById('<%=cboTipoEstudio.ClientID%>');
            if (cboTE.selectedIndex < 1) {
                alert('¡ Seleccione Tipo Estudio !');
                cboTE.focus();
                return false;
            }
            var cboCP = document.getElementById('<%=cboCarPro.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                alert('¡ Seleccione Carrera Profesional !');
                cboCP.focus();
                return false;
            }
            var cboPE = document.getElementById('<%=cboPlanEstudio.ClientID%>');
            if (cboPE.selectedIndex < 1) {
                alert('¡ Seleccione Plan Estudio !');
                cboPE.focus();
                return false;
            }
        }

        function fc_ocultarBusy() {
            //$('#BusyBox1Overlay').removeAttr('style');
            $('#BusyBox1').removeAttr('style');
        }

        function openModal(accion) {
            $('#txtObservacion').val('');
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <%--<busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />--%>
    <!-- Listado de Silabos -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Publicar Silabos</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Semestre:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Tipo Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboTipoEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarPro" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Plan Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboPlanEstudio" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" Width="99%" AutoGenerateColumns="false"
                                ShowHeader="true" AllowPaging="True" PageSize="10" OnRowDataBound="gvAsignatura_RowDataBound"
                                DataKeyNames="codigo_cup, nombre_Cur, grupoHor_Cup, descripcion_Cac, IdArchivo, codigo_Cur, codigo_Pes, codigo_Cac, nro_fecha, nro_sesion, IdArchivo_Anexo"
                                CssClass="table table-bordered  table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="identificador_Cur" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                    <asp:BoundField DataField="creditos_Cur" HeaderText="Creditos" />
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Diseño Aprobado">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDis" runat="server" Checked='<%# Eval("dis_aprobado") %>' Enabled="false" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="sin_fecha" HeaderText="Fecha / Sesión" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnVer" runat="server" CommandName="Ver" ToolTip="Visualizar Sílabo"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-info btn-sm"
                                                Visible='<%# IIf(Eval("dis_aprobado")=0,False,IIf(Eval("IdArchivo")=0,True,False)) %>'
                                                OnClientClick="return confirm('¿Desea visualizar el silabo?');">
                                            <span><i class="fa fa-eye"></i></span>
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnPublicar" runat="server" CommandName="Publicar" ToolTip="Publicar Sílabo"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-success btn-sm"
                                                Visible='<%# IIf(Eval("dis_aprobado")=1 and Eval("sin_fecha")=0 and Eval("IdArchivo")=0,True,False) %>'
                                                OnClientClick="return confirm('¿Desea publicar el silabo?');">
                                            <span><i class="fa fa-share-alt"></i></span>
                                            </asp:LinkButton>
                                            <%--<asp:LinkButton ID="btnObservar" runat="server" CommandName="Observar" ToolTip="Observar Sílabo"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                                            Visible='<%# IIf(Eval("dis_aprobado")=1 and Eval("sin_fecha")=0,True,False) %>'  
                                            OnClientClick="return confirm('¿Desea generar una observación el silabo?');" >
                                            <span><i class="fa fa-unlink"></i></span>
                                        </asp:LinkButton>--%>
                                            <asp:LinkButton ID="btnDescargar" runat="server" CommandName="Descargar" OnClick="btnDescargar_Click"
                                                ToolTip="Descargar Sílabo" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-warning btn-sm" Visible='<%# IIf(Eval("dis_aprobado")=1 and Eval("sin_fecha")=0 and Eval("IdArchivo")<>0,True,False) %>'
                                                OnClientClick="return confirm('¿Desea Descargar el silabo?');">
                                            <span><i class="fa fa-download"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Registro de Observacion -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registrar Observación</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm"
                                    TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                            Cancelar</button>
                        <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-success" />
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdcodigo_cup" runat="server" />
    </form>
</body>
</html>
