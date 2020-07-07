<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmCoordinador.aspx.vb"
    Inherits="GestionCurricular_FrmCoordinador" EnableEventValidation="false" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <title>Coordinador de Área</title>
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/regular.min.css" rel="stylesheet" type="text/css" />
    <link href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css" rel="stylesheet"
        type="text/css" />
    <link href="css/paginacion.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#btnAceptar').click(function() {
                var cboDo = document.getElementById('<%=ddlDocente.ClientID%>');
                if (cboDo.selectedIndex < 0) {
                    alert("¡ Seleccione al Docente !");
                    cboDo.focus();
                    return false;
                }
                var doc = $('#ddlDocente').val()
                if (doc == "-1") {
                    alert("¡ Seleccione al Docente !");
                    cboDo.focus();
                    return false;
                }
            });
        });

        function openModal(acc, docente, asignatura) {
            $('#myModal').modal('show');
            if (acc == "agregar") {
                $('#hdCodigoCoordinador').val('');
                //$('#ddlDocente').val('');
            } else {
                $('#ddlDocente').val(docente);
            }
            $('#txtAsignatura').val(asignatura);
        }

        function closeModal() {
            $('#hdCodigoCoordinador').val('');
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

    <style>
        .bootstrap-select > .dropdown-toggle
        {
            width: 100%;
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock"
        Text="Su solicitud está siendo procesada..." Title="Por favor espere" />
    <!-- Listado Coordinador -->
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Registrar Coordinador de Asignatura</h4>
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
                                Carrera Profesional:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboCarrProf" runat="server" CssClass="form-control input-sm"
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
                                Área:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboTipoCur" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="1"> Estudios generales (Transversales) </asp:ListItem>
                                    <asp:ListItem Value="2"> Estudios generales (No Transversales) </asp:ListItem>
                                    <asp:ListItem Value="3"> Estudios específicos </asp:ListItem>
                                    <asp:ListItem Value="4"> Estudios de especialidad </asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">
                                Estado:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="-1">TODOS</asp:ListItem>
                                    <asp:ListItem Value="0">SIN COORDINADOR</asp:ListItem>
                                    <asp:ListItem Value="1">CON COORDINADOR</asp:ListItem>
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
                    <div class="col-md-6">
                        <div class="form-group">
                            <div class"col-md-12">
                                <Label ID="lblAviso" runat="server" style="color:Red; background-color: Yellow; font-size: 12px;"></Label>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvCoordinador" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                            AllowPaging="True" PageSize="20" DataKeyNames="codigo_coo, codigo_cur, codigo_per, codigo_pes, nombre_Cur, indicador_coo"
                            OnRowDataBound="gvCoordinador_RowDataBound" CssClass="table table-bordered table-hover">
                            <Columns>
                                <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" />
                                <asp:BoundField DataField="identificador_Cur" HeaderText="Código" />
                                <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" />
                                <asp:BoundField DataField="creditos_Cur" HeaderText="Créditos" />
                                <asp:BoundField DataField="Coordinador" HeaderText="Coordinador" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Acción">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAgregar" runat="server" CommandName='<%# IIF(Eval("codigo_coo")=-1,"Agregar","Editar") %>'
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass='<%# IIF(Eval("codigo_coo")=-1,"btn btn-success btn-sm","btn btn-primary btn-sm") %>'
                                            OnClientClick="return confirm('¿Desea Agregar o Editar el Coordinador de Asignatura?');">
                                            <span><i class='<%# IIF(Eval("codigo_coo")=-1,"fa fa-plus","fa fa-pen") %>'></i></span>
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron Datos
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
    <!-- Modal Registro de Coordinador -->
    <div id="myModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel"
        aria-hidden="true" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog" role="document">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title">
                        Registro de Coordinador</h4>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="panModal" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-2" for="ddlArea">
                                            Asignatura:</label>
                                        <div class="col-xs-10">
                                            <asp:TextBox ID="txtAsignatura" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <label class="col-xs-2" for="ddlDocente">
                                            Docente:</label>
                                        <div class="col-xs-10">
                                            <asp:DropDownList ID="ddlDocente" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="col-xs-12">
                                    <div class="form-group">
                                        <div class="col-xs-12">
                                            <asp:CheckBox ID="chkDocente" runat="server" Text=" Listar Docentes Adscritos al Departamento Académico"
                                                AutoPostBack="true" OnCheckedChanged="chkDocente_CheckedChanged" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnCancelar" class="btn btn-danger" data-dismiss="modal">
                        Cancelar</button>
                    <asp:Button ID="btnAceptar" runat="server" Text="Grabar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdCodigoCoordinador" runat="server" />
    </form>
</body>
</html>
