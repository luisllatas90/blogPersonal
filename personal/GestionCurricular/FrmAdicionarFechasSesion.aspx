<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAdicionarFechasSesion.aspx.vb"
    Inherits="GestionCurricular_FrmAdicionarFechasSesion" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Seleccionar fechas de sesión</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../assets/fontawesome-5.2/css/all.min.css" />

    <script type="text/javascript" src="../assets/js/jquery.js"></script>

    <script type="text/javascript" src="js/popper.js"></script>

    <script type="text/javascript" src="../assets/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="../assets/fontawesome-5.2/js/all.min.js"></script>

    <script type="text/javascript">
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
    <form id="form1" runat="server" method="post" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release" AsyncPostBackTimeout="360">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <div class="row">
                    <div class="col-md-1">
                        <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-default">
                            <span><i class="fa fa-arrow-left"></i></span> Volver
                        </asp:LinkButton>
                    </div>
                    <div class="col-md-11">
                        <h4>
                            Asignar fechas:
                            <label id="lblCursoA" runat="server" style="font-weight: 100">
                            </label>
                        </h4>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <asp:UpdatePanel ID="updMensaje" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                        <div class="messagealert" id="divAlertModal" runat="server" visible="false" style="margin-bottom: 10px">
                            <div id="alert_div_modal" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;"
                                class="alert alert-info">
                                <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a> <span
                                    id="lblMensaje" runat="server"></span>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="udpSesion" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="False">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-md-4">
                                        <asp:DropDownList ID="ddlUnidad" runat="server" CssClass="form-control input-sm"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-8">
                                        <label style="font-weight: 100; text-align: right; float: right; margin-top: 5px;
                                            margin-bottom: 0px; font-size: smaller; color: #8B0000;">
                                            (*)Mantenga presionado la tecla Ctrl para seleccionar más de una fecha</label>
                                    </div>
                                </div>
                                <asp:GridView ID="gvSesion" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_ses,nombre_ses,dia_fec,codigo_fec,codigo_gru,codigo_sgr,codigo_uni,numero_uni,descripcion_uni"
                                    CellPadding="0" ForeColor="#333333" CssClass="table table-sm table-striped table-bordered table-condensed">
                                    <Columns>
                                        <asp:BoundField DataField="codigo_ses" HeaderText="Código" Visible="false" HeaderStyle-Width="0%"
                                            ItemStyle-Width="0%" FooterStyle-Width="0%"></asp:BoundField>
                                        <asp:BoundField HtmlEncode="false" DataField="contenido" HeaderText="Contenido" ReadOnly="True"
                                            HeaderStyle-Width="28%" ItemStyle-Width="28%" FooterStyle-Width="28%">
                                            <ItemStyle Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField HtmlEncode="false" DataField="actividad" HeaderText="Actividad" ReadOnly="True"
                                            HeaderStyle-Width="28%" ItemStyle-Width="28%" FooterStyle-Width="28%">
                                            <ItemStyle Wrap="true" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="sesion" HeaderText="Sesión" ReadOnly="True" HeaderStyle-Width="5%"
                                            Visible="false">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="5%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre_ses" HeaderText="Descripción" ReadOnly="True" HeaderStyle-Width="10%"
                                            ItemStyle-Width="10%" FooterStyle-Width="10%">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcion_sgr" HeaderText="Subgrupo" ReadOnly="True"
                                            HeaderStyle-Width="6%" ItemStyle-Width="6%" FooterStyle-Width="6%">
                                            <ItemStyle Wrap="True" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Fecha(s)*" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                            FooterStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFecha" runat="server" Text='<%# Eval("nombre_fec") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ListBox ID="ddlFecha" runat="server" SelectionMode="Multiple" CssClass="form-control form-control-sm selectpicker"
                                                    size="6" Style="font-size: 12px; width: 180px; height: 120px;" ToolTip="Presione Ctrl para seleccionar más de una fecha">
                                                </asp:ListBox>
                                            </EditItemTemplate>
                                            <ItemStyle Wrap="True" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Editar" ShowHeader="False" HeaderStyle-Width="8%"
                                            ItemStyle-Width="8%" FooterStyle-Width="8%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Editar fecha(s)" CssClass="btn btn-primary btn-sm"
                                                    CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                                    Text='<i class="fa fa-pen"></i>'></asp:LinkButton>
                                                <span onclick="return confirm('¿Está seguro de eliminar la fecha asignada?')">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" ToolTip="Eliminar fecha(s)" CssClass="btn btn-danger btn-sm"
                                                        CausesValidation="True" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>'
                                                        Text='<i class="fa fa-backspace"></i>' OnClick="OnDeleteFecha" />
                                                </span>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" ToolTip="Actualizar fecha(s)" CssClass="btn btn-success btn-sm"
                                                    CausesValidation="True" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>'
                                                    Text='<i class="fa fa-save"></i>'></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton4" runat="server" ToolTip="Cancelar edición" CssClass="btn btn-info btn-sm"
                                                    CausesValidation="False" CommandName="Cancel" CommandArgument='<%# Container.DataItemIndex %>'
                                                    Text='<i class="fa fa-times"></i>'></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No se encontraron Datos
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                        Font-Size="12px" />
                                    <RowStyle Font-Size="12px" />
                                    <EditRowStyle Font-Size="12px" />
                                    <FooterStyle Font-Size="12px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
