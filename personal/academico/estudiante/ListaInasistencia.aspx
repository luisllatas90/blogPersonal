<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaInasistencia.aspx.vb" Inherits="academico_estudiante_ListaInasistencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!--<meta http-equiv="X-UA-Compatible" content="IE=8; IE=9; " />-->
    <title>Alumnos Inhabilitados</title>    
    <link href="../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>    
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Errors':
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

        $(document).ready(function() {
            $("#chkTodos").on("click", function() {
                var condiciones = $("#chkTodos").is(":checked");
                $('input[type="checkbox"]').prop('checked', condiciones);
                /*
                if (condiciones == true) {                    
                    $('input[type="checkbox"]').prop('checked', this.checked);
                }
                */
            });

            $("#chkTodosAnula").on("click", function() {
                var condiciones = $("#chkTodosAnula").is(":checked");
                $('input[type="checkbox"]').prop('checked', condiciones);
                /*
                if (condiciones == true) {                    
                $('input[type="checkbox"]').prop('checked', this.checked);
                }
                */
            });
        });

        $('input[type=checkbox]:checked').on('change', function() {
            console.log("Checkbox " + $(this).prop("id") + " (" + $(this).val() + ") => Deseleccionado");
        });
    </script>
</head>
<body>    
    <form id="form1" class="form-horizontal" runat="server">   
        <div class="container-fluid">                                                    
            <!--
            <button type="button" class="btn btn-default glyphicon glyphicon-asterisk" data-toggle="collapse" data-target="#aviso">                
            </button>            
            <div id="aviso" class="panel-collapse collapse">
                Usar Google Chrome
            </div>
            -->        
            <div class="messagealert" id="alert_container">                
            </div> <br />          
            <div class="panel with-nav-tabs panel-default">
		        <div class="panel-heading">
    				<ul class="nav nav-tabs">
					    <li id="tab1"><a href="#tab1default" data-toggle="tab">Inhabilitar</a></li>
					    <li id="tab2"><a href="#tab2default" data-toggle="tab">Anular Inhabilitaci&oacute;n</a></li>					
				    </ul>
		        </div>
		        <div class="panel-body">
			        <div class="tab-content">
                        <div class="tab-pane fade" id="tab1default">
				            <div class="panel panel-default">
                              <div class="panel-heading">Alumnos Inhabilitados - Filtros de B&uacute;squeda</div>
                              <div class="panel-body">
                                <div class="form-group">  
                                    <div class="col-md-2">
                                        Carrera Profesional 
                                    </div>        
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="cboCarrera" runat="server" CssClass="form-control">
                                        </asp:DropDownList>    
                                    </div>        
                                    <div class="col-md-2">
                                        Semestre académico 
                                    </div>
                                    <div class="col-md-3">
                                    <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control" 
                                            AutoPostBack="True">
                                    </asp:DropDownList>
                                    </div>                
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4">
                                    Cod. Univ./DNI/Apellidos y Nombres
                                    </div>
                                    <div class="col-md-6">
                                    <asp:TextBox ID="txtAlumno" runat="server" CssClass="form-control"></asp:TextBox>                    
                                    </div>
                                    <div class="col-md-2">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" />
                                    </div>                
                                </div>
                              </div>
                            </div>
                            
                            <div class="panel panel-default">
                                <div class="panel-body">                    
                                    <div class="pull-right">
                                        <asp:Button ID="btnInactivar" runat="server" Text="Inhabilitar" CssClass="btn btn-primary" /> <br />
                                        <%--<asp:CheckBox ID="chkTodos" runat="server" Text="Todos" AutoPostBack="True" />--%>
                                        <input id="chkTodos" type="checkbox"/> Seleccionar Todos
                                    </div>                    
                                </div>                
                                <div class="panel-body">
                                    <asp:GridView ID="gvDatos" runat="server" Width="100%" DataKeyNames="codigo_dma" 
                                        CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_dma" HeaderText="ID DET" Visible="False" />
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                                            <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                                            <asp:BoundField DataField="Curso" HeaderText="CURSO" />
                                            <asp:BoundField DataField="NroSesiones" HeaderText="SESIONES" />
                                            <asp:BoundField DataField="NroMaxFaltas" HeaderText="FALTAS PERMT." />
                                            <asp:BoundField DataField="NroFaltas" HeaderText="FALTAS" />
                                            <asp:BoundField DataField="nroFaltasJusti" HeaderText="JUSTIFICADAS" />
                                            <asp:BoundField DataField="SesionesAula" HeaderText="SES. AULA VIRT." />
                                            <asp:TemplateField HeaderText="INHABILITAR">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAccept" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>0 registros!</EmptyDataTemplate>
                                         <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                                            Font-Size="11px" />
                                        <RowStyle Font-Size="11px" />
                                        <EditRowStyle BackColor="#ffffcc" />
                                        <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    </asp:GridView>
                                </div>
                            </div>          
				        
				        </div>
				        
				        <div class="tab-pane fade" id="tab2default">
				            <div class="panel panel-default">
				            <div class="panel-heading">Anula Inhabilitaciones - Filtros de B&uacute;squeda</div>
                              <div class="panel-body">
                                <div class="form-group">  
                                    <div class="col-md-2">
                                        Carrera Profesional 
                                    </div>        
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="cboCarreraAnula" runat="server" CssClass="form-control">
                                        </asp:DropDownList>    
                                    </div>        
                                    <div class="col-md-2">
                                        Semestre académico 
                                    </div>
                                    <div class="col-md-3">
                                    <asp:DropDownList ID="cboSemestreAnula" runat="server" CssClass="form-control" 
                                            AutoPostBack="True">
                                    </asp:DropDownList>
                                    </div>                
                                </div>
                                <div class="form-group">
                                    <div class="col-md-4">
                                    Cod. Univ./DNI/Apellidos y Nombres
                                    </div>
                                    <div class="col-md-6">
                                    <asp:TextBox ID="txtAlumnoAnula" runat="server" CssClass="form-control"></asp:TextBox>                    
                                    </div>
                                    <div class="col-md-2">
                                    <asp:Button ID="btnBuscarAnula" runat="server" Text="Buscar" CssClass="btn btn-success" />
                                    </div>                
                                </div>
                              </div>
				            </div>
				            
				            <div class="panel panel-default">
                                <div class="panel-body">                    
                                    <div class="pull-right">
                                         <asp:Button ID="btnAnula" runat="server" Text="Anula Inhabilitación" CssClass="btn btn-primary" /><br />
                                        <%--<asp:CheckBox ID="chkTodos" runat="server" Text="Todos" AutoPostBack="True" />--%>
                                        <input id="chkTodosAnula" type="checkbox"/> Seleccionar Todos
                                    </div>                    
                                </div>                
                                <div class="panel-body">
                                     <asp:GridView ID="gvAnula" runat="server" Width="100%" DataKeyNames="codigo_dma" 
                                        CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField DataField="codigo_dma" HeaderText="ID DET" Visible="False" />
                                            <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD. UNIV." />
                                            <asp:BoundField DataField="Estudiante" HeaderText="ESTUDIANTE" />
                                            <asp:BoundField DataField="Curso" HeaderText="CURSO" />                                            
                                            <asp:TemplateField HeaderText="HABILITAR">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkAcceptAnula" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>0 registros!</EmptyDataTemplate>
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
	        </div>
            
              
                        
        </div>
    </form>
</body>
</html>
