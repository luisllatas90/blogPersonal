<%@ Page Language="VB" AutoEventWireup="false" CodeFile="lstalumnosmatriculados.aspx.vb" Inherits="academico_notas_administrarconsultar_lstalumnosmatriculados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    <script src="../../../assets/bootstrap-toggle/js/bootstrap-toggle.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.min.css" rel="stylesheet" type="text/css" />    
    <link href="../../../assets/bootstrap-toggle/css/bootstrap-toggle.min.css" rel="stylesheet" type="text/css" />    
    <style>
        body .btn-danger2:active, body .btn-danger2:visited, body .btn-danger2:active:focus, body .btn-danger2:active:hover {
            border-color: #639d34;
            background-color: #639d34;
            color: #FFF; }
            .GvGrid:hover
        {
            background-color: #FEFDC3  ;
          
        }
    </style>
    <script type="text/javascript">
        function SoloNotas(obj) {
            /*
            if (event.keyCode < 48 || event.keyCode > 57) {
                if (event.keyCode != 46) {
                    event.returnValue = false;
                }
            }     */
            if (event.keyCode >= 48 && event.keyCode <= 57) {
                return true;
            }
            return false; 
        }

        function verificaDato(obj) {
            if (isNaN($(obj).val()) == true) {
                $(obj).val("0");
            }
            if ($(obj).val() > 20) {
                $(obj).val("0");
            }            
        }

        function verificaDatoEstado(obj) {
            if (isNaN($(obj).val()) == true) {
                $(obj).val("0");
            }
            if ($(obj).val() > 20) {
                $(obj).val("0");

            }
            if ($(obj).val() >= 14) {
                $("#lblCondicionNueva").text("Aprobado");
            } else {
                $("#lblCondicionNueva").text("Desaprobado");
            }
        }
        
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

        function openModal(MD) {
            $('#'+MD).modal('show');
        }

        function closeModal(MD) {
            $('#' + MD).modal('hide');
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
                <div class="panel-heading"><h4 id="hTitulo" runat="server">REGISTRO DE NOTAS</h4></div>
                <div class="panel-body">
                    <div class="col-md-2">Curso: </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblCurso" runat="server" Text="-"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <span class="pull-right">
                            <asp:LinkButton ID="btnDescargarActa" runat="server" CssClass="btn btn-warning" Text='<i class="fa fa-download""></i> Descargar Acta de Notas' />
                            <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="btn btn-info"/>
                        </span>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="col-md-2"> Docente: </div>
                    <div class="col-md-6">
                        <asp:Label ID="lblDocente" runat="server" Text="No encontrado"></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <span class="pull-right">
                            <asp:CheckBox ID="chkConfirmarPublicacion" runat="server" Text="Confirmo generación de Actas de Notas" 
                                    OnCheckedChanged="chkConfirmarPublicacion_ChekedChanged" AutoPostBack="true"/>
                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClientClick="return confirm('¿Desea guardar las notas?');"/> 
                        </span>
                    </div>
                </div>
            </div>

            <asp:GridView ID="gvAlumnos" runat="server" DataKeyNames="codigo_dma,inhabilitado_dma" 
                CssClass="table table-bordered bs-table datatable" AutoGenerateColumns="False" RowStyle-CssClass="GvGrid">
                <Columns>
                    <asp:BoundField DataField="codigo_dma" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="codigo_Alu" HeaderText="Nro" />
                    <asp:BoundField DataField="codigoUniver_Alu" HeaderText="COD.UNIV" />
                    <asp:BoundField DataField="nombre_cpf" HeaderText="CARRERA PROFESIONAL" />
                    <asp:BoundField DataField="alumno" HeaderText="ALUMNO" />
                    <%--<asp:BoundField DataField="notafinal_dma" HeaderText="NOTA FINAL" />--%>
                    
                    <asp:TemplateField HeaderText=" NOTA FINAL" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:TextBox ID="txtNota" runat="server" Text='<%# Bind("notafinal_dma") %>' onkeypress="return SoloNotas(this)" onkeyUp="verificaDato(this)" 
                                Visible='<%# HdEsFormacionComplementaria.Value = "N" %>'  />
                            <%--andy.daz 31/07/2020--%>
                            <asp:Panel ID="divAcredita" runat="server" Visible='<%# HdEsFormacionComplementaria.Value = "S" %>'>
                                <strong><asp:Label ID="lblAcredita" runat="server" /></strong>
                                <input type="checkbox" data-toggle="toggle" data-on="Si" data-off="No" data-size="mini" data-onstyle="primary" data-offstyle="danger" id="chkAcredita" runat="server">
                            </asp:Panel>
                            
                        </ItemTemplate>
                        <ItemStyle Width="50px" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="condicion_dma" HeaderText="CONDICION" />
                    <asp:BoundField DataField="estado_dma" HeaderText="ESTADO" />
                    <asp:BoundField DataField="inhabilitado_dma" HeaderText="INHABILITADO" 
                        Visible="False" />
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:Button ID="btnModificarNota" runat="server" Text="Modificar Nota" CssClass="btn btn-primary" 
                             CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" 
                             CommandName="ModificarNota"
                            
                             />
                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>No se encontraron registros!</EmptyDataTemplate>
                <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center"
                    Font-Size="11px" />
                <RowStyle Font-Size="11px" />
                <EditRowStyle BackColor="#ffffcc" />
                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
            </asp:GridView>        
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                         <button type="button" class="btn btn-primary"><span runat="server" id="spTextAprobados">Aprobados</span> <span class="badge" runat="server" id="spAprobados">0</span></button>
                         <button type="button" class="btn btn-danger"><span runat="server" id="spTextDesaprobados">Desaprobados</span> <span class="badge" runat="server" id="spDesaprobados">0</span></button>
                         <button type="button" class="btn btn-default">Retirados <span class="badge" runat="server" id="spRetirados">0</span></button>
                         <button type="button" class="btn btn-warning" id="btnInhabilitados" runat="server">Inhabilitados <span class="badge" runat="server" id="spInhabilitados">0</span></button>
                    </div>                    
                    <div class="col-md-2"></div>
                </div>
            </div>
    </div>
    <asp:HiddenField ID="HdCodigoCup" runat="server" />
    <asp:HiddenField ID="HdEstadoCurso" runat="server" />
    <asp:HiddenField ID="HdEsRecuperacion" runat="server" />
    <asp:HiddenField ID="HdEsFormacionComplementaria" runat="server" />

  <!-- Modal -->
<div id="myModal"  class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Modificar Nota</h4>
      </div>
      <div class="modal-body">
        <table>
        <tr>
            <td rowspan="7"><asp:Image ID="FotoAlumno" Width="100px" Height="120px" runat="server" style="margin:15px;" /> </td>
        </tr>
        <tr><td><b>Código</b></td>
            <td colspan="4"><asp:Label ID="lblCodigo" runat="server" Text=""></asp:Label></td></tr>
        <tr><td><b>Estudiante</b></td>
            <td colspan="4">
            <asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></td></tr>
        <tr>
            <td><b>Nota Anterior: </b></td><td> <asp:Label ID="lblNotaAnterior" runat="server" Text=""></asp:Label></td>
            <td><b>Condición Anterior: </b></td><td><asp:Label ID="lblCondicionAnterior" runat="server" Text=""></asp:Label></td>
          </tr>
        <tr><td><br /> </td></tr>
        <tr>
        
            <td><b>Nota Nueva</b></td><td><asp:TextBox ID="txtNotaNueva" Width="35px" runat="server" onkeypress="SoloNotas(this)" onkeyUp="verificaDatoEstado(this)">0</asp:TextBox></td>
            <td><b>Condición Nueva</b></td><td><asp:Label ID="lblCondicionNueva" runat="server" Text="Desaprobado"></asp:Label> </td>
         </tr>
        <tr>
            <td colspan="4"><b>Indique el motivo de cambio de nota:</b><br />
            <asp:TextBox ID="txtMotivo" runat="server" Columns="50"></asp:TextBox></td>
        </tr>
        
        </table>
      </div>
      <div class="modal-footer">
        <asp:Button ID="btnGuardarCambio"  class="btn btn-primary" runat="server" Text="Guardar" />
        <asp:Button ID="BtnCerrar" class="btn btn-default" runat="server" Text="Cancelar" data-dismiss="modal" />
      </div>
    </div>

  </div>
</div>

    </form>
      

</body>
</html>
