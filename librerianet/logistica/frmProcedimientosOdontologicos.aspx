<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmProcedimientosOdontologicos.aspx.vb" 
    Inherits="logistica_frmProcedimientosOdontologicos" EnableEventValidation="false" %>
<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Registro Procedimiento Odontologico</title>
    
    <script src="../Scripts/js1_12/jquery-1.12.3.min.js" type="text/javascript"></script>
    
    <link href="../Scripts/css/bootstrap.css" rel="Stylesheet" type="text/css"/>
    
    <script src="../Scripts/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        jQuery(document).ready(function() {
            $('#btnGrabar').click(function() {
                var nom = $('#txtNombre').val();
                if (nom.trim() == '') {
                    alert('Ingrese Nombre de Procedimiento');
                    $('#Nombre').focus();
                    return false;
                }
                var des = $('#txtDescripcion').val()
                if (des.trim() == '') {
                    alert('Ingrese Descripcion de Procedimiento');
                    $('#txtDescripcion').focus();
                    return false;
                }
            })
        });

        function openModal(accion) {
            if (accion == 'Agregar') {
                $('#hdCodPro').val('');
                $('#txtNombre').val('');
                $('#txtDescripcion').val('');
                $("#cboEstado").attr('disabled', true);
            }
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
                <h4> Registro Procedimiento Odontológico </h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Tipo Estudio:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboTipoEstBus" runat="server" CssClass="form-control input-sm">
                                    <asp:ListItem Value="-1">TODOS</asp:ListItem>
                                    <asp:ListItem Value="2">PRE GRADO</asp:ListItem>
                                    <asp:ListItem Value="8">SEGUNDA ESPECIALIDAD</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Procedimiento:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control input-sm"></asp:TextBox>
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
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvProcedimiento" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_pro,nombre_pro,descripcion_pro,codigo_test,estado_pro" CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_pro" HeaderText="Codigo"/>
                                <asp:BoundField DataField="nombre_pro" HeaderText="Nombre" />
                                <asp:BoundField DataField="descripcion_pro" HeaderText="Descripcion" />
                                <asp:BoundField DataField="descripcion_test" HeaderText="Tipo Estudio" />
                                <asp:BoundField DataField="estado_pro" HeaderText="Estado" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" OnClick="btnEditar_Click" CommandName="Editar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-warning btn-sm"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CommandName="Eliminar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                                            OnClientClick="return confirm('¿Desea eliminar este procedimiento?');"/>
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
    <div id="myModal" class="modal fade" role="dialog" data-backdrop="static" data-keyboard="false">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Registro de Procedimiento Odontológico</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Nombre:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Descripcion:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control input-sm" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Tipo Estudio:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboTipoEstudio" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="2">PRE GRADO</asp:ListItem>
                                        <asp:ListItem Value="8">SEGUNDA ESPECIALIDAD</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Estado:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control input-sm" >
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
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
    </form>
</body>
</html>
