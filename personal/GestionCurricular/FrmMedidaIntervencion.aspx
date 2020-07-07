<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmMedidaIntervencion.aspx.vb"
    Inherits="GestionCurricular_FrmMedidaIntervencion" %>

<%@ Register Assembly="BusyBoxDotNet" Namespace="BusyBoxDotNet" TagPrefix="busyboxdotnet" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate">
    <title>Medida de Intervención</title>
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
    <form id="form1" runat="server" autocomplete="off" method="post">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>
                    Detallar Medida de Intervención por Asignatura</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-5">
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
                    <div class="col-md-7">
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
                <br />
                <div class="row">
                    <div class="col-md-5">
                        <div class="form-group">
                            <label class="col-md-4">
                                Momento:</label>
                            <div class="col-md-8">
                                <asp:DropDownList ID="cboMomento" runat="server" CssClass="form-control input-sm"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvAsignatura" runat="server" AutoGenerateColumns="false" ShowHeader="true"
                                AllowPaging="True" PageSize="10" DataKeyNames="codigo_cup, codigo_med" OnRowEditing="gvAsignatura_RowEditing"
                                CssClass="table table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="cicloRom" HeaderText="Ciclo" ItemStyle-Width="5%" HeaderStyle-Width="5%"
                                        FooterStyle-Width="5%" ReadOnly="true" />
                                    <asp:BoundField DataField="abreviatura_Cpf" HeaderText="Carr. Prof." ItemStyle-Width="9%"
                                        HeaderStyle-Width="9%" FooterStyle-Width="9%" ReadOnly="true" />
                                    <asp:BoundField DataField="nombre_Cur" HeaderText="Asignatura" ItemStyle-Width="20%"
                                        HeaderStyle-Width="20%" FooterStyle-Width="20%" ReadOnly="true" />
                                    <asp:BoundField DataField="grupoHor_Cup" HeaderText="Grupo" ItemStyle-Width="5%"
                                        HeaderStyle-Width="5%" FooterStyle-Width="5%" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Medida de Intervención" HeaderStyle-Width="46%" ItemStyle-Width="46%"
                                        FooterStyle-Width="46%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMedida" runat="server" Text='<%#Eval("descripcion_med")%>' TextMode="MultiLine"
                                                Rows="3"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtMedida" runat="server" CssClass="form-control input-sm" Text='<%#Eval("descripcion_med")%>'
                                                TextMode="MultiLine" Rows="2"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="¿Realizado?" HeaderStyle-Width="5%" ItemStyle-Width="5%"
                                        FooterStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:RadioButton ID="lblSI" runat="server" GroupName="realizado" Text="Sí" Enabled="false"
                                                Checked='<%# IIF(Eval("realizado_med"), "true", "false")%>' />
                                            &nbsp;
                                            <asp:RadioButton ID="lblNO" runat="server" GroupName="realizado" Text="No" Enabled="false"
                                                Checked='<%# IIF(Eval("realizado_med"), "false", "true")%>' />
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:RadioButton ID="rbtSI" runat="server" GroupName="realizado" Text="Sí" Checked='<%# IIF(Eval("realizado_med"), "true", "false")%>' />
                                            &nbsp;
                                            <asp:RadioButton ID="rbtNO" runat="server" GroupName="realizado" Text="No" Checked='<%# IIF(Eval("realizado_med"), "false", "true")%>' />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acción" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                        FooterStyle-Width="10%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" ToolTip="Editar" CssClass="btn btn-primary btn-sm"
                                                CausesValidation="False" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>'
                                                Text='<i class="fa fa-pen"></i>'></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" Text='<i class="fa fa-save"></i>' ToolTip="Actualizar"
                                                runat="server" OnClick="OnUpdateMedida" CssClass="btn btn-success btn-sm" />
                                            <asp:LinkButton ID="LinkButton4" Text='<i class="fa fa-times"></i>' ToolTip="Cancelar"
                                                runat="server" OnClick="OnCancelMedida" CssClass="btn btn-info btn-sm" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron datos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" Font-Bold="true" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#003399" HorizontalAlign="Center" CssClass="pagination-ys" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="«" LastPageText="»" PageButtonCount="10"
                                    Position="Bottom" Visible="true" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
