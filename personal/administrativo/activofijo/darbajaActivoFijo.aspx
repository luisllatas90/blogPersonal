<%@ Page Language="VB" AutoEventWireup="false" CodeFile="darbajaActivoFijo.aspx.vb" 
    Inherits="administrativo_activofijo_darbajaActivoFijo" EnableEventValidation="false" %>
<%@ Register assembly="BusyBoxDotNet" namespace="BusyBoxDotNet" tagprefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Dar Baja Activo Fijo</title>
    
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css"/>
    
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script src="../../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var fecha = $('#txtFecha').val();
                if (fecha == '') {
                    alert("Ingrese Fecha");
                    $('#txtFecha').focus();
                    return false;
                }
                var ddlTB = document.getElementById('<%=cboTipoBien.ClientID%>');
                if (ddlTB.selectedIndex < 0) {
                    alert("Debe Seleccionar Tipo Activo");
                    $('#cboTipoBien').focus();
                    return false;
                }
                var ddlMB = document.getElementById('<%=cboMotivoBaja.ClientID%>');
                if (ddlMB.selectedIndex < 0) {
                    alert("Debe Seleccionar Motivo Baja");
                    $('#cboMotivoBaja').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
            $('#cboTipoBien').val('');
            var motivo = $('#hdMotivoBaja').val();
            $('#cboMotivoBaja').val(motivo);
        }

        function closeModal() {
            $('#hdIdActivoFijo').val('');
            $('#hdMotivoBaja').val('');
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
            if (cssclss != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }
    
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <busyboxdotnet:BusyBox ID="BusyBox1" runat="server" ShowBusyBox="OnLeavingPage" Image="Clock" Text="Su solicitud esta siendo procesada..." Title="Por favor espere" />
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                Dar de Baja Activo Fijo
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Etiqueta:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Ubicaci&oacute;n:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboUbicacion" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info"/>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvActivoFijo" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_af,motivo_af" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_af" HeaderText="Codigo"/>
                                <asp:BoundField DataField="etiqueta_af" HeaderText="Etiqueta" />
                                <asp:BoundField DataField="descripcionArt" HeaderText="Descripcion" />
                                <asp:BoundField DataField="valorCompra_af" HeaderText="Valor"/>
                                <asp:BoundField DataField="motivo" HeaderText="Motivo" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnDarBaja" runat="server" Text="Dar Baja" OnClick="btnDarBaja_Click" CommandName="DarBaja" 
                                            CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                                            OnClientClick="return confirm('¿Desea dar de baja este Activo Fijo?');" />
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
    <!-- Modal Registro de Ubicacion Activo Fijo -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registro de Baja de Activo Fijo</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Fecha Baja</label>
                                <div class="col-md-8">
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" data-provide="datepicker"></asp:TextBox>
                                        <span class="input-group-addon bg">
                                            <i class="ion ion-ios-calendar-outline"></i>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Tipo Bien:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboTipoBien" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="1">Aparato Eléctrico y Electrónico</asp:ListItem>
                                        <asp:ListItem Value="2">Otro bien inoperativo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>      
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Motivo de Baja:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboMotivoBaja" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="L">LICITACIÓN</asp:ListItem>
                                        <asp:ListItem Value="D">DONACIÓN</asp:ListItem>
                                        <asp:ListItem Value="E">ELIMINACIÓN</asp:ListItem>
                                    </asp:DropDownList>
                                </div>      
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Informe T&eacute;cnico:</label>
                                <div class="col-md-8">
                                    <asp:FileUpload ID="fuArchivo" runat="server" CssClass="form-control"/>
                                </div>
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
    <asp:HiddenField ID="hdIdActivoFijo" runat="server" />
    <asp:HiddenField ID="hdMotivoBaja" runat="server" />
    </form>
</body>
</html>
