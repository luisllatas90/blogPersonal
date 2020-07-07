<%@ Page Language="VB" AutoEventWireup="false" CodeFile="accederaulavirtual.aspx.vb"
    Inherits="academico_notas_profesor_accederaulavirtual" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="google" value="notranslate" />
    <title>Mis cursos en Aula Virtual</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    <link rel="stylesheet" type="text/css" href="../../assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../../assets/css/font-awesome.min.css" />

    <script type="text/javascript" src="../../assets/js/jquery.js"></script>

    <script type="text/javascript" src="../../assets/js/popper.js"></script>

    <script type="text/javascript" src="../../assets/js/bootstrap.min.js"></script>

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

    <script type="text/javascript">
        function downSilabo(codigo) {
            window.open("DescargarArchivo.aspx?id=" + codigo);
        }
    </script>

</head>
<body>
    <form runat="server" name="frm" method="post" autocomplete="off">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>
        <br />
        <div class="panel panel-default">
            <div class="panel panel-heading">
                <h4>
                    Mis Cursos en Aula Virtual</h4>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="col-xs-5">
                            <div class="row">
                                <label class="col-xs-12">
                                    Ciclo Académico</label>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <asp:DropDownList ID="ddlCiclo" runat="server" CssClass="form-control input-sm" AutoPostBack="True">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            &nbsp;
                        </div>
                        <div class="col-xs-3" style="border-left: 1px solid #DDD; border-right: 1px solid #DDD;">
                            <div class="row">
                                <div class="col-xs-12">
                                    <h5 style="font-weight: 700">
                                        ¿Consultas sobre Aula Virtual?</h5>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <label style="font-weight: 300">
                                        <i class='fa fa-envelope'></i>&nbsp;serviciosti@usat.edu.pe</label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-xs-12">
                                    <label style="font-weight: 300">
                                        <i class='fa fa-phone'></i>&nbsp;(074) 606200 – Anexo 4050</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-2">
                            <br />
                            <asp:LinkButton ID="lnkMoodle2" runat="server"><img src="../../../aulavirtual/img/btn2.png" alt="Ayuda al docente" style="width: 148px; height: 60px;" /></asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-xs-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvCarga" runat="server" AutoGenerateColumns="False" DataKeyNames="codigo_cup, idcurso_mdl, grupoHor_cup, ciclo_cur, nombre_cpf, matriculados, silabo"
                                ShowHeader="true" CellPadding="0" ForeColor="#333333" CssClass="table table-sm table-bordered table-hover">
                                <Columns>
                                    <asp:BoundField DataField="mdl" HeaderText="Estado" Visible="true" HeaderStyle-Width="6%"
                                        ItemStyle-Width="6%" FooterStyle-Width="6%" />
                                    <asp:BoundField DataField="nombre_cur" HeaderText="Curso Programado" HeaderStyle-Width="20%"
                                        ItemStyle-Width="20%" FooterStyle-Width="20%" />
                                    <%--  
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblAula" Text="Acceder" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnAcceder" runat="server" Text="<i class='fa fa-share-square'></i>"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="cmdAcceder"
                                                CssClass="btn btn-primary btn-sm" ToolTip="Ir a Aula Virtual" OnClientClick="return confirm('¿Desea ir al aula virtual?');" />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="grupoHor_cup" HeaderText="Grupo" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="10%" ItemStyle-Width="10%" FooterStyle-Width="10%" />
                                    <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="4%" ItemStyle-Width="4%" FooterStyle-Width="4%" />
                                    <asp:BoundField DataField="nombre_cpf" HeaderText="Programa de estudios" HeaderStyle-Width="17%"
                                        ItemStyle-Width="17%" FooterStyle-Width="17%" />
                                    <asp:BoundField DataField="matriculados" HeaderText="Nro.Mat." ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-Width="5%" ItemStyle-Width="5%" FooterStyle-Width="5%" />
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblLstDocente" Text="Docentes asignados" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDocente" runat="server" Text="Actualizar <i class='fa fa-refresh'></i>"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="actDocente"
                                                CssClass="btn btn-warning btn-sm" ToolTip="Actualizar lista de docentes" />
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblLstEstudiante" Text="Lista de estudiantes" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEstudiante" runat="server" Text="Actualizar <i class='fa fa-refresh'></i>"
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" CommandName="actEstudiante"
                                                CssClass="btn btn-success btn-sm" ToolTip="Actualizar lista de estudiantes" />
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="silabo" HeaderText="Sílabo" HeaderStyle-Width="7%" ItemStyle-Width="7%"
                                        FooterStyle-Width="7%" />
                                        --%>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron cursos
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
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
