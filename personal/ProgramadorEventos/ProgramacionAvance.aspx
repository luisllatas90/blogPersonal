<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProgramacionAvance.aspx.vb"
    Inherits="Programacion" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Pragma" content="no-cache" />
    <%--Compatibilidad con IE--%>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <%--Compatibilidad con IE--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <title>Programación de comunicación</title>
    <link href="css/programacionAvance.css" rel="stylesheet" type="text/css" />
    <link href="libs/bootstrap-4.1/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="libs/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="libs/jquery/jquery-3.3.1.js" type="text/javascript"></script>

    <script src="libs/bootstrap-4.1/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="libs/fontawesome-5.2/js/fontawesome.min.js" type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off" method="post">
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>
    <div class="container-fluid">
        <div class="panel panel-default" style="margin-top: 15px; background-color: #FBFBFB;">
            <div class="panel panel-body">
                <h2 class="page-header" style="margin-top: 0px; margin-bottom: 5px; border-bottom: 1px solid #FFC4C4;">
                    <span style="color: rgb(205,0,0);">Avance de Programación</span></h2>
                <asp:UpdatePanel ID="UpdatePanel" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-xs-12 col-sm-4">
                                <label for="ddlTipoEstudio" class="form-control-sm">
                                    Tipo Estudio</label>
                                <asp:DropDownList runat="server" ID="ddlTipoEstudio" CssClass="form-control form-control-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-4">
                                <label for="ddlConvocatoria" class="form-control-sm">
                                    Convocatoria</label>
                                <asp:DropDownList runat="server" ID="ddlConvocatoria" CssClass="form-control form-control-sm"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-xs-12 col-sm-4">
                                <label for="ddlEvento" class="form-control-sm">
                                    Evento</label>
                                <asp:DropDownList runat="server" ID="ddlEvento" CssClass="form-control form-control-sm">
                                    <asp:ListItem Value="">--Seleccione--</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-1">
                                &nbsp;
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-2">
                                <button type="button" id="btnFiltrar" name="btnFiltrar" runat="server" class="btn btn-sm btn-success" style="width: 100%">
                                    Buscar
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12 col-sm-1">
                                &nbsp;
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlTipoEstudio" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="panel panel-body">
                <div class="table-responsive">
                    <asp:UpdatePanel ID="udpEvento" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:GridView ID="grwEvento" runat="server" AutoGenerateColumns="false" DataKeyNames="codigo"
                                CssClass="table table-sm table-bordered table-hover" GridLines="None" ShowFooter="true">
                                <Columns>
                                    <asp:TemplateField HeaderText="EVENTO" SortExpression="evento" ItemStyle-Width="16%">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkEvento" runat="server" Text='<%# Eval("evento") %>' NavigateUrl='<%# String.Format("~/ProgramadorEventos/Programacion.aspx?con={0}&evt={1}&est={2}&des={3}&cat={4}", Eval("codigo_con"), Eval("codigo"), Eval("estado"), Eval("evento"), Eval("categoria_pro")) %>'></asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="convocatoria" HeaderText="CONVOCATORIA" ItemStyle-Width="12%" />
                                    <asp:TemplateField HeaderText="PORCENTAJE DE AVANCE" SortExpression="%" ItemStyle-Width="55%">
                                        <ItemTemplate>
                                            <div class="progress">
                                                <div id="divProgress" class="progress-bar <%# PintarAvanceDIV(CalcularPorcentaje(Eval("enviados"), Eval("total"))) %>"
                                                    role="progressbar" aria-valuenow="<%# CalcularPorcentaje(Eval("enviados"), Eval("total")) %>" aria-valuemin="0"
                                                    aria-valuemax="100" style="width: <%# CalcularPorcentaje(Eval("enviados"), Eval("total")) %>%">
                                                    <span class="sr-only">
                                                        <%# Eval("enviados") %>
                                                        % Completado </span>
                                                </div>
                                                <span class="progress-type">
                                                    <%# Eval("enviados") %>
                                                    /
                                                    <%# Eval("total") %>
                                                </span><span class="progress-completed">
                                                    <%# CalcularPorcentaje(Eval("enviados"), Eval("total")) %>
                                                    % </span>
                                            </div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <div class="progress-meter">
                                                <div class="meter meter-left" style="width: 25%;">
                                                    <span class="meter-text">Muy bajo</span></div>
                                                <div class="meter meter-left" style="width: 25%;">
                                                    <span class="meter-text">Bajo</span></div>
                                                <div class="meter meter-right" style="width: 30%;">
                                                    <span class="meter-text">Muy alto</span></div>
                                                <div class="meter meter-right" style="width: 20%;">
                                                    <span class="meter-text">Alto</span></div>
                                            </div>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="fecini" HeaderText="ABIERTO EL" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="fecfin" HeaderText="CERRADO" ItemStyle-Width="6%" />
                                    <asp:BoundField DataField="estado" HeaderText="ESTADO" ItemStyle-Width="5%" />
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontró ningún evento
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="10px" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
