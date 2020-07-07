<%@ Page Language="VB" AutoEventWireup="false" CodeFile="miscursos.aspx.vb" Inherits="academico_notas_profesor_miscursos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">
        <div class="messagealert" id="alert_container">
        </div>        
        <br />
        <div class="panel panel-default">
            <div class="panel-heading"><h4>REGISTRO DE NOTAS FINALES</h4></div>
            <div class="panel-body">
                <div class="col-md-1"> 
                    Semestre 
                </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cboCiclo" runat="server" CssClass="form-control" >
                    </asp:DropDownList>    
                </div>
                <div class="col-md-1"> 
                    Docente 
                </div>
                <div class="col-md-6">
                    <asp:DropDownList ID="cboDocente" runat="server" CssClass="form-control" >
                    </asp:DropDownList>
                </div>        
                <div class="col-md-2">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" />
                </div>
            </div>
        </div>
        
        <br />        
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <div class="table-responsive">            
                        <asp:GridView ID="gvCursos" runat="server" AutoGenerateColumns="False" Width="100%" 
                            CssClass="table table-bordered bs-table datatable" 
                            DataKeyNames="codigo_cup,codigo_aut,codigo_test,modular_pcu,codigo_cpf,refrecuperacion_cup">
                            <Columns>
                                <asp:BoundField DataField="codigo_cup" HeaderText="codigo_cup" Visible="false" />
                                <asp:BoundField DataField="nombre_cur" HeaderText="Asignatura" />
                                <asp:BoundField DataField="ciclo_cur" HeaderText="Ciclo" />
                                <asp:BoundField DataField="nombre_cpf" HeaderText="Carrera Profesional" />
                                <asp:BoundField DataField="grupohor_cup" HeaderText="Grupo" />
                                <asp:BoundField DataField="matriculados" HeaderText="Matriculados" />
                                <asp:BoundField DataField="retirados" HeaderText="Retirados" />
                                <asp:BoundField DataField="estadonota_cup" HeaderText="Estado Reg. Nota" />
                                <asp:BoundField DataField="totalhorasaula" HeaderText="Hrs. Clase" />
                                <asp:BoundField DataField="totalhorasasesoria" HeaderText="Hrs. Asesoría" />
                                <asp:BoundField DataField="totalhoras_car" HeaderText="Total Hrs." />                                
                                <asp:BoundField DataField="codigo_aut" Visible="False" />
                                <asp:BoundField DataField="codigo_Test" HeaderText="codigo_test"  Visible="False" />
                                <asp:BoundField DataField="modular_pcu" HeaderText="modular_pcu"   Visible="False" />
                                <asp:BoundField DataField="codigo_cpf" HeaderText="codigo_cpf"   Visible="False" />
                                <asp:BoundField DataField="refrecuperacion_cup" HeaderText="refrecuperacion_cup"   Visible="False" />
                                   
                                <asp:CommandField ShowSelectButton="True" />
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
    </div>    
    </form>
</body>
</html>
