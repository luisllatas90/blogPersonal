<%@ Page Language="VB" AutoEventWireup="false" CodeFile="vstsilabos.aspx.vb" Inherits="academico_silabos_vstsilabos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Administrar s&iacute;labo</title>

    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <link href="../../scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <style type="text/css" >
        .form-group {
            display: flex;
            align-items: center;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {

            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case '1':
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

        function DescargarArchivo(IdArchivo) {
            window.open("DescargarArchivo.aspx?Id=" + IdArchivo, 'ta', "");
        }
    </script>
</head>
<body>
    <form id="form1" class="form form-horizontal" runat="server">        
        <div class="messagealert" id="alert_container"></div>
        <br />
        <div class="container-fluid">            
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>S&iacute;labos de Cursos Programados - Filtro de Busqueda </h5>
                </div>
                <div class="panel-body">
                    <div class="row">                                            
                        <div class="col-md-1 text-right">
                            Semestre
                        </div>
                        <div class="col-md-2">
                            <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control" 
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3 text-right">
                            Carrera Profesional
                        </div>                                                
                        <div class="col-md-4">
                            <asp:DropDownList ID="cboCarrera" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" />
                        </div>
                    </div>
                    <div class="row" style="display:none">
                        <div class="col-md-11">
                            <asp:Label ID="lblMensaje" CssClass="form-control alert-danger" runat="server" Text=""></asp:Label>
                        </div>
                    </div>
                </div>    
                    <div class="panel-body">
                    
                        <div class="table-responsive">
                        <asp:HiddenField ID="lblnum" runat="server" />
                            <asp:GridView ID="gvCursos" runat="server" Width="100%" CssClass="table table-bordered bs-table datatable" 
                                DataKeyNames="codigo_cup,codigo_dis,IdArchivo_Anexo,codigo_sil" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="codigo_cup" HeaderText="ID"/>
                                    <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                                    <asp:BoundField DataField="tipo_cur" HeaderText="Tipo" />
                                    <asp:BoundField DataField="identificador_cur" HeaderText="Código" />
                                    <asp:BoundField DataField="nombre_cur" HeaderText="Nombre del Curso" />
                                    <asp:BoundField DataField="creditos_cur" HeaderText="Créditos" />
                                    <asp:BoundField DataField="totalhoras_cur" HeaderText="TH" />
                                    <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" />
                                    <asp:BoundField DataField="profesor_cup" HeaderText="Docente" />
                                    <asp:BoundField DataField="codigo_dis" HeaderText="" />
                                    <asp:BoundField DataField="IdArchivo_Anexo" HeaderText=""/>
                                    <asp:BoundField DataField="silabo_cup" HeaderText=""  /> 
                                    <asp:BoundField DataField="codigo_sil" HeaderText="" /> 
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                          
                                            <asp:LinkButton ID="lnkVer"  runat="server" CommandName="descargar"   CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" >                                                
                                                <asp:Image ID="Image2" runat="server" ImageUrl="img/search.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkVerAnt"  runat="server" CommandName="descargarAnt" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" >                                                
                                                <asp:Image ID="ImageAnt" runat="server" ImageUrl="img/.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    No se encontraron registros!
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                    Font-Size="11px" />
                                <RowStyle Font-Size="11px" />
                                <EditRowStyle BackColor="#ffffcc" />
                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            </asp:GridView>
                        </div>
                    
                </div>
            </div>    
        </div>
        <asp:HiddenField ID="HdCronograma" runat="server" />
    </form>
</body>
</html>

