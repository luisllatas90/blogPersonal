<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmGrupoAsignacionAlumno_Exportar.aspx.vb" 
    Inherits="administrativo_gestion_educativa_frmGrupoAsignacionAlumno" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=0" />
    <meta name="google" value="notranslate" />
    <title>Asignación de Postulantes a Grupo de Evaluación</title>
    
    <!-- custom scrollbar stylesheet -->
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta http-equiv='X-UA-Compatible' content='IE=7' />
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <meta http-equiv='X-UA-Compatible' content='IE=10' />
    
    <%--<link href="../../scripts/css/bootstrap.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/css/bootstrap-datepicker3.css" rel="Stylesheet" type="text/css" />
    <link href="../../assets/fontawesome-5.2/css/all.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">--%>
    
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../assets/js/popper.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../assets/bootstrap-select-1.13.1/js/bootstrap-select.min.js"></script>
    
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
                <div class="row">
                    <h4>Asignación de Postulantes a Grupo de Evaluación</h4>
                </div>
            </div>
            <div class="panel panel-body">
                <div class="row">
                    <div class="col-md-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvGrupoAlumno" runat="server" AutoGenerateColumns="false" 
                                DataKeyNames="codigo_gva, codigo_gru, codigo_alu"
                                CssClass="table table-sm table-bordered table-hover" GridLines="None">
                                <Columns>
                                    <asp:TemplateField HeaderText="Selec" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Visible='<%# IIF(Eval("codigo_gva")<>-1,"False","True") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="Codigo"/>
                                    <asp:BoundField DataField="nroDocIdent_Alu" HeaderText="DNI"/>
                                    <asp:BoundField DataField="Alumno" HeaderText="Postulante"/>
                                    <asp:BoundField DataField="CentroCosto" HeaderText="Centro Costo"/>
                                    <asp:BoundField DataField="escuela" HeaderText="Car. Prof."/>
                                    <asp:BoundField DataField="nombre_gru" HeaderText="Grupo Admisión"/>
                                    <asp:BoundField DataField="password_Alu" HeaderText="Clave"/>
                                    <asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnQuitar" runat="server" CommandName="Quitar" 
                                                CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                                                CssClass="btn btn-danger btn-sm" ToolTip = "Quitar"
                                                Visible='<%# IIF(Eval("codigo_gva")<>-1,"True","False") %>'>
                                                <span><i class="fa fa-trash"></i></span>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate> No se encontró ningun registro </EmptyDataTemplate>
                                <HeaderStyle BackColor="#E33439" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                                <RowStyle Font-Size="11px" />
                                <EditRowStyle BackColor="#FFFFCC" />
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
