<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAlineacionCompetenciasAsignatura.aspx.vb"
    Inherits="GestionCurricular_frmAlineacionCompetenciasAsignatura" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Alinear Asignaturas con las Competencias</title>
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="../scripts/js/jquery-1.12.3.min.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../scripts/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
        jQuery(document).ready(function() {
            $('#btnListar').click(function() {
                return fc_validarCombos();
            });

            $('#btnAgregar').click(function() {
                var cboCom = document.getElementById('<%=cboCompetencias.ClientID%>');
                if (cboCom.selectedIndex < 1) {
                    alert('¡ Seleccione Competencia !');
                    cboCom.focus();
                    return false;
                }
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
            var cboCP = document.getElementById('<%=cboCarProf.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                //alert('¡ Seleccione Carrera Profesional !');
                ShowMessage('¡ Seleccione Carrera Profesional !', 'Warning');
                cboCP.focus();
                return false;
            }
            var cboPC = document.getElementById('<%=cboPlanCurr.ClientID%>');
            if (cboPC.selectedIndex < 1) {
                //alert('¡ Seleccione Plan Curricular !');
                ShowMessage('¡ Seleccione Plan Curricular !', 'Warning');
                cboPC.focus();
                return false;
            }
            var cboPE = document.getElementById('<%=cboPlanEst.ClientID%>');
            if (cboPE.selectedIndex < 1) {
                //alert('¡ Seleccione Plan Estudio !');
                ShowMessage('¡ Seleccione Plan Estudio !', 'Warning');
                cboPE.focus();
                return false;
            }
        }

        function txtBuscar_onKeyPress(obj, e) {
            var key;
            if (window.event)
                key = window.event.keyCode; //IE, Chrome
            else
                key = e.which; //firefox

            if (key == 13) {
                var btn = document.getElementById(obj);
                if (btn != null) {
                    //$('#btnBuscar').focus();
                    //$('#btnBuscar').click();
                    btn.click();
                    event.keyCode = 0
                }
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Competencias con Asignaturas -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Alinear Asignaturas con las Competencias</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarProf" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-3">
                                Plan Curricular:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboPlanCurr" runat="server" CssClass="form-control input-sm"
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
                                Plan de Estudios:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboPlanEst" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="col-md-6">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info" />
                    </div>--%>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <%--<div class="col-md-1">
                                        <asp:CheckBox ID="chkBuscar" runat="server" AutoPostBack="true" OnCheckedChanged="chkBuscar_CheckedChanged" />
                                    </div>
                                    <label class="col-md-2" runat="server" id="lblBuscar">
                                        Activar Búsqueda</label>--%>
                                    <div class="col-md-7">
                                        <asp:Panel ID="panBuscar" runat="server" DefaultButton="btnBuscar" Width="100%">
                                            <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                                        </asp:Panel>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info" OnClientClick="btnBuscar_Click">
                                            <span><i class="fa fa-search"></i></span>
                                        </asp:LinkButton>
                                    </div>
                                    <div class="col-md-1">
                                        <asp:CheckBox ID="chkVerCursos" runat="server" AutoPostBack="true" OnCheckedChanged="chkVerCursos_CheckedChanged" />
                                    </div>
                                    <label class="col-md-3" runat="server" id="lblBuscar" for="chkVerCursos">
                                        Ver Asignaturas Alineadas</label>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-11">
                                <asp:GridView ID="gvAsignatura" runat="server" Width="99%" AutoGenerateColumns="false"
                                    ShowHeader="true" DataKeyNames="codigo_pes, codigo_Cur" CssClass="table table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' AutoPostBack="true"
                                                    OnCheckedChanged="chkSelect_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" HeaderStyle-Width="50%" />
                                        <asp:BoundField DataField="creditos_Cur" HeaderText="Crédito" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron Asignaturas!
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="11px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-1">
                                <br />
                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-success">
                                            <span><i class="fa fa-angle-double-right"></i></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="btnQuitar" runat="server" CssClass="btn btn-danger">
                                            <span><i class="fa fa-angle-double-left"></i></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-md-1">
                        <br />
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnAgregar" runat="server" CssClass="btn btn-success">
                                    <span><i class="fa fa-angle-double-right"></i></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:LinkButton ID="btnQuitar" runat="server" CssClass="btn btn-danger">
                                    <span><i class="fa fa-angle-double-left"></i></span>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>--%>
                    <div class="col-md-6">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="col-md-4" runat="server" id="lblCompetencia">
                                        Competencias:</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="cboCompetencias" runat="server" CssClass="form-control input-sm"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="gvPerfilEgresoCurso" runat="server" Width="99%" AutoGenerateColumns="false"
                                    ShowHeader="true" DataKeyNames="codigo_pEgrCur, codigo_pes, codigo_Cur, codigo_dom, nombre_Cur"
                                    CssClass="table table-bordered table-hover">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                        <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" HeaderStyle-Width="50%" />
                                        <asp:BoundField DataField="creditos_Cur" HeaderText="Crédito" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Nivel" HeaderStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cboNivel" runat="server" CssClass="form-control input-sm" AutoPostBack="true"
                                                    OnSelectedIndexChanged="cboNivel_SelectedIndexChanged" SelectedValue='<%# Eval("codigo_dom") %>'>
                                                    <asp:ListItem Value="1">Básico</asp:ListItem>
                                                    <asp:ListItem Value="2">Intermedio</asp:ListItem>
                                                    <asp:ListItem Value="3">Avanzado</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Clave">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdClave" runat="server" GroupName="FakeGroup" AutoPostBack="True"
                                                    OnCheckedChanged="rdClave_CheckedChanged" Checked='<%# Eval("asigClave_pEgrCur") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron Asignaturas en esta Competencia!
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
        </div>
    </div>
    </form>
</body>
</html>
