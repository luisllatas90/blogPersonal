<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FrmConfiguraSolicitud.aspx.vb" Inherits="administrativo_SISREQ_SisSolicitudes_FrmConfiguraSolicitud" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../../../scripts/js/jquery-1.12.3.min.js" type="text/javascript"></script>
    <link href="../../../scripts/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <script src="../../../scripts/js/bootstrap.min.js" type="text/javascript"></script>
    
    <meta http-equiv='X-UA-Compatible' content='IE=edge'/>
    <script type="text/javascript" language="javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
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

        function openModal() {
            $('#myModal').modal('show');
        }

        function closeModal() {
            $('#myModal').modal('hide');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container-fluid">                        
        <div class="messagealert" id="alert_container">            
        </div>
        <br />
        <div class="row">
            <div class="col-md-3">
                Buscar:
                <asp:DropDownList ID="cboTipoEstudio" runat="server" Width="70%"></asp:DropDownList>
            </div>
            
            <div class="col-md-5">                
                <asp:TextBox ID="txtDescripcion" runat="server" Width="95%"></asp:TextBox>                
            </div>                            
            <div class="col-md-4">
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" Width="100px" class="btn btn-info btn-sm"  />&nbsp;&nbsp;
                <asp:Button ID="btnAgregar" runat="server" Text="Agregar" Width="100px" class="btn btn-danger btn-sm"/> 
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <br />
                <asp:GridView ID="gvCarreras" runat="server" Width="99%" 
                    CssClass="table table-bordered bs-table" AutoGenerateColumns="False" >
                    <Columns>
                        <asp:BoundField DataField="codigo_rso" HeaderText="ID" />
                        <asp:BoundField DataField="descripcion_test" HeaderText="TIPO ESTUDIO" />
                        <asp:BoundField DataField="nombre_Cpf" HeaderText="CARRERA PROFESIONAL" />
                        <asp:BoundField DataField="Personal" HeaderText="RESPONSABLE" />                        
                    </Columns>
                    <EmptyDataTemplate>
                        No se encontraron registros!
                    </EmptyDataTemplate>      
                    <HeaderStyle BackColor="#D9534F" ForeColor="White" VerticalAlign="Middle" HorizontalAlign="Center" Font-Size="12px" />
                    <RowStyle Font-Size="11px" />
                    <EditRowStyle BackColor="#ffffcc" />
                    <EmptyDataRowStyle forecolor="Red" CssClass="table table-bordered" />
                </asp:GridView>             
            </div>           
        </div>
    </div>
    <!-- Modal -->
    <div id="myModal" class="modal fade" role="dialog">            
        <div class="modal-dialog">
        <!-- Modal content-->
            <div class="modal-content" id="modalFinalizaBody">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">REGISTRO DE EVALUADOR</h4>
                </div>
                <div class="modal-body">
                <p>
                    <div class="form-group">
                        <label for="TipoEstudio">Tipo de Estudio</label> <br />
                        <asp:DropDownList ID="cboTipoEstudio2" runat="server" class="btn btn-default dropdown-toggle" AutoPostBack="true" >
                        </asp:DropDownList>
                        <!-- <input type="text" class="form-control" id="Text1" placeholder="Enter email"> -->
                    </div>
                    <div class="form-group">
                      <label for="usrname">Carrera Profesional</label><br />
                        <asp:DropDownList ID="cboCarrera" runat="server" class="btn btn-default dropdown-toggle">
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="psw">Tipo Personal</label><br />
                        <asp:DropDownList ID="cboTipoPersonal" runat="server" class="btn btn-default dropdown-toggle" AutoPostBack="true" >
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label for="psw">Personal</label> <br />
                        <asp:DropDownList ID="cboPersonal" runat="server" class="btn btn-default dropdown-toggle" >
                        </asp:DropDownList>
                    </div>
                </p>
                </div>
                <div class="modal-footer">        
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-info" />
                    <button type="button" id="btnSalir" class="btn btn-danger" data-dismiss="modal" >Salir</button>                     
                </div>
            </div>
        </div>
    </div> 
    </form>
</body>
</html>
