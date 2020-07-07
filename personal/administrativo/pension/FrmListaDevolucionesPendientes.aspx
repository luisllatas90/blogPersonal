<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmListaDevolucionesPendientes.aspx.vb"
    Inherits="administrativo_tramite_FrmListaDevolucionesPendientes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Información Incompleta</title>

    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />

    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="private/bootstrap-datepicker3.standalone.css" rel="stylesheet" type="text/css" />
    <link href="private/jquery.loadmask.css" rel="stylesheet" type="text/css" />
    <link href="private/timeline.css" rel="stylesheet" type="text/css" />

    <script src="private/bootstrap-datepicker.min.js" type="text/javascript"></script>

    <script src="private/jquery.loadmask.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function() {
            $("#ddlFormaPago").change(function() {
                var FormaPago = $("#ddlFormaPago").val();
                //alert(FormaPago);
                if (FormaPago == "E" || FormaPago == "0") {
                    $("#ddlBanco").attr('disabled', true);
                    $("#txtCodigoInterbancario").attr('disabled', true);

                } else {
                    $("#ddlBanco").attr('disabled', false);
                    $("#txtCodigoInterbancario").attr('disabled', false);
                }

            });
            $("#btnGuardar").click(function() {
                var TipoDevolucion = $("#ddlTipoDevolucion").val();
                if (TipoDevolucion == "0") {
                    alert("Debe Seleccionar un Tipo de Devolución");
                    return false;
                }

                var FormaPago = $("#ddlFormaPago").val();
                if (FormaPago == "0") {
                    alert("Debe Seleccionar un Tipo de Devolución");
                    return false;
                }

                var Dni = $("#txtDni").val();
                if (Dni == "") {
                    alert("Debe ingresar un DNI");
                    return false;
                }

                var Titular = $("#txtTitular").val();
                if (Titular == "") {
                    alert("Debe ingrese Apellidos y Nombre");
                    return false;
                }
            });
        });

        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');

        }

        function btnGuardar_onclick() {

        }

        $("#txtDni").click(function() {
            var texto = $("#txtDni").val();
            if (texto == "") {
                alert("Debe ingresar un DNI");
                return false;
            }
            MascaraEspera('1');
        });


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

        //ID="txtCodigoInterbancario"
        $(document).ready(function() {
            $('#txtCodigoInterbancario').keyup(function() {
                this.value = (this.value + '').replace(/[^0-9]/g, '');
            });
        });
    

    </script>

    <style type="text/css">
        .row
        {
            margin-right: 0px;
            margin-left: 0px;
        }
        .content .main-content
        {
            padding-right: 18px;
        }
        .content
        {
            margin-left: 0px;
        }
        .form-control
        {
            border-radius: 0px;
            box-shadow: none;
            border-color: #d9d9d9;
            height: 30px;
            font-weight: 300; /* line-height: 40px; */
            color: black;
        }
        .form-group
        {
            margin: 6px;
        }
        .form-horizontal .control-label
        {
            padding-top: 5px;
        }
        .i-am-new
        {
            z-index: 100;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="form-inline">
    <div class="messagealert" id="alert_container">
    </div>
    <div class="container-fluid">
        <div class="page-header">
            <h3>
                Actualización de Datos <small>Personas con Devoluciones Pendientes</small></h3>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="txtNombres" runat="server" Text="DNI/Apellidos Nombres:"></asp:Label>
            </div>
            <div class="col-md-8">
                <asp:TextBox ID="txt_persona" runat="server" CssClass="form-control" Width="100%"></asp:TextBox></div>
            <div class="col-md-2">
                <center>
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" />
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:Label ID="Label1" runat="server" Text="Estado:"></asp:Label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlEstado" runat="server" AutoPostBack="true" CssClass="form-control">
                    <asp:ListItem Value="P">Pendientes</asp:ListItem>
                    <asp:ListItem Value="E">Enviados</asp:ListItem>
                </asp:DropDownList>
                <div class="col-md-2">
                    <%--<center>
                    <asp:Button ID="Button1" runat="server" Text="Buscar" class="btn btn-info" />
                </center>--%>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <hr style="color: #0056b2;" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="gvDeudas" runat="server" DataKeyNames="codigo_cin,codigo_pso,codigo_tid,codigo_fop,dni_ini,titular_Ini,codigo_ban,nroCuenta_Ini,observacion_Ini,codigo_Ini" CssClass="table table-bordered bs-table"
                        AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="fecha" HeaderText="FECHA">
                                <ItemStyle Width="8%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="documento" HeaderText="DOCUMENTO">
                                <ItemStyle Width="10%" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="persona" HeaderText="PERSONA" HtmlEncode="False">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="numeroDocIdent_Pso" HeaderText="DNI">
                                <ItemStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="total_cin" DataFormatString="{0:C2}" HeaderText="IMPORTE">
                                <ItemStyle HorizontalAlign="Right" Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Detalle_nc" HeaderText="DETALLE" HtmlEncode="False">
                                <ItemStyle Width="22%" />
                            </asp:BoundField>
                            
                            
                            <asp:BoundField DataField="codigo_tid" HeaderText="codigo_tid" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%"/>
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_fop" HeaderText="codigo_fop" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="dni_ini" HeaderText="dni_ini" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="titular_Ini" HeaderText="titular_Ini" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="codigo_ban" HeaderText="codigo_ban" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="nroCuenta_Ini" HeaderText="nroCuenta_Ini" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="observacion_Ini" HeaderText="observacion_Ini" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>                           
                             <asp:BoundField DataField="codigo_Ini" HeaderText="codigo_Ini" HtmlEncode="False" Visible=false>
                                <ItemStyle Width="0%" />
                            </asp:BoundField>                                                                                                                                                                                           
                            
                            
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="OPERACION">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Actualizar" CssClass="btn btn-info btn-sm"
                                        OnClick="btnActualizar_Click" CommandName="Update" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                </ItemTemplate>
                                <HeaderStyle></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            No se registraron cambios!
                        </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                            Font-Size="12px" />
                        <RowStyle Font-Size="11px" />
                        <EditRowStyle BackColor="#ffffcc" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <!-- Modal -->
        <div id="myModal" role="dialog" runat="server" class="modal fade" >
            <%--class="modal fade"--%>
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                        <button type="button" class="close" data-dismiss="modal">
                            &times;</button>
                        <h4 class="modal-title">
                            Actualización de Datos</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="container">
                                <h2>
                                </h2>
                            </div>
                            <div class="alert alert-danger alert-dismissable">
                                <button type="button" class="close" data-dismiss="alert">
                                    &times;</button>
                                <strong>¡Atención! </strong>
                                <asp:Label ID="lblDetalle_nc" runat="server" Style="font-weight: 700" Text="Label"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class="control-label">
                                        Tipo Devolución:</label></div>
                                <div class="col-sm-9">
                                    <div>
                                        <asp:DropDownList ID="ddlTipoDevolucion" runat="server" CssClass="form-control" Style="width: 100%">
                                            <asp:ListItem Value="0">-- SELECCIONE -- </asp:ListItem>
                                            <asp:ListItem Value="N">NORMAL</asp:ListItem>
                                            <asp:ListItem Value="B">BECAS</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class=" control-label">
                                        Forma de Pago:</label></div>
                                <div class="col-sm-9">
                                    <div>
                                        <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control" Style="width: 100%">
                                            <asp:ListItem Value="0">-- SELECCIONE -- </asp:ListItem>
                                            <asp:ListItem Value="E">EFECTIVO</asp:ListItem>
                                            <asp:ListItem Value="C">CAJA USAT</asp:ListItem>
                                            <asp:ListItem Value="A">ABONO EN CUENTA</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-12">
                                    <label class="control-label" style="color: #E33439; font-weight: bold;">
                                        Datos de la Cuenta</label></div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class=" control-label">
                                        Titular:</label></div>
                                <div class="col-sm-3">
                                    <asp:TextBox ID="txtDni" name="txtDni" runat="server" CssClass="form-control" Width="100%"
                                        placeholder="Ingrese DNI"></asp:TextBox>
                                </div>
                                <div class="col-sm-6">
                                    <asp:TextBox ID="txtTitular" name="txtTitular" runat="server" CssClass="form-control"
                                        Width="100%" placeholder="Ingrese Apellidos y Nombres"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class=" control-label">
                                        Banco:</label></div>
                                <div class="col-sm-9">
                                    <div>
                                        <asp:DropDownList ID="ddlBanco" runat="server" CssClass="form-control" Style="width: 100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class=" control-label">
                                        Número de Cuenta:</label></div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtCodigoInterbancario" name="txtCodigoInterbancario" runat="server"
                                        CssClass="form-control" Width="100%" placeholder="Código Número de Cuenta"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-12">
                                <div class="col-sm-3">
                                    <label class=" control-label">
                                        Observación:</label></div>
                                <div class="col-sm-9">
                                    <asp:TextBox ID="txtObservacion" name="txtObservacion" runat="server" Height="67px"
                                        Style="width: 100%" placeholder="Ingrese Observación" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <center>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-info" />
                            <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn btn-danger" />
                        </center>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="HDCodigo_Cin" runat="server" />
        <asp:HiddenField ID="HDcodigo_per" runat="server" />
        <asp:HiddenField ID="HDCodigo_ini" runat="server" />        
    </form>
</body>
</html>
