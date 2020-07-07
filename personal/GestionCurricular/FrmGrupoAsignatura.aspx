<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmGrupoAsignatura.aspx.vb"
    Inherits="GestionCurricular_FrmGrupoAsignatura" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Gestión de grupos por asignatura (solo medicina)</title>
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

    <script src="js/bootbox.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function mostrarMensaje(yesNo, mensaje, tipo) {
            var backcolor = "#64C45C";
            var forecolor = "#FFFFFF";

            if (tipo == "danger") {
                backcolor = "#FC7E7E";
                forecolor = "#FFFFFF";
            } else if (tipo == "warning") {
                backcolor = "#FFDC96";
                forecolor = "#000000";
            }

            var box;
            if (yesNo !== "") {
                if (yesNo == "no") {
                    box = bootbox.alert({ message: mensaje
                    , onEscape: false
                    , backdrop: true
                    , closeButton: false
                    , buttons: {
                        ok: {
                            label: "Aceptar",
                            className: "btn-success"
                        }
                    }
                    });
                } else {
                    box = bootbox.confirm({ message: mensaje
                    , onEscape: false
                    , backdrop: true
                    , closeButton: false
                    , buttons: {
                        cancel: {
                            label: "Cancelar",
                            className: "btn-danger"
                        }, confirm: {
                            label: "Continuar",
                            className: "btn-success"
                        }
                    }
                    , callback: function(result) {
                        if (result) {
                            var auth = "yes";
                            __doPostBack("returnOpt", auth);
                        }
                    }
                    });
                };

                box.find('.modal-body').css({ 'background-color': backcolor });
                box.find('.bootbox-body').css({ 'color': forecolor });
            };
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
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Gestión de grupos por asignatura (solo medicina)</h4>
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
            <br />
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-7">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPaging="True" PageSize="20" DataKeyNames="codigo_cup, codigo_Cpf, codigo_Cur, grupoHor_Cup, ciclo_Cur, nombre_Cur, totalGrupo"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="ciclo_Cur" HeaderText="Ciclo" ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        FooterStyle-Width="5%" ReadOnly="true" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" ItemStyle-Width="60%"
                                        HeaderStyle-Width="60%" FooterStyle-Width="60%" ReadOnly="true" />
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" ItemStyle-Width="5%"
                                        HeaderStyle-Width="5%" FooterStyle-Width="5%" ReadOnly="true" />
                                    <asp:BoundField DataField="vacantes_Cup" HeaderText="Vacantes" ItemStyle-Width="10%"
                                        HeaderStyle-Width="10%" FooterStyle-Width="10%" ReadOnly="true" />
                                    <asp:BoundField DataField="nroMatriculados_Cup" HeaderText="Matriculados" ItemStyle-Width="10%"
                                        HeaderStyle-Width="10%" FooterStyle-Width="10%" ReadOnly="true" />
                                    <asp:BoundField DataField="totalGrupo" HeaderText="Subgrupos" ItemStyle-Width="5%"
                                        HeaderStyle-Width="5%" FooterStyle-Width="5%" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Config." HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        FooterStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" Text='<i class="fa fa-cogs"></i>'
                                                ToolTip="Configurar Curso" CssClass="btn btn-warning btn-sm" CausesValidation="False"
                                                OnClick="onEditarGrupo"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" Font-Bold="true" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="«" LastPageText="»" PageButtonCount="10"
                                    Position="Bottom" Visible="true" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="table-responsive" id="tabDetalle" runat="server">
                            <div class="row">
                                <div class="col-md-12" style="background-color: yellow; margin-bottom: 10px;">
                                    <asp:Label ID="lblCurso" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12" style="margin-bottom: 10px;">
                                    <div class="form-group">
                                        <div class="col-md-8" style="padding: 0px;">
                                            <asp:Label ID="txtGrupo" runat="server" CssClass="form-control input-sm"></asp:Label>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:LinkButton ID="LinkButton2" runat="server" Text='<i class="fa fa-plus"></i>'
                                                ToolTip="Agregar nuevo grupo" CssClass="btn btn-info btn-sm" CausesValidation="False"
                                                OnClick="onAgregarGrupo"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvDetalle" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                        DataKeyNames="codigo_sgr, codigo_cup, descripcion_sgr" CssClass="table table-bordered table-hover">
                                        <Columns>
                                            <asp:BoundField DataField="descripcion_sgr" HeaderText="Sub grupo" ItemStyle-Width="7%"
                                                HeaderStyle-Width="7%" FooterStyle-Width="7%" ReadOnly="true" />
                                            <asp:BoundField DataField="alumnos" HeaderText="Total alumnos" ItemStyle-Width="8%"
                                                HeaderStyle-Width="8%" FooterStyle-Width="8%" ReadOnly="true" />
                                            <asp:BoundField DataField="programado" HeaderText="Tot. Fechas asignadas" ItemStyle-Width="10%"
                                                HeaderStyle-Width="10%" FooterStyle-Width="10%" ReadOnly="true" />
                                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-Width="7%" ItemStyle-Width="7%"
                                                FooterStyle-Width="7%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" Text='<i class="fa fa-pen"></i>'
                                                        ToolTip="Agregar alumnos" CssClass="btn btn-success btn-sm" CausesValidation="False"
                                                        OnClick="onAgregarAlumno" OnClientClick="return confirm('¿Desea editar alumnos?');"
                                                        Visible='<%# IIF(Eval("total_corte") = 0, "true", "false") %>'></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton4" runat="server" Text='<i class="fa fa-trash"></i>'
                                                        ToolTip="Eliminar grupo" CssClass="btn btn-danger btn-sm" CausesValidation="False"
                                                        OnClick="onEliminarGrupo" Visible='<%# IIF(Eval("total_corte") = 0, "true", "false") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron datos
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="12px" Font-Bold="true" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal asignar alumnos -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Editar alumnos del grupo</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <asp:UpdatePanel ID="udpAlumno" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                <ContentTemplate>
                                    <asp:GridView ID="gvAlumno" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                        DataKeyNames="codigo_cup, codigo_mat, codigo_dma, habilitar" CssClass="table table-bordered table-hover">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Selec" HeaderStyle-Width="8%" ItemStyle-Width="8%"
                                                FooterStyle-Width="8%">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelec" runat="server" CssClass="form-control-check" Enabled='<%#Eval("habilitar") %>'
                                                        Checked='<%#Eval("checked") %>' OnCheckedChanged="OnCheck" AutoPostBack="true" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="codigo_uni" HeaderText="Cód. Univ." ItemStyle-Width="20%"
                                                HeaderStyle-Width="20%" FooterStyle-Width="20%" ReadOnly="true" />
                                            <asp:BoundField DataField="estudiante" HeaderText="Alumno" ItemStyle-Width="62%"
                                                HeaderStyle-Width="62%" FooterStyle-Width="62%" ReadOnly="true" />
                                            <asp:BoundField DataField="descripcion_sgr" HeaderText="Grupo" ItemStyle-Width="10%"
                                                HeaderStyle-Width="10%" FooterStyle-Width="10%" ReadOnly="true" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            No se encontraron datos
                                        </EmptyDataTemplate>
                                        <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="12px" Font-Bold="true" />
                                        <RowStyle Font-Size="11px" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <div style="float: left">
                        <asp:UpdatePanel ID="updSeleccion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <label style="font-size: smaller; color: Red" id="lblAlumnos" runat="server">
                                    Usted ha seleccionado 0 alumno(s)
                                </label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
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
