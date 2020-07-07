<%@ Page Language="VB" AutoEventWireup="false" CodeFile="listaUbicacionesAF.aspx.vb" 
    Inherits="administrativo_activofijo_listaUbicacionesAF" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Lista de Ubicaciones de Activo Fijo</title>
    
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        $(document).ready(function() {
            $('#btnGrabar').click(function() {
                var ddl = document.getElementById('<%=cboUbicacion.ClientID%>');
                if (ddl.selectedIndex < 0) {
                    alert("Debe Seleccionar Ubicacion");
                    $('#cboUbicacion').focus();
                    return false;
                }
                var des = $('#txtDescripcion').val();
                if (des == '') {
                    alert("Debe Ingresar Descripcion");
                    $('#txtDescripcion').focus();
                    return false;
                }
            });
        });

        function openModal(accion) {
            $('#myModal').modal('show');
            $('#txtDescripcion').val('');
            $('#cboUbicacion').val('');
            $('#hdAccion').val(accion);
            if (accion == 'Editar') {
                var descripcion = $('#hdDescripcion').val();
                $('#txtDescripcion').val(descripcion);
                var ubicacion = $('#hdCodigoUbe').val();
                $('#cboUbicacion').val(ubicacion);
            }
        }

        function closeModal() {
            $('#hdIdUbicacion').val('');
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

        function showConfirm() {
            var rpta = window.confirm('¿Desea Eliminar este Registro?');
            document.getElementById('hdConfirmacion')['value'] = rpta;
            return true;
        }
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                Listado de Ubicaciones de Activo Fijo
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-4">
                        <div class="form-group">
                            <label class="col-md-4">Ubicaci&oacute;n:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboUbicacionBus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Descripci&oacute;n:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtDescripcionBus" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <center>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-info"/>
                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar" CssClass="btn btn-success"/>
                        </center>
                    </div>
                </div>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="gvUbicacion" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_uba,codigo_ube,des_uba" CssClass="table table-bordered">
                            <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Operaciones" HeaderStyle-Width="20%">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditar" runat="server" Text = "Editar" OnClick="btnEditar_Click" CommandName="Editar"
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-warning btn-sm"/>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click" CommandName="Eliminar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-danger btn-sm" 
                                            OnClientClick="return confirm('¿Desea eliminar esta ubicacion?');"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="codigo_uba" HeaderText="Codigo"/>
                                <asp:BoundField DataField="des_ube" HeaderText="Ubicacion" />
                                <asp:BoundField DataField="des_uba" HeaderText="Descripcion" />
                            </Columns>
                            <EmptyDataTemplate>
                                No se encontraron registros!
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                            <RowStyle Font-Size="11px" />
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
                    <h4 class="modal-title">Registro de Ubicaciones</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Ubicaci&oacute;n:</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="cboUbicacion" runat="server" CssClass="form-control" >
                                        <asp:ListItem Value="0">TODOS</asp:ListItem>
                                    </asp:DropDownList>
                                </div>      
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-4">Descripci&oacute;n:</label>
                                <div class="col-md-8">
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
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
    <asp:HiddenField ID="hdIdUbicacion" runat="server"/>
    <asp:HiddenField ID="hdCodigoUbe" runat="server"/>
    <asp:HiddenField ID="hdDescripcion" runat="server"/>
    <asp:HiddenField ID="hdAccion" runat="server"/>
    <asp:HiddenField ID="hdConfirmacion" runat="server"/>
    </form>
</body>
</html>
