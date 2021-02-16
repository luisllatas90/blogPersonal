<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmAdminSilabosFC.aspx.vb" Inherits="academico_silabos_FrmAdminSilabosFC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administrar s&iacute;labo</title>

   <%-- <script src="../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../scripts/js/bootstrap.min.js" type="text/javascript"></script>--%>

<%--    <link href="../../scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">--%>
    
    
     <!-- Scripts externos -->
    <script src="../assets/jquery/jquery-3.3.1.js"></script>
    <script src="../assets/bootstrap-4.1/js/bootstrap.bundle.js"></script>
    <script src="../assets/bootstrap-select-1.13.1/js/bootstrap-select.js"></script>
    <script src="../assets/bootstrap-datepicker/js/bootstrap-datepicker.js"></script>
    <script src="../assets/bootstrap-datepicker/locales/bootstrap-datepicker.es.min.js"></script>
    <script src="../assets/iframeresizer/iframeResizer.min.js"></script>
    <script src="../assets/fileDownload/jquery.fileDownload.js"></script>
    <!-- Scripts propios -->
    
       
    <link rel="stylesheet" href="../assets/bootstrap-4.1/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/smart-wizard/css/smart_wizard.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-select-1.13.1/css/bootstrap-select.min.css">
    <link rel="stylesheet" href="../assets/bootstrap-datepicker/css/bootstrap-datepicker.min.css">
    <link rel="stylesheet" href="../assets/fontawesome-5.2/css/all.min.css">
    
    <style type="text/css" >
             .bootstrap-select .dropdown-toggle .filter-option
        {
            position: relative;
            padding-top: 0px;
            padding-bottom: 0px;
            padding-left: 0px;
        }

    </style>
    <script type="text/javascript">

        $(document).ready(function() { 
        
         $('#cboCarrera').selectpicker({size: 10});
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


        function openModal(nombre) {
            $('#' + nombre).modal('show');
        }

        function closeModal(confirm) {
            if (confirm) {
                $('#modalArchivo').modal('hide');
                $("#modalArchivo").remove();

                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $(".modal-dialog").remove();
                $(".modal").remove();
            }
        }
        
    </script>
</head>
<body>
    <form id="form1" class="form form-horizontal" runat="server">        
        <div class="messagealert" id="alert_container">      
        </div>               
        <br />
        <div class="container-fluid">            
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <h5>S&iacute;labos de Cursos Programados - Filtro de B&uacute;squeda </h5>
                </div>
                <div class="panel-body">              
                  <div class="card">
                        <div class="card-body">
                         <div class="row"> 
                               <label for="cboSemestre" class="col-sm-2 col-form-label form-control-sm">Semestre:</label>                                                                             
                               <div class="col-sm-2">
                                    <asp:DropDownList ID="cboSemestre" runat="server" CssClass="form-control" 
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                               </div>
                               
                               <label for="cboCarrera" class="col-sm-2 col-form-label form-control-sm">Carrera Profesional:</label>                                                                             
                               <div class="col-sm-4">
                                    <asp:DropDownList ID="cboCarrera" runat="server" CssClass="form-control" data-live-search="true" > 
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-2">
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-success" />
                                </div>                              
                         </div>
                         
                       </div>
                  </div>
               
                  
					<br>
                    <div class="row" >
                        <div class="col-md-12">
                            <asp:Label ID="lblMensaje" CssClass="form-control alert-danger" runat="server" Text=""></asp:Label>
                            
                        </div>
                    </div>
                </div>    
                    <div class="panel-body">
                    
                        <div class="table-responsive">
                        <asp:HiddenField ID="lblnum" runat="server" />
                            <asp:GridView ID="gvCursos" runat="server" Width="100%" DataKeyNames="codigo_cup,codigo_dis,IdArchivo_Anexo,codigo_sil,nombre_cur"  CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False">
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
                                    <asp:BoundField DataField="silabo_cup" HeaderText="silabo_cup" /> 
                                    <asp:BoundField DataField="codigo_sil" HeaderText="codigo_sil" /> 
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                                                                       
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField>
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkAgregar" CssClass="btn btn-default btn-xs"  runat="server" CommandName="Agregar"   CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" >                                                
                                                <asp:Image ID="Image1" runat="server" ImageUrl="img/add.png" />
                                            </asp:LinkButton>
                                             <asp:LinkButton ID="lnkVer"  CssClass="btn btn-default btn-xs" runat="server" CommandName="descargar"   CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" Visible=false >
                                                <asp:Image ID="Image2" runat="server" ImageUrl="img/search.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkEliminar"  CssClass="btn btn-default btn-xs" runat="server" CommandName="Eliminar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" >                                                
                                                <asp:Image ID="Image3" runat="server" ImageUrl="img/delete.png" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="lnkCancelar"  CssClass="btn btn-default btn-xs" runat="server" Enabled=false  CommandName="Cancelar" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" >                                                
                                                <asp:Image ID="Image4" runat="server" ImageUrl="img/cancel.png" />
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
        <asp:HiddenField ID="HdCup" runat="server" />
        <asp:HiddenField ID="HdCiclo" runat="server" />
            <div id="modalArchivo" class="modal fade" tabindex="-1" role="dialog" data-postback-listar="true"
            data-backdrop="static" data-keyboard="false" aria-hidden="true" runat="server">
            <div class="modal-dialog modal-lg" role="document">
            <!-- Modal content -->
            <div class="modal-content">
                <div class="modal-header" style="background-color: #E33439; color: White; font-weight: bold;">
                    <span class="modal-title" id="divTit" runat="server">Subir S&iacute;labo</span>
                </div>
                <div class="modal-body">
                    <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <label class="col-sm-4" for="FileUploadControl">
                                        Adjunte Sílabo:</label>
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FileUploadControl" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="row">
                            <div class="col-xs-12 col-sm-12">
                                <div class="form-group row">
                                    <label class="col-sm-4" for="FileUploadControl">
                                        Nota:</label>
                                        <div class="col-sm-8">
                                            <label style="text-align:justify">Debe tomar en cuenta que el tamaño del archivo no sobrepase los 300 Kb y debe ser empaquetado (.ZIP), caso contrario el sistema no le admitirá subir el archivo. Recuerde tomar en cuenta estas indicaciones para que el estudiante pueda descargar con facilidad el archivo.</label>
                                        </div>
                                </div>
                            </div>
                        </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="btnAceptar" runat="server" CssClass="btn btn-primary" Text="Subir" OnClick="btnAceptar_Click">
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnCancelar" runat="server" CssClass="btn btn-danger" Text="Cancelar" OnClick="btnCancelar_Click">
                    </asp:LinkButton> <!-- OnClientClick="closeModal(true);"  -->
                </div>
            
         </div>
            </div>
         </div>
    </form>
</body>
</html>
