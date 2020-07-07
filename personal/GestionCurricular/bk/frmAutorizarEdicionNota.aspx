<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAutorizarEdicionNota.aspx.vb"
    Inherits="GestionCurricular_frmAutorizarEdicionNota" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Autorizar Modificación de Calificaciones</title>
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
        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var cboCor = document.getElementById('<%=cboCorte.ClientID%>');
                if (cboCor.selectedIndex < 1) {
                    alert('¡ Seleccione Corte Académico !');
                    cboCor.focus();
                    return false;
                }
                var cboEva = document.getElementById('<%=cboEvaluacion.ClientID%>');
                if (cboEva.selectedIndex < 1) {
                    alert('¡ Seleccione Evaluación !');
                    cboEva.focus();
                    return false;
                }
                var nombre = $('#txtObservacion').val();
                if (nombre.trim() == '') {
                    alert("¡ Ingrese Observacion !");
                    $('#txtObservacion').focus();
                    return false;
                }
                var total = 0;
                $("#<%=gvEstudiantes.ClientID%> tr input[id$='chkSelect']:checkbox").each(function(index) {
                    if ($(this).is(':checked'))
                        total++;
                });
                if (total == 0) {
                    alert("¡ Seleccione al menos un estudiante !");
                    return false;
                }
                //                var txt = document.getElementById('<%=txtObservacion.ClientID%>').getAttribute("tag");
                //                var nro = parseInt(txt);
                //                //console.log(txt);
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
        }

        function closeModal() {
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Autorizar Modificación de Calificaciones</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-5">
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
                    <div class="col-md-7">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class="col-md-10">
                                <asp:Panel ID="panBuscar" runat="server" DefaultButton="btnBuscar" Width="100%">
                                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info">
                                    <span><i class="fa fa-search"></i></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPaging="True" PageSize="20" DataKeyNames="codigo_cup, codigo_cur, codigo_pes, nombre_Cur, docente_cur, email_per, descripcion_cur"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="identificador_Cur" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Autorizar">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSolicitar" runat="server" CommandName="Solicitar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                                CssClass="btn btn-primary btn-sm" OnClientClick="return confirm('¿Desea generar una autorización de modificación de calificaciones?');">
                                            <span><i class="fa fa-file-signature"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron Datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" Font-Bold="true" />
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
    <!-- Modal Registro de Autorizacion  -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Autorización de Modificación de Calificaciones</h4>
                </div>
                <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-3">
                                            Corte:
                                        </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="cboCorte" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                                OnSelectedIndexChanged="cboCorte_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-3">
                                            Evaluación:
                                        </label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="cboEvaluacion" runat="server" CssClass="form-control input-sm"
                                                AutoPostBack="true" OnSelectedIndexChanged="cboEvaluacion_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="col-md-3">
                                            Observación:
                                        </label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control input-sm"
                                                TextMode="MultiLine" Rows="5"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:HiddenField ID="hdnro_est" runat="server" Visible="false" />
                                    <asp:GridView ID="gvEstudiantes" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                        DataKeyNames="codigo_Dma, nombre_Alu, codigo_nop, codigo_eva, codigo_cup, codigo_ins" CssClass="table table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' AutoPostBack="true"
                                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Código" />
                                            <asp:BoundField DataField="nombre_alu" HeaderText="Alumno" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron Datos
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="12px" Font-Bold="true" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                        <FooterStyle Font-Bold="True" ForeColor="White" />
                                        <%--<PagerStyle ForeColor="#003399" HorizontalAlign="Center" />--%>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
