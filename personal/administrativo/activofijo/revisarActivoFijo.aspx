<%@ Page Language="VB" AutoEventWireup="false" CodeFile="revisarActivoFijo.aspx.vb" 
    Inherits="administrativo_activofijo_revisarActivoFijo" EnableEventValidation="false"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html;charset=ISO-8859-1" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Lista de Traslados de Activo Fijo</title>
    
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    
    <link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    
    <script type="text/javascript">

        $(document).ready(function() {
            $('#btnGuardar').click(function() {
                var txtObs = $('#txtObservacion').val();
                if (txtObs == '') {
                    alert("Debe ingresar observación");
                    return false;
                }
                //MascaraEsperaModal('1');
            });
        });

        function openModal() {
            $('#myModal').modal('show');
            var obs = $('#hdObservacion').val();
            $('#txtObservacion').val(obs);
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }

        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            if (cssclass != 'alert-danger') {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-success"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            } else {
                $('#alert_container').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert alert-danger"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><span>' + message + '</span></div>');
            }
        }

        function MascaraEsperaModal(sw) {
            if (sw == "1") {
                $('#modalFinalizaBody').mask("Espere...");
            }
            if (sw == "0") {
                $('#modalFinalizaBody').unmask();
            }
        }
        
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel-heading">
                Revisi&oacute;n de Traslado de Activo Fijo
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Estado:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboEstado" runat="server" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                                    <asp:ListItem Value="1">PENDIENTE</asp:ListItem>
                                    <asp:ListItem Value="2">AUTORIZADO</asp:ListItem>
                                    <asp:ListItem Value="3">CONFIRMADO</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">Nro Traslado:</label>
                            <div class="col-md-8">
                                <asp:TextBox ID="txtNroPedido" runat="server" CssClass="form-control"></asp:TextBox>
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
                        <asp:GridView ID="gvTraslado" runat="server" Width="99%" AutoGenerateColumns="false" ShowHeader="true" 
                            DataKeyNames="codigo_tld,codigo_per,codigo_Cco,cod_estado,codigo_rta,observacion_tld" 
                            CssClass="table table-bordered">
                            <Columns>
                                <asp:BoundField DataField="codigo_tld" HeaderText="Codigo"/>
                                <asp:BoundField DataField="fecha_tld" HeaderText="Fecha"/>
                                <asp:BoundField DataField="tipomov_tld" HeaderText="Tipo"/>
                                <asp:BoundField DataField="codigo_per" Visible="false"/>
                                <asp:BoundField DataField="responsable" HeaderText="Responsable" />
                                <asp:BoundField DataField="codigo_Cco" Visible="false" />
                                <asp:BoundField DataField="descripcion_Cco" HeaderText="Centro de Costo" />
                                <asp:BoundField DataField="observacion_tld" HeaderText="Observacion" />
                                <asp:BoundField DataField="estado_tld" HeaderText="Estado" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACION">
                                    <ItemTemplate>
                                        <asp:Button ID="btnAutorizar" runat="server" Text="Autorizar" OnClick="btnAutorizar_Click" CommandName="Autorizar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-success btn-sm" 
                                            OnClientClick="return confirm('¿Desea autorizar este traslado?');" />
                                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar" OnClick="btnConfirmar_Click" CommandName="Confirmar" 
                                            CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CssClass="btn btn-info btn-sm" 
                                            OnClientClick="return confirm('¿Desea confirmar este traslado?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Revisi&oacute;n de Traslado</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <div class="form-group">
                            <asp:TextBox ID="txtObservacion" runat="server" CssClass="form-control" Height="100px" MaxLength="200" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal">Salir</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-info"/>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdTraslado" runat="server" />
    <asp:HiddenField ID="hdEstado" runat="server" />
    <asp:HiddenField ID="hdIdRevision" runat="server" />
    <asp:HiddenField ID="hdObservacion" runat="server" />
    </form>
</body>
</html>
