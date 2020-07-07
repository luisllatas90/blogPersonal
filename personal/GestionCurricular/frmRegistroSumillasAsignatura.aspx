<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmRegistroSumillasAsignatura.aspx.vb"
    Inherits="GestionCurricular_frmRegistroSumillasAsignatura" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Registrar Sumillas por Asignatura</title>
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
            $('#btnListar').click(function() {
                return fc_validarCombos();
            });
            $('#btnGrabar').click(function() {
                var nombre = $('#txtSumilla').val();
                if (nombre.trim() == '') {
                    alert("¡ Ingrese Sumilla !");
                    $('#txtSumilla').focus();
                    return false;
                }
                //                var comp = $('#txtCompetencia').val();
                //                if (comp.trim() == '') {
                //                    alert("¡ Ingrese Competencia !");
                //                    $('#txtCompetencia').focus();
                //                    return false;
                //                }
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
            var nombre = $('#txtSumilla').val();
            if (nombre.trim() == '') {
                $('#txtSumilla').val(' ');
            }
            document.getElementById('txtSumilla').select();
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado de Sumillas por Asignatura -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registrar Sumillas por Asignatura</h4>
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
                                Estado:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="-1" Selected="True"> TODOS </asp:ListItem>
                                    <asp:ListItem Value="0"> SIN SUMILLA </asp:ListItem>
                                    <asp:ListItem Value="1"> CON SUMILLA </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row" runat="server" id="divPlanEst">
                    <div class="col-md-6">
                        <div class="form-group" runat="server" id="divPlanCurr">
                            <label class="col-md-4">
                                Plan Curricular:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboPlanCurr" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                    </div>
                    <div class="col-md-6">
                        <div class="form-group" >
                            <label class="col-md-3">
                                Plan de Estudios:</label>
                            <div class="col-md-9">
                                <asp:DropDownList ID="cboPlanEst" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <%--<div class="col-md-6">
                        <asp:Button ID="btnListar" runat="server" Text="Listar" CssClass="btn btn-info" />
                    </div>--%>
                </div>
            </div>
            <div class="panel panel-body">
                <%--<asp:UpdatePanel ID="panGrid" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <contenttemplate>--%>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <%--<div class="col-md-1">
                                    <asp:CheckBox ID="chkBuscar" runat="server" AutoPostBack="true" OnCheckedChanged="chkBuscar_CheckedChanged" />
                                </div>
                                <label class="col-md-3" runat="server" id="lblBuscar">
                                    Activar Búsqueda:</label>--%>
                                <div class="col-md-8">
                                    <asp:Panel ID="panBuscar" runat="server" DefaultButton="btnBuscar" Width="100%">
                                        <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm" placeholder="Ingrese Nombre de la Asignatura"></asp:TextBox>
                                    </asp:Panel>
                                </div>
                                <div class="col-md-2">
                                    <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-info btn-sm">
                                        <span><i class="fa fa-search"></i></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnExportar" runat="server" CssClass="btn btn-success btn-sm" 
                                            Text='<i class="fa fa-file-excel"></i> Exportar' />
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <asp:GridView ID="gvAsignatura" runat="server" Width="99%" AutoGenerateColumns="false"
                                ShowHeader="true" AllowPaging="True" PageSize="10" 
                                DataKeyNames="codigo_sum, codigo_pes, codigo_Cur, descripcion_sum,competencia_sum,transversal,cod_aux"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                    <%--<asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" />--%>
                                    <asp:BoundField DataField="descripcion_sum" HeaderText="Sumilla" HeaderStyle-Width="35%" />
                                    <asp:BoundField DataField="competencia_sum" HeaderText="Competencia" HeaderStyle-Width="35%" />
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción" HeaderStyle-Width="4%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditar" runat="server" OnClick="btnEditar_Click" CommandName="Editar"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                Enabled='<%# IIF(Eval("transversal"),IIF(Eval("cod_aux")<>-2,"False","True"),"True") %>'
                                                CssClass='<%# IIF(Eval("codigo_sum")=-1,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                                OnClientClick="return confirm('¿Desea Agregar o Editar sumilla a este curso?');" >
                                                <span><i class='<%# IIF(Eval("codigo_sum")=-1,"fa fa-plus","fa fa-pen") %>'></i></span>
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
                <%--</contenttemplate>
                </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
    <!-- Modal Registro de Sumilla -->
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog  modal-lg">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registrar Sumillas por Asignatura</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-2">
                                    Sumilla:
                                </label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtSumilla" runat="server" CssClass="form-control input-sm" TextMode="MultiLine"
                                        Rows="8"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-2">
                                    Competencia por Asignatura:
                                </label>
                                <div class="col-md-10">
                                    <asp:TextBox ID="txtCompetencia" runat="server" CssClass="form-control input-sm"
                                        TextMode="MultiLine" Rows="8"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvAsignaturaEsp" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" DataKeyNames="codigo_pes, codigo_Cur, codigo_sum" CssClass="table table-bordered  table-hover">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nombre" HeaderText="Plan de Estudio" />
                                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                                    <asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron Datos!
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="11px" />
                                                <RowStyle Font-Size="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvAsignaturaGen" runat="server" Width="99%" AutoGenerateColumns="false"
                                                ShowHeader="true" AllowPaging="True" PageSize="9" DataKeyNames="codigo_pes, codigo_Cur, codigo_sum"
                                                CssClass="table table-bordered table-hover">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selec">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%# Eval("selec") %>' AutoPostBack="true"
                                                                OnCheckedChanged="chkSelect_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="nombre" HeaderText="Plan de Estudio" />
                                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                                    <asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" />
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    No se encontraron Datos!
                                                </EmptyDataTemplate>
                                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                                    Font-Size="11px" />
                                                <RowStyle Font-Size="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdselec" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
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
