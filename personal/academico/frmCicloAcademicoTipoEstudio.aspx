<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmCicloAcademicoTipoEstudio.aspx.vb" 
    Inherits="academico_frmCicloAcademicoTipoEstudio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Ciclo Academico Tipo Estudio</title>
    
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
    <link href="../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>

    <script src="js/popper.js" type="text/javascript"></script>

    <script src="../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script src="../assets/js/bootstrap-datepicker.js" type="text/javascript"></script>

    <script src="../assets/fontawesome-5.2/js/all.min.js" type="text/javascript"></script>

    <script src="js/bootbox.min.js" type="text/javascript"></script>
    
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
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default" id="pnlLista" runat="server">
            <div class="panel panel-heading">
                <h4>Ciclo Academico Tipo Estudio</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Descripción:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:TextBox ID="txtdescripcion" runat="server" CssClass="form-control input-sm" MaxLength="7" Style="text-transform: uppercase"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Duración Acad:</label>
                            <div class="col-md-4">
                                <div class="input-group date">
                                    <asp:TextBox ID="fecInicioAca" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group date">
                                    <asp:TextBox ID="fecFinAca" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Duración Adm:</label>
                            <div class="col-md-4">
                                <div class="input-group date">
                                    <asp:TextBox ID="fecInicioAdm" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group date">
                                    <asp:TextBox ID="fecFinAdm" runat="server" CssClass="form-control input-sm" data-provide="datepicker"></asp:TextBox>
                                    <span class="input-group-addon bg"><i class="ion ion-ios-calendar-outline"></i></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label class="col-md-4">Tipo Estudio:</label>
                            <div class="col-md-4">
                                <asp:DropDownList ID="cboTipoEstudio" runat="server" CssClass="form-control input-sm" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="btnAgregar" runat="server" Text='<i class="fa fa-plus"></i> Agregar'
                                    CssClass="btn btn-success" OnClick="btnAgregar_Click"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvCATest" runat="server" AutoGenerateColumns="false" 
                    DataKeyNames="codigo_ctest,vigente_cte,codigo_test,admision_cte,fechaInicio_cte,fechaFin_cte,fechaIniAdm_cte,fechaFinAdm_cte"
                    CssClass="table table-sm table-bordered table-hover" OnRowCreated="gvCATest_OnRowCreated" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="codigo_test" HeaderText="ID"/>
                            <asp:BoundField DataField="descripcion_Cac" HeaderText="Descripción"/>
                            <asp:BoundField DataField="descripcion_test" HeaderText="Tipo Estudio"/>
                            <asp:BoundField DataField="fechaInicio_cte" HeaderText="F. Inicio" DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="fechaFin_cte" HeaderText="F. Fin" DataFormatString="{0:d}"/>
                            <asp:TemplateField HeaderText="Vigente">
                                <ItemTemplate><%#IIf(Boolean.Parse(Eval("vigente_cte").ToString()), "SI", "No")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Habilitar Vigencia" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnHabilitar" runat="server" CommandName="Habilitar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-warning btn-sm" ToolTip = "Habilitar Vigencia"
                                        OnClientClick="return confirm('¿ Desea activar la vigencia ?');">
                                        <span><i class="fa fa-check-circle"></i></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="fechaIniAdm_cte" HeaderText="F. Inicio Adm." DataFormatString="{0:d}"/>
                            <asp:BoundField DataField="fechaFinAdm_cte" HeaderText="F. Fin Adm." DataFormatString="{0:d}"/>
                            <asp:TemplateField HeaderText="Admisión">
                                <ItemTemplate><%#IIf(Boolean.Parse(Eval("admision_cte").ToString()), "SI", "No")%></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Habilitar Adm." ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnAdmision" runat="server" CommandName="Admision" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-warning btn-sm" ToolTip = "Habilitar Admisión"
                                        OnClientClick="return confirm('¿ Desea activar la admisión ?');">
                                        <span><i class="fa fa-calendar-check"></i></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acción" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditar" runat="server" CommandName="Editar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>"
                                        CssClass="btn btn-primary btn-sm" ToolTip = "Editar"
                                        OnClientClick="return confirm('¿ Desea editar el registro ?');">
                                        <span><i class="fa fa-pen"></i></span>
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate> No se encontró ningun registro </EmptyDataTemplate>
                        <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                        <EditRowStyle BackColor="#FFFFCC" />
                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
