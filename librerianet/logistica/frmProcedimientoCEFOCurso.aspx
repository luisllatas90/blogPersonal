<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProcedimientoCEFOCurso.aspx.vb" 
    Inherits="logistica_frmProcedimientoCEFOCurso" EnableEventValidation="false" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Configuracion Curso - Procedimiento Odontologico</title>
    
    <script src="../assets/js/jquery.js" type="text/javascript"></script>
    
    <script src="../assets/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script src="../assets/js/jquery-ui-1.10.3.custom.min.js" type="text/javascript"></script>
    
    <link href="../assets/css/bootstrap.min.css" rel="Stylesheet" type="text/css"/>
    
    <script type="text/javascript">

        var lstProcedimiento;
        var cod_pro;
        var nom_pro;

        jQuery(document).ready(function() {

            $('#btnGrabar').click(function() {
                if ($('#txtNombre').val() == '' || $('#hdCodPro').val() == '') {
                    alert('Ingrese Nombre de Procedimiento Odontologico');
                    $('#txtNombre').focus();
                    return false;
                }
                if ($('#txtCant').val() == '') {
                    alert('Ingrese Cantidad');
                    $('#txtCant').focus();
                    return false;
                }
            });

            $('#btnBuscar').click(function() {
                return fc_validarCombos();
            });

            $('#btnAgregar').click(function() {
                return fc_validarCombos();
            });

            lstProcedimiento = fc_cargarLista("lstProcedimiento");
            var jsonString = JSON.parse(lstProcedimiento);

            $('#txtNombre').autocomplete({
                source: $.map(jsonString, function(item) {
                    return item.nom;
                }),
                select: function(event, ui) {
                    var selectedItem = jsonString.filter(function(value) {
                        return value.nom == ui.item.value;
                    });
                    cod_pro = selectedItem[0].cod;
                    nom_pro = selectedItem[0].nom;
                    $('#hdCodPro').val(cod_pro);
                },
                minLength: 3,
                delay: 100
            });

            $('#txtNombre').keyup(function() {
                var len = parseInt($(this).val().length);
                if (len == 0) {
                    cod_pro = "";
                    nom_pro = "";
                }
            });

        });

        function fc_cargarLista(param) {
            try {
                var ddl = document.getElementById('<%=cboTipoEstudio.ClientID%>');
                //console.log(ddl.options[ddl.selectedIndex].value);
                var param1 = ddl.options[ddl.selectedIndex].value;
                var arr;
                $.ajax({
                    type: "GET",
                    contentType: "application/json; charset=utf-8",
                    url: "../DataJson/Logistica/processProcedimientoCEFO.aspx",
                    data: { "param": param, "param1": param1 },
                    async: false,
                    cache: false,
                    success: function(data) {
                        arr = data;
                    },
                    error: function(result) {
                        arr = null;
                    }
                })
                //console.log(arr);
                return arr;
            }
            catch (err) {
                console.log('error');
            }
        }

        function openModal(accion) {
            $('#myModal').modal('show');
            $('#txtNombre').val('');
            $('#txtCant').val('1');
        }

        function closeModal() {
            $('#hdCodPro').val('');
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
            var cboTE = document.getElementById('<%=cboTipoEstudio.ClientID%>');
            if (cboTE.selectedIndex < 1) {
                alert('Seleccione Tipo Estudio');
                cboTE.focus();
                return false;
            }
            var cboCP = document.getElementById('<%=cboCarPro.ClientID%>');
            if (cboCP.selectedIndex < 1) {
                alert('Seleccione Carrera Profesional');
                cboCP.focus();
                return false;
            }
            var cboPE = document.getElementById('<%=cboPlaEst.ClientID%>');
            if (cboPE.selectedIndex < 1) {
                alert('Seleccione Plan de Estudio');
                cboPE.focus();
                return false;
            }
            var cboCU = document.getElementById('<%=cboCurso.ClientID%>');
            if (cboCU.selectedIndex < 1) {
                alert('Seleccione Curso');
                cboCU.focus();
                return false;
            }
        }
        
    </script>
    
    <style type="text/css">
    
        .ui-widget-content {
            border: 1px solid #dddddd;
            background: #eeeeee url(images/ui-bg_highlight-soft_100_eeeeee_1x100.png) 50% top repeat-x;
            color: #333333;
        }
        .ui-widget {
            font-family: Trebuchet MS,Tahoma,Verdana,Arial,sans-serif;
            font-size: 1.1em;
        }
        .ui-menu {
            list-style: none;
            padding: 2px;
            margin: 0;
            display: block;
            outline: none;
        }
        .ui-autocomplete {
            position: absolute;
            top: 0;
            left: 0;
            cursor: default;
        }
        .ui-front {
            z-index: 100;
        }
        .ui-menu .ui-menu-item {
            margin: 0;
            padding: 0;
            width: 100%;
            list-style-image: url(data:image/gif;base64,R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7);
        }
        .ui-menu .ui-menu-item a {
            text-decoration: none;
            display: block;
            padding: 2px .4em;
            line-height: 1.5;
            min-height: 0;
            font-weight: normal;
        }
        .ui-widget-content a {
            color: #333333;
        }
        .ui-helper-hidden-accessible {
            border: 0;
            clip: rect(0 0 0 0);
            height: 1px;
            margin: -1px;
            overflow: hidden;
            padding: 0;
            position: absolute;
            width: 1px;
        }
        
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" 
        Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <asp:HiddenField ID="hdCodPro" runat="server"/>
    <div class="container-fluid">
        <br />
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>Configuración Curso - Procedimiento Odontológico</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="col-md-2">Tipo Estudio:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboTipoEstudio" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                    <asp:ListItem Value="-1">[ --- Seleccione Tipo Estudio --- ]</asp:ListItem>
                                    <asp:ListItem Value="2">PRE GRADO</asp:ListItem>
                                    <asp:ListItem Value="8">SEGUNDA ESPECIALIDAD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <label class="col-md-2">Carrera Profesional:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboCarPro" runat="server" CssClass="form-control input-sm" AutoPostBack="true" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <label class="col-md-2">Plan de Estudio:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboPlaEst" runat="server" CssClass="form-control input-sm" AutoPostBack="true" >
                                </asp:DropDownList>
                            </div>
                            <label class="col-md-2">Curso:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboCurso" runat="server" CssClass="form-control input-sm" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-group">
                            <div class="col-md-2">
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info"/>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success"/>
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnEliminar_Click" 
                                OnClientClick="return confirm('¿Desea eliminar los procedimientos seleccionados?');"/>
                            </div>  
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvProcedimiento" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_cup,codigo_pro" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_pro" HeaderText="Codigo"/>
                                <asp:BoundField DataField="nombre_pro" HeaderText="Nombre"  HeaderStyle-Width="50%" />
                                <asp:BoundField DataField="cantidad_cup" HeaderText="Cantidad" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Seleccionar" HeaderStyle-Width="10%">
                                    <ItemTemplate>
                                        <%--<asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CommandName="Eliminar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                                            OnClientClick="return confirm('¿Desea eliminar este procedimiento?');"/>--%>
                                        <asp:CheckBox ID="chkEliminar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron Datos!
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px"/>
                            <RowStyle Font-Size="11px"/>
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Seleccion de Procedimiento Odontologicos -->
    <div id="myModal" class="modal fade" role="dialog" aria-labelledby="myModalLabel" style="z-index: 0;" aria-hidden="true" 
        data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Seleccione Procedimiento Odontológico</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Nombre (*):</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3">Cantidad:</label>
                                <div class="col-md-4">
                                    <asp:TextBox ID="txtCant" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <br />
                     <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-12">(*)Escriba al menos 3 digitos para buscar</label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnsalir" class="btn btn-danger" data-dismiss="modal">Salir</button>
                    <asp:Button ID="btnGrabar" runat="server" Text="Guardar" class="btn btn-info"/>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
